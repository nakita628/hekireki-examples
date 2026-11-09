defmodule Jitsu.WorkspaceAccess do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Jitsu.PrismaDateTime, autogenerate: {Jitsu.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          workspace_id: String.t(),
          user_id: String.t(),
          role: atom(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          workspace: Jitsu.Workspace.t() | nil,
          user: Jitsu.UserProfile.t() | nil
        }

  schema "WorkspaceAccess" do
    field(:role, Ecto.Enum, values: [:owner, :editor, :analyst], default: :owner)
    field(:workspace_id, :string, primary_key: true, source: :workspaceId)
    field(:user_id, :string, primary_key: true, source: :userId)
    belongs_to(:workspace, Jitsu.Workspace, foreign_key: :workspace_id, define_field: false, type: :string)
    belongs_to(:user, Jitsu.UserProfile, foreign_key: :user_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end