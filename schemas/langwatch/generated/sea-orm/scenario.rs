use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Scenario")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    pub name: String,
    pub situation: String,
    pub criteria: Vec<String>,
    pub labels: Vec<String>,
    pub parameters: Option<Json>,
    #[sea_orm(column_name = "simulatorModel")]
    pub simulator_model: Option<String>,
    #[sea_orm(column_name = "judgeModel")]
    pub judge_model: Option<String>,
    #[sea_orm(column_name = "maxTurns")]
    pub max_turns: Option<i32>,
    #[sea_orm(column_name = "minTurns")]
    pub min_turns: Option<i32>,
    pub fields: Option<Json>,
    #[sea_orm(column_name = "callerVoice")]
    pub caller_voice: Option<Json>,
    #[sea_orm(column_name = "testSuiteId")]
    pub test_suite_id: Option<String>,
    #[sea_orm(default_value = 1)]
    pub version: i32,
    #[sea_orm(column_name = "lastUpdatedById")]
    pub last_updated_by_id: Option<String>,
    #[sea_orm(column_name = "archivedAt")]
    pub archived_at: Option<DateTime>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::project::Entity",
        from = "Column::ProjectId",
        to = "super::project::Column::Id"
    )]
    Project,
    #[sea_orm(
        belongs_to = "super::simulation_suite::Entity",
        from = "Column::TestSuiteId",
        to = "super::simulation_suite::Column::Id",
        on_delete = "SetNull"
    )]
    TestSuite,
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::LastUpdatedById",
        to = "super::user::Column::Id"
    )]
    LastUpdatedBy,
    #[sea_orm(has_many = "super::scenario_version::Entity")]
    Versions,
}

impl Related<super::project::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Project.def()
    }
}

impl Related<super::simulation_suite::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TestSuite.def()
    }
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::LastUpdatedBy.def()
    }
}

impl Related<super::scenario_version::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Versions.def()
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
