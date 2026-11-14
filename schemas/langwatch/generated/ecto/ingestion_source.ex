defmodule LangWatch.IngestionSource do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          source_type: String.t(),
          name: String.t(),
          description: String.t() | nil,
          ingest_secret_hash: String.t(),
          parser_config: map(),
          poller_cursor: map() | nil,
          error_count: integer(),
          pull_schedule: String.t() | nil,
          status: String.t(),
          trace_project_id: String.t() | nil,
          provider_account_id: String.t() | nil,
          last_event_at: DateTime.t() | nil,
          last_success_at: DateTime.t() | nil,
          last_read_through_at: DateTime.t() | nil,
          last_run_completeness: String.t() | nil,
          unpriced_usage_since: DateTime.t() | nil,
          unpriced_usage_through: DateTime.t() | nil,
          archived_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: LangWatch.Organization.t() | nil,
          team: LangWatch.Team.t() | nil,
          created_by: LangWatch.User.t() | nil
        }

  schema "IngestionSource" do
    field(:id, :string, primary_key: true)
    field(:source_type, :string, source: :sourceType)
    field(:name, :string)
    field(:description, :string)
    field(:ingest_secret_hash, :string, source: :ingestSecretHash)
    field(:parser_config, :map, default: %{}, source: :parserConfig)
    field(:poller_cursor, :map, source: :pollerCursor)
    field(:error_count, :integer, default: 0, source: :errorCount)
    field(:pull_schedule, :string, source: :pullSchedule)
    field(:status, :string, default: "awaiting_first_event")
    field(:trace_project_id, :string, source: :traceProjectId)
    field(:provider_account_id, :string, source: :providerAccountId)
    field(:last_event_at, LangWatch.PrismaDateTime, source: :lastEventAt)
    field(:last_success_at, LangWatch.PrismaDateTime, source: :lastSuccessAt)
    field(:last_read_through_at, LangWatch.PrismaDateTime, source: :lastReadThroughAt)
    field(:last_run_completeness, :string, source: :lastRunCompleteness)
    field(:unpriced_usage_since, LangWatch.PrismaDateTime, source: :unpricedUsageSince)
    field(:unpriced_usage_through, LangWatch.PrismaDateTime, source: :unpricedUsageThrough)
    field(:archived_at, LangWatch.PrismaDateTime, source: :archivedAt)
    field(:organization_id, :string, source: :organizationId)
    field(:team_id, :string, source: :teamId)
    field(:created_by_id, :string, source: :createdById)
    belongs_to(:organization, LangWatch.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    belongs_to(:team, LangWatch.Team, foreign_key: :team_id, define_field: false, type: :string)
    belongs_to(:created_by, LangWatch.User, foreign_key: :created_by_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end