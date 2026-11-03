use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "MockServerLog")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "mockServerID")]
    pub mock_server_id: String,
    #[sea_orm(column_name = "requestMethod")]
    pub request_method: String,
    #[sea_orm(column_name = "requestPath")]
    pub request_path: String,
    #[sea_orm(column_name = "requestHeaders")]
    pub request_headers: Json,
    #[sea_orm(column_name = "requestBody")]
    pub request_body: Option<Json>,
    #[sea_orm(column_name = "requestQuery")]
    pub request_query: Option<Json>,
    #[sea_orm(column_name = "responseStatus")]
    pub response_status: i32,
    #[sea_orm(column_name = "responseHeaders")]
    pub response_headers: Json,
    #[sea_orm(column_name = "responseBody")]
    pub response_body: Option<Json>,
    #[sea_orm(column_name = "responseTime")]
    pub response_time: i32,
    #[sea_orm(column_name = "ipAddress")]
    pub ip_address: Option<String>,
    #[sea_orm(column_name = "userAgent")]
    pub user_agent: Option<String>,
    #[sea_orm(column_name = "executedAt", column_type = "TimestampWithTimeZone")]
    pub executed_at: DateTimeWithTimeZone,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::mock_server::Entity",
        from = "Column::MockServerId",
        to = "super::mock_server::Column::Id",
        on_delete = "Cascade"
    )]
    MockServer,
}

impl Related<super::mock_server::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::MockServer.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.executed_at.is_not_set() {
            self.executed_at = Set(now.fixed_offset());
        }
        Ok(self)
    }
}
