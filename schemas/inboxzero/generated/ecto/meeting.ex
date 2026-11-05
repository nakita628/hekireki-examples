defmodule InboxZero.Meeting do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          calendar_event_id: String.t(),
          event_title: String.t(),
          start_time: DateTime.t(),
          end_time: DateTime.t(),
          attendees: map(),
          organizer_email: String.t() | nil,
          join_override: boolean() | nil,
          processing_status: atom(),
          processing_attempts: integer(),
          summary: map() | nil,
          follow_up_draft_started_at: DateTime.t() | nil,
          follow_up_draft_id: String.t() | nil,
          recap_sent_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          recording: InboxZero.MeetingRecording.t() | nil,
          email_account: InboxZero.EmailAccount.t() | nil
        }

  schema "Meeting" do
    field(:id, :string, primary_key: true)
    field(:calendar_event_id, :string, source: :calendarEventId)
    field(:event_title, :string, source: :eventTitle)
    field(:start_time, InboxZero.PrismaDateTime, source: :startTime)
    field(:end_time, InboxZero.PrismaDateTime, source: :endTime)
    field(:attendees, :map)
    field(:organizer_email, :string, source: :organizerEmail)
    field(:join_override, :boolean, source: :joinOverride)
    field(:processing_status, Ecto.Enum, values: [:PENDING, :PROCESSING, :COMPLETED, :FAILED], default: :PENDING, source: :processingStatus)
    field(:processing_attempts, :integer, default: 0, source: :processingAttempts)
    field(:summary, :map)
    field(:follow_up_draft_started_at, InboxZero.PrismaDateTime, source: :followUpDraftStartedAt)
    field(:follow_up_draft_id, :string, source: :followUpDraftId)
    field(:recap_sent_at, InboxZero.PrismaDateTime, source: :recapSentAt)
    field(:recording_id, :string, source: :recordingId)
    field(:email_account_id, :string, source: :emailAccountId)
    belongs_to(:recording, InboxZero.MeetingRecording, foreign_key: :recording_id, define_field: false, type: :string)
    belongs_to(:email_account, InboxZero.EmailAccount, foreign_key: :email_account_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end