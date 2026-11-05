class BookingLink < ApplicationRecord
  self.table_name = "BookingLink"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :isActive, default: true
  attribute :minimumNoticeMinutes, default: 120
  attribute :maxDaysAhead, default: 90
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :locationType, { in_person: "IN_PERSON", google_meet: "GOOGLE_MEET", microsoft_teams: "MICROSOFT_TEAMS", phone: "PHONE", custom: "CUSTOM" },
       default: :custom, validate: true

  validates :slug, presence: true, uniqueness: true
  validates :title, presence: true
  validates :durationMinutes, presence: true
  validates :slotIntervalMinutes, presence: true
  validates :emailAccountId, uniqueness: true

  belongs_to :email_account, foreign_key: "emailAccountId", inverse_of: :booking_links
  belongs_to :destination_calendar, class_name: "Calendar", foreign_key: "destinationCalendarId", optional: true,
                                    inverse_of: :booking_links
  belongs_to :availability_schedule, foreign_key: [ :availabilityScheduleId, :emailAccountId ],
                                     primary_key: [ :id, :emailAccountId ], inverse_of: :booking_links
  has_many :bookings, foreign_key: "bookingLinkId", inverse_of: :booking_link, dependent: :destroy
end
