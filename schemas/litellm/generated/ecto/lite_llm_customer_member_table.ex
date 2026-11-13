defmodule LiteLLM.LiteLLMCustomerMemberTable do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true, source: :customer_member_id}
  @foreign_key_type :binary_id
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          spend: float(),
          budget_reset_at: DateTime.t() | nil,
          alias: String.t() | nil,
          budget_override_expires_at: DateTime.t() | nil,
          created_by: String.t(),
          updated_by: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          parent_team: LiteLLM.LiteLLMCustomerTeamTable.t() | nil,
          budget: LiteLLM.LiteLLMBudgetTable.t() | nil,
          budget_template: LiteLLM.LiteLLMBudgetTable.t() | nil
        }

  schema "LiteLLM_CustomerMemberTable" do
    field(:spend, :float, default: 0.0)
    field(:budget_reset_at, LiteLLM.PrismaDateTime)
    field(:alias, :string)
    field(:budget_override_expires_at, LiteLLM.PrismaDateTime)
    field(:created_by, :string)
    field(:updated_by, :string)
    belongs_to(:parent_team, LiteLLM.LiteLLMCustomerTeamTable, foreign_key: :parent_team_id)
    belongs_to(:budget, LiteLLM.LiteLLMBudgetTable, foreign_key: :budget_id)
    belongs_to(:budget_template, LiteLLM.LiteLLMBudgetTable, foreign_key: :budget_template_id)
    timestamps(inserted_at_source: :created_at)
  end
end