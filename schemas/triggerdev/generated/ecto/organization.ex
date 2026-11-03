defmodule Trigger.Organization do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          slug: String.t(),
          title: String.t(),
          maximum_execution_time_per_run_in_ms: integer(),
          maximum_concurrency_limit: integer(),
          maximum_schedules_limit: integer(),
          maximum_dev_queue_size: integer() | nil,
          maximum_deployed_queue_size: integer() | nil,
          deleted_at: DateTime.t() | nil,
          company_size: String.t() | nil,
          onboarding_data: map() | nil,
          avatar: map() | nil,
          runs_enabled: boolean(),
          is_activated: boolean(),
          v2_enabled: boolean(),
          v2_marqs_enabled: boolean(),
          has_requested_v3: boolean(),
          api_rate_limiter_config: map() | nil,
          realtime_rate_limiter_config: map() | nil,
          batch_rate_limit_config: map() | nil,
          batch_queue_concurrency_config: map() | nil,
          webhook_limits_config: map() | nil,
          feature_flags: map() | nil,
          maximum_project_count: integer(),
          max_session_duration: integer() | nil,
          stream_basin_name: String.t() | nil,
          support_access_mode: atom(),
          support_access_mode_updated_at: DateTime.t() | nil,
          support_access_mode_updated_by_id: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          environments: [Trigger.RuntimeEnvironment.t()],
          projects: [Trigger.Project.t()],
          members: [Trigger.OrgMember.t()],
          invites: [Trigger.OrgMemberInvite.t()],
          organization_integrations: [Trigger.OrganizationIntegration.t()],
          organization_access_tokens: [Trigger.OrganizationAccessToken.t()],
          worker_groups: [Trigger.WorkerInstanceGroup.t()],
          worker_instances: [Trigger.WorkerInstance.t()],
          github_app_installations: [Trigger.GithubAppInstallation.t()],
          customer_queries: [Trigger.CustomerQuery.t()],
          metrics_dashboards: [Trigger.MetricsDashboard.t()],
          prompts: [Trigger.Prompt.t()],
          platform_notifications: [Trigger.PlatformNotification.t()],
          error_group_states: [Trigger.ErrorGroupState.t()],
          support_access_requests: [Trigger.SupportAccessRequest.t()]
        }

  schema "Organization" do
    field(:id, :string, primary_key: true)
    field(:slug, :string)
    field(:title, :string)
    field(:maximum_execution_time_per_run_in_ms, :integer, default: 900000, source: :maximumExecutionTimePerRunInMs)
    field(:maximum_concurrency_limit, :integer, default: 10, source: :maximumConcurrencyLimit)
    field(:maximum_schedules_limit, :integer, default: 5, source: :maximumSchedulesLimit)
    field(:maximum_dev_queue_size, :integer, source: :maximumDevQueueSize)
    field(:maximum_deployed_queue_size, :integer, source: :maximumDeployedQueueSize)
    field(:deleted_at, Trigger.PrismaDateTime, source: :deletedAt)
    field(:company_size, :string, source: :companySize)
    field(:onboarding_data, :map, source: :onboardingData)
    field(:avatar, :map)
    field(:runs_enabled, :boolean, default: true, source: :runsEnabled)
    field(:is_activated, :boolean, default: false, source: :v3Enabled)
    field(:v2_enabled, :boolean, default: false, source: :v2Enabled)
    field(:v2_marqs_enabled, :boolean, default: false, source: :v2MarqsEnabled)
    field(:has_requested_v3, :boolean, default: false, source: :hasRequestedV3)
    field(:api_rate_limiter_config, :map, source: :apiRateLimiterConfig)
    field(:realtime_rate_limiter_config, :map, source: :realtimeRateLimiterConfig)
    field(:batch_rate_limit_config, :map, source: :batchRateLimitConfig)
    field(:batch_queue_concurrency_config, :map, source: :batchQueueConcurrencyConfig)
    field(:webhook_limits_config, :map, source: :webhookLimitsConfig)
    field(:feature_flags, :map, source: :featureFlags)
    field(:maximum_project_count, :integer, default: 25, source: :maximumProjectCount)
    field(:max_session_duration, :integer, source: :maxSessionDuration)
    field(:stream_basin_name, :string, source: :streamBasinName)
    field(:support_access_mode, Ecto.Enum, values: [:ALLOW, :REQUIRES_REQUEST], default: :ALLOW, source: :supportAccessMode)
    field(:support_access_mode_updated_at, Trigger.PrismaDateTime, source: :supportAccessModeUpdatedAt)
    field(:support_access_mode_updated_by_id, :string, source: :supportAccessModeUpdatedById)
    has_many(:environments, Trigger.RuntimeEnvironment, foreign_key: :organization_id)
    has_many(:projects, Trigger.Project, foreign_key: :organization_id)
    has_many(:members, Trigger.OrgMember, foreign_key: :organization_id)
    has_many(:invites, Trigger.OrgMemberInvite, foreign_key: :organization_id)
    has_many(:organization_integrations, Trigger.OrganizationIntegration, foreign_key: :organization_id)
    has_many(:organization_access_tokens, Trigger.OrganizationAccessToken, foreign_key: :organization_id)
    has_many(:worker_groups, Trigger.WorkerInstanceGroup, foreign_key: :organization_id)
    has_many(:worker_instances, Trigger.WorkerInstance, foreign_key: :organization_id)
    has_many(:github_app_installations, Trigger.GithubAppInstallation, foreign_key: :organization_id)
    has_many(:customer_queries, Trigger.CustomerQuery, foreign_key: :organization_id)
    has_many(:metrics_dashboards, Trigger.MetricsDashboard, foreign_key: :organization_id)
    has_many(:prompts, Trigger.Prompt, foreign_key: :organization_id)
    has_many(:platform_notifications, Trigger.PlatformNotification, foreign_key: :organization_id)
    has_many(:error_group_states, Trigger.ErrorGroupState, foreign_key: :organization_id)
    has_many(:support_access_requests, Trigger.SupportAccessRequest, foreign_key: :organization_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end