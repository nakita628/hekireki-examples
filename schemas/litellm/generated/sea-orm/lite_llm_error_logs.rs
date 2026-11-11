use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "LiteLLM_ErrorLogs")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub request_id: String,
    #[sea_orm(column_name = "startTime")]
    pub start_time: DateTime,
    #[sea_orm(column_name = "endTime")]
    pub end_time: DateTime,
    #[sea_orm(default_value = "")]
    pub api_base: String,
    #[sea_orm(default_value = "")]
    pub model_group: String,
    #[sea_orm(default_value = "")]
    pub litellm_model_name: String,
    #[sea_orm(default_value = "")]
    pub model_id: String,
    #[sea_orm(default_value = "{}")]
    pub request_kwargs: Json,
    #[sea_orm(default_value = "")]
    pub exception_type: String,
    #[sea_orm(default_value = "")]
    pub exception_string: String,
    #[sea_orm(default_value = "")]
    pub status_code: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

impl ActiveModelBehavior for ActiveModel {
    fn new() -> Self {
        Self {
            request_id: Set(uuid::Uuid::new_v4().to_string()),
            ..ActiveModelTrait::default()
        }
    }
}
