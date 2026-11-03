use super::retention_run_item_action::RetentionRunItemAction;
use super::retention_skip_reason::RetentionSkipReason;
use super::retention_target_type::RetentionTargetType;
use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "RetentionRunItem")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "runId")]
    pub run_id: String,
    #[sea_orm(column_name = "targetType")]
    pub target_type: RetentionTargetType,
    #[sea_orm(column_name = "targetId")]
    pub target_id: String,
    #[sea_orm(column_name = "targetName")]
    pub target_name: Option<String>,
    pub action: RetentionRunItemAction,
    #[sea_orm(default_value = 1)]
    pub count: i32,
    pub recipient: Option<String>,
    #[sea_orm(column_name = "skipReason")]
    pub skip_reason: Option<RetentionSkipReason>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::retention_run::Entity",
        from = "Column::RunId",
        to = "super::retention_run::Column::Id",
        on_delete = "Cascade"
    )]
    Run,
}

impl Related<super::retention_run::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Run.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
