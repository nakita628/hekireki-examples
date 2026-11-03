use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "heatmap_event")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "heatmap_event_id", column_type = "Uuid")]
    pub id: String,
    #[sea_orm(column_type = "Uuid")]
    pub website_id: String,
    #[sea_orm(column_type = "Uuid")]
    pub session_id: String,
    #[sea_orm(column_type = "Uuid")]
    pub visit_id: String,
    #[sea_orm(column_type = "String(StringLen::N(500))")]
    pub url_path: String,
    pub event_type: i32,
    pub x: Option<i32>,
    pub y: Option<i32>,
    pub page_x: Option<i32>,
    pub page_y: Option<i32>,
    pub page_w: Option<i32>,
    pub viewport_w: Option<i32>,
    pub viewport_h: Option<i32>,
    pub page_h: Option<i32>,
    pub scroll_pct: Option<i32>,
    #[sea_orm(column_type = "TimestampWithTimeZone")]
    pub created_at: DateTimeWithTimeZone,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::website::Entity",
        from = "Column::WebsiteId",
        to = "super::website::Column::Id"
    )]
    Website,
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
            self.created_at = Set(now.fixed_offset());
        }
        Ok(self)
    }
}
