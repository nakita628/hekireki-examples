use super::evaluator_block_reason::EvaluatorBlockReason;
use super::job_config_state::JobConfigState;
use super::job_type::JobType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "job_configurations")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub created_at: DateTime,
    pub updated_at: DateTime,
    pub project_id: String,
    pub job_type: JobType,
    #[sea_orm(default_value = "ACTIVE")]
    pub status: JobConfigState,
    pub blocked_at: Option<DateTime>,
    pub block_reason: Option<EvaluatorBlockReason>,
    pub block_message: Option<String>,
    pub eval_template_id: Option<String>,
    pub score_name: String,
    pub filter: Json,
    pub target_object: String,
    pub variable_mapping: Json,
    pub sampling: Decimal,
    pub delay: i32,
    pub time_scope: Vec<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::project::Entity",
        from = "Column::ProjectId",
        to = "super::project::Column::Id",
        on_delete = "Cascade"
    )]
    Project,
    #[sea_orm(
        belongs_to = "super::eval_template::Entity",
        from = "Column::EvalTemplateId",
        to = "super::eval_template::Column::Id",
        on_delete = "SetNull"
    )]
    EvalTemplate,
}

impl Related<super::project::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Project.def()
    }
}

impl Related<super::eval_template::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EvalTemplate.def()
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
