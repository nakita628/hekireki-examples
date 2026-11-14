defmodule LangWatch.LangyMessageProjection do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          project_id: String.t(),
          conversation_id: String.t(),
          message_id: String.t(),
          role: String.t(),
          parts: map(),
          source_event_id: String.t(),
          occurred_at: float(),
          accepted_at: float(),
          created_at: float(),
          updated_at: float()
        }

  schema "LangyMessageProjection" do
    field(:id, :string, primary_key: true)
    field(:project_id, :string, source: :projectId)
    field(:conversation_id, :string, source: :conversationId)
    field(:message_id, :string, source: :messageId)
    field(:role, :string)
    field(:parts, :map)
    field(:source_event_id, :string, source: :sourceEventId)
    field(:occurred_at, :float, source: :occurredAt)
    field(:accepted_at, :float, source: :acceptedAt)
    field(:created_at, :float, source: :createdAt)
    field(:updated_at, :float, source: :updatedAt)
  end
end