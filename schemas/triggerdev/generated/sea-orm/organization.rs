use super::support_access_mode::SupportAccessMode;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Organization")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(unique)]
    pub slug: String,
    pub title: String,
    #[sea_orm(column_name = "maximumExecutionTimePerRunInMs", default_value = 900000)]
    pub maximum_execution_time_per_run_in_ms: i32,
    #[sea_orm(column_name = "maximumConcurrencyLimit", default_value = 10)]
    pub maximum_concurrency_limit: i32,
    #[sea_orm(column_name = "maximumSchedulesLimit", default_value = 5)]
    pub maximum_schedules_limit: i32,
    #[sea_orm(column_name = "maximumDevQueueSize")]
    pub maximum_dev_queue_size: Option<i32>,
    #[sea_orm(column_name = "maximumDeployedQueueSize")]
    pub maximum_deployed_queue_size: Option<i32>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "deletedAt")]
    pub deleted_at: Option<DateTime>,
    #[sea_orm(column_name = "companySize")]
    pub company_size: Option<String>,
    #[sea_orm(column_name = "onboardingData")]
    pub onboarding_data: Option<Json>,
    pub avatar: Option<Json>,
    #[sea_orm(column_name = "runsEnabled", default_value = true)]
    pub runs_enabled: bool,
    #[sea_orm(column_name = "v3Enabled", default_value = false)]
    pub is_activated: bool,
    #[sea_orm(column_name = "v2Enabled", default_value = false)]
    pub v2_enabled: bool,
    #[sea_orm(column_name = "v2MarqsEnabled", default_value = false)]
    pub v2_marqs_enabled: bool,
    #[sea_orm(column_name = "hasRequestedV3", default_value = false)]
    pub has_requested_v3: bool,
    #[sea_orm(column_name = "apiRateLimiterConfig")]
    pub api_rate_limiter_config: Option<Json>,
    #[sea_orm(column_name = "realtimeRateLimiterConfig")]
    pub realtime_rate_limiter_config: Option<Json>,
    #[sea_orm(column_name = "batchRateLimitConfig")]
    pub batch_rate_limit_config: Option<Json>,
    #[sea_orm(column_name = "batchQueueConcurrencyConfig")]
    pub batch_queue_concurrency_config: Option<Json>,
    #[sea_orm(column_name = "webhookLimitsConfig")]
    pub webhook_limits_config: Option<Json>,
    #[sea_orm(column_name = "featureFlags")]
    pub feature_flags: Option<Json>,
    #[sea_orm(column_name = "maximumProjectCount", default_value = 25)]
    pub maximum_project_count: i32,
    #[sea_orm(column_name = "maxSessionDuration")]
    pub max_session_duration: Option<i32>,
    #[sea_orm(column_name = "streamBasinName")]
    pub stream_basin_name: Option<String>,
    #[sea_orm(column_name = "supportAccessMode", default_value = "ALLOW")]
    pub support_access_mode: SupportAccessMode,
    #[sea_orm(column_name = "supportAccessModeUpdatedAt")]
    pub support_access_mode_updated_at: Option<DateTime>,
    #[sea_orm(column_name = "supportAccessModeUpdatedById")]
    pub support_access_mode_updated_by_id: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::runtime_environment::Entity")]
    Environments,
    #[sea_orm(has_many = "super::project::Entity")]
    Projects,
    #[sea_orm(has_many = "super::org_member::Entity")]
    Members,
    #[sea_orm(has_many = "super::org_member_invite::Entity")]
    Invites,
    #[sea_orm(has_many = "super::organization_integration::Entity")]
    OrganizationIntegrations,
    #[sea_orm(has_many = "super::organization_access_token::Entity")]
    OrganizationAccessTokens,
    #[sea_orm(has_many = "super::worker_instance_group::Entity")]
    WorkerGroups,
    #[sea_orm(has_many = "super::worker_instance::Entity")]
    WorkerInstances,
    #[sea_orm(has_many = "super::github_app_installation::Entity")]
    GithubAppInstallations,
    #[sea_orm(has_many = "super::customer_query::Entity")]
    CustomerQueries,
    #[sea_orm(has_many = "super::metrics_dashboard::Entity")]
    MetricsDashboards,
    #[sea_orm(has_many = "super::prompt::Entity")]
    Prompts,
    #[sea_orm(has_many = "super::platform_notification::Entity")]
    PlatformNotifications,
    #[sea_orm(has_many = "super::error_group_state::Entity")]
    ErrorGroupStates,
    #[sea_orm(has_many = "super::support_access_request::Entity")]
    SupportAccessRequests,
}

impl Related<super::runtime_environment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Environments.def()
    }
}

impl Related<super::project::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Projects.def()
    }
}

impl Related<super::org_member::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Members.def()
    }
}

impl Related<super::org_member_invite::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Invites.def()
    }
}

impl Related<super::organization_integration::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OrganizationIntegrations.def()
    }
}

impl Related<super::organization_access_token::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OrganizationAccessTokens.def()
    }
}

impl Related<super::worker_instance_group::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::WorkerGroups.def()
    }
}

impl Related<super::worker_instance::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::WorkerInstances.def()
    }
}

impl Related<super::github_app_installation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::GithubAppInstallations.def()
    }
}

impl Related<super::customer_query::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CustomerQueries.def()
    }
}

impl Related<super::metrics_dashboard::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::MetricsDashboards.def()
    }
}

impl Related<super::prompt::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Prompts.def()
    }
}

impl Related<super::platform_notification::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::PlatformNotifications.def()
    }
}

impl Related<super::error_group_state::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ErrorGroupStates.def()
    }
}

impl Related<super::support_access_request::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SupportAccessRequests.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.created_at.is_not_set() {
            self.created_at = Set(now.naive_utc());
        }
        if !self.updated_at.is_set() {
            self.updated_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
