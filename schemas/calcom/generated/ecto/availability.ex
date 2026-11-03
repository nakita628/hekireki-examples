defmodule Calcom.Availability do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}

  @type t :: %__MODULE__{
          id: integer(),
          days: [integer()],
          start_time: Time.t(),
          end_time: Time.t(),
          date: Date.t() | nil,
          user: Calcom.User.t() | nil,
          event_type: Calcom.EventType.t() | nil,
          schedule: Calcom.Schedule.t() | nil
        }

  schema "Availability" do
    field(:days, {:array, :integer})
    field(:start_time, Calcom.PrismaTime, source: :startTime)
    field(:end_time, Calcom.PrismaTime, source: :endTime)
    field(:date, Calcom.PrismaDate)
    field(:user_id, :id, source: :userId)
    field(:event_type_id, :id, source: :eventTypeId)
    field(:schedule_id, :id, source: :scheduleId)
    belongs_to(:user, Calcom.User, foreign_key: :user_id, define_field: false)
    belongs_to(:event_type, Calcom.EventType, foreign_key: :event_type_id, define_field: false)
    belongs_to(:schedule, Calcom.Schedule, foreign_key: :schedule_id, define_field: false)
  end
end