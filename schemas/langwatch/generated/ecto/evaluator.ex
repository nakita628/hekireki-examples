defmodule LangWatch.Evaluator do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          slug: String.t() | nil,
          type: String.t(),
          config: map(),
          archived_at: DateTime.t() | nil,
          created_by_id: String.t() | nil,
          updated_by_id: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: LangWatch.Project.t() | nil,
          workflow: LangWatch.Workflow.t() | nil,
          copied_from: LangWatch.Evaluator.t() | nil,
          copied_evaluators: [LangWatch.Evaluator.t()],
          monitors: [LangWatch.Monitor.t()],
          gateway_guardrails: [LangWatch.GatewayGuardrail.t()]
        }

  schema "Evaluator" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:slug, :string)
    field(:type, :string)
    field(:config, :map)
    field(:archived_at, LangWatch.PrismaDateTime, source: :archivedAt)
    field(:created_by_id, :string, source: :createdById)
    field(:updated_by_id, :string, source: :updatedById)
    field(:project_id, :string, source: :projectId)
    field(:workflow_id, :string, source: :workflowId)
    field(:copied_from_evaluator_id, :string, source: :copiedFromEvaluatorId)
    belongs_to(:project, LangWatch.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:workflow, LangWatch.Workflow, foreign_key: :workflow_id, define_field: false, type: :string)
    belongs_to(:copied_from, LangWatch.Evaluator, foreign_key: :copied_from_evaluator_id, define_field: false, type: :string)
    has_many(:copied_evaluators, LangWatch.Evaluator, foreign_key: :copied_from_evaluator_id, references: :id)
    has_many(:monitors, LangWatch.Monitor, foreign_key: :evaluator_id, references: :id)
    has_many(:gateway_guardrails, LangWatch.GatewayGuardrail, foreign_key: :evaluator_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end