use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "source_spec")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub package: String,
    #[sea_orm(primary_key, auto_increment = false)]
    pub version: String,
    pub specs: Option<Json>,
    #[sea_orm(column_type = "TimestampWithTimeZone")]
    pub timestamp: DateTimeWithTimeZone,
    pub error: Option<String>,
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
        if insert && self.timestamp.is_not_set() {
            self.timestamp = Set(now.fixed_offset());
        }
        Ok(self)
    }
}
