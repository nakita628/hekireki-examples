defmodule LangWatch.Agent do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          type: String.t(),
          config: map(),
          environment: String.t() | nil,
          owner_user_id: String.t() | nil,
          host_label: String.t() | nil,
          identity_key: String.t() | nil,
          last_seen_at: DateTime.t() | nil,
          archived_at: DateTime.t() | nil,
          created_by_id: String.t() | nil,
          updated_by_id: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: LangWatch.Project.t() | nil,
          workflow: LangWatch.Workflow.t() | nil,
          copied_from: LangWatch.Agent.t() | nil,
          copied_agents: [LangWatch.Agent.t()]
        }

  schema "Agent" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:type, :string)
    field(:config, :map)
    field(:environment, :string)
    field(:owner_user_id, :string, source: :ownerUserId)
    field(:host_label, :string, source: :hostLabel)
    field(:identity_key, :string, source: :identityKey)
    field(:last_seen_at, LangWatch.PrismaDateTime, source: :lastSeenAt)
    field(:archived_at, LangWatch.PrismaDateTime, source: :archivedAt)
    field(:created_by_id, :string, source: :createdById)
    field(:updated_by_id, :string, source: :updatedById)
    field(:project_id, :string, source: :projectId)
    field(:workflow_id, :string, source: :workflowId)
    field(:copied_from_agent_id, :string, source: :copiedFromAgentId)
    belongs_to(:project, LangWatch.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:workflow, LangWatch.Workflow, foreign_key: :workflow_id, define_field: false, type: :string)
    belongs_to(:copied_from, LangWatch.Agent, foreign_key: :copied_from_agent_id, define_field: false, type: :string)
    has_many(:copied_agents, LangWatch.Agent, foreign_key: :copied_from_agent_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end