defmodule Trigger.LlmPricingTier do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          is_default: boolean(),
          priority: integer(),
          conditions: map(),
          model: Trigger.LlmModel.t() | nil,
          prices: [Trigger.LlmPrice.t()]
        }

  schema "llm_pricing_tiers" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:is_default, :boolean, default: true)
    field(:priority, :integer, default: 0)
    field(:conditions, :map)
    belongs_to(:model, Trigger.LlmModel, foreign_key: :model_id, type: :string)
    has_many(:prices, Trigger.LlmPrice, foreign_key: :pricing_tier_id, references: :id)
  end
end