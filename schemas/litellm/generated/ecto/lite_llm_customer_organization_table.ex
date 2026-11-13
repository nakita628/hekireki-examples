defmodule LiteLLM.LiteLLMCustomerOrganizationTable do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true, source: :customer_org_id}
  @foreign_key_type :binary_id
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          spend: float(),
          budget_reset_at: DateTime.t() | nil,
          budget_ids: [String.t()],
          budget_names: map(),
          alias: String.t() | nil,
          created_by: String.t(),
          updated_by: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          budget: LiteLLM.LiteLLMBudgetTable.t() | nil,
          default_budget: LiteLLM.LiteLLMBudgetTable.t() | nil,
          teams: [LiteLLM.LiteLLMCustomerTeamTable.t()]
        }

  schema "LiteLLM_CustomerOrganizationTable" do
    field(:spend, :float, default: 0.0)
    field(:budget_reset_at, LiteLLM.PrismaDateTime)
    field(:budget_ids, {:array, :string})
    field(:budget_names, :map, default: %{})
    field(:alias, :string)
    field(:created_by, :string)
    field(:updated_by, :string)
    belongs_to(:budget, LiteLLM.LiteLLMBudgetTable, foreign_key: :budget_id)
    belongs_to(:default_budget, LiteLLM.LiteLLMBudgetTable, foreign_key: :default_budget_id)
    has_many(:teams, LiteLLM.LiteLLMCustomerTeamTable, foreign_key: :parent_org_id)
    timestamps(inserted_at_source: :created_at)
  end
end