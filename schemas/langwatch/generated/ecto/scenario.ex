defmodule LangWatch.Scenario do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          situation: String.t(),
          criteria: [String.t()],
          labels: [String.t()],
          parameters: map() | nil,
          simulator_model: String.t() | nil,
          judge_model: String.t() | nil,
          max_turns: integer() | nil,
          min_turns: integer() | nil,
          fields: map() | nil,
          caller_voice: map() | nil,
          version: integer(),
          archived_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: LangWatch.Project.t() | nil,
          test_suite: LangWatch.SimulationSuite.t() | nil,
          last_updated_by: LangWatch.User.t() | nil,
          versions: [LangWatch.ScenarioVersion.t()]
        }

  schema "Scenario" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:situation, :string)
    field(:criteria, {:array, :string})
    field(:labels, {:array, :string})
    field(:parameters, :map)
    field(:simulator_model, :string, source: :simulatorModel)
    field(:judge_model, :string, source: :judgeModel)
    field(:max_turns, :integer, source: :maxTurns)
    field(:min_turns, :integer, source: :minTurns)
    field(:fields, :map)
    field(:caller_voice, :map, source: :callerVoice)
    field(:version, :integer, default: 1)
    field(:archived_at, LangWatch.PrismaDateTime, source: :archivedAt)
    field(:project_id, :string, source: :projectId)
    field(:test_suite_id, :string, source: :testSuiteId)
    field(:last_updated_by_id, :string, source: :lastUpdatedById)
    belongs_to(:project, LangWatch.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:test_suite, LangWatch.SimulationSuite, foreign_key: :test_suite_id, define_field: false, type: :string)
    belongs_to(:last_updated_by, LangWatch.User, foreign_key: :last_updated_by_id, define_field: false, type: :string)
    has_many(:versions, LangWatch.ScenarioVersion, foreign_key: :scenario_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end