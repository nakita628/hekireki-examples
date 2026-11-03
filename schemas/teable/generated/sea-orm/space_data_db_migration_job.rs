use super::space_data_db_migration_job_state::SpaceDataDbMigrationJobState;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "space_data_db_migration_job")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub space_id: String,
    pub source_connection_id: Option<String>,
    pub target_connection_id: Option<String>,
    #[sea_orm(default_value = "migrate-space")]
    pub target_mode: String,
    #[sea_orm(default_value = false)]
    pub switch_on_completion: bool,
    #[sea_orm(default_value = "pending")]
    pub state: SpaceDataDbMigrationJobState,
    pub target_url_fingerprint: String,
    pub target_internal_schema: String,
    pub inventory: Option<Json>,
    pub copy_stats: Option<Json>,
    pub validation_stats: Option<Json>,
    pub last_error: Option<String>,
    pub started_at: Option<DateTime>,
    pub completed_at: Option<DateTime>,
    pub created_by: String,
    pub created_time: DateTime,
    pub last_modified_time: Option<DateTime>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::space::Entity",
        from = "Column::SpaceId",
        to = "super::space::Column::Id",
        on_delete = "Cascade"
    )]
    Space,
    #[sea_orm(
        belongs_to = "super::data_db_connection::Entity",
        from = "Column::SourceConnectionId",
        to = "super::data_db_connection::Column::Id"
    )]
    SourceConnection,
    #[sea_orm(
        belongs_to = "super::data_db_connection::Entity",
        from = "Column::TargetConnectionId",
        to = "super::data_db_connection::Column::Id"
    )]
    TargetConnection,
}

impl Related<super::space::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Space.def()
    }
}

impl Related<super::data_db_connection::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SourceConnection.def()
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
