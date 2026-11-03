defmodule Trigger.LlmPrice do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          usage_type: String.t(),
          price: Decimal.t(),
          model: Trigger.LlmModel.t() | nil,
          pricing_tier: Trigger.LlmPricingTier.t() | nil
        }

  schema "llm_prices" do
    field(:id, :string, primary_key: true)
    field(:usage_type, :string)
    field(:price, :decimal)
    belongs_to(:model, Trigger.LlmModel, foreign_key: :model_id, type: :string)
    belongs_to(:pricing_tier, Trigger.LlmPricingTier, foreign_key: :pricing_tier_id, type: :string)
  end
end