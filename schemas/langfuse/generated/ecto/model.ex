defmodule Langfuse.Model do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          model_name: String.t(),
          match_pattern: String.t(),
          start_date: DateTime.t() | nil,
          input_price: Decimal.t() | nil,
          output_price: Decimal.t() | nil,
          total_price: Decimal.t() | nil,
          unit: String.t() | nil,
          tokenizer_id: String.t() | nil,
          tokenizer_config: map() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Langfuse.Project.t() | nil,
          price: [Langfuse.Price.t()],
          pricing_tiers: [Langfuse.PricingTier.t()]
        }

  schema "models" do
    field(:id, :string, primary_key: true)
    field(:model_name, :string)
    field(:match_pattern, :string)
    field(:start_date, Langfuse.PrismaDateTime)
    field(:input_price, :decimal)
    field(:output_price, :decimal)
    field(:total_price, :decimal)
    field(:unit, :string)
    field(:tokenizer_id, :string)
    field(:tokenizer_config, :map)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    has_many(:price, Langfuse.Price, foreign_key: :model_id)
    has_many(:pricing_tiers, Langfuse.PricingTier, foreign_key: :model_id)
    timestamps(inserted_at_source: :created_at)
  end
end