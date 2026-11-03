use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "mastra_schedules")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub target: Json,
    pub cron: String,
    pub timezone: Option<String>,
    pub status: String,
    pub next_fire_at: i64,
    pub last_fire_at: Option<i64>,
    pub last_run_id: Option<String>,
    pub created_at: i64,
    pub updated_at: i64,
    pub metadata: Option<Json>,
    pub owner_type: Option<String>,
    pub owner_id: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

impl ActiveModelBehavior for ActiveModel {}
