defmodule Papermark.DocumentUpload do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          original_filename: String.t() | nil,
          file_size: integer() | nil,
          num_pages: integer() | nil,
          mime_type: String.t() | nil,
          uploaded_at: DateTime.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          document: Papermark.Document.t() | nil,
          team: Papermark.Team.t() | nil,
          viewer: Papermark.Viewer.t() | nil,
          view: Papermark.View.t() | nil,
          link: Papermark.Link.t() | nil,
          dataroom: Papermark.Dataroom.t() | nil,
          dataroom_document: Papermark.DataroomDocument.t() | nil,
          task: Papermark.Task.t() | nil
        }

  schema "DocumentUpload" do
    field(:id, :string, primary_key: true)
    field(:original_filename, :string, source: :originalFilename)
    field(:file_size, :integer, source: :fileSize)
    field(:num_pages, :integer, source: :numPages)
    field(:mime_type, :string, source: :mimeType)
    field(:uploaded_at, Papermark.PrismaDateTime, autogenerate: true, source: :uploadedAt)
    field(:document_id, :string, source: :documentId)
    field(:team_id, :string, source: :teamId)
    field(:viewer_id, :string, source: :viewerId)
    field(:view_id, :string, source: :viewId)
    field(:link_id, :string, source: :linkId)
    field(:dataroom_id, :string, source: :dataroomId)
    field(:dataroom_document_id, :string, source: :dataroomDocumentId)
    field(:task_id, :string, source: :taskId)
    belongs_to(:document, Papermark.Document, foreign_key: :document_id, define_field: false, type: :string)
    belongs_to(:team, Papermark.Team, foreign_key: :team_id, define_field: false, type: :string)
    belongs_to(:viewer, Papermark.Viewer, foreign_key: :viewer_id, define_field: false, type: :string)
    belongs_to(:view, Papermark.View, foreign_key: :view_id, define_field: false, type: :string)
    belongs_to(:link, Papermark.Link, foreign_key: :link_id, define_field: false, type: :string)
    belongs_to(:dataroom, Papermark.Dataroom, foreign_key: :dataroom_id, define_field: false, type: :string)
    belongs_to(:dataroom_document, Papermark.DataroomDocument, foreign_key: :dataroom_document_id, define_field: false, type: :string)
    belongs_to(:task, Papermark.Task, foreign_key: :task_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end