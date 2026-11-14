use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "IngestionSource")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: String,
    #[sea_orm(column_name = "teamId")]
    pub team_id: Option<String>,
    #[sea_orm(column_name = "sourceType")]
    pub source_type: String,
    pub name: String,
    pub description: Option<String>,
    #[sea_orm(column_name = "ingestSecretHash")]
    pub ingest_secret_hash: String,
    #[sea_orm(column_name = "parserConfig", default_value = "{}")]
    pub parser_config: Json,
    #[sea_orm(column_name = "pollerCursor")]
    pub poller_cursor: Option<Json>,
    #[sea_orm(column_name = "errorCount", default_value = 0)]
    pub error_count: i32,
    #[sea_orm(column_name = "pullSchedule")]
    pub pull_schedule: Option<String>,
    #[sea_orm(default_value = "awaiting_first_event")]
    pub status: String,
    #[sea_orm(column_name = "traceProjectId")]
    pub trace_project_id: Option<String>,
    #[sea_orm(column_name = "providerAccountId")]
    pub provider_account_id: Option<String>,
    #[sea_orm(column_name = "lastEventAt")]
    pub last_event_at: Option<DateTime>,
    #[sea_orm(column_name = "lastSuccessAt")]
    pub last_success_at: Option<DateTime>,
    #[sea_orm(column_name = "lastReadThroughAt")]
    pub last_read_through_at: Option<DateTime>,
    #[sea_orm(column_name = "lastRunCompleteness")]
    pub last_run_completeness: Option<String>,
    #[sea_orm(column_name = "unpricedUsageSince")]
    pub unpriced_usage_since: Option<DateTime>,
    #[sea_orm(column_name = "unpricedUsageThrough")]
    pub unpriced_usage_through: Option<DateTime>,
    #[sea_orm(column_name = "archivedAt")]
    pub archived_at: Option<DateTime>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "createdById")]
    pub created_by_id: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::organization::Entity",
        from = "Column::OrganizationId",
        to = "super::organization::Column::Id",
        on_delete = "Cascade"
    )]
    Organization,
    #[sea_orm(
        belongs_to = "super::team::Entity",
        from = "Column::TeamId",
        to = "super::team::Column::Id",
        on_delete = "SetNull"
    )]
    Team,
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::CreatedById",
        to = "super::user::Column::Id",
        on_delete = "SetNull"
    )]
    CreatedBy,
}

impl Related<super::organization::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Organization.def()
    }
}

impl Related<super::team::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Team.def()
    }
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CreatedBy.def()
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
