use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "aiProviders")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    #[sea_orm(column_type = "String(StringLen::N(255))")]
    pub title: String,
    #[sea_orm(column_type = "String(StringLen::N(50))")]
    pub provider: String,
    #[sea_orm(column_name = "baseURL", column_type = "String(StringLen::N(500))")]
    pub base_url: Option<String>,
    #[sea_orm(column_name = "apiKey", column_type = "String(StringLen::N(500))")]
    pub api_key: Option<String>,
    pub config: Option<Json>,
    #[sea_orm(column_name = "sortOrder", default_value = 0)]
    pub sort_order: i32,
    #[sea_orm(column_name = "createdAt", column_type = "TimestampWithTimeZone")]
    pub created_at: DateTimeWithTimeZone,
    #[sea_orm(column_name = "updatedAt", column_type = "TimestampWithTimeZone")]
    pub updated_at: DateTimeWithTimeZone,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::ai_models::Entity")]
    Models,
}

impl Related<super::ai_models::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Models.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.created_at.is_not_set() {
            self.created_at = Set(now.fixed_offset());
        }
        if !self.updated_at.is_set() {
            self.updated_at = Set(now.fixed_offset());
        }
        Ok(self)
    }
}
