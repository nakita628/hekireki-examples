use super::environment_key_wrapping_provider::EnvironmentKeyWrappingProvider;
use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "EnvironmentEncryptionPolicy")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "environmentId")]
    pub environment_id: String,
    #[sea_orm(default_value = 0)]
    pub revision: i32,
    #[sea_orm(column_name = "wrappingProvider")]
    pub wrapping_provider: Option<EnvironmentKeyWrappingProvider>,
    #[sea_orm(column_name = "wrappingKeyReference")]
    pub wrapping_key_reference: Option<String>,
    #[sea_orm(column_name = "activeKeyId")]
    pub active_key_id: Option<String>,
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
    #[sea_orm(
        belongs_to = "super::environment_encryption_key::Entity",
        from = "(Column::ActiveKeyId, Column::EnvironmentId)",
        to = "(super::environment_encryption_key::Column::Id, super::environment_encryption_key::Column::EnvironmentId)",
        on_update = "NoAction",
        on_delete = "NoAction"
    )]
    ActiveKey,
}

impl Related<super::runtime_environment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Environment.def()
    }
}

impl Related<super::environment_encryption_key::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ActiveKey.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
