defmodule Langfuse.PricingTier do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          is_default: boolean(),
          priority: integer(),
          conditions: map(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          model: Langfuse.Model.t() | nil,
          prices: [Langfuse.Price.t()]
        }

  schema "pricing_tiers" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:is_default, :boolean, default: false)
    field(:priority, :integer)
    field(:conditions, :map)
    belongs_to(:model, Langfuse.Model, foreign_key: :model_id, type: :string)
    has_many(:prices, Langfuse.Price, foreign_key: :pricing_tier_id)
    timestamps(inserted_at_source: :created_at)
  end
end