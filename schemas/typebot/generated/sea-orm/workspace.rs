use super::plan::Plan;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Workspace")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    pub name: String,
    pub icon: Option<String>,
    #[sea_orm(default_value = "FREE")]
    pub plan: Plan,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "stripeId")]
    pub stripe_id: Option<String>,
    #[sea_orm(column_name = "additionalChatsIndex", default_value = 0)]
    pub additional_chats_index: i32,
    #[sea_orm(column_name = "additionalStorageIndex", default_value = 0)]
    pub additional_storage_index: i32,
    #[sea_orm(column_name = "chatsLimitFirstEmailSentAt")]
    pub chats_limit_first_email_sent_at: Option<DateTime>,
    #[sea_orm(column_name = "storageLimitFirstEmailSentAt")]
    pub storage_limit_first_email_sent_at: Option<DateTime>,
    #[sea_orm(column_name = "chatsLimitSecondEmailSentAt")]
    pub chats_limit_second_email_sent_at: Option<DateTime>,
    #[sea_orm(column_name = "storageLimitSecondEmailSentAt")]
    pub storage_limit_second_email_sent_at: Option<DateTime>,
    #[sea_orm(column_name = "customChatsLimit")]
    pub custom_chats_limit: Option<i32>,
    #[sea_orm(column_name = "customStorageLimit")]
    pub custom_storage_limit: Option<i32>,
    #[sea_orm(column_name = "customSeatsLimit")]
    pub custom_seats_limit: Option<i32>,
    #[sea_orm(column_name = "isQuarantined", default_value = false)]
    pub is_quarantined: bool,
    #[sea_orm(column_name = "isSuspended", default_value = false)]
    pub is_suspended: bool,
    #[sea_orm(column_name = "isPastDue", default_value = false)]
    pub is_past_due: bool,
    #[sea_orm(column_name = "isVerified")]
    pub is_verified: Option<bool>,
    #[sea_orm(column_name = "chatsHardLimit")]
    pub chats_hard_limit: Option<i32>,
    pub settings: Option<Json>,
    #[sea_orm(column_name = "lastActivityAt")]
    pub last_activity_at: Option<DateTime>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::credentials::Entity")]
    Credentials,
    #[sea_orm(has_many = "super::custom_domain::Entity")]
    CustomDomains,
    #[sea_orm(has_many = "super::dashboard_folder::Entity")]
    Folders,
    #[sea_orm(has_many = "super::member_in_workspace::Entity")]
    Members,
    #[sea_orm(has_many = "super::typebot::Entity")]
    Typebots,
    #[sea_orm(has_many = "super::workspace_invitation::Entity")]
    Invitations,
    #[sea_orm(has_many = "super::theme_template::Entity")]
    ThemeTemplates,
    #[sea_orm(has_many = "super::space::Entity")]
    Spaces,
    #[sea_orm(has_one = "super::claimable_custom_plan::Entity")]
    ClaimableCustomPlan,
}

impl Related<super::credentials::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Credentials.def()
    }
}

impl Related<super::custom_domain::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CustomDomains.def()
    }
}

impl Related<super::dashboard_folder::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Folders.def()
    }
}

impl Related<super::member_in_workspace::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Members.def()
    }
}

impl Related<super::typebot::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Typebots.def()
    }
}

impl Related<super::workspace_invitation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Invitations.def()
    }
}

impl Related<super::theme_template::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ThemeTemplates.def()
    }
}

impl Related<super::space::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Spaces.def()
    }
}

impl Related<super::claimable_custom_plan::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ClaimableCustomPlan.def()
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
