use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "DelegationCredential")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "workspacePlatformId")]
    pub workspace_platform_id: i32,
    #[sea_orm(column_name = "serviceAccountKey")]
    pub service_account_key: Json,
    #[sea_orm(default_value = false)]
    pub enabled: bool,
    #[sea_orm(column_name = "lastEnabledAt")]
    pub last_enabled_at: Option<DateTime>,
    #[sea_orm(column_name = "lastDisabledAt")]
    pub last_disabled_at: Option<DateTime>,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: i32,
    pub domain: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::workspace_platform::Entity",
        from = "Column::WorkspacePlatformId",
        to = "super::workspace_platform::Column::Id",
        on_delete = "Cascade"
    )]
    WorkspacePlatform,
    #[sea_orm(
        belongs_to = "super::team::Entity",
        from = "Column::OrganizationId",
        to = "super::team::Column::Id",
        on_delete = "Cascade"
    )]
    Organization,
    #[sea_orm(has_many = "super::selected_calendar::Entity")]
    SelectedCalendars,
    #[sea_orm(has_many = "super::destination_calendar::Entity")]
    DestinationCalendar,
    #[sea_orm(has_many = "super::booking_reference::Entity")]
    BookingReferences,
    #[sea_orm(has_many = "super::credential::Entity")]
    Credentials,
}

impl Related<super::workspace_platform::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::WorkspacePlatform.def()
    }
}

impl Related<super::team::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Organization.def()
    }
}

impl Related<super::selected_calendar::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SelectedCalendars.def()
    }
}

impl Related<super::destination_calendar::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::DestinationCalendar.def()
    }
}

impl Related<super::booking_reference::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::BookingReferences.def()
    }
}

impl Related<super::credential::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Credentials.def()
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
