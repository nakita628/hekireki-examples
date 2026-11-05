class OrganizationRule < ApplicationRecord
  self.table_name = "OrganizationRule"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :enabled, default: true
  attribute :runOnThreads, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :conditionalOperator, { and: "AND", or: "OR" }, default: :and, prefix: true, validate: true

  validates :name, presence: true, uniqueness: { scope: :organizationId }

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :rules
  has_many :actions, class_name: "OrganizationRuleAction", foreign_key: "organizationRuleId",
                     inverse_of: :organization_rule, dependent: :destroy
  has_many :member_rules, class_name: "Rule", foreign_key: "organizationRuleId", inverse_of: :organization_rule,
                          dependent: :destroy
end
