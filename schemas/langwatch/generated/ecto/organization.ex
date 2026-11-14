defmodule LangWatch.Organization do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          kind: String.t(),
          phone_number: String.t() | nil,
          slug: String.t(),
          usage_spending_max_limit: integer() | nil,
          dataset_attachment_max_mb: integer() | nil,
          private_click_house_label: String.t() | nil,
          private_s3_label: String.t() | nil,
          private_dataplane_seen_at: DateTime.t() | nil,
          max_session_duration_days: integer(),
          mfa_required: boolean(),
          lockout_after_failed_attempts: integer(),
          lockout_minutes: integer(),
          session_idle_timeout_minutes: integer(),
          session_max_lifetime_minutes: integer(),
          signup_data: map() | nil,
          signed_dpa: boolean(),
          elasticsearch_node_url: String.t() | nil,
          elasticsearch_api_key: String.t() | nil,
          use_custom_elasticsearch: boolean(),
          s3_endpoint: String.t() | nil,
          s3_access_key_id: String.t() | nil,
          s3_secret_access_key: String.t() | nil,
          s3_bucket: String.t() | nil,
          use_custom_s3: boolean(),
          sent_plan_limit_alert: DateTime.t() | nil,
          sso_domain: String.t() | nil,
          sso_provider: String.t() | nil,
          domain_join: String.t(),
          joiner_role: atom(),
          join_domains: [String.t()],
          presence_enabled: boolean(),
          trace_sharing_enabled: boolean(),
          support_contact: String.t() | nil,
          primary_intent: atom() | nil,
          promo_code: String.t() | nil,
          stripe_customer_id: String.t() | nil,
          currency: atom(),
          pricing_model: atom(),
          license: String.t() | nil,
          license_expires_at: DateTime.t() | nil,
          license_last_validated_at: DateTime.t() | nil,
          self_hosted_customer: boolean(),
          connect_services_disabled: [String.t()],
          instant_evals_enabled_at: DateTime.t() | nil,
          instant_evals_enabled_by_user_id: String.t() | nil,
          connect_last_sync_at: DateTime.t() | nil,
          connect_last_sync_error: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          connected_billing: LangWatch.ConnectedBillingAccount.t() | nil,
          members: [LangWatch.OrganizationUser.t()],
          teams: [LangWatch.Team.t()],
          webhook_endpoints: [LangWatch.WebhookEndpoint.t()],
          organization_invite: [LangWatch.OrganizationInvite.t()],
          custom_roles: [LangWatch.CustomRole.t()],
          notifications: [LangWatch.Notification.t()],
          subscriptions: [LangWatch.Subscription.t()],
          scim_tokens: [LangWatch.ScimToken.t()],
          scim_request_log: [LangWatch.ScimRequestLog.t()],
          prompt_tags: [LangWatch.PromptTag.t()],
          groups: [LangWatch.Group.t()],
          role_bindings: [LangWatch.RoleBinding.t()],
          api_keys: [LangWatch.ApiKey.t()],
          issued_licenses: [LangWatch.IssuedLicense.t()],
          gateway_change_events: [LangWatch.GatewayChangeEvent.t()],
          routing_policies: [LangWatch.RoutingPolicy.t()],
          ingestion_sources: [LangWatch.IngestionSource.t()],
          anomaly_rules: [LangWatch.AnomalyRule.t()],
          anomaly_alerts: [LangWatch.AnomalyAlert.t()],
          ai_tool_entries: [LangWatch.AiToolEntry.t()],
          ingestion_templates: [LangWatch.IngestionTemplate.t()],
          github_installations: [LangWatch.GithubInstallation.t()],
          github_pull_requests: [LangWatch.GithubPullRequest.t()],
          github_branch_pr_checks: [LangWatch.GithubBranchPullRequestCheck.t()]
        }

  schema "Organization" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:kind, :string, default: "customer")
    field(:phone_number, :string, source: :phoneNumber)
    field(:slug, :string)
    field(:usage_spending_max_limit, :integer, source: :usageSpendingMaxLimit)
    field(:dataset_attachment_max_mb, :integer, source: :datasetAttachmentMaxMb)
    field(:private_click_house_label, :string, source: :privateClickHouseLabel)
    field(:private_s3_label, :string, source: :privateS3Label)
    field(:private_dataplane_seen_at, LangWatch.PrismaDateTime, source: :privateDataplaneSeenAt)
    field(:max_session_duration_days, :integer, default: 0, source: :maxSessionDurationDays)
    field(:mfa_required, :boolean, default: false, source: :mfaRequired)
    field(:lockout_after_failed_attempts, :integer, default: 0, source: :lockoutAfterFailedAttempts)
    field(:lockout_minutes, :integer, default: 30, source: :lockoutMinutes)
    field(:session_idle_timeout_minutes, :integer, default: 1440, source: :sessionIdleTimeoutMinutes)
    field(:session_max_lifetime_minutes, :integer, default: 0, source: :sessionMaxLifetimeMinutes)
    field(:signup_data, :map, source: :signupData)
    field(:signed_dpa, :boolean, default: false, source: :signedDPA)
    field(:elasticsearch_node_url, :string, source: :elasticsearchNodeUrl)
    field(:elasticsearch_api_key, :string, source: :elasticsearchApiKey)
    field(:use_custom_elasticsearch, :boolean, default: false, source: :useCustomElasticsearch)
    field(:s3_endpoint, :string, source: :s3Endpoint)
    field(:s3_access_key_id, :string, source: :s3AccessKeyId)
    field(:s3_secret_access_key, :string, source: :s3SecretAccessKey)
    field(:s3_bucket, :string, source: :s3Bucket)
    field(:use_custom_s3, :boolean, default: false, source: :useCustomS3)
    field(:sent_plan_limit_alert, LangWatch.PrismaDateTime, source: :sentPlanLimitAlert)
    field(:sso_domain, :string, source: :ssoDomain)
    field(:sso_provider, :string, source: :ssoProvider)
    field(:domain_join, :string, default: "request", source: :domainJoin)
    field(:joiner_role, Ecto.Enum, values: [:ADMIN, :MEMBER, :EXTERNAL, :DEVELOPER], default: :MEMBER, source: :joinerRole)
    field(:join_domains, {:array, :string}, source: :joinDomains)
    field(:presence_enabled, :boolean, default: true, source: :presenceEnabled)
    field(:trace_sharing_enabled, :boolean, default: true, source: :traceSharingEnabled)
    field(:support_contact, :string, source: :supportContact)
    field(:primary_intent, Ecto.Enum, values: [:AGENT_GOVERNANCE, :LLM_OPS], source: :primaryIntent)
    field(:promo_code, :string, source: :promoCode)
    field(:stripe_customer_id, :string, source: :stripeCustomerId)
    field(:currency, Ecto.Enum, values: [:USD, :EUR], default: :EUR)
    field(:pricing_model, Ecto.Enum, values: [:TIERED, :SEAT_EVENT], default: :TIERED, source: :pricingModel)
    field(:license, :string)
    field(:license_expires_at, LangWatch.PrismaDateTime, source: :licenseExpiresAt)
    field(:license_last_validated_at, LangWatch.PrismaDateTime, source: :licenseLastValidatedAt)
    field(:self_hosted_customer, :boolean, default: false, source: :selfHostedCustomer)
    field(:connect_services_disabled, {:array, :string}, source: :connectServicesDisabled)
    field(:instant_evals_enabled_at, LangWatch.PrismaDateTime, source: :instantEvalsEnabledAt)
    field(:instant_evals_enabled_by_user_id, :string, source: :instantEvalsEnabledByUserId)
    field(:connect_last_sync_at, LangWatch.PrismaDateTime, source: :connectLastSyncAt)
    field(:connect_last_sync_error, :string, source: :connectLastSyncError)
    has_one(:connected_billing, LangWatch.ConnectedBillingAccount, foreign_key: :organization_id, references: :id)
    has_many(:members, LangWatch.OrganizationUser, foreign_key: :organization_id, references: :id)
    has_many(:teams, LangWatch.Team, foreign_key: :organization_id, references: :id)
    has_many(:webhook_endpoints, LangWatch.WebhookEndpoint, foreign_key: :organization_id, references: :id)
    has_many(:organization_invite, LangWatch.OrganizationInvite, foreign_key: :organization_id, references: :id)
    has_many(:custom_roles, LangWatch.CustomRole, foreign_key: :organization_id, references: :id)
    has_many(:notifications, LangWatch.Notification, foreign_key: :organization_id, references: :id)
    has_many(:subscriptions, LangWatch.Subscription, foreign_key: :organization_id, references: :id)
    has_many(:scim_tokens, LangWatch.ScimToken, foreign_key: :organization_id, references: :id)
    has_many(:scim_request_log, LangWatch.ScimRequestLog, foreign_key: :organization_id, references: :id)
    has_many(:prompt_tags, LangWatch.PromptTag, foreign_key: :organization_id, references: :id)
    has_many(:groups, LangWatch.Group, foreign_key: :organization_id, references: :id)
    has_many(:role_bindings, LangWatch.RoleBinding, foreign_key: :organization_id, references: :id)
    has_many(:api_keys, LangWatch.ApiKey, foreign_key: :organization_id, references: :id)
    has_many(:issued_licenses, LangWatch.IssuedLicense, foreign_key: :organization_id, references: :id)
    has_many(:gateway_change_events, LangWatch.GatewayChangeEvent, foreign_key: :organization_id, references: :id)
    has_many(:routing_policies, LangWatch.RoutingPolicy, foreign_key: :organization_id, references: :id)
    has_many(:ingestion_sources, LangWatch.IngestionSource, foreign_key: :organization_id, references: :id)
    has_many(:anomaly_rules, LangWatch.AnomalyRule, foreign_key: :organization_id, references: :id)
    has_many(:anomaly_alerts, LangWatch.AnomalyAlert, foreign_key: :organization_id, references: :id)
    has_many(:ai_tool_entries, LangWatch.AiToolEntry, foreign_key: :organization_id, references: :id)
    has_many(:ingestion_templates, LangWatch.IngestionTemplate, foreign_key: :organization_id, references: :id)
    has_many(:github_installations, LangWatch.GithubInstallation, foreign_key: :organization_id, references: :id)
    has_many(:github_pull_requests, LangWatch.GithubPullRequest, foreign_key: :organization_id, references: :id)
    has_many(:github_branch_pr_checks, LangWatch.GithubBranchPullRequestCheck, foreign_key: :organization_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end