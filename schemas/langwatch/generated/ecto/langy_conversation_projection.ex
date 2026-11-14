defmodule LangWatch.LangyConversationProjection do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          project_id: String.t(),
          conversation_id: String.t(),
          user_id: String.t(),
          title: String.t() | nil,
          title_source: atom(),
          origin: String.t(),
          status: String.t(),
          is_shared: boolean(),
          shared_at: float() | nil,
          shared_by_id: String.t() | nil,
          message_count: integer(),
          last_activity_at: float() | nil,
          current_turn_id: String.t() | nil,
          last_error: String.t() | nil,
          last_model: String.t() | nil,
          pending_handoff_token: String.t() | nil,
          pending_handoff_turn_id: String.t() | nil,
          run_token: String.t() | nil,
          archived_at: float() | nil,
          created_at: float(),
          updated_at: float(),
          occurred_at: float(),
          accepted_at: float(),
          last_event_id: String.t(),
          projection_version: String.t()
        }

  schema "LangyConversationProjection" do
    field(:id, :string, primary_key: true)
    field(:project_id, :string, source: :projectId)
    field(:conversation_id, :string, source: :conversationId)
    field(:user_id, :string, source: :userId)
    field(:title, :string)
    field(:title_source, Ecto.Enum, values: [:derived, :auto, :user], source: :titleSource)
    field(:origin, :string, default: "interactive")
    field(:status, :string)
    field(:is_shared, :boolean, default: false, source: :isShared)
    field(:shared_at, :float, source: :sharedAt)
    field(:shared_by_id, :string, source: :sharedById)
    field(:message_count, :integer, default: 0, source: :messageCount)
    field(:last_activity_at, :float, source: :lastActivityAt)
    field(:current_turn_id, :string, source: :currentTurnId)
    field(:last_error, :string, source: :lastError)
    field(:last_model, :string, source: :lastModel)
    field(:pending_handoff_token, :string, source: :pendingHandoffToken)
    field(:pending_handoff_turn_id, :string, source: :pendingHandoffTurnId)
    field(:run_token, :string, source: :runToken)
    field(:archived_at, :float, source: :archivedAt)
    field(:created_at, :float, source: :createdAt)
    field(:updated_at, :float, source: :updatedAt)
    field(:occurred_at, :float, source: :occurredAt)
    field(:accepted_at, :float, source: :acceptedAt)
    field(:last_event_id, :string, source: :lastEventId)
    field(:projection_version, :string, source: :projectionVersion)
  end
end