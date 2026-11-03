use super::dataset_status::DatasetStatus;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "dataset_items")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(primary_key, auto_increment = false)]
    pub project_id: String,
    #[sea_orm(default_value = "ACTIVE")]
    pub status: Option<DatasetStatus>,
    pub input: Option<Json>,
    pub expected_output: Option<Json>,
    pub metadata: Option<Json>,
    pub source_trace_id: Option<String>,
    pub source_observation_id: Option<String>,
    pub dataset_id: String,
    pub created_at: DateTime,
    pub updated_at: DateTime,
    #[sea_orm(primary_key, auto_increment = false)]
    pub valid_from: DateTime,
    pub valid_to: Option<DateTime>,
    #[sea_orm(default_value = false)]
    pub is_deleted: bool,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::dataset::Entity",
        from = "(Column::DatasetId, Column::ProjectId)",
        to = "(super::dataset::Column::Id, super::dataset::Column::ProjectId)",
        on_delete = "Cascade"
    )]
    Dataset,
}

impl Related<super::dataset::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Dataset.def()
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
        if !self.updated_at.is_set() {
            self.updated_at = Set(now.naive_utc());
        }
        if insert && self.valid_from.is_not_set() {
            self.valid_from = Set(now.naive_utc());
        }
        Ok(self)
    }
}
