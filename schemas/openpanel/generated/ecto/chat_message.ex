defmodule OpenPanel.ChatMessage do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: OpenPanel.PrismaDateTime, autogenerate: {OpenPanel.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          role: String.t(),
          parts: map(),
          metadata: map() | nil,
          inserted_at: DateTime.t(),
          conversation: OpenPanel.Conversation.t() | nil
        }

  schema "chat_messages" do
    field(:id, :string, primary_key: true, read_after_writes: true)
    field(:role, :string)
    field(:parts, :map)
    field(:metadata, :map)
    field(:conversation_id, :string, source: :conversationId)
    belongs_to(:conversation, OpenPanel.Conversation, foreign_key: :conversation_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end