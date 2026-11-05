use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "DigestItem")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "messageId")]
    pub message_id: String,
    #[sea_orm(column_name = "threadId")]
    pub thread_id: String,
    #[sea_orm(column_type = "Text")]
    pub content: String,
    #[sea_orm(column_name = "digestId")]
    pub digest_id: String,
    #[sea_orm(column_name = "actionId")]
    pub action_id: Option<String>,
    #[sea_orm(column_name = "coldEmailId")]
    pub cold_email_id: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::digest::Entity",
        from = "Column::DigestId",
        to = "super::digest::Column::Id",
        on_delete = "Cascade"
    )]
    Digest,
    #[sea_orm(
        belongs_to = "super::executed_action::Entity",
        from = "Column::ActionId",
        to = "super::executed_action::Column::Id",
        on_delete = "Cascade"
    )]
    Action,
    #[sea_orm(
        belongs_to = "super::cold_email::Entity",
        from = "Column::ColdEmailId",
        to = "super::cold_email::Column::Id"
    )]
    ColdEmail,
}

impl Related<super::digest::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Digest.def()
    }
}

impl Related<super::executed_action::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Action.def()
    }
}

impl Related<super::cold_email::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ColdEmail.def()
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
