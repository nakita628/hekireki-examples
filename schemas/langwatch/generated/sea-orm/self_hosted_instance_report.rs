use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "SelfHostedInstanceReport")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "instanceId")]
    pub instance_id: String,
    #[sea_orm(column_name = "receivedAt")]
    pub received_at: DateTime,
    pub version: Option<String>,
    #[sea_orm(column_name = "reportSchemaVersion")]
    pub report_schema_version: Option<i32>,
    #[sea_orm(column_name = "unknownFields", default_value = 0)]
    pub unknown_fields: i32,
    pub payload: Json,
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
