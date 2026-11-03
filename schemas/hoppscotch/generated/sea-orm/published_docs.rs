use super::workspace_type::WorkspaceType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "PublishedDocs")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub slug: String,
    pub title: String,
    #[sea_orm(column_name = "collectionID")]
    pub collection_id: String,
    #[sea_orm(column_name = "creatorUid")]
    pub creator_uid: String,
    pub version: String,
    #[sea_orm(column_name = "autoSync")]
    pub auto_sync: bool,
    #[sea_orm(column_name = "documentTree")]
    pub document_tree: Option<Json>,
    #[sea_orm(column_name = "workspaceType")]
    pub workspace_type: WorkspaceType,
    #[sea_orm(column_name = "workspaceID")]
    pub workspace_id: String,
    #[sea_orm(column_name = "environmentID")]
    pub environment_id: Option<String>,
    #[sea_orm(column_name = "environmentName")]
    pub environment_name: Option<String>,
    #[sea_orm(column_name = "environmentVariables")]
    pub environment_variables: Option<Json>,
    pub metadata: Option<Json>,
    #[sea_orm(column_name = "createdOn", column_type = "TimestampWithTimeZone")]
    pub created_on: DateTimeWithTimeZone,
    #[sea_orm(column_name = "updatedOn", column_type = "TimestampWithTimeZone")]
    pub updated_on: DateTimeWithTimeZone,
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
        if insert && self.created_on.is_not_set() {
            self.created_on = Set(now.fixed_offset());
        }
        if !self.updated_on.is_set() {
            self.updated_on = Set(now.fixed_offset());
        }
        Ok(self)
    }
}
