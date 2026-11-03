use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "ClippingClip")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "clippingId")]
    pub clipping_id: String,
    pub title: String,
    pub content: String,
    pub start: f64,
    pub end: f64,
    #[sea_orm(column_name = "trimStart")]
    pub trim_start: Option<i32>,
    #[sea_orm(default_value = "pending")]
    pub status: String,
    pub error: Option<String>,
    #[sea_orm(column_name = "mediaId")]
    pub media_id: Option<String>,
    pub path: Option<String>,
    pub thumbnail: Option<String>,
    #[sea_orm(column_name = "draftedAt")]
    pub drafted_at: Option<DateTime>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::clipping::Entity",
        from = "Column::ClippingId",
        to = "super::clipping::Column::Id"
    )]
    Clipping,
}

impl Related<super::clipping::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Clipping.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    fn new() -> Self {
        Self {
            id: Set(uuid::Uuid::new_v4().to_string()),
            ..ActiveModelTrait::default()
        }
    }

    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.created_at.is_not_set() {
            self.created_at = Set(now.naive_utc());
        }
        if !self.updated_at.is_set() {
            self.updated_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
