defmodule Calcom.Role do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          color: String.t() | nil,
          description: String.t() | nil,
          type: atom(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          team: Calcom.Team.t() | nil,
          permissions: [Calcom.RolePermission.t()],
          memberships: [Calcom.Membership.t()]
        }

  schema "Role" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:color, :string)
    field(:description, :string)
    field(:type, Ecto.Enum, values: [:SYSTEM, :CUSTOM], default: :CUSTOM)
    field(:team_id, :id, source: :teamId)
    belongs_to(:team, Calcom.Team, foreign_key: :team_id, define_field: false)
    has_many(:permissions, Calcom.RolePermission, foreign_key: :role_id)
    has_many(:memberships, Calcom.Membership, foreign_key: :custom_role_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end