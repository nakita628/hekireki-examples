use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "datasets")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(primary_key, auto_increment = false)]
    pub project_id: String,
    pub name: String,
    pub description: Option<String>,
    pub metadata: Option<Json>,
    pub remote_experiment_url: Option<String>,
    pub remote_experiment_payload: Option<Json>,
    #[sea_orm(default_value = true)]
    pub remote_experiment_enabled: bool,
    pub remote_experiment_secret_key: Option<String>,
    pub remote_experiment_display_secret_key: Option<String>,
    pub remote_experiment_request_headers: Option<Json>,
    pub remote_experiment_display_headers: Option<Json>,
    pub input_schema: Option<Json>,
    pub expected_output_schema: Option<Json>,
    pub created_at: DateTime,
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::project::Entity",
        from = "Column::ProjectId",
        to = "super::project::Column::Id",
        on_delete = "Cascade"
    )]
    Project,
    #[sea_orm(has_many = "super::dataset_item::Entity")]
    DatasetItems,
    #[sea_orm(has_many = "super::dataset_runs::Entity")]
    DatasetRuns,
}

impl Related<super::project::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Project.def()
    }
}

impl Related<super::dataset_item::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::DatasetItems.def()
    }
}

impl Related<super::dataset_runs::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::DatasetRuns.def()
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
        Ok(self)
    }
}
