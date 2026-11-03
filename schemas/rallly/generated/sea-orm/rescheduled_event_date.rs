use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "rescheduled_event_dates")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub scheduled_event_id: String,
    pub start: DateTime,
    pub end: DateTime,
    #[sea_orm(default_value = false)]
    pub all_day: bool,
    pub created_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::scheduled_event::Entity",
        from = "Column::ScheduledEventId",
        to = "super::scheduled_event::Column::Id",
        on_delete = "Cascade"
    )]
    ScheduledEvent,
}

impl Related<super::scheduled_event::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ScheduledEvent.def()
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
        Ok(self)
    }
}
