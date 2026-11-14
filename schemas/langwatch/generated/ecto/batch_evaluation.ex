defmodule LangWatch.BatchEvaluation do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          data: map(),
          status: String.t(),
          score: float(),
          label: String.t() | nil,
          passed: boolean(),
          details: String.t(),
          cost: float(),
          dataset_slug: String.t(),
          evaluation: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          experiment: LangWatch.Experiment.t() | nil,
          project: LangWatch.Project.t() | nil,
          dataset: LangWatch.Dataset.t() | nil
        }

  schema "BatchEvaluation" do
    field(:id, :string, primary_key: true)
    field(:data, :map, default: %{})
    field(:status, :string)
    field(:score, :float)
    field(:label, :string)
    field(:passed, :boolean)
    field(:details, :string)
    field(:cost, :float)
    field(:dataset_slug, :string, source: :datasetSlug)
    field(:evaluation, :string)
    field(:experiment_id, :string, source: :experimentId)
    field(:project_id, :string, source: :projectId)
    field(:dataset_id, :string, source: :datasetId)
    belongs_to(:experiment, LangWatch.Experiment, foreign_key: :experiment_id, define_field: false, type: :string)
    belongs_to(:project, LangWatch.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:dataset, LangWatch.Dataset, foreign_key: :dataset_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end