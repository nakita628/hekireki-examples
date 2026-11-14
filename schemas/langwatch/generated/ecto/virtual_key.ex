defmodule LangWatch.VirtualKey do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          organization_id: String.t(),
          name: String.t(),
          description: String.t() | nil,
          status: atom(),
          purpose: atom(),
          external_id: String.t() | nil,
          metadata: map(),
          disabled_at: DateTime.t() | nil,
          disabled_reason: String.t() | nil,
          expires_at: DateTime.t() | nil,
          hashed_secret: String.t(),
          display_prefix: String.t(),
          trace_project_id: String.t() | nil,
          config: map(),
          revision: integer(),
          previous_hashed_secret: String.t() | nil,
          previous_secret_valid_until: DateTime.t() | nil,
          revoked_at: DateTime.t() | nil,
          revoked_by_id: String.t() | nil,
          created_by_id: String.t(),
          last_used_at: DateTime.t() | nil,
          routing_mode: atom(),
          connect_services: [String.t()],
          license_token_hash: String.t() | nil,
          license_instance_id: String.t() | nil,
          license_expires_at: DateTime.t() | nil,
          license_id: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          principal_user: LangWatch.User.t() | nil,
          routing_policy: LangWatch.RoutingPolicy.t() | nil,
          scopes: [LangWatch.VirtualKeyScope.t()],
          ledger_entries: [LangWatch.GatewayBudgetLedger.t()]
        }

  schema "VirtualKey" do
    field(:id, :string, primary_key: true)
    field(:organization_id, :string, source: :organizationId)
    field(:name, :string)
    field(:description, :string)
    field(:status, Ecto.Enum, values: [:ACTIVE, :DISABLED, :REVOKED], default: :ACTIVE)
    field(:purpose, Ecto.Enum, values: [:USER, :LANGY, :CONNECT], default: :USER)
    field(:external_id, :string, source: :externalId)
    field(:metadata, :map, default: %{})
    field(:disabled_at, LangWatch.PrismaDateTime, source: :disabledAt)
    field(:disabled_reason, :string, source: :disabledReason)
    field(:expires_at, LangWatch.PrismaDateTime, source: :expiresAt)
    field(:hashed_secret, :string, source: :hashedSecret)
    field(:display_prefix, :string, source: :displayPrefix)
    field(:trace_project_id, :string, source: :traceProjectId)
    field(:config, :map, default: %{})
    field(:revision, :integer, default: 0)
    field(:previous_hashed_secret, :string, source: :previousHashedSecret)
    field(:previous_secret_valid_until, LangWatch.PrismaDateTime, source: :previousSecretValidUntil)
    field(:revoked_at, LangWatch.PrismaDateTime, source: :revokedAt)
    field(:revoked_by_id, :string, source: :revokedById)
    field(:created_by_id, :string, source: :createdById)
    field(:last_used_at, LangWatch.PrismaDateTime, source: :lastUsedAt)
    field(:routing_mode, Ecto.Enum, values: [:NONE, :FALLBACK_ALL, :POLICY], default: :NONE, source: :routingMode)
    field(:connect_services, {:array, :string}, source: :connectServices)
    field(:license_token_hash, :string, source: :licenseTokenHash)
    field(:license_instance_id, :string, source: :licenseInstanceId)
    field(:license_expires_at, LangWatch.PrismaDateTime, source: :licenseExpiresAt)
    field(:license_id, :string, source: :licenseId)
    field(:principal_user_id, :string, source: :principalUserId)
    field(:routing_policy_id, :string, source: :routingPolicyId)
    belongs_to(:principal_user, LangWatch.User, foreign_key: :principal_user_id, define_field: false, type: :string)
    belongs_to(:routing_policy, LangWatch.RoutingPolicy, foreign_key: :routing_policy_id, define_field: false, type: :string)
    has_many(:scopes, LangWatch.VirtualKeyScope, foreign_key: :virtual_key_id, references: :id)
    has_many(:ledger_entries, LangWatch.GatewayBudgetLedger, foreign_key: :virtual_key_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end