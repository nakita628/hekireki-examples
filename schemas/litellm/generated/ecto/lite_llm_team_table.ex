defmodule LiteLLM.LiteLLMTeamTable do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true, source: :team_id}
  @foreign_key_type :binary_id
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          team_alias: String.t() | nil,
          admins: [String.t()],
          members: [String.t()],
          members_with_roles: map(),
          metadata: map(),
          max_budget: float() | nil,
          soft_budget: float() | nil,
          spend: float(),
          models: [String.t()],
          max_parallel_requests: integer() | nil,
          tpm_limit: integer() | nil,
          rpm_limit: integer() | nil,
          tpd_limit: integer() | nil,
          budget_duration: String.t() | nil,
          budget_reset_at: DateTime.t() | nil,
          blocked: boolean(),
          model_spend: map(),
          model_max_budget: map(),
          router_settings: map() | nil,
          team_member_permissions: [String.t()],
          access_group_ids: [String.t()],
          policies: [String.t()],
          default_team_member_models: [String.t()],
          budget_limits: map() | nil,
          allow_team_guardrail_config: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          litellm_organization_table: LiteLLM.LiteLLMOrganizationTable.t() | nil,
          litellm_model_table: LiteLLM.LiteLLMModelTable.t() | nil,
          object_permission: LiteLLM.LiteLLMObjectPermissionTable.t() | nil,
          projects: [LiteLLM.LiteLLMProjectTable.t()]
        }

  schema "LiteLLM_TeamTable" do
    field(:team_alias, :string)
    field(:admins, {:array, :string})
    field(:members, {:array, :string})
    field(:members_with_roles, :map, default: %{})
    field(:metadata, :map, default: %{})
    field(:max_budget, :float)
    field(:soft_budget, :float)
    field(:spend, :float, default: 0.0)
    field(:models, {:array, :string})
    field(:max_parallel_requests, :integer)
    field(:tpm_limit, :integer)
    field(:rpm_limit, :integer)
    field(:tpd_limit, :integer)
    field(:budget_duration, :string)
    field(:budget_reset_at, LiteLLM.PrismaDateTime)
    field(:blocked, :boolean, default: false)
    field(:model_spend, :map, default: %{})
    field(:model_max_budget, :map, default: %{})
    field(:router_settings, :map, default: %{})
    field(:team_member_permissions, {:array, :string})
    field(:access_group_ids, {:array, :string})
    field(:policies, {:array, :string})
    field(:default_team_member_models, {:array, :string})
    field(:budget_limits, :map)
    field(:allow_team_guardrail_config, :boolean, default: false)
    belongs_to(:litellm_organization_table, LiteLLM.LiteLLMOrganizationTable, foreign_key: :organization_id)
    belongs_to(:litellm_model_table, LiteLLM.LiteLLMModelTable, foreign_key: :model_id, type: :id)
    belongs_to(:object_permission, LiteLLM.LiteLLMObjectPermissionTable, foreign_key: :object_permission_id)
    has_many(:projects, LiteLLM.LiteLLMProjectTable, foreign_key: :team_id)
    timestamps(inserted_at_source: :created_at)
  end
end