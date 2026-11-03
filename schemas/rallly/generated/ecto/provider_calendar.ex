defmodule Rallly.ProviderCalendar do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Rallly.PrismaDateTime, autogenerate: {Rallly.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          provider_calendar_id: String.t(),
          name: String.t(),
          is_primary: boolean(),
          time_zone: String.t() | nil,
          is_selected: boolean(),
          is_deleted: boolean(),
          is_writable: boolean(),
          provider_data: map() | nil,
          last_synced_at: DateTime.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          calendar_connection: Rallly.CalendarConnection.t() | nil,
          user_default_destination: [Rallly.User.t()]
        }

  schema "provider_calendars" do
    field(:id, :string, primary_key: true)
    field(:provider_calendar_id, :string)
    field(:name, :string)
    field(:is_primary, :boolean, default: false, source: :primary)
    field(:time_zone, :string)
    field(:is_selected, :boolean, default: false, source: :selected)
    field(:is_deleted, :boolean, default: false, source: :deleted)
    field(:is_writable, :boolean, default: false, source: :writable)
    field(:provider_data, :map)
    field(:last_synced_at, Rallly.PrismaDateTime, autogenerate: true)
    belongs_to(:calendar_connection, Rallly.CalendarConnection, foreign_key: :calendar_connection_id, type: :string)
    has_many(:user_default_destination, Rallly.User, foreign_key: :default_destination_calendar_id)
    timestamps(inserted_at_source: :created_at)
  end
end