use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Workspace")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    pub name: String,
    #[sea_orm(default_value = false)]
    pub deleted: bool,
    #[sea_orm(unique)]
    pub slug: Option<String>,
    #[sea_orm(column_name = "featuresEnabled")]
    pub features_enabled: Vec<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::workspace_access::Entity")]
    WorkspaceAccess,
    #[sea_orm(has_many = "super::workspace_options::Entity")]
    WorkspaceOptions,
    #[sea_orm(has_many = "super::configuration_object::Entity")]
    ConfigurationObject,
    #[sea_orm(has_many = "super::configuration_object_link::Entity")]
    ConfigurationObjectLink,
    #[sea_orm(has_many = "super::user_preferences::Entity")]
    Preferences,
    #[sea_orm(has_many = "super::workspace_user_properties::Entity")]
    WorkspaceUserProperties,
    #[sea_orm(has_many = "super::profile_builder::Entity")]
    ProfileBuilders,
    #[sea_orm(has_many = "super::oidc_login_group::Entity")]
    OidcLoginGroups,
}

impl Related<super::workspace_access::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::WorkspaceAccess.def()
    }
}

impl Related<super::workspace_options::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::WorkspaceOptions.def()
    }
}

impl Related<super::configuration_object::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ConfigurationObject.def()
    }
}

impl Related<super::configuration_object_link::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ConfigurationObjectLink.def()
    }
}

impl Related<super::user_preferences::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Preferences.def()
    }
}

impl Related<super::workspace_user_properties::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::WorkspaceUserProperties.def()
    }
}

impl Related<super::profile_builder::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ProfileBuilders.def()
    }
}

impl Related<super::oidc_login_group::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OidcLoginGroups.def()
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
