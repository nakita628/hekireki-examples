defmodule Trigger.WebhookDelivery do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          friendly_id: String.t(),
          webhook_endpoint_id: String.t(),
          organization_id: String.t(),
          project_id: String.t(),
          runtime_environment_id: String.t(),
          environment_type: atom(),
          external_delivery_id: String.t(),
          idempotency_key: String.t(),
          run_id: String.t() | nil,
          status: atom(),
          is_test: boolean(),
          parsed_event: map() | nil,
          headers: map() | nil,
          raw_body_hash: String.t() | nil,
          error_message: String.t() | nil,
          filter_reason: String.t() | nil,
          targets: map(),
          processed_at: DateTime.t() | nil,
          retention_days: integer(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "WebhookDelivery" do
    field(:id, :string, primary_key: true)
    field(:friendly_id, :string, source: :friendlyId)
    field(:webhook_endpoint_id, :string, source: :webhookEndpointId)
    field(:organization_id, :string, source: :organizationId)
    field(:project_id, :string, source: :projectId)
    field(:runtime_environment_id, :string, source: :runtimeEnvironmentId)
    field(:environment_type, Ecto.Enum, values: [:PRODUCTION, :STAGING, :DEVELOPMENT, :PREVIEW], source: :environmentType)
    field(:external_delivery_id, :string, source: :externalDeliveryId)
    field(:idempotency_key, :string, source: :idempotencyKey)
    field(:run_id, :string, source: :runId)
    field(:status, Ecto.Enum, values: [:PENDING, :PROCESSING, :SUCCEEDED, :FAILED, :FILTERED, :UNMATCHED], default: :PENDING)
    field(:is_test, :boolean, default: false, source: :isTest)
    field(:parsed_event, :map, source: :parsedEvent)
    field(:headers, :map)
    field(:raw_body_hash, :string, source: :rawBodyHash)
    field(:error_message, :string, source: :errorMessage)
    field(:filter_reason, :string, source: :filterReason)
    field(:targets, :map)
    field(:processed_at, Trigger.PrismaDateTime, source: :processedAt)
    field(:retention_days, :integer, primary_key: true, default: 30, source: :retentionDays)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end