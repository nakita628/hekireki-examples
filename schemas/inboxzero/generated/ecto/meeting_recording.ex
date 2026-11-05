defmodule InboxZero.MeetingRecording do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          bot_provider: String.t(),
          external_bot_id: String.t() | nil,
          bot_name: String.t() | nil,
          external_recording_id: String.t() | nil,
          transcript_requested_at: DateTime.t() | nil,
          external_transcript_id: String.t() | nil,
          meeting_url: String.t(),
          normalized_meeting_url: String.t(),
          active_key: String.t() | nil,
          meeting_start_time: DateTime.t(),
          status: atom(),
          failure_reason: String.t() | nil,
          transcript: map() | nil,
          transcript_fetched_at: DateTime.t() | nil,
          media_deleted_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          email_account: InboxZero.EmailAccount.t() | nil,
          meetings: [InboxZero.Meeting.t()]
        }

  schema "MeetingRecording" do
    field(:id, :string, primary_key: true)
    field(:bot_provider, :string, default: "recall", source: :botProvider)
    field(:external_bot_id, :string, source: :externalBotId)
    field(:bot_name, :string, source: :botName)
    field(:external_recording_id, :string, source: :externalRecordingId)
    field(:transcript_requested_at, InboxZero.PrismaDateTime, source: :transcriptRequestedAt)
    field(:external_transcript_id, :string, source: :externalTranscriptId)
    field(:meeting_url, :string, source: :meetingUrl)
    field(:normalized_meeting_url, :string, source: :normalizedMeetingUrl)
    field(:active_key, :string, source: :activeKey)
    field(:meeting_start_time, InboxZero.PrismaDateTime, source: :meetingStartTime)
    field(:status, Ecto.Enum, values: [:PENDING, :SCHEDULED, :CANCELLING, :JOINING, :IN_WAITING_ROOM, :IN_CALL, :RECORDING, :CALL_ENDED, :DONE, :FAILED, :CANCELLED], default: :PENDING)
    field(:failure_reason, :string, source: :failureReason)
    field(:transcript, :map)
    field(:transcript_fetched_at, InboxZero.PrismaDateTime, source: :transcriptFetchedAt)
    field(:media_deleted_at, InboxZero.PrismaDateTime, source: :mediaDeletedAt)
    field(:email_account_id, :string, source: :emailAccountId)
    belongs_to(:email_account, InboxZero.EmailAccount, foreign_key: :email_account_id, define_field: false, type: :string)
    has_many(:meetings, InboxZero.Meeting, foreign_key: :recording_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end