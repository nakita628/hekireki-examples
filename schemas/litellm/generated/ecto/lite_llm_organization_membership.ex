defmodule LiteLLM.LiteLLMOrganizationMembership do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          user_id: String.t(),
          organization_id: Ecto.UUID.t(),
          user_role: String.t() | nil,
          spend: float() | nil,
          inserted_at: DateTime.t() | nil,
          updated_at: DateTime.t() | nil,
          user: LiteLLM.LiteLLMUserTable.t() | nil,
          organization: LiteLLM.LiteLLMOrganizationTable.t() | nil,
          litellm_budget_table: LiteLLM.LiteLLMBudgetTable.t() | nil
        }

  schema "LiteLLM_OrganizationMembership" do
    field(:user_role, :string)
    field(:spend, :float, default: 0.0)
    field(:user_id, :string, primary_key: true)
    field(:organization_id, :binary_id, primary_key: true)
    belongs_to(:user, LiteLLM.LiteLLMUserTable, foreign_key: :user_id, define_field: false, type: :string, references: :user_id)
    belongs_to(:organization, LiteLLM.LiteLLMOrganizationTable, foreign_key: :organization_id, define_field: false, type: :binary_id)
    belongs_to(:litellm_budget_table, LiteLLM.LiteLLMBudgetTable, foreign_key: :budget_id, type: :binary_id)
    timestamps(inserted_at_source: :created_at)
  end
end