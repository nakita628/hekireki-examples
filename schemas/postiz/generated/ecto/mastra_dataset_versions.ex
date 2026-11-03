defmodule Postiz.MastraDatasetVersions do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          dataset_id: String.t(),
          version: integer(),
          created_at: DateTime.t(),
          created_at_z: DateTime.t() | nil
        }

  schema "mastra_dataset_versions" do
    field(:id, :string, primary_key: true)
    field(:dataset_id, :string, source: :datasetId)
    field(:version, :integer)
    field(:created_at, Postiz.PrismaDateTime, source: :createdAt)
    field(:created_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :createdAtZ)
  end
end