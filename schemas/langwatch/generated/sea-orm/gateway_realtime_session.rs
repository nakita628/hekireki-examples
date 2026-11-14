use super::gateway_realtime_session_status::GatewayRealtimeSessionStatus;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "GatewayRealtimeSession")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: String,
    #[sea_orm(column_name = "virtualKeyId")]
    pub virtual_key_id: String,
    #[sea_orm(column_name = "modelProviderId")]
    pub model_provider_id: String,
    pub vendor: String,
    #[sea_orm(column_name = "agentId")]
    pub agent_id: Option<String>,
    pub model: String,
    #[sea_orm(column_name = "requestedModel")]
    pub requested_model: Option<String>,
    #[sea_orm(column_name = "vendorConversationId")]
    pub vendor_conversation_id: Option<String>,
    #[sea_orm(default_value = "OPEN")]
    pub status: GatewayRealtimeSessionStatus,
    #[sea_orm(column_name = "mintedAt")]
    pub minted_at: DateTime,
    #[sea_orm(column_name = "closedAt")]
    pub closed_at: Option<DateTime>,
    #[sea_orm(column_name = "closeReason")]
    pub close_reason: Option<String>,
    #[sea_orm(column_name = "traceId")]
    pub trace_id: Option<String>,
    #[sea_orm(column_name = "vendorCostRaw")]
    pub vendor_cost_raw: Option<Json>,
    pub kind: Option<String>,
    pub metering: Option<String>,
    #[sea_orm(column_name = "credentialExpiresAt")]
    pub credential_expires_at: Option<DateTime>,
    #[sea_orm(column_name = "transcriptionModel")]
    pub transcription_model: Option<String>,
    #[sea_orm(column_name = "endUserId")]
    pub end_user_id: Option<String>,
    #[sea_orm(column_name = "lastReportAt")]
    pub last_report_at: Option<DateTime>,
    #[sea_orm(column_name = "reportedCostNanoUsd", default_value = "0")]
    pub reported_cost_nano_usd: i64,
    #[sea_orm(column_name = "reportCount", default_value = 0)]
    pub report_count: i32,
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
        if insert && self.minted_at.is_not_set() {
            self.minted_at = Set(now.naive_utc());
        }
        if insert && self.created_at.is_not_set() {
            self.created_at = Set(now.naive_utc());
        }
        if !self.updated_at.is_set() {
            self.updated_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
