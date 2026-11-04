from sqlalchemy import ARRAY, BigInteger, CHAR, Dialect, Enum, ForeignKey, ForeignKeyConstraint, Index, Integer, JSON, MetaData, Numeric, String, Text, TypeDecorator, Uuid, text
from sqlalchemy.dialects.postgresql import JSONB, TIMESTAMP
from sqlalchemy.orm import DeclarativeBase, Mapped, mapped_column, relationship
from typing import Any, Optional
from decimal import Decimal as DecimalType
from datetime import datetime, timezone
import uuid as uuid_mod


class UtcDateTime(TypeDecorator[datetime]):
    impl = TIMESTAMP
    cache_ok = True

    def process_bind_param(self, value: Optional[datetime], dialect: Dialect) -> Optional[datetime]:
        if value is None or value.tzinfo is None:
            return value
        return value.astimezone(timezone.utc).replace(tzinfo=None)

    def process_result_value(self, value: Optional[datetime], dialect: Dialect) -> Optional[datetime]:
        return None if value is None else value.replace(tzinfo=timezone.utc)


class UtcDateTimeTz(TypeDecorator[datetime]):
    impl = TIMESTAMP
    cache_ok = True

    def __init__(self, precision: Optional[int] = None) -> None:
        super().__init__(timezone=True, precision=precision)

    def process_bind_param(self, value: Optional[datetime], dialect: Dialect) -> Optional[datetime]:
        return value if value is None or value.tzinfo else value.replace(tzinfo=timezone.utc)

    def process_result_value(self, value: Optional[datetime], dialect: Dialect) -> Optional[datetime]:
        return None if value is None else value.astimezone(timezone.utc)


def utc_now() -> datetime:
    return datetime.now(timezone.utc)


class Base(DeclarativeBase):
    metadata = MetaData(naming_convention={"pk": "%(table_name)s_pkey"})
    type_annotation_map = {datetime: UtcDateTime(precision=3), str: Text, DecimalType: Numeric(precision=65, scale=30)}


class Account(Base):
    __tablename__ = "Account"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[str] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="Account_user_id_fkey"))
    type: Mapped[str]
    provider: Mapped[str]
    provider_account_id: Mapped[str] = mapped_column("providerAccountId")
    refresh_token: Mapped[Optional[str]]
    access_token: Mapped[Optional[str]]
    expires_at: Mapped[Optional[int]]
    expires_in: Mapped[Optional[int]]
    ext_expires_in: Mapped[Optional[int]]
    token_type: Mapped[Optional[str]]
    scope: Mapped[Optional[str]]
    id_token: Mapped[Optional[str]]
    session_state: Mapped[Optional[str]]
    refresh_token_expires_in: Mapped[Optional[int]]
    created_at: Mapped[Optional[int]]

    __table_args__ = (
        Index("Account_user_id_idx", "user_id"),
        Index("Account_provider_providerAccountId_key", "provider", "providerAccountId", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="accounts")

class Session(Base):
    __tablename__ = "Session"

    id: Mapped[str] = mapped_column(primary_key=True)
    session_token: Mapped[str]
    user_id: Mapped[str] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="Session_user_id_fkey"))
    expires: Mapped[datetime]

    __table_args__ = (
        Index("Session_session_token_key", "session_token", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="sessions")

class User(Base):
    __tablename__ = "users"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[Optional[str]]
    email: Mapped[Optional[str]]
    email_verified: Mapped[Optional[datetime]]
    password: Mapped[Optional[str]]
    sessions_expired_at: Mapped[Optional[datetime]]
    image: Mapped[Optional[str]]
    admin: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    v4_beta_enabled: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    support_access: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    feature_flags: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))

    __table_args__ = (
        Index("users_email_key", "email", unique=True),
    )

    accounts: Mapped[list["Account"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    sessions: Mapped[list["Session"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    organization_memberships: Mapped[list["OrganizationMembership"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    project_memberships: Mapped[list["ProjectMembership"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    invitations: Mapped[list["MembershipInvitation"]] = relationship(back_populates="invited_by_user")
    annotated_locked_item: Mapped[list["AnnotationQueueItem"]] = relationship(foreign_keys="AnnotationQueueItem.locked_by_user_id", back_populates="locked_by_user")
    annotated_completed_item: Mapped[list["AnnotationQueueItem"]] = relationship(foreign_keys="AnnotationQueueItem.annotator_user_id", back_populates="annotator_user")
    dashboard_widgets_created: Mapped[list["DashboardWidget"]] = relationship(foreign_keys="DashboardWidget.created_by", back_populates="created_by_user")
    dashboard_widgets_updated: Mapped[list["DashboardWidget"]] = relationship(foreign_keys="DashboardWidget.updated_by", back_populates="updated_by_user")
    dashboard_created: Mapped[list["Dashboard"]] = relationship(foreign_keys="Dashboard.created_by", back_populates="created_by_user")
    dashboard_updated: Mapped[list["Dashboard"]] = relationship(foreign_keys="Dashboard.updated_by", back_populates="updated_by_user")
    table_view_preset_created: Mapped[list["TableViewPreset"]] = relationship(foreign_keys="TableViewPreset.created_by", back_populates="created_by_user")
    table_view_preset_updated: Mapped[list["TableViewPreset"]] = relationship(foreign_keys="TableViewPreset.updated_by", back_populates="updated_by_user")
    monitors_created: Mapped[list["Monitor"]] = relationship(foreign_keys="Monitor.created_by", back_populates="created_by_user")
    monitors_updated: Mapped[list["Monitor"]] = relationship(foreign_keys="Monitor.updated_by", back_populates="updated_by_user")
    topic_configs_created: Mapped[list["TopicConfig"]] = relationship(foreign_keys="TopicConfig.created_by", back_populates="created_by_user")
    topic_configs_updated: Mapped[list["TopicConfig"]] = relationship(foreign_keys="TopicConfig.updated_by", back_populates="updated_by_user")
    tracing_readiness_created: Mapped[list["TracingReadinessSettings"]] = relationship(foreign_keys="TracingReadinessSettings.created_by", back_populates="created_by_user")
    tracing_readiness_updated: Mapped[list["TracingReadinessSettings"]] = relationship(foreign_keys="TracingReadinessSettings.updated_by", back_populates="updated_by_user")
    annotation_queue_assignment: Mapped[list["AnnotationQueueAssignment"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    surveys: Mapped[list["Survey"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    comment_reactions: Mapped[list["CommentReaction"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    notification_preferences: Mapped[list["NotificationPreference"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    default_views: Mapped[list["DefaultView"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    in_app_agent_conversations: Mapped[list["InAppAgentConversation"]] = relationship(back_populates="created_by_user")
    in_app_agent_runs: Mapped[list["InAppAgentRun"]] = relationship(back_populates="triggered_by_user")
    api_keys_created: Mapped[list["ApiKey"]] = relationship(foreign_keys="ApiKey.created_by_user_id", back_populates="created_by_user")
    api_keys: Mapped[list["ApiKey"]] = relationship(foreign_keys="ApiKey.user_id", cascade="all, delete", passive_deletes=True, back_populates="user")
    role_assignments: Mapped[list["RoleAssignment"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    evaluators_created: Mapped[list["Evaluator"]] = relationship(back_populates="created_by_user")
    evaluator_versions_created: Mapped[list["EvaluatorVersion"]] = relationship(back_populates="created_by_user")
    evaluation_rules_created: Mapped[list["EvaluationRule"]] = relationship(back_populates="created_by_user")

class VerificationToken(Base):
    __tablename__ = "verification_tokens"

    identifier: Mapped[str]
    token: Mapped[str] = mapped_column()
    expires: Mapped[datetime]

    __table_args__ = (
        Index("verification_tokens_token_key", "token", unique=True),
        Index("verification_tokens_identifier_token_key", "identifier", "token", unique=True),
    )

    __mapper_args__ = {"primary_key": [token]}

class Organization(Base):
    __tablename__ = "organizations"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    cloud_config: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))
    cloud_billing_cycle_anchor: Mapped[Optional[datetime]] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    cloud_billing_cycle_updated_at: Mapped[Optional[datetime]]
    cloud_current_cycle_usage: Mapped[Optional[int]]
    cloud_free_tier_usage_threshold_state: Mapped[Optional[str]]
    ai_features_enabled: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    ai_telemetry_enabled: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    access_audit_log_enabled: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    feature_flag_org_defaults: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    sfdc_org_id: Mapped[Optional[str]]

    organization_memberships: Mapped[list["OrganizationMembership"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    projects: Mapped[list["Project"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    membership_invitation: Mapped[list["MembershipInvitation"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    api_key: Mapped[list["ApiKey"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    surveys: Mapped[list["Survey"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="org")
    cloud_spend_alerts: Mapped[list["CloudSpendAlert"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="org")
    verified_domains: Mapped[list["VerifiedDomain"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    gateway_ai_connections: Mapped[list["GatewayAiConnection"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    organization_llm_api_keys: Mapped[list["LlmApiKeys"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    role_assignments: Mapped[list["RoleAssignment"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")
    gateway_config: Mapped[Optional["GatewayConfig"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization")

class Project(Base):
    __tablename__ = "projects"

    id: Mapped[str] = mapped_column(primary_key=True)
    org_id: Mapped[str] = mapped_column(ForeignKey("organizations.id", ondelete="CASCADE", onupdate="CASCADE", name="projects_org_id_fkey"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    deleted_at: Mapped[Optional[datetime]]
    name: Mapped[str]
    retention_days: Mapped[Optional[int]]
    has_traces: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))
    home_dashboard_id: Mapped[Optional[str]] = mapped_column(ForeignKey("dashboards.id", ondelete="SET NULL", onupdate="CASCADE", name="projects_home_dashboard_id_fkey"))

    __table_args__ = (
        Index("projects_org_id_idx", "org_id"),
    )

    home_dashboard: Mapped[Optional["Dashboard"]] = relationship(foreign_keys=[home_dashboard_id], back_populates="home_dashboard_projects")
    organization: Mapped["Organization"] = relationship(back_populates="projects")
    project_members: Mapped[list["ProjectMembership"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    api_keys: Mapped[list["ApiKey"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    role_assignments: Mapped[list["RoleAssignment"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    dataset: Mapped[list["Dataset"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    invitations: Mapped[list["MembershipInvitation"]] = relationship(back_populates="project")
    sessions: Mapped[list["TraceSession"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    prompt: Mapped[list["Prompt"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    skill: Mapped[list["Skill"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    skill_file: Mapped[list["SkillFile"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    skill_blob: Mapped[list["SkillBlob"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    model: Mapped[list["Model"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    eval_template: Mapped[list["EvalTemplate"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    job_configuration: Mapped[list["JobConfiguration"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    job_execution: Mapped[list["JobExecution"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    evaluator: Mapped[list["Evaluator"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    evaluation_rule: Mapped[list["EvaluationRule"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    evaluation_rule_evaluator_assignment: Mapped[list["EvaluationRuleEvaluatorAssignment"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    llm_api_keys: Mapped[list["LlmApiKeys"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    posthog_integration: Mapped[list["PosthogIntegration"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    mixpanel_integration: Mapped[list["MixpanelIntegration"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    blob_storage_integration: Mapped[list["BlobStorageIntegration"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    web_callout_endpoints: Mapped[list["WebCalloutEndpoint"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    score_config: Mapped[list["ScoreConfig"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    batch_export: Mapped[list["BatchExport"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    batch_action: Mapped[list["BatchAction"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    comment: Mapped[list["Comment"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    comment_reactions: Mapped[list["CommentReaction"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    annotation_queue: Mapped[list["AnnotationQueue"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    annotation_queue_item: Mapped[list["AnnotationQueueItem"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    media: Mapped[list["Media"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    prompt_dependency: Mapped[list["PromptDependency"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    llm_schema: Mapped[list["LlmSchema"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    llm_tool: Mapped[list["LlmTool"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    prompt_protected_labels: Mapped[list["PromptProtectedLabels"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    dashboard: Mapped[list["Dashboard"]] = relationship(foreign_keys="Dashboard.project_id", cascade="all, delete", passive_deletes=True, back_populates="project")
    dashboard_widget: Mapped[list["DashboardWidget"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    table_view_preset: Mapped[list["TableViewPreset"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    actions: Mapped[list["Action"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    triggers: Mapped[list["Trigger"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    automation_executions: Mapped[list["AutomationExecution"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    automation: Mapped[list["Automation"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    monitors: Mapped[list["Monitor"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    default_llm_model: Mapped[list["DefaultLlmModel"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    price: Mapped[list["Price"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    pending_deletion: Mapped[list["PendingDeletion"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    annotation_queue_assignment: Mapped[list["AnnotationQueueAssignment"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    notification_preference: Mapped[list["NotificationPreference"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    default_view: Mapped[list["DefaultView"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    in_app_agent_conversation: Mapped[list["InAppAgentConversation"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    in_app_agent_event: Mapped[list["InAppAgentEvent"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    in_app_agent_run: Mapped[list["InAppAgentRun"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    in_app_agent_pending_tool_approval: Mapped[list["InAppAgentPendingToolApproval"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    topic_clustering_runs: Mapped[list["TopicClusteringRun"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    gateway_configs: Mapped[list["GatewayConfig"]] = relationship(back_populates="default_ingestion_project")
    tracing_readiness_settings: Mapped[Optional["TracingReadinessSettings"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    external_media_storage_integration: Mapped[Optional["ExternalMediaStorageIntegration"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    topic_config: Mapped[Optional["TopicConfig"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    slack_integration: Mapped[Optional["SlackIntegration"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")

class ApiKey(Base):
    __tablename__ = "api_keys"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    note: Mapped[Optional[str]]
    public_key: Mapped[str]
    hashed_secret_key: Mapped[str]
    fast_hashed_secret_key: Mapped[Optional[str]]
    display_secret_key: Mapped[str]
    last_used_at: Mapped[Optional[datetime]]
    expires_at: Mapped[Optional[datetime]]
    is_in_app_agent_key: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    project_id: Mapped[Optional[str]] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="api_keys_project_id_fkey"))
    org_id: Mapped[Optional[str]] = mapped_column("organization_id", ForeignKey("organizations.id", ondelete="CASCADE", onupdate="CASCADE", name="api_keys_organization_id_fkey"))
    scope: Mapped[str] = mapped_column(Enum("ORGANIZATION", "PROJECT", name="ApiKeyScope"), default="PROJECT", server_default=text("'PROJECT'"))
    created_by_user_id: Mapped[Optional[str]] = mapped_column(ForeignKey("users.id", ondelete="SET NULL", onupdate="CASCADE", name="api_keys_created_by_user_id_fkey"))
    user_id: Mapped[Optional[str]] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="api_keys_user_id_fkey"))
    created_by_api_key_id: Mapped[Optional[str]] = mapped_column(ForeignKey("api_keys.id", ondelete="SET NULL", onupdate="CASCADE", name="api_keys_created_by_api_key_id_fkey"))

    __table_args__ = (
        Index("api_keys_id_key", "id", unique=True),
        Index("api_keys_public_key_key", "public_key", unique=True),
        Index("api_keys_hashed_secret_key_key", "hashed_secret_key", unique=True),
        Index("api_keys_fast_hashed_secret_key_key", "fast_hashed_secret_key", unique=True),
        Index("api_keys_organization_id_idx", "organization_id"),
        Index("api_keys_project_id_idx", "project_id"),
        Index("api_keys_created_by_api_key_id_idx", "created_by_api_key_id"),
        Index("api_keys_user_id_organization_id_idx", "user_id", "organization_id"),
        Index("api_keys_public_key_idx", "public_key"),
        Index("api_keys_hashed_secret_key_idx", "hashed_secret_key"),
        Index("api_keys_fast_hashed_secret_key_idx", "fast_hashed_secret_key"),
    )

    project: Mapped[Optional["Project"]] = relationship(back_populates="api_keys")
    organization: Mapped[Optional["Organization"]] = relationship(back_populates="api_key")
    created_by_user: Mapped[Optional["User"]] = relationship(foreign_keys=[created_by_user_id], back_populates="api_keys_created")
    user: Mapped[Optional["User"]] = relationship(foreign_keys=[user_id], back_populates="api_keys")
    created_by_api_key: Mapped[Optional["ApiKey"]] = relationship(remote_side=[id], back_populates="api_keys_created")
    api_keys_created: Mapped[list["ApiKey"]] = relationship(back_populates="created_by_api_key")
    role_assignments: Mapped[list["RoleAssignment"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="api_key")
    gateway_association: Mapped[Optional["GatewayApiKeyAssociation"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="api_key")

class RoleAssignment(Base):
    __tablename__ = "role_assignments"

    id: Mapped[str] = mapped_column(primary_key=True)
    owner_id: Mapped[str]
    project_id: Mapped[Optional[str]] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="role_assignments_project_id_fkey"))
    org_id: Mapped[str] = mapped_column(ForeignKey("organizations.id", ondelete="CASCADE", onupdate="CASCADE", name="role_assignments_org_id_fkey"))
    principal_id: Mapped[str]
    user_id: Mapped[Optional[str]] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="role_assignments_user_id_fkey"))
    api_key_id: Mapped[Optional[str]] = mapped_column(ForeignKey("api_keys.id", ondelete="CASCADE", onupdate="CASCADE", name="role_assignments_api_key_id_fkey"))
    role_id: Mapped[str]
    system_role: Mapped[str] = mapped_column(Enum("OWNER", "ADMIN", "MEMBER", "VIEWER", "NONE", "LEGACY_PROJECT_API_KEY", "LEGACY_ORGANIZATION_API_KEY", "SCORES_INGEST", "INGEST", "AI_GATEWAY", name="SystemRole"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("role_assignments_org_id_idx", "org_id"),
        Index("role_assignments_project_id_idx", "project_id"),
        Index("role_assignments_principal_id_idx", "principal_id"),
        Index("role_assignments_user_id_idx", "user_id"),
        Index("role_assignments_api_key_id_idx", "api_key_id"),
        Index("role_assignments_owner_id_principal_id_role_id_key", "owner_id", "principal_id", "role_id", unique=True),
    )

    project: Mapped[Optional["Project"]] = relationship(back_populates="role_assignments")
    organization: Mapped["Organization"] = relationship(back_populates="role_assignments")
    user: Mapped[Optional["User"]] = relationship(back_populates="role_assignments")
    api_key: Mapped[Optional["ApiKey"]] = relationship(back_populates="role_assignments")

class GatewayConfig(Base):
    __tablename__ = "gateway_configs"

    organization_id: Mapped[str] = mapped_column(ForeignKey("organizations.id", ondelete="CASCADE", onupdate="CASCADE", name="gateway_configs_organization_id_fkey"), primary_key=True)
    default_ingestion_project_id: Mapped[Optional[str]] = mapped_column(ForeignKey("projects.id", ondelete="SET NULL", onupdate="CASCADE", name="gateway_configs_default_ingestion_project_id_fkey"))
    ingestion_mode: Mapped[str] = mapped_column(Enum("none", "usage", "full", name="GatewayIngestionMode"), default="usage", server_default=text("'usage'"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    organization: Mapped["Organization"] = relationship(back_populates="gateway_config")
    default_ingestion_project: Mapped[Optional["Project"]] = relationship(back_populates="gateway_configs")

class GatewayAiConnection(Base):
    __tablename__ = "gateway_ai_connections"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column(ForeignKey("organizations.id", ondelete="CASCADE", onupdate="CASCADE", name="gateway_ai_connections_organization_id_fkey"))
    name: Mapped[str]
    provider: Mapped[str] = mapped_column(Enum("openai", "anthropic", name="GatewayProvider"))
    encrypted_credential: Mapped[str] = mapped_column("encrypted_credentials")
    display_secret: Mapped[str] = mapped_column("display_secret_key")
    created_by_id: Mapped[Optional[str]]
    status: Mapped[str] = mapped_column(Enum("enabled", "disabled", "error", name="GatewayConnectionStatus"), default="enabled", server_default=text("'enabled'"))
    allowed_models: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("gateway_ai_conn_org_created_at_id_idx", "organization_id", "created_at", "id"),
        Index("gateway_ai_conn_org_status_created_at_id_idx", "organization_id", "status", "created_at", "id"),
        Index("gateway_ai_connections_organization_id_name_key", "organization_id", "name", unique=True),
    )

    organization: Mapped["Organization"] = relationship(back_populates="gateway_ai_connections")

class GatewayApiKeyAssociation(Base):
    __tablename__ = "gateway_api_key_associations"

    api_key_id: Mapped[str] = mapped_column(ForeignKey("api_keys.id", ondelete="CASCADE", onupdate="CASCADE", name="gateway_api_key_associations_api_key_id_fkey"), primary_key=True)
    metadata_: Mapped[dict[str, Any]] = mapped_column("metadata", JSONB, default=lambda: {}, server_default=text("'{}'"))

    api_key: Mapped["ApiKey"] = relationship(back_populates="gateway_association")

class InAppAgentConversation(Base):
    __tablename__ = "in_app_agent_conversations"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="in_app_agent_conversations_project_id_fkey"), primary_key=True)
    created_by_user_id: Mapped[Optional[str]] = mapped_column(ForeignKey("users.id", ondelete="SET NULL", onupdate="CASCADE", name="in_app_agent_conversations_created_by_user_id_fkey"))
    title: Mapped[Optional[str]]
    renamed_by_user_at: Mapped[Optional[datetime]]
    visibility_scope: Mapped[str] = mapped_column(Enum("PERSONAL", "PROJECT", name="InAppAgentConversationVisibilityScope"), default="PERSONAL", server_default=text("'PERSONAL'"))
    provider_session_id: Mapped[Optional[str]]
    always_allowed_tools: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    deleted_at: Mapped[Optional[datetime]]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("in_app_agent_conversations_project_user_list_idx", "project_id", "created_by_user_id", "deleted_at", "updated_at", "id"),
        Index("in_app_agent_conversations_project_updated_at_idx", "project_id", "updated_at"),
    )

    project: Mapped["Project"] = relationship(back_populates="in_app_agent_conversation")
    created_by_user: Mapped[Optional["User"]] = relationship(back_populates="in_app_agent_conversations")
    events: Mapped[list["InAppAgentEvent"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="conversation")
    runs: Mapped[list["InAppAgentRun"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="conversation")
    pending_tool_approvals: Mapped[list["InAppAgentPendingToolApproval"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="conversation")

class InAppAgentEvent(Base):
    __tablename__ = "in_app_agent_events"

    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="in_app_agent_events_project_id_fkey"), primary_key=True)
    conversation_id: Mapped[str] = mapped_column(primary_key=True)
    run_id: Mapped[str]
    sequence_number: Mapped[int] = mapped_column(primary_key=True)
    type: Mapped[str]
    event: Mapped[dict[str, Any]] = mapped_column(JSONB)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("in_app_agent_events_project_run_idx", "project_id", "run_id"),
        ForeignKeyConstraint(["conversation_id", "project_id"], ["in_app_agent_conversations.id", "in_app_agent_conversations.project_id"], ondelete="CASCADE", onupdate="CASCADE", name="in_app_agent_events_conversation_id_project_id_fkey"),
        ForeignKeyConstraint(["run_id", "project_id"], ["in_app_agent_runs.id", "in_app_agent_runs.project_id"], ondelete="CASCADE", onupdate="CASCADE", name="in_app_agent_events_run_id_project_id_fkey"),
    )

    project: Mapped["Project"] = relationship(back_populates="in_app_agent_event")
    conversation: Mapped["InAppAgentConversation"] = relationship(back_populates="events")
    run: Mapped["InAppAgentRun"] = relationship(back_populates="events")

class InAppAgentRun(Base):
    __tablename__ = "in_app_agent_runs"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="in_app_agent_runs_project_id_fkey"), primary_key=True)
    conversation_id: Mapped[str]
    triggered_by_user_id: Mapped[Optional[str]] = mapped_column(ForeignKey("users.id", ondelete="SET NULL", onupdate="CASCADE", name="in_app_agent_runs_triggered_by_user_id_fkey"))
    model: Mapped[Optional[str]]
    mcp_api_key_id: Mapped[Optional[str]]
    error_code: Mapped[Optional[str]]
    error_message: Mapped[Optional[str]]
    finished_at: Mapped[Optional[datetime]]
    status: Mapped[Optional[str]]
    request: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    claimed_at: Mapped[Optional[datetime]]
    heartbeat_at: Mapped[Optional[datetime]]
    cancel_requested_at: Mapped[Optional[datetime]]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("in_app_agent_runs_project_conversation_created_idx", "project_id", "conversation_id", "created_at"),
        ForeignKeyConstraint(["conversation_id", "project_id"], ["in_app_agent_conversations.id", "in_app_agent_conversations.project_id"], ondelete="CASCADE", onupdate="CASCADE", name="in_app_agent_runs_conversation_id_project_id_fkey"),
    )

    project: Mapped["Project"] = relationship(back_populates="in_app_agent_run")
    conversation: Mapped["InAppAgentConversation"] = relationship(back_populates="runs")
    triggered_by_user: Mapped[Optional["User"]] = relationship(back_populates="in_app_agent_runs")
    events: Mapped[list["InAppAgentEvent"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="run")

class InAppAgentPendingToolApproval(Base):
    __tablename__ = "in_app_agent_pending_tool_approvals"

    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="in_app_agent_pending_tool_approvals_project_id_fkey"), primary_key=True)
    conversation_id: Mapped[str] = mapped_column(primary_key=True)
    tool_call_id: Mapped[str] = mapped_column(primary_key=True)
    approval_fingerprint: Mapped[str]
    expires_at: Mapped[datetime]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("in_app_agent_pending_tool_approvals_expires_at_idx", "expires_at"),
        ForeignKeyConstraint(["conversation_id", "project_id"], ["in_app_agent_conversations.id", "in_app_agent_conversations.project_id"], ondelete="CASCADE", onupdate="CASCADE", name="in_app_agent_pending_tool_approvals_conversation_id_projec_fkey"),
    )

    project: Mapped["Project"] = relationship(back_populates="in_app_agent_pending_tool_approval")
    conversation: Mapped["InAppAgentConversation"] = relationship(back_populates="pending_tool_approvals")

class BackgroundMigration(Base):
    __tablename__ = "background_migrations"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    script: Mapped[str]
    args: Mapped[dict[str, Any]] = mapped_column(JSONB)
    state: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    finished_at: Mapped[Optional[datetime]]
    failed_at: Mapped[Optional[datetime]]
    failed_reason: Mapped[Optional[str]]
    worker_id: Mapped[Optional[str]]
    locked_at: Mapped[Optional[datetime]]

    __table_args__ = (
        Index("background_migrations_name_key", "name", unique=True),
    )

class LlmApiKeys(Base):
    __tablename__ = "llm_api_keys"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    provider: Mapped[str]
    adapter: Mapped[str]
    display_secret_key: Mapped[str]
    secret_key: Mapped[str]
    base_url: Mapped[Optional[str]]
    custom_models: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    with_default_models: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    extra_headers: Mapped[Optional[str]]
    extra_header_keys: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    config: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    project_id: Mapped[Optional[str]] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="llm_api_keys_project_id_fkey"))
    organization_id: Mapped[Optional[str]] = mapped_column(ForeignKey("organizations.id", ondelete="CASCADE", onupdate="CASCADE", name="llm_api_keys_organization_id_fkey"))

    __table_args__ = (
        Index("llm_api_keys_id_key", "id", unique=True),
        Index("llm_api_keys_project_id_provider_key", "project_id", "provider", unique=True),
        Index("llm_api_keys_organization_id_provider_key", "organization_id", "provider", unique=True),
    )

    project: Mapped[Optional["Project"]] = relationship(back_populates="llm_api_keys")
    organization: Mapped[Optional["Organization"]] = relationship(back_populates="organization_llm_api_keys")
    default_llm_model: Mapped[list["DefaultLlmModel"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="llm_api_key")
    topic_summary_configs: Mapped[list["TopicConfig"]] = relationship(foreign_keys="TopicConfig.summary_llm_api_key_id", back_populates="summary_llm_api_key")
    topic_embedding_configs: Mapped[list["TopicConfig"]] = relationship(foreign_keys="TopicConfig.embedding_llm_api_key_id", back_populates="embedding_llm_api_key")
    topic_clustering_configs: Mapped[list["TopicConfig"]] = relationship(foreign_keys="TopicConfig.clustering_llm_api_key_id", back_populates="clustering_llm_api_key")

class OrganizationMembership(Base):
    __tablename__ = "organization_memberships"

    id: Mapped[str] = mapped_column(primary_key=True)
    org_id: Mapped[str] = mapped_column(ForeignKey("organizations.id", ondelete="CASCADE", onupdate="CASCADE", name="organization_memberships_org_id_fkey"))
    user_id: Mapped[str] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="organization_memberships_user_id_fkey"))
    role: Mapped[str] = mapped_column(Enum("OWNER", "ADMIN", "MEMBER", "VIEWER", "NONE", "AI_GATEWAY", name="Role"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("organization_memberships_user_id_idx", "user_id"),
        Index("organization_memberships_org_id_user_id_key", "org_id", "user_id", unique=True),
    )

    organization: Mapped["Organization"] = relationship(back_populates="organization_memberships")
    user: Mapped["User"] = relationship(back_populates="organization_memberships")
    project_memberships: Mapped[list["ProjectMembership"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="organization_membership")

class ProjectMembership(Base):
    __tablename__ = "project_memberships"

    org_membership_id: Mapped[str] = mapped_column(ForeignKey("organization_memberships.id", ondelete="CASCADE", onupdate="CASCADE", name="project_memberships_org_membership_id_fkey"))
    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="project_memberships_project_id_fkey"), primary_key=True)
    user_id: Mapped[str] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="project_memberships_user_id_fkey"), primary_key=True)
    role: Mapped[str] = mapped_column(Enum("OWNER", "ADMIN", "MEMBER", "VIEWER", "NONE", "AI_GATEWAY", name="Role"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("project_memberships_user_id_idx", "user_id"),
        Index("project_memberships_project_id_idx", "project_id"),
        Index("project_memberships_org_membership_id_idx", "org_membership_id"),
    )

    organization_membership: Mapped["OrganizationMembership"] = relationship(back_populates="project_memberships")
    project: Mapped["Project"] = relationship(back_populates="project_members")
    user: Mapped["User"] = relationship(back_populates="project_memberships")

class MembershipInvitation(Base):
    __tablename__ = "membership_invitations"

    id: Mapped[str] = mapped_column(primary_key=True)
    email: Mapped[str]
    org_id: Mapped[str] = mapped_column(ForeignKey("organizations.id", ondelete="CASCADE", onupdate="CASCADE", name="membership_invitations_org_id_fkey"))
    org_role: Mapped[str] = mapped_column(Enum("OWNER", "ADMIN", "MEMBER", "VIEWER", "NONE", "AI_GATEWAY", name="Role"))
    project_id: Mapped[Optional[str]] = mapped_column(ForeignKey("projects.id", ondelete="SET NULL", onupdate="CASCADE", name="membership_invitations_project_id_fkey"))
    project_role: Mapped[Optional[str]] = mapped_column(Enum("OWNER", "ADMIN", "MEMBER", "VIEWER", "NONE", "AI_GATEWAY", name="Role"))
    invited_by_user_id: Mapped[Optional[str]] = mapped_column(ForeignKey("users.id", ondelete="SET NULL", onupdate="CASCADE", name="membership_invitations_invited_by_user_id_fkey"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("membership_invitations_id_key", "id", unique=True),
        Index("membership_invitations_project_id_idx", "project_id"),
        Index("membership_invitations_org_id_idx", "org_id"),
        Index("membership_invitations_email_idx", "email"),
        Index("membership_invitations_email_org_id_key", "email", "org_id", unique=True),
    )

    organization: Mapped["Organization"] = relationship(back_populates="membership_invitation")
    project: Mapped[Optional["Project"]] = relationship(back_populates="invitations")
    invited_by_user: Mapped[Optional["User"]] = relationship(back_populates="invitations")

class TraceSession(Base):
    __tablename__ = "trace_sessions"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="trace_sessions_project_id_fkey"), primary_key=True)
    bookmarked: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    public: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    environment: Mapped[str] = mapped_column(default="default", server_default=text("'default'"))

    __table_args__ = (
        Index("trace_sessions_project_id_created_at_idx", "project_id", "created_at"),
    )

    project: Mapped["Project"] = relationship(back_populates="sessions")

class ScoreConfig(Base):
    __tablename__ = "score_configs"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="score_configs_project_id_fkey"))
    name: Mapped[str]
    data_type: Mapped[str] = mapped_column(Enum("CATEGORICAL", "NUMERIC", "BOOLEAN", "TEXT", name="ScoreConfigDataType"))
    is_archived: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    min_value: Mapped[Optional[float]]
    max_value: Mapped[Optional[float]]
    categories: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    description: Mapped[Optional[str]]

    __table_args__ = (
        Index("score_configs_data_type_idx", "data_type"),
        Index("score_configs_is_archived_idx", "is_archived"),
        Index("score_configs_project_id_idx", "project_id"),
        Index("score_configs_categories_idx", "categories"),
        Index("score_configs_created_at_idx", "created_at"),
        Index("score_configs_updated_at_idx", "updated_at"),
        Index("score_configs_id_project_id_key", "id", "project_id", unique=True),
    )

    project: Mapped["Project"] = relationship(back_populates="score_config")

class AnnotationQueue(Base):
    __tablename__ = "annotation_queues"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    description: Mapped[Optional[str]]
    score_config_ids: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="annotation_queues_project_id_fkey"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("annotation_queues_id_project_id_idx", "id", "project_id"),
        Index("annotation_queues_project_id_created_at_idx", "project_id", "created_at"),
        Index("annotation_queues_project_id_name_key", "project_id", "name", unique=True),
    )

    project: Mapped["Project"] = relationship(back_populates="annotation_queue")
    annotation_queue_item: Mapped[list["AnnotationQueueItem"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="queue")
    annotation_queue_assignment: Mapped[list["AnnotationQueueAssignment"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="queue")

class AnnotationQueueItem(Base):
    __tablename__ = "annotation_queue_items"

    id: Mapped[str] = mapped_column(primary_key=True)
    queue_id: Mapped[str] = mapped_column(ForeignKey("annotation_queues.id", ondelete="CASCADE", onupdate="CASCADE", name="annotation_queue_items_queue_id_fkey"))
    object_id: Mapped[str]
    object_type: Mapped[str] = mapped_column(Enum("TRACE", "OBSERVATION", "SESSION", name="AnnotationQueueObjectType"))
    status: Mapped[str] = mapped_column(Enum("PENDING", "COMPLETED", name="AnnotationQueueStatus"), default="PENDING", server_default=text("'PENDING'"))
    locked_at: Mapped[Optional[datetime]]
    locked_by_user_id: Mapped[Optional[str]] = mapped_column(ForeignKey("users.id", ondelete="SET NULL", onupdate="CASCADE", name="annotation_queue_items_locked_by_user_id_fkey"))
    annotator_user_id: Mapped[Optional[str]] = mapped_column(ForeignKey("users.id", ondelete="SET NULL", onupdate="CASCADE", name="annotation_queue_items_annotator_user_id_fkey"))
    completed_at: Mapped[Optional[datetime]]
    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="annotation_queue_items_project_id_fkey"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("annotation_queue_items_id_project_id_idx", "id", "project_id"),
        Index("annotation_queue_items_project_id_queue_id_status_idx", "project_id", "queue_id", "status"),
        Index("annotation_queue_items_object_id_object_type_project_id_que_idx", "object_id", "object_type", "project_id", "queue_id"),
        Index("annotation_queue_items_annotator_user_id_idx", "annotator_user_id"),
        Index("annotation_queue_items_created_at_idx", "created_at"),
    )

    queue: Mapped["AnnotationQueue"] = relationship(back_populates="annotation_queue_item")
    locked_by_user: Mapped[Optional["User"]] = relationship(foreign_keys=[locked_by_user_id], back_populates="annotated_locked_item")
    annotator_user: Mapped[Optional["User"]] = relationship(foreign_keys=[annotator_user_id], back_populates="annotated_completed_item")
    project: Mapped["Project"] = relationship(back_populates="annotation_queue_item")

class AnnotationQueueAssignment(Base):
    __tablename__ = "annotation_queue_assignments"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="annotation_queue_assignments_project_id_fkey"))
    user_id: Mapped[str] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="annotation_queue_assignments_user_id_fkey"))
    queue_id: Mapped[str] = mapped_column(ForeignKey("annotation_queues.id", ondelete="CASCADE", onupdate="CASCADE", name="annotation_queue_assignments_queue_id_fkey"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("annotation_queue_assignments_project_id_queue_id_user_id_key", "project_id", "queue_id", "user_id", unique=True),
    )

    project: Mapped["Project"] = relationship(back_populates="annotation_queue_assignment")
    user: Mapped["User"] = relationship(back_populates="annotation_queue_assignment")
    queue: Mapped["AnnotationQueue"] = relationship(back_populates="annotation_queue_assignment")

class CronJobs(Base):
    __tablename__ = "cron_jobs"

    name: Mapped[str] = mapped_column(primary_key=True)
    last_run: Mapped[Optional[datetime]]
    job_started_at: Mapped[Optional[datetime]]
    state: Mapped[Optional[str]]

class Dataset(Base):
    __tablename__ = "datasets"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="datasets_project_id_fkey"), primary_key=True)
    name: Mapped[str]
    description: Mapped[Optional[str]]
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))
    remote_experiment_url: Mapped[Optional[str]]
    remote_experiment_payload: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    remote_experiment_enabled: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    remote_experiment_secret_key: Mapped[Optional[str]]
    remote_experiment_display_secret_key: Mapped[Optional[str]]
    remote_experiment_request_headers: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    remote_experiment_display_headers: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    input_schema: Mapped[Optional[dict[str, Any]]] = mapped_column(JSON(none_as_null=True))
    expected_output_schema: Mapped[Optional[dict[str, Any]]] = mapped_column(JSON(none_as_null=True))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("datasets_created_at_idx", "created_at"),
        Index("datasets_updated_at_idx", "updated_at"),
        Index("datasets_project_id_name_key", "project_id", "name", unique=True),
    )

    project: Mapped["Project"] = relationship(back_populates="dataset")
    dataset_items: Mapped[list["DatasetItem"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="dataset")
    dataset_runs: Mapped[list["DatasetRuns"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="dataset")

class DatasetItem(Base):
    __tablename__ = "dataset_items"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column(primary_key=True)
    status: Mapped[Optional[str]] = mapped_column(Enum("ACTIVE", "ARCHIVED", name="DatasetStatus"), default="ACTIVE", server_default=text("'ACTIVE'"))
    input: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    expected_output: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))
    source_trace_id: Mapped[Optional[str]]
    source_observation_id: Mapped[Optional[str]]
    dataset_id: Mapped[str]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    valid_from: Mapped[datetime] = mapped_column(primary_key=True, default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    valid_to: Mapped[Optional[datetime]]
    is_deleted: Mapped[bool] = mapped_column(default=False, server_default=text("false"))

    __table_args__ = (
        Index("dataset_items_project_id_valid_to_idx", "project_id", "valid_to"),
        Index("dataset_items_project_id_id_valid_from_idx", "project_id", "id", "valid_from"),
        Index("dataset_items_source_trace_id_idx", "source_trace_id"),
        Index("dataset_items_source_observation_id_idx", "source_observation_id"),
        Index("dataset_items_dataset_id_idx", "dataset_id"),
        Index("dataset_items_created_at_idx", "created_at"),
        Index("dataset_items_updated_at_idx", "updated_at"),
        ForeignKeyConstraint(["dataset_id", "project_id"], ["datasets.id", "datasets.project_id"], ondelete="CASCADE", onupdate="CASCADE", name="dataset_items_dataset_id_project_id_fkey"),
    )

    dataset: Mapped["Dataset"] = relationship(back_populates="dataset_items")

class DatasetRuns(Base):
    __tablename__ = "dataset_runs"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    description: Mapped[Optional[str]]
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))
    dataset_id: Mapped[str]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("dataset_runs_dataset_id_idx", "dataset_id"),
        Index("dataset_runs_created_at_idx", "created_at"),
        Index("dataset_runs_updated_at_idx", "updated_at"),
        Index("dataset_runs_dataset_id_project_id_name_key", "dataset_id", "project_id", "name", unique=True),
        ForeignKeyConstraint(["dataset_id", "project_id"], ["datasets.id", "datasets.project_id"], ondelete="CASCADE", onupdate="CASCADE", name="dataset_runs_dataset_id_project_id_fkey"),
    )

    dataset: Mapped["Dataset"] = relationship(back_populates="dataset_runs")

class Comment(Base):
    __tablename__ = "comments"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="comments_project_id_fkey"))
    object_type: Mapped[str] = mapped_column(Enum("TRACE", "OBSERVATION", "SESSION", "PROMPT", name="CommentObjectType"))
    object_id: Mapped[str]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    content: Mapped[str]
    author_user_id: Mapped[Optional[str]]
    data_field: Mapped[Optional[str]]
    path: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    range_start: Mapped[list[int]] = mapped_column(ARRAY(Integer), nullable=True, default=lambda: [], server_default=text("ARRAY[]::integer[]"))
    range_end: Mapped[list[int]] = mapped_column(ARRAY(Integer), nullable=True, default=lambda: [], server_default=text("ARRAY[]::integer[]"))

    __table_args__ = (
        Index("comments_project_id_object_type_object_id_idx", "project_id", "object_type", "object_id"),
    )

    project: Mapped["Project"] = relationship(back_populates="comment")
    reactions: Mapped[list["CommentReaction"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="comment")

class CommentReaction(Base):
    __tablename__ = "comment_reactions"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="comment_reactions_project_id_fkey"))
    comment_id: Mapped[str] = mapped_column(ForeignKey("comments.id", ondelete="CASCADE", onupdate="CASCADE", name="comment_reactions_comment_id_fkey"))
    user_id: Mapped[str] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="comment_reactions_user_id_fkey"))
    emoji: Mapped[str]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("comment_reactions_comment_id_user_id_emoji_key", "comment_id", "user_id", "emoji", unique=True),
    )

    project: Mapped["Project"] = relationship(back_populates="comment_reactions")
    comment: Mapped["Comment"] = relationship(back_populates="reactions")
    user: Mapped["User"] = relationship(back_populates="comment_reactions")

class NotificationPreference(Base):
    __tablename__ = "notification_preferences"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[str] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="notification_preferences_user_id_fkey"))
    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="notification_preferences_project_id_fkey"))
    channel: Mapped[str] = mapped_column(Enum("EMAIL", name="NotificationChannel"))
    type: Mapped[str] = mapped_column(Enum("COMMENT_MENTION", name="NotificationType"))
    enabled: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("notification_preferences_user_id_project_id_channel_type_key", "user_id", "project_id", "channel", "type", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="notification_preferences")
    project: Mapped["Project"] = relationship(back_populates="notification_preference")

class Prompt(Base):
    __tablename__ = "prompts"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="prompts_project_id_fkey"))
    created_by: Mapped[str]
    prompt: Mapped[dict[str, Any]] = mapped_column(JSONB)
    name: Mapped[str]
    version: Mapped[int]
    type: Mapped[str] = mapped_column(default="text", server_default=text("'text'"))
    is_active: Mapped[Optional[bool]]
    config: Mapped[dict[str, Any]] = mapped_column(JSON, default=lambda: {}, server_default=text("'{}'"))
    tags: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    labels: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    commit_message: Mapped[Optional[str]]

    __table_args__ = (
        Index("prompts_project_id_id_idx", "project_id", "id"),
        Index("prompts_created_at_idx", "created_at"),
        Index("prompts_updated_at_idx", "updated_at"),
        Index("prompts_tags_idx", "tags"),
        Index("prompts_project_id_name_version_key", "project_id", "name", "version", unique=True),
    )

    project: Mapped["Project"] = relationship(back_populates="prompt")
    prompt_dependency: Mapped[list["PromptDependency"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="parent")

class PromptDependency(Base):
    __tablename__ = "prompt_dependencies"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="prompt_dependencies_project_id_fkey"))
    parent_id: Mapped[str] = mapped_column(ForeignKey("prompts.id", ondelete="CASCADE", onupdate="CASCADE", name="prompt_dependencies_parent_id_fkey"))
    child_name: Mapped[str]
    child_label: Mapped[Optional[str]]
    child_version: Mapped[Optional[int]]

    __table_args__ = (
        Index("prompt_dependencies_project_id_parent_id", "project_id", "parent_id"),
        Index("prompt_dependencies_project_id_child_name", "project_id", "child_name"),
    )

    project: Mapped["Project"] = relationship(back_populates="prompt_dependency")
    parent: Mapped["Prompt"] = relationship(back_populates="prompt_dependency")

class PromptProtectedLabels(Base):
    __tablename__ = "prompt_protected_labels"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="prompt_protected_labels_project_id_fkey"))
    label: Mapped[str]

    __table_args__ = (
        Index("prompt_protected_labels_project_id_label_key", "project_id", "label", unique=True),
    )

    project: Mapped["Project"] = relationship(back_populates="prompt_protected_labels")

class Skill(Base):
    __tablename__ = "skills"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="skills_project_id_fkey"))
    created_by: Mapped[str]
    name: Mapped[str]
    description: Mapped[str]
    frontmatter: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    version: Mapped[int]
    tags: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    labels: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    commit_message: Mapped[Optional[str]]

    __table_args__ = (
        Index("skills_created_at_idx", "created_at"),
        Index("skills_updated_at_idx", "updated_at"),
        Index("skills_tags_idx", "tags"),
        Index("skills_labels_idx", "labels"),
        Index("skills_project_id_name_version_key", "project_id", "name", "version", unique=True),
    )

    project: Mapped["Project"] = relationship(back_populates="skill")
    files: Mapped[list["SkillFile"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="skill")

class SkillFile(Base):
    __tablename__ = "skill_files"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="skill_files_project_id_fkey"))
    skill_id: Mapped[str] = mapped_column(ForeignKey("skills.id", ondelete="CASCADE", onupdate="CASCADE", name="skill_files_skill_id_fkey"))
    blob_id: Mapped[str] = mapped_column(ForeignKey("skill_blobs.id", ondelete="RESTRICT", onupdate="CASCADE", name="skill_files_blob_id_fkey"))
    path: Mapped[str]

    __table_args__ = (
        Index("skill_files_project_id_blob_id_idx", "project_id", "blob_id"),
        Index("skill_files_project_id_skill_id_path_key", "project_id", "skill_id", "path", unique=True),
    )

    project: Mapped["Project"] = relationship(back_populates="skill_file")
    skill: Mapped["Skill"] = relationship(back_populates="files")
    blob: Mapped["SkillBlob"] = relationship(back_populates="files")

class SkillBlob(Base):
    __tablename__ = "skill_blobs"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="skill_blobs_project_id_fkey"))
    sha256_hash: Mapped[str] = mapped_column("sha_256_hash", CHAR(44))
    content_type: Mapped[str]
    content_length: Mapped[int]
    content: Mapped[str]

    __table_args__ = (
        Index("skill_blobs_project_id_sha_256_hash_key", "project_id", "sha_256_hash", unique=True),
    )

    project: Mapped["Project"] = relationship(back_populates="skill_blob")
    files: Mapped[list["SkillFile"]] = relationship(passive_deletes="all", back_populates="blob")

class Model(Base):
    __tablename__ = "models"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    project_id: Mapped[Optional[str]] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="models_project_id_fkey"))
    model_name: Mapped[str]
    match_pattern: Mapped[str]
    start_date: Mapped[Optional[datetime]]
    input_price: Mapped[Optional[DecimalType]]
    output_price: Mapped[Optional[DecimalType]]
    total_price: Mapped[Optional[DecimalType]]
    unit: Mapped[Optional[str]]
    tokenizer_id: Mapped[Optional[str]]
    tokenizer_config: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))

    __table_args__ = (
        Index("models_model_name_idx", "model_name"),
        Index("models_project_id_model_name_start_date_unit_key", "project_id", "model_name", "start_date", "unit", unique=True),
    )

    project: Mapped[Optional["Project"]] = relationship(back_populates="model")
    price: Mapped[list["Price"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="model")
    pricing_tiers: Mapped[list["PricingTier"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="model")

class Price(Base):
    __tablename__ = "prices"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    model_id: Mapped[str] = mapped_column(ForeignKey("models.id", ondelete="CASCADE", onupdate="CASCADE", name="prices_model_id_fkey"))
    project_id: Mapped[Optional[str]] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="prices_project_id_fkey"))
    pricing_tier_id: Mapped[str] = mapped_column(ForeignKey("pricing_tiers.id", ondelete="CASCADE", onupdate="CASCADE", name="prices_pricing_tier_id_fkey"))
    usage_type: Mapped[str]
    price: Mapped[DecimalType]

    __table_args__ = (
        Index("prices_pricing_tier_id_idx", "pricing_tier_id"),
        Index("prices_model_id_usage_type_pricing_tier_id_key", "model_id", "usage_type", "pricing_tier_id", unique=True),
    )

    model: Mapped["Model"] = relationship(back_populates="price")
    project: Mapped[Optional["Project"]] = relationship(back_populates="price")
    pricing_tier: Mapped["PricingTier"] = relationship(back_populates="prices")

class PricingTier(Base):
    __tablename__ = "pricing_tiers"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    model_id: Mapped[str] = mapped_column(ForeignKey("models.id", ondelete="CASCADE", onupdate="CASCADE", name="pricing_tiers_model_id_fkey"))
    name: Mapped[str]
    is_default: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    priority: Mapped[int]
    conditions: Mapped[dict[str, Any]] = mapped_column(JSONB)

    __table_args__ = (
        Index("pricing_tiers_model_id_priority_key", "model_id", "priority", unique=True),
        Index("pricing_tiers_model_id_name_key", "model_id", "name", unique=True),
    )

    model: Mapped["Model"] = relationship(back_populates="pricing_tiers")
    prices: Mapped[list["Price"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="pricing_tier")

class AuditLog(Base):
    __tablename__ = "audit_logs"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    type: Mapped[str] = mapped_column(Enum("USER", "API_KEY", name="AuditLogRecordType"), default="USER", server_default=text("'USER'"))
    api_key_id: Mapped[Optional[str]]
    user_id: Mapped[Optional[str]]
    org_id: Mapped[str]
    user_org_role: Mapped[Optional[str]]
    project_id: Mapped[Optional[str]]
    user_project_role: Mapped[Optional[str]]
    resource_type: Mapped[str]
    resource_id: Mapped[str]
    action: Mapped[str]
    before: Mapped[Optional[str]]
    after: Mapped[Optional[str]]

    __table_args__ = (
        Index("audit_logs_project_id_idx", "project_id"),
        Index("audit_logs_api_key_id_idx", "api_key_id"),
        Index("audit_logs_user_id_idx", "user_id"),
        Index("audit_logs_org_id_idx", "org_id"),
        Index("audit_logs_created_at_idx", "created_at"),
        Index("audit_logs_updated_at_idx", "updated_at"),
    )

class AuditLogStaging(Base):
    __tablename__ = "audit_log_staging"

    id: Mapped[uuid_mod.UUID] = mapped_column(Uuid, primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    payload: Mapped[dict[str, Any]] = mapped_column(JSONB)
    failed_at: Mapped[Optional[datetime]]
    last_error: Mapped[Optional[str]]

class EvalTemplate(Base):
    __tablename__ = "eval_templates"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    project_id: Mapped[Optional[str]] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="eval_templates_project_id_fkey"))
    name: Mapped[str]
    version: Mapped[int]
    prompt: Mapped[Optional[str]]
    type: Mapped[str] = mapped_column(Enum("LLM_AS_JUDGE", "CODE", "DECISION_MODEL", "FACET", name="EvalTemplateType"), default="LLM_AS_JUDGE", server_default=text("'LLM_AS_JUDGE'"))
    partner: Mapped[Optional[str]]
    model: Mapped[Optional[str]]
    provider: Mapped[Optional[str]]
    model_params: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    vars: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    output_definition: Mapped[Optional[dict[str, Any]]] = mapped_column("output_schema", JSONB(none_as_null=True))
    source_code: Mapped[Optional[str]] = mapped_column(String(262144))
    source_code_language: Mapped[Optional[str]] = mapped_column(Enum("PYTHON", "TYPESCRIPT", name="EvalTemplateSourceCodeLanguage"))

    __table_args__ = (
        Index("eval_templates_project_id_id_idx", "project_id", "id"),
        Index("eval_templates_project_id_name_version_key", "project_id", "name", "version", unique=True),
    )

    project: Mapped[Optional["Project"]] = relationship(back_populates="eval_template")
    job_configuration: Mapped[list["JobConfiguration"]] = relationship(back_populates="eval_template")

class Evaluator(Base):
    __tablename__ = "evaluators"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="evaluators_project_id_fkey"))
    name: Mapped[str]
    type: Mapped[str] = mapped_column(Enum("LLM_AS_JUDGE", "CODE", "DECISION_MODEL", "FACET", name="EvalTemplateType"))
    is_built_in: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    description: Mapped[Optional[str]]
    created_by_user_id: Mapped[Optional[str]] = mapped_column(ForeignKey("users.id", ondelete="SET NULL", onupdate="CASCADE", name="evaluators_created_by_user_id_fkey"))
    blocked_at: Mapped[Optional[datetime]]
    block_reason: Mapped[Optional[str]] = mapped_column(Enum("LLM_CONNECTION_AUTH_INVALID", "LLM_CONNECTION_BILLING_EXHAUSTED", "LLM_CONNECTION_ENDPOINT_UNREACHABLE", "LLM_CONNECTION_MISSING", "DEFAULT_EVAL_MODEL_MISSING", "EVAL_MODEL_CONFIG_INVALID", "EVAL_MODEL_UNAVAILABLE", "PROVIDER_ACCOUNT_NOT_READY", name="EvaluatorBlockReason"))
    block_message: Mapped[Optional[str]]

    __table_args__ = (
        Index("evaluators_project_id_created_at_idx", "project_id", "created_at"),
    )

    project: Mapped["Project"] = relationship(back_populates="evaluator")
    created_by_user: Mapped[Optional["User"]] = relationship(back_populates="evaluators_created")
    versions: Mapped[list["EvaluatorVersion"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="evaluator")
    assignments: Mapped[list["EvaluationRuleEvaluatorAssignment"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="evaluator")

class EvaluatorVersion(Base):
    __tablename__ = "evaluator_versions"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    evaluator_id: Mapped[str] = mapped_column(ForeignKey("evaluators.id", ondelete="CASCADE", onupdate="CASCADE", name="evaluator_versions_evaluator_id_fkey"))
    version: Mapped[int]
    created_by_user_id: Mapped[Optional[str]] = mapped_column(ForeignKey("users.id", ondelete="SET NULL", onupdate="CASCADE", name="evaluator_versions_created_by_user_id_fkey"))
    prompt: Mapped[Optional[str]]
    prompt_messages: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    partner: Mapped[Optional[str]]
    model: Mapped[Optional[str]]
    provider: Mapped[Optional[str]]
    model_params: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    vars: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    variable_mapping: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    output_definition: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    source_code: Mapped[Optional[str]] = mapped_column(String(262144))
    source_code_language: Mapped[Optional[str]] = mapped_column(Enum("PYTHON", "TYPESCRIPT", name="EvaluatorSourceCodeLanguage"))
    questions: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))

    __table_args__ = (
        Index("evaluator_versions_evaluator_id_version_key", "evaluator_id", "version", unique=True),
    )

    evaluator: Mapped["Evaluator"] = relationship(back_populates="versions")
    created_by_user: Mapped[Optional["User"]] = relationship(back_populates="evaluator_versions_created")
    topic_clustering_runs: Mapped[list["TopicClusteringRun"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="facet")

class JobConfiguration(Base):
    __tablename__ = "job_configurations"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="job_configurations_project_id_fkey"))
    job_type: Mapped[str] = mapped_column(Enum("EVAL", name="JobType"))
    status: Mapped[str] = mapped_column(Enum("ACTIVE", "INACTIVE", name="JobConfigState"), default="ACTIVE", server_default=text("'ACTIVE'"))
    blocked_at: Mapped[Optional[datetime]]
    block_reason: Mapped[Optional[str]] = mapped_column(Enum("LLM_CONNECTION_AUTH_INVALID", "LLM_CONNECTION_BILLING_EXHAUSTED", "LLM_CONNECTION_ENDPOINT_UNREACHABLE", "LLM_CONNECTION_MISSING", "DEFAULT_EVAL_MODEL_MISSING", "EVAL_MODEL_CONFIG_INVALID", "EVAL_MODEL_UNAVAILABLE", "PROVIDER_ACCOUNT_NOT_READY", name="EvaluatorBlockReason"))
    block_message: Mapped[Optional[str]]
    eval_template_id: Mapped[Optional[str]] = mapped_column(ForeignKey("eval_templates.id", ondelete="SET NULL", onupdate="CASCADE", name="job_configurations_eval_template_id_fkey"))
    score_name: Mapped[str]
    filter: Mapped[dict[str, Any]] = mapped_column(JSONB)
    target_object: Mapped[str]
    variable_mapping: Mapped[dict[str, Any]] = mapped_column(JSONB)
    sampling: Mapped[DecimalType]
    delay: Mapped[int]
    time_scope: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: ["NEW"], server_default=text("ARRAY['NEW']"))

    __table_args__ = (
        Index("job_configurations_project_id_id_idx", "project_id", "id"),
    )

    project: Mapped["Project"] = relationship(back_populates="job_configuration")
    eval_template: Mapped[Optional["EvalTemplate"]] = relationship(back_populates="job_configuration")

class EvaluationRule(Base):
    __tablename__ = "evaluation_rules"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="evaluation_rules_project_id_fkey"))
    created_by_user_id: Mapped[Optional[str]] = mapped_column(ForeignKey("users.id", ondelete="SET NULL", onupdate="CASCADE", name="evaluation_rules_created_by_user_id_fkey"))
    name: Mapped[str]
    status: Mapped[str] = mapped_column(Enum("ACTIVE", "INACTIVE", name="JobConfigState"), default="ACTIVE", server_default=text("'ACTIVE'"))
    target_object: Mapped[str]
    filter: Mapped[dict[str, Any]] = mapped_column(JSONB)
    sampling: Mapped[DecimalType]
    delay: Mapped[int]
    idle_time: Mapped[Optional[int]]
    time_scope: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: ["NEW"], server_default=text("ARRAY['NEW']"))

    __table_args__ = (
        Index("evaluation_rules_project_id_updated_at_idx", "project_id", "updated_at"),
    )

    project: Mapped["Project"] = relationship(back_populates="evaluation_rule")
    created_by_user: Mapped[Optional["User"]] = relationship(back_populates="evaluation_rules_created")
    assignments: Mapped[list["EvaluationRuleEvaluatorAssignment"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="evaluation_rule")

class TracingReadinessSettings(Base):
    __tablename__ = "tracing_readiness_settings"

    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="tracing_readiness_settings_project_id_fkey"), primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    created_by: Mapped[Optional[str]] = mapped_column(ForeignKey("users.id", ondelete="SET NULL", onupdate="CASCADE", name="tracing_readiness_settings_created_by_fkey"))
    updated_by: Mapped[Optional[str]] = mapped_column(ForeignKey("users.id", ondelete="SET NULL", onupdate="CASCADE", name="tracing_readiness_settings_updated_by_fkey"))
    trace_idle_ms: Mapped[Optional[int]]

    project: Mapped["Project"] = relationship(back_populates="tracing_readiness_settings")
    created_by_user: Mapped[Optional["User"]] = relationship(foreign_keys=[created_by], back_populates="tracing_readiness_created")
    updated_by_user: Mapped[Optional["User"]] = relationship(foreign_keys=[updated_by], back_populates="tracing_readiness_updated")

class EvaluationRuleEvaluatorAssignment(Base):
    __tablename__ = "evaluation_rule_evaluator_assignments"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="evaluation_rule_evaluator_assignments_project_id_fkey"))
    evaluation_rule_id: Mapped[str] = mapped_column(ForeignKey("evaluation_rules.id", ondelete="CASCADE", onupdate="CASCADE", name="evaluation_rule_evaluator_assignments_evaluation_rule_id_fkey"))
    evaluator_id: Mapped[str] = mapped_column(ForeignKey("evaluators.id", ondelete="CASCADE", onupdate="CASCADE", name="evaluation_rule_evaluator_assignments_evaluator_id_fkey"))
    variable_mapping: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))

    __table_args__ = (
        Index("evaluation_rule_assignments_project_id_idx", "project_id"),
        Index("evaluation_rule_assignments_evaluator_id_idx", "evaluator_id"),
        Index("evaluation_rule_assignments_rule_evaluator_key", "evaluation_rule_id", "evaluator_id", unique=True),
    )

    project: Mapped["Project"] = relationship(back_populates="evaluation_rule_evaluator_assignment")
    evaluation_rule: Mapped["EvaluationRule"] = relationship(back_populates="assignments")
    evaluator: Mapped["Evaluator"] = relationship(back_populates="assignments")

class JobExecution(Base):
    __tablename__ = "job_executions"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="job_executions_project_id_fkey"))
    job_configuration_id: Mapped[str]
    job_template_id: Mapped[Optional[str]]
    status: Mapped[str] = mapped_column(Enum("COMPLETED", "ERROR", "PENDING", "CANCELLED", "DELAYED", name="JobExecutionStatus"))
    start_time: Mapped[Optional[datetime]]
    end_time: Mapped[Optional[datetime]]
    error: Mapped[Optional[str]]
    job_input_trace_id: Mapped[Optional[str]]
    job_input_trace_timestamp: Mapped[Optional[datetime]]
    job_input_observation_id: Mapped[Optional[str]]
    job_input_dataset_item_id: Mapped[Optional[str]]
    job_input_dataset_item_valid_from: Mapped[Optional[datetime]]
    job_output_score_id: Mapped[Optional[str]]
    execution_trace_id: Mapped[Optional[str]]

    __table_args__ = (
        Index("job_executions_project_id_job_configuration_id_job_input_tr_idx", "project_id", "job_configuration_id", "job_input_trace_id"),
        Index("job_executions_project_id_job_output_score_id_idx", "project_id", "job_output_score_id"),
        Index("job_executions_job_configuration_id_idx", "job_configuration_id"),
    )

    project: Mapped["Project"] = relationship(back_populates="job_execution")

class DefaultLlmModel(Base):
    __tablename__ = "default_llm_models"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="default_llm_models_project_id_fkey"))
    llm_api_key_id: Mapped[str] = mapped_column(ForeignKey("llm_api_keys.id", ondelete="CASCADE", onupdate="CASCADE", name="default_llm_models_llm_api_key_id_fkey"))
    provider: Mapped[str]
    adapter: Mapped[str]
    model: Mapped[str]
    model_params: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))

    __table_args__ = (
        Index("default_llm_models_project_id_key", "project_id", unique=True),
    )

    project: Mapped["Project"] = relationship(back_populates="default_llm_model")
    llm_api_key: Mapped["LlmApiKeys"] = relationship(back_populates="default_llm_model")

class TopicConfig(Base):
    __tablename__ = "topic_configs"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    created_by: Mapped[Optional[str]] = mapped_column(ForeignKey("users.id", ondelete="SET NULL", onupdate="CASCADE", name="topic_configs_created_by_fkey"))
    updated_by: Mapped[Optional[str]] = mapped_column(ForeignKey("users.id", ondelete="SET NULL", onupdate="CASCADE", name="topic_configs_updated_by_fkey"))
    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="topic_configs_project_id_fkey"))
    enabled: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    blocked_at: Mapped[Optional[datetime]]
    block_reason: Mapped[Optional[str]] = mapped_column(Enum("LLM_CONNECTION_AUTH_INVALID", "LLM_CONNECTION_BILLING_EXHAUSTED", "LLM_CONNECTION_ENDPOINT_UNREACHABLE", "LLM_CONNECTION_MISSING", "DEFAULT_EVAL_MODEL_MISSING", "EVAL_MODEL_CONFIG_INVALID", "EVAL_MODEL_UNAVAILABLE", "PROVIDER_ACCOUNT_NOT_READY", name="EvaluatorBlockReason"))
    block_message: Mapped[Optional[str]]
    summary_llm_api_key_id: Mapped[Optional[str]] = mapped_column(ForeignKey("llm_api_keys.id", ondelete="SET NULL", onupdate="CASCADE", name="topic_configs_summary_llm_api_key_id_fkey"))
    summary_model: Mapped[Optional[str]]
    embedding_llm_api_key_id: Mapped[Optional[str]] = mapped_column(ForeignKey("llm_api_keys.id", ondelete="SET NULL", onupdate="CASCADE", name="topic_configs_embedding_llm_api_key_id_fkey"))
    embedding_model: Mapped[Optional[str]]
    embedding_dimensions: Mapped[int] = mapped_column(default=1024, server_default=text("1024"))
    clustering_llm_api_key_id: Mapped[Optional[str]] = mapped_column(ForeignKey("llm_api_keys.id", ondelete="SET NULL", onupdate="CASCADE", name="topic_configs_clustering_llm_api_key_id_fkey"))
    clustering_model: Mapped[Optional[str]]

    __table_args__ = (
        Index("topic_configs_project_id_key", "project_id", unique=True),
        Index("topic_configs_summary_llm_api_key_id_idx", "summary_llm_api_key_id"),
        Index("topic_configs_embedding_llm_api_key_id_idx", "embedding_llm_api_key_id"),
        Index("topic_configs_clustering_llm_api_key_id_idx", "clustering_llm_api_key_id"),
    )

    created_by_user: Mapped[Optional["User"]] = relationship(foreign_keys=[created_by], back_populates="topic_configs_created")
    updated_by_user: Mapped[Optional["User"]] = relationship(foreign_keys=[updated_by], back_populates="topic_configs_updated")
    project: Mapped["Project"] = relationship(back_populates="topic_config")
    summary_llm_api_key: Mapped[Optional["LlmApiKeys"]] = relationship(foreign_keys=[summary_llm_api_key_id], back_populates="topic_summary_configs")
    embedding_llm_api_key: Mapped[Optional["LlmApiKeys"]] = relationship(foreign_keys=[embedding_llm_api_key_id], back_populates="topic_embedding_configs")
    clustering_llm_api_key: Mapped[Optional["LlmApiKeys"]] = relationship(foreign_keys=[clustering_llm_api_key_id], back_populates="topic_clustering_configs")

class SsoConfig(Base):
    __tablename__ = "sso_configs"

    domain: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    auth_provider: Mapped[str]
    auth_config: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))

class VerifiedDomain(Base):
    __tablename__ = "verified_domains"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    organization_id: Mapped[str] = mapped_column(ForeignKey("organizations.id", ondelete="CASCADE", onupdate="CASCADE", name="verified_domains_organization_id_fkey"))
    domain: Mapped[str]
    verification_token: Mapped[str] = mapped_column(default=lambda: str(uuid_mod.uuid4()))
    verified_at: Mapped[Optional[datetime]]
    created_by_user_id: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("verified_domains_verification_token_key", "verification_token", unique=True),
        Index("verified_domains_organization_id_idx", "organization_id"),
        Index("verified_domains_organization_id_domain_key", "organization_id", "domain", unique=True),
    )

    organization: Mapped["Organization"] = relationship(back_populates="verified_domains")

class PosthogIntegration(Base):
    __tablename__ = "posthog_integrations"

    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="posthog_integrations_project_id_fkey"), primary_key=True)
    encrypted_posthog_api_key: Mapped[str]
    posthog_host_name: Mapped[str]
    last_sync_at: Mapped[Optional[datetime]]
    enabled: Mapped[bool]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    export_source: Mapped[str] = mapped_column(Enum("TRACES_OBSERVATIONS", "TRACES_OBSERVATIONS_EVENTS", "EVENTS", name="AnalyticsIntegrationExportSource"), default="TRACES_OBSERVATIONS", server_default=text("'TRACES_OBSERVATIONS'"))
    backfill: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    last_error: Mapped[Optional[str]]
    last_error_at: Mapped[Optional[datetime]]

    project: Mapped["Project"] = relationship(back_populates="posthog_integration")

class MixpanelIntegration(Base):
    __tablename__ = "mixpanel_integrations"

    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="mixpanel_integrations_project_id_fkey"), primary_key=True)
    encrypted_mixpanel_project_token: Mapped[str]
    mixpanel_region: Mapped[str]
    last_sync_at: Mapped[Optional[datetime]]
    enabled: Mapped[bool]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    export_source: Mapped[str] = mapped_column(Enum("TRACES_OBSERVATIONS", "TRACES_OBSERVATIONS_EVENTS", "EVENTS", name="AnalyticsIntegrationExportSource"), default="TRACES_OBSERVATIONS", server_default=text("'TRACES_OBSERVATIONS'"))
    backfill: Mapped[bool] = mapped_column(default=True, server_default=text("true"))

    project: Mapped["Project"] = relationship(back_populates="mixpanel_integration")

class BlobStorageIntegration(Base):
    __tablename__ = "blob_storage_integrations"

    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="blob_storage_integrations_project_id_fkey"), primary_key=True)
    type: Mapped[str] = mapped_column(Enum("S3", "S3_COMPATIBLE", "AZURE_BLOB_STORAGE", "GOOGLE_CLOUD_STORAGE", name="BlobStorageIntegrationType"))
    bucket_name: Mapped[str]
    prefix: Mapped[str]
    access_key_id: Mapped[Optional[str]]
    secret_access_key: Mapped[Optional[str]]
    region: Mapped[str]
    endpoint: Mapped[Optional[str]]
    force_path_style: Mapped[bool]
    next_sync_at: Mapped[Optional[datetime]]
    last_sync_at: Mapped[Optional[datetime]]
    enabled: Mapped[bool]
    export_frequency: Mapped[str]
    file_type: Mapped[str] = mapped_column(Enum("JSON", "CSV", "JSONL", "PARQUET", name="BlobStorageIntegrationFileType"), default="CSV", server_default=text("'CSV'"))
    export_mode: Mapped[str] = mapped_column(Enum("FULL_HISTORY", "FROM_TODAY", "FROM_CUSTOM_DATE", name="BlobStorageExportMode"), default="FULL_HISTORY", server_default=text("'FULL_HISTORY'"))
    export_start_date: Mapped[Optional[datetime]]
    export_source: Mapped[str] = mapped_column(Enum("TRACES_OBSERVATIONS", "TRACES_OBSERVATIONS_EVENTS", "EVENTS", name="AnalyticsIntegrationExportSource"), default="TRACES_OBSERVATIONS", server_default=text("'TRACES_OBSERVATIONS'"))
    export_field_groups: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: ["core", "basic", "time", "io", "metadata", "model", "usage", "prompt", "metrics", "tools", "trace_context"], server_default=text("ARRAY['core', 'basic', 'time', 'io', 'metadata', 'model', 'usage', 'prompt', 'metrics', 'tools', 'trace_context']"))
    compressed: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    export_tuning: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    run_started_at: Mapped[Optional[datetime]]
    backfill: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    last_error: Mapped[Optional[str]]
    last_error_at: Mapped[Optional[datetime]]
    last_failure_notification_sent_at: Mapped[Optional[datetime]]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    project: Mapped["Project"] = relationship(back_populates="blob_storage_integration")

class ExternalMediaStorageIntegration(Base):
    __tablename__ = "external_media_storage_integrations"

    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="external_media_storage_integrations_project_id_fkey"), primary_key=True)
    type: Mapped[str] = mapped_column(Enum("S3", "S3_COMPATIBLE", name="ExternalMediaStorageIntegrationType"))
    bucket_name: Mapped[str]
    prefix: Mapped[Optional[str]]
    access_key_id: Mapped[Optional[str]]
    secret_access_key: Mapped[Optional[str]]
    region: Mapped[str]
    endpoint: Mapped[Optional[str]]
    force_path_style: Mapped[bool]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    project: Mapped["Project"] = relationship(back_populates="external_media_storage_integration")

class WebCalloutEndpoint(Base):
    __tablename__ = "web_callout_endpoints"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="web_callout_endpoints_project_id_fkey"))
    name: Mapped[str] = mapped_column(default="Default", server_default=text("'Default'"))
    url: Mapped[str]
    enabled: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    toast_message: Mapped[str] = mapped_column(default="Callout sent", server_default=text("'Callout sent'"))
    request_headers: Mapped[Optional[str]]
    request_header_keys: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))

    __table_args__ = (
        Index("web_callout_endpoints_project_id_idx", "project_id"),
        Index("web_callout_endpoints_project_id_enabled_idx", "project_id", "enabled"),
    )

    project: Mapped["Project"] = relationship(back_populates="web_callout_endpoints")

class BatchExport(Base):
    __tablename__ = "batch_exports"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="batch_exports_project_id_fkey"))
    user_id: Mapped[str]
    finished_at: Mapped[Optional[datetime]]
    expires_at: Mapped[Optional[datetime]]
    name: Mapped[str]
    status: Mapped[str]
    query: Mapped[dict[str, Any]] = mapped_column(JSONB)
    format: Mapped[str]
    url: Mapped[Optional[str]]
    log: Mapped[Optional[str]]

    __table_args__ = (
        Index("batch_exports_project_id_user_id_idx", "project_id", "user_id"),
        Index("batch_exports_status_idx", "status"),
    )

    project: Mapped["Project"] = relationship(back_populates="batch_export")

class BatchAction(Base):
    __tablename__ = "batch_actions"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="batch_actions_project_id_fkey"))
    user_id: Mapped[str]
    action_type: Mapped[str]
    table_name: Mapped[str]
    status: Mapped[str]
    finished_at: Mapped[Optional[datetime]]
    query: Mapped[dict[str, Any]] = mapped_column(JSONB)
    config: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    total_count: Mapped[Optional[int]]
    processed_count: Mapped[Optional[int]]
    failed_count: Mapped[Optional[int]]
    log: Mapped[Optional[str]]

    __table_args__ = (
        Index("batch_actions_project_id_user_id_idx", "project_id", "user_id"),
        Index("batch_actions_status_idx", "status"),
        Index("batch_actions_project_id_action_type_idx", "project_id", "action_type"),
    )

    project: Mapped["Project"] = relationship(back_populates="batch_action")

class Media(Base):
    __tablename__ = "media"

    id: Mapped[str] = mapped_column()
    sha256_hash: Mapped[str] = mapped_column("sha_256_hash", CHAR(44))
    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="media_project_id_fkey"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    uploaded_at: Mapped[Optional[datetime]]
    upload_http_status: Mapped[Optional[int]]
    upload_http_error: Mapped[Optional[str]]
    bucket_path: Mapped[str]
    bucket_name: Mapped[str]
    content_type: Mapped[str]
    content_length: Mapped[int] = mapped_column(BigInteger)

    __table_args__ = (
        Index("media_project_id_created_at_idx", "project_id", "created_at"),
        Index("media_project_id_id_key", "project_id", "id", unique=True),
        Index("media_project_id_sha_256_hash_key", "project_id", "sha_256_hash", unique=True),
    )

    __mapper_args__ = {"primary_key": [project_id, id]}

    project: Mapped["Project"] = relationship(back_populates="media")

class TraceMedia(Base):
    __tablename__ = "trace_media"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    media_id: Mapped[str]
    trace_id: Mapped[str]
    field: Mapped[str]
    origin: Mapped[str] = mapped_column(Enum("UNKNOWN", "CLIENT_UPLOAD", "INGESTION_MEDIA_EXTRACTION", "INGESTION_FIELD_OVERFLOW", name="MediaAssociationOrigin"), default="UNKNOWN", server_default=text("'UNKNOWN'"))

    __table_args__ = (
        Index("trace_media_project_id_media_id_idx", "project_id", "media_id"),
        Index("trace_media_project_id_trace_id_media_id_field_key", "project_id", "trace_id", "media_id", "field", unique=True),
    )

class ObservationMedia(Base):
    __tablename__ = "observation_media"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    media_id: Mapped[str]
    trace_id: Mapped[str]
    observation_id: Mapped[str]
    field: Mapped[str]
    origin: Mapped[str] = mapped_column(Enum("UNKNOWN", "CLIENT_UPLOAD", "INGESTION_MEDIA_EXTRACTION", "INGESTION_FIELD_OVERFLOW", name="MediaAssociationOrigin"), default="UNKNOWN", server_default=text("'UNKNOWN'"))

    __table_args__ = (
        Index("observation_media_project_id_media_id_idx", "project_id", "media_id"),
        Index("observation_media_project_id_trace_id_observation_id_media__key", "project_id", "trace_id", "observation_id", "media_id", "field", unique=True),
    )

class DatasetItemMedia(Base):
    __tablename__ = "dataset_item_media"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    media_id: Mapped[str]
    dataset_id: Mapped[str]
    dataset_item_id: Mapped[str]
    dataset_item_valid_from: Mapped[Optional[datetime]]
    field: Mapped[str]
    json_path: Mapped[Optional[str]]
    reference_string: Mapped[Optional[str]]

    __table_args__ = (
        Index("dataset_item_media_project_id_media_id_idx", "project_id", "media_id"),
        Index("dataset_item_media_project_id_dataset_id_idx", "project_id", "dataset_id"),
        Index("dataset_item_media_item_version_field_path_key", "project_id", "dataset_item_id", "dataset_item_valid_from", "field", "json_path", unique=True),
    )

class BillingMeterBackup(Base):
    __tablename__ = "billing_meter_backups"

    stripe_customer_id: Mapped[str] = mapped_column()
    meter_id: Mapped[str] = mapped_column()
    start_time: Mapped[datetime] = mapped_column()
    end_time: Mapped[datetime] = mapped_column()
    aggregated_value: Mapped[int]
    event_name: Mapped[str]
    org_id: Mapped[str]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("billing_meter_backups_stripe_customer_id_meter_id_start_tim_idx", "stripe_customer_id", "meter_id", "start_time", "end_time"),
        Index("billing_meter_backups_stripe_customer_id_meter_id_start_tim_key", "stripe_customer_id", "meter_id", "start_time", "end_time", unique=True),
    )

    __mapper_args__ = {"primary_key": [stripe_customer_id, meter_id, start_time, end_time]}

class LlmSchema(Base):
    __tablename__ = "llm_schemas"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="llm_schemas_project_id_fkey"))
    name: Mapped[str]
    description: Mapped[str]
    schema: Mapped[dict[str, Any]] = mapped_column(JSON)

    __table_args__ = (
        Index("llm_schemas_project_id_name_key", "project_id", "name", unique=True),
    )

    project: Mapped["Project"] = relationship(back_populates="llm_schema")

class LlmTool(Base):
    __tablename__ = "llm_tools"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="llm_tools_project_id_fkey"))
    name: Mapped[str]
    description: Mapped[str]
    parameters: Mapped[dict[str, Any]] = mapped_column(JSON)

    __table_args__ = (
        Index("llm_tools_project_id_name_key", "project_id", "name", unique=True),
    )

    project: Mapped["Project"] = relationship(back_populates="llm_tool")

class Dashboard(Base):
    __tablename__ = "dashboards"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    created_by: Mapped[Optional[str]] = mapped_column(ForeignKey("users.id", ondelete="SET NULL", onupdate="CASCADE", name="dashboards_created_by_fkey"))
    updated_by: Mapped[Optional[str]] = mapped_column(ForeignKey("users.id", ondelete="SET NULL", onupdate="CASCADE", name="dashboards_updated_by_fkey"))
    project_id: Mapped[Optional[str]] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="dashboards_project_id_fkey"))
    name: Mapped[str]
    description: Mapped[str]
    definition: Mapped[dict[str, Any]] = mapped_column(JSONB)
    filters: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: [], server_default=text("'[]'"))

    created_by_user: Mapped[Optional["User"]] = relationship(foreign_keys=[created_by], back_populates="dashboard_created")
    updated_by_user: Mapped[Optional["User"]] = relationship(foreign_keys=[updated_by], back_populates="dashboard_updated")
    project: Mapped[Optional["Project"]] = relationship(foreign_keys=[project_id], back_populates="dashboard")
    home_dashboard_projects: Mapped[list["Project"]] = relationship(foreign_keys="Project.home_dashboard_id", back_populates="home_dashboard")

class DashboardWidget(Base):
    __tablename__ = "dashboard_widgets"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    created_by: Mapped[Optional[str]] = mapped_column(ForeignKey("users.id", ondelete="SET NULL", onupdate="CASCADE", name="dashboard_widgets_created_by_fkey"))
    updated_by: Mapped[Optional[str]] = mapped_column(ForeignKey("users.id", ondelete="SET NULL", onupdate="CASCADE", name="dashboard_widgets_updated_by_fkey"))
    project_id: Mapped[Optional[str]] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="dashboard_widgets_project_id_fkey"))
    name: Mapped[str]
    description: Mapped[str]
    view: Mapped[str] = mapped_column(Enum("TRACES", "OBSERVATIONS", "SCORES_NUMERIC", "SCORES_CATEGORICAL", "SCORES_BOOLEAN", name="DashboardWidgetViews"))
    dimensions: Mapped[dict[str, Any]] = mapped_column(JSONB)
    metrics: Mapped[dict[str, Any]] = mapped_column(JSONB)
    filters: Mapped[dict[str, Any]] = mapped_column(JSONB)
    chart_type: Mapped[str] = mapped_column(Enum("LINE_TIME_SERIES", "AREA_TIME_SERIES", "BAR_TIME_SERIES", "HORIZONTAL_BAR", "VERTICAL_BAR", "PIE", "NUMBER", "HISTOGRAM", "PIVOT_TABLE", name="DashboardWidgetChartType"))
    chart_config: Mapped[dict[str, Any]] = mapped_column(JSONB)
    min_version: Mapped[int] = mapped_column(default=1, server_default=text("1"))

    created_by_user: Mapped[Optional["User"]] = relationship(foreign_keys=[created_by], back_populates="dashboard_widgets_created")
    updated_by_user: Mapped[Optional["User"]] = relationship(foreign_keys=[updated_by], back_populates="dashboard_widgets_updated")
    project: Mapped[Optional["Project"]] = relationship(back_populates="dashboard_widget")

class TableViewPreset(Base):
    __tablename__ = "table_view_presets"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="table_view_presets_project_id_fkey"))
    name: Mapped[str]
    table_name: Mapped[str]
    created_by: Mapped[Optional[str]] = mapped_column(ForeignKey("users.id", ondelete="SET NULL", onupdate="CASCADE", name="table_view_presets_created_by_fkey"))
    updated_by: Mapped[Optional[str]] = mapped_column(ForeignKey("users.id", ondelete="SET NULL", onupdate="CASCADE", name="table_view_presets_updated_by_fkey"))
    filters: Mapped[dict[str, Any]] = mapped_column(JSONB)
    column_order: Mapped[dict[str, Any]] = mapped_column(JSONB)
    column_visibility: Mapped[dict[str, Any]] = mapped_column(JSONB)
    search_query: Mapped[Optional[str]]
    order_by: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))

    __table_args__ = (
        Index("table_view_presets_project_id_table_name_name_key", "project_id", "table_name", "name", unique=True),
    )

    project: Mapped["Project"] = relationship(back_populates="table_view_preset")
    created_by_user: Mapped[Optional["User"]] = relationship(foreign_keys=[created_by], back_populates="table_view_preset_created")
    updated_by_user: Mapped[Optional["User"]] = relationship(foreign_keys=[updated_by], back_populates="table_view_preset_updated")

class DefaultView(Base):
    __tablename__ = "default_views"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="default_views_project_id_fkey"))
    user_id: Mapped[Optional[str]] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="default_views_user_id_fkey"))
    view_name: Mapped[str]
    view_id: Mapped[str]

    __table_args__ = (
        Index("default_views_project_id_view_name_idx", "project_id", "view_name"),
    )

    project: Mapped["Project"] = relationship(back_populates="default_view")
    user: Mapped[Optional["User"]] = relationship(back_populates="default_views")

class Action(Base):
    __tablename__ = "actions"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="actions_project_id_fkey"))
    type: Mapped[str] = mapped_column(Enum("WEBHOOK", "SLACK", "GITHUB_DISPATCH", "PAGERDUTY", name="ActionType"))
    config: Mapped[dict[str, Any]] = mapped_column(JSONB)

    __table_args__ = (
        Index("actions_project_id_idx", "project_id"),
    )

    project: Mapped["Project"] = relationship(back_populates="actions")
    automations: Mapped[list["Automation"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="action")
    automation_executions: Mapped[list["AutomationExecution"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="action")

class Trigger(Base):
    __tablename__ = "triggers"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="triggers_project_id_fkey"))
    event_source: Mapped[str] = mapped_column("eventSource")
    event_actions: Mapped[list[str]] = mapped_column("eventActions", ARRAY(Text), nullable=True)
    filter: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    status: Mapped[str] = mapped_column(Enum("ACTIVE", "INACTIVE", name="JobConfigState"), default="ACTIVE", server_default=text("'ACTIVE'"))

    __table_args__ = (
        Index("triggers_project_id_idx", "project_id"),
    )

    project: Mapped["Project"] = relationship(back_populates="triggers")
    automation_executions: Mapped[list["AutomationExecution"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="trigger")
    automations: Mapped[list["Automation"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="trigger")

class Automation(Base):
    __tablename__ = "automations"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    trigger_id: Mapped[str] = mapped_column(ForeignKey("triggers.id", ondelete="CASCADE", onupdate="CASCADE", name="automations_trigger_id_fkey"))
    action_id: Mapped[str] = mapped_column(ForeignKey("actions.id", ondelete="CASCADE", onupdate="CASCADE", name="automations_action_id_fkey"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="automations_project_id_fkey"))

    __table_args__ = (
        Index("automations_project_id_action_id_trigger_id_idx", "project_id", "action_id", "trigger_id"),
        Index("automations_project_id_name_idx", "project_id", "name"),
    )

    trigger: Mapped["Trigger"] = relationship(back_populates="automations")
    action: Mapped["Action"] = relationship(back_populates="automations")
    project: Mapped["Project"] = relationship(back_populates="automation")
    automation_execution: Mapped[list["AutomationExecution"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="automation")

class AutomationExecution(Base):
    __tablename__ = "automation_executions"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    source_id: Mapped[str]
    automation_id: Mapped[str] = mapped_column(ForeignKey("automations.id", ondelete="CASCADE", onupdate="CASCADE", name="automation_executions_automation_id_fkey"))
    trigger_id: Mapped[str] = mapped_column(ForeignKey("triggers.id", ondelete="CASCADE", onupdate="CASCADE", name="automation_executions_trigger_id_fkey"))
    action_id: Mapped[str] = mapped_column(ForeignKey("actions.id", ondelete="CASCADE", onupdate="CASCADE", name="automation_executions_action_id_fkey"))
    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="automation_executions_project_id_fkey"))
    status: Mapped[str] = mapped_column(Enum("COMPLETED", "ERROR", "PENDING", "CANCELLED", name="ActionExecutionStatus"), default="PENDING", server_default=text("'PENDING'"))
    input: Mapped[dict[str, Any]] = mapped_column(JSONB)
    output: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    started_at: Mapped[Optional[datetime]]
    finished_at: Mapped[Optional[datetime]]
    error: Mapped[Optional[str]]

    __table_args__ = (
        Index("automation_executions_trigger_id_idx", "trigger_id"),
        Index("automation_executions_action_id_idx", "action_id"),
        Index("automation_executions_project_id_idx", "project_id"),
    )

    automation: Mapped["Automation"] = relationship(back_populates="automation_execution")
    trigger: Mapped["Trigger"] = relationship(back_populates="automation_executions")
    action: Mapped["Action"] = relationship(back_populates="automation_executions")
    project: Mapped["Project"] = relationship(back_populates="automation_executions")

class Monitor(Base):
    __tablename__ = "monitors"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    created_by: Mapped[Optional[str]] = mapped_column(ForeignKey("users.id", ondelete="SET NULL", onupdate="CASCADE", name="monitors_created_by_fkey"))
    updated_by: Mapped[Optional[str]] = mapped_column(ForeignKey("users.id", ondelete="SET NULL", onupdate="CASCADE", name="monitors_updated_by_fkey"))
    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="monitors_project_id_fkey"))
    view: Mapped[str] = mapped_column(Enum("OBSERVATIONS", "SCORES_NUMERIC", "SCORES_CATEGORICAL", "SCORES_BOOLEAN", name="MonitorView"))
    filters: Mapped[dict[str, Any]] = mapped_column(JSONB)
    metric: Mapped[dict[str, Any]] = mapped_column(JSONB)
    window_ms: Mapped[int] = mapped_column(BigInteger)
    cadence_ms: Mapped[int] = mapped_column(BigInteger)
    threshold_operator: Mapped[str] = mapped_column(Enum("GT", "GTE", "LT", "LTE", "EQ", "NEQ", name="MonitorThresholdOperator"))
    alert_threshold: Mapped[DecimalType]
    warning_threshold: Mapped[Optional[DecimalType]]
    severity: Mapped[str] = mapped_column(Enum("PAUSED", "UNKNOWN", "NO_DATA", "OK", "WARNING", "ALERT", name="MonitorSeverity"), default="UNKNOWN", server_default=text("'UNKNOWN'"))
    severity_changed_at: Mapped[Optional[datetime]]
    no_data: Mapped[dict[str, Any]] = mapped_column(JSONB)
    renotify: Mapped[dict[str, Any]] = mapped_column(JSONB)
    status: Mapped[str] = mapped_column(Enum("PAUSED", "ACTIVE", "ERROR_BAD_QUERY", name="MonitorStatus"), default="ACTIVE", server_default=text("'ACTIVE'"))
    scheduler_batch_id: Mapped[int] = mapped_column(BigInteger)
    next_run_at: Mapped[Optional[datetime]]
    last_published_at: Mapped[Optional[datetime]]
    last_claimed_at: Mapped[Optional[datetime]]
    last_completed_at: Mapped[Optional[datetime]]
    name: Mapped[str]
    tags: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    trigger_ids: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    alerted_at: Mapped[Optional[datetime]]

    __table_args__ = (
        Index("monitors_project_id_idx", "project_id"),
        Index("monitors_scheduler_batch_id_idx", "scheduler_batch_id"),
        Index("monitors_scheduler_tick_idx", "next_run_at", "scheduler_batch_id"),
    )

    created_by_user: Mapped[Optional["User"]] = relationship(foreign_keys=[created_by], back_populates="monitors_created")
    updated_by_user: Mapped[Optional["User"]] = relationship(foreign_keys=[updated_by], back_populates="monitors_updated")
    project: Mapped["Project"] = relationship(back_populates="monitors")

class SlackIntegration(Base):
    __tablename__ = "slack_integrations"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="slack_integrations_project_id_fkey"))
    team_id: Mapped[str]
    team_name: Mapped[str]
    bot_token: Mapped[str]
    bot_user_id: Mapped[str]
    installer_user_id: Mapped[Optional[str]]
    installer_slack_user_id: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("slack_integrations_project_id_key", "project_id", unique=True),
        Index("slack_integrations_team_id_idx", "team_id"),
    )

    project: Mapped["Project"] = relationship(back_populates="slack_integration")

class PendingDeletion(Base):
    __tablename__ = "pending_deletions"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="pending_deletions_project_id_fkey"))
    object: Mapped[str]
    object_id: Mapped[str]
    is_deleted: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("pending_deletions_project_id_object_is_deleted_object_id_id_idx", "project_id", "object", "is_deleted", "object_id", "id"),
        Index("pending_deletions_object_id_object_idx", "object_id", "object"),
    )

    project: Mapped["Project"] = relationship(back_populates="pending_deletion")

class Survey(Base):
    __tablename__ = "surveys"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    survey_name: Mapped[str] = mapped_column(Enum("org_onboarding", "user_onboarding", name="SurveyName"))
    response: Mapped[dict[str, Any]] = mapped_column(JSONB)
    user_id: Mapped[Optional[str]] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="surveys_user_id_fkey"))
    user_email: Mapped[Optional[str]]
    org_id: Mapped[Optional[str]] = mapped_column(ForeignKey("organizations.id", ondelete="CASCADE", onupdate="CASCADE", name="surveys_org_id_fkey"))

    org: Mapped[Optional["Organization"]] = relationship(back_populates="surveys")
    user: Mapped[Optional["User"]] = relationship(back_populates="surveys")

class CloudSpendAlert(Base):
    __tablename__ = "cloud_spend_alerts"

    id: Mapped[str] = mapped_column(primary_key=True)
    org_id: Mapped[str] = mapped_column(ForeignKey("organizations.id", ondelete="CASCADE", onupdate="CASCADE", name="cloud_spend_alerts_org_id_fkey"))
    title: Mapped[str]
    threshold: Mapped[DecimalType]
    triggered_at: Mapped[Optional[datetime]]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("cloud_spend_alerts_org_id_idx", "org_id"),
    )

    org: Mapped["Organization"] = relationship(back_populates="cloud_spend_alerts")

class TopicClusteringRun(Base):
    __tablename__ = "topic_clustering_runs"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column(ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="topic_clustering_runs_project_id_fkey"))
    facet_id: Mapped[str]
    facet_version: Mapped[int]
    status: Mapped[str] = mapped_column(default="pending", server_default=text("'pending'"))
    config: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    error: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column(UtcDateTimeTz(precision=3), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    started_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=3))
    finished_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=3))
    topic_version_ids: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    rule_id: Mapped[Optional[str]]
    batch_action_id: Mapped[Optional[str]]

    __table_args__ = (
        Index("topic_clustering_runs_serving_map_idx", "project_id", "facet_id", "status", "facet_version", "created_at", "id"),
        Index("topic_clustering_runs_rule_serving_map_idx", "project_id", "rule_id", "facet_id", "status"),
        Index("topic_clustering_runs_project_id_batch_action_id_idx", "project_id", "batch_action_id"),
        ForeignKeyConstraint(["facet_id", "facet_version"], ["evaluator_versions.evaluator_id", "evaluator_versions.version"], ondelete="CASCADE", onupdate="CASCADE", name="topic_clustering_runs_facet_id_facet_version_fkey"),
    )

    project: Mapped["Project"] = relationship(back_populates="topic_clustering_runs")
    facet: Mapped["EvaluatorVersion"] = relationship(back_populates="topic_clustering_runs")
