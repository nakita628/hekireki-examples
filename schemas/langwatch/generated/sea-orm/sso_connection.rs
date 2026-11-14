use super::sso_connection_migration_phase::SsoConnectionMigrationPhase;
use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "SsoConnection")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: String,
    pub r#type: String,
    pub state: String,
    #[sea_orm(column_name = "claimedDomains")]
    pub claimed_domains: Vec<String>,
    #[sea_orm(column_name = "domainClaims", default_value = "[]")]
    pub domain_claims: Json,
    #[sea_orm(column_name = "approvedDomains")]
    pub approved_domains: Vec<String>,
    #[sea_orm(column_name = "verifiedDomains")]
    pub verified_domains: Vec<String>,
    #[sea_orm(column_name = "lapsedDomains")]
    pub lapsed_domains: Vec<String>,
    #[sea_orm(column_name = "domainVerifications", default_value = "[]")]
    pub domain_verifications: Json,
    #[sea_orm(column_name = "pendingVerification")]
    pub pending_verification: Option<Json>,
    #[sea_orm(column_name = "idpMetadata")]
    pub idp_metadata: Json,
    #[sea_orm(column_name = "arrivalPolicy", default_value = "refuse")]
    pub arrival_policy: String,
    #[sea_orm(column_name = "allowsJit", default_value = false)]
    pub allows_jit: bool,
    #[sea_orm(column_name = "arrivalPolicyDecidedAt")]
    pub arrival_policy_decided_at: Option<DateTime>,
    pub source: String,
    #[sea_orm(column_name = "testLoginAccountId")]
    pub test_login_account_id: Option<String>,
    #[sea_orm(column_name = "replacesConnectionId")]
    pub replaces_connection_id: Option<String>,
    #[sea_orm(column_name = "migrationPhase")]
    pub migration_phase: Option<SsoConnectionMigrationPhase>,
    #[sea_orm(column_name = "graceStartedAt")]
    pub grace_started_at: Option<DateTime>,
    #[sea_orm(column_name = "routeChangedAt")]
    pub route_changed_at: Option<DateTime>,
    #[sea_orm(column_name = "finalizationRequestedAt")]
    pub finalization_requested_at: Option<DateTime>,
    #[sea_orm(column_name = "finalizedAt")]
    pub finalized_at: Option<DateTime>,
    pub rejection: Option<Json>,
    #[sea_orm(column_name = "createdBy")]
    pub created_by: Option<String>,
    #[sea_orm(column_name = "tearDownAfter")]
    pub tear_down_after: Option<DateTime>,
    #[sea_orm(column_name = "occurredAt")]
    pub occurred_at: DateTime,
    #[sea_orm(column_name = "lastEventId")]
    pub last_event_id: String,
    #[sea_orm(column_name = "acceptedAt")]
    pub accepted_at: DateTime,
    #[sea_orm(column_name = "projectionVersion")]
    pub projection_version: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_one = "super::sso_connection_reproof_cursor::Entity")]
    ReproofCursor,
}

impl Related<super::sso_connection_reproof_cursor::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ReproofCursor.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
