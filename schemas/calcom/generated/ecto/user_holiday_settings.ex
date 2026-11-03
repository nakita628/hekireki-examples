defmodule Calcom.UserHolidaySettings do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          country_code: String.t() | nil,
          disabled_ids: [String.t()],
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Calcom.User.t() | nil
        }

  schema "UserHolidaySettings" do
    field(:country_code, :string, source: :countryCode)
    field(:disabled_ids, {:array, :string}, source: :disabledIds)
    field(:user_id, :id, source: :userId)
    belongs_to(:user, Calcom.User, foreign_key: :user_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end