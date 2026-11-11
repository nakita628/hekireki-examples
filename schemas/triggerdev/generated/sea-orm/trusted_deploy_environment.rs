use super::trusted_deploy_scope::TrustedDeployScope;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "TrustedDeployEnvironment")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "trustedDeployRepositoryId")]
    pub trusted_deploy_repository_id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "runtimeEnvironmentId")]
    pub runtime_environment_id: String,
    #[sea_orm(default_value = false)]
    pub enabled: bool,
    pub scope: TrustedDeployScope,
    #[sea_orm(column_name = "scopeValue")]
    pub scope_value: Option<String>,
    #[sea_orm(column_name = "additionalClaims", default_value = "{}")]
    pub additional_claims: Json,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::trusted_deploy_repository::Entity",
        from = "Column::TrustedDeployRepositoryId",
        to = "super::trusted_deploy_repository::Column::Id",
        on_delete = "Cascade"
    )]
    TrustedDeployRepository,
    #[sea_orm(
        belongs_to = "super::runtime_environment::Entity",
        from = "Column::RuntimeEnvironmentId",
        to = "super::runtime_environment::Column::Id",
        on_delete = "Cascade"
    )]
    RuntimeEnvironment,
}

impl Related<super::trusted_deploy_repository::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TrustedDeployRepository.def()
    }
}

impl Related<super::runtime_environment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RuntimeEnvironment.def()
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
