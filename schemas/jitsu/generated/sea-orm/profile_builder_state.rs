use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "ProfileBuilderState")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "profileBuilderId")]
    pub profile_builder_id: String,
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "profileBuilderVersion")]
    pub profile_builder_version: i32,
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "instanceIndex")]
    pub instance_index: i32,
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "totalInstances")]
    pub total_instances: i32,
    #[sea_orm(column_name = "startedAt")]
    pub started_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "lastTimestamp")]
    pub last_timestamp: Option<DateTime>,
    #[sea_orm(column_name = "processedUsers")]
    pub processed_users: i32,
    #[sea_orm(column_name = "totalUsers")]
    pub total_users: i32,
    #[sea_orm(column_name = "errorUsers")]
    pub error_users: i32,
    pub speed: f64,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

impl ActiveModelBehavior for ActiveModel {}
