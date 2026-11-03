use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "website_event")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "event_id", column_type = "Uuid")]
    pub id: String,
    #[sea_orm(column_type = "Uuid")]
    pub website_id: String,
    #[sea_orm(column_type = "Uuid")]
    pub session_id: String,
    #[sea_orm(column_type = "Uuid")]
    pub visit_id: String,
    #[sea_orm(column_type = "TimestampWithTimeZone")]
    pub created_at: Option<DateTimeWithTimeZone>,
    #[sea_orm(column_type = "String(StringLen::N(500))")]
    pub url_path: String,
    #[sea_orm(column_type = "String(StringLen::N(500))")]
    pub url_query: Option<String>,
    #[sea_orm(column_type = "String(StringLen::N(255))")]
    pub utm_source: Option<String>,
    #[sea_orm(column_type = "String(StringLen::N(255))")]
    pub utm_medium: Option<String>,
    #[sea_orm(column_type = "String(StringLen::N(255))")]
    pub utm_campaign: Option<String>,
    #[sea_orm(column_type = "String(StringLen::N(255))")]
    pub utm_content: Option<String>,
    #[sea_orm(column_type = "String(StringLen::N(255))")]
    pub utm_term: Option<String>,
    #[sea_orm(column_type = "String(StringLen::N(500))")]
    pub referrer_path: Option<String>,
    #[sea_orm(column_type = "String(StringLen::N(500))")]
    pub referrer_query: Option<String>,
    #[sea_orm(column_type = "String(StringLen::N(500))")]
    pub referrer_domain: Option<String>,
    #[sea_orm(column_type = "String(StringLen::N(500))")]
    pub page_title: Option<String>,
    #[sea_orm(column_type = "String(StringLen::N(255))")]
    pub gclid: Option<String>,
    #[sea_orm(column_type = "String(StringLen::N(255))")]
    pub fbclid: Option<String>,
    #[sea_orm(column_type = "String(StringLen::N(255))")]
    pub msclkid: Option<String>,
    #[sea_orm(column_type = "String(StringLen::N(255))")]
    pub ttclid: Option<String>,
    #[sea_orm(column_name = "li_fat_id", column_type = "String(StringLen::N(255))")]
    pub lifatid: Option<String>,
    #[sea_orm(column_type = "String(StringLen::N(255))")]
    pub twclid: Option<String>,
    #[sea_orm(default_value = 1)]
    pub event_type: i32,
    #[sea_orm(column_type = "String(StringLen::N(50))")]
    pub event_name: Option<String>,
    #[sea_orm(column_type = "String(StringLen::N(50))")]
    pub tag: Option<String>,
    #[sea_orm(column_type = "String(StringLen::N(100))")]
    pub hostname: Option<String>,
    #[sea_orm(column_type = "Decimal(Some((10, 1)))")]
    pub lcp: Option<Decimal>,
    #[sea_orm(column_type = "Decimal(Some((10, 1)))")]
    pub inp: Option<Decimal>,
    #[sea_orm(column_type = "Decimal(Some((10, 4)))")]
    pub cls: Option<Decimal>,
    #[sea_orm(column_type = "Decimal(Some((10, 1)))")]
    pub fcp: Option<Decimal>,
    #[sea_orm(column_type = "Decimal(Some((10, 1)))")]
    pub ttfb: Option<Decimal>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::session::Entity",
        from = "Column::SessionId",
        to = "super::session::Column::Id"
    )]
    Session,
    #[sea_orm(has_many = "super::event_data::Entity")]
    EventData,
}

impl Related<super::session::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Session.def()
    }
}

impl Related<super::event_data::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EventData.def()
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
