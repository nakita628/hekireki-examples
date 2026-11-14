use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "PromptTagAssignment")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "configId")]
    pub config_id: String,
    #[sea_orm(column_name = "versionId")]
    pub version_id: String,
    #[sea_orm(column_name = "tagId")]
    pub tag_id: String,
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "createdById")]
    pub created_by_id: Option<String>,
    #[sea_orm(column_name = "updatedById")]
    pub updated_by_id: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::llm_prompt_config::Entity",
        from = "Column::ConfigId",
        to = "super::llm_prompt_config::Column::Id",
        on_delete = "Cascade"
    )]
    Config,
    #[sea_orm(
        belongs_to = "super::llm_prompt_config_version::Entity",
        from = "Column::VersionId",
        to = "super::llm_prompt_config_version::Column::Id",
        on_delete = "Cascade"
    )]
    Version,
    #[sea_orm(
        belongs_to = "super::prompt_tag::Entity",
        from = "Column::TagId",
        to = "super::prompt_tag::Column::Id",
        on_delete = "Cascade"
    )]
    PromptTag,
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::CreatedById",
        to = "super::user::Column::Id"
    )]
    CreatedBy,
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::UpdatedById",
        to = "super::user::Column::Id"
    )]
    UpdatedBy,
}

impl Related<super::llm_prompt_config::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Config.def()
    }
}

impl Related<super::llm_prompt_config_version::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Version.def()
    }
}

impl Related<super::prompt_tag::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::PromptTag.def()
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
