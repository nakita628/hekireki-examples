use super::gateway_provider_health_status::GatewayProviderHealthStatus;
use super::gateway_provider_rotation_policy::GatewayProviderRotationPolicy;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "ModelProvider")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub name: String,
    pub provider: String,
    #[sea_orm(column_name = "routingHandle")]
    pub routing_handle: Option<String>,
    pub enabled: bool,
    #[sea_orm(column_name = "customKeys")]
    pub custom_keys: Option<Json>,
    #[sea_orm(column_name = "extraHeaders")]
    pub extra_headers: Option<Json>,
    #[sea_orm(column_name = "customModels")]
    pub custom_models: Option<Json>,
    #[sea_orm(column_name = "customEmbeddingsModels")]
    pub custom_embeddings_models: Option<Json>,
    #[sea_orm(column_name = "deploymentMapping")]
    pub deployment_mapping: Option<Json>,
    #[sea_orm(column_name = "rateLimitRpm")]
    pub rate_limit_rpm: Option<i32>,
    #[sea_orm(column_name = "rateLimitTpm")]
    pub rate_limit_tpm: Option<i32>,
    #[sea_orm(column_name = "rateLimitRpd")]
    pub rate_limit_rpd: Option<i32>,
    #[sea_orm(column_name = "rotationPolicy", default_value = "MANUAL")]
    pub rotation_policy: GatewayProviderRotationPolicy,
    #[sea_orm(column_name = "providerConfig")]
    pub provider_config: Option<Json>,
    #[sea_orm(column_name = "fallbackPriorityGlobal")]
    pub fallback_priority_global: Option<i32>,
    #[sea_orm(column_name = "langySkipPermissionsModels")]
    pub langy_skip_permissions_models: Option<Json>,
    #[sea_orm(column_name = "healthStatus", default_value = "UNKNOWN")]
    pub health_status: GatewayProviderHealthStatus,
    #[sea_orm(column_name = "circuitOpenedAt")]
    pub circuit_opened_at: Option<DateTime>,
    #[sea_orm(column_name = "lastHealthCheckAt")]
    pub last_health_check_at: Option<DateTime>,
    #[sea_orm(column_name = "disabledAt")]
    pub disabled_at: Option<DateTime>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::model_provider_scope::Entity")]
    Scopes,
}

impl Related<super::model_provider_scope::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Scopes.def()
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
