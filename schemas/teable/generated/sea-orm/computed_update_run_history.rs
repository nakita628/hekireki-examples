use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "computed_update_run_history")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub task_id: String,
    pub base_id: String,
    pub seed_table_id: String,
    pub change_type: String,
    pub run_id: String,
    pub origin_run_ids: Vec<String>,
    pub steps: Option<Json>,
    pub edges: Option<Json>,
    pub affected_table_ids: Vec<String>,
    pub affected_field_ids: Vec<String>,
    pub source_field_ids: Vec<String>,
    #[sea_orm(default_value = 0)]
    pub seed_record_count: i32,
    #[sea_orm(default_value = 0)]
    pub stage_depth: i32,
    pub predecessor_task_id: Option<String>,
    #[sea_orm(default_value = 0)]
    pub run_total_steps: i32,
    #[sea_orm(default_value = 0)]
    pub run_completed_steps_before: i32,
    pub sync_max_level: Option<i32>,
    #[sea_orm(default_value = 0)]
    pub estimated_complexity: i32,
    #[sea_orm(default_value = 0)]
    pub attempts: i32,
    pub outcome: String,
    pub source_changed_at: Option<DateTime>,
    pub enqueued_at: DateTime,
    pub started_at: Option<DateTime>,
    pub completed_at: DateTime,
    #[sea_orm(default_value = 0)]
    pub duration_ms: i32,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

impl ActiveModelBehavior for ActiveModel {}
