defmodule InboxZero.ConversationParticipant do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          generation: integer(),
          active: boolean(),
          read_revision: integer(),
          muted: boolean(),
          inserted_at: DateTime.t(),
          conversation: InboxZero.Conversation.t() | nil,
          member: InboxZero.Member.t() | nil,
          activity: [InboxZero.ConversationActivity.t()]
        }

  schema "ConversationParticipant" do
    field(:id, :string, primary_key: true)
    field(:generation, :integer)
    field(:active, :boolean, default: true)
    field(:read_revision, :integer, default: 0, source: :readRevision)
    field(:muted, :boolean, default: false)
    field(:conversation_id, :string, source: :conversationId)
    field(:member_id, :string, source: :memberId)
    belongs_to(:conversation, InboxZero.Conversation, foreign_key: :conversation_id, define_field: false, type: :string)
    belongs_to(:member, InboxZero.Member, foreign_key: :member_id, define_field: false, type: :string)
    has_many(:activity, InboxZero.ConversationActivity, foreign_key: :participant_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end