defmodule LiteLLM.LiteLLMTagTable do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          tag_name: String.t(),
          description: String.t() | nil,
          models: [String.t()],
          model_info: map() | nil,
          spend: float(),
          created_by: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          litellm_budget_table: LiteLLM.LiteLLMBudgetTable.t() | nil
        }

  schema "LiteLLM_TagTable" do
    field(:tag_name, :string, primary_key: true)
    field(:description, :string)
    field(:models, {:array, :string})
    field(:model_info, :map)
    field(:spend, :float, default: 0.0)
    field(:created_by, :string)
    belongs_to(:litellm_budget_table, LiteLLM.LiteLLMBudgetTable, foreign_key: :budget_id, type: :binary_id)
    timestamps(inserted_at_source: :created_at)
  end
end