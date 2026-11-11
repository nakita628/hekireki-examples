defmodule LiteLLM.LiteLLMEndUserTable do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          user_id: String.t(),
          alias: String.t() | nil,
          spend: float(),
          allowed_model_region: String.t() | nil,
          default_model: String.t() | nil,
          models: [String.t()],
          blocked: boolean(),
          litellm_budget_table: LiteLLM.LiteLLMBudgetTable.t() | nil,
          object_permission: LiteLLM.LiteLLMObjectPermissionTable.t() | nil
        }

  schema "LiteLLM_EndUserTable" do
    field(:user_id, :string, primary_key: true)
    field(:alias, :string)
    field(:spend, :float, default: 0.0)
    field(:allowed_model_region, :string)
    field(:default_model, :string)
    field(:models, {:array, :string})
    field(:blocked, :boolean, default: false)
    belongs_to(:litellm_budget_table, LiteLLM.LiteLLMBudgetTable, foreign_key: :budget_id, type: :binary_id)
    belongs_to(:object_permission, LiteLLM.LiteLLMObjectPermissionTable, foreign_key: :object_permission_id, type: :binary_id)
  end
end