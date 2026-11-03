use super::error_group_status::ErrorGroupStatus;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "ErrorGroupState")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: String,
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    #[sea_orm(column_name = "environmentId")]
    pub environment_id: Option<String>,
    #[sea_orm(column_name = "taskIdentifier")]
    pub task_identifier: String,
    #[sea_orm(column_name = "errorFingerprint")]
    pub error_fingerprint: String,
    #[sea_orm(default_value = "UNRESOLVED")]
    pub status: ErrorGroupStatus,
    #[sea_orm(column_name = "ignoredUntil")]
    pub ignored_until: Option<DateTime>,
    #[sea_orm(column_name = "ignoredUntilOccurrenceRate")]
    pub ignored_until_occurrence_rate: Option<i32>,
    #[sea_orm(column_name = "ignoredUntilTotalOccurrences")]
    pub ignored_until_total_occurrences: Option<i32>,
    #[sea_orm(column_name = "ignoredAtOccurrenceCount")]
    pub ignored_at_occurrence_count: Option<i64>,
    #[sea_orm(column_name = "ignoredAt")]
    pub ignored_at: Option<DateTime>,
    #[sea_orm(column_name = "ignoredReason")]
    pub ignored_reason: Option<String>,
    #[sea_orm(column_name = "ignoredByUserId")]
    pub ignored_by_user_id: Option<String>,
    #[sea_orm(column_name = "resolvedAt")]
    pub resolved_at: Option<DateTime>,
    #[sea_orm(column_name = "resolvedInVersion")]
    pub resolved_in_version: Option<String>,
    #[sea_orm(column_name = "resolvedBy")]
    pub resolved_by: Option<String>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::organization::Entity",
        from = "Column::OrganizationId",
        to = "super::organization::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    Organization,
    #[sea_orm(
        belongs_to = "super::project::Entity",
        from = "Column::ProjectId",
        to = "super::project::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    Project,
    #[sea_orm(
        belongs_to = "super::runtime_environment::Entity",
        from = "Column::EnvironmentId",
        to = "super::runtime_environment::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    Environment,
}

impl Related<super::organization::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Organization.def()
    }
}

impl Related<super::project::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Project.def()
    }
}

impl Related<super::runtime_environment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Environment.def()
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
