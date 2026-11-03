use super::feature_type::FeatureType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Feature")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub slug: String,
    #[sea_orm(default_value = false)]
    pub enabled: bool,
    pub description: Option<String>,
    #[sea_orm(default_value = "RELEASE")]
    pub r#type: Option<FeatureType>,
    #[sea_orm(default_value = false)]
    pub stale: Option<bool>,
    #[sea_orm(column_name = "lastUsedAt")]
    pub last_used_at: Option<DateTime>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: Option<DateTime>,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: Option<DateTime>,
    #[sea_orm(column_name = "updatedBy")]
    pub updated_by: Option<i32>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::user_features::Entity")]
    Users,
    #[sea_orm(has_many = "super::team_features::Entity")]
    Teams,
}

impl Related<super::user_features::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Users.def()
    }
}

impl Related<super::team_features::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Teams.def()
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
            self.created_at = Set(Some(now.naive_utc()));
        }
        if !self.updated_at.is_set() {
            self.updated_at = Set(Some(now.naive_utc()));
        }
        Ok(self)
    }
}
