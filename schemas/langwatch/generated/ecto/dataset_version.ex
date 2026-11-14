defmodule LangWatch.DatasetVersion do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          project_id: String.t(),
          dataset_id: String.t(),
          version: integer(),
          name: String.t() | nil,
          trigger: String.t(),
          content_hash: String.t(),
          column_types: map(),
          row_count: integer(),
          size_bytes: integer(),
          chunk_count: integer(),
          restored_from_version: integer() | nil,
          author_id: String.t() | nil,
          author_kind: String.t() | nil,
          pinned_at: DateTime.t() | nil,
          inserted_at: DateTime.t()
        }

  schema "DatasetVersion" do
    field(:id, :string, primary_key: true)
    field(:project_id, :string, source: :projectId)
    field(:dataset_id, :string, source: :datasetId)
    field(:version, :integer)
    field(:name, :string)
    field(:trigger, :string)
    field(:content_hash, :string, source: :contentHash)
    field(:column_types, :map, source: :columnTypes)
    field(:row_count, :integer, source: :rowCount)
    field(:size_bytes, :integer, source: :sizeBytes)
    field(:chunk_count, :integer, source: :chunkCount)
    field(:restored_from_version, :integer, source: :restoredFromVersion)
    field(:author_id, :string, source: :authorId)
    field(:author_kind, :string, source: :authorKind)
    field(:pinned_at, LangWatch.PrismaDateTime, source: :pinnedAt)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end