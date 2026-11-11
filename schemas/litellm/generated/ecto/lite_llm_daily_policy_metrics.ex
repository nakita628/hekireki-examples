defmodule LiteLLM.LiteLLMDailyPolicyMetrics do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          policy_id: String.t(),
          date: String.t(),
          requests_evaluated: integer(),
          passed_count: integer(),
          blocked_count: integer(),
          flagged_count: integer(),
          avg_score: float() | nil,
          avg_latency_ms: float() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "LiteLLM_DailyPolicyMetrics" do
    field(:policy_id, :string, primary_key: true)
    field(:date, :string, primary_key: true)
    field(:requests_evaluated, :integer, default: 0)
    field(:passed_count, :integer, default: 0)
    field(:blocked_count, :integer, default: 0)
    field(:flagged_count, :integer, default: 0)
    field(:avg_score, :float)
    field(:avg_latency_ms, :float)
    timestamps(inserted_at_source: :created_at)
  end
end