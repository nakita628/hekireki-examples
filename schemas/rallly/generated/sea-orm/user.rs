use super::time_format::TimeFormat;
use super::user_role::UserRole;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "users")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub name: String,
    #[sea_orm(unique)]
    pub email: String,
    pub email_verified: Option<bool>,
    pub image: Option<String>,
    pub time_zone: Option<String>,
    pub week_start: Option<i32>,
    pub time_format: Option<TimeFormat>,
    pub locale: Option<String>,
    pub created_at: DateTime,
    pub updated_at: Option<DateTime>,
    pub customer_id: Option<String>,
    #[sea_orm(default_value = false)]
    pub banned: bool,
    pub banned_at: Option<DateTime>,
    pub ban_reason: Option<String>,
    pub ban_expires: Option<DateTime>,
    #[sea_orm(default_value = "user")]
    pub role: UserRole,
    pub last_login_method: Option<String>,
    #[sea_orm(column_name = "anonymous", default_value = false)]
    pub is_anonymous: bool,
    pub last_seen_at: DateTime,
    pub deleted_at: Option<DateTime>,
    pub job_title: Option<String>,
    pub review_requested_at: Option<DateTime>,
    pub default_destination_calendar_id: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::provider_calendar::Entity",
        from = "Column::DefaultDestinationCalendarId",
        to = "super::provider_calendar::Column::Id",
        on_delete = "SetNull"
    )]
    DefaultDestinationCalendar,
    #[sea_orm(has_many = "super::comment::Entity")]
    Comments,
    #[sea_orm(has_many = "super::poll::Entity")]
    Polls,
    #[sea_orm(has_many = "super::queued_email::Entity")]
    QueuedEmails,
    #[sea_orm(has_many = "super::account::Entity")]
    Accounts,
    #[sea_orm(has_many = "super::participant::Entity")]
    Participants,
    #[sea_orm(has_many = "super::payment_method::Entity")]
    PaymentMethods,
    #[sea_orm(has_many = "super::space_member_invite::Entity")]
    SpaceMemberInvites,
    #[sea_orm(has_many = "super::nonprofit_application::Entity")]
    NonprofitApplications,
    #[sea_orm(has_many = "super::subscription::Entity")]
    Subscriptions,
    #[sea_orm(has_many = "super::space::Entity")]
    Spaces,
    #[sea_orm(has_many = "super::space_member::Entity")]
    MemberOf,
    #[sea_orm(has_many = "super::scheduled_event::Entity")]
    ScheduledEvents,
    #[sea_orm(has_many = "super::scheduled_event_invite::Entity")]
    ScheduledEventInvites,
    #[sea_orm(has_many = "super::event_type::Entity")]
    HostedEventTypes,
    #[sea_orm(has_many = "super::sheet::Entity")]
    HostedSheets,
    #[sea_orm(has_many = "super::calendar_connection::Entity")]
    CalendarConnections,
    #[sea_orm(has_many = "super::conferencing_connection::Entity")]
    ConferencingConnections,
    #[sea_orm(has_many = "super::credential::Entity")]
    Credentials,
    #[sea_orm(
        has_many = "super::session::Entity",
        from = "Column::Id",
        to = "super::session::Column::UserId"
    )]
    Sessions,
    #[sea_orm(
        has_many = "super::session::Entity",
        from = "Column::Id",
        to = "super::session::Column::ImpersonatedBy"
    )]
    ImpersonatedSessions,
    #[sea_orm(has_one = "super::user_notification_preferences::Entity")]
    NotificationPreferences,
}

impl Related<super::provider_calendar::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::DefaultDestinationCalendar.def()
    }
}

impl Related<super::comment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Comments.def()
    }
}

impl Related<super::poll::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Polls.def()
    }
}

impl Related<super::queued_email::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::QueuedEmails.def()
    }
}

impl Related<super::account::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Accounts.def()
    }
}

impl Related<super::participant::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Participants.def()
    }
}

impl Related<super::payment_method::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::PaymentMethods.def()
    }
}

impl Related<super::space_member_invite::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SpaceMemberInvites.def()
    }
}

impl Related<super::nonprofit_application::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::NonprofitApplications.def()
    }
}

impl Related<super::subscription::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Subscriptions.def()
    }
}

impl Related<super::space::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Spaces.def()
    }
}

impl Related<super::space_member::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::MemberOf.def()
    }
}

impl Related<super::scheduled_event::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ScheduledEvents.def()
    }
}

impl Related<super::scheduled_event_invite::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ScheduledEventInvites.def()
    }
}

impl Related<super::event_type::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::HostedEventTypes.def()
    }
}

impl Related<super::sheet::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::HostedSheets.def()
    }
}

impl Related<super::calendar_connection::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CalendarConnections.def()
    }
}

impl Related<super::conferencing_connection::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ConferencingConnections.def()
    }
}

impl Related<super::credential::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Credentials.def()
    }
}

impl Related<super::session::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Sessions.def()
    }
}

impl Related<super::user_notification_preferences::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::NotificationPreferences.def()
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
            self.updated_at = Set(Some(now.naive_utc()));
        }
        if insert && self.last_seen_at.is_not_set() {
            self.last_seen_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
