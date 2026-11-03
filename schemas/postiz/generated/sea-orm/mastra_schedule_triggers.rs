use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "mastra_schedule_triggers")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub schedule_id: String,
    pub run_id: Option<String>,
    pub scheduled_fire_at: i64,
    pub actual_fire_at: i64,
    pub outcome: String,
    pub error: Option<String>,
    pub trigger_kind: String,
    pub parent_trigger_id: Option<String>,
    pub metadata: Option<Json>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

impl ActiveModelBehavior for ActiveModel {}
