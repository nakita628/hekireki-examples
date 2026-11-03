use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "mastra_knowledge_mentions")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "sourceType")]
    pub source_type: String,
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "sourceId")]
    pub source_id: String,
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "recordId")]
    pub record_id: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

impl ActiveModelBehavior for ActiveModel {}
