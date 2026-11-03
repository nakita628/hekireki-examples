defmodule Formbricks.WorkspaceTeam do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Formbricks.PrismaDateTime, autogenerate: {Formbricks.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          workspace_id: String.t(),
          team_id: String.t(),
          permission: atom(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          workspace: Formbricks.Workspace.t() | nil,
          team: Formbricks.Team.t() | nil
        }

  schema "WorkspaceTeam" do
    field(:permission, Ecto.Enum, values: [:read, :readWrite, :manage], default: :read)
    field(:workspace_id, :string, primary_key: true, source: :workspaceId)
    field(:team_id, :string, primary_key: true, source: :teamId)
    belongs_to(:workspace, Formbricks.Workspace, foreign_key: :workspace_id, define_field: false, type: :string)
    belongs_to(:team, Formbricks.Team, foreign_key: :team_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end