use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "report_layouts")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_type = "Uuid")]
    pub id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "reportId", column_type = "Uuid")]
    pub report_id: String,
    #[sea_orm(column_name = "gridColumns", default_value = 12)]
    pub grid_columns: i32,
    #[sea_orm(default_value = 0)]
    pub x: i32,
    #[sea_orm(default_value = 0)]
    pub y: i32,
    #[sea_orm(default_value = 8)]
    pub w: i32,
    #[sea_orm(default_value = 6)]
    pub h: i32,
    #[sea_orm(column_name = "minW", default_value = 4)]
    pub min_w: Option<i32>,
    #[sea_orm(column_name = "minH", default_value = 4)]
    pub min_h: Option<i32>,
    #[sea_orm(column_name = "maxW")]
    pub max_w: Option<i32>,
    #[sea_orm(column_name = "maxH")]
    pub max_h: Option<i32>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::report::Entity",
        from = "Column::ReportId",
        to = "super::report::Column::Id",
        on_delete = "Cascade"
    )]
    Report,
}

impl Related<super::report::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Report.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.created_at.is_not_set() {
            self.created_at = Set(now.naive_utc());
        }
        if !self.updated_at.is_set() {
            self.updated_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
