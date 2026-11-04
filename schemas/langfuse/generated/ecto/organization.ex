defmodule Langfuse.Organization do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          cloud_config: map() | nil,
          metadata: map() | nil,
          cloud_billing_cycle_anchor: DateTime.t() | nil,
          cloud_billing_cycle_updated_at: DateTime.t() | nil,
          cloud_current_cycle_usage: integer() | nil,
          cloud_free_tier_usage_threshold_state: String.t() | nil,
          ai_features_enabled: boolean(),
          ai_telemetry_enabled: boolean(),
          access_audit_log_enabled: boolean(),
          feature_flag_org_defaults: [String.t()],
          sfdc_org_id: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          gateway_config: Langfuse.GatewayConfig.t() | nil,
          organization_memberships: [Langfuse.OrganizationMembership.t()],
          projects: [Langfuse.Project.t()],
          membership_invitation: [Langfuse.MembershipInvitation.t()],
          api_key: [Langfuse.ApiKey.t()],
          surveys: [Langfuse.Survey.t()],
          cloud_spend_alerts: [Langfuse.CloudSpendAlert.t()],
          verified_domains: [Langfuse.VerifiedDomain.t()],
          gateway_ai_connections: [Langfuse.GatewayAiConnection.t()],
          organization_llm_api_keys: [Langfuse.LlmApiKeys.t()],
          role_assignments: [Langfuse.RoleAssignment.t()]
        }

  schema "organizations" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:cloud_config, :map)
    field(:metadata, :map)
    field(:cloud_billing_cycle_anchor, Langfuse.PrismaDateTime, autogenerate: true)
    field(:cloud_billing_cycle_updated_at, Langfuse.PrismaDateTime)
    field(:cloud_current_cycle_usage, :integer)
    field(:cloud_free_tier_usage_threshold_state, :string)
    field(:ai_features_enabled, :boolean, default: false)
    field(:ai_telemetry_enabled, :boolean, default: true)
    field(:access_audit_log_enabled, :boolean, default: false)
    field(:feature_flag_org_defaults, {:array, :string})
    field(:sfdc_org_id, :string)
    has_one(:gateway_config, Langfuse.GatewayConfig, foreign_key: :organization_id, references: :id)
    has_many(:organization_memberships, Langfuse.OrganizationMembership, foreign_key: :org_id, references: :id)
    has_many(:projects, Langfuse.Project, foreign_key: :org_id, references: :id)
    has_many(:membership_invitation, Langfuse.MembershipInvitation, foreign_key: :org_id, references: :id)
    has_many(:api_key, Langfuse.ApiKey, foreign_key: :org_id, references: :id)
    has_many(:surveys, Langfuse.Survey, foreign_key: :org_id, references: :id)
    has_many(:cloud_spend_alerts, Langfuse.CloudSpendAlert, foreign_key: :org_id, references: :id)
    has_many(:verified_domains, Langfuse.VerifiedDomain, foreign_key: :organization_id, references: :id)
    has_many(:gateway_ai_connections, Langfuse.GatewayAiConnection, foreign_key: :organization_id, references: :id)
    has_many(:organization_llm_api_keys, Langfuse.LlmApiKeys, foreign_key: :organization_id, references: :id)
    has_many(:role_assignments, Langfuse.RoleAssignment, foreign_key: :org_id, references: :id)
    timestamps(inserted_at_source: :created_at)
  end
end