use super::slack_integration_kind::SlackIntegrationKind;
use super::slack_integration_scope_type::SlackIntegrationScopeType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "SlackIntegration")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub name: String,
    pub kind: SlackIntegrationKind,
    #[sea_orm(column_name = "scopeType")]
    pub scope_type: SlackIntegrationScopeType,
    #[sea_orm(column_name = "scopeId")]
    pub scope_id: String,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: String,
    #[sea_orm(column_name = "botTokenEncrypted")]
    pub bot_token_encrypted: Option<String>,
    #[sea_orm(column_name = "webhookUrlEncrypted")]
    pub webhook_url_encrypted: Option<String>,
    #[sea_orm(column_name = "secretFingerprint")]
    pub secret_fingerprint: String,
    #[sea_orm(column_name = "secretHint")]
    pub secret_hint: String,
    #[sea_orm(column_name = "slackTeamId")]
    pub slack_team_id: Option<String>,
    #[sea_orm(column_name = "slackTeamName")]
    pub slack_team_name: Option<String>,
    #[sea_orm(column_name = "createdById")]
    pub created_by_id: String,
    #[sea_orm(column_name = "updatedById")]
    pub updated_by_id: String,
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
