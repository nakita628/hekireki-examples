use super::webhook_delivery_channel::WebhookDeliveryChannel;
use super::webhook_delivery_outcome::WebhookDeliveryOutcome;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "WebhookEndpointDelivery")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(default_value = "platform")]
    pub channel: WebhookDeliveryChannel,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: Option<String>,
    #[sea_orm(column_name = "endpointId")]
    pub endpoint_id: Option<String>,
    #[sea_orm(column_name = "projectId")]
    pub project_id: Option<String>,
    #[sea_orm(column_name = "triggerId")]
    pub trigger_id: Option<String>,
    #[sea_orm(column_name = "dispatchId")]
    pub dispatch_id: String,
    pub attempt: Option<i32>,
    #[sea_orm(column_name = "eventCount")]
    pub event_count: Option<i32>,
    #[sea_orm(column_name = "responseStatus")]
    pub response_status: Option<i32>,
    #[sea_orm(column_name = "latencyMs")]
    pub latency_ms: Option<i32>,
    pub error: Option<String>,
    pub response: Option<Json>,
    pub outcome: WebhookDeliveryOutcome,
    #[sea_orm(column_name = "firedAt")]
    pub fired_at: DateTime,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::webhook_endpoint::Entity",
        from = "Column::EndpointId",
        to = "super::webhook_endpoint::Column::Id",
        on_delete = "Cascade"
    )]
    Endpoint,
    #[sea_orm(
        belongs_to = "super::project::Entity",
        from = "Column::ProjectId",
        to = "super::project::Column::Id",
        on_delete = "Cascade"
    )]
    Project,
    #[sea_orm(
        belongs_to = "super::trigger::Entity",
        from = "Column::TriggerId",
        to = "super::trigger::Column::Id",
        on_delete = "Cascade"
    )]
    Trigger,
}

impl Related<super::webhook_endpoint::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Endpoint.def()
    }
}

impl Related<super::project::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Project.def()
    }
}

impl Related<super::trigger::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Trigger.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.fired_at.is_not_set() {
            self.fired_at = Set(now.naive_utc());
        }
        if insert && self.created_at.is_not_set() {
            self.created_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
