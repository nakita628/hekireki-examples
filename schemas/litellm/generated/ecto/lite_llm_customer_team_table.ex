defmodule LiteLLM.LiteLLMCustomerTeamTable do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true, source: :customer_team_id}
  @foreign_key_type :binary_id
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          spend: float(),
          budget_reset_at: DateTime.t() | nil,
          alias: String.t() | nil,
          created_by: String.t(),
          updated_by: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          parent_org: LiteLLM.LiteLLMCustomerOrganizationTable.t() | nil,
          budget: LiteLLM.LiteLLMBudgetTable.t() | nil,
          members: [LiteLLM.LiteLLMCustomerMemberTable.t()]
        }

  schema "LiteLLM_CustomerTeamTable" do
    field(:spend, :float, default: 0.0)
    field(:budget_reset_at, LiteLLM.PrismaDateTime)
    field(:alias, :string)
    field(:created_by, :string)
    field(:updated_by, :string)
    belongs_to(:parent_org, LiteLLM.LiteLLMCustomerOrganizationTable, foreign_key: :parent_org_id)
    belongs_to(:budget, LiteLLM.LiteLLMBudgetTable, foreign_key: :budget_id)
    has_many(:members, LiteLLM.LiteLLMCustomerMemberTable, foreign_key: :parent_team_id)
    timestamps(inserted_at_source: :created_at)
  end
end