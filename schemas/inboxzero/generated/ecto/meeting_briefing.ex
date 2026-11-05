defmodule InboxZero.MeetingBriefing do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          calendar_event_id: String.t(),
          event_title: String.t(),
          event_start_time: DateTime.t(),
          guest_count: integer(),
          status: atom(),
          inserted_at: DateTime.t(),
          email_account: InboxZero.EmailAccount.t() | nil
        }

  schema "MeetingBriefing" do
    field(:id, :string, primary_key: true)
    field(:calendar_event_id, :string, source: :calendarEventId)
    field(:event_title, :string, source: :eventTitle)
    field(:event_start_time, InboxZero.PrismaDateTime, source: :eventStartTime)
    field(:guest_count, :integer, source: :guestCount)
    field(:status, Ecto.Enum, values: [:PENDING, :SENT, :FAILED, :SKIPPED])
    field(:email_account_id, :string, source: :emailAccountId)
    belongs_to(:email_account, InboxZero.EmailAccount, foreign_key: :email_account_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end