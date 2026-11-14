use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "BugReport")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "createdAt", column_type = "TimestampWithTimeZone")]
    pub created_at: DateTimeWithTimeZone,
    pub source: String,
    pub kind: String,
    pub title: String,
    pub summary: Option<String>,
    #[sea_orm(column_name = "sessionData")]
    pub session_data: Option<String>,
    #[sea_orm(column_name = "sessionTruncated", default_value = false)]
    pub session_truncated: bool,
    pub agent: Option<String>,
    #[sea_orm(column_name = "contactEmail")]
    pub contact_email: Option<String>,
    #[sea_orm(column_name = "cliVersion")]
    pub cli_version: Option<String>,
    #[sea_orm(column_name = "linkedProjectId")]
    pub linked_project_id: Option<String>,
    pub metadata: Option<Json>,
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
            self.created_at = Set(now.fixed_offset());
        }
        Ok(self)
    }
}
