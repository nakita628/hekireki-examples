use super::monitor_severity::MonitorSeverity;
use super::monitor_status::MonitorStatus;
use super::monitor_threshold_operator::MonitorThresholdOperator;
use super::monitor_view::MonitorView;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "monitors")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub created_at: DateTime,
    pub updated_at: DateTime,
    pub created_by: Option<String>,
    pub updated_by: Option<String>,
    pub project_id: String,
    pub view: MonitorView,
    pub filters: Json,
    pub metric: Json,
    pub window_ms: i64,
    pub cadence_ms: i64,
    pub threshold_operator: MonitorThresholdOperator,
    pub alert_threshold: Decimal,
    pub warning_threshold: Option<Decimal>,
    #[sea_orm(default_value = "UNKNOWN")]
    pub severity: MonitorSeverity,
    pub severity_changed_at: Option<DateTime>,
    pub no_data: Json,
    pub renotify: Json,
    #[sea_orm(default_value = "ACTIVE")]
    pub status: MonitorStatus,
    pub scheduler_batch_id: i64,
    pub next_run_at: Option<DateTime>,
    pub last_published_at: Option<DateTime>,
    pub last_claimed_at: Option<DateTime>,
    pub last_completed_at: Option<DateTime>,
    pub name: String,
    pub tags: Vec<String>,
    pub trigger_ids: Vec<String>,
    pub alerted_at: Option<DateTime>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::CreatedBy",
        to = "super::user::Column::Id",
        on_delete = "SetNull"
    )]
    CreatedByUser,
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::UpdatedBy",
        to = "super::user::Column::Id",
        on_delete = "SetNull"
    )]
    UpdatedByUser,
    #[sea_orm(
        belongs_to = "super::project::Entity",
        from = "Column::ProjectId",
        to = "super::project::Column::Id",
        on_delete = "Cascade"
    )]
    Project,
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CreatedByUser.def()
    }
}

impl Related<super::project::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Project.def()
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
