defmodule LangWatch.Workflow do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          icon: String.t(),
          description: String.t(),
          published_id: String.t() | nil,
          archived_at: DateTime.t() | nil,
          is_evaluator: boolean(),
          is_component: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: LangWatch.Project.t() | nil,
          latest_version: LangWatch.WorkflowVersion.t() | nil,
          current_version: LangWatch.WorkflowVersion.t() | nil,
          published_by: LangWatch.User.t() | nil,
          copied_from: LangWatch.Workflow.t() | nil,
          versions: [LangWatch.WorkflowVersion.t()],
          experiments: [LangWatch.Experiment.t()],
          copied_workflows: [LangWatch.Workflow.t()],
          agents: [LangWatch.Agent.t()],
          evaluators: [LangWatch.Evaluator.t()]
        }

  schema "Workflow" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:icon, :string)
    field(:description, :string)
    field(:published_id, :string, source: :publishedId)
    field(:archived_at, LangWatch.PrismaDateTime, source: :archivedAt)
    field(:is_evaluator, :boolean, default: false, source: :isEvaluator)
    field(:is_component, :boolean, default: false, source: :isComponent)
    field(:project_id, :string, source: :projectId)
    field(:latest_version_id, :string, source: :latestVersionId)
    field(:current_version_id, :string, source: :currentVersionId)
    field(:published_by_id, :string, source: :publishedById)
    field(:copied_from_workflow_id, :string, source: :copiedFromWorkflowId)
    belongs_to(:project, LangWatch.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:latest_version, LangWatch.WorkflowVersion, foreign_key: :latest_version_id, define_field: false, type: :string)
    belongs_to(:current_version, LangWatch.WorkflowVersion, foreign_key: :current_version_id, define_field: false, type: :string)
    belongs_to(:published_by, LangWatch.User, foreign_key: :published_by_id, define_field: false, type: :string)
    belongs_to(:copied_from, LangWatch.Workflow, foreign_key: :copied_from_workflow_id, define_field: false, type: :string)
    has_many(:versions, LangWatch.WorkflowVersion, foreign_key: :workflow_id, references: :id)
    has_many(:experiments, LangWatch.Experiment, foreign_key: :workflow_id, references: :id)
    has_many(:copied_workflows, LangWatch.Workflow, foreign_key: :copied_from_workflow_id, references: :id)
    has_many(:agents, LangWatch.Agent, foreign_key: :workflow_id, references: :id)
    has_many(:evaluators, LangWatch.Evaluator, foreign_key: :workflow_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end