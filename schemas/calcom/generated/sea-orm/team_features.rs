use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "TeamFeatures")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "teamId")]
    pub team_id: i32,
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "featureId")]
    pub feature_id: String,
    pub enabled: bool,
    #[sea_orm(column_name = "assignedAt")]
    pub assigned_at: DateTime,
    #[sea_orm(column_name = "assignedBy")]
    pub assigned_by: String,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::team::Entity",
        from = "Column::TeamId",
        to = "super::team::Column::Id",
        on_delete = "Cascade"
    )]
    Team,
    #[sea_orm(
        belongs_to = "super::feature::Entity",
        from = "Column::FeatureId",
        to = "super::feature::Column::Slug",
        on_delete = "Cascade"
    )]
    Feature,
}

impl Related<super::team::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Team.def()
    }
}

impl Related<super::feature::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Feature.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.assigned_at.is_not_set() {
            self.assigned_at = Set(now.naive_utc());
        }
        if !self.updated_at.is_set() {
            self.updated_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
