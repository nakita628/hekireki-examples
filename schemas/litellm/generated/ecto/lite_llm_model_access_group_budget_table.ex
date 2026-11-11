defmodule LiteLLM.LiteLLMModelAccessGroupBudgetTable do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          access_group_name: String.t(),
          spend: float(),
          created_by: String.t() | nil,
          updated_by: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          litellm_budget_table: LiteLLM.LiteLLMBudgetTable.t() | nil
        }

  schema "LiteLLM_ModelAccessGroupBudgetTable" do
    field(:access_group_name, :string, primary_key: true)
    field(:spend, :float, default: 0.0)
    field(:created_by, :string)
    field(:updated_by, :string)
    belongs_to(:litellm_budget_table, LiteLLM.LiteLLMBudgetTable, foreign_key: :budget_id, type: :binary_id)
    timestamps(inserted_at_source: :created_at)
  end
end