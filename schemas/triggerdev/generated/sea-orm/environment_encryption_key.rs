use super::environment_key_wrapping_provider::EnvironmentKeyWrappingProvider;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "EnvironmentEncryptionKey")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "environmentId")]
    pub environment_id: String,
    #[sea_orm(column_name = "wrappedKey")]
    pub wrapped_key: Json,
    #[sea_orm(column_name = "wrappingProvider")]
    pub wrapping_provider: EnvironmentKeyWrappingProvider,
    #[sea_orm(column_name = "wrappingKeyReference")]
    pub wrapping_key_reference: String,
    #[sea_orm(default_value = 0)]
    pub revision: i32,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::runtime_environment::Entity",
        from = "Column::EnvironmentId",
        to = "super::runtime_environment::Column::Id",
        on_delete = "Cascade"
    )]
    Environment,
    #[sea_orm(has_many = "super::environment_encryption_policy::Entity")]
    ActiveFor,
}

impl Related<super::runtime_environment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Environment.def()
    }
}

impl Related<super::environment_encryption_policy::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ActiveFor.def()
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
