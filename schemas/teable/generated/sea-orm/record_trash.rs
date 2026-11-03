use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "record_trash")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub table_id: String,
    pub record_id: String,
    pub snapshot: String,
    pub created_time: DateTime,
    pub created_by: String,
    #[sea_orm(default_value = "deleted")]
    pub reason: String,
    pub record_created_time: Option<DateTime>,
    pub record_created_by: Option<String>,
    pub record_last_modified_time: Option<DateTime>,
    pub record_last_modified_by: Option<String>,
    pub operation_id: Option<String>,
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
        if insert && self.created_time.is_not_set() {
            self.created_time = Set(now.naive_utc());
        }
        Ok(self)
    }
}
