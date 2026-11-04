defmodule Papermark.Viewer do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          email: String.t(),
          verified: boolean(),
          invited_at: DateTime.t() | nil,
          notification_preferences: map() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          dataroom: Papermark.Dataroom.t() | nil,
          team: Papermark.Team.t() | nil,
          views: [Papermark.View.t()],
          groups: [Papermark.ViewerGroupMembership.t()],
          invitations: [Papermark.ViewerInvitation.t()],
          participated_conversations: [Papermark.ConversationParticipant.t()],
          messages: [Papermark.Message.t()],
          recipient_conversations: [Papermark.Conversation.t()],
          question_assignments: [Papermark.DataroomQuestionAssignment.t()],
          uploaded_documents: [Papermark.DocumentUpload.t()],
          task_assignments: [Papermark.TaskAssignment.t()],
          task_activities: [Papermark.TaskActivity.t()],
          chats: [Papermark.Chat.t()]
        }

  schema "Viewer" do
    field(:id, :string, primary_key: true)
    field(:email, :string)
    field(:verified, :boolean, default: false)
    field(:invited_at, Papermark.PrismaDateTime, source: :invitedAt)
    field(:notification_preferences, :map, source: :notificationPreferences)
    field(:dataroom_id, :string, source: :dataroomId)
    field(:team_id, :string, source: :teamId)
    belongs_to(:dataroom, Papermark.Dataroom, foreign_key: :dataroom_id, define_field: false, type: :string)
    belongs_to(:team, Papermark.Team, foreign_key: :team_id, define_field: false, type: :string)
    has_many(:views, Papermark.View, foreign_key: :viewer_id, references: :id)
    has_many(:groups, Papermark.ViewerGroupMembership, foreign_key: :viewer_id, references: :id)
    has_many(:invitations, Papermark.ViewerInvitation, foreign_key: :viewer_id, references: :id)
    has_many(:participated_conversations, Papermark.ConversationParticipant, foreign_key: :viewer_id, references: :id)
    has_many(:messages, Papermark.Message, foreign_key: :viewer_id, references: :id)
    has_many(:recipient_conversations, Papermark.Conversation, foreign_key: :recipient_viewer_id, references: :id)
    has_many(:question_assignments, Papermark.DataroomQuestionAssignment, foreign_key: :viewer_id, references: :id)
    has_many(:uploaded_documents, Papermark.DocumentUpload, foreign_key: :viewer_id, references: :id)
    has_many(:task_assignments, Papermark.TaskAssignment, foreign_key: :viewer_id, references: :id)
    has_many(:task_activities, Papermark.TaskActivity, foreign_key: :viewer_id, references: :id)
    has_many(:chats, Papermark.Chat, foreign_key: :viewer_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end