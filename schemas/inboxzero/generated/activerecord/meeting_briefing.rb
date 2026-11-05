class MeetingBriefing < ApplicationRecord
  self.table_name = "MeetingBriefing"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  enum :status, { pending: "PENDING", sent: "SENT", failed: "FAILED", skipped: "SKIPPED" }, validate: true

  validates :calendarEventId, presence: true, uniqueness: { scope: :emailAccountId }
  validates :eventTitle, presence: true
  validates :eventStartTime, presence: true
  validates :guestCount, presence: true

  belongs_to :email_account, foreign_key: "emailAccountId", inverse_of: :meeting_briefings
end
