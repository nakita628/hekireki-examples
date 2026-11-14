defmodule LangWatch.InsightDailyScheduleProjection do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          project_id: String.t(),
          user_id: String.t(),
          board_kind: String.t(),
          board_id: String.t(),
          board_name: String.t(),
          state: String.t(),
          off_reason: String.t() | nil,
          hour: integer() | nil,
          timezone: String.t() | nil,
          max_insights: integer() | nil,
          last_run_id: String.t() | nil,
          last_run_at: float() | nil,
          last_run_outcome: String.t() | nil,
          last_run_reason: String.t() | nil,
          last_run_filed: integer() | nil,
          last_run_renewed: integer() | nil,
          last_run_conversation_id: String.t() | nil,
          consecutive_skips: integer(),
          skip_streak_slot: float() | nil,
          created_at: float(),
          updated_at: float(),
          occurred_at: float(),
          accepted_at: float(),
          last_event_id: String.t(),
          projection_version: String.t()
        }

  schema "InsightDailyScheduleProjection" do
    field(:id, :string, primary_key: true)
    field(:project_id, :string, source: :projectId)
    field(:user_id, :string, source: :userId)
    field(:board_kind, :string, source: :boardKind)
    field(:board_id, :string, source: :boardId)
    field(:board_name, :string, source: :boardName)
    field(:state, :string, default: "undecided")
    field(:off_reason, :string, source: :offReason)
    field(:hour, :integer)
    field(:timezone, :string)
    field(:max_insights, :integer, source: :maxInsights)
    field(:last_run_id, :string, source: :lastRunId)
    field(:last_run_at, :float, source: :lastRunAt)
    field(:last_run_outcome, :string, source: :lastRunOutcome)
    field(:last_run_reason, :string, source: :lastRunReason)
    field(:last_run_filed, :integer, source: :lastRunFiled)
    field(:last_run_renewed, :integer, source: :lastRunRenewed)
    field(:last_run_conversation_id, :string, source: :lastRunConversationId)
    field(:consecutive_skips, :integer, default: 0, source: :consecutiveSkips)
    field(:skip_streak_slot, :float, source: :skipStreakSlot)
    field(:created_at, :float, source: :createdAt)
    field(:updated_at, :float, source: :updatedAt)
    field(:occurred_at, :float, source: :occurredAt)
    field(:accepted_at, :float, source: :acceptedAt)
    field(:last_event_id, :string, source: :lastEventId)
    field(:projection_version, :string, source: :projectionVersion)
  end
end