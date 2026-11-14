defmodule LangWatch.ModelProvider do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          provider: String.t(),
          routing_handle: String.t() | nil,
          enabled: boolean(),
          custom_keys: map() | nil,
          extra_headers: map() | nil,
          custom_models: map() | nil,
          custom_embeddings_models: map() | nil,
          deployment_mapping: map() | nil,
          rate_limit_rpm: integer() | nil,
          rate_limit_tpm: integer() | nil,
          rate_limit_rpd: integer() | nil,
          rotation_policy: atom(),
          provider_config: map() | nil,
          fallback_priority_global: integer() | nil,
          langy_skip_permissions_models: map() | nil,
          health_status: atom(),
          circuit_opened_at: DateTime.t() | nil,
          last_health_check_at: DateTime.t() | nil,
          disabled_at: DateTime.t() | nil,
          organization_id: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          scopes: [LangWatch.ModelProviderScope.t()]
        }

  schema "ModelProvider" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:provider, :string)
    field(:routing_handle, :string, source: :routingHandle)
    field(:enabled, :boolean)
    field(:custom_keys, :map, source: :customKeys)
    field(:extra_headers, :map, source: :extraHeaders)
    field(:custom_models, :map, source: :customModels)
    field(:custom_embeddings_models, :map, source: :customEmbeddingsModels)
    field(:deployment_mapping, :map, source: :deploymentMapping)
    field(:rate_limit_rpm, :integer, source: :rateLimitRpm)
    field(:rate_limit_tpm, :integer, source: :rateLimitTpm)
    field(:rate_limit_rpd, :integer, source: :rateLimitRpd)
    field(:rotation_policy, Ecto.Enum, values: [:MANUAL], default: :MANUAL, source: :rotationPolicy)
    field(:provider_config, :map, source: :providerConfig)
    field(:fallback_priority_global, :integer, source: :fallbackPriorityGlobal)
    field(:langy_skip_permissions_models, :map, source: :langySkipPermissionsModels)
    field(:health_status, Ecto.Enum, values: [:UNKNOWN, :HEALTHY, :DEGRADED, :CIRCUIT_OPEN], default: :UNKNOWN, source: :healthStatus)
    field(:circuit_opened_at, LangWatch.PrismaDateTime, source: :circuitOpenedAt)
    field(:last_health_check_at, LangWatch.PrismaDateTime, source: :lastHealthCheckAt)
    field(:disabled_at, LangWatch.PrismaDateTime, source: :disabledAt)
    field(:organization_id, :string, source: :organizationId)
    has_many(:scopes, LangWatch.ModelProviderScope, foreign_key: :model_provider_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end