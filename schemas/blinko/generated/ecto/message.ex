defmodule Blinko.Message do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Blinko.PrismaDateTime, autogenerate: {Blinko.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          content: String.t(),
          role: String.t(),
          metadata: map() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          conversation: Blinko.Conversation.t() | nil
        }

  schema "message" do
    field(:content, :string)
    field(:role, :string)
    field(:metadata, :map)
    field(:conversation_id, :id, source: :conversationId)
    belongs_to(:conversation, Blinko.Conversation, foreign_key: :conversation_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end