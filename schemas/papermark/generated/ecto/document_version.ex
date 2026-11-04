defmodule Papermark.DocumentVersion do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          version_number: integer(),
          file: String.t(),
          original_file: String.t() | nil,
          type: String.t() | nil,
          content_type: String.t() | nil,
          file_size: integer() | nil,
          storage_type: atom(),
          num_pages: integer() | nil,
          is_primary: boolean(),
          is_vertical: boolean(),
          file_id: String.t() | nil,
          vector_store_file_id: String.t() | nil,
          has_pages: boolean(),
          length: integer() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          document: Papermark.Document.t() | nil,
          pages: [Papermark.DocumentPage.t()],
          redaction_jobs: [Papermark.DocumentRedactionJob.t()]
        }

  schema "DocumentVersion" do
    field(:id, :string, primary_key: true)
    field(:version_number, :integer, source: :versionNumber)
    field(:file, :string)
    field(:original_file, :string, source: :originalFile)
    field(:type, :string)
    field(:content_type, :string, source: :contentType)
    field(:file_size, :integer, source: :fileSize)
    field(:storage_type, Ecto.Enum, values: [:S3_PATH, :VERCEL_BLOB], default: :VERCEL_BLOB, source: :storageType)
    field(:num_pages, :integer, source: :numPages)
    field(:is_primary, :boolean, default: false, source: :isPrimary)
    field(:is_vertical, :boolean, default: false, source: :isVertical)
    field(:file_id, :string, source: :fileId)
    field(:vector_store_file_id, :string, source: :vectorStoreFileId)
    field(:has_pages, :boolean, default: false, source: :hasPages)
    field(:length, :integer)
    field(:document_id, :string, source: :documentId)
    belongs_to(:document, Papermark.Document, foreign_key: :document_id, define_field: false, type: :string)
    has_many(:pages, Papermark.DocumentPage, foreign_key: :version_id, references: :id)
    has_many(:redaction_jobs, Papermark.DocumentRedactionJob, foreign_key: :document_version_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end