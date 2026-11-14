defmodule LangWatch.WebhookEndpoint do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          destination_kind: atom(),
          url: String.t() | nil,
          sqs_queue_url: String.t() | nil,
          sqs_role_arn: String.t() | nil,
          sqs_external_id: String.t() | nil,
          sqs_access_key_id: String.t() | nil,
          sqs_secret_access_key_encrypted: String.t() | nil,
          secret_encrypted: String.t(),
          signature_scheme: String.t() | nil,
          idempotency_key: String.t() | nil,
          previous_secret_encrypted: String.t() | nil,
          previous_secret_expires_at: DateTime.t() | nil,
          enabled_events: [String.t()],
          status: atom(),
          disabled_reason: String.t() | nil,
          disabled_at: DateTime.t() | nil,
          failing_since: DateTime.t() | nil,
          max_batch_size: integer(),
          max_batch_delay_ms: integer(),
          max_in_flight: integer(),
          allow_self_signed_certificate: boolean(),
          last_success_at: DateTime.t() | nil,
          last_failure_at: DateTime.t() | nil,
          archived_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: LangWatch.Organization.t() | nil,
          deliveries: [LangWatch.WebhookEndpointDelivery.t()]
        }

  schema "WebhookEndpoint" do
    field(:id, :string, primary_key: true)
    field(:destination_kind, Ecto.Enum, values: [:http, :sqs], default: :http, source: :destinationKind)
    field(:url, :string)
    field(:sqs_queue_url, :string, source: :sqsQueueUrl)
    field(:sqs_role_arn, :string, source: :sqsRoleArn)
    field(:sqs_external_id, :string, source: :sqsExternalId)
    field(:sqs_access_key_id, :string, source: :sqsAccessKeyId)
    field(:sqs_secret_access_key_encrypted, :string, source: :sqsSecretAccessKeyEncrypted)
    field(:secret_encrypted, :string, source: :secretEncrypted)
    field(:signature_scheme, :string, source: :signatureScheme)
    field(:idempotency_key, :string, source: :idempotencyKey)
    field(:previous_secret_encrypted, :string, source: :previousSecretEncrypted)
    field(:previous_secret_expires_at, LangWatch.PrismaDateTime, source: :previousSecretExpiresAt)
    field(:enabled_events, {:array, :string}, source: :enabledEvents)
    field(:status, Ecto.Enum, values: [:ACTIVE, :DISABLED], default: :ACTIVE)
    field(:disabled_reason, :string, source: :disabledReason)
    field(:disabled_at, LangWatch.PrismaDateTime, source: :disabledAt)
    field(:failing_since, LangWatch.PrismaDateTime, source: :failingSince)
    field(:max_batch_size, :integer, default: 100, source: :maxBatchSize)
    field(:max_batch_delay_ms, :integer, default: 250, source: :maxBatchDelayMs)
    field(:max_in_flight, :integer, default: 4, source: :maxInFlight)
    field(:allow_self_signed_certificate, :boolean, default: false, source: :allowSelfSignedCertificate)
    field(:last_success_at, LangWatch.PrismaDateTime, source: :lastSuccessAt)
    field(:last_failure_at, LangWatch.PrismaDateTime, source: :lastFailureAt)
    field(:archived_at, LangWatch.PrismaDateTime, source: :archivedAt)
    field(:organization_id, :string, source: :organizationId)
    belongs_to(:organization, LangWatch.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    has_many(:deliveries, LangWatch.WebhookEndpointDelivery, foreign_key: :endpoint_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end