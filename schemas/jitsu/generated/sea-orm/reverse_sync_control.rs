use super::reverse_sync_extraction::reverse_sync_extraction;
use super::reverse_sync_mode::reverse_sync_mode;
use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "reverse_sync_control")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub workspace_id: String,
    #[sea_orm(primary_key, auto_increment = false)]
    pub sync_id: String,
    #[sea_orm(primary_key, auto_increment = false)]
    pub run_id: String,
    #[sea_orm(default_value = "0")]
    pub run_order: i64,
    #[sea_orm(default_value = false)]
    pub detached: bool,
    pub revision: String,
    pub target_hash: String,
    #[sea_orm(default_value = "new")]
    pub phase: String,
    pub mode: reverse_sync_mode,
    pub extraction: reverse_sync_extraction,
    pub artifact_head: Option<Vec<u8>>,
    #[sea_orm(default_value = "0")]
    pub base_sequence: i64,
    #[sea_orm(default_value = "0")]
    pub next_sequence: i64,
    #[sea_orm(default_value = "0")]
    pub checkpoint_sequence: i64,
    pub finish_sequence: Option<i64>,
    pub store: Option<Vec<u8>>,
    pub provider_state: Option<Vec<u8>>,
    pub finish_result: Option<Vec<u8>>,
    pub committed_generation: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

impl ActiveModelBehavior for ActiveModel {}
