use super::secret_store_provider::SecretStoreProvider;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "SecretReference")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(unique)]
    pub key: String,
    #[sea_orm(default_value = "DATABASE")]
    pub provider: SecretStoreProvider,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::environment_variable_value::Entity")]
    EnvironmentVariableValues,
    #[sea_orm(has_many = "super::organization_integration::Entity")]
    OrganizationIntegration,
    #[sea_orm(has_many = "super::user::Entity")]
    User,
}

impl Related<super::environment_variable_value::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EnvironmentVariableValues.def()
    }
}

impl Related<super::organization_integration::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OrganizationIntegration.def()
    }
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::User.def()
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
