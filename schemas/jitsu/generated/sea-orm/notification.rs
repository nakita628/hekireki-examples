use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Notification")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_type = "Uuid")]
    pub id: String,
    #[sea_orm(column_name = "workspaceId")]
    pub workspace_id: String,
    #[sea_orm(column_name = "actorId")]
    pub actor_id: String,
    pub r#type: String,
    #[sea_orm(column_name = "tableName")]
    pub table_name: String,
    #[sea_orm(column_name = "channelId")]
    pub channel_id: String,
    #[sea_orm(column_name = "statusChangeId")]
    pub status_change_id: i64,
    #[sea_orm(column_type = "TimestampWithTimeZone")]
    pub timestamp: DateTimeWithTimeZone,
    pub status: String,
    pub error: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    fn new() -> Self {
        Self {
            id: Set(uuid::Uuid::new_v4().to_string()),
            ..ActiveModelTrait::default()
        }
    }

    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.timestamp.is_not_set() {
            self.timestamp = Set(now.fixed_offset());
        }
        Ok(self)
    }
}
