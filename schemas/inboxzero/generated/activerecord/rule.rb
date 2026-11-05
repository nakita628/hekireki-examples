class Rule < ApplicationRecord
  self.table_name = "Rule"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :enabled, default: true
  attribute :automate, default: true
  attribute :runOnThreads, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :conditionalOperator, { and: "AND", or: "OR" }, default: :and, prefix: true, validate: true
  enum :categoryFilterType, { include: "INCLUDE", exclude: "EXCLUDE" }, prefix: true, validate: { allow_nil: true }
  enum :systemType, { to_reply: "TO_REPLY", fyi: "FYI", awaiting_reply: "AWAITING_REPLY", actioned: "ACTIONED", cold_email: "COLD_EMAIL", newsletter: "NEWSLETTER", marketing: "MARKETING", calendar: "CALENDAR", receipt: "RECEIPT", notification: "NOTIFICATION", otp: "OTP" },
       validate: { allow_nil: true }

  validates :id, uniqueness: { scope: :emailAccountId }
  validates :name, presence: true, uniqueness: { scope: :emailAccountId }
  validates :emailAccountId, uniqueness: { scope: :systemType, conditions: -> { where.not(systemType: nil) } }
  validates :emailAccountId,
            uniqueness: { scope: :organizationRuleId, conditions: -> { where.not(organizationRuleId: nil) } }
  validates :groupId, uniqueness: { allow_nil: true }

  belongs_to :email_account, foreign_key: "emailAccountId", inverse_of: :rules
  belongs_to :organization_rule, foreign_key: "organizationRuleId", optional: true, inverse_of: :member_rules
  belongs_to :group, foreign_key: "groupId", optional: true, inverse_of: :rule
  has_many :actions, foreign_key: [ :ruleId, :emailAccountId ], primary_key: [ :id, :emailAccountId ], inverse_of: :rule,
                     dependent: :destroy
  has_many :attachment_sources, foreign_key: "ruleId", inverse_of: :rule, dependent: :destroy
  has_many :executed_rules, foreign_key: "ruleId", inverse_of: :rule, dependent: :nullify
  has_many :classification_feedback, foreign_key: "ruleId", inverse_of: :rule, dependent: :destroy
  has_many :history, class_name: "RuleHistory", foreign_key: "ruleId", inverse_of: :rule, dependent: :destroy
  has_and_belongs_to_many :category_filters, class_name: "Category", join_table: "_CategoryToRule", foreign_key: "B",
                                             association_foreign_key: "A"
end
