use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "OrganizationSeatPeak")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: String,
    #[sea_orm(column_name = "openedAt")]
    pub opened_at: f64,
    #[sea_orm(column_name = "fullSeats")]
    pub full_seats: i32,
    #[sea_orm(column_name = "liteSeats")]
    pub lite_seats: i32,
    #[sea_orm(column_name = "developerSeats")]
    pub developer_seats: i32,
    #[sea_orm(column_name = "latestChangeAt")]
    pub latest_change_at: f64,
    pub mismatches: i32,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

impl ActiveModelBehavior for ActiveModel {}
