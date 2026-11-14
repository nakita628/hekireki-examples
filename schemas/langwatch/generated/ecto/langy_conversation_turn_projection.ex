defmodule LangWatch.LangyConversationTurnProjection do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          project_id: String.t(),
          conversation_id: String.t(),
          turn_id: String.t(),
          status: String.t(),
          question_parts: map(),
          answer_parts: map(),
          tool_calls: map(),
          plan: map() | nil,
          error: String.t() | nil,
          started_at: float() | nil,
          ended_at: float() | nil,
          created_at: float(),
          updated_at: float(),
          occurred_at: float(),
          accepted_at: float(),
          last_event_id: String.t(),
          projection_version: String.t()
        }

  schema "LangyConversationTurnProjection" do
    field(:id, :string, primary_key: true)
    field(:project_id, :string, source: :projectId)
    field(:conversation_id, :string, source: :conversationId)
    field(:turn_id, :string, source: :turnId)
    field(:status, :string)
    field(:question_parts, :map, source: :questionParts)
    field(:answer_parts, :map, source: :answerParts)
    field(:tool_calls, :map, source: :toolCalls)
    field(:plan, :map)
    field(:error, :string)
    field(:started_at, :float, source: :startedAt)
    field(:ended_at, :float, source: :endedAt)
    field(:created_at, :float, source: :createdAt)
    field(:updated_at, :float, source: :updatedAt)
    field(:occurred_at, :float, source: :occurredAt)
    field(:accepted_at, :float, source: :acceptedAt)
    field(:last_event_id, :string, source: :lastEventId)
    field(:projection_version, :string, source: :projectionVersion)
  end
end