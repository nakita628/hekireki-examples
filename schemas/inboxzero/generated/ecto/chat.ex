defmodule InboxZero.Chat do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          deleted_at: DateTime.t() | nil,
          name: String.t() | nil,
          compaction_count: integer(),
          last_seen_rules_revision: integer() | nil,
          active_stream_id: String.t() | nil,
          active_stream_started_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          email_account: InboxZero.EmailAccount.t() | nil,
          messages: [InboxZero.ChatMessage.t()],
          compactions: [InboxZero.ChatCompaction.t()],
          memories: [InboxZero.ChatMemory.t()]
        }

  schema "Chat" do
    field(:id, :string, primary_key: true)
    field(:deleted_at, InboxZero.PrismaDateTime, source: :deletedAt)
    field(:name, :string)
    field(:compaction_count, :integer, default: 0, source: :compactionCount)
    field(:last_seen_rules_revision, :integer, source: :lastSeenRulesRevision)
    field(:active_stream_id, :string, source: :activeStreamId)
    field(:active_stream_started_at, InboxZero.PrismaDateTime, source: :activeStreamStartedAt)
    field(:email_account_id, :string, source: :emailAccountId)
    belongs_to(:email_account, InboxZero.EmailAccount, foreign_key: :email_account_id, define_field: false, type: :string)
    has_many(:messages, InboxZero.ChatMessage, foreign_key: :chat_id, references: :id)
    has_many(:compactions, InboxZero.ChatCompaction, foreign_key: :chat_id, references: :id)
    has_many(:memories, InboxZero.ChatMemory, foreign_key: :chat_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end