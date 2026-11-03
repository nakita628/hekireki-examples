defmodule Papermark.ConversationParticipant do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          role: atom(),
          receive_notifications: boolean(),
          inserted_at: DateTime.t(),
          conversation: Papermark.Conversation.t() | nil,
          viewer: Papermark.Viewer.t() | nil,
          user: Papermark.User.t() | nil
        }

  schema "ConversationParticipant" do
    field(:id, :string, primary_key: true)
    field(:role, Ecto.Enum, values: [:OWNER, :PARTICIPANT], default: :PARTICIPANT)
    field(:receive_notifications, :boolean, default: false, source: :receiveNotifications)
    field(:conversation_id, :string, source: :conversationId)
    field(:viewer_id, :string, source: :viewerId)
    field(:user_id, :string, source: :userId)
    belongs_to(:conversation, Papermark.Conversation, foreign_key: :conversation_id, define_field: false, type: :string)
    belongs_to(:viewer, Papermark.Viewer, foreign_key: :viewer_id, define_field: false, type: :string)
    belongs_to(:user, Papermark.User, foreign_key: :user_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end