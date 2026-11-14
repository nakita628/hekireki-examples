use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "OrganizationSeatPeakMonth")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: String,
    #[sea_orm(primary_key, auto_increment = false)]
    pub month: String,
    #[sea_orm(column_name = "fullPeak")]
    pub full_peak: i32,
    #[sea_orm(column_name = "fullPeakAt")]
    pub full_peak_at: f64,
    #[sea_orm(column_name = "litePeak")]
    pub lite_peak: i32,
    #[sea_orm(column_name = "litePeakAt")]
    pub lite_peak_at: f64,
    #[sea_orm(column_name = "fullAtEnd")]
    pub full_at_end: i32,
    #[sea_orm(column_name = "liteAtEnd")]
    pub lite_at_end: i32,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

impl ActiveModelBehavior for ActiveModel {}
