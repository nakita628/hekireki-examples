use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "computed_update_outbox")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub base_id: String,
    pub seed_table_id: String,
    pub seed_record_ids: Option<Json>,
    pub change_type: String,
    pub steps: Option<Json>,
    pub edges: Option<Json>,
    pub status: String,
    #[sea_orm(default_value = 0)]
    pub attempts: i32,
    #[sea_orm(default_value = 8)]
    pub max_attempts: i32,
    pub next_run_at: DateTime,
    pub locked_at: Option<DateTime>,
    pub locked_by: Option<String>,
    pub last_error: Option<String>,
    #[sea_orm(default_value = 0)]
    pub estimated_complexity: i32,
    pub plan_hash: String,
    pub dirty_stats: Option<Json>,
    pub run_id: String,
    pub origin_run_ids: Vec<String>,
    #[sea_orm(default_value = 0)]
    pub run_total_steps: i32,
    #[sea_orm(default_value = 0)]
    pub run_completed_steps_before: i32,
    pub affected_table_ids: Vec<String>,
    pub affected_field_ids: Vec<String>,
    pub sync_max_level: Option<i32>,
    pub source_changed_at: Option<DateTime>,
    #[sea_orm(default_value = 0)]
    pub stage_depth: i32,
    pub predecessor_task_id: Option<String>,
    pub created_at: DateTime,
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::computed_update_outbox_seed::Entity")]
    Seeds,
}

impl Related<super::computed_update_outbox_seed::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Seeds.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.next_run_at.is_not_set() {
            self.next_run_at = Set(now.naive_utc());
        }
        if insert && self.created_at.is_not_set() {
            self.created_at = Set(now.naive_utc());
        }
        if !self.updated_at.is_set() {
            self.updated_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
