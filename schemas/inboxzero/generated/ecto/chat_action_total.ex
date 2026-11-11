defmodule InboxZero.ChatActionTotal do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          action: String.t(),
          count: integer(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          chat: InboxZero.Chat.t() | nil
        }

  schema "ChatActionTotal" do
    field(:id, :string, primary_key: true)
    field(:action, :string)
    field(:count, :integer, default: 0)
    field(:chat_id, :string, source: :chatId)
    belongs_to(:chat, InboxZero.Chat, foreign_key: :chat_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end