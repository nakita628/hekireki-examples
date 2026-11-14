class BillingMeterCheckpoint < ApplicationRecord
  self.table_name = "BillingMeterCheckpoint"

  attribute :id, default: -> { Cuid.generate }
  attribute :meter, default: "langwatch_billable_events"
  attribute :lastReportedTotal, default: 0
  attribute :consecutiveFailures, default: 0
  alias_attribute :updated_at, :updatedAt

  validates :organizationId, presence: true, uniqueness: { scope: [ :billingMonth, :meter ] }
  validates :billingMonth, presence: true
end
