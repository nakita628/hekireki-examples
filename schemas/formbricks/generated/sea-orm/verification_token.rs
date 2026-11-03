use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "VerificationToken")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub identifier: String,
    #[sea_orm(primary_key, auto_increment = false)]
    pub token: String,
    pub expires: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

impl ActiveModelBehavior for ActiveModel {}
