use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "LiteLLM_PerformanceAlertState")]
pub struct Model {
    #[sea_orm(default_value = true)]
    pub enabled: bool,
    pub alerts_used: Option<i32>,
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: i32,
    #[sea_orm(column_type = "String(StringLen::N(32))")]
    pub owner: String,
    #[sea_orm(column_type = "TimestampWithTimeZone")]
    pub next_run_at: DateTimeWithTimeZone,
    #[sea_orm(column_type = "String(StringLen::N(10))")]
    pub day: Option<String>,
    #[sea_orm(column_type = "String(StringLen::N(16))")]
    pub scope: Option<String>,
    #[sea_orm(column_type = "String(StringLen::N(1024))", default_value = "")]
    pub resume_after: String,
    #[sea_orm(default_value = false)]
    pub wrapped: bool,
    #[sea_orm(default_value = 0)]
    pub checked: i32,
    #[sea_orm(default_value = 0)]
    pub skipped: i32,
    #[sea_orm(column_type = "String(StringLen::N(16))")]
    pub start_scope: Option<String>,
    #[sea_orm(column_type = "String(StringLen::N(1024))", default_value = "")]
    pub start_after: String,
    pub completed_at: Option<DateTime>,
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, _insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if !self.updated_at.is_set() {
            self.updated_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
