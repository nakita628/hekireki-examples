use super::project_type::ProjectType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "projects")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub name: String,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: String,
    #[sea_orm(column_name = "eventsCount", default_value = 0)]
    pub events_count: i32,
    pub types: Vec<ProjectType>,
    pub domain: Option<String>,
    pub cors: Vec<String>,
    #[sea_orm(column_name = "crossDomain", default_value = false)]
    pub cross_domain: bool,
    #[sea_orm(column_name = "allowUnsafeRevenueTracking", default_value = false)]
    pub allow_unsafe_revenue_tracking: bool,
    #[sea_orm(column_name = "aiStoreContent", default_value = true)]
    pub ai_store_content: bool,
    #[sea_orm(default_value = "[]")]
    pub filters: Json,
    #[sea_orm(column_name = "firstEventAt")]
    pub first_event_at: Option<DateTime>,
    #[sea_orm(column_name = "noDataNotifiedAt")]
    pub no_data_notified_at: Option<DateTime>,
    #[sea_orm(column_name = "dataStoppedNotifiedAt")]
    pub data_stopped_notified_at: Option<DateTime>,
    #[sea_orm(column_name = "deleteAt")]
    pub delete_at: Option<DateTime>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::organization::Entity",
        from = "Column::OrganizationId",
        to = "super::organization::Column::Id",
        on_delete = "Cascade"
    )]
    Organization,
    #[sea_orm(has_many = "super::client::Entity")]
    Clients,
    #[sea_orm(has_many = "super::report::Entity")]
    Reports,
    #[sea_orm(has_many = "super::dashboard::Entity")]
    Dashboards,
    #[sea_orm(has_many = "super::share_dashboard::Entity")]
    ShareDashboards,
    #[sea_orm(has_many = "super::share_report::Entity")]
    ShareReports,
    #[sea_orm(has_many = "super::share_widget::Entity")]
    ShareWidgets,
    #[sea_orm(has_many = "super::event_meta::Entity")]
    Meta,
    #[sea_orm(has_many = "super::custom_event::Entity")]
    CustomEvents,
    #[sea_orm(has_many = "super::reference::Entity")]
    References,
    #[sea_orm(has_many = "super::member_project::Entity")]
    MemberProjects,
    #[sea_orm(has_many = "super::notification_rule::Entity")]
    NotificationRules,
    #[sea_orm(has_many = "super::notification::Entity")]
    Notifications,
    #[sea_orm(has_many = "super::import::Entity")]
    Imports,
    #[sea_orm(has_many = "super::cohort::Entity")]
    Cohorts,
    #[sea_orm(has_many = "super::export_watermark::Entity")]
    ExportWatermarks,
    #[sea_orm(has_many = "super::integration::Entity")]
    Integrations,
    #[sea_orm(has_many = "super::conversation::Entity")]
    Conversations,
    #[sea_orm(has_one = "super::share_overview::Entity")]
    Share,
    #[sea_orm(has_one = "super::gsc_connection::Entity")]
    GscConnection,
}

impl Related<super::organization::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Organization.def()
    }
}

impl Related<super::client::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Clients.def()
    }
}

impl Related<super::report::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Reports.def()
    }
}

impl Related<super::dashboard::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Dashboards.def()
    }
}

impl Related<super::share_dashboard::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ShareDashboards.def()
    }
}

impl Related<super::share_report::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ShareReports.def()
    }
}

impl Related<super::share_widget::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ShareWidgets.def()
    }
}

impl Related<super::event_meta::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Meta.def()
    }
}

impl Related<super::custom_event::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CustomEvents.def()
    }
}

impl Related<super::reference::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::References.def()
    }
}

impl Related<super::member_project::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::MemberProjects.def()
    }
}

impl Related<super::notification_rule::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::NotificationRules.def()
    }
}

impl Related<super::notification::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Notifications.def()
    }
}

impl Related<super::import::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Imports.def()
    }
}

impl Related<super::cohort::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Cohorts.def()
    }
}

impl Related<super::export_watermark::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ExportWatermarks.def()
    }
}

impl Related<super::integration::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Integrations.def()
    }
}

impl Related<super::conversation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Conversations.def()
    }
}

impl Related<super::share_overview::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Share.def()
    }
}

impl Related<super::gsc_connection::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::GscConnection.def()
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
