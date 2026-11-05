defmodule InboxZero.ChatCompaction do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          summary: String.t(),
          message_count: integer(),
          compacted_before_created_at: DateTime.t(),
          inserted_at: DateTime.t(),
          chat: InboxZero.Chat.t() | nil
        }

  schema "ChatCompaction" do
    field(:id, :string, primary_key: true)
    field(:summary, :string)
    field(:message_count, :integer, source: :messageCount)
    field(:compacted_before_created_at, InboxZero.PrismaDateTime, source: :compactedBeforeCreatedAt)
    field(:chat_id, :string, source: :chatId)
    belongs_to(:chat, InboxZero.Chat, foreign_key: :chat_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end