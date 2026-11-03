use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "session_data")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "session_data_id", column_type = "Uuid")]
    pub id: String,
    #[sea_orm(column_type = "Uuid")]
    pub website_id: String,
    #[sea_orm(column_type = "Uuid")]
    pub session_id: String,
    #[sea_orm(column_type = "String(StringLen::N(500))")]
    pub data_key: String,
    #[sea_orm(column_type = "String(StringLen::N(500))")]
    pub string_value: Option<String>,
    #[sea_orm(column_type = "Decimal(Some((19, 4)))")]
    pub number_value: Option<Decimal>,
    #[sea_orm(column_type = "TimestampWithTimeZone")]
    pub date_value: Option<DateTimeWithTimeZone>,
    pub data_type: i32,
    #[sea_orm(column_type = "String(StringLen::N(50))")]
    pub distinct_id: Option<String>,
    #[sea_orm(column_type = "TimestampWithTimeZone")]
    pub created_at: Option<DateTimeWithTimeZone>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::website::Entity",
        from = "Column::WebsiteId",
        to = "super::website::Column::Id"
    )]
    Website,
    #[sea_orm(
        belongs_to = "super::session::Entity",
        from = "Column::SessionId",
        to = "super::session::Column::Id"
    )]
    Session,
}

impl Related<super::website::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Website.def()
    }
}

impl Related<super::session::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Session.def()
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
        Ok(self)
    }
}
