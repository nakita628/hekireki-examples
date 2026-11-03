defmodule Papermark.ConversationView do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          inserted_at: DateTime.t(),
          conversation: Papermark.Conversation.t() | nil,
          view: Papermark.View.t() | nil
        }

  schema "ConversationView" do
    field(:id, :string, primary_key: true)
    field(:conversation_id, :string, source: :conversationId)
    field(:view_id, :string, source: :viewId)
    belongs_to(:conversation, Papermark.Conversation, foreign_key: :conversation_id, define_field: false, type: :string)
    belongs_to(:view, Papermark.View, foreign_key: :view_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end