defmodule LiteLLM.LiteLLMAutoRouterDailySpend do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          date: String.t(),
          api_key: String.t(),
          user_id: String.t(),
          router_name: String.t(),
          router_type: String.t(),
          turns: integer(),
          total_tokens: integer(),
          token_recorded_turns: integer(),
          spend: float(),
          saved_spend: float(),
          savings_estimated_turns: integer(),
          savings_estimated_actual_spend: float(),
          savings_estimated_saved_spend: float(),
          classifier_cost: float(),
          classifier_cost_recorded_turns: integer()
        }

  schema "LiteLLM_AutoRouterDailySpend" do
    field(:date, :string, primary_key: true)
    field(:api_key, :string, primary_key: true)
    field(:user_id, :string, primary_key: true)
    field(:router_name, :string, primary_key: true)
    field(:router_type, :string, primary_key: true)
    field(:turns, :integer, default: 0)
    field(:total_tokens, :integer, default: 0)
    field(:token_recorded_turns, :integer, default: 0)
    field(:spend, :float, default: 0.0)
    field(:saved_spend, :float, default: 0.0)
    field(:savings_estimated_turns, :integer, default: 0)
    field(:savings_estimated_actual_spend, :float, default: 0.0)
    field(:savings_estimated_saved_spend, :float, default: 0.0)
    field(:classifier_cost, :float, default: 0.0)
    field(:classifier_cost_recorded_turns, :integer, default: 0)
  end
end