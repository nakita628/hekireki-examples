defmodule LangWatch.TeamUser do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          user_id: String.t(),
          team_id: String.t(),
          role: atom(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          assigned_role: LangWatch.CustomRole.t() | nil,
          user: LangWatch.User.t() | nil,
          team: LangWatch.Team.t() | nil
        }

  schema "TeamUser" do
    field(:role, Ecto.Enum, values: [:ADMIN, :MEMBER, :VIEWER, :CUSTOM])
    field(:assigned_role_id, :string, source: :assignedRoleId)
    field(:user_id, :string, primary_key: true, source: :userId)
    field(:team_id, :string, primary_key: true, source: :teamId)
    belongs_to(:assigned_role, LangWatch.CustomRole, foreign_key: :assigned_role_id, define_field: false, type: :string)
    belongs_to(:user, LangWatch.User, foreign_key: :user_id, define_field: false, type: :string)
    belongs_to(:team, LangWatch.Team, foreign_key: :team_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end