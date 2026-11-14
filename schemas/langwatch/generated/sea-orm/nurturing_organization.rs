use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "NurturingOrganization")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: String,
    #[sea_orm(column_name = "adminUserId")]
    pub admin_user_id: Option<String>,
    #[sea_orm(default_value = false)]
    pub seeded: bool,
    #[sea_orm(column_name = "evaluationCount", default_value = 0)]
    pub evaluation_count: i32,
    #[sea_orm(column_name = "simulationRunCount", default_value = 0)]
    pub simulation_run_count: i32,
    #[sea_orm(column_name = "codingFirstSessionAt")]
    pub coding_first_session_at: Option<DateTime>,
    #[sea_orm(column_name = "codingAgentTypes")]
    pub coding_agent_types: Vec<String>,
    #[sea_orm(column_name = "codingActiveDays", default_value = 0)]
    pub coding_active_days: i32,
    #[sea_orm(column_name = "codingLastActiveDay")]
    pub coding_last_active_day: Option<String>,
    #[sea_orm(column_name = "codingTerminalTypes")]
    pub coding_terminal_types: Vec<String>,
    #[sea_orm(column_name = "codingRepositoryKeys")]
    pub coding_repository_keys: Vec<String>,
    #[sea_orm(column_name = "codingFirstDays")]
    pub coding_first_days: Vec<i32>,
    #[sea_orm(column_name = "lastTraceAt")]
    pub last_trace_at: Option<DateTime>,
    #[sea_orm(column_name = "updatedAt")]
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
