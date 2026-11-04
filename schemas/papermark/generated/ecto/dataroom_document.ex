defmodule Papermark.DataroomDocument do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          order_index: integer() | nil,
          hierarchical_index: String.t() | nil,
          vector_store_file_id: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          dataroom: Papermark.Dataroom.t() | nil,
          document: Papermark.Document.t() | nil,
          folder: Papermark.DataroomFolder.t() | nil,
          conversations: [Papermark.Conversation.t()],
          questions: [Papermark.DataroomQuestion.t()],
          dataroom_faqs: [Papermark.DataroomFaqItem.t()],
          uploaded_documents: [Papermark.DocumentUpload.t()]
        }

  schema "DataroomDocument" do
    field(:id, :string, primary_key: true)
    field(:order_index, :integer, source: :orderIndex)
    field(:hierarchical_index, :string, source: :hierarchicalIndex)
    field(:vector_store_file_id, :string, source: :vectorStoreFileId)
    field(:dataroom_id, :string, source: :dataroomId)
    field(:document_id, :string, source: :documentId)
    field(:folder_id, :string, source: :folderId)
    belongs_to(:dataroom, Papermark.Dataroom, foreign_key: :dataroom_id, define_field: false, type: :string)
    belongs_to(:document, Papermark.Document, foreign_key: :document_id, define_field: false, type: :string)
    belongs_to(:folder, Papermark.DataroomFolder, foreign_key: :folder_id, define_field: false, type: :string)
    has_many(:conversations, Papermark.Conversation, foreign_key: :dataroom_document_id, references: :id)
    has_many(:questions, Papermark.DataroomQuestion, foreign_key: :dataroom_document_id, references: :id)
    has_many(:dataroom_faqs, Papermark.DataroomFaqItem, foreign_key: :dataroom_document_id, references: :id)
    has_many(:uploaded_documents, Papermark.DocumentUpload, foreign_key: :dataroom_document_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end