use super::data_db_connection_health_state::DataDbConnectionHealthState;
use super::data_db_connection_status::DataDbConnectionStatus;
use super::data_db_provider::DataDbProvider;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "data_db_connection")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(default_value = "postgres")]
    pub provider: DataDbProvider,
    pub encrypted_url: String,
    #[sea_orm(unique)]
    pub url_fingerprint: String,
    pub display_host: Option<String>,
    pub display_database: Option<String>,
    pub internal_schema: String,
    #[sea_orm(default_value = "pending")]
    pub status: DataDbConnectionStatus,
    pub schema_version: Option<String>,
    pub capabilities: Option<Json>,
    pub last_validated_at: Option<DateTime>,
    pub last_error: Option<String>,
    #[sea_orm(default_value = "healthy")]
    pub health_state: DataDbConnectionHealthState,
    pub health_reason: Option<String>,
    pub health_changed_at: Option<DateTime>,
    pub last_health_check_at: Option<DateTime>,
    #[sea_orm(default_value = 0)]
    pub consecutive_health_failures: i32,
    pub created_by: String,
    pub created_time: DateTime,
    pub last_modified_time: Option<DateTime>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::space_data_db_binding::Entity")]
    SpaceBindings,
    #[sea_orm(
        has_many = "super::space_data_db_migration_job::Entity",
        from = "Column::Id",
        to = "super::space_data_db_migration_job::Column::SourceConnectionId"
    )]
    SourceMigrationJobs,
    #[sea_orm(
        has_many = "super::space_data_db_migration_job::Entity",
        from = "Column::Id",
        to = "super::space_data_db_migration_job::Column::TargetConnectionId"
    )]
    TargetMigrationJobs,
}

impl Related<super::space_data_db_binding::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SpaceBindings.def()
    }
}

impl Related<super::space_data_db_migration_job::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SourceMigrationJobs.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.created_time.is_not_set() {
            self.created_time = Set(now.naive_utc());
        }
        if !self.last_modified_time.is_set() {
            self.last_modified_time = Set(Some(now.naive_utc()));
        }
        Ok(self)
    }
}
