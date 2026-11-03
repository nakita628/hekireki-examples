defmodule Papermark.ChatMessage do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          role: String.t(),
          content: String.t(),
          metadata: map() | nil,
          inserted_at: DateTime.t(),
          chat: Papermark.Chat.t() | nil
        }

  schema "ChatMessage" do
    field(:id, :string, primary_key: true)
    field(:role, :string)
    field(:content, :string)
    field(:metadata, :map)
    field(:chat_id, :string, source: :chatId)
    belongs_to(:chat, Papermark.Chat, foreign_key: :chat_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end