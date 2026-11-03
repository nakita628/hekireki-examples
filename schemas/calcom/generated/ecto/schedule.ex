defmodule Calcom.Schedule do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}

  @type t :: %__MODULE__{
          id: integer(),
          name: String.t(),
          time_zone: String.t() | nil,
          user: Calcom.User.t() | nil,
          event_type: [Calcom.EventType.t()],
          instant_meeting_events: [Calcom.EventType.t()],
          restriction_schedule: [Calcom.EventType.t()],
          availability: [Calcom.Availability.t()],
          host: [Calcom.Host.t()]
        }

  schema "Schedule" do
    field(:name, :string)
    field(:time_zone, :string, source: :timeZone)
    field(:user_id, :id, source: :userId)
    belongs_to(:user, Calcom.User, foreign_key: :user_id, define_field: false)
    has_many(:event_type, Calcom.EventType, foreign_key: :schedule_id)
    has_many(:instant_meeting_events, Calcom.EventType, foreign_key: :instant_meeting_schedule_id)
    has_many(:restriction_schedule, Calcom.EventType, foreign_key: :restriction_schedule_id)
    has_many(:availability, Calcom.Availability, foreign_key: :schedule_id)
    has_many(:host, Calcom.Host, foreign_key: :schedule_id)
  end
end