defmodule InboxZero.ChatMessage do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          role: String.t(),
          parts: map(),
          metadata: map() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          chat: InboxZero.Chat.t() | nil
        }

  schema "ChatMessage" do
    field(:id, :string, primary_key: true)
    field(:role, :string)
    field(:parts, :map)
    field(:metadata, :map)
    field(:chat_id, :string, source: :chatId)
    belongs_to(:chat, InboxZero.Chat, foreign_key: :chat_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end