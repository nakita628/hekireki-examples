use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "_BackgroundWorkerToBackgroundWorkerFile")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false, column_name = "A")]
    pub background_worker_id: String,
    #[sea_orm(primary_key, auto_increment = false, column_name = "B")]
    pub background_worker_file_id: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::background_worker::Entity",
        from = "Column::BackgroundWorkerId",
        to = "super::background_worker::Column::Id"
    )]
    BackgroundWorker,
    #[sea_orm(
        belongs_to = "super::background_worker_file::Entity",
        from = "Column::BackgroundWorkerFileId",
        to = "super::background_worker_file::Column::Id"
    )]
    BackgroundWorkerFile,
}

impl ActiveModelBehavior for ActiveModel {}
