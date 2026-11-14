defmodule LangWatch.DatasetRecord do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          entry: map(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          dataset: LangWatch.Dataset.t() | nil,
          project: LangWatch.Project.t() | nil
        }

  schema "DatasetRecord" do
    field(:id, :string, primary_key: true)
    field(:entry, :map)
    field(:dataset_id, :string, source: :datasetId)
    field(:project_id, :string, source: :projectId)
    belongs_to(:dataset, LangWatch.Dataset, foreign_key: :dataset_id, define_field: false, type: :string)
    belongs_to(:project, LangWatch.Project, foreign_key: :project_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end