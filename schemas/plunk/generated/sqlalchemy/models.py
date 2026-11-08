from sqlalchemy import Dialect, Enum, ForeignKey, Index, MetaData, Text, TypeDecorator, text
from sqlalchemy.dialects.postgresql import JSONB, TIMESTAMP
from sqlalchemy.orm import DeclarativeBase, Mapped, mapped_column, relationship
from typing import Any, Optional
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


def utc_now() -> datetime:
    return datetime.now(timezone.utc)


class Base(DeclarativeBase):
    metadata = MetaData(naming_convention={"pk": "%(table_name)s_pkey"})
    type_annotation_map = {datetime: UtcDateTime(precision=3), str: Text}


class User(Base):
    __tablename__ = "users"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    email: Mapped[str]
    password: Mapped[Optional[str]]
    type: Mapped[str] = mapped_column(Enum("PASSWORD", "GOOGLE_OAUTH", "GITHUB_OAUTH", name="AuthMethod"))
    email_verified: Mapped[bool] = mapped_column("emailVerified", default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("users_email_key", "email", unique=True),
    )

    memberships: Mapped[list["Membership"]] = relationship(passive_deletes="all", back_populates="user")

class Project(Base):
    __tablename__ = "projects"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    name: Mapped[str]
    public: Mapped[str]
    secret: Mapped[str]
    disabled: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    disabled_reason: Mapped[Optional[str]] = mapped_column("disabledReason", Enum("PAYMENT_FAILED", "EMAIL_REPUTATION", "PHISHING_DETECTED", "CARD_VERIFICATION_FAILED", "CARD_VERIFICATION_REQUIRED", "MANUAL", name="ProjectDisabledReason"))
    customer: Mapped[Optional[str]]
    subscription: Mapped[Optional[str]]
    card_verification: Mapped[Optional[str]] = mapped_column("cardVerification", Enum("PENDING", "VERIFIED", "REQUIRES_ACTION", "FAILED", name="CardVerificationStatus"))
    card_verification_intent: Mapped[Optional[str]] = mapped_column("cardVerificationIntent")
    card_verification_at: Mapped[Optional[datetime]] = mapped_column("cardVerificationAt")
    card_verification_session: Mapped[Optional[str]] = mapped_column("cardVerificationSession")
    card_verification_payment_method: Mapped[Optional[str]] = mapped_column("cardVerificationPaymentMethod")
    card_verification_fingerprint: Mapped[Optional[str]] = mapped_column("cardVerificationFingerprint")
    card_verification_attempts: Mapped[int] = mapped_column("cardVerificationAttempts", default=0, server_default=text("0"))
    billing_limit_workflows: Mapped[Optional[int]] = mapped_column("billingLimitWorkflows")
    billing_limit_campaigns: Mapped[Optional[int]] = mapped_column("billingLimitCampaigns")
    billing_limit_transactional: Mapped[Optional[int]] = mapped_column("billingLimitTransactional")
    billing_limit_inbound: Mapped[Optional[int]] = mapped_column("billingLimitInbound")
    tracking: Mapped[str] = mapped_column(Enum("ENABLED", "DISABLED", "MARKETING_ONLY", name="TrackingMode"), default="ENABLED", server_default=text("'ENABLED'"))
    language: Mapped[str] = mapped_column(default="en", server_default=text("'en'"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("projects_public_key", "public", unique=True),
        Index("projects_secret_key", "secret", unique=True),
        Index("projects_customer_key", "customer", unique=True),
        Index("projects_subscription_key", "subscription", unique=True),
        Index("projects_cardVerification_cardVerificationAt_idx", "cardVerification", "cardVerificationAt"),
    )

    members: Mapped[list["Membership"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    contacts: Mapped[list["Contact"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    templates: Mapped[list["Template"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    segments: Mapped[list["Segment"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    workflows: Mapped[list["Workflow"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    campaigns: Mapped[list["Campaign"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    emails: Mapped[list["Email"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    events: Mapped[list["Event"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    domains: Mapped[list["Domain"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    api_requests: Mapped[list["ApiRequest"]] = relationship(back_populates="project")
    idempotency_keys: Mapped[list["IdempotencyKey"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    throttle_claims: Mapped[list["ThrottleClaim"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")

class Membership(Base):
    __tablename__ = "memberships"

    user_id: Mapped[str] = mapped_column("userId", ForeignKey("users.id", ondelete="RESTRICT", onupdate="CASCADE", name="memberships_userId_fkey"), primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="memberships_projectId_fkey"), primary_key=True)
    role: Mapped[str] = mapped_column(Enum("OWNER", "ADMIN", "MEMBER", name="Role"), default="MEMBER", server_default=text("'MEMBER'"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    user: Mapped["User"] = relationship(back_populates="memberships")
    project: Mapped["Project"] = relationship(back_populates="members")

class Domain(Base):
    __tablename__ = "domains"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    domain: Mapped[str]
    verified: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    dkim_tokens: Mapped[Optional[dict[str, Any]]] = mapped_column("dkimTokens", JSONB(none_as_null=True))
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="domains_projectId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("domains_projectId_idx", "projectId"),
        Index("domains_projectId_verified_idx", "projectId", "verified"),
        Index("domains_projectId_domain_key", "projectId", "domain", unique=True),
    )

    project: Mapped["Project"] = relationship(back_populates="domains")

class Contact(Base):
    __tablename__ = "contacts"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    email: Mapped[str]
    data: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    subscribed: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    snoozed_until: Mapped[Optional[datetime]] = mapped_column("snoozedUntil")
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="contacts_projectId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("contacts_projectId_idx", "projectId"),
        Index("contacts_projectId_subscribed_idx", "projectId", "subscribed"),
        Index("contacts_snoozedUntil_idx", "snoozedUntil"),
        Index("contacts_data_idx", "data"),
        Index("contacts_projectId_email_key", "projectId", "email", unique=True),
    )

    project: Mapped["Project"] = relationship(back_populates="contacts")
    emails: Mapped[list["Email"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="contact")
    workflow_executions: Mapped[list["WorkflowExecution"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="contact")
    events: Mapped[list["Event"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="contact")
    segment_memberships: Mapped[list["SegmentMembership"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="contact")
    throttle_claims: Mapped[list["ThrottleClaim"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="contact")

class Template(Base):
    __tablename__ = "templates"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    name: Mapped[str]
    description: Mapped[Optional[str]]
    subject: Mapped[str]
    body: Mapped[str]
    from_: Mapped[str] = mapped_column("from")
    from_name: Mapped[Optional[str]] = mapped_column("fromName")
    reply_to: Mapped[Optional[str]] = mapped_column("replyTo")
    type: Mapped[str] = mapped_column(Enum("TRANSACTIONAL", "MARKETING", "HEADLESS", name="TemplateType"), default="MARKETING", server_default=text("'MARKETING'"))
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="templates_projectId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("templates_projectId_idx", "projectId"),
        Index("templates_projectId_type_idx", "projectId", "type"),
    )

    project: Mapped["Project"] = relationship(back_populates="templates")
    emails: Mapped[list["Email"]] = relationship(back_populates="template")
    workflow_steps: Mapped[list["WorkflowStep"]] = relationship(back_populates="template")

class Segment(Base):
    __tablename__ = "segments"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    name: Mapped[str]
    description: Mapped[Optional[str]]
    type: Mapped[str] = mapped_column(Enum("DYNAMIC", "STATIC", name="SegmentType"), default="DYNAMIC", server_default=text("'DYNAMIC'"))
    condition: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    track_membership: Mapped[bool] = mapped_column("trackMembership", default=False, server_default=text("false"))
    member_count: Mapped[int] = mapped_column("memberCount", default=0, server_default=text("0"))
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="segments_projectId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("segments_projectId_idx", "projectId"),
        Index("segments_condition_idx", "condition"),
    )

    project: Mapped["Project"] = relationship(back_populates="segments")
    memberships: Mapped[list["SegmentMembership"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="segment")
    campaigns: Mapped[list["Campaign"]] = relationship(back_populates="segment")

class SegmentMembership(Base):
    __tablename__ = "segment_memberships"

    contact_id: Mapped[str] = mapped_column("contactId", ForeignKey("contacts.id", ondelete="CASCADE", onupdate="CASCADE", name="segment_memberships_contactId_fkey"), primary_key=True)
    segment_id: Mapped[str] = mapped_column("segmentId", ForeignKey("segments.id", ondelete="CASCADE", onupdate="CASCADE", name="segment_memberships_segmentId_fkey"), primary_key=True)
    entered_at: Mapped[datetime] = mapped_column("enteredAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    exited_at: Mapped[Optional[datetime]] = mapped_column("exitedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("segment_memberships_segmentId_exitedAt_idx", "segmentId", "exitedAt"),
        Index("segment_memberships_contactId_exitedAt_idx", "contactId", "exitedAt"),
        Index("segment_memberships_enteredAt_idx", "enteredAt"),
    )

    contact: Mapped["Contact"] = relationship(back_populates="segment_memberships")
    segment: Mapped["Segment"] = relationship(back_populates="memberships")

class Campaign(Base):
    __tablename__ = "campaigns"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    name: Mapped[str]
    description: Mapped[Optional[str]]
    status: Mapped[str] = mapped_column(Enum("DRAFT", "SCHEDULED", "SENDING", "SENT", "CANCELLED", name="CampaignStatus"), default="DRAFT", server_default=text("'DRAFT'"))
    subject: Mapped[str]
    body: Mapped[str]
    from_: Mapped[str] = mapped_column("from")
    from_name: Mapped[Optional[str]] = mapped_column("fromName")
    reply_to: Mapped[Optional[str]] = mapped_column("replyTo")
    type: Mapped[str] = mapped_column(Enum("TRANSACTIONAL", "MARKETING", "HEADLESS", name="TemplateType"), default="MARKETING", server_default=text("'MARKETING'"))
    audience_type: Mapped[str] = mapped_column("audienceType", Enum("ALL", "FILTERED", "SEGMENT", name="CampaignAudienceType"), default="ALL", server_default=text("'ALL'"))
    audience_condition: Mapped[Optional[dict[str, Any]]] = mapped_column("audienceCondition", JSONB(none_as_null=True))
    segment_id: Mapped[Optional[str]] = mapped_column("segmentId", ForeignKey("segments.id", ondelete="SET NULL", onupdate="CASCADE", name="campaigns_segmentId_fkey"))
    scheduled_for: Mapped[Optional[datetime]] = mapped_column("scheduledFor")
    total_recipients: Mapped[int] = mapped_column("totalRecipients", default=0, server_default=text("0"))
    sent_count: Mapped[int] = mapped_column("sentCount", default=0, server_default=text("0"))
    delivered_count: Mapped[int] = mapped_column("deliveredCount", default=0, server_default=text("0"))
    opened_count: Mapped[int] = mapped_column("openedCount", default=0, server_default=text("0"))
    clicked_count: Mapped[int] = mapped_column("clickedCount", default=0, server_default=text("0"))
    bounced_count: Mapped[int] = mapped_column("bouncedCount", default=0, server_default=text("0"))
    complained_count: Mapped[int] = mapped_column("complainedCount", default=0, server_default=text("0"))
    unsubscribed_count: Mapped[int] = mapped_column("unsubscribedCount", default=0, server_default=text("0"))
    archived: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="campaigns_projectId_fkey"))
    sent_at: Mapped[Optional[datetime]] = mapped_column("sentAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("campaigns_projectId_status_idx", "projectId", "status"),
        Index("campaigns_projectId_archived_createdAt_idx", "projectId", "archived", "createdAt"),
        Index("campaigns_scheduledFor_idx", "scheduledFor"),
        Index("campaigns_segmentId_idx", "segmentId"),
        Index("campaigns_audienceCondition_idx", "audienceCondition"),
    )

    segment: Mapped[Optional["Segment"]] = relationship(back_populates="campaigns")
    project: Mapped["Project"] = relationship(back_populates="campaigns")
    emails: Mapped[list["Email"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="campaign")

class Workflow(Base):
    __tablename__ = "workflows"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    name: Mapped[str]
    description: Mapped[Optional[str]]
    enabled: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    trigger_type: Mapped[str] = mapped_column("triggerType", Enum("EVENT", "MANUAL", "SCHEDULE", name="WorkflowTriggerType"))
    trigger_config: Mapped[Optional[dict[str, Any]]] = mapped_column("triggerConfig", JSONB(none_as_null=True))
    allow_reentry: Mapped[bool] = mapped_column("allowReentry", default=False, server_default=text("false"))
    throttle_window_seconds: Mapped[Optional[int]] = mapped_column("throttleWindowSeconds")
    throttle_key: Mapped[Optional[str]] = mapped_column("throttleKey")
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="workflows_projectId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("workflows_projectId_idx", "projectId"),
        Index("workflows_projectId_enabled_idx", "projectId", "enabled"),
    )

    project: Mapped["Project"] = relationship(back_populates="workflows")
    steps: Mapped[list["WorkflowStep"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="workflow")
    executions: Mapped[list["WorkflowExecution"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="workflow")
    throttle_claims: Mapped[list["ThrottleClaim"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="workflow")

class WorkflowStep(Base):
    __tablename__ = "workflow_steps"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    type: Mapped[str] = mapped_column(Enum("TRIGGER", "SEND_EMAIL", "DELAY", "WAIT_FOR_EVENT", "CONDITION", "EXIT", "WEBHOOK", "UPDATE_CONTACT", name="WorkflowStepType"))
    name: Mapped[str]
    position: Mapped[dict[str, Any]] = mapped_column(JSONB)
    config: Mapped[dict[str, Any]] = mapped_column(JSONB)
    workflow_id: Mapped[str] = mapped_column("workflowId", ForeignKey("workflows.id", ondelete="CASCADE", onupdate="CASCADE", name="workflow_steps_workflowId_fkey"))
    template_id: Mapped[Optional[str]] = mapped_column("templateId", ForeignKey("templates.id", ondelete="SET NULL", onupdate="CASCADE", name="workflow_steps_templateId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("workflow_steps_workflowId_idx", "workflowId"),
        Index("workflow_steps_templateId_idx", "templateId"),
    )

    workflow: Mapped["Workflow"] = relationship(back_populates="steps")
    template: Mapped[Optional["Template"]] = relationship(back_populates="workflow_steps")
    outgoing_transitions: Mapped[list["WorkflowTransition"]] = relationship(foreign_keys="WorkflowTransition.from_step_id", cascade="all, delete", passive_deletes=True, back_populates="from_step")
    incoming_transitions: Mapped[list["WorkflowTransition"]] = relationship(foreign_keys="WorkflowTransition.to_step_id", cascade="all, delete", passive_deletes=True, back_populates="to_step")
    executions: Mapped[list["WorkflowStepExecution"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="step")
    current_workflow_executions: Mapped[list["WorkflowExecution"]] = relationship(back_populates="current_step")

class WorkflowTransition(Base):
    __tablename__ = "workflow_transitions"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    from_step_id: Mapped[str] = mapped_column("fromStepId", ForeignKey("workflow_steps.id", ondelete="CASCADE", onupdate="CASCADE", name="workflow_transitions_fromStepId_fkey"))
    to_step_id: Mapped[str] = mapped_column("toStepId", ForeignKey("workflow_steps.id", ondelete="CASCADE", onupdate="CASCADE", name="workflow_transitions_toStepId_fkey"))
    condition: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    priority: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("workflow_transitions_fromStepId_idx", "fromStepId"),
        Index("workflow_transitions_toStepId_idx", "toStepId"),
    )

    from_step: Mapped["WorkflowStep"] = relationship(foreign_keys=[from_step_id], back_populates="outgoing_transitions")
    to_step: Mapped["WorkflowStep"] = relationship(foreign_keys=[to_step_id], back_populates="incoming_transitions")

class WorkflowExecution(Base):
    __tablename__ = "workflow_executions"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    workflow_id: Mapped[str] = mapped_column("workflowId", ForeignKey("workflows.id", ondelete="CASCADE", onupdate="CASCADE", name="workflow_executions_workflowId_fkey"))
    contact_id: Mapped[str] = mapped_column("contactId", ForeignKey("contacts.id", ondelete="CASCADE", onupdate="CASCADE", name="workflow_executions_contactId_fkey"))
    status: Mapped[str] = mapped_column(Enum("RUNNING", "WAITING", "COMPLETED", "EXITED", "FAILED", "CANCELLED", name="WorkflowExecutionStatus"), default="RUNNING", server_default=text("'RUNNING'"))
    current_step_id: Mapped[Optional[str]] = mapped_column("currentStepId", ForeignKey("workflow_steps.id", ondelete="SET NULL", onupdate="CASCADE", name="workflow_executions_currentStepId_fkey"))
    exit_reason: Mapped[Optional[str]] = mapped_column("exitReason")
    context: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    started_at: Mapped[datetime] = mapped_column("startedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    completed_at: Mapped[Optional[datetime]] = mapped_column("completedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("workflow_executions_workflowId_contactId_idx", "workflowId", "contactId"),
        Index("workflow_executions_workflowId_status_idx", "workflowId", "status"),
        Index("workflow_executions_contactId_status_idx", "contactId", "status"),
        Index("workflow_executions_status_currentStepId_idx", "status", "currentStepId"),
    )

    workflow: Mapped["Workflow"] = relationship(back_populates="executions")
    contact: Mapped["Contact"] = relationship(back_populates="workflow_executions")
    current_step: Mapped[Optional["WorkflowStep"]] = relationship(back_populates="current_workflow_executions")
    step_executions: Mapped[list["WorkflowStepExecution"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="execution")
    emails: Mapped[list["Email"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="workflow_execution")

class WorkflowStepExecution(Base):
    __tablename__ = "workflow_step_executions"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    execution_id: Mapped[str] = mapped_column("executionId", ForeignKey("workflow_executions.id", ondelete="CASCADE", onupdate="CASCADE", name="workflow_step_executions_executionId_fkey"))
    step_id: Mapped[str] = mapped_column("stepId", ForeignKey("workflow_steps.id", ondelete="CASCADE", onupdate="CASCADE", name="workflow_step_executions_stepId_fkey"))
    status: Mapped[str] = mapped_column(Enum("PENDING", "SCHEDULED", "WAITING", "RUNNING", "COMPLETED", "SKIPPED", "FAILED", name="StepExecutionStatus"), default="PENDING", server_default=text("'PENDING'"))
    scheduled_for: Mapped[Optional[datetime]] = mapped_column("scheduledFor")
    execute_after: Mapped[Optional[datetime]] = mapped_column("executeAfter")
    output: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    error: Mapped[Optional[str]]
    started_at: Mapped[Optional[datetime]] = mapped_column("startedAt")
    completed_at: Mapped[Optional[datetime]] = mapped_column("completedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("workflow_step_executions_executionId_status_idx", "executionId", "status"),
        Index("workflow_step_executions_stepId_idx", "stepId"),
        Index("workflow_step_executions_status_scheduledFor_idx", "status", "scheduledFor"),
        Index("workflow_step_executions_scheduledFor_idx", "scheduledFor"),
    )

    execution: Mapped["WorkflowExecution"] = relationship(back_populates="step_executions")
    step: Mapped["WorkflowStep"] = relationship(back_populates="executions")
    emails: Mapped[list["Email"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="workflow_step_execution")

class Email(Base):
    __tablename__ = "emails"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    contact_id: Mapped[str] = mapped_column("contactId", ForeignKey("contacts.id", ondelete="CASCADE", onupdate="CASCADE", name="emails_contactId_fkey"))
    to_name: Mapped[Optional[str]] = mapped_column("toName")
    subject: Mapped[str]
    body: Mapped[str]
    from_: Mapped[str] = mapped_column("from")
    from_name: Mapped[Optional[str]] = mapped_column("fromName")
    reply_to: Mapped[Optional[str]] = mapped_column("replyTo")
    headers: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    attachments: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    message_id: Mapped[Optional[str]] = mapped_column("messageId")
    source_type: Mapped[str] = mapped_column("sourceType", Enum("TRANSACTIONAL", "CAMPAIGN", "WORKFLOW", "INBOUND", name="EmailSourceType"))
    template_id: Mapped[Optional[str]] = mapped_column("templateId", ForeignKey("templates.id", ondelete="SET NULL", onupdate="CASCADE", name="emails_templateId_fkey"))
    campaign_id: Mapped[Optional[str]] = mapped_column("campaignId", ForeignKey("campaigns.id", ondelete="CASCADE", onupdate="CASCADE", name="emails_campaignId_fkey"))
    workflow_execution_id: Mapped[Optional[str]] = mapped_column("workflowExecutionId", ForeignKey("workflow_executions.id", ondelete="CASCADE", onupdate="CASCADE", name="emails_workflowExecutionId_fkey"))
    workflow_step_execution_id: Mapped[Optional[str]] = mapped_column("workflowStepExecutionId", ForeignKey("workflow_step_executions.id", ondelete="CASCADE", onupdate="CASCADE", name="emails_workflowStepExecutionId_fkey"))
    status: Mapped[str] = mapped_column(Enum("SCHEDULED", "CANCELLED", "PENDING", "SENDING", "SENT", "DELIVERED", "RECEIVED", "OPENED", "CLICKED", "BOUNCED", "COMPLAINED", "FAILED", name="EmailStatus"), default="PENDING", server_default=text("'PENDING'"))
    scheduled_for: Mapped[Optional[datetime]] = mapped_column("scheduledFor")
    sent_at: Mapped[Optional[datetime]] = mapped_column("sentAt")
    delivered_at: Mapped[Optional[datetime]] = mapped_column("deliveredAt")
    opened_at: Mapped[Optional[datetime]] = mapped_column("openedAt")
    clicked_at: Mapped[Optional[datetime]] = mapped_column("clickedAt")
    bounced_at: Mapped[Optional[datetime]] = mapped_column("bouncedAt")
    complained_at: Mapped[Optional[datetime]] = mapped_column("complainedAt")
    unsubscribed_at: Mapped[Optional[datetime]] = mapped_column("unsubscribedAt")
    opens: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    clicks: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    simulated: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    error: Mapped[Optional[str]]
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="emails_projectId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("emails_messageId_key", "messageId", unique=True),
        Index("emails_projectId_contactId_idx", "projectId", "contactId"),
        Index("emails_contactId_idx", "contactId"),
        Index("emails_campaignId_idx", "campaignId"),
        Index("emails_campaignId_status_idx", "campaignId", "status"),
        Index("emails_workflowExecutionId_idx", "workflowExecutionId"),
        Index("emails_workflowStepExecutionId_idx", "workflowStepExecutionId"),
        Index("emails_status_idx", "status"),
        Index("emails_createdAt_idx", "createdAt"),
        Index("emails_projectId_sourceType_createdAt_idx", "projectId", "sourceType", "createdAt"),
        Index("emails_contactId_openedAt_idx", "contactId", "openedAt"),
        Index("emails_contactId_clickedAt_idx", "contactId", "clickedAt"),
        Index("emails_contactId_bouncedAt_idx", "contactId", "bouncedAt"),
        Index("emails_contactId_complainedAt_idx", "contactId", "complainedAt"),
    )

    contact: Mapped["Contact"] = relationship(back_populates="emails")
    template: Mapped[Optional["Template"]] = relationship(back_populates="emails")
    campaign: Mapped[Optional["Campaign"]] = relationship(back_populates="emails")
    workflow_execution: Mapped[Optional["WorkflowExecution"]] = relationship(back_populates="emails")
    workflow_step_execution: Mapped[Optional["WorkflowStepExecution"]] = relationship(back_populates="emails")
    project: Mapped["Project"] = relationship(back_populates="emails")
    events: Mapped[list["Event"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="email")

class Event(Base):
    __tablename__ = "events"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    name: Mapped[str]
    data: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="events_projectId_fkey"))
    contact_id: Mapped[Optional[str]] = mapped_column("contactId", ForeignKey("contacts.id", ondelete="CASCADE", onupdate="CASCADE", name="events_contactId_fkey"))
    email_id: Mapped[Optional[str]] = mapped_column("emailId", ForeignKey("emails.id", ondelete="CASCADE", onupdate="CASCADE", name="events_emailId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("events_projectId_name_idx", "projectId", "name"),
        Index("events_contactId_idx", "contactId"),
        Index("events_emailId_idx", "emailId"),
        Index("events_createdAt_idx", "createdAt"),
        Index("events_projectId_contactId_name_createdAt_idx", "projectId", "contactId", "name", "createdAt"),
        Index("events_contactId_name_createdAt_idx", "contactId", "name", "createdAt"),
        Index("events_projectId_name_createdAt_idx", "projectId", "name", "createdAt"),
        Index("events_contactId_createdAt_idx", "contactId", "createdAt"),
        Index("events_data_idx", "data"),
    )

    project: Mapped["Project"] = relationship(back_populates="events")
    contact: Mapped[Optional["Contact"]] = relationship(back_populates="events")
    email: Mapped[Optional["Email"]] = relationship(back_populates="events")

class IdempotencyKey(Base):
    __tablename__ = "idempotency_keys"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    key: Mapped[str]
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="idempotency_keys_projectId_fkey"))
    method: Mapped[str]
    path: Mapped[str]
    status_code: Mapped[Optional[int]] = mapped_column("statusCode")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    expires_at: Mapped[datetime] = mapped_column("expiresAt")

    __table_args__ = (
        Index("idempotency_keys_expiresAt_idx", "expiresAt"),
        Index("idempotency_keys_projectId_key_key", "projectId", "key", unique=True),
    )

    project: Mapped["Project"] = relationship(back_populates="idempotency_keys")

class ThrottleClaim(Base):
    __tablename__ = "throttle_claims"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("projects.id", ondelete="CASCADE", onupdate="CASCADE", name="throttle_claims_projectId_fkey"))
    key: Mapped[str]
    recipient_key: Mapped[str] = mapped_column("recipientKey")
    contact_id: Mapped[Optional[str]] = mapped_column("contactId", ForeignKey("contacts.id", ondelete="CASCADE", onupdate="CASCADE", name="throttle_claims_contactId_fkey"))
    email_id: Mapped[Optional[str]] = mapped_column("emailId")
    workflow_id: Mapped[Optional[str]] = mapped_column("workflowId", ForeignKey("workflows.id", ondelete="CASCADE", onupdate="CASCADE", name="throttle_claims_workflowId_fkey"))
    window_seconds: Mapped[Optional[int]] = mapped_column("windowSeconds")
    expires_at: Mapped[Optional[datetime]] = mapped_column("expiresAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("throttle_claims_emailId_idx", "emailId"),
        Index("throttle_claims_workflowId_idx", "workflowId"),
        Index("throttle_claims_contactId_idx", "contactId"),
        Index("throttle_claims_expiresAt_idx", "expiresAt"),
        Index("throttle_claims_projectId_key_recipientKey_key", "projectId", "key", "recipientKey", unique=True),
    )

    project: Mapped["Project"] = relationship(back_populates="throttle_claims")
    contact: Mapped[Optional["Contact"]] = relationship(back_populates="throttle_claims")
    workflow: Mapped[Optional["Workflow"]] = relationship(back_populates="throttle_claims")

class ApiRequest(Base):
    __tablename__ = "api_requests"

    id: Mapped[str] = mapped_column(primary_key=True)
    method: Mapped[str]
    path: Mapped[str]
    status_code: Mapped[int] = mapped_column("statusCode")
    duration: Mapped[int]
    project_id: Mapped[Optional[str]] = mapped_column("projectId", ForeignKey("projects.id", ondelete="SET NULL", onupdate="CASCADE", name="api_requests_projectId_fkey"))
    user_id: Mapped[Optional[str]] = mapped_column("userId")
    auth_type: Mapped[Optional[str]] = mapped_column("authType")
    ip: Mapped[Optional[str]]
    user_agent: Mapped[Optional[str]] = mapped_column("userAgent")
    error_code: Mapped[Optional[str]] = mapped_column("errorCode")
    error_message: Mapped[Optional[str]] = mapped_column("errorMessage")
    request_size: Mapped[Optional[int]] = mapped_column("requestSize")
    response_size: Mapped[Optional[int]] = mapped_column("responseSize")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("api_requests_projectId_createdAt_idx", "projectId", "createdAt"),
        Index("api_requests_userId_createdAt_idx", "userId", "createdAt"),
        Index("api_requests_statusCode_createdAt_idx", "statusCode", "createdAt"),
        Index("api_requests_path_createdAt_idx", "path", "createdAt"),
        Index("api_requests_createdAt_idx", "createdAt"),
        Index("api_requests_projectId_statusCode_createdAt_idx", "projectId", "statusCode", "createdAt"),
    )

    project: Mapped[Optional["Project"]] = relationship(back_populates="api_requests")
