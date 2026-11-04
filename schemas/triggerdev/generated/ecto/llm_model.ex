defmodule Trigger.LlmModel do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          friendly_id: String.t(),
          model_name: String.t(),
          match_pattern: String.t(),
          start_date: DateTime.t() | nil,
          source: String.t(),
          provider: String.t() | nil,
          description: String.t() | nil,
          context_window: integer() | nil,
          max_output_tokens: integer() | nil,
          capabilities: [String.t()],
          is_hidden: boolean(),
          base_model_name: String.t() | nil,
          pricing_unit: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Trigger.Project.t() | nil,
          pricing_tiers: [Trigger.LlmPricingTier.t()],
          prices: [Trigger.LlmPrice.t()]
        }

  schema "llm_models" do
    field(:id, :string, primary_key: true)
    field(:friendly_id, :string)
    field(:model_name, :string)
    field(:match_pattern, :string)
    field(:start_date, Trigger.PrismaDateTime)
    field(:source, :string, default: "default")
    field(:provider, :string)
    field(:description, :string)
    field(:context_window, :integer)
    field(:max_output_tokens, :integer)
    field(:capabilities, {:array, :string})
    field(:is_hidden, :boolean, default: false)
    field(:base_model_name, :string)
    field(:pricing_unit, :string)
    belongs_to(:project, Trigger.Project, foreign_key: :project_id, type: :string)
    has_many(:pricing_tiers, Trigger.LlmPricingTier, foreign_key: :model_id, references: :id)
    has_many(:prices, Trigger.LlmPrice, foreign_key: :model_id, references: :id)
    timestamps(inserted_at_source: :created_at)
  end
end