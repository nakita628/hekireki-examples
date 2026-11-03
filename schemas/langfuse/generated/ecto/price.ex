defmodule Langfuse.Price do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          usage_type: String.t(),
          price: Decimal.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          model: Langfuse.Model.t() | nil,
          project: Langfuse.Project.t() | nil,
          pricing_tier: Langfuse.PricingTier.t() | nil
        }

  schema "prices" do
    field(:id, :string, primary_key: true)
    field(:usage_type, :string)
    field(:price, :decimal)
    belongs_to(:model, Langfuse.Model, foreign_key: :model_id, type: :string)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    belongs_to(:pricing_tier, Langfuse.PricingTier, foreign_key: :pricing_tier_id, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end