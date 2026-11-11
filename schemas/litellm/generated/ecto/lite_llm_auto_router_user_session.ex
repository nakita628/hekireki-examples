defmodule LiteLLM.LiteLLMAutoRouterUserSession do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          user_id: String.t(),
          api_key: String.t(),
          session_id: String.t(),
          router_name: String.t(),
          router_type: String.t(),
          first_turn_at: DateTime.t(),
          last_turn_at: DateTime.t(),
          last_model: String.t(),
          models: map(),
          turns: integer(),
          unordered_turns: integer(),
          covered_turns: integer(),
          cache_hits: integer(),
          same_model_turns: integer(),
          same_model_hits: integer(),
          first_visit_turns: integer(),
          first_visit_hits: integer(),
          return_turns: integer(),
          return_hits: integer(),
          return_expired_misses: integer(),
          return_within_ttl_misses: integer(),
          ttl_5m_turns: integer(),
          ttl_1h_turns: integer(),
          total_tokens: integer(),
          spend: float(),
          saved_spend: float(),
          savings_estimated_turns: integer(),
          savings_estimated_actual_spend: float(),
          savings_estimated_saved_spend: float(),
          savings_estimated_baseline_models: map(),
          classifier_cost: float(),
          classifier_cost_recorded_turns: integer(),
          tier_turns: map(),
          baseline_models: map()
        }

  schema "LiteLLM_AutoRouterUserSession" do
    field(:user_id, :string, primary_key: true)
    field(:api_key, :string, primary_key: true)
    field(:session_id, :string, primary_key: true)
    field(:router_name, :string, primary_key: true)
    field(:router_type, :string)
    field(:first_turn_at, LiteLLM.PrismaDateTime)
    field(:last_turn_at, LiteLLM.PrismaDateTime)
    field(:last_model, :string)
    field(:models, :map, default: %{})
    field(:turns, :integer, default: 0)
    field(:unordered_turns, :integer, default: 0)
    field(:covered_turns, :integer, default: 0)
    field(:cache_hits, :integer, default: 0)
    field(:same_model_turns, :integer, default: 0)
    field(:same_model_hits, :integer, default: 0)
    field(:first_visit_turns, :integer, default: 0)
    field(:first_visit_hits, :integer, default: 0)
    field(:return_turns, :integer, default: 0)
    field(:return_hits, :integer, default: 0)
    field(:return_expired_misses, :integer, default: 0)
    field(:return_within_ttl_misses, :integer, default: 0)
    field(:ttl_5m_turns, :integer, default: 0)
    field(:ttl_1h_turns, :integer, default: 0)
    field(:total_tokens, :integer, default: 0)
    field(:spend, :float, default: 0.0)
    field(:saved_spend, :float, default: 0.0)
    field(:savings_estimated_turns, :integer, default: 0)
    field(:savings_estimated_actual_spend, :float, default: 0.0)
    field(:savings_estimated_saved_spend, :float, default: 0.0)
    field(:savings_estimated_baseline_models, :map, default: %{})
    field(:classifier_cost, :float, default: 0.0)
    field(:classifier_cost_recorded_turns, :integer, default: 0)
    field(:tier_turns, :map, default: %{})
    field(:baseline_models, :map, default: %{})
  end
end