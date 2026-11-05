use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "AutomationJob")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    pub name: String,
    #[sea_orm(default_value = true)]
    pub enabled: bool,
    #[sea_orm(column_type = "Text")]
    pub prompt: Option<String>,
    #[sea_orm(column_name = "cronExpression")]
    pub cron_expression: String,
    #[sea_orm(column_name = "nextRunAt")]
    pub next_run_at: DateTime,
    #[sea_orm(column_name = "messagingChannelId")]
    pub messaging_channel_id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "emailAccountId")]
    pub email_account_id: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::messaging_channel::Entity",
        from = "(Column::MessagingChannelId, Column::EmailAccountId)",
        to = "(super::messaging_channel::Column::Id, super::messaging_channel::Column::EmailAccountId)",
        on_delete = "Cascade"
    )]
    MessagingChannel,
    #[sea_orm(has_many = "super::automation_job_run::Entity")]
    Runs,
}

impl Related<super::messaging_channel::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::MessagingChannel.def()
    }
}

impl Related<super::automation_job_run::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Runs.def()
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
