use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "PublicTypebot")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub version: Option<String>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "lastActivityAt")]
    pub last_activity_at: Option<DateTime>,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "typebotId")]
    pub typebot_id: String,
    pub groups: Json,
    pub events: Option<Json>,
    pub variables: Json,
    pub edges: Json,
    pub theme: Json,
    pub settings: Json,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::typebot::Entity",
        from = "Column::TypebotId",
        to = "super::typebot::Column::Id",
        on_delete = "Cascade"
    )]
    Typebot,
    #[sea_orm(has_many = "super::runtime_media_id_cache::Entity")]
    MediaIdsCache,
}

impl Related<super::typebot::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Typebot.def()
    }
}

impl Related<super::runtime_media_id_cache::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::MediaIdsCache.def()
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
            self.created_at = Set(now.naive_utc());
        }
        if insert && self.updated_at.is_not_set() {
            self.updated_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
