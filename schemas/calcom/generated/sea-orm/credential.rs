use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Credential")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    pub r#type: String,
    pub key: Json,
    #[sea_orm(column_name = "encryptedKey")]
    pub encrypted_key: Option<String>,
    #[sea_orm(column_name = "userId")]
    pub user_id: Option<i32>,
    #[sea_orm(column_name = "teamId")]
    pub team_id: Option<i32>,
    #[sea_orm(column_name = "appId")]
    pub app_id: Option<String>,
    #[sea_orm(column_name = "subscriptionId")]
    pub subscription_id: Option<String>,
    #[sea_orm(column_name = "paymentStatus")]
    pub payment_status: Option<String>,
    #[sea_orm(column_name = "billingCycleStart")]
    pub billing_cycle_start: Option<i32>,
    #[sea_orm(default_value = false)]
    pub invalid: Option<bool>,
    #[sea_orm(column_name = "delegationCredentialId")]
    pub delegation_credential_id: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::UserId",
        to = "super::user::Column::Id",
        on_delete = "Cascade"
    )]
    User,
    #[sea_orm(
        belongs_to = "super::team::Entity",
        from = "Column::TeamId",
        to = "super::team::Column::Id",
        on_delete = "Cascade"
    )]
    Team,
    #[sea_orm(
        belongs_to = "super::app::Entity",
        from = "Column::AppId",
        to = "super::app::Column::Slug",
        on_delete = "Cascade"
    )]
    App,
    #[sea_orm(
        belongs_to = "super::delegation_credential::Entity",
        from = "Column::DelegationCredentialId",
        to = "super::delegation_credential::Column::Id",
        on_delete = "Cascade"
    )]
    DelegationCredential,
    #[sea_orm(has_many = "super::destination_calendar::Entity")]
    DestinationCalendars,
    #[sea_orm(has_many = "super::selected_calendar::Entity")]
    SelectedCalendars,
    #[sea_orm(has_many = "super::calendar_cache::Entity")]
    CalendarCache,
    #[sea_orm(has_many = "super::booking_reference::Entity")]
    References,
    #[sea_orm(has_many = "super::integration_attribute_sync::Entity")]
    IntegrationAttributeSyncs,
    #[sea_orm(has_many = "super::host_location::Entity")]
    HostLocations,
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::User.def()
    }
}

impl Related<super::team::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Team.def()
    }
}

impl Related<super::app::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::App.def()
    }
}

impl Related<super::delegation_credential::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::DelegationCredential.def()
    }
}

impl Related<super::destination_calendar::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::DestinationCalendars.def()
    }
}

impl Related<super::selected_calendar::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SelectedCalendars.def()
    }
}

impl Related<super::calendar_cache::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CalendarCache.def()
    }
}

impl Related<super::booking_reference::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::References.def()
    }
}

impl Related<super::integration_attribute_sync::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::IntegrationAttributeSyncs.def()
    }
}

impl Related<super::host_location::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::HostLocations.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
