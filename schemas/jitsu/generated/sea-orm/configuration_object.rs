use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "ConfigurationObject")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(default_value = false)]
    pub deleted: Option<bool>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    pub r#type: String,
    #[sea_orm(column_name = "workspaceId")]
    pub workspace_id: String,
    pub config: Option<Json>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::workspace::Entity",
        from = "Column::WorkspaceId",
        to = "super::workspace::Column::Id"
    )]
    Workspace,
    #[sea_orm(
        has_many = "super::configuration_object_link::Entity",
        from = "Column::Id",
        to = "super::configuration_object_link::Column::ToId"
    )]
    FromLinks,
    #[sea_orm(
        has_many = "super::configuration_object_link::Entity",
        from = "Column::Id",
        to = "super::configuration_object_link::Column::FromId"
    )]
    ToLinks,
    #[sea_orm(has_many = "super::profile_builder::Entity")]
    ProfileBuilder,
    #[sea_orm(has_many = "super::profile_builder_function::Entity")]
    ProfileBuilderFunction,
}

impl Related<super::workspace::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Workspace.def()
    }
}

impl Related<super::configuration_object_link::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::FromLinks.def()
    }
}

impl Related<super::profile_builder::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ProfileBuilder.def()
    }
}

impl Related<super::profile_builder_function::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ProfileBuilderFunction.def()
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
