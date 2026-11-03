class Booking < ApplicationRecord
  self.table_name = "Booking"

  attribute :paid, default: false
  attribute :scheduledJobs, default: -> { [] }
  attribute :isRecorded, default: false
  attribute :iCalUID, default: ""
  attribute :iCalSequence, default: 0
  attribute :noShowHost, default: false
  attribute :oneTimePassword, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :status, { cancelled: "cancelled", accepted: "accepted", rejected: "rejected", pending: "pending", awaiting_host: "awaiting_host" },
       default: :accepted, validate: true
  enum :creationSource, { api_v1: "api_v1", api_v2: "api_v2", webapp: "webapp" }, validate: { allow_nil: true }

  validates :uid, presence: true, uniqueness: true
  validates :idempotencyKey, uniqueness: { allow_nil: true }
  validates :title, presence: true
  validates :startTime, presence: true
  validates :endTime, presence: true
  validates :oneTimePassword, uniqueness: { allow_nil: true }

  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :bookings
  belongs_to :event_type, foreign_key: "eventTypeId", optional: true, inverse_of: :bookings
  belongs_to :destination_calendar, foreign_key: "destinationCalendarId", optional: true, inverse_of: :booking
  belongs_to :reassign_by, class_name: "User", foreign_key: "reassignById", optional: true,
                           inverse_of: :reassigned_bookings
  has_one :instant_meeting_token, foreign_key: "bookingId", inverse_of: :booking, dependent: :destroy
  has_one :tracking, foreign_key: "bookingId", inverse_of: :booking, dependent: :destroy
  has_one :report, class_name: "BookingReport", foreign_key: "bookingUid", primary_key: "uid", inverse_of: :booking,
                   dependent: :destroy
  has_many :references, class_name: "BookingReference", foreign_key: "bookingId", inverse_of: :booking,
                        dependent: :destroy
  has_many :attendees, foreign_key: "bookingId", inverse_of: :booking, dependent: :destroy
  has_many :payment, foreign_key: "bookingId", inverse_of: :booking, dependent: :destroy
  has_many :seats_references, class_name: "BookingSeat", foreign_key: "bookingId", inverse_of: :booking,
                              dependent: :destroy
  has_many :scheduled_triggers, class_name: "WebhookScheduledTriggers", foreign_key: "bookingId", inverse_of: :booking,
                                dependent: :destroy
  has_many :assignment_reason, foreign_key: "bookingId", inverse_of: :booking, dependent: :destroy
  has_many :internal_note, class_name: "BookingInternalNote", foreign_key: "bookingId", inverse_of: :booking,
                           dependent: :destroy
  has_many :expense_logs, class_name: "CreditExpenseLog", foreign_key: "bookingUid", primary_key: "uid",
                          inverse_of: :booking, dependent: :destroy
  has_many :wrong_assignment_reports, foreign_key: "bookingUid", primary_key: "uid", inverse_of: :booking,
                                      dependent: :destroy
end
