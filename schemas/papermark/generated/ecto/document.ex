defmodule Papermark.Document do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          description: String.t() | nil,
          file: String.t(),
          original_file: String.t() | nil,
          type: String.t() | nil,
          content_type: String.t() | nil,
          storage_type: atom(),
          num_pages: integer() | nil,
          assistant_enabled: boolean(),
          advanced_excel_enabled: boolean(),
          agents_enabled: boolean(),
          download_only: boolean(),
          hidden_in_all_documents: boolean(),
          is_external_upload: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          owner: Papermark.User.t() | nil,
          team: Papermark.Team.t() | nil,
          folder: Papermark.Folder.t() | nil,
          links: [Papermark.Link.t()],
          views: [Papermark.View.t()],
          versions: [Papermark.DocumentVersion.t()],
          chats: [Papermark.Chat.t()],
          datarooms: [Papermark.DataroomDocument.t()],
          tags: [Papermark.TagItem.t()],
          annotations: [Papermark.DocumentAnnotation.t()],
          redaction_jobs: [Papermark.DocumentRedactionJob.t()],
          uploaded_document: [Papermark.DocumentUpload.t()]
        }

  schema "Document" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:description, :string)
    field(:file, :string)
    field(:original_file, :string, source: :originalFile)
    field(:type, :string)
    field(:content_type, :string, source: :contentType)
    field(:storage_type, Ecto.Enum, values: [:S3_PATH, :VERCEL_BLOB], default: :VERCEL_BLOB, source: :storageType)
    field(:num_pages, :integer, source: :numPages)
    field(:assistant_enabled, :boolean, default: false, source: :assistantEnabled)
    field(:advanced_excel_enabled, :boolean, default: false, source: :advancedExcelEnabled)
    field(:agents_enabled, :boolean, default: false, source: :agentsEnabled)
    field(:download_only, :boolean, default: false, source: :downloadOnly)
    field(:hidden_in_all_documents, :boolean, default: false, source: :hiddenInAllDocuments)
    field(:is_external_upload, :boolean, default: false, source: :isExternalUpload)
    field(:owner_id, :string, source: :ownerId)
    field(:team_id, :string, source: :teamId)
    field(:folder_id, :string, source: :folderId)
    belongs_to(:owner, Papermark.User, foreign_key: :owner_id, define_field: false, type: :string)
    belongs_to(:team, Papermark.Team, foreign_key: :team_id, define_field: false, type: :string)
    belongs_to(:folder, Papermark.Folder, foreign_key: :folder_id, define_field: false, type: :string)
    has_many(:links, Papermark.Link, foreign_key: :document_id)
    has_many(:views, Papermark.View, foreign_key: :document_id)
    has_many(:versions, Papermark.DocumentVersion, foreign_key: :document_id)
    has_many(:chats, Papermark.Chat, foreign_key: :document_id)
    has_many(:datarooms, Papermark.DataroomDocument, foreign_key: :document_id)
    has_many(:tags, Papermark.TagItem, foreign_key: :document_id)
    has_many(:annotations, Papermark.DocumentAnnotation, foreign_key: :document_id)
    has_many(:redaction_jobs, Papermark.DocumentRedactionJob, foreign_key: :document_id)
    has_many(:uploaded_document, Papermark.DocumentUpload, foreign_key: :document_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end