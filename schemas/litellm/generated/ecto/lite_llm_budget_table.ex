defmodule LiteLLM.LiteLLMBudgetTable do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true, source: :budget_id}
  @foreign_key_type :binary_id
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          name: String.t() | nil,
          max_budget: float() | nil,
          soft_budget: float() | nil,
          max_parallel_requests: integer() | nil,
          tpm_limit: integer() | nil,
          rpm_limit: integer() | nil,
          tpd_limit: integer() | nil,
          model_max_budget: map() | nil,
          model_tpm_limit: map() | nil,
          model_rpm_limit: map() | nil,
          budget_duration: String.t() | nil,
          budget_limits: map() | nil,
          cap_policy: String.t() | nil,
          budget_reset_at: DateTime.t() | nil,
          allowed_models: [String.t()],
          temp_budget_increase: float() | nil,
          temp_budget_expiry: DateTime.t() | nil,
          created_by: String.t(),
          updated_by: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          customer_organization_defaults: [LiteLLM.LiteLLMCustomerOrganizationTable.t()],
          customer_organizations: [LiteLLM.LiteLLMCustomerOrganizationTable.t()],
          customer_teams: [LiteLLM.LiteLLMCustomerTeamTable.t()],
          customer_members: [LiteLLM.LiteLLMCustomerMemberTable.t()],
          customer_member_overrides: [LiteLLM.LiteLLMCustomerMemberTable.t()],
          customer_team_overrides: [LiteLLM.LiteLLMCustomerTeamTable.t()],
          organization: [LiteLLM.LiteLLMOrganizationTable.t()],
          projects: [LiteLLM.LiteLLMProjectTable.t()],
          keys: [LiteLLM.LiteLLMVerificationToken.t()],
          end_users: [LiteLLM.LiteLLMEndUserTable.t()],
          tags: [LiteLLM.LiteLLMTagTable.t()],
          model_access_groups: [LiteLLM.LiteLLMModelAccessGroupBudgetTable.t()],
          team_membership: [LiteLLM.LiteLLMTeamMembership.t()],
          organization_membership: [LiteLLM.LiteLLMOrganizationMembership.t()]
        }

  schema "LiteLLM_BudgetTable" do
    field(:name, :string)
    field(:max_budget, :float)
    field(:soft_budget, :float)
    field(:max_parallel_requests, :integer)
    field(:tpm_limit, :integer)
    field(:rpm_limit, :integer)
    field(:tpd_limit, :integer)
    field(:model_max_budget, :map)
    field(:model_tpm_limit, :map)
    field(:model_rpm_limit, :map)
    field(:budget_duration, :string)
    field(:budget_limits, :map)
    field(:cap_policy, :string)
    field(:budget_reset_at, LiteLLM.PrismaDateTime)
    field(:allowed_models, {:array, :string})
    field(:temp_budget_increase, :float)
    field(:temp_budget_expiry, LiteLLM.PrismaDateTime)
    field(:created_by, :string)
    field(:updated_by, :string)
    has_many(:customer_organization_defaults, LiteLLM.LiteLLMCustomerOrganizationTable, foreign_key: :default_budget_template_id)
    has_many(:customer_organizations, LiteLLM.LiteLLMCustomerOrganizationTable, foreign_key: :budget_id)
    has_many(:customer_teams, LiteLLM.LiteLLMCustomerTeamTable, foreign_key: :budget_id)
    has_many(:customer_members, LiteLLM.LiteLLMCustomerMemberTable, foreign_key: :budget_id)
    has_many(:customer_member_overrides, LiteLLM.LiteLLMCustomerMemberTable, foreign_key: :budget_override_id)
    has_many(:customer_team_overrides, LiteLLM.LiteLLMCustomerTeamTable, foreign_key: :budget_override_id)
    has_many(:organization, LiteLLM.LiteLLMOrganizationTable, foreign_key: :budget_id)
    has_many(:projects, LiteLLM.LiteLLMProjectTable, foreign_key: :budget_id)
    has_many(:keys, LiteLLM.LiteLLMVerificationToken, foreign_key: :budget_id)
    has_many(:end_users, LiteLLM.LiteLLMEndUserTable, foreign_key: :budget_id)
    has_many(:tags, LiteLLM.LiteLLMTagTable, foreign_key: :budget_id)
    has_many(:model_access_groups, LiteLLM.LiteLLMModelAccessGroupBudgetTable, foreign_key: :budget_id)
    has_many(:team_membership, LiteLLM.LiteLLMTeamMembership, foreign_key: :budget_id)
    has_many(:organization_membership, LiteLLM.LiteLLMOrganizationMembership, foreign_key: :budget_id)
    timestamps(inserted_at_source: :created_at)
  end
end