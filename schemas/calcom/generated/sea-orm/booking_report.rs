use super::booking_report_reason::BookingReportReason;
use super::booking_report_status::BookingReportStatus;
use super::system_report_status::SystemReportStatus;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "BookingReport")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_type = "Uuid")]
    pub id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "bookingUid")]
    pub booking_uid: String,
    #[sea_orm(column_name = "bookerEmail")]
    pub booker_email: String,
    #[sea_orm(column_name = "reportedById")]
    pub reported_by_id: Option<i32>,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: Option<i32>,
    pub reason: BookingReportReason,
    pub description: Option<String>,
    #[sea_orm(default_value = false)]
    pub cancelled: bool,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(default_value = "PENDING")]
    pub status: BookingReportStatus,
    #[sea_orm(column_name = "systemStatus", default_value = "PENDING")]
    pub system_status: SystemReportStatus,
    #[sea_orm(column_name = "watchlistId", column_type = "Uuid")]
    pub watchlist_id: Option<String>,
    #[sea_orm(column_name = "globalWatchlistId", column_type = "Uuid")]
    pub global_watchlist_id: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::booking::Entity",
        from = "Column::BookingUid",
        to = "super::booking::Column::Uid",
        on_delete = "Cascade"
    )]
    Booking,
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::ReportedById",
        to = "super::user::Column::Id",
        on_delete = "SetNull"
    )]
    ReportedBy,
    #[sea_orm(
        belongs_to = "super::team::Entity",
        from = "Column::OrganizationId",
        to = "super::team::Column::Id",
        on_delete = "Cascade"
    )]
    Organization,
    #[sea_orm(
        belongs_to = "super::watchlist::Entity",
        from = "Column::WatchlistId",
        to = "super::watchlist::Column::Id",
        on_delete = "SetNull"
    )]
    Watchlist,
    #[sea_orm(
        belongs_to = "super::watchlist::Entity",
        from = "Column::GlobalWatchlistId",
        to = "super::watchlist::Column::Id",
        on_delete = "SetNull"
    )]
    GlobalWatchlist,
}

impl Related<super::booking::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Booking.def()
    }
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ReportedBy.def()
    }
}

impl Related<super::team::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Organization.def()
    }
}

impl Related<super::watchlist::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Watchlist.def()
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
        if insert && self.created_at.is_not_set() {
            self.created_at = Set(now.naive_utc());
        }
        if !self.updated_at.is_set() {
            self.updated_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
