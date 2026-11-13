use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "user_sync_state")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub user_id: i32,
    #[sea_orm(default_value = 0)]
    pub last_seq: i32,
    pub last_snapshot_seq: Option<i32>,
    pub snapshot_data: Option<Vec<u8>>,
    pub snapshot_at: Option<i64>,
    #[sea_orm(default_value = 1)]
    pub snapshot_schema_version: Option<i32>,
    pub latest_full_state_seq: Option<i32>,
    pub latest_full_state_vector_clock: Option<Json>,
    pub latest_state_replacement_seq: Option<i32>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::UserId",
        to = "super::user::Column::Id",
        on_delete = "Cascade"
    )]
    User,
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::User.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
