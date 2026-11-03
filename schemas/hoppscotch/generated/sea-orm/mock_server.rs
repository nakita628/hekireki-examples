use super::workspace_type::WorkspaceType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "MockServer")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub name: String,
    #[sea_orm(unique)]
    pub subdomain: String,
    #[sea_orm(column_name = "creatorUid")]
    pub creator_uid: Option<String>,
    #[sea_orm(column_name = "collectionID")]
    pub collection_id: String,
    #[sea_orm(column_name = "workspaceType")]
    pub workspace_type: WorkspaceType,
    #[sea_orm(column_name = "workspaceID")]
    pub workspace_id: String,
    #[sea_orm(column_name = "delayInMs", default_value = 0)]
    pub delay_in_ms: i32,
    #[sea_orm(column_name = "isPublic", default_value = true)]
    pub is_public: bool,
    #[sea_orm(column_name = "isActive", default_value = true)]
    pub is_active: bool,
    #[sea_orm(column_name = "hitCount", default_value = 0)]
    pub hit_count: i32,
    #[sea_orm(column_name = "lastHitAt", column_type = "TimestampWithTimeZone")]
    pub last_hit_at: Option<DateTimeWithTimeZone>,
    #[sea_orm(column_name = "createdOn", column_type = "TimestampWithTimeZone")]
    pub created_on: DateTimeWithTimeZone,
    #[sea_orm(column_name = "updatedOn", column_type = "TimestampWithTimeZone")]
    pub updated_on: DateTimeWithTimeZone,
    #[sea_orm(column_name = "deletedAt", column_type = "TimestampWithTimeZone")]
    pub deleted_at: Option<DateTimeWithTimeZone>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::CreatorUid",
        to = "super::user::Column::Uid",
        on_delete = "SetNull"
    )]
    User,
    #[sea_orm(has_many = "super::mock_server_log::Entity")]
    RequestLogs,
    #[sea_orm(has_many = "super::mock_server_activity::Entity")]
    ActivityHistory,
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::User.def()
    }
}

impl Related<super::mock_server_log::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RequestLogs.def()
    }
}

impl Related<super::mock_server_activity::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ActivityHistory.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.created_on.is_not_set() {
            self.created_on = Set(now.fixed_offset());
        }
        if !self.updated_on.is_set() {
            self.updated_on = Set(now.fixed_offset());
        }
        Ok(self)
    }
}
