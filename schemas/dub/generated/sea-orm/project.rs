use super::plan_period::PlanPeriod;
use super::stripe_connect_mode::StripeConnectMode;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Project")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub name: String,
    #[sea_orm(unique)]
    pub slug: String,
    pub logo: Option<String>,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "inviteCode")]
    pub invite_code: Option<String>,
    #[sea_orm(default_value = "free")]
    pub plan: String,
    #[sea_orm(column_name = "planTier", default_value = 1)]
    pub plan_tier: i32,
    #[sea_orm(column_name = "planPeriod")]
    pub plan_period: Option<PlanPeriod>,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "stripeId")]
    pub stripe_id: Option<String>,
    #[sea_orm(column_name = "billingCycleStart")]
    pub billing_cycle_start: i32,
    #[sea_orm(column_name = "billingCycleEndsAt")]
    pub billing_cycle_ends_at: Option<DateTime>,
    #[sea_orm(column_name = "trialEndsAt")]
    pub trial_ends_at: Option<DateTime>,
    #[sea_orm(column_name = "subscriptionCanceledAt")]
    pub subscription_canceled_at: Option<DateTime>,
    #[sea_orm(column_name = "paymentFailedAt")]
    pub payment_failed_at: Option<DateTime>,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "invoicePrefix")]
    pub invoice_prefix: Option<String>,
    #[sea_orm(column_name = "stripeConnectId")]
    pub stripe_connect_id: Option<String>,
    #[sea_orm(column_name = "stripeConnectMode")]
    pub stripe_connect_mode: Option<StripeConnectMode>,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "shopifyStoreId")]
    pub shopify_store_id: Option<String>,
    #[sea_orm(column_name = "totalLinks", default_value = 0)]
    pub total_links: i32,
    #[sea_orm(column_name = "totalClicks", default_value = 0)]
    pub total_clicks: i32,
    #[sea_orm(default_value = 0)]
    pub usage: i32,
    #[sea_orm(column_name = "usageLimit", default_value = 1000)]
    pub usage_limit: i32,
    #[sea_orm(column_name = "linksUsage", default_value = 0)]
    pub links_usage: i32,
    #[sea_orm(column_name = "linksLimit", default_value = 25)]
    pub links_limit: i32,
    #[sea_orm(column_name = "domainsLimit", default_value = 3)]
    pub domains_limit: i32,
    #[sea_orm(column_name = "tagsLimit", default_value = 5)]
    pub tags_limit: i32,
    #[sea_orm(column_name = "foldersUsage", default_value = 0)]
    pub folders_usage: i32,
    #[sea_orm(column_name = "foldersLimit", default_value = 0)]
    pub folders_limit: i32,
    #[sea_orm(column_name = "usersLimit", default_value = 1)]
    pub users_limit: i32,
    #[sea_orm(column_name = "aiUsage", default_value = 0)]
    pub ai_usage: i32,
    #[sea_orm(column_name = "aiLimit", default_value = 10)]
    pub ai_limit: i32,
    pub store: Option<Json>,
    #[sea_orm(column_name = "siteVisitTrackingSettings")]
    pub site_visit_tracking_settings: Option<Json>,
    #[sea_orm(column_name = "allowedHostnames")]
    pub allowed_hostnames: Option<Json>,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "publishableKey")]
    pub publishable_key: Option<String>,
    #[sea_orm(column_name = "conversionEnabled", default_value = false)]
    pub conversion_enabled: bool,
    #[sea_orm(column_name = "webhookEnabled", default_value = false)]
    pub webhook_enabled: bool,
    #[sea_orm(column_name = "dotLinkClaimed", default_value = false)]
    pub dot_link_claimed: bool,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "ssoEmailDomain")]
    pub sso_email_domain: Option<String>,
    #[sea_orm(column_name = "ssoEnforcedAt")]
    pub sso_enforced_at: Option<DateTime>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "usageLastChecked")]
    pub usage_last_checked: DateTime,
    #[sea_orm(column_name = "disabledAt")]
    pub disabled_at: Option<DateTime>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::project_users::Entity")]
    Users,
    #[sea_orm(has_many = "super::project_invite::Entity")]
    Invites,
    #[sea_orm(has_many = "super::sent_email::Entity")]
    SentEmails,
    #[sea_orm(has_many = "super::link::Entity")]
    Links,
    #[sea_orm(has_many = "super::domain::Entity")]
    Domains,
    #[sea_orm(has_many = "super::tag::Entity")]
    Tags,
    #[sea_orm(has_many = "super::invoice::Entity")]
    Invoices,
    #[sea_orm(has_many = "super::customer::Entity")]
    Customers,
    #[sea_orm(has_many = "super::default_domains::Entity")]
    DefaultDomains,
    #[sea_orm(has_many = "super::restricted_token::Entity")]
    RestrictedTokens,
    #[sea_orm(has_many = "super::oauth_code::Entity")]
    OAuthCodes,
    #[sea_orm(has_many = "super::integration::Entity")]
    Integrations,
    #[sea_orm(has_many = "super::installed_integration::Entity")]
    InstalledIntegrations,
    #[sea_orm(has_many = "super::webhook::Entity")]
    Webhooks,
    #[sea_orm(has_many = "super::folder::Entity")]
    Folders,
    #[sea_orm(has_many = "super::registered_domain::Entity")]
    RegisteredDomains,
    #[sea_orm(has_many = "super::dashboard::Entity")]
    Dashboards,
    #[sea_orm(has_many = "super::utm_template::Entity")]
    UtmTemplates,
    #[sea_orm(has_many = "super::year_in_review::Entity")]
    YearInReviews,
}

impl Related<super::project_users::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Users.def()
    }
}

impl Related<super::project_invite::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Invites.def()
    }
}

impl Related<super::sent_email::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SentEmails.def()
    }
}

impl Related<super::link::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Links.def()
    }
}

impl Related<super::domain::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Domains.def()
    }
}

impl Related<super::tag::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Tags.def()
    }
}

impl Related<super::invoice::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Invoices.def()
    }
}

impl Related<super::customer::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Customers.def()
    }
}

impl Related<super::default_domains::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::DefaultDomains.def()
    }
}

impl Related<super::restricted_token::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RestrictedTokens.def()
    }
}

impl Related<super::oauth_code::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OAuthCodes.def()
    }
}

impl Related<super::integration::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Integrations.def()
    }
}

impl Related<super::installed_integration::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::InstalledIntegrations.def()
    }
}

impl Related<super::webhook::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Webhooks.def()
    }
}

impl Related<super::folder::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Folders.def()
    }
}

impl Related<super::registered_domain::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RegisteredDomains.def()
    }
}

impl Related<super::dashboard::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Dashboards.def()
    }
}

impl Related<super::utm_template::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::UtmTemplates.def()
    }
}

impl Related<super::year_in_review::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::YearInReviews.def()
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
        if insert && self.usage_last_checked.is_not_set() {
            self.usage_last_checked = Set(now.naive_utc());
        }
        Ok(self)
    }
}
