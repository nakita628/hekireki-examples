class OrganizationSeatPeak < ApplicationRecord
  self.table_name = "OrganizationSeatPeak"
  self.primary_key = "organizationId"

  validates :openedAt, presence: true
  validates :fullSeats, presence: true
  validates :liteSeats, presence: true
  validates :developerSeats, presence: true
  validates :latestChangeAt, presence: true
  validates :mismatches, presence: true
end
