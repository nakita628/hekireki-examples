defmodule LangWatch.WebhookEndpointDelivery do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          channel: atom(),
          organization_id: String.t() | nil,
          dispatch_id: String.t(),
          attempt: integer() | nil,
          event_count: integer() | nil,
          response_status: integer() | nil,
          latency_ms: integer() | nil,
          error: String.t() | nil,
          response: map() | nil,
          outcome: atom(),
          fired_at: DateTime.t(),
          inserted_at: DateTime.t(),
          endpoint: LangWatch.WebhookEndpoint.t() | nil,
          project: LangWatch.Project.t() | nil,
          trigger: LangWatch.Trigger.t() | nil
        }

  schema "WebhookEndpointDelivery" do
    field(:id, :string, primary_key: true)
    field(:channel, Ecto.Enum, values: [:platform, :automations], default: :platform)
    field(:organization_id, :string, source: :organizationId)
    field(:dispatch_id, :string, source: :dispatchId)
    field(:attempt, :integer)
    field(:event_count, :integer, source: :eventCount)
    field(:response_status, :integer, source: :responseStatus)
    field(:latency_ms, :integer, source: :latencyMs)
    field(:error, :string)
    field(:response, :map)
    field(:outcome, Ecto.Enum, values: [:success, :retryable, :terminal, :pending])
    field(:fired_at, LangWatch.PrismaDateTime, autogenerate: true, source: :firedAt)
    field(:endpoint_id, :string, source: :endpointId)
    field(:project_id, :string, source: :projectId)
    field(:trigger_id, :string, source: :triggerId)
    belongs_to(:endpoint, LangWatch.WebhookEndpoint, foreign_key: :endpoint_id, define_field: false, type: :string)
    belongs_to(:project, LangWatch.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:trigger, LangWatch.Trigger, foreign_key: :trigger_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end