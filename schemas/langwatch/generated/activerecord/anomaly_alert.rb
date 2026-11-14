class AnomalyAlert < ApplicationRecord
  self.table_name = "AnomalyAlert"

  attribute :id, default: -> { Nanoid.generate }
  attribute :detail, default: -> { JSON.parse("{}") }
  attribute :state, default: "open"
  attribute :destinationStatus, default: -> { JSON.parse("{}") }

  validates :severity, presence: true
  validates :ruleName, presence: true
  validates :ruleType, presence: true
  validates :triggerWindowStart, presence: true, uniqueness: { scope: :ruleId }
  validates :triggerWindowEnd, presence: true

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :anomaly_alerts
  belongs_to :rule, class_name: "AnomalyRule", foreign_key: "ruleId", inverse_of: :alerts
end
