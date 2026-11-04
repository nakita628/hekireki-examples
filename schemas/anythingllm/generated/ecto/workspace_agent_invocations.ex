defmodule AnythingLLM.WorkspaceAgentInvocations do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: AnythingLLM.PrismaDateTime, autogenerate: {AnythingLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          uuid: String.t(),
          prompt: String.t(),
          closed: boolean(),
          thread_id: integer() | nil,
          last_updated_at: DateTime.t(),
          inserted_at: DateTime.t(),
          user: AnythingLLM.Users.t() | nil,
          workspace: AnythingLLM.Workspaces.t() | nil
        }

  schema "workspace_agent_invocations" do
    field(:uuid, :string)
    field(:prompt, :string)
    field(:closed, :boolean, default: false)
    field(:thread_id, :integer)
    field(:last_updated_at, AnythingLLM.PrismaDateTime, autogenerate: true, source: :lastUpdatedAt)
    belongs_to(:user, AnythingLLM.Users, foreign_key: :user_id)
    belongs_to(:workspace, AnythingLLM.Workspaces, foreign_key: :workspace_id)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end