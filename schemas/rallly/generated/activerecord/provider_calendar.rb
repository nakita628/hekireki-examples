class ProviderCalendar < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :primary, default: false
  attribute :selected, default: false
  attribute :deleted, default: false
  attribute :writable, default: false

  validates :provider_calendar_id, presence: true, uniqueness: { scope: :calendar_connection_id }
  validates :name, presence: true

  belongs_to :calendar_connection, inverse_of: :provider_calendars
  has_many :user_default_destination, class_name: "User", foreign_key: "default_destination_calendar_id",
                                      inverse_of: :default_destination_calendar, dependent: :nullify
end
