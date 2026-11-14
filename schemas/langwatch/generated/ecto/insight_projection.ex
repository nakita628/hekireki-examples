defmodule LangWatch.InsightProjection do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          project_id: String.t(),
          title: String.t(),
          body: String.t(),
          tone: String.t(),
          topic: String.t() | nil,
          valid_days: integer(),
          lwql: String.t() | nil,
          source_conversation_id: String.t() | nil,
          source_message_id: String.t() | nil,
          filed_by_user_id: String.t() | nil,
          filed_at: float(),
          renewed_at: float() | nil,
          created_at: float(),
          updated_at: float(),
          occurred_at: float(),
          accepted_at: float(),
          last_event_id: String.t(),
          projection_version: String.t(),
          board_id: String.t() | nil,
          board_name: String.t() | nil,
          widget_id: String.t() | nil,
          widget_name: String.t() | nil,
          filed_via: String.t(),
          replay_start: float() | nil,
          replay_end: float() | nil,
          replay_granularity_seconds: integer() | nil,
          replay_context: map() | nil,
          owner_user_id: String.t() | nil,
          finding_key: String.t() | nil
        }

  schema "InsightProjection" do
    field(:id, :string, primary_key: true)
    field(:project_id, :string, source: :projectId)
    field(:title, :string)
    field(:body, :string)
    field(:tone, :string)
    field(:topic, :string)
    field(:valid_days, :integer, source: :validDays)
    field(:lwql, :string)
    field(:source_conversation_id, :string, source: :sourceConversationId)
    field(:source_message_id, :string, source: :sourceMessageId)
    field(:filed_by_user_id, :string, source: :filedByUserId)
    field(:filed_at, :float, source: :filedAt)
    field(:renewed_at, :float, source: :renewedAt)
    field(:created_at, :float, source: :createdAt)
    field(:updated_at, :float, source: :updatedAt)
    field(:occurred_at, :float, source: :occurredAt)
    field(:accepted_at, :float, source: :acceptedAt)
    field(:last_event_id, :string, source: :lastEventId)
    field(:projection_version, :string, source: :projectionVersion)
    field(:board_id, :string, source: :boardId)
    field(:board_name, :string, source: :boardName)
    field(:widget_id, :string, source: :widgetId)
    field(:widget_name, :string, source: :widgetName)
    field(:filed_via, :string, default: "chat", source: :filedVia)
    field(:replay_start, :float, source: :replayStart)
    field(:replay_end, :float, source: :replayEnd)
    field(:replay_granularity_seconds, :integer, source: :replayGranularitySeconds)
    field(:replay_context, :map, source: :replayContext)
    field(:owner_user_id, :string, source: :ownerUserId)
    field(:finding_key, :string, source: :findingKey)
  end
end