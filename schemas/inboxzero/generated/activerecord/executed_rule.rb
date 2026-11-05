class ExecutedRule < ApplicationRecord
  self.table_name = "ExecutedRule"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :status, { applied: "APPLIED", applying: "APPLYING", rejected: "REJECTED", pending: "PENDING", skipped: "SKIPPED", error: "ERROR" },
       validate: true

  validates :threadId, presence: true
  validates :messageId, presence: true
  validates :automated, inclusion: { in: [ true, false ] }

  belongs_to :rule, foreign_key: "ruleId", optional: true, inverse_of: :executed_rules
  belongs_to :email_account, foreign_key: "emailAccountId", inverse_of: :executed_rules
  has_many :action_items, class_name: "ExecutedAction", foreign_key: "executedRuleId", inverse_of: :executed_rule,
                          dependent: :destroy
  has_many :scheduled_actions, foreign_key: "executedRuleId", inverse_of: :executed_rule, dependent: :destroy
end
