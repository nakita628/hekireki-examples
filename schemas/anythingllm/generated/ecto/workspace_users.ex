defmodule AnythingLLM.WorkspaceUsers do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: AnythingLLM.PrismaDateTime, autogenerate: {AnythingLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          last_updated_at: DateTime.t(),
          inserted_at: DateTime.t(),
          workspaces: AnythingLLM.Workspaces.t() | nil,
          users: AnythingLLM.Users.t() | nil
        }

  schema "workspace_users" do
    field(:last_updated_at, AnythingLLM.PrismaDateTime, autogenerate: true, source: :lastUpdatedAt)
    belongs_to(:workspaces, AnythingLLM.Workspaces, foreign_key: :workspace_id)
    belongs_to(:users, AnythingLLM.Users, foreign_key: :user_id)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end