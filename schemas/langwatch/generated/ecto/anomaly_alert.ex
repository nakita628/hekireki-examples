defmodule LangWatch.AnomalyAlert do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          severity: String.t(),
          rule_name: String.t(),
          rule_type: String.t(),
          trigger_window_start: DateTime.t(),
          trigger_window_end: DateTime.t(),
          trigger_spend_usd: Decimal.t() | nil,
          trigger_event_count: integer() | nil,
          detail: map(),
          state: String.t(),
          destination_status: map(),
          detected_at: DateTime.t(),
          acknowledged_at: DateTime.t() | nil,
          resolved_at: DateTime.t() | nil,
          organization: LangWatch.Organization.t() | nil,
          rule: LangWatch.AnomalyRule.t() | nil
        }

  schema "AnomalyAlert" do
    field(:id, :string, primary_key: true)
    field(:severity, :string)
    field(:rule_name, :string, source: :ruleName)
    field(:rule_type, :string, source: :ruleType)
    field(:trigger_window_start, LangWatch.PrismaDateTime, source: :triggerWindowStart)
    field(:trigger_window_end, LangWatch.PrismaDateTime, source: :triggerWindowEnd)
    field(:trigger_spend_usd, :decimal, source: :triggerSpendUsd)
    field(:trigger_event_count, :integer, source: :triggerEventCount)
    field(:detail, :map, default: %{})
    field(:state, :string, default: "open")
    field(:destination_status, :map, default: %{}, source: :destinationStatus)
    field(:detected_at, LangWatch.PrismaDateTime, autogenerate: true, source: :detectedAt)
    field(:acknowledged_at, LangWatch.PrismaDateTime, source: :acknowledgedAt)
    field(:resolved_at, LangWatch.PrismaDateTime, source: :resolvedAt)
    field(:organization_id, :string, source: :organizationId)
    field(:rule_id, :string, source: :ruleId)
    belongs_to(:organization, LangWatch.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    belongs_to(:rule, LangWatch.AnomalyRule, foreign_key: :rule_id, define_field: false, type: :string)
  end
end