defmodule Papermark.DataroomQuestion do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          content: String.t(),
          category: String.t() | nil,
          priority: String.t() | nil,
          status: String.t(),
          order_index: integer() | nil,
          import_batch_id: String.t() | nil,
          document_page_number: integer() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          dataroom: Papermark.Dataroom.t() | nil,
          team: Papermark.Team.t() | nil,
          created_by_user: Papermark.User.t() | nil,
          dataroom_document: Papermark.DataroomDocument.t() | nil,
          assignments: [Papermark.DataroomQuestionAssignment.t()],
          conversations: [Papermark.Conversation.t()]
        }

  schema "DataroomQuestion" do
    field(:id, :string, primary_key: true)
    field(:content, :string)
    field(:category, :string)
    field(:priority, :string)
    field(:status, :string, default: "unanswered")
    field(:order_index, :integer, source: :orderIndex)
    field(:import_batch_id, :string, source: :importBatchId)
    field(:document_page_number, :integer, source: :documentPageNumber)
    field(:dataroom_id, :string, source: :dataroomId)
    field(:team_id, :string, source: :teamId)
    field(:created_by_user_id, :string, source: :createdByUserId)
    field(:dataroom_document_id, :string, source: :dataroomDocumentId)
    belongs_to(:dataroom, Papermark.Dataroom, foreign_key: :dataroom_id, define_field: false, type: :string)
    belongs_to(:team, Papermark.Team, foreign_key: :team_id, define_field: false, type: :string)
    belongs_to(:created_by_user, Papermark.User, foreign_key: :created_by_user_id, define_field: false, type: :string)
    belongs_to(:dataroom_document, Papermark.DataroomDocument, foreign_key: :dataroom_document_id, define_field: false, type: :string)
    has_many(:assignments, Papermark.DataroomQuestionAssignment, foreign_key: :question_id)
    has_many(:conversations, Papermark.Conversation, foreign_key: :question_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end