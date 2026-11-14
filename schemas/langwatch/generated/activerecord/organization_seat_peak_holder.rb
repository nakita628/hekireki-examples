class OrganizationSeatPeakHolder < ApplicationRecord
  self.table_name = "OrganizationSeatPeakHolder"
  self.primary_key = %w[organizationId holderKind holderId]

  attribute :expiredByFold, default: false
  attribute :restoredByFold, default: false

  validates :seat, presence: true
  validates :lastChangedAt, presence: true
end
