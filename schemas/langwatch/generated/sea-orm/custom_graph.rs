use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "CustomGraph")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    pub name: String,
    pub graph: Json,
    pub filters: Option<Json>,
    #[sea_orm(default_value = "builder")]
    pub kind: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "dashboardId")]
    pub dashboard_id: Option<String>,
    #[sea_orm(column_name = "gridColumn", default_value = 0)]
    pub grid_column: i32,
    #[sea_orm(column_name = "gridRow", default_value = 0)]
    pub grid_row: i32,
    #[sea_orm(column_name = "colSpan", default_value = 1)]
    pub col_span: i32,
    #[sea_orm(column_name = "rowSpan", default_value = 1)]
    pub row_span: i32,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::project::Entity",
        from = "Column::ProjectId",
        to = "super::project::Column::Id"
    )]
    Project,
    #[sea_orm(
        belongs_to = "super::dashboard::Entity",
        from = "Column::DashboardId",
        to = "super::dashboard::Column::Id",
        on_delete = "Cascade"
    )]
    Dashboard,
    #[sea_orm(has_many = "super::trigger_sent::Entity")]
    TriggerSent,
    #[sea_orm(has_one = "super::trigger::Entity")]
    Trigger,
}

impl Related<super::project::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Project.def()
    }
}

impl Related<super::dashboard::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Dashboard.def()
    }
}

impl Related<super::trigger_sent::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TriggerSent.def()
    }
}

impl Related<super::trigger::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Trigger.def()
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
