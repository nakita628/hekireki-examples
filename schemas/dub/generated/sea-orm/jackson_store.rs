use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "jackson_store")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_type = "String(StringLen::N(250))")]
    pub key: String,
    #[sea_orm(column_type = "Text")]
    pub value: String,
    #[sea_orm(column_type = "String(StringLen::N(64))")]
    pub iv: Option<String>,
    #[sea_orm(column_type = "String(StringLen::N(64))")]
    pub tag: Option<String>,
    #[sea_orm(column_type = "String(StringLen::N(64))")]
    pub namespace: Option<String>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTimeUtc,
    #[sea_orm(column_name = "modifiedAt")]
    pub modified_at: Option<DateTimeUtc>,
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
        if insert && self.created_at.is_not_set() {
            self.created_at = Set(now);
        }
        Ok(self)
    }
}
