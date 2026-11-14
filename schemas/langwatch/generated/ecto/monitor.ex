defmodule LangWatch.Monitor do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          check_type: String.t(),
          name: String.t(),
          slug: String.t(),
          execution_mode: atom(),
          enabled: boolean(),
          preconditions: map(),
          parameters: map(),
          mappings: map() | nil,
          sample: float(),
          level: String.t(),
          thread_idle_timeout: integer() | nil,
          auto_paused_at: DateTime.t() | nil,
          auto_paused_reason: String.t() | nil,
          evaluator_label: String.t() | nil,
          created_by_id: String.t() | nil,
          updated_by_id: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: LangWatch.Project.t() | nil,
          experiment: LangWatch.Experiment.t() | nil,
          evaluator: LangWatch.Evaluator.t() | nil
        }

  schema "Monitor" do
    field(:id, :string, primary_key: true)
    field(:check_type, :string, source: :checkType)
    field(:name, :string)
    field(:slug, :string)
    field(:execution_mode, Ecto.Enum, values: [:ON_MESSAGE, :AS_GUARDRAIL, :MANUALLY], default: :ON_MESSAGE, source: :executionMode)
    field(:enabled, :boolean, default: true)
    field(:preconditions, :map)
    field(:parameters, :map)
    field(:mappings, :map)
    field(:sample, :float, default: 1.0)
    field(:level, :string, default: "trace")
    field(:thread_idle_timeout, :integer, source: :threadIdleTimeout)
    field(:auto_paused_at, LangWatch.PrismaDateTime, source: :autoPausedAt)
    field(:auto_paused_reason, :string, source: :autoPausedReason)
    field(:evaluator_label, :string, source: :evaluatorLabel)
    field(:created_by_id, :string, source: :createdById)
    field(:updated_by_id, :string, source: :updatedById)
    field(:project_id, :string, source: :projectId)
    field(:experiment_id, :string, source: :experimentId)
    field(:evaluator_id, :string, source: :evaluatorId)
    belongs_to(:project, LangWatch.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:experiment, LangWatch.Experiment, foreign_key: :experiment_id, define_field: false, type: :string)
    belongs_to(:evaluator, LangWatch.Evaluator, foreign_key: :evaluator_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end