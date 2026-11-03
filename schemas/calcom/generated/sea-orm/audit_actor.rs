use super::audit_actor_type::AuditActorType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "AuditActor")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_type = "Uuid")]
    pub id: String,
    pub r#type: AuditActorType,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "userUuid", column_type = "Uuid")]
    pub user_uuid: Option<String>,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "attendeeId")]
    pub attendee_id: Option<i32>,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "credentialId")]
    pub credential_id: Option<i32>,
    #[sea_orm(unique)]
    pub email: Option<String>,
    #[sea_orm(unique)]
    pub phone: Option<String>,
    pub name: Option<String>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::booking_audit::Entity")]
    BookingAudits,
}

impl Related<super::booking_audit::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::BookingAudits.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    fn new() -> Self {
        Self {
            id: Set(uuid::Uuid::now_v7().to_string()),
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
        Ok(self)
    }
}
