use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "NotificationState")]
pub struct Model {
    #[sea_orm(column_name = "workspaceId")]
    pub workspace_id: String,
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "actorId")]
    pub actor_id: String,
    #[sea_orm(primary_key, auto_increment = false)]
    pub r#type: String,
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "tableName")]
    pub table_name: String,
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "channelId")]
    pub channel_id: String,
    #[sea_orm(column_name = "flappingSince", column_type = "TimestampWithTimeZone")]
    pub flapping_since: Option<DateTimeWithTimeZone>,
    #[sea_orm(
        column_name = "lastNotification",
        column_type = "TimestampWithTimeZone"
    )]
    pub last_notification: DateTimeWithTimeZone,
    #[sea_orm(column_name = "statusChangeId")]
    pub status_change_id: i64,
    pub error: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

impl ActiveModelBehavior for ActiveModel {}
