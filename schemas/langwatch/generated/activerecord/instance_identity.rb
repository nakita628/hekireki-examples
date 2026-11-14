class InstanceIdentity < ApplicationRecord
  self.table_name = "InstanceIdentity"

  attribute :id, default: "self"
  attribute :optionalMetricsOptOut, default: false
  attribute :hostnameOptOut, default: false
  attribute :upgradeDiagnosticsOptOut, default: false
  alias_attribute :created_at, :createdAt

  validates :instanceId, presence: true, uniqueness: true
end
