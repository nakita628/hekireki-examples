use super::runtime_environment_type::RuntimeEnvironmentType;
use super::webhook_delivery_status::WebhookDeliveryStatus;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "WebhookDelivery")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "friendlyId")]
    pub friendly_id: String,
    #[sea_orm(column_name = "webhookEndpointId")]
    pub webhook_endpoint_id: String,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: String,
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    #[sea_orm(column_name = "runtimeEnvironmentId")]
    pub runtime_environment_id: String,
    #[sea_orm(column_name = "environmentType")]
    pub environment_type: RuntimeEnvironmentType,
    #[sea_orm(column_name = "externalDeliveryId")]
    pub external_delivery_id: String,
    #[sea_orm(column_name = "idempotencyKey")]
    pub idempotency_key: String,
    #[sea_orm(column_name = "runId")]
    pub run_id: Option<String>,
    #[sea_orm(default_value = "PENDING")]
    pub status: WebhookDeliveryStatus,
    #[sea_orm(column_name = "isTest", default_value = false)]
    pub is_test: bool,
    #[sea_orm(column_name = "parsedEvent")]
    pub parsed_event: Option<Json>,
    pub headers: Option<Json>,
    #[sea_orm(column_name = "rawBodyHash")]
    pub raw_body_hash: Option<String>,
    #[sea_orm(column_name = "errorMessage")]
    pub error_message: Option<String>,
    #[sea_orm(column_name = "filterReason")]
    pub filter_reason: Option<String>,
    #[sea_orm(default_value = "[]")]
    pub targets: Json,
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "processedAt")]
    pub processed_at: Option<DateTime>,
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "retentionDays", default_value = 30)]
    pub retention_days: i32,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

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
