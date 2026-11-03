use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Tag")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    pub name: String,
    #[sea_orm(column_name = "ownerId")]
    pub owner_id: i32,
    #[sea_orm(column_name = "archiveAsScreenshot")]
    pub archive_as_screenshot: Option<bool>,
    #[sea_orm(column_name = "archiveAsMonolith")]
    pub archive_as_monolith: Option<bool>,
    #[sea_orm(column_name = "archiveAsPDF")]
    pub archive_as_pdf: Option<bool>,
    #[sea_orm(column_name = "archiveAsReadable")]
    pub archive_as_readable: Option<bool>,
    #[sea_orm(column_name = "archiveAsWaybackMachine")]
    pub archive_as_wayback_machine: Option<bool>,
    #[sea_orm(column_name = "aiTag")]
    pub ai_tag: Option<bool>,
    #[sea_orm(column_name = "aiGenerated", default_value = false)]
    pub ai_generated: bool,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::OwnerId",
        to = "super::user::Column::Id",
        on_delete = "Cascade"
    )]
    Owner,
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Owner.def()
    }
}

impl Related<super::link::Entity> for Entity {
    fn to() -> RelationDef {
        super::link_to_tag::Relation::Link.def()
    }
    fn via() -> Option<RelationDef> {
        Some(super::link_to_tag::Relation::Tag.def().rev())
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
