use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "VerificationToken")]
pub struct Model {
    pub identifier: String,
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(unique)]
    pub token: String,
    pub value: Option<String>,
    #[sea_orm(column_name = "failedAttempts", default_value = 0)]
    pub failed_attempts: i32,
    pub expires: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

impl ActiveModelBehavior for ActiveModel {}
