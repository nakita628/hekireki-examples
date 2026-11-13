use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "BulkInboxActionThread")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "jobId")]
    pub job_id: String,
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "threadId")]
    pub thread_id: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::bulk_inbox_action_job::Entity",
        from = "Column::JobId",
        to = "super::bulk_inbox_action_job::Column::Id",
        on_delete = "Cascade"
    )]
    Job,
}

impl Related<super::bulk_inbox_action_job::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Job.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
