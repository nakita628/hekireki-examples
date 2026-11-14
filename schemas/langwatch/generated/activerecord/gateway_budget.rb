class GatewayBudget < ApplicationRecord
  self.table_name = "GatewayBudget"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :metadata, default: -> { JSON.parse("{}") }
  attribute :spentUsd, default: BigDecimal("0")
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :scopeType, { organization: "ORGANIZATION", team: "TEAM", project: "PROJECT", virtual_key: "VIRTUAL_KEY", principal: "PRINCIPAL", group: "GROUP", attributed_user: "ATTRIBUTED_USER" },
       prefix: true, validate: true
  enum :window, { minute: "MINUTE", hour: "HOUR", day: "DAY", week: "WEEK", month: "MONTH", total: "TOTAL", manual: "MANUAL" },
       validate: true
  enum :onBreach, { block: "BLOCK", warn: "WARN" }, default: :block, prefix: true, validate: true

  validates :organizationId, presence: true,
                             uniqueness: { scope: :externalId, conditions: -> { where.not(externalId: nil) } }
  validates :scopeId, presence: true
  validates :name, presence: true
  validates :limitUsd, presence: true
  validates :resetsAt, presence: true

  belongs_to :created_by, class_name: "User", foreign_key: "createdById", inverse_of: :gateway_budgets_created
  has_many :ledger_entries, class_name: "GatewayBudgetLedger", foreign_key: "budgetId", inverse_of: :budget,
                            dependent: :destroy
  has_many :bucket_boundaries, class_name: "GatewayBudgetBucketBoundary", foreign_key: "budgetId", inverse_of: :budget,
                               dependent: :destroy
end
