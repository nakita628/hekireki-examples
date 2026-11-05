use super::chart_type::ChartType;
use super::interval::Interval;
use super::metric::Metric;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "reports")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_type = "Uuid")]
    pub id: String,
    pub name: String,
    pub interval: Interval,
    #[sea_orm(default_value = "30d")]
    pub range: String,
    #[sea_orm(column_name = "chartType")]
    pub chart_type: ChartType,
    #[sea_orm(column_name = "lineType", default_value = "monotone")]
    pub line_type: String,
    pub breakdowns: Json,
    pub events: Json,
    #[sea_orm(column_name = "globalFilters", default_value = "[]")]
    pub global_filters: Json,
    pub formula: Option<String>,
    pub unit: Option<String>,
    #[sea_orm(default_value = "sum")]
    pub metric: Metric,
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    #[sea_orm(default_value = false)]
    pub previous: bool,
    pub criteria: Option<String>,
    #[sea_orm(column_name = "funnelGroup")]
    pub funnel_group: Option<String>,
    #[sea_orm(column_name = "funnelWindow")]
    pub funnel_window: Option<f64>,
    pub options: Option<Json>,
    pub display: Option<Json>,
    #[sea_orm(column_name = "visibleSeries")]
    pub visible_series: Vec<String>,
    #[sea_orm(column_name = "startDate")]
    pub start_date: Option<String>,
    #[sea_orm(column_name = "endDate")]
    pub end_date: Option<String>,
    #[sea_orm(column_name = "dashboardId")]
    pub dashboard_id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::project::Entity",
        from = "Column::ProjectId",
        to = "super::project::Column::Id",
        on_delete = "Cascade"
    )]
    Project,
    #[sea_orm(
        belongs_to = "super::dashboard::Entity",
        from = "Column::DashboardId",
        to = "super::dashboard::Column::Id",
        on_delete = "Cascade"
    )]
    Dashboard,
    #[sea_orm(has_one = "super::report_layout::Entity")]
    Layout,
    #[sea_orm(has_one = "super::share_report::Entity")]
    Share,
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

impl Related<super::report_layout::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Layout.def()
    }
}

impl Related<super::share_report::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Share.def()
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
