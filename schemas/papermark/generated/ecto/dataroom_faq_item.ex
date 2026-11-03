defmodule Papermark.DataroomFaqItem do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          title: String.t() | nil,
          edited_question: String.t(),
          original_question: String.t() | nil,
          answer: String.t(),
          description: String.t() | nil,
          visibility_mode: atom(),
          status: atom(),
          is_anonymized: boolean(),
          view_count: integer(),
          tags: [String.t()],
          document_page_number: integer() | nil,
          document_version_number: integer() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          dataroom: Papermark.Dataroom.t() | nil,
          link: Papermark.Link.t() | nil,
          dataroom_document: Papermark.DataroomDocument.t() | nil,
          source_conversation: Papermark.Conversation.t() | nil,
          question_message: Papermark.Message.t() | nil,
          answer_message: Papermark.Message.t() | nil,
          team: Papermark.Team.t() | nil,
          published_by_user: Papermark.User.t() | nil
        }

  schema "DataroomFaqItem" do
    field(:id, :string, primary_key: true)
    field(:title, :string)
    field(:edited_question, :string, source: :editedQuestion)
    field(:original_question, :string, source: :originalQuestion)
    field(:answer, :string)
    field(:description, :string)
    field(:visibility_mode, Ecto.Enum, values: [:PUBLIC_DATAROOM, :PUBLIC_LINK, :PUBLIC_DOCUMENT], default: :PUBLIC_DATAROOM, source: :visibilityMode)
    field(:status, Ecto.Enum, values: [:DRAFT, :PUBLISHED, :ARCHIVED], default: :PUBLISHED)
    field(:is_anonymized, :boolean, default: true, source: :isAnonymized)
    field(:view_count, :integer, default: 0, source: :viewCount)
    field(:tags, {:array, :string})
    field(:document_page_number, :integer, source: :documentPageNumber)
    field(:document_version_number, :integer, source: :documentVersionNumber)
    field(:dataroom_id, :string, source: :dataroomId)
    field(:link_id, :string, source: :linkId)
    field(:dataroom_document_id, :string, source: :dataroomDocumentId)
    field(:source_conversation_id, :string, source: :sourceConversationId)
    field(:question_message_id, :string, source: :questionMessageId)
    field(:answer_message_id, :string, source: :answerMessageId)
    field(:team_id, :string, source: :teamId)
    field(:published_by_user_id, :string, source: :publishedByUserId)
    belongs_to(:dataroom, Papermark.Dataroom, foreign_key: :dataroom_id, define_field: false, type: :string)
    belongs_to(:link, Papermark.Link, foreign_key: :link_id, define_field: false, type: :string)
    belongs_to(:dataroom_document, Papermark.DataroomDocument, foreign_key: :dataroom_document_id, define_field: false, type: :string)
    belongs_to(:source_conversation, Papermark.Conversation, foreign_key: :source_conversation_id, define_field: false, type: :string)
    belongs_to(:question_message, Papermark.Message, foreign_key: :question_message_id, define_field: false, type: :string)
    belongs_to(:answer_message, Papermark.Message, foreign_key: :answer_message_id, define_field: false, type: :string)
    belongs_to(:team, Papermark.Team, foreign_key: :team_id, define_field: false, type: :string)
    belongs_to(:published_by_user, Papermark.User, foreign_key: :published_by_user_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end