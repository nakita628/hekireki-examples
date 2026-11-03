use super::runtime_environment_type::RuntimeEnvironmentType;
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
    #[sea_orm(unique)]
    #[sea_orm(column_name = "friendlyId")]
    pub friendly_id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "opaqueId")]
    pub opaque_id: String,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: String,
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    #[sea_orm(column_name = "runtimeEnvironmentId")]
    pub runtime_environment_id: String,
    #[sea_orm(column_name = "environmentType")]
    pub environment_type: RuntimeEnvironmentType,
    #[sea_orm(column_name = "endpointTenantId", default_value = "")]
    pub endpoint_tenant_id: String,
    #[sea_orm(column_name = "endpointExternalRef", default_value = "")]
    pub endpoint_external_ref: String,
    pub source: String,
    #[sea_orm(column_name = "declaredId")]
    pub declared_id: String,
    #[sea_orm(column_name = "routingTargets", default_value = "[]")]
    pub routing_targets: Json,
    #[sea_orm(column_name = "verifierArtifact")]
    pub verifier_artifact: Json,
    #[sea_orm(default_value = "{}")]
    pub metadata: Json,
    #[sea_orm(column_name = "secretProvisioning", default_value = "either")]
    pub secret_provisioning: String,
    #[sea_orm(column_name = "setupPrompt")]
    pub setup_prompt: Option<String>,
    #[sea_orm(column_name = "signingSecretKey")]
    pub signing_secret_key: Option<String>,
    #[sea_orm(default_value = "ACTIVE")]
    pub status: WebhookEndpointStatus,
    #[sea_orm(column_name = "manuallyDeactivatedAt")]
    pub manually_deactivated_at: Option<DateTime>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
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
