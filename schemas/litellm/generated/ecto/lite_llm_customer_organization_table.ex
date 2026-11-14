defmodule LiteLLM.LiteLLMCustomerOrganizationTable do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true, source: :customer_org_id}
  @foreign_key_type :binary_id
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          spend: float(),
          budget_template_ids: [String.t()],
          alias: String.t() | nil,
          created_by: String.t(),
          updated_by: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          budget: LiteLLM.LiteLLMBudgetTable.t() | nil,
          default_budget_template: LiteLLM.LiteLLMBudgetTable.t() | nil,
          teams: [LiteLLM.LiteLLMCustomerTeamTable.t()],
          members: [LiteLLM.LiteLLMCustomerMemberTable.t()]
        }

  schema "LiteLLM_CustomerOrganizationTable" do
    field(:spend, :float, default: 0.0)
    field(:budget_template_ids, {:array, :string})
    field(:alias, :string)
    field(:created_by, :string)
    field(:updated_by, :string)
    belongs_to(:budget, LiteLLM.LiteLLMBudgetTable, foreign_key: :budget_id)
    belongs_to(:default_budget_template, LiteLLM.LiteLLMBudgetTable, foreign_key: :default_budget_template_id)
    has_many(:teams, LiteLLM.LiteLLMCustomerTeamTable, foreign_key: :parent_org_id)
    has_many(:members, LiteLLM.LiteLLMCustomerMemberTable, foreign_key: :parent_org_id)
    timestamps(inserted_at_source: :created_at)
  end
end