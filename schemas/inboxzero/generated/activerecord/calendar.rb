class Calendar < ApplicationRecord
  self.table_name = "Calendar"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :primary, default: false
  attribute :isEnabled, default: true
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :calendarId, presence: true, uniqueness: { scope: :connectionId }
  validates :name, presence: true

  belongs_to :connection, class_name: "CalendarConnection", foreign_key: "connectionId", inverse_of: :calendars
  has_many :booking_links, foreign_key: "destinationCalendarId", inverse_of: :destination_calendar, dependent: :nullify
end
