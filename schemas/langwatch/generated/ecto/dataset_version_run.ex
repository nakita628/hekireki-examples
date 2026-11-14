defmodule LangWatch.DatasetVersionRun do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          project_id: String.t(),
          dataset_id: String.t(),
          version_id: String.t(),
          run_id: String.t(),
          experiment_id: String.t() | nil,
          inserted_at: DateTime.t()
        }

  schema "DatasetVersionRun" do
    field(:id, :string, primary_key: true)
    field(:project_id, :string, source: :projectId)
    field(:dataset_id, :string, source: :datasetId)
    field(:version_id, :string, source: :versionId)
    field(:run_id, :string, source: :runId)
    field(:experiment_id, :string, source: :experimentId)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end