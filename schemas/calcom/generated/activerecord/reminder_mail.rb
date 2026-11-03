class ReminderMail < ApplicationRecord
  self.table_name = "ReminderMail"

  alias_attribute :created_at, :createdAt

  enum :reminderType, { pending_booking_confirmation: "PENDING_BOOKING_CONFIRMATION" }, validate: true

  validates :referenceId, presence: true
  validates :elapsedMinutes, presence: true
end
