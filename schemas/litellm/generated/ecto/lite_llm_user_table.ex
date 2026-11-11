defmodule LiteLLM.LiteLLMUserTable do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          user_id: String.t(),
          user_alias: String.t() | nil,
          team_id: String.t() | nil,
          sso_user_id: String.t() | nil,
          password: String.t() | nil,
          password_reset_required: boolean() | nil,
          last_breach_check_at: DateTime.t() | nil,
          teams: [String.t()],
          user_role: String.t() | nil,
          max_budget: float() | nil,
          spend: float(),
          user_email: String.t() | nil,
          models: [String.t()],
          metadata: map(),
          max_parallel_requests: integer() | nil,
          tpm_limit: integer() | nil,
          rpm_limit: integer() | nil,
          budget_duration: String.t() | nil,
          budget_reset_at: DateTime.t() | nil,
          allowed_cache_controls: [String.t()],
          policies: [String.t()],
          model_spend: map(),
          model_max_budget: map(),
          inserted_at: DateTime.t() | nil,
          updated_at: DateTime.t() | nil,
          litellm_organization_table: LiteLLM.LiteLLMOrganizationTable.t() | nil,
          object_permission: LiteLLM.LiteLLMObjectPermissionTable.t() | nil,
          verified_subjects: [LiteLLM.LiteLLMVerifiedSubject.t()],
          organization_memberships: [LiteLLM.LiteLLMOrganizationMembership.t()],
          invitations_created: [LiteLLM.LiteLLMInvitationLink.t()],
          invitations_updated: [LiteLLM.LiteLLMInvitationLink.t()],
          invitations_user: [LiteLLM.LiteLLMInvitationLink.t()]
        }

  schema "LiteLLM_UserTable" do
    field(:user_id, :string, primary_key: true)
    field(:user_alias, :string)
    field(:team_id, :string)
    field(:sso_user_id, :string)
    field(:password, :string)
    field(:password_reset_required, :boolean)
    field(:last_breach_check_at, LiteLLM.PrismaDateTime)
    field(:teams, {:array, :string})
    field(:user_role, :string)
    field(:max_budget, :float)
    field(:spend, :float, default: 0.0)
    field(:user_email, :string)
    field(:models, {:array, :string})
    field(:metadata, :map, default: %{})
    field(:max_parallel_requests, :integer)
    field(:tpm_limit, :integer)
    field(:rpm_limit, :integer)
    field(:budget_duration, :string)
    field(:budget_reset_at, LiteLLM.PrismaDateTime)
    field(:allowed_cache_controls, {:array, :string})
    field(:policies, {:array, :string})
    field(:model_spend, :map, default: %{})
    field(:model_max_budget, :map, default: %{})
    belongs_to(:litellm_organization_table, LiteLLM.LiteLLMOrganizationTable, foreign_key: :organization_id, type: :binary_id)
    belongs_to(:object_permission, LiteLLM.LiteLLMObjectPermissionTable, foreign_key: :object_permission_id, type: :binary_id)
    has_many(:verified_subjects, LiteLLM.LiteLLMVerifiedSubject, foreign_key: :user_id, references: :user_id)
    has_many(:organization_memberships, LiteLLM.LiteLLMOrganizationMembership, foreign_key: :user_id, references: :user_id)
    has_many(:invitations_created, LiteLLM.LiteLLMInvitationLink, foreign_key: :created_by, references: :user_id)
    has_many(:invitations_updated, LiteLLM.LiteLLMInvitationLink, foreign_key: :updated_by, references: :user_id)
    has_many(:invitations_user, LiteLLM.LiteLLMInvitationLink, foreign_key: :user_id, references: :user_id)
    timestamps(inserted_at_source: :created_at)
  end
end