class Meeting < ApplicationRecord
  self.table_name = "Meeting"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :processingAttempts, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :processingStatus, { pending: "PENDING", processing: "PROCESSING", completed: "COMPLETED", failed: "FAILED" },
       default: :pending, validate: true

  validates :calendarEventId, presence: true, uniqueness: { scope: :emailAccountId }
  validates :eventTitle, presence: true
  validates :startTime, presence: true
  validates :endTime, presence: true
  validates :attendees, exclusion: { in: [ nil ] }
  validates :emailAccountId, uniqueness: { scope: :recordingId, conditions: -> { where.not(recordingId: nil) } }

  belongs_to :recording, class_name: "MeetingRecording", foreign_key: "recordingId", optional: true,
                         inverse_of: :meetings
  belongs_to :email_account, foreign_key: "emailAccountId", inverse_of: :meetings
end
