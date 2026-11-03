use super::mock_server_action::MockServerAction;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "MockServerActivity")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "mockServerID")]
    pub mock_server_id: String,
    pub action: MockServerAction,
    #[sea_orm(column_name = "performedBy")]
    pub performed_by: Option<String>,
    #[sea_orm(column_name = "performedAt", column_type = "TimestampWithTimeZone")]
    pub performed_at: DateTimeWithTimeZone,
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
        if insert && self.performed_at.is_not_set() {
            self.performed_at = Set(now.fixed_offset());
        }
        Ok(self)
    }
}
