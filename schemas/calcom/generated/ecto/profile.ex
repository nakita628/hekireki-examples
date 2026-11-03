defmodule Calcom.Profile do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          uid: String.t(),
          username: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Calcom.User.t() | nil,
          organization: Calcom.Team.t() | nil,
          moved_from_user: Calcom.User.t() | nil,
          event_types: [Calcom.EventType.t()]
        }

  schema "Profile" do
    field(:uid, :string)
    field(:username, :string)
    field(:user_id, :id, source: :userId)
    field(:organization_id, :id, source: :organizationId)
    belongs_to(:user, Calcom.User, foreign_key: :user_id, define_field: false)
    belongs_to(:organization, Calcom.Team, foreign_key: :organization_id, define_field: false)
    has_one(:moved_from_user, Calcom.User, foreign_key: :moved_to_profile_id)
    has_many(:event_types, Calcom.EventType, foreign_key: :profile_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end