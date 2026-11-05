defmodule InboxZero.ConversationActivity do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          kind: atom(),
          revision: integer(),
          inserted_at: DateTime.t(),
          participant: InboxZero.ConversationParticipant.t() | nil,
          comment: InboxZero.ConversationComment.t() | nil
        }

  schema "ConversationActivity" do
    field(:id, :string, primary_key: true)
    field(:kind, Ecto.Enum, values: [:INVITED, :COMMENT, :MENTION])
    field(:revision, :integer)
    field(:participant_id, :string, source: :participantId)
    field(:comment_id, :string, source: :commentId)
    belongs_to(:participant, InboxZero.ConversationParticipant, foreign_key: :participant_id, define_field: false, type: :string)
    belongs_to(:comment, InboxZero.ConversationComment, foreign_key: :comment_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end