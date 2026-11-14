use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "SimulationSuite")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    pub name: String,
    pub slug: String,
    pub description: Option<String>,
    #[sea_orm(column_name = "scenarioIds")]
    pub scenario_ids: Vec<String>,
    pub targets: Json,
    #[sea_orm(column_name = "repeatCount", default_value = 1)]
    pub repeat_count: i32,
    pub labels: Vec<String>,
    #[sea_orm(column_name = "simulatorModel")]
    pub simulator_model: Option<String>,
    #[sea_orm(column_name = "judgeModel")]
    pub judge_model: Option<String>,
    #[sea_orm(default_value = "run_plan")]
    pub kind: String,
    pub scope: Option<Json>,
    pub fields: Option<Json>,
    pub evaluators: Option<Json>,
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
    #[sea_orm(has_many = "super::scenario::Entity")]
    TestSuiteScenarios,
}

impl Related<super::project::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Project.def()
    }
}

impl Related<super::scenario::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TestSuiteScenarios.def()
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
