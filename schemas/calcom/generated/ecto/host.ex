defmodule Calcom.Host do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          user_id: term(),
          event_type_id: term(),
          is_fixed: boolean(),
          priority: integer() | nil,
          weight: integer() | nil,
          weight_adjustment: integer() | nil,
          inserted_at: DateTime.t(),
          user: Calcom.User.t() | nil,
          event_type: Calcom.EventType.t() | nil,
          schedule: Calcom.Schedule.t() | nil,
          group: Calcom.HostGroup.t() | nil,
          member: Calcom.Membership.t() | nil
        }

  schema "Host" do
    field(:is_fixed, :boolean, default: false, source: :isFixed)
    field(:priority, :integer)
    field(:weight, :integer)
    field(:weight_adjustment, :integer, source: :weightAdjustment)
    field(:user_id, :id, primary_key: true, source: :userId)
    field(:event_type_id, :id, primary_key: true, source: :eventTypeId)
    field(:schedule_id, :id, source: :scheduleId)
    field(:group_id, :binary_id, source: :groupId)
    field(:member_id, :id, source: :memberId)
    belongs_to(:user, Calcom.User, foreign_key: :user_id, define_field: false)
    belongs_to(:event_type, Calcom.EventType, foreign_key: :event_type_id, define_field: false)
    belongs_to(:schedule, Calcom.Schedule, foreign_key: :schedule_id, define_field: false)
    belongs_to(:group, Calcom.HostGroup, foreign_key: :group_id, define_field: false, type: :binary_id)
    belongs_to(:member, Calcom.Membership, foreign_key: :member_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end