use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "report")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "report_id", column_type = "Uuid")]
    pub id: String,
    #[sea_orm(column_type = "Uuid")]
    pub user_id: String,
    #[sea_orm(column_type = "Uuid")]
    pub website_id: String,
    #[sea_orm(column_type = "String(StringLen::N(50))")]
    pub r#type: String,
    #[sea_orm(column_type = "String(StringLen::N(200))")]
    pub name: String,
    #[sea_orm(column_type = "String(StringLen::N(500))")]
    pub description: String,
    pub parameters: Json,
    #[sea_orm(column_type = "TimestampWithTimeZone")]
    pub created_at: Option<DateTimeWithTimeZone>,
    #[sea_orm(column_type = "TimestampWithTimeZone")]
    pub updated_at: Option<DateTimeWithTimeZone>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::UserId",
        to = "super::user::Column::Id"
    )]
    User,
    #[sea_orm(
        belongs_to = "super::website::Entity",
        from = "Column::WebsiteId",
        to = "super::website::Column::Id"
    )]
    Website,
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::User.def()
    }
}

impl Related<super::website::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Website.def()
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
            self.created_at = Set(Some(now.fixed_offset()));
        }
        if !self.updated_at.is_set() {
            self.updated_at = Set(Some(now.fixed_offset()));
        }
        Ok(self)
    }
}
