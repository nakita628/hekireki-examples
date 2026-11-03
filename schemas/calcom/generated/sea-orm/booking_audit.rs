use super::booking_audit_action::BookingAuditAction;
use super::booking_audit_source::BookingAuditSource;
use super::booking_audit_type::BookingAuditType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "BookingAudit")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_type = "Uuid")]
    pub id: String,
    #[sea_orm(column_name = "bookingUid")]
    pub booking_uid: String,
    #[sea_orm(column_name = "actorId", column_type = "Uuid")]
    pub actor_id: String,
    pub r#type: BookingAuditType,
    pub action: BookingAuditAction,
    pub timestamp: DateTime,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    pub source: BookingAuditSource,
    #[sea_orm(column_name = "operationId")]
    pub operation_id: String,
    pub data: Option<Json>,
    pub context: Option<Json>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::audit_actor::Entity",
        from = "Column::ActorId",
        to = "super::audit_actor::Column::Id",
        on_delete = "Restrict"
    )]
    Actor,
}

impl Related<super::audit_actor::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Actor.def()
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
        if !self.updated_at.is_set() {
            self.updated_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
