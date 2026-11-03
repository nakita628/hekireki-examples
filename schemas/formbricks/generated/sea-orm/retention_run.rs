use super::retention_entity::RetentionEntity;
use super::retention_run_kind::RetentionRunKind;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "RetentionRun")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: String,
    pub entity: RetentionEntity,
    #[sea_orm(default_value = "sweep")]
    pub kind: RetentionRunKind,
    #[sea_orm(column_name = "startedAt")]
    pub started_at: DateTime,
    #[sea_orm(column_name = "finishedAt")]
    pub finished_at: Option<DateTime>,
    #[sea_orm(column_name = "notifiedCount", default_value = 0)]
    pub notified_count: i32,
    #[sea_orm(column_name = "archivedCount", default_value = 0)]
    pub archived_count: i32,
    #[sea_orm(column_name = "deletedCount", default_value = 0)]
    pub deleted_count: i32,
    #[sea_orm(column_name = "skippedCount", default_value = 0)]
    pub skipped_count: i32,
    #[sea_orm(column_name = "hasChanges", default_value = false)]
    pub has_changes: bool,
    #[sea_orm(column_name = "scanCursor")]
    pub scan_cursor: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::organization::Entity",
        from = "Column::OrganizationId",
        to = "super::organization::Column::Id",
        on_delete = "Cascade"
    )]
    Organization,
    #[sea_orm(has_many = "super::retention_run_item::Entity")]
    Items,
}

impl Related<super::organization::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Organization.def()
    }
}

impl Related<super::retention_run_item::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Items.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.started_at.is_not_set() {
            self.started_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
