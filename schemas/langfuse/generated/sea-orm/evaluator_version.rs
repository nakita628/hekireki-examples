use super::evaluator_source_code_language::EvaluatorSourceCodeLanguage;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "evaluator_versions")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub created_at: DateTime,
    pub evaluator_id: String,
    pub version: i32,
    pub created_by_user_id: Option<String>,
    pub prompt: Option<String>,
    pub prompt_messages: Option<Json>,
    pub partner: Option<String>,
    pub model: Option<String>,
    pub provider: Option<String>,
    pub model_params: Option<Json>,
    pub vars: Vec<String>,
    pub variable_mapping: Option<Json>,
    pub output_definition: Option<Json>,
    #[sea_orm(column_type = "String(StringLen::N(262144))")]
    pub source_code: Option<String>,
    pub source_code_language: Option<EvaluatorSourceCodeLanguage>,
    pub questions: Option<Json>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::evaluator::Entity",
        from = "Column::EvaluatorId",
        to = "super::evaluator::Column::Id",
        on_delete = "Cascade"
    )]
    Evaluator,
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::CreatedByUserId",
        to = "super::user::Column::Id",
        on_delete = "SetNull"
    )]
    CreatedByUser,
    #[sea_orm(has_many = "super::topic_clustering_run::Entity")]
    TopicClusteringRuns,
}

impl Related<super::evaluator::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Evaluator.def()
    }
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CreatedByUser.def()
    }
}

impl Related<super::topic_clustering_run::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TopicClusteringRuns.def()
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
        Ok(self)
    }
}
