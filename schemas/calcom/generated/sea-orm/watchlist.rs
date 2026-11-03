use super::watchlist_action::WatchlistAction;
use super::watchlist_source::WatchlistSource;
use super::watchlist_type::WatchlistType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Watchlist")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_type = "Uuid")]
    pub id: String,
    pub r#type: WatchlistType,
    pub value: String,
    pub description: Option<String>,
    #[sea_orm(column_name = "isGlobal", default_value = false)]
    pub is_global: bool,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: Option<i32>,
    #[sea_orm(default_value = "REPORT")]
    pub action: WatchlistAction,
    #[sea_orm(default_value = "MANUAL")]
    pub source: WatchlistSource,
    #[sea_orm(column_name = "lastUpdatedAt")]
    pub last_updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        has_many = "super::booking_report::Entity",
        from = "Column::Id",
        to = "super::booking_report::Column::WatchlistId"
    )]
    BookingReports,
    #[sea_orm(
        has_many = "super::booking_report::Entity",
        from = "Column::Id",
        to = "super::booking_report::Column::GlobalWatchlistId"
    )]
    GlobalBookingReports,
    #[sea_orm(has_many = "super::watchlist_audit::Entity")]
    Audits,
}

impl Related<super::booking_report::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::BookingReports.def()
    }
}

impl Related<super::watchlist_audit::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Audits.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    fn new() -> Self {
        Self {
            id: Set(uuid::Uuid::new_v4().to_string()),
            ..ActiveModelTrait::default()
        }
    }

    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.last_updated_at.is_not_set() {
            self.last_updated_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
