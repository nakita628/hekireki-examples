defmodule Papermark.View do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          dataroom_view_id: String.t() | nil,
          viewer_email: String.t() | nil,
          viewer_name: String.t() | nil,
          verified: boolean(),
          viewed_at: DateTime.t(),
          downloaded_at: DateTime.t() | nil,
          download_type: atom() | nil,
          download_metadata: map() | nil,
          view_type: atom(),
          is_archived: boolean(),
          link: Papermark.Link.t() | nil,
          document: Papermark.Document.t() | nil,
          dataroom: Papermark.Dataroom.t() | nil,
          viewer: Papermark.Viewer.t() | nil,
          group: Papermark.ViewerGroup.t() | nil,
          team: Papermark.Team.t() | nil,
          feedback_response: Papermark.FeedbackResponse.t() | nil,
          agreement_response: Papermark.AgreementResponse.t() | nil,
          custom_field_response: Papermark.CustomFieldResponse.t() | nil,
          reactions: [Papermark.Reaction.t()],
          conversation_views: [Papermark.ConversationView.t()],
          messages: [Papermark.Message.t()],
          initial_conversations: [Papermark.Conversation.t()],
          uploaded_documents: [Papermark.DocumentUpload.t()],
          task_activities: [Papermark.TaskActivity.t()],
          chats: [Papermark.Chat.t()]
        }

  schema "View" do
    field(:id, :string, primary_key: true)
    field(:dataroom_view_id, :string, source: :dataroomViewId)
    field(:viewer_email, :string, source: :viewerEmail)
    field(:viewer_name, :string, source: :viewerName)
    field(:verified, :boolean, default: false)
    field(:viewed_at, Papermark.PrismaDateTime, autogenerate: true, source: :viewedAt)
    field(:downloaded_at, Papermark.PrismaDateTime, source: :downloadedAt)
    field(:download_type, Ecto.Enum, values: [:SINGLE, :BULK, :FOLDER], source: :downloadType)
    field(:download_metadata, :map, source: :downloadMetadata)
    field(:view_type, Ecto.Enum, values: [:DOCUMENT_VIEW, :DATAROOM_VIEW], default: :DOCUMENT_VIEW, source: :viewType)
    field(:is_archived, :boolean, default: false, source: :isArchived)
    field(:link_id, :string, source: :linkId)
    field(:document_id, :string, source: :documentId)
    field(:dataroom_id, :string, source: :dataroomId)
    field(:viewer_id, :string, source: :viewerId)
    field(:group_id, :string, source: :groupId)
    field(:team_id, :string, source: :teamId)
    belongs_to(:link, Papermark.Link, foreign_key: :link_id, define_field: false, type: :string)
    belongs_to(:document, Papermark.Document, foreign_key: :document_id, define_field: false, type: :string)
    belongs_to(:dataroom, Papermark.Dataroom, foreign_key: :dataroom_id, define_field: false, type: :string)
    belongs_to(:viewer, Papermark.Viewer, foreign_key: :viewer_id, define_field: false, type: :string)
    belongs_to(:group, Papermark.ViewerGroup, foreign_key: :group_id, define_field: false, type: :string)
    belongs_to(:team, Papermark.Team, foreign_key: :team_id, define_field: false, type: :string)
    has_one(:feedback_response, Papermark.FeedbackResponse, foreign_key: :view_id, references: :id)
    has_one(:agreement_response, Papermark.AgreementResponse, foreign_key: :view_id, references: :id)
    has_one(:custom_field_response, Papermark.CustomFieldResponse, foreign_key: :view_id, references: :id)
    has_many(:reactions, Papermark.Reaction, foreign_key: :view_id, references: :id)
    has_many(:conversation_views, Papermark.ConversationView, foreign_key: :view_id, references: :id)
    has_many(:messages, Papermark.Message, foreign_key: :view_id, references: :id)
    has_many(:initial_conversations, Papermark.Conversation, foreign_key: :initial_view_id, references: :id)
    has_many(:uploaded_documents, Papermark.DocumentUpload, foreign_key: :view_id, references: :id)
    has_many(:task_activities, Papermark.TaskActivity, foreign_key: :view_id, references: :id)
    has_many(:chats, Papermark.Chat, foreign_key: :view_id, references: :id)
  end
end