defmodule Formbricks.Team do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Formbricks.PrismaDateTime, autogenerate: {Formbricks.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: Formbricks.Organization.t() | nil,
          team_users: [Formbricks.TeamUser.t()],
          workspace_teams: [Formbricks.WorkspaceTeam.t()]
        }

  schema "Team" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:organization_id, :string, source: :organizationId)
    belongs_to(:organization, Formbricks.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    has_many(:team_users, Formbricks.TeamUser, foreign_key: :team_id)
    has_many(:workspace_teams, Formbricks.WorkspaceTeam, foreign_key: :team_id)
    timestamps(inserted_at_source: :created_at)
  end
end