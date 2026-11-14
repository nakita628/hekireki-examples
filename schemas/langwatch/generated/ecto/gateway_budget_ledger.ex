defmodule LangWatch.GatewayBudgetLedger do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          model_provider_id: String.t() | nil,
          provider_key: String.t() | nil,
          gateway_request_id: String.t(),
          amount_usd: Decimal.t(),
          tokens_input: integer(),
          tokens_output: integer(),
          tokens_cache_read: integer(),
          tokens_cache_write: integer(),
          model: String.t(),
          duration_ms: integer() | nil,
          status: atom(),
          occurred_at: DateTime.t(),
          budget: LangWatch.GatewayBudget.t() | nil,
          virtual_key: LangWatch.VirtualKey.t() | nil
        }

  schema "GatewayBudgetLedger" do
    field(:id, :string, primary_key: true)
    field(:model_provider_id, :string, source: :modelProviderId)
    field(:provider_key, :string, source: :providerKey)
    field(:gateway_request_id, :string, source: :gatewayRequestId)
    field(:amount_usd, :decimal, source: :amountUsd)
    field(:tokens_input, :integer, default: 0, source: :tokensInput)
    field(:tokens_output, :integer, default: 0, source: :tokensOutput)
    field(:tokens_cache_read, :integer, default: 0, source: :tokensCacheRead)
    field(:tokens_cache_write, :integer, default: 0, source: :tokensCacheWrite)
    field(:model, :string)
    field(:duration_ms, :integer, source: :durationMs)
    field(:status, Ecto.Enum, values: [:SUCCESS, :PROVIDER_ERROR, :BLOCKED_BY_GUARDRAIL, :CANCELLED], default: :SUCCESS)
    field(:occurred_at, LangWatch.PrismaDateTime, autogenerate: true, source: :occurredAt)
    field(:budget_id, :string, source: :budgetId)
    field(:virtual_key_id, :string, source: :virtualKeyId)
    belongs_to(:budget, LangWatch.GatewayBudget, foreign_key: :budget_id, define_field: false, type: :string)
    belongs_to(:virtual_key, LangWatch.VirtualKey, foreign_key: :virtual_key_id, define_field: false, type: :string)
  end
end