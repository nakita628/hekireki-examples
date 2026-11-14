defmodule LangWatch.AgentVersion do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          project_id: String.t(),
          agent_id: String.t(),
          version: integer(),
          name: String.t(),
          type: String.t(),
          config: map(),
          workflow_id: String.t() | nil,
          commit_message: String.t() | nil,
          author_id: String.t() | nil,
          author_kind: String.t() | nil,
          inserted_at: DateTime.t()
        }

  schema "AgentVersion" do
    field(:id, :string, primary_key: true)
    field(:project_id, :string, source: :projectId)
    field(:agent_id, :string, source: :agentId)
    field(:version, :integer)
    field(:name, :string)
    field(:type, :string)
    field(:config, :map)
    field(:workflow_id, :string, source: :workflowId)
    field(:commit_message, :string, source: :commitMessage)
    field(:author_id, :string, source: :authorId)
    field(:author_kind, :string, source: :authorKind)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end