defmodule LangWatch.TopicClusteringRunProjection do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          project_id: String.t(),
          last_requested_at: float() | nil,
          last_request_trigger: String.t() | nil,
          last_run_at: float() | nil,
          last_run_outcome: String.t() | nil,
          last_run_mode: String.t() | nil,
          last_run_skipped_reason: String.t() | nil,
          last_run_error: String.t() | nil,
          last_run_error_code: String.t() | nil,
          last_run_error_user_actionable: boolean(),
          last_run_traces_processed: integer(),
          last_run_topics_count: integer(),
          last_run_subtopics_count: integer(),
          last_run_pages: integer(),
          in_progress_run_id: String.t() | nil,
          in_progress_traces: integer(),
          in_progress_pages: integer(),
          in_progress_started_at: float() | nil,
          created_at: float(),
          updated_at: float(),
          occurred_at: float(),
          accepted_at: float(),
          last_event_id: String.t(),
          projection_version: String.t()
        }

  schema "TopicClusteringRunProjection" do
    field(:id, :string, primary_key: true)
    field(:project_id, :string, source: :projectId)
    field(:last_requested_at, :float, source: :lastRequestedAt)
    field(:last_request_trigger, :string, source: :lastRequestTrigger)
    field(:last_run_at, :float, source: :lastRunAt)
    field(:last_run_outcome, :string, source: :lastRunOutcome)
    field(:last_run_mode, :string, source: :lastRunMode)
    field(:last_run_skipped_reason, :string, source: :lastRunSkippedReason)
    field(:last_run_error, :string, source: :lastRunError)
    field(:last_run_error_code, :string, source: :lastRunErrorCode)
    field(:last_run_error_user_actionable, :boolean, default: false, source: :lastRunErrorUserActionable)
    field(:last_run_traces_processed, :integer, default: 0, source: :lastRunTracesProcessed)
    field(:last_run_topics_count, :integer, default: 0, source: :lastRunTopicsCount)
    field(:last_run_subtopics_count, :integer, default: 0, source: :lastRunSubtopicsCount)
    field(:last_run_pages, :integer, default: 0, source: :lastRunPages)
    field(:in_progress_run_id, :string, source: :inProgressRunId)
    field(:in_progress_traces, :integer, default: 0, source: :inProgressTraces)
    field(:in_progress_pages, :integer, default: 0, source: :inProgressPages)
    field(:in_progress_started_at, :float, source: :inProgressStartedAt)
    field(:created_at, :float, source: :createdAt)
    field(:updated_at, :float, source: :updatedAt)
    field(:occurred_at, :float, source: :occurredAt)
    field(:accepted_at, :float, source: :acceptedAt)
    field(:last_event_id, :string, source: :lastEventId)
    field(:projection_version, :string, source: :projectionVersion)
  end
end