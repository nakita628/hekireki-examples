defmodule LiteLLM.LiteLLMBudgetWindowSpend do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          entity_type: String.t(),
          entity_id: String.t(),
          window_duration: String.t(),
          window_start: DateTime.t(),
          spend: float(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "LiteLLM_BudgetWindowSpend" do
    field(:entity_type, :string, primary_key: true)
    field(:entity_id, :string, primary_key: true)
    field(:window_duration, :string, primary_key: true)
    field(:window_start, LiteLLM.PrismaDateTime)
    field(:spend, :float, default: 0.0)
    timestamps(inserted_at_source: :created_at)
  end
end