defmodule LiteLLM.LiteLLMDeletedTeamTable do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          team_id: String.t(),
          team_alias: String.t() | nil,
          organization_id: String.t() | nil,
          object_permission_id: String.t() | nil,
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
          model_id: integer() | nil,
          allow_team_guardrail_config: boolean(),
          created_at: DateTime.t() | nil,
          updated_at: DateTime.t() | nil,
          deleted_at: DateTime.t(),
          deleted_by: String.t() | nil,
          deleted_by_api_key: String.t() | nil,
          litellm_changed_by: String.t() | nil
        }

  schema "LiteLLM_DeletedTeamTable" do
    field(:team_id, :string)
    field(:team_alias, :string)
    field(:organization_id, :string)
    field(:object_permission_id, :string)
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
    field(:model_id, :integer)
    field(:allow_team_guardrail_config, :boolean, default: false)
    field(:created_at, LiteLLM.PrismaDateTime)
    field(:updated_at, LiteLLM.PrismaDateTime)
    field(:deleted_at, LiteLLM.PrismaDateTime, autogenerate: true)
    field(:deleted_by, :string)
    field(:deleted_by_api_key, :string)
    field(:litellm_changed_by, :string)
  end
end