use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "DestinationCalendar")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    pub integration: String,
    #[sea_orm(column_name = "externalId")]
    pub external_id: String,
    #[sea_orm(column_name = "primaryEmail")]
    pub primary_email: Option<String>,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "userId")]
    pub user_id: Option<i32>,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "eventTypeId")]
    pub event_type_id: Option<i32>,
    #[sea_orm(column_name = "credentialId")]
    pub credential_id: Option<i32>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: Option<DateTime>,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: Option<DateTime>,
    #[sea_orm(column_name = "delegationCredentialId")]
    pub delegation_credential_id: Option<String>,
    #[sea_orm(column_name = "customCalendarReminder")]
    pub custom_calendar_reminder: Option<i32>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::UserId",
        to = "super::user::Column::Id",
        on_delete = "Cascade"
    )]
    User,
    #[sea_orm(
        belongs_to = "super::event_type::Entity",
        from = "Column::EventTypeId",
        to = "super::event_type::Column::Id",
        on_delete = "Cascade"
    )]
    EventType,
    #[sea_orm(
        belongs_to = "super::credential::Entity",
        from = "Column::CredentialId",
        to = "super::credential::Column::Id",
        on_delete = "Cascade"
    )]
    Credential,
    #[sea_orm(
        belongs_to = "super::delegation_credential::Entity",
        from = "Column::DelegationCredentialId",
        to = "super::delegation_credential::Column::Id",
        on_delete = "Cascade"
    )]
    DelegationCredential,
    #[sea_orm(has_many = "super::booking::Entity")]
    Booking,
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::User.def()
    }
}

impl Related<super::event_type::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EventType.def()
    }
}

impl Related<super::credential::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Credential.def()
    }
}

impl Related<super::delegation_credential::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::DelegationCredential.def()
    }
}

impl Related<super::booking::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Booking.def()
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
            self.created_at = Set(Some(now.naive_utc()));
        }
        if !self.updated_at.is_set() {
            self.updated_at = Set(Some(now.naive_utc()));
        }
        Ok(self)
    }
}
