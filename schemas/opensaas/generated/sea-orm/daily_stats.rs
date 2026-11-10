use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "DailyStats")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    #[sea_orm(unique)]
    pub date: DateTime,
    #[sea_orm(column_name = "totalViews", default_value = 0)]
    pub total_views: i32,
    #[sea_orm(column_name = "prevDayViewsChangePercent", default_value = "0")]
    pub prev_day_views_change_percent: String,
    #[sea_orm(column_name = "userCount", default_value = 0)]
    pub user_count: i32,
    #[sea_orm(column_name = "paidUserCount", default_value = 0)]
    pub paid_user_count: i32,
    #[sea_orm(column_name = "userDelta", default_value = 0)]
    pub user_delta: i32,
    #[sea_orm(column_name = "paidUserDelta", default_value = 0)]
    pub paid_user_delta: i32,
    #[sea_orm(column_name = "totalRevenue", default_value = 0)]
    pub total_revenue: f64,
    #[sea_orm(column_name = "totalProfit", default_value = 0)]
    pub total_profit: f64,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::page_view_source::Entity")]
    Sources,
}

impl Related<super::page_view_source::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Sources.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.date.is_not_set() {
            self.date = Set(now.naive_utc());
        }
        Ok(self)
    }
}
