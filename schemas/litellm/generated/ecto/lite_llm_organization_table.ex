defmodule LiteLLM.LiteLLMOrganizationTable do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true, source: :organization_id}
  @foreign_key_type :binary_id
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          organization_alias: String.t(),
          metadata: map(),
          models: [String.t()],
          spend: float(),
          model_spend: map(),
          created_by: String.t(),
          updated_by: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          litellm_budget_table: LiteLLM.LiteLLMBudgetTable.t() | nil,
          object_permission: LiteLLM.LiteLLMObjectPermissionTable.t() | nil,
          teams: [LiteLLM.LiteLLMTeamTable.t()],
          users: [LiteLLM.LiteLLMUserTable.t()],
          keys: [LiteLLM.LiteLLMVerificationToken.t()],
          members: [LiteLLM.LiteLLMOrganizationMembership.t()]
        }

  schema "LiteLLM_OrganizationTable" do
    field(:organization_alias, :string)
    field(:metadata, :map, default: %{})
    field(:models, {:array, :string})
    field(:spend, :float, default: 0.0)
    field(:model_spend, :map, default: %{})
    field(:created_by, :string)
    field(:updated_by, :string)
    belongs_to(:litellm_budget_table, LiteLLM.LiteLLMBudgetTable, foreign_key: :budget_id)
    belongs_to(:object_permission, LiteLLM.LiteLLMObjectPermissionTable, foreign_key: :object_permission_id)
    has_many(:teams, LiteLLM.LiteLLMTeamTable, foreign_key: :organization_id)
    has_many(:users, LiteLLM.LiteLLMUserTable, foreign_key: :organization_id)
    has_many(:keys, LiteLLM.LiteLLMVerificationToken, foreign_key: :organization_id)
    has_many(:members, LiteLLM.LiteLLMOrganizationMembership, foreign_key: :organization_id)
    timestamps(inserted_at_source: :created_at)
  end
end