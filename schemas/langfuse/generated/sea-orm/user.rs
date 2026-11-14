use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "users")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub name: Option<String>,
    #[sea_orm(unique)]
    pub email: Option<String>,
    pub email_verified: Option<DateTime>,
    pub password: Option<String>,
    pub sessions_expired_at: Option<DateTime>,
    pub image: Option<String>,
    #[sea_orm(default_value = false)]
    pub admin: bool,
    #[sea_orm(default_value = false)]
    pub v4_beta_enabled: bool,
    pub support_access: Option<Json>,
    pub created_at: DateTime,
    pub updated_at: DateTime,
    pub feature_flags: Vec<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::account::Entity")]
    Accounts,
    #[sea_orm(has_many = "super::session::Entity")]
    Sessions,
    #[sea_orm(has_many = "super::organization_membership::Entity")]
    OrganizationMemberships,
    #[sea_orm(has_many = "super::project_membership::Entity")]
    ProjectMemberships,
    #[sea_orm(has_many = "super::membership_invitation::Entity")]
    Invitations,
    #[sea_orm(
        has_many = "super::annotation_queue_item::Entity",
        from = "Column::Id",
        to = "super::annotation_queue_item::Column::LockedByUserId"
    )]
    AnnotatedLockedItem,
    #[sea_orm(
        has_many = "super::annotation_queue_item::Entity",
        from = "Column::Id",
        to = "super::annotation_queue_item::Column::AnnotatorUserId"
    )]
    AnnotatedCompletedItem,
    #[sea_orm(
        has_many = "super::dashboard_widget::Entity",
        from = "Column::Id",
        to = "super::dashboard_widget::Column::CreatedBy"
    )]
    DashboardWidgetsCreated,
    #[sea_orm(
        has_many = "super::dashboard_widget::Entity",
        from = "Column::Id",
        to = "super::dashboard_widget::Column::UpdatedBy"
    )]
    DashboardWidgetsUpdated,
    #[sea_orm(
        has_many = "super::dashboard::Entity",
        from = "Column::Id",
        to = "super::dashboard::Column::CreatedBy"
    )]
    DashboardCreated,
    #[sea_orm(
        has_many = "super::dashboard::Entity",
        from = "Column::Id",
        to = "super::dashboard::Column::UpdatedBy"
    )]
    DashboardUpdated,
    #[sea_orm(
        has_many = "super::table_view_preset::Entity",
        from = "Column::Id",
        to = "super::table_view_preset::Column::CreatedBy"
    )]
    TableViewPresetCreated,
    #[sea_orm(
        has_many = "super::table_view_preset::Entity",
        from = "Column::Id",
        to = "super::table_view_preset::Column::UpdatedBy"
    )]
    TableViewPresetUpdated,
    #[sea_orm(has_many = "super::session_view::Entity")]
    SessionViewsCreated,
    #[sea_orm(has_many = "super::session_view_pin_override::Entity")]
    SessionViewPinOverrides,
    #[sea_orm(
        has_many = "super::monitor::Entity",
        from = "Column::Id",
        to = "super::monitor::Column::CreatedBy"
    )]
    MonitorsCreated,
    #[sea_orm(
        has_many = "super::monitor::Entity",
        from = "Column::Id",
        to = "super::monitor::Column::UpdatedBy"
    )]
    MonitorsUpdated,
    #[sea_orm(
        has_many = "super::topic_config::Entity",
        from = "Column::Id",
        to = "super::topic_config::Column::CreatedBy"
    )]
    TopicConfigsCreated,
    #[sea_orm(
        has_many = "super::topic_config::Entity",
        from = "Column::Id",
        to = "super::topic_config::Column::UpdatedBy"
    )]
    TopicConfigsUpdated,
    #[sea_orm(
        has_many = "super::tracing_readiness_settings::Entity",
        from = "Column::Id",
        to = "super::tracing_readiness_settings::Column::CreatedBy"
    )]
    TracingReadinessCreated,
    #[sea_orm(
        has_many = "super::tracing_readiness_settings::Entity",
        from = "Column::Id",
        to = "super::tracing_readiness_settings::Column::UpdatedBy"
    )]
    TracingReadinessUpdated,
    #[sea_orm(has_many = "super::annotation_queue_assignment::Entity")]
    AnnotationQueueAssignment,
    #[sea_orm(has_many = "super::survey::Entity")]
    Surveys,
    #[sea_orm(has_many = "super::comment_reaction::Entity")]
    CommentReactions,
    #[sea_orm(has_many = "super::notification_preference::Entity")]
    NotificationPreferences,
    #[sea_orm(has_many = "super::default_view::Entity")]
    DefaultViews,
    #[sea_orm(has_many = "super::in_app_agent_conversation::Entity")]
    InAppAgentConversations,
    #[sea_orm(has_many = "super::in_app_agent_run::Entity")]
    InAppAgentRuns,
    #[sea_orm(
        has_many = "super::api_key::Entity",
        from = "Column::Id",
        to = "super::api_key::Column::CreatedByUserId"
    )]
    ApiKeysCreated,
    #[sea_orm(
        has_many = "super::api_key::Entity",
        from = "Column::Id",
        to = "super::api_key::Column::UserId"
    )]
    ApiKeys,
    #[sea_orm(has_many = "super::role_assignment::Entity")]
    RoleAssignments,
    #[sea_orm(has_many = "super::evaluator::Entity")]
    EvaluatorsCreated,
    #[sea_orm(has_many = "super::evaluator_version::Entity")]
    EvaluatorVersionsCreated,
    #[sea_orm(has_many = "super::evaluation_rule::Entity")]
    EvaluationRulesCreated,
}

impl Related<super::account::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Accounts.def()
    }
}

impl Related<super::session::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Sessions.def()
    }
}

impl Related<super::organization_membership::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OrganizationMemberships.def()
    }
}

impl Related<super::project_membership::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ProjectMemberships.def()
    }
}

impl Related<super::membership_invitation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Invitations.def()
    }
}

impl Related<super::annotation_queue_item::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AnnotatedLockedItem.def()
    }
}

impl Related<super::dashboard_widget::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::DashboardWidgetsCreated.def()
    }
}

impl Related<super::dashboard::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::DashboardCreated.def()
    }
}

impl Related<super::table_view_preset::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TableViewPresetCreated.def()
    }
}

impl Related<super::session_view::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SessionViewsCreated.def()
    }
}

impl Related<super::session_view_pin_override::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SessionViewPinOverrides.def()
    }
}

impl Related<super::monitor::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::MonitorsCreated.def()
    }
}

impl Related<super::topic_config::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TopicConfigsCreated.def()
    }
}

impl Related<super::tracing_readiness_settings::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TracingReadinessCreated.def()
    }
}

impl Related<super::annotation_queue_assignment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AnnotationQueueAssignment.def()
    }
}

impl Related<super::survey::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Surveys.def()
    }
}

impl Related<super::comment_reaction::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CommentReactions.def()
    }
}

impl Related<super::notification_preference::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::NotificationPreferences.def()
    }
}

impl Related<super::default_view::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::DefaultViews.def()
    }
}

impl Related<super::in_app_agent_conversation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::InAppAgentConversations.def()
    }
}

impl Related<super::in_app_agent_run::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::InAppAgentRuns.def()
    }
}

impl Related<super::api_key::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ApiKeysCreated.def()
    }
}

impl Related<super::role_assignment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RoleAssignments.def()
    }
}

impl Related<super::evaluator::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EvaluatorsCreated.def()
    }
}

impl Related<super::evaluator_version::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EvaluatorVersionsCreated.def()
    }
}

impl Related<super::evaluation_rule::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EvaluationRulesCreated.def()
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
