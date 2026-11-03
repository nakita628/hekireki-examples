defmodule Papermark.Conversation do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          title: String.t() | nil,
          is_enabled: boolean(),
          visibility_mode: atom(),
          document_version_number: integer() | nil,
          document_page_number: integer() | nil,
          origin: String.t(),
          category: String.t() | nil,
          priority: String.t() | nil,
          last_message_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          dataroom: Papermark.Dataroom.t() | nil,
          dataroom_document: Papermark.DataroomDocument.t() | nil,
          link: Papermark.Link.t() | nil,
          viewer_group: Papermark.ViewerGroup.t() | nil,
          question: Papermark.DataroomQuestion.t() | nil,
          recipient_viewer: Papermark.Viewer.t() | nil,
          initial_view: Papermark.View.t() | nil,
          team: Papermark.Team.t() | nil,
          views: [Papermark.ConversationView.t()],
          participants: [Papermark.ConversationParticipant.t()],
          messages: [Papermark.Message.t()],
          faq_items: [Papermark.DataroomFaqItem.t()]
        }

  schema "Conversation" do
    field(:id, :string, primary_key: true)
    field(:title, :string)
    field(:is_enabled, :boolean, default: true, source: :isEnabled)
    field(:visibility_mode, Ecto.Enum, values: [:PRIVATE, :PUBLIC_LINK, :PUBLIC_GROUP, :PUBLIC_DOCUMENT, :PUBLIC_DATAROOM], default: :PRIVATE, source: :visibilityMode)
    field(:document_version_number, :integer, source: :documentVersionNumber)
    field(:document_page_number, :integer, source: :documentPageNumber)
    field(:origin, :string, default: "visitor")
    field(:category, :string)
    field(:priority, :string)
    field(:last_message_at, Papermark.PrismaDateTime, source: :lastMessageAt)
    field(:dataroom_id, :string, source: :dataroomId)
    field(:dataroom_document_id, :string, source: :dataroomDocumentId)
    field(:link_id, :string, source: :linkId)
    field(:viewer_group_id, :string, source: :viewerGroupId)
    field(:question_id, :string, source: :questionId)
    field(:recipient_viewer_id, :string, source: :recipientViewerId)
    field(:initial_view_id, :string, source: :initialViewId)
    field(:team_id, :string, source: :teamId)
    belongs_to(:dataroom, Papermark.Dataroom, foreign_key: :dataroom_id, define_field: false, type: :string)
    belongs_to(:dataroom_document, Papermark.DataroomDocument, foreign_key: :dataroom_document_id, define_field: false, type: :string)
    belongs_to(:link, Papermark.Link, foreign_key: :link_id, define_field: false, type: :string)
    belongs_to(:viewer_group, Papermark.ViewerGroup, foreign_key: :viewer_group_id, define_field: false, type: :string)
    belongs_to(:question, Papermark.DataroomQuestion, foreign_key: :question_id, define_field: false, type: :string)
    belongs_to(:recipient_viewer, Papermark.Viewer, foreign_key: :recipient_viewer_id, define_field: false, type: :string)
    belongs_to(:initial_view, Papermark.View, foreign_key: :initial_view_id, define_field: false, type: :string)
    belongs_to(:team, Papermark.Team, foreign_key: :team_id, define_field: false, type: :string)
    has_many(:views, Papermark.ConversationView, foreign_key: :conversation_id)
    has_many(:participants, Papermark.ConversationParticipant, foreign_key: :conversation_id)
    has_many(:messages, Papermark.Message, foreign_key: :conversation_id)
    has_many(:faq_items, Papermark.DataroomFaqItem, foreign_key: :source_conversation_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end