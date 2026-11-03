use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "OnlineUpgradeState")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub release: String,
    #[sea_orm(column_name = "targetImage")]
    pub target_image: String,
    pub phase: String,
    #[sea_orm(column_name = "completedPhases")]
    pub completed_phases: Vec<String>,
    #[sea_orm(column_name = "surveyCursor")]
    pub survey_cursor: Option<String>,
    #[sea_orm(column_name = "surveyCount", default_value = "0")]
    pub survey_count: i64,
    #[sea_orm(column_name = "baselineCompletedAt")]
    pub baseline_completed_at: Option<DateTime>,
    #[sea_orm(column_name = "forwardOnlyAt")]
    pub forward_only_at: Option<DateTime>,
    #[sea_orm(column_name = "queuePausedAt")]
    pub queue_paused_at: Option<DateTime>,
    #[sea_orm(column_name = "queuePauseDeadlineAt")]
    pub queue_pause_deadline_at: Option<DateTime>,
    #[sea_orm(column_name = "queueResumedAt")]
    pub queue_resumed_at: Option<DateTime>,
    #[sea_orm(column_name = "activatedAt")]
    pub activated_at: Option<DateTime>,
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
