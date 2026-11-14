use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "SelfHostedInstanceDiagnostic")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "instanceId")]
    pub instance_id: String,
    #[sea_orm(column_name = "stepId")]
    pub step_id: String,
    #[sea_orm(column_name = "stepDescription")]
    pub step_description: Option<String>,
    pub release: Option<String>,
    pub attempt: i32,
    pub trigger: String,
    #[sea_orm(column_name = "byteLength")]
    pub byte_length: i32,
    #[sea_orm(column_name = "objectKey")]
    pub object_key: String,
    #[sea_orm(column_name = "receivedAt")]
    pub received_at: DateTime,
    #[sea_orm(column_name = "expiresAt")]
    pub expires_at: DateTime,
    #[sea_orm(column_name = "slackAnnouncedAt")]
    pub slack_announced_at: Option<DateTime>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.received_at.is_not_set() {
            self.received_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
