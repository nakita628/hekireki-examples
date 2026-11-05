class MeetingRecording < ApplicationRecord
  self.table_name = "MeetingRecording"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :botProvider, default: "recall"
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :status, { pending: "PENDING", scheduled: "SCHEDULED", cancelling: "CANCELLING", joining: "JOINING", in_waiting_room: "IN_WAITING_ROOM", in_call: "IN_CALL", recording: "RECORDING", call_ended: "CALL_ENDED", done: "DONE", failed: "FAILED", cancelled: "CANCELLED" },
       default: :pending, validate: true

  validates :botProvider, uniqueness: { scope: :externalBotId, conditions: -> { where.not(externalBotId: nil) } }
  validates :meetingUrl, presence: true
  validates :normalizedMeetingUrl, presence: true
  validates :activeKey, uniqueness: { scope: :meetingStartTime, allow_nil: true }
  validates :meetingStartTime, presence: true

  belongs_to :email_account, foreign_key: "emailAccountId", inverse_of: :meeting_recordings
  has_many :meetings, foreign_key: "recordingId", inverse_of: :recording, dependent: :nullify
end
