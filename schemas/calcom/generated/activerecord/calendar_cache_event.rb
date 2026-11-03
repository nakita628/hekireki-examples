class CalendarCacheEvent < ApplicationRecord
  self.table_name = "CalendarCacheEvent"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :iCalSequence, default: 0
  attribute :isAllDay, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :status, { confirmed: "confirmed", tentative: "tentative", cancelled: "cancelled" }, default: :confirmed,
                                                                                            validate: true

  validates :externalId, presence: true, uniqueness: { scope: :selectedCalendarId }
  validates :externalEtag, presence: true
  validates :start, presence: true
  validates :end, presence: true

  belongs_to :selected_calendar, foreign_key: "selectedCalendarId", inverse_of: :calendar_cache_events
end
