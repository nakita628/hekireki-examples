use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "DashboardWidget")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub created_at: DateTime,
    pub updated_at: DateTime,
    #[sea_orm(column_name = "dashboardId")]
    pub dashboard_id: String,
    #[sea_orm(column_name = "chartId")]
    pub chart_id: String,
    #[sea_orm(default_value = "{\"x\":0,\"y\":0,\"w\":4,\"h\":3}")]
    pub layout: Json,
    #[sea_orm(default_value = 0)]
    pub order: i32,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::dashboard::Entity",
        from = "Column::DashboardId",
        to = "super::dashboard::Column::Id",
        on_delete = "Cascade"
    )]
    Dashboard,
    #[sea_orm(
        belongs_to = "super::chart::Entity",
        from = "Column::ChartId",
        to = "super::chart::Column::Id",
        on_delete = "Cascade"
    )]
    Chart,
}

impl Related<super::dashboard::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Dashboard.def()
    }
}

impl Related<super::chart::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Chart.def()
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
