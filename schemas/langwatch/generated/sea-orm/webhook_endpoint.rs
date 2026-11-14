use super::webhook_destination_kind::WebhookDestinationKind;
use super::webhook_endpoint_status::WebhookEndpointStatus;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "WebhookEndpoint")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: String,
    #[sea_orm(column_name = "destinationKind", default_value = "http")]
    pub destination_kind: WebhookDestinationKind,
    pub url: Option<String>,
    #[sea_orm(column_name = "sqsQueueUrl")]
    pub sqs_queue_url: Option<String>,
    #[sea_orm(column_name = "sqsRoleArn")]
    pub sqs_role_arn: Option<String>,
    #[sea_orm(column_name = "sqsExternalId")]
    pub sqs_external_id: Option<String>,
    #[sea_orm(column_name = "sqsAccessKeyId")]
    pub sqs_access_key_id: Option<String>,
    #[sea_orm(column_name = "sqsSecretAccessKeyEncrypted")]
    pub sqs_secret_access_key_encrypted: Option<String>,
    #[sea_orm(column_name = "secretEncrypted")]
    pub secret_encrypted: String,
    #[sea_orm(column_name = "signatureScheme")]
    pub signature_scheme: Option<String>,
    #[sea_orm(column_name = "idempotencyKey")]
    pub idempotency_key: Option<String>,
    #[sea_orm(column_name = "previousSecretEncrypted")]
    pub previous_secret_encrypted: Option<String>,
    #[sea_orm(column_name = "previousSecretExpiresAt")]
    pub previous_secret_expires_at: Option<DateTime>,
    #[sea_orm(column_name = "enabledEvents")]
    pub enabled_events: Vec<String>,
    #[sea_orm(default_value = "ACTIVE")]
    pub status: WebhookEndpointStatus,
    #[sea_orm(column_name = "disabledReason")]
    pub disabled_reason: Option<String>,
    #[sea_orm(column_name = "disabledAt")]
    pub disabled_at: Option<DateTime>,
    #[sea_orm(column_name = "failingSince")]
    pub failing_since: Option<DateTime>,
    #[sea_orm(column_name = "maxBatchSize", default_value = 100)]
    pub max_batch_size: i32,
    #[sea_orm(column_name = "maxBatchDelayMs", default_value = 250)]
    pub max_batch_delay_ms: i32,
    #[sea_orm(column_name = "maxInFlight", default_value = 4)]
    pub max_in_flight: i32,
    #[sea_orm(column_name = "allowSelfSignedCertificate", default_value = false)]
    pub allow_self_signed_certificate: bool,
    #[sea_orm(column_name = "lastSuccessAt")]
    pub last_success_at: Option<DateTime>,
    #[sea_orm(column_name = "lastFailureAt")]
    pub last_failure_at: Option<DateTime>,
    #[sea_orm(column_name = "archivedAt")]
    pub archived_at: Option<DateTime>,
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
    #[sea_orm(has_many = "super::webhook_endpoint_delivery::Entity")]
    Deliveries,
}

impl Related<super::organization::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Organization.def()
    }
}

impl Related<super::webhook_endpoint_delivery::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Deliveries.def()
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
