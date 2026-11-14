use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "ExperimentVersion")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "experimentId")]
    pub experiment_id: String,
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    pub version: i32,
    #[sea_orm(column_name = "counterVersion")]
    pub counter_version: i32,
    #[sea_orm(column_name = "autoSaved", default_value = false)]
    pub auto_saved: bool,
    #[sea_orm(column_name = "commitMessage")]
    pub commit_message: Option<String>,
    #[sea_orm(column_name = "authorId")]
    pub author_id: Option<String>,
    #[sea_orm(column_name = "authorLabel")]
    pub author_label: String,
    #[sea_orm(column_name = "runId")]
    pub run_id: Option<String>,
    pub state: Json,
    #[sea_orm(column_name = "schemaVersion", default_value = "1")]
    pub schema_version: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

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
