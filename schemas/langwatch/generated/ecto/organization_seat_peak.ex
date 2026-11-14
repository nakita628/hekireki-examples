defmodule LangWatch.OrganizationSeatPeak do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          organization_id: String.t(),
          opened_at: float(),
          full_seats: integer(),
          lite_seats: integer(),
          developer_seats: integer(),
          latest_change_at: float(),
          mismatches: integer()
        }

  schema "OrganizationSeatPeak" do
    field(:organization_id, :string, primary_key: true, source: :organizationId)
    field(:opened_at, :float, source: :openedAt)
    field(:full_seats, :integer, source: :fullSeats)
    field(:lite_seats, :integer, source: :liteSeats)
    field(:developer_seats, :integer, source: :developerSeats)
    field(:latest_change_at, :float, source: :latestChangeAt)
    field(:mismatches, :integer)
  end
end