defmodule Papermark.Message do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          content: String.t(),
          is_read: boolean(),
          is_internal_note: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          conversation: Papermark.Conversation.t() | nil,
          user: Papermark.User.t() | nil,
          viewer: Papermark.Viewer.t() | nil,
          view: Papermark.View.t() | nil,
          faq_as_question: [Papermark.DataroomFaqItem.t()],
          faq_as_answer: [Papermark.DataroomFaqItem.t()]
        }

  schema "Message" do
    field(:id, :string, primary_key: true)
    field(:content, :string)
    field(:is_read, :boolean, default: false, source: :isRead)
    field(:is_internal_note, :boolean, default: false, source: :isInternalNote)
    field(:conversation_id, :string, source: :conversationId)
    field(:user_id, :string, source: :userId)
    field(:viewer_id, :string, source: :viewerId)
    field(:view_id, :string, source: :viewId)
    belongs_to(:conversation, Papermark.Conversation, foreign_key: :conversation_id, define_field: false, type: :string)
    belongs_to(:user, Papermark.User, foreign_key: :user_id, define_field: false, type: :string)
    belongs_to(:viewer, Papermark.Viewer, foreign_key: :viewer_id, define_field: false, type: :string)
    belongs_to(:view, Papermark.View, foreign_key: :view_id, define_field: false, type: :string)
    has_many(:faq_as_question, Papermark.DataroomFaqItem, foreign_key: :question_message_id, references: :id)
    has_many(:faq_as_answer, Papermark.DataroomFaqItem, foreign_key: :answer_message_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end