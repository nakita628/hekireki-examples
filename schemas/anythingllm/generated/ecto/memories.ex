defmodule AnythingLLM.Memories do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: AnythingLLM.PrismaDateTime, autogenerate: {AnythingLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          scope: String.t(),
          content: String.t(),
          last_used_at: DateTime.t() | nil,
          updated_at: DateTime.t(),
          inserted_at: DateTime.t(),
          user: AnythingLLM.Users.t() | nil,
          workspace: AnythingLLM.Workspaces.t() | nil
        }

  schema "memories" do
    field(:scope, :string, default: "workspace")
    field(:content, :string)
    field(:last_used_at, AnythingLLM.PrismaDateTime)
    field(:updated_at, AnythingLLM.PrismaDateTime, autogenerate: true)
    belongs_to(:user, AnythingLLM.Users, foreign_key: :user_id)
    belongs_to(:workspace, AnythingLLM.Workspaces, foreign_key: :workspace_id)
    timestamps(inserted_at_source: :created_at, updated_at: false)
  end
end