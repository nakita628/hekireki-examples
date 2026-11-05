use super::attachment_source_type::AttachmentSourceType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "AttachmentSource")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    pub name: String,
    pub r#type: AttachmentSourceType,
    #[sea_orm(column_name = "sourceId")]
    pub source_id: String,
    #[sea_orm(column_name = "sourcePath")]
    pub source_path: Option<String>,
    #[sea_orm(column_name = "ruleId")]
    pub rule_id: String,
    #[sea_orm(column_name = "driveConnectionId")]
    pub drive_connection_id: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::rule::Entity",
        from = "Column::RuleId",
        to = "super::rule::Column::Id",
        on_delete = "Cascade"
    )]
    Rule,
    #[sea_orm(
        belongs_to = "super::drive_connection::Entity",
        from = "Column::DriveConnectionId",
        to = "super::drive_connection::Column::Id",
        on_delete = "Cascade"
    )]
    DriveConnection,
    #[sea_orm(has_many = "super::attachment_document::Entity")]
    Documents,
}

impl Related<super::rule::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Rule.def()
    }
}

impl Related<super::drive_connection::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::DriveConnection.def()
    }
}

impl Related<super::attachment_document::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Documents.def()
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
