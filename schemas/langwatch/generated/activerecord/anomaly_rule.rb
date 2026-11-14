class AnomalyRule < ApplicationRecord
  self.table_name = "AnomalyRule"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :thresholdConfig, default: -> { JSON.parse("{}") }
  attribute :destinationConfig, default: -> { JSON.parse("{}") }
  attribute :status, default: "active"
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :scope, presence: true
  validates :scopeId, presence: true
  validates :name, presence: true, uniqueness: { scope: :organizationId }
  validates :severity, presence: true
  validates :ruleType, presence: true

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :anomaly_rules
  belongs_to :created_by, class_name: "User", foreign_key: "createdById", optional: true,
                          inverse_of: :anomaly_rules_created
  has_many :alerts, class_name: "AnomalyAlert", foreign_key: "ruleId", inverse_of: :rule, dependent: :destroy
end
