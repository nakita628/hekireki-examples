defmodule InboxZero.Schedule do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          interval_days: integer() | nil,
          occurrences: integer() | nil,
          days_of_week: integer() | nil,
          time_of_day: DateTime.t() | nil,
          last_occurrence_at: DateTime.t() | nil,
          next_occurrence_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          email_account: InboxZero.EmailAccount.t() | nil
        }

  schema "Schedule" do
    field(:id, :string, primary_key: true)
    field(:interval_days, :integer, source: :intervalDays)
    field(:occurrences, :integer)
    field(:days_of_week, :integer, source: :daysOfWeek)
    field(:time_of_day, InboxZero.PrismaDateTime, source: :timeOfDay)
    field(:last_occurrence_at, InboxZero.PrismaDateTime, source: :lastOccurrenceAt)
    field(:next_occurrence_at, InboxZero.PrismaDateTime, source: :nextOccurrenceAt)
    field(:email_account_id, :string, source: :emailAccountId)
    belongs_to(:email_account, InboxZero.EmailAccount, foreign_key: :email_account_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end