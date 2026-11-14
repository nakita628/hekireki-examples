use super::currency::Currency;
use super::organization_intent::OrganizationIntent;
use super::organization_user_role::OrganizationUserRole;
use super::pricing_model::PricingModel;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Organization")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub name: String,
    #[sea_orm(default_value = "customer")]
    pub kind: String,
    #[sea_orm(column_name = "phoneNumber")]
    pub phone_number: Option<String>,
    #[sea_orm(unique)]
    pub slug: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "usageSpendingMaxLimit")]
    pub usage_spending_max_limit: Option<i32>,
    #[sea_orm(column_name = "datasetAttachmentMaxMb")]
    pub dataset_attachment_max_mb: Option<i32>,
    #[sea_orm(column_name = "privateClickHouseLabel")]
    pub private_click_house_label: Option<String>,
    #[sea_orm(column_name = "privateS3Label")]
    pub private_s3_label: Option<String>,
    #[sea_orm(column_name = "privateDataplaneSeenAt")]
    pub private_dataplane_seen_at: Option<DateTime>,
    #[sea_orm(column_name = "maxSessionDurationDays", default_value = 0)]
    pub max_session_duration_days: i32,
    #[sea_orm(column_name = "mfaRequired", default_value = false)]
    pub mfa_required: bool,
    #[sea_orm(column_name = "lockoutAfterFailedAttempts", default_value = 0)]
    pub lockout_after_failed_attempts: i32,
    #[sea_orm(column_name = "lockoutMinutes", default_value = 30)]
    pub lockout_minutes: i32,
    #[sea_orm(column_name = "sessionIdleTimeoutMinutes", default_value = 1440)]
    pub session_idle_timeout_minutes: i32,
    #[sea_orm(column_name = "sessionMaxLifetimeMinutes", default_value = 0)]
    pub session_max_lifetime_minutes: i32,
    #[sea_orm(column_name = "signupData")]
    pub signup_data: Option<Json>,
    #[sea_orm(column_name = "signedDPA", default_value = false)]
    pub signed_dpa: bool,
    #[sea_orm(column_name = "elasticsearchNodeUrl")]
    pub elasticsearch_node_url: Option<String>,
    #[sea_orm(column_name = "elasticsearchApiKey")]
    pub elasticsearch_api_key: Option<String>,
    #[sea_orm(column_name = "useCustomElasticsearch", default_value = false)]
    pub use_custom_elasticsearch: bool,
    #[sea_orm(column_name = "s3Endpoint")]
    pub s3_endpoint: Option<String>,
    #[sea_orm(column_name = "s3AccessKeyId")]
    pub s3_access_key_id: Option<String>,
    #[sea_orm(column_name = "s3SecretAccessKey")]
    pub s3_secret_access_key: Option<String>,
    #[sea_orm(column_name = "s3Bucket")]
    pub s3_bucket: Option<String>,
    #[sea_orm(column_name = "useCustomS3", default_value = false)]
    pub use_custom_s3: bool,
    #[sea_orm(column_name = "sentPlanLimitAlert")]
    pub sent_plan_limit_alert: Option<DateTime>,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "ssoDomain")]
    pub sso_domain: Option<String>,
    #[sea_orm(column_name = "ssoProvider")]
    pub sso_provider: Option<String>,
    #[sea_orm(column_name = "domainJoin", default_value = "request")]
    pub domain_join: String,
    #[sea_orm(column_name = "joinerRole", default_value = "MEMBER")]
    pub joiner_role: OrganizationUserRole,
    #[sea_orm(column_name = "joinDomains")]
    pub join_domains: Vec<String>,
    #[sea_orm(column_name = "presenceEnabled", default_value = true)]
    pub presence_enabled: bool,
    #[sea_orm(column_name = "traceSharingEnabled", default_value = true)]
    pub trace_sharing_enabled: bool,
    #[sea_orm(column_name = "supportContact")]
    pub support_contact: Option<String>,
    #[sea_orm(column_name = "primaryIntent")]
    pub primary_intent: Option<OrganizationIntent>,
    #[sea_orm(column_name = "promoCode")]
    pub promo_code: Option<String>,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "stripeCustomerId")]
    pub stripe_customer_id: Option<String>,
    #[sea_orm(default_value = "EUR")]
    pub currency: Currency,
    #[sea_orm(column_name = "pricingModel", default_value = "TIERED")]
    pub pricing_model: PricingModel,
    #[sea_orm(column_type = "Text")]
    pub license: Option<String>,
    #[sea_orm(column_name = "licenseExpiresAt")]
    pub license_expires_at: Option<DateTime>,
    #[sea_orm(column_name = "licenseLastValidatedAt")]
    pub license_last_validated_at: Option<DateTime>,
    #[sea_orm(column_name = "selfHostedCustomer", default_value = false)]
    pub self_hosted_customer: bool,
    #[sea_orm(column_name = "connectServicesDisabled")]
    pub connect_services_disabled: Vec<String>,
    #[sea_orm(column_name = "instantEvalsEnabledAt")]
    pub instant_evals_enabled_at: Option<DateTime>,
    #[sea_orm(column_name = "instantEvalsEnabledByUserId")]
    pub instant_evals_enabled_by_user_id: Option<String>,
    #[sea_orm(column_name = "connectLastSyncAt")]
    pub connect_last_sync_at: Option<DateTime>,
    #[sea_orm(column_name = "connectLastSyncError")]
    pub connect_last_sync_error: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::organization_user::Entity")]
    Members,
    #[sea_orm(has_many = "super::team::Entity")]
    Teams,
    #[sea_orm(has_many = "super::webhook_endpoint::Entity")]
    WebhookEndpoints,
    #[sea_orm(has_many = "super::organization_invite::Entity")]
    OrganizationInvite,
    #[sea_orm(has_many = "super::custom_role::Entity")]
    CustomRoles,
    #[sea_orm(has_many = "super::notification::Entity")]
    Notifications,
    #[sea_orm(has_many = "super::subscription::Entity")]
    Subscriptions,
    #[sea_orm(has_many = "super::scim_token::Entity")]
    ScimTokens,
    #[sea_orm(has_many = "super::scim_request_log::Entity")]
    ScimRequestLog,
    #[sea_orm(has_many = "super::prompt_tag::Entity")]
    PromptTags,
    #[sea_orm(has_many = "super::group::Entity")]
    Groups,
    #[sea_orm(has_many = "super::role_binding::Entity")]
    RoleBindings,
    #[sea_orm(has_many = "super::api_key::Entity")]
    ApiKeys,
    #[sea_orm(has_many = "super::issued_license::Entity")]
    IssuedLicenses,
    #[sea_orm(has_many = "super::gateway_change_event::Entity")]
    GatewayChangeEvents,
    #[sea_orm(has_many = "super::routing_policy::Entity")]
    RoutingPolicies,
    #[sea_orm(has_many = "super::ingestion_source::Entity")]
    IngestionSources,
    #[sea_orm(has_many = "super::anomaly_rule::Entity")]
    AnomalyRules,
    #[sea_orm(has_many = "super::anomaly_alert::Entity")]
    AnomalyAlerts,
    #[sea_orm(has_many = "super::ai_tool_entry::Entity")]
    AiToolEntries,
    #[sea_orm(has_many = "super::ingestion_template::Entity")]
    IngestionTemplates,
    #[sea_orm(has_many = "super::github_installation::Entity")]
    GithubInstallations,
    #[sea_orm(has_many = "super::github_pull_request::Entity")]
    GithubPullRequests,
    #[sea_orm(has_many = "super::github_branch_pull_request_check::Entity")]
    GithubBranchPrChecks,
    #[sea_orm(has_one = "super::connected_billing_account::Entity")]
    ConnectedBilling,
}

impl Related<super::organization_user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Members.def()
    }
}

impl Related<super::team::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Teams.def()
    }
}

impl Related<super::webhook_endpoint::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::WebhookEndpoints.def()
    }
}

impl Related<super::organization_invite::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OrganizationInvite.def()
    }
}

impl Related<super::custom_role::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CustomRoles.def()
    }
}

impl Related<super::notification::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Notifications.def()
    }
}

impl Related<super::subscription::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Subscriptions.def()
    }
}

impl Related<super::scim_token::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ScimTokens.def()
    }
}

impl Related<super::scim_request_log::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ScimRequestLog.def()
    }
}

impl Related<super::prompt_tag::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::PromptTags.def()
    }
}

impl Related<super::group::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Groups.def()
    }
}

impl Related<super::role_binding::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RoleBindings.def()
    }
}

impl Related<super::api_key::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ApiKeys.def()
    }
}

impl Related<super::issued_license::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::IssuedLicenses.def()
    }
}

impl Related<super::gateway_change_event::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::GatewayChangeEvents.def()
    }
}

impl Related<super::routing_policy::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RoutingPolicies.def()
    }
}

impl Related<super::ingestion_source::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::IngestionSources.def()
    }
}

impl Related<super::anomaly_rule::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AnomalyRules.def()
    }
}

impl Related<super::anomaly_alert::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AnomalyAlerts.def()
    }
}

impl Related<super::ai_tool_entry::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AiToolEntries.def()
    }
}

impl Related<super::ingestion_template::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::IngestionTemplates.def()
    }
}

impl Related<super::github_installation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::GithubInstallations.def()
    }
}

impl Related<super::github_pull_request::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::GithubPullRequests.def()
    }
}

impl Related<super::github_branch_pull_request_check::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::GithubBranchPrChecks.def()
    }
}

impl Related<super::connected_billing_account::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ConnectedBilling.def()
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
