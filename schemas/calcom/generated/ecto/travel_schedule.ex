defmodule Calcom.TravelSchedule do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}

  @type t :: %__MODULE__{
          id: integer(),
          time_zone: String.t(),
          start_date: DateTime.t(),
          end_date: DateTime.t() | nil,
          prev_time_zone: String.t() | nil,
          user: Calcom.User.t() | nil
        }

  schema "TravelSchedule" do
    field(:time_zone, :string, source: :timeZone)
    field(:start_date, Calcom.PrismaDateTime, source: :startDate)
    field(:end_date, Calcom.PrismaDateTime, source: :endDate)
    field(:prev_time_zone, :string, source: :prevTimeZone)
    field(:user_id, :id, source: :userId)
    belongs_to(:user, Calcom.User, foreign_key: :user_id, define_field: false)
  end
end