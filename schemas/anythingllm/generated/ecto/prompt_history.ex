defmodule AnythingLLM.PromptHistory do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}

  @type t :: %__MODULE__{
          id: integer(),
          prompt: String.t(),
          modified_at: DateTime.t(),
          workspace: AnythingLLM.Workspaces.t() | nil,
          user: AnythingLLM.Users.t() | nil
        }

  schema "prompt_history" do
    field(:prompt, :string)
    field(:modified_at, AnythingLLM.PrismaDateTime, autogenerate: true, source: :modifiedAt)
    field(:workspace_id, :id, source: :workspaceId)
    field(:modified_by, :id, source: :modifiedBy)
    belongs_to(:workspace, AnythingLLM.Workspaces, foreign_key: :workspace_id, define_field: false)
    belongs_to(:user, AnythingLLM.Users, foreign_key: :modified_by, define_field: false)
  end
end