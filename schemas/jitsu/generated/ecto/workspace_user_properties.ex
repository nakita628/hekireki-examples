defmodule Jitsu.WorkspaceUserProperties do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          workspace_id: String.t(),
          user_id: String.t(),
          last_used: DateTime.t(),
          workspace: Jitsu.Workspace.t() | nil,
          user: Jitsu.UserProfile.t() | nil
        }

  schema "WorkspaceUserProperties" do
    field(:last_used, Jitsu.PrismaDateTime, autogenerate: true, source: :lastUsed)
    field(:workspace_id, :string, primary_key: true, source: :workspaceId)
    field(:user_id, :string, primary_key: true, source: :userId)
    belongs_to(:workspace, Jitsu.Workspace, foreign_key: :workspace_id, define_field: false, type: :string)
    belongs_to(:user, Jitsu.UserProfile, foreign_key: :user_id, define_field: false, type: :string)
  end
end