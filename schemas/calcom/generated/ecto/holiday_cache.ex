defmodule Calcom.HolidayCache do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          country_code: String.t(),
          calendar_id: String.t(),
          event_id: String.t(),
          name: String.t(),
          date: DateTime.t(),
          year: integer(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "HolidayCache" do
    field(:country_code, :string, source: :countryCode)
    field(:calendar_id, :string, source: :calendarId)
    field(:event_id, :string, source: :eventId)
    field(:name, :string)
    field(:date, Calcom.PrismaDateTime)
    field(:year, :integer)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end