defmodule InboxZero.Calendar do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          calendar_id: String.t(),
          name: String.t(),
          description: String.t() | nil,
          primary: boolean(),
          is_enabled: boolean(),
          timezone: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          connection: InboxZero.CalendarConnection.t() | nil,
          booking_links: [InboxZero.BookingLink.t()]
        }

  schema "Calendar" do
    field(:id, :string, primary_key: true)
    field(:calendar_id, :string, source: :calendarId)
    field(:name, :string)
    field(:description, :string)
    field(:primary, :boolean, default: false)
    field(:is_enabled, :boolean, default: true, source: :isEnabled)
    field(:timezone, :string)
    field(:connection_id, :string, source: :connectionId)
    belongs_to(:connection, InboxZero.CalendarConnection, foreign_key: :connection_id, define_field: false, type: :string)
    has_many(:booking_links, InboxZero.BookingLink, foreign_key: :destination_calendar_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end