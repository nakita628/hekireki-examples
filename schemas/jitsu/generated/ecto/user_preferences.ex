defmodule Jitsu.UserPreferences do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Jitsu.PrismaDateTime, autogenerate: {Jitsu.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          scope: String.t() | nil,
          preferences: map(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Jitsu.UserProfile.t() | nil,
          workspace: Jitsu.Workspace.t() | nil
        }

  schema "UserPreferences" do
    field(:id, :string, primary_key: true)
    field(:scope, :string)
    field(:preferences, :map)
    field(:user_id, :string, source: :userId)
    field(:workspace_id, :string, source: :workspaceId)
    belongs_to(:user, Jitsu.UserProfile, foreign_key: :user_id, define_field: false, type: :string)
    belongs_to(:workspace, Jitsu.Workspace, foreign_key: :workspace_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end