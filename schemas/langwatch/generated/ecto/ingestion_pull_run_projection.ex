defmodule LangWatch.IngestionPullRunProjection do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          source_id: String.t(),
          project_id: String.t(),
          enabled: boolean(),
          cron: String.t() | nil,
          cursor: String.t() | nil,
          last_run_at: float() | nil,
          last_run_outcome: String.t() | nil,
          last_run_event_count: integer(),
          last_run_error: String.t() | nil,
          last_run_error_code: String.t() | nil,
          consecutive_errors: integer(),
          last_success_at: float() | nil,
          last_run_scheduled_for: float() | nil,
          last_read_through_at: float() | nil,
          last_run_completeness: String.t() | nil,
          last_agents_listing_at: float() | nil,
          last_agents_listing_outcome: String.t() | nil,
          last_agents_listing_count: integer() | nil,
          last_agents_listing_reason: String.t() | nil,
          last_agents_listing_status: integer() | nil,
          last_people_listing_at: float() | nil,
          last_people_listing_outcome: String.t() | nil,
          last_people_directory_count: integer() | nil,
          last_people_withheld_count: integer() | nil,
          last_people_listing_reason: String.t() | nil,
          last_people_listing_status: integer() | nil,
          created_at: float(),
          updated_at: float(),
          occurred_at: float(),
          accepted_at: float(),
          last_event_id: String.t(),
          projection_version: String.t()
        }

  schema "IngestionPullRunProjection" do
    field(:id, :string, primary_key: true)
    field(:source_id, :string, source: :sourceId)
    field(:project_id, :string, source: :projectId)
    field(:enabled, :boolean, default: false)
    field(:cron, :string)
    field(:cursor, :string)
    field(:last_run_at, :float, source: :lastRunAt)
    field(:last_run_outcome, :string, source: :lastRunOutcome)
    field(:last_run_event_count, :integer, default: 0, source: :lastRunEventCount)
    field(:last_run_error, :string, source: :lastRunError)
    field(:last_run_error_code, :string, source: :lastRunErrorCode)
    field(:consecutive_errors, :integer, default: 0, source: :consecutiveErrors)
    field(:last_success_at, :float, source: :lastSuccessAt)
    field(:last_run_scheduled_for, :float, source: :lastRunScheduledFor)
    field(:last_read_through_at, :float, source: :lastReadThroughAt)
    field(:last_run_completeness, :string, source: :lastRunCompleteness)
    field(:last_agents_listing_at, :float, source: :lastAgentsListingAt)
    field(:last_agents_listing_outcome, :string, source: :lastAgentsListingOutcome)
    field(:last_agents_listing_count, :integer, source: :lastAgentsListingCount)
    field(:last_agents_listing_reason, :string, source: :lastAgentsListingReason)
    field(:last_agents_listing_status, :integer, source: :lastAgentsListingStatus)
    field(:last_people_listing_at, :float, source: :lastPeopleListingAt)
    field(:last_people_listing_outcome, :string, source: :lastPeopleListingOutcome)
    field(:last_people_directory_count, :integer, source: :lastPeopleDirectoryCount)
    field(:last_people_withheld_count, :integer, source: :lastPeopleWithheldCount)
    field(:last_people_listing_reason, :string, source: :lastPeopleListingReason)
    field(:last_people_listing_status, :integer, source: :lastPeopleListingStatus)
    field(:created_at, :float, source: :createdAt)
    field(:updated_at, :float, source: :updatedAt)
    field(:occurred_at, :float, source: :occurredAt)
    field(:accepted_at, :float, source: :acceptedAt)
    field(:last_event_id, :string, source: :lastEventId)
    field(:projection_version, :string, source: :projectionVersion)
  end
end