use super::card_verification_status::CardVerificationStatus;
use super::project_disabled_reason::ProjectDisabledReason;
use super::tracking_mode::TrackingMode;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "projects")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub name: String,
    #[sea_orm(unique)]
    pub public: String,
    #[sea_orm(unique)]
    pub secret: String,
    #[sea_orm(default_value = false)]
    pub disabled: bool,
    #[sea_orm(column_name = "disabledReason")]
    pub disabled_reason: Option<ProjectDisabledReason>,
    #[sea_orm(unique)]
    pub customer: Option<String>,
    #[sea_orm(unique)]
    pub subscription: Option<String>,
    #[sea_orm(column_name = "cardVerification")]
    pub card_verification: Option<CardVerificationStatus>,
    #[sea_orm(column_name = "cardVerificationIntent")]
    pub card_verification_intent: Option<String>,
    #[sea_orm(column_name = "cardVerificationAt")]
    pub card_verification_at: Option<DateTime>,
    #[sea_orm(column_name = "cardVerificationSession")]
    pub card_verification_session: Option<String>,
    #[sea_orm(column_name = "cardVerificationPaymentMethod")]
    pub card_verification_payment_method: Option<String>,
    #[sea_orm(column_name = "cardVerificationFingerprint")]
    pub card_verification_fingerprint: Option<String>,
    #[sea_orm(column_name = "cardVerificationAttempts", default_value = 0)]
    pub card_verification_attempts: i32,
    #[sea_orm(column_name = "billingLimitWorkflows")]
    pub billing_limit_workflows: Option<i32>,
    #[sea_orm(column_name = "billingLimitCampaigns")]
    pub billing_limit_campaigns: Option<i32>,
    #[sea_orm(column_name = "billingLimitTransactional")]
    pub billing_limit_transactional: Option<i32>,
    #[sea_orm(column_name = "billingLimitInbound")]
    pub billing_limit_inbound: Option<i32>,
    #[sea_orm(default_value = "ENABLED")]
    pub tracking: TrackingMode,
    #[sea_orm(default_value = "en")]
    pub language: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::membership::Entity")]
    Members,
    #[sea_orm(has_many = "super::contact::Entity")]
    Contacts,
    #[sea_orm(has_many = "super::template::Entity")]
    Templates,
    #[sea_orm(has_many = "super::segment::Entity")]
    Segments,
    #[sea_orm(has_many = "super::workflow::Entity")]
    Workflows,
    #[sea_orm(has_many = "super::campaign::Entity")]
    Campaigns,
    #[sea_orm(has_many = "super::email::Entity")]
    Emails,
    #[sea_orm(has_many = "super::event::Entity")]
    Events,
    #[sea_orm(has_many = "super::domain::Entity")]
    Domains,
    #[sea_orm(has_many = "super::api_request::Entity")]
    ApiRequests,
    #[sea_orm(has_many = "super::idempotency_key::Entity")]
    IdempotencyKeys,
    #[sea_orm(has_many = "super::throttle_claim::Entity")]
    ThrottleClaims,
}

impl Related<super::membership::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Members.def()
    }
}

impl Related<super::contact::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Contacts.def()
    }
}

impl Related<super::template::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Templates.def()
    }
}

impl Related<super::segment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Segments.def()
    }
}

impl Related<super::workflow::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Workflows.def()
    }
}

impl Related<super::campaign::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Campaigns.def()
    }
}

impl Related<super::email::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Emails.def()
    }
}

impl Related<super::event::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Events.def()
    }
}

impl Related<super::domain::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Domains.def()
    }
}

impl Related<super::api_request::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ApiRequests.def()
    }
}

impl Related<super::idempotency_key::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::IdempotencyKeys.def()
    }
}

impl Related<super::throttle_claim::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ThrottleClaims.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    fn new() -> Self {
        Self {
            id: Set(uuid::Uuid::new_v4().to_string()),
            ..ActiveModelTrait::default()
        }
    }

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
