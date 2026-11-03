defmodule Calcom.Membership do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          accepted: boolean(),
          role: atom(),
          inserted_at: DateTime.t() | nil,
          updated_at: DateTime.t() | nil,
          custom_role: Calcom.Role.t() | nil,
          team: Calcom.Team.t() | nil,
          user: Calcom.User.t() | nil,
          attribute_to_user: [Calcom.AttributeToUser.t()],
          host: [Calcom.Host.t()]
        }

  schema "Membership" do
    field(:accepted, :boolean, default: false)
    field(:role, Ecto.Enum, values: [:MEMBER, :ADMIN, :OWNER])
    field(:custom_role_id, :string, source: :customRoleId)
    field(:team_id, :id, source: :teamId)
    field(:user_id, :id, source: :userId)
    belongs_to(:custom_role, Calcom.Role, foreign_key: :custom_role_id, define_field: false, type: :string)
    belongs_to(:team, Calcom.Team, foreign_key: :team_id, define_field: false)
    belongs_to(:user, Calcom.User, foreign_key: :user_id, define_field: false)
    has_many(:attribute_to_user, Calcom.AttributeToUser, foreign_key: :member_id)
    has_many(:host, Calcom.Host, foreign_key: :member_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end