defmodule LangWatch.Experiment do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t() | nil,
          type: atom(),
          slug: String.t(),
          archived_at: DateTime.t() | nil,
          workbench_state: map() | nil,
          workbench_version: integer(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: LangWatch.Project.t() | nil,
          workflow: LangWatch.Workflow.t() | nil,
          monitor: LangWatch.Monitor.t() | nil,
          batch_evaluations: [LangWatch.BatchEvaluation.t()]
        }

  schema "Experiment" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:type, Ecto.Enum, values: [:DSPY, :BATCH_EVALUATION, :BATCH_EVALUATION_V2, :EVALUATIONS_V3])
    field(:slug, :string)
    field(:archived_at, LangWatch.PrismaDateTime, source: :archivedAt)
    field(:workbench_state, :map, source: :workbenchState)
    field(:workbench_version, :integer, default: 0, source: :workbenchVersion)
    field(:project_id, :string, source: :projectId)
    field(:workflow_id, :string, source: :workflowId)
    belongs_to(:project, LangWatch.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:workflow, LangWatch.Workflow, foreign_key: :workflow_id, define_field: false, type: :string)
    has_one(:monitor, LangWatch.Monitor, foreign_key: :experiment_id, references: :id)
    has_many(:batch_evaluations, LangWatch.BatchEvaluation, foreign_key: :experiment_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end