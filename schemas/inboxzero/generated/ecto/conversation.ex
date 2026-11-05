defmodule InboxZero.Conversation do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          thread_id: String.t(),
          status: atom(),
          revision: integer(),
          generation: integer(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: InboxZero.Organization.t() | nil,
          publisher: InboxZero.Member.t() | nil,
          participants: [InboxZero.ConversationParticipant.t()],
          comments: [InboxZero.ConversationComment.t()]
        }

  schema "Conversation" do
    field(:id, :string, primary_key: true)
    field(:thread_id, :string, source: :threadId)
    field(:status, Ecto.Enum, values: [:ACTIVE, :STOPPED], default: :ACTIVE)
    field(:revision, :integer, default: 0)
    field(:generation, :integer, default: 1)
    field(:organization_id, :string, source: :organizationId)
    field(:publisher_id, :string, source: :publisherId)
    belongs_to(:organization, InboxZero.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    belongs_to(:publisher, InboxZero.Member, foreign_key: :publisher_id, define_field: false, type: :string)
    has_many(:participants, InboxZero.ConversationParticipant, foreign_key: :conversation_id, references: :id)
    has_many(:comments, InboxZero.ConversationComment, foreign_key: :conversation_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end