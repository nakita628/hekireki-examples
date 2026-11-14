defmodule LangWatch.Dataset do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          slug: String.t(),
          column_types: map(),
          archived_at: DateTime.t() | nil,
          mapping: map() | nil,
          use_s3: boolean(),
          s3_record_count: integer() | nil,
          content_layout: String.t(),
          status: String.t(),
          status_error: String.t() | nil,
          staging_key: String.t() | nil,
          source_stored_object_id: String.t() | nil,
          upload_filename: String.t() | nil,
          row_count: integer() | nil,
          size_bytes: integer() | nil,
          chunk_count: integer() | nil,
          chunk_offsets: map() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: LangWatch.Project.t() | nil,
          dataset_records: [LangWatch.DatasetRecord.t()],
          batch_evaluations: [LangWatch.BatchEvaluation.t()]
        }

  schema "Dataset" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:slug, :string)
    field(:column_types, :map, source: :columnTypes)
    field(:archived_at, LangWatch.PrismaDateTime, source: :archivedAt)
    field(:mapping, :map)
    field(:use_s3, :boolean, default: false, source: :useS3)
    field(:s3_record_count, :integer, source: :s3RecordCount)
    field(:content_layout, :string, default: "postgres", source: :contentLayout)
    field(:status, :string, default: "ready")
    field(:status_error, :string, source: :statusError)
    field(:staging_key, :string, source: :stagingKey)
    field(:source_stored_object_id, :string, source: :sourceStoredObjectId)
    field(:upload_filename, :string, source: :uploadFilename)
    field(:row_count, :integer, source: :rowCount)
    field(:size_bytes, :integer, source: :sizeBytes)
    field(:chunk_count, :integer, source: :chunkCount)
    field(:chunk_offsets, :map, source: :chunkOffsets)
    field(:project_id, :string, source: :projectId)
    belongs_to(:project, LangWatch.Project, foreign_key: :project_id, define_field: false, type: :string)
    has_many(:dataset_records, LangWatch.DatasetRecord, foreign_key: :dataset_id, references: :id)
    has_many(:batch_evaluations, LangWatch.BatchEvaluation, foreign_key: :dataset_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end