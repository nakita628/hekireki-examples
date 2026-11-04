use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "OutboundEmailAudit")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "expiresAt")]
    pub expires_at: DateTime,
    #[sea_orm(column_name = "contentExpiresAt")]
    pub content_expires_at: DateTime,
    pub environment: String,
    #[sea_orm(column_name = "emailType")]
    pub email_type: String,
    pub trigger: String,
    #[sea_orm(column_name = "actorKind")]
    pub actor_kind: String,
    #[sea_orm(column_name = "actorUserId")]
    pub actor_user_id: Option<String>,
    #[sea_orm(column_name = "configuredByUserId")]
    pub configured_by_user_id: Option<String>,
    #[sea_orm(column_name = "actorUserHash")]
    pub actor_user_hash: Option<String>,
    #[sea_orm(column_name = "configuredByUserHash")]
    pub configured_by_user_hash: Option<String>,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: Option<String>,
    #[sea_orm(column_name = "surveyId")]
    pub survey_id: Option<String>,
    #[sea_orm(column_name = "responseId")]
    pub response_id: Option<String>,
    #[sea_orm(column_name = "followUpId")]
    pub follow_up_id: Option<String>,
    #[sea_orm(column_name = "workflowId")]
    pub workflow_id: Option<String>,
    #[sea_orm(column_name = "workflowRunId")]
    pub workflow_run_id: Option<String>,
    #[sea_orm(column_name = "inviteId")]
    pub invite_id: Option<String>,
    #[sea_orm(column_name = "recipientMac")]
    pub recipient_mac: Option<String>,
    #[sea_orm(column_name = "sesMessageHash")]
    pub ses_message_hash: Option<String>,
    #[sea_orm(column_name = "snapshotKey")]
    pub snapshot_key: Option<String>,
    #[sea_orm(column_name = "snapshotSha256")]
    pub snapshot_sha256: Option<String>,
    #[sea_orm(default_value = "prepared")]
    pub status: String,
    #[sea_orm(column_name = "statusRecordedAt")]
    pub status_recorded_at: Option<DateTime>,
    #[sea_orm(column_name = "auditGap", default_value = false)]
    pub audit_gap: bool,
    #[sea_orm(column_name = "erasureRequestedAt")]
    pub erasure_requested_at: Option<DateTime>,
    #[sea_orm(column_name = "erasedAt")]
    pub erased_at: Option<DateTime>,
    #[sea_orm(column_name = "contentDeleteRetryAt")]
    pub content_delete_retry_at: Option<DateTime>,
    #[sea_orm(column_name = "contentDeleteFailures", default_value = 0)]
    pub content_delete_failures: i32,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

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
