use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "organizations")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub name: String,
    pub created_at: DateTime,
    pub updated_at: DateTime,
    pub cloud_config: Option<Json>,
    pub metadata: Option<Json>,
    pub cloud_billing_cycle_anchor: Option<DateTime>,
    pub cloud_billing_cycle_updated_at: Option<DateTime>,
    pub cloud_current_cycle_usage: Option<i32>,
    pub cloud_free_tier_usage_threshold_state: Option<String>,
    #[sea_orm(default_value = false)]
    pub ai_features_enabled: bool,
    #[sea_orm(default_value = true)]
    pub ai_telemetry_enabled: bool,
    #[sea_orm(default_value = false)]
    pub access_audit_log_enabled: bool,
    pub feature_flag_org_defaults: Vec<String>,
    pub sfdc_org_id: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::organization_membership::Entity")]
    OrganizationMemberships,
    #[sea_orm(has_many = "super::project::Entity")]
    Projects,
    #[sea_orm(has_many = "super::membership_invitation::Entity")]
    MembershipInvitation,
    #[sea_orm(has_many = "super::api_key::Entity")]
    ApiKey,
    #[sea_orm(has_many = "super::survey::Entity")]
    Surveys,
    #[sea_orm(has_many = "super::cloud_spend_alert::Entity")]
    CloudSpendAlerts,
    #[sea_orm(has_many = "super::verified_domain::Entity")]
    VerifiedDomains,
    #[sea_orm(has_many = "super::gateway_ai_connection::Entity")]
    GatewayAiConnections,
    #[sea_orm(has_many = "super::llm_api_keys::Entity")]
    OrganizationLlmApiKeys,
    #[sea_orm(has_many = "super::role_assignment::Entity")]
    RoleAssignments,
    #[sea_orm(has_one = "super::gateway_config::Entity")]
    GatewayConfig,
}

impl Related<super::organization_membership::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OrganizationMemberships.def()
    }
}

impl Related<super::project::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Projects.def()
    }
}

impl Related<super::membership_invitation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::MembershipInvitation.def()
    }
}

impl Related<super::api_key::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ApiKey.def()
    }
}

impl Related<super::survey::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Surveys.def()
    }
}

impl Related<super::cloud_spend_alert::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CloudSpendAlerts.def()
    }
}

impl Related<super::verified_domain::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::VerifiedDomains.def()
    }
}

impl Related<super::gateway_ai_connection::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::GatewayAiConnections.def()
    }
}

impl Related<super::llm_api_keys::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OrganizationLlmApiKeys.def()
    }
}

impl Related<super::role_assignment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RoleAssignments.def()
    }
}

impl Related<super::gateway_config::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::GatewayConfig.def()
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
        if insert && self.cloud_billing_cycle_anchor.is_not_set() {
            self.cloud_billing_cycle_anchor = Set(Some(now.naive_utc()));
        }
        Ok(self)
    }
}
