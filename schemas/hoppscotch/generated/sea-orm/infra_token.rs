use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "InfraToken")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "creatorUid")]
    pub creator_uid: String,
    pub label: String,
    #[sea_orm(unique)]
    pub token: String,
    #[sea_orm(column_name = "expiresOn", column_type = "TimestampWithTimeZone")]
    pub expires_on: Option<DateTimeWithTimeZone>,
    #[sea_orm(column_name = "createdOn", column_type = "TimestampWithTimeZone")]
    pub created_on: DateTimeWithTimeZone,
    #[sea_orm(column_name = "updatedOn", column_type = "TimestampWithTimeZone")]
    pub updated_on: DateTimeWithTimeZone,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    fn new() -> Self {
        Self {
            token: Set(uuid::Uuid::new_v4().to_string()),
            ..ActiveModelTrait::default()
        }
    }

    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.created_on.is_not_set() {
            self.created_on = Set(now.fixed_offset());
        }
        if insert && self.updated_on.is_not_set() {
            self.updated_on = Set(now.fixed_offset());
        }
        Ok(self)
    }
}
