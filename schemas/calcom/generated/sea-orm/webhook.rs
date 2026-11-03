use super::time_unit::TimeUnit;
use super::webhook_trigger_events::WebhookTriggerEvents;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Webhook")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "userId")]
    pub user_id: Option<i32>,
    #[sea_orm(column_name = "teamId")]
    pub team_id: Option<i32>,
    #[sea_orm(column_name = "eventTypeId")]
    pub event_type_id: Option<i32>,
    #[sea_orm(column_name = "platformOAuthClientId")]
    pub platform_oauth_client_id: Option<String>,
    #[sea_orm(column_name = "subscriberUrl")]
    pub subscriber_url: String,
    #[sea_orm(column_name = "payloadTemplate")]
    pub payload_template: Option<String>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(default_value = true)]
    pub active: bool,
    #[sea_orm(column_name = "eventTriggers")]
    pub event_triggers: Vec<WebhookTriggerEvents>,
    #[sea_orm(column_name = "appId")]
    pub app_id: Option<String>,
    pub secret: Option<String>,
    #[sea_orm(default_value = false)]
    pub platform: bool,
    pub time: Option<i32>,
    #[sea_orm(column_name = "timeUnit")]
    pub time_unit: Option<TimeUnit>,
    #[sea_orm(default_value = "2021-10-20")]
    pub version: String,
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
        belongs_to = "super::event_type::Entity",
        from = "Column::EventTypeId",
        to = "super::event_type::Column::Id",
        on_delete = "Cascade"
    )]
    EventType,
    #[sea_orm(
        belongs_to = "super::platform_oauth_client::Entity",
        from = "Column::PlatformOauthClientId",
        to = "super::platform_oauth_client::Column::Id",
        on_delete = "Cascade"
    )]
    PlatformOAuthClient,
    #[sea_orm(
        belongs_to = "super::app::Entity",
        from = "Column::AppId",
        to = "super::app::Column::Slug",
        on_delete = "Cascade"
    )]
    App,
    #[sea_orm(has_many = "super::webhook_scheduled_triggers::Entity")]
    ScheduledTriggers,
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

impl Related<super::event_type::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EventType.def()
    }
}

impl Related<super::platform_oauth_client::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::PlatformOAuthClient.def()
    }
}

impl Related<super::app::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::App.def()
    }
}

impl Related<super::webhook_scheduled_triggers::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ScheduledTriggers.def()
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
