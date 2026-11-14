class TraceIngestSourceBilling < ApplicationRecord
  self.table_name = "TraceIngestSourceBilling"
  self.primary_key = %w[organizationId sourceType]

  alias_attribute :updated_at, :updatedAt

  validates :billed, inclusion: { in: [ true, false ] }
  validates :recordedAt, presence: true
end
