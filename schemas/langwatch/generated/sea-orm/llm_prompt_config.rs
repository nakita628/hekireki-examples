use super::prompt_scope::PromptScope;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "LlmPromptConfig")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(unique)]
    pub handle: Option<String>,
    pub name: String,
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "deletedAt")]
    pub deleted_at: Option<DateTime>,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: String,
    #[sea_orm(default_value = "PROJECT")]
    pub scope: PromptScope,
    #[sea_orm(column_name = "copiedFromPromptId")]
    pub copied_from_prompt_id: Option<String>,
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
        belongs_to = "super::llm_prompt_config::Entity",
        from = "Column::CopiedFromPromptId",
        to = "super::llm_prompt_config::Column::Id",
        on_update = "Restrict",
        on_delete = "Restrict"
    )]
    CopiedFrom,
    #[sea_orm(
        has_many = "super::llm_prompt_config::Entity",
        from = "Column::Id",
        to = "super::llm_prompt_config::Column::CopiedFromPromptId"
    )]
    CopiedPrompts,
    #[sea_orm(has_many = "super::llm_prompt_config_version::Entity")]
    Versions,
    #[sea_orm(has_many = "super::prompt_tag_assignment::Entity")]
    VersionTags,
}

impl Related<super::project::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Project.def()
    }
}

impl Related<super::llm_prompt_config::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CopiedFrom.def()
    }
}

impl Related<super::llm_prompt_config_version::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Versions.def()
    }
}

impl Related<super::prompt_tag_assignment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::VersionTags.def()
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
