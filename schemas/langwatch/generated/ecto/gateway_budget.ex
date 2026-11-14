defmodule LangWatch.GatewayBudget do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          organization_id: String.t(),
          scope_type: atom(),
          scope_id: String.t(),
          provider_key: String.t() | nil,
          name: String.t(),
          description: String.t() | nil,
          window: atom(),
          limit_usd: Decimal.t(),
          on_breach: atom(),
          timezone: String.t() | nil,
          external_id: String.t() | nil,
          metadata: map(),
          spent_usd: Decimal.t(),
          current_period_started_at: DateTime.t(),
          resets_at: DateTime.t(),
          last_reset_at: DateTime.t() | nil,
          cycle_anchor_at: DateTime.t() | nil,
          archived_at: DateTime.t() | nil,
          managed_by_virtual_key_id: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          created_by: LangWatch.User.t() | nil,
          ledger_entries: [LangWatch.GatewayBudgetLedger.t()],
          bucket_boundaries: [LangWatch.GatewayBudgetBucketBoundary.t()]
        }

  schema "GatewayBudget" do
    field(:id, :string, primary_key: true)
    field(:organization_id, :string, source: :organizationId)
    field(:scope_type, Ecto.Enum, values: [:ORGANIZATION, :TEAM, :PROJECT, :VIRTUAL_KEY, :PRINCIPAL, :GROUP, :ATTRIBUTED_USER], source: :scopeType)
    field(:scope_id, :string, source: :scopeId)
    field(:provider_key, :string, source: :providerKey)
    field(:name, :string)
    field(:description, :string)
    field(:window, Ecto.Enum, values: [:MINUTE, :HOUR, :DAY, :WEEK, :MONTH, :TOTAL, :MANUAL])
    field(:limit_usd, :decimal, source: :limitUsd)
    field(:on_breach, Ecto.Enum, values: [:BLOCK, :WARN], default: :BLOCK, source: :onBreach)
    field(:timezone, :string)
    field(:external_id, :string, source: :externalId)
    field(:metadata, :map, default: %{})
    field(:spent_usd, :decimal, default: Decimal.new("0"), source: :spentUsd)
    field(:current_period_started_at, LangWatch.PrismaDateTime, autogenerate: true, source: :currentPeriodStartedAt)
    field(:resets_at, LangWatch.PrismaDateTime, source: :resetsAt)
    field(:last_reset_at, LangWatch.PrismaDateTime, source: :lastResetAt)
    field(:cycle_anchor_at, LangWatch.PrismaDateTime, source: :cycleAnchorAt)
    field(:archived_at, LangWatch.PrismaDateTime, source: :archivedAt)
    field(:managed_by_virtual_key_id, :string, source: :managedByVirtualKeyId)
    field(:created_by_id, :string, source: :createdById)
    belongs_to(:created_by, LangWatch.User, foreign_key: :created_by_id, define_field: false, type: :string)
    has_many(:ledger_entries, LangWatch.GatewayBudgetLedger, foreign_key: :budget_id, references: :id)
    has_many(:bucket_boundaries, LangWatch.GatewayBudgetBucketBoundary, foreign_key: :budget_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end