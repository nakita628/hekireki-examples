class HolidayCache < ApplicationRecord
  self.table_name = "HolidayCache"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :countryCode, presence: true, uniqueness: { scope: :eventId }
  validates :calendarId, presence: true
  validates :eventId, presence: true
  validates :name, presence: true
  validates :date, presence: true
  validates :year, presence: true
end
