defmodule LiteLLM.LiteLLMCustomerMemberTable do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true, source: :customer_member_id}
  @foreign_key_type :binary_id
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          spend: float(),
          alias: String.t() | nil,
          parent_team_ids: [String.t()],
          budget_override_expires_at: DateTime.t() | nil,
          created_by: String.t(),
          updated_by: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          parent_org: LiteLLM.LiteLLMCustomerOrganizationTable.t() | nil,
          budget: LiteLLM.LiteLLMBudgetTable.t() | nil,
          budget_override: LiteLLM.LiteLLMBudgetTable.t() | nil,
          budget_increase_requests: [LiteLLM.LiteLLMCustomerBudgetIncreaseRequest.t()]
        }

  schema "LiteLLM_CustomerMemberTable" do
    field(:spend, :float, default: 0.0)
    field(:alias, :string)
    field(:parent_team_ids, {:array, :string})
    field(:budget_override_expires_at, LiteLLM.PrismaDateTime)
    field(:created_by, :string)
    field(:updated_by, :string)
    belongs_to(:parent_org, LiteLLM.LiteLLMCustomerOrganizationTable, foreign_key: :parent_org_id)
    belongs_to(:budget, LiteLLM.LiteLLMBudgetTable, foreign_key: :budget_id)
    belongs_to(:budget_override, LiteLLM.LiteLLMBudgetTable, foreign_key: :budget_override_id)
    has_many(:budget_increase_requests, LiteLLM.LiteLLMCustomerBudgetIncreaseRequest, foreign_key: :customer_member_id)
    timestamps(inserted_at_source: :created_at)
  end
end