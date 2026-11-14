use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "User")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub name: Option<String>,
    #[sea_orm(unique)]
    pub email: Option<String>,
    #[sea_orm(column_name = "emailVerified", default_value = false)]
    pub email_verified: bool,
    #[sea_orm(column_name = "signupConfirmationPending", default_value = false)]
    pub signup_confirmation_pending: bool,
    #[sea_orm(column_name = "passkeySignupClaimHash")]
    pub passkey_signup_claim_hash: Option<String>,
    #[sea_orm(column_type = "Text")]
    pub image: Option<String>,
    #[sea_orm(column_name = "pendingSsoSetup", default_value = false)]
    pub pending_sso_setup: bool,
    #[sea_orm(column_name = "userHashKey")]
    pub user_hash_key: Option<String>,
    #[sea_orm(column_name = "twoFactorEnabled", default_value = false)]
    pub two_factor_enabled: bool,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "lastLoginAt")]
    pub last_login_at: Option<DateTime>,
    #[sea_orm(column_name = "deactivatedAt")]
    pub deactivated_at: Option<DateTime>,
    #[sea_orm(column_name = "lastHomePath")]
    pub last_home_path: Option<String>,
    #[sea_orm(column_name = "tracesExplorerTourDismissedAt")]
    pub traces_explorer_tour_dismissed_at: Option<DateTime>,
    #[sea_orm(column_name = "langyCodeAccessPreference")]
    pub langy_code_access_preference: Option<String>,
    #[sea_orm(column_name = "passkeyNudgeDismissedAt")]
    pub passkey_nudge_dismissed_at: Option<DateTime>,
    #[sea_orm(column_name = "joinOfferDismissedDomains")]
    pub join_offer_dismissed_domains: Vec<String>,
    #[sea_orm(column_name = "notificationPreferences", default_value = "{}")]
    pub notification_preferences: Json,
    #[sea_orm(column_name = "passwordResetRequiredAt")]
    pub password_reset_required_at: Option<DateTime>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::account::Entity")]
    Accounts,
    #[sea_orm(has_many = "super::account_credential::Entity")]
    AccountCredentials,
    #[sea_orm(has_many = "super::session::Entity")]
    Sessions,
    #[sea_orm(has_many = "super::two_factor::Entity")]
    TwoFactors,
    #[sea_orm(has_many = "super::passkey::Entity")]
    Passkeys,
    #[sea_orm(has_many = "super::team_user::Entity")]
    TeamMemberships,
    #[sea_orm(has_many = "super::organization_user::Entity")]
    OrgMemberships,
    #[sea_orm(has_many = "super::scim_user_resource::Entity")]
    ScimUserResources,
    #[sea_orm(has_many = "super::annotation::Entity")]
    Annotation,
    #[sea_orm(has_many = "super::share_link::Entity")]
    ShareLinks,
    #[sea_orm(has_many = "super::workflow::Entity")]
    Workflow,
    #[sea_orm(has_many = "super::workflow_version::Entity")]
    WorkflowVersion,
    #[sea_orm(has_many = "super::annotation_queue_members::Entity")]
    AnnotationQueues,
    #[sea_orm(
        has_many = "super::annotation_queue_item::Entity",
        from = "Column::Id",
        to = "super::annotation_queue_item::Column::UserId"
    )]
    AssignedQueueItems,
    #[sea_orm(
        has_many = "super::annotation_queue_item::Entity",
        from = "Column::Id",
        to = "super::annotation_queue_item::Column::CreatedByUserId"
    )]
    CreatedQueueItems,
    #[sea_orm(has_many = "super::llm_prompt_config_version::Entity")]
    LlmPromptConfigVersions,
    #[sea_orm(has_many = "super::scenario::Entity")]
    Scenarios,
    #[sea_orm(has_many = "super::organization_invite::Entity")]
    InviteRequests,
    #[sea_orm(
        has_many = "super::project_secret::Entity",
        from = "Column::Id",
        to = "super::project_secret::Column::CreatedById"
    )]
    SecretsCreated,
    #[sea_orm(
        has_many = "super::project_secret::Entity",
        from = "Column::Id",
        to = "super::project_secret::Column::UpdatedById"
    )]
    SecretsUpdated,
    #[sea_orm(has_many = "super::saved_view::Entity")]
    SavedViews,
    #[sea_orm(
        has_many = "super::prompt_tag_assignment::Entity",
        from = "Column::Id",
        to = "super::prompt_tag_assignment::Column::CreatedById"
    )]
    VersionTagsCreated,
    #[sea_orm(
        has_many = "super::prompt_tag_assignment::Entity",
        from = "Column::Id",
        to = "super::prompt_tag_assignment::Column::UpdatedById"
    )]
    VersionTagsUpdated,
    #[sea_orm(
        has_many = "super::prompt_tag::Entity",
        from = "Column::Id",
        to = "super::prompt_tag::Column::CreatedById"
    )]
    PromptTagsCreated,
    #[sea_orm(
        has_many = "super::prompt_tag::Entity",
        from = "Column::Id",
        to = "super::prompt_tag::Column::UpdatedById"
    )]
    PromptTagsUpdated,
    #[sea_orm(has_many = "super::group_membership::Entity")]
    GroupMemberships,
    #[sea_orm(has_many = "super::role_binding::Entity")]
    RoleBindings,
    #[sea_orm(has_many = "super::scim_external_id::Entity")]
    ScimExternalIds,
    #[sea_orm(has_many = "super::api_key::Entity")]
    ApiKeys,
    #[sea_orm(has_many = "super::gateway_budget::Entity")]
    GatewayBudgetsCreated,
    #[sea_orm(has_many = "super::virtual_key::Entity")]
    PrincipalVirtualKeys,
    #[sea_orm(has_many = "super::team::Entity")]
    PersonalTeams,
    #[sea_orm(has_many = "super::project::Entity")]
    PersonalProjects,
    #[sea_orm(
        has_many = "super::routing_policy::Entity",
        from = "Column::Id",
        to = "super::routing_policy::Column::CreatedById"
    )]
    RoutingPoliciesCreated,
    #[sea_orm(
        has_many = "super::routing_policy::Entity",
        from = "Column::Id",
        to = "super::routing_policy::Column::UpdatedById"
    )]
    RoutingPoliciesUpdated,
    #[sea_orm(
        has_many = "super::gateway_guardrail::Entity",
        from = "Column::Id",
        to = "super::gateway_guardrail::Column::CreatedById"
    )]
    GatewayGuardrailsCreated,
    #[sea_orm(
        has_many = "super::gateway_guardrail::Entity",
        from = "Column::Id",
        to = "super::gateway_guardrail::Column::UpdatedById"
    )]
    GatewayGuardrailsUpdated,
    #[sea_orm(has_many = "super::ingestion_source::Entity")]
    IngestionSourcesCreated,
    #[sea_orm(has_many = "super::anomaly_rule::Entity")]
    AnomalyRulesCreated,
    #[sea_orm(has_many = "super::pinned_trace::Entity")]
    PinnedTraces,
    #[sea_orm(
        has_many = "super::trace_edit_overlay::Entity",
        from = "Column::Id",
        to = "super::trace_edit_overlay::Column::CreatedById"
    )]
    TraceEditOverlaysCreated,
    #[sea_orm(
        has_many = "super::trace_edit_overlay::Entity",
        from = "Column::Id",
        to = "super::trace_edit_overlay::Column::UpdatedById"
    )]
    TraceEditOverlaysUpdated,
}

impl Related<super::account::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Accounts.def()
    }
}

impl Related<super::account_credential::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AccountCredentials.def()
    }
}

impl Related<super::session::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Sessions.def()
    }
}

impl Related<super::two_factor::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TwoFactors.def()
    }
}

impl Related<super::passkey::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Passkeys.def()
    }
}

impl Related<super::team_user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TeamMemberships.def()
    }
}

impl Related<super::organization_user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OrgMemberships.def()
    }
}

impl Related<super::scim_user_resource::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ScimUserResources.def()
    }
}

impl Related<super::annotation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Annotation.def()
    }
}

impl Related<super::share_link::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ShareLinks.def()
    }
}

impl Related<super::workflow::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Workflow.def()
    }
}

impl Related<super::workflow_version::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::WorkflowVersion.def()
    }
}

impl Related<super::annotation_queue_members::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AnnotationQueues.def()
    }
}

impl Related<super::annotation_queue_item::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AssignedQueueItems.def()
    }
}

impl Related<super::llm_prompt_config_version::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::LlmPromptConfigVersions.def()
    }
}

impl Related<super::scenario::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Scenarios.def()
    }
}

impl Related<super::organization_invite::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::InviteRequests.def()
    }
}

impl Related<super::project_secret::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SecretsCreated.def()
    }
}

impl Related<super::saved_view::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SavedViews.def()
    }
}

impl Related<super::prompt_tag_assignment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::VersionTagsCreated.def()
    }
}

impl Related<super::prompt_tag::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::PromptTagsCreated.def()
    }
}

impl Related<super::group_membership::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::GroupMemberships.def()
    }
}

impl Related<super::role_binding::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RoleBindings.def()
    }
}

impl Related<super::scim_external_id::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ScimExternalIds.def()
    }
}

impl Related<super::api_key::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ApiKeys.def()
    }
}

impl Related<super::gateway_budget::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::GatewayBudgetsCreated.def()
    }
}

impl Related<super::virtual_key::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::PrincipalVirtualKeys.def()
    }
}

impl Related<super::team::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::PersonalTeams.def()
    }
}

impl Related<super::project::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::PersonalProjects.def()
    }
}

impl Related<super::routing_policy::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RoutingPoliciesCreated.def()
    }
}

impl Related<super::gateway_guardrail::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::GatewayGuardrailsCreated.def()
    }
}

impl Related<super::ingestion_source::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::IngestionSourcesCreated.def()
    }
}

impl Related<super::anomaly_rule::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AnomalyRulesCreated.def()
    }
}

impl Related<super::pinned_trace::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::PinnedTraces.def()
    }
}

impl Related<super::trace_edit_overlay::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TraceEditOverlaysCreated.def()
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
