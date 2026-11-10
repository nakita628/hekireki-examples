use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "PageViewSource")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub name: String,
    #[sea_orm(primary_key, auto_increment = false)]
    pub date: DateTime,
    #[sea_orm(column_name = "dailyStatsId")]
    pub daily_stats_id: Option<i32>,
    pub visitors: i32,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::daily_stats::Entity",
        from = "Column::DailyStatsId",
        to = "super::daily_stats::Column::Id"
    )]
    DailyStats,
}

impl Related<super::daily_stats::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::DailyStats.def()
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
