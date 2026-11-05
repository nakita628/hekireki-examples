use super::automation_job_run_status::AutomationJobRunStatus;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "AutomationJobRun")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(default_value = "PENDING")]
    pub status: AutomationJobRunStatus,
    #[sea_orm(column_name = "scheduledFor")]
    pub scheduled_for: DateTime,
    #[sea_orm(column_name = "processedAt")]
    pub processed_at: Option<DateTime>,
    #[sea_orm(column_name = "outboundMessage", column_type = "Text")]
    pub outbound_message: Option<String>,
    #[sea_orm(column_name = "providerMessageId")]
    pub provider_message_id: Option<String>,
    #[sea_orm(column_type = "Text")]
    pub error: Option<String>,
    #[sea_orm(column_name = "automationJobId")]
    pub automation_job_id: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::automation_job::Entity",
        from = "Column::AutomationJobId",
        to = "super::automation_job::Column::Id",
        on_delete = "Cascade"
    )]
    AutomationJob,
}

impl Related<super::automation_job::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AutomationJob.def()
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
        Ok(self)
    }
}
