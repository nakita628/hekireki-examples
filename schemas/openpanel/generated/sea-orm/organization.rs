use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "organizations")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub name: String,
    #[sea_orm(unique)]
    pub slug: Option<String>,
    #[sea_orm(column_name = "createdByUserId")]
    pub created_by_user_id: Option<String>,
    pub timezone: Option<String>,
    #[sea_orm(default_value = "completed")]
    pub onboarding: Option<String>,
    pub attribution: Option<Json>,
    #[sea_orm(column_name = "subscriptionId")]
    pub subscription_id: Option<String>,
    #[sea_orm(column_name = "subscriptionCustomerId")]
    pub subscription_customer_id: Option<String>,
    #[sea_orm(column_name = "subscriptionPriceId")]
    pub subscription_price_id: Option<String>,
    #[sea_orm(column_name = "subscriptionProductId")]
    pub subscription_product_id: Option<String>,
    #[sea_orm(column_name = "subscriptionStatus")]
    pub subscription_status: Option<String>,
    #[sea_orm(column_name = "subscriptionStartsAt")]
    pub subscription_starts_at: Option<DateTime>,
    #[sea_orm(column_name = "subscriptionEndsAt")]
    pub subscription_ends_at: Option<DateTime>,
    #[sea_orm(column_name = "subscriptionCanceledAt")]
    pub subscription_canceled_at: Option<DateTime>,
    #[sea_orm(column_name = "subscriptionCreatedByUserId")]
    pub subscription_created_by_user_id: Option<String>,
    #[sea_orm(column_name = "subscriptionPeriodEventsCount", default_value = 0)]
    pub subscription_period_events_count: i32,
    #[sea_orm(column_name = "subscriptionPeriodEventsCountExceededAt")]
    pub subscription_period_events_count_exceeded_at: Option<DateTime>,
    #[sea_orm(column_name = "subscriptionPeriodEventsLimit", default_value = 0)]
    pub subscription_period_events_limit: i32,
    #[sea_orm(column_name = "subscriptionInterval")]
    pub subscription_interval: Option<String>,
    #[sea_orm(column_name = "subscriptionCancelReason")]
    pub subscription_cancel_reason: Option<String>,
    #[sea_orm(column_name = "subscriptionCancelComment")]
    pub subscription_cancel_comment: Option<String>,
    #[sea_orm(column_name = "subscriptionSaveDiscountAppliedAt")]
    pub subscription_save_discount_applied_at: Option<DateTime>,
    #[sea_orm(column_name = "subscriptionPauseAtPeriodEnd", default_value = false)]
    pub subscription_pause_at_period_end: bool,
    #[sea_orm(column_name = "subscriptionResumesAt")]
    pub subscription_resumes_at: Option<DateTime>,
    #[sea_orm(column_name = "subscriptionFirstStartedAt")]
    pub subscription_first_started_at: Option<DateTime>,
    #[sea_orm(column_name = "usageWarningSentAt")]
    pub usage_warning_sent_at: Option<DateTime>,
    #[sea_orm(column_name = "usageExceededSentAt")]
    pub usage_exceeded_sent_at: Option<DateTime>,
    #[sea_orm(column_name = "subscriptionDiscount")]
    pub subscription_discount: Option<Json>,
    #[sea_orm(column_name = "windDownStartedAt")]
    pub wind_down_started_at: Option<DateTime>,
    #[sea_orm(column_name = "windDownStep")]
    pub wind_down_step: Option<String>,
    #[sea_orm(column_name = "deleteAt")]
    pub delete_at: Option<DateTime>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::CreatedByUserId",
        to = "super::user::Column::Id",
        on_delete = "SetNull"
    )]
    CreatedBy,
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::SubscriptionCreatedByUserId",
        to = "super::user::Column::Id"
    )]
    SubscriptionCreatedBy,
    #[sea_orm(has_many = "super::project::Entity")]
    Projects,
    #[sea_orm(has_many = "super::member::Entity")]
    Members,
    #[sea_orm(has_many = "super::client::Entity")]
    Client,
    #[sea_orm(has_many = "super::dashboard::Entity")]
    Dashboard,
    #[sea_orm(has_many = "super::share_overview::Entity")]
    ShareOverview,
    #[sea_orm(has_many = "super::share_dashboard::Entity")]
    ShareDashboard,
    #[sea_orm(has_many = "super::share_report::Entity")]
    ShareReport,
    #[sea_orm(has_many = "super::share_widget::Entity")]
    ShareWidget,
    #[sea_orm(has_many = "super::integration::Entity")]
    Integrations,
    #[sea_orm(has_many = "super::invite::Entity")]
    Invites,
    #[sea_orm(has_many = "super::conversation::Entity")]
    Conversations,
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CreatedBy.def()
    }
}

impl Related<super::project::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Projects.def()
    }
}

impl Related<super::member::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Members.def()
    }
}

impl Related<super::client::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Client.def()
    }
}

impl Related<super::dashboard::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Dashboard.def()
    }
}

impl Related<super::share_overview::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ShareOverview.def()
    }
}

impl Related<super::share_dashboard::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ShareDashboard.def()
    }
}

impl Related<super::share_report::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ShareReport.def()
    }
}

impl Related<super::share_widget::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ShareWidget.def()
    }
}

impl Related<super::integration::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Integrations.def()
    }
}

impl Related<super::invite::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Invites.def()
    }
}

impl Related<super::conversation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Conversations.def()
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
