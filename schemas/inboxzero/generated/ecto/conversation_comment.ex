defmodule InboxZero.ConversationComment do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          client_mutation_id: String.t(),
          body: String.t(),
          revision: integer(),
          deleted_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          conversation: InboxZero.Conversation.t() | nil,
          author: InboxZero.Member.t() | nil,
          activity: [InboxZero.ConversationActivity.t()]
        }

  schema "ConversationComment" do
    field(:id, :string, primary_key: true)
    field(:client_mutation_id, :string, source: :clientMutationId)
    field(:body, :string)
    field(:revision, :integer)
    field(:deleted_at, InboxZero.PrismaDateTime, source: :deletedAt)
    field(:conversation_id, :string, source: :conversationId)
    field(:author_member_id, :string, source: :authorMemberId)
    belongs_to(:conversation, InboxZero.Conversation, foreign_key: :conversation_id, define_field: false, type: :string)
    belongs_to(:author, InboxZero.Member, foreign_key: :author_member_id, define_field: false, type: :string)
    has_many(:activity, InboxZero.ConversationActivity, foreign_key: :comment_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end