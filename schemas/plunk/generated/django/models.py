import uuid
from datetime import datetime
from datetime import timezone as dt_timezone
from typing import Any, TypeVar

from django.conf import settings
from django.contrib.postgres.indexes import GinIndex
from django.db import models
from django.db.backends.base.base import BaseDatabaseWrapper
from django.utils import timezone


class UtcDateTimeField(models.DateTimeField):  # type: ignore[type-arg]
    def get_db_prep_value(self, value: Any, connection: BaseDatabaseWrapper, prepared: bool = False) -> Any:
        if not prepared:
            value = self.get_prep_value(value)
        if not isinstance(value, datetime):
            return super().get_db_prep_value(value, connection, prepared=True)
        if timezone.is_naive(value):
            value = timezone.make_aware(value, timezone.get_default_timezone())
        value = value.astimezone(dt_timezone.utc)
        if connection.vendor == "sqlite":
            return value.isoformat(timespec="milliseconds")
        return connection.ops.adapt_datetimefield_value(value.replace(tzinfo=None))

    def from_db_value(self, value: datetime | None, expression: Any, connection: BaseDatabaseWrapper) -> datetime | None:
        if value is None:
            return None
        if timezone.is_naive(value):
            value = value.replace(tzinfo=dt_timezone.utc)
        return value if settings.USE_TZ else timezone.make_naive(value, timezone.get_default_timezone())


_M = TypeVar("_M", bound=models.Model)


class AutoNowQuerySet(models.QuerySet[_M]):
    def update(self, **kwargs: Any) -> int:
        for field in self.model._meta.concrete_fields:
            if getattr(field, "auto_now", False):
                kwargs.setdefault(field.name, getattr(field, "stamp", timezone.now)())
        return super().update(**kwargs)


def uuid4_str() -> str:
    return str(uuid.uuid4())


class ProjectDisabledReason(models.TextChoices):
    PAYMENT_FAILED = "PAYMENT_FAILED"
    EMAIL_REPUTATION = "EMAIL_REPUTATION"
    PHISHING_DETECTED = "PHISHING_DETECTED"
    CARD_VERIFICATION_FAILED = "CARD_VERIFICATION_FAILED"
    CARD_VERIFICATION_REQUIRED = "CARD_VERIFICATION_REQUIRED"
    MANUAL = "MANUAL"


class CardVerificationStatus(models.TextChoices):
    PENDING = "PENDING"
    VERIFIED = "VERIFIED"
    REQUIRES_ACTION = "REQUIRES_ACTION"
    FAILED = "FAILED"


class AuthMethod(models.TextChoices):
    PASSWORD = "PASSWORD"
    GOOGLE_OAUTH = "GOOGLE_OAUTH"
    GITHUB_OAUTH = "GITHUB_OAUTH"


class Role(models.TextChoices):
    OWNER = "OWNER"
    ADMIN = "ADMIN"
    MEMBER = "MEMBER"


class TemplateType(models.TextChoices):
    TRANSACTIONAL = "TRANSACTIONAL"
    MARKETING = "MARKETING"
    HEADLESS = "HEADLESS"


class TrackingMode(models.TextChoices):
    ENABLED = "ENABLED"
    DISABLED = "DISABLED"
    MARKETING_ONLY = "MARKETING_ONLY"


class CampaignStatus(models.TextChoices):
    DRAFT = "DRAFT"
    SCHEDULED = "SCHEDULED"
    SENDING = "SENDING"
    SENT = "SENT"
    CANCELLED = "CANCELLED"


class CampaignAudienceType(models.TextChoices):
    ALL = "ALL"
    FILTERED = "FILTERED"
    SEGMENT = "SEGMENT"


class WorkflowTriggerType(models.TextChoices):
    EVENT = "EVENT"
    MANUAL = "MANUAL"
    SCHEDULE = "SCHEDULE"


class WorkflowStepType(models.TextChoices):
    TRIGGER = "TRIGGER"
    SEND_EMAIL = "SEND_EMAIL"
    DELAY = "DELAY"
    WAIT_FOR_EVENT = "WAIT_FOR_EVENT"
    CONDITION = "CONDITION"
    EXIT = "EXIT"
    WEBHOOK = "WEBHOOK"
    UPDATE_CONTACT = "UPDATE_CONTACT"


class WorkflowExecutionStatus(models.TextChoices):
    RUNNING = "RUNNING"
    WAITING = "WAITING"
    COMPLETED = "COMPLETED"
    EXITED = "EXITED"
    FAILED = "FAILED"
    CANCELLED = "CANCELLED"


class StepExecutionStatus(models.TextChoices):
    PENDING = "PENDING"
    SCHEDULED = "SCHEDULED"
    WAITING = "WAITING"
    RUNNING = "RUNNING"
    COMPLETED = "COMPLETED"
    SKIPPED = "SKIPPED"
    FAILED = "FAILED"


class SegmentType(models.TextChoices):
    DYNAMIC = "DYNAMIC"
    STATIC = "STATIC"


class EmailSourceType(models.TextChoices):
    TRANSACTIONAL = "TRANSACTIONAL"
    CAMPAIGN = "CAMPAIGN"
    WORKFLOW = "WORKFLOW"
    INBOUND = "INBOUND"


class EmailStatus(models.TextChoices):
    SCHEDULED = "SCHEDULED"
    CANCELLED = "CANCELLED"
    PENDING = "PENDING"
    SENDING = "SENDING"
    SENT = "SENT"
    DELIVERED = "DELIVERED"
    RECEIVED = "RECEIVED"
    OPENED = "OPENED"
    CLICKED = "CLICKED"
    BOUNCED = "BOUNCED"
    COMPLAINED = "COMPLAINED"
    FAILED = "FAILED"


class User(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    email = models.TextField(unique=True)
    password = models.TextField(null=True)
    type = models.TextField(choices=AuthMethod.choices)
    email_verified = models.BooleanField(db_column="emailVerified", default=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "users"


class Project(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    name = models.TextField()
    public = models.TextField(unique=True)
    secret = models.TextField(unique=True)
    disabled = models.BooleanField(default=False)
    disabled_reason = models.TextField(choices=ProjectDisabledReason.choices, null=True, db_column="disabledReason")
    customer = models.TextField(null=True, unique=True)
    subscription = models.TextField(null=True, unique=True)
    card_verification = models.TextField(choices=CardVerificationStatus.choices, null=True, db_column="cardVerification")
    card_verification_intent = models.TextField(null=True, db_column="cardVerificationIntent")
    card_verification_at = UtcDateTimeField(null=True, db_column="cardVerificationAt")
    card_verification_session = models.TextField(null=True, db_column="cardVerificationSession")
    card_verification_payment_method = models.TextField(null=True, db_column="cardVerificationPaymentMethod")
    card_verification_fingerprint = models.TextField(null=True, db_column="cardVerificationFingerprint")
    card_verification_attempts = models.IntegerField(db_column="cardVerificationAttempts", default=0)
    billing_limit_workflows = models.IntegerField(null=True, db_column="billingLimitWorkflows")
    billing_limit_campaigns = models.IntegerField(null=True, db_column="billingLimitCampaigns")
    billing_limit_transactional = models.IntegerField(null=True, db_column="billingLimitTransactional")
    billing_limit_inbound = models.IntegerField(null=True, db_column="billingLimitInbound")
    tracking = models.TextField(choices=TrackingMode.choices, default=TrackingMode.ENABLED)
    language = models.TextField(default="en")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "projects"
        indexes = [
            models.Index(fields=["card_verification", "card_verification_at"]),
        ]


class Membership(models.Model):
    pk = models.CompositePrimaryKey("user_id", "project_id")
    user = models.ForeignKey("User", on_delete=models.RESTRICT, related_name="memberships", db_column="userId", db_index=False)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="members", db_column="projectId", db_index=False)
    role = models.TextField(choices=Role.choices, default=Role.MEMBER)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "memberships"


class Domain(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    domain = models.TextField()
    verified = models.BooleanField(default=False)
    dkim_tokens = models.JSONField(null=True, db_column="dkimTokens")
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="domains", db_column="projectId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "domains"
        constraints = [
            models.UniqueConstraint(fields=["project", "domain"], name="domains_projectId_domain_key"),
        ]
        indexes = [
            models.Index(fields=["project"]),
            models.Index(fields=["project", "verified"]),
        ]


class Contact(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    email = models.TextField()
    data = models.JSONField(null=True)
    subscribed = models.BooleanField(default=True)
    snoozed_until = UtcDateTimeField(null=True, db_column="snoozedUntil")
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="contacts", db_column="projectId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "contacts"
        constraints = [
            models.UniqueConstraint(fields=["project", "email"], name="contacts_projectId_email_key"),
        ]
        indexes = [
            models.Index(fields=["project"]),
            models.Index(fields=["project", "subscribed"]),
            models.Index(fields=["snoozed_until"]),
            GinIndex(fields=["data"]),
        ]


class Template(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    name = models.TextField()
    description = models.TextField(null=True)
    subject = models.TextField()
    body = models.TextField()
    from_field = models.TextField(db_column="from")
    from_name = models.TextField(null=True, db_column="fromName")
    reply_to = models.TextField(null=True, db_column="replyTo")
    type = models.TextField(choices=TemplateType.choices, default=TemplateType.MARKETING)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="templates", db_column="projectId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "templates"
        indexes = [
            models.Index(fields=["project"]),
            models.Index(fields=["project", "type"]),
        ]


class Segment(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    name = models.TextField()
    description = models.TextField(null=True)
    type = models.TextField(choices=SegmentType.choices, default=SegmentType.DYNAMIC)
    condition = models.JSONField(null=True)
    track_membership = models.BooleanField(db_column="trackMembership", default=False)
    member_count = models.IntegerField(db_column="memberCount", default=0)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="segments", db_column="projectId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "segments"
        indexes = [
            models.Index(fields=["project"]),
            GinIndex(fields=["condition"]),
        ]


class SegmentMembership(models.Model):
    pk = models.CompositePrimaryKey("contact_id", "segment_id")
    contact = models.ForeignKey("Contact", on_delete=models.CASCADE, related_name="segment_memberships", db_column="contactId", db_index=False)
    segment = models.ForeignKey("Segment", on_delete=models.CASCADE, related_name="memberships", db_column="segmentId", db_index=False)
    entered_at = UtcDateTimeField(db_column="enteredAt", default=timezone.now)
    exited_at = UtcDateTimeField(null=True, db_column="exitedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "segment_memberships"
        indexes = [
            models.Index(fields=["segment", "exited_at"]),
            models.Index(fields=["contact", "exited_at"]),
            models.Index(fields=["entered_at"]),
        ]


class Campaign(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    name = models.TextField()
    description = models.TextField(null=True)
    status = models.TextField(choices=CampaignStatus.choices, default=CampaignStatus.DRAFT)
    subject = models.TextField()
    body = models.TextField()
    from_field = models.TextField(db_column="from")
    from_name = models.TextField(null=True, db_column="fromName")
    reply_to = models.TextField(null=True, db_column="replyTo")
    type = models.TextField(choices=TemplateType.choices, default=TemplateType.MARKETING)
    audience_type = models.TextField(choices=CampaignAudienceType.choices, db_column="audienceType", default=CampaignAudienceType.ALL)
    audience_condition = models.JSONField(null=True, db_column="audienceCondition")
    segment = models.ForeignKey("Segment", on_delete=models.SET_NULL, related_name="campaigns", db_column="segmentId", null=True, db_index=False)
    scheduled_for = UtcDateTimeField(null=True, db_column="scheduledFor")
    total_recipients = models.IntegerField(db_column="totalRecipients", default=0)
    sent_count = models.IntegerField(db_column="sentCount", default=0)
    delivered_count = models.IntegerField(db_column="deliveredCount", default=0)
    opened_count = models.IntegerField(db_column="openedCount", default=0)
    clicked_count = models.IntegerField(db_column="clickedCount", default=0)
    bounced_count = models.IntegerField(db_column="bouncedCount", default=0)
    complained_count = models.IntegerField(db_column="complainedCount", default=0)
    unsubscribed_count = models.IntegerField(db_column="unsubscribedCount", default=0)
    archived = models.BooleanField(default=False)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="campaigns", db_column="projectId", db_index=False)
    sent_at = UtcDateTimeField(null=True, db_column="sentAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "campaigns"
        indexes = [
            models.Index(fields=["project", "status"]),
            models.Index(fields=["project", "archived", "created_at"]),
            models.Index(fields=["scheduled_for"]),
            models.Index(fields=["segment"]),
            GinIndex(fields=["audience_condition"]),
        ]


class Workflow(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    name = models.TextField()
    description = models.TextField(null=True)
    enabled = models.BooleanField(default=False)
    trigger_type = models.TextField(choices=WorkflowTriggerType.choices, db_column="triggerType")
    trigger_config = models.JSONField(null=True, db_column="triggerConfig")
    allow_reentry = models.BooleanField(db_column="allowReentry", default=False)
    throttle_window_seconds = models.IntegerField(null=True, db_column="throttleWindowSeconds")
    throttle_key = models.TextField(null=True, db_column="throttleKey")
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="workflows", db_column="projectId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "workflows"
        indexes = [
            models.Index(fields=["project"]),
            models.Index(fields=["project", "enabled"]),
        ]


class WorkflowStep(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    type = models.TextField(choices=WorkflowStepType.choices)
    name = models.TextField()
    position = models.JSONField()
    config = models.JSONField()
    workflow = models.ForeignKey("Workflow", on_delete=models.CASCADE, related_name="steps", db_column="workflowId", db_index=False)
    template = models.ForeignKey("Template", on_delete=models.SET_NULL, related_name="workflow_steps", db_column="templateId", null=True, db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "workflow_steps"
        indexes = [
            models.Index(fields=["workflow"]),
            models.Index(fields=["template"]),
        ]


class WorkflowTransition(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    from_step = models.ForeignKey("WorkflowStep", on_delete=models.CASCADE, related_name="outgoing_transitions", db_column="fromStepId", db_index=False)
    to_step = models.ForeignKey("WorkflowStep", on_delete=models.CASCADE, related_name="incoming_transitions", db_column="toStepId", db_index=False)
    condition = models.JSONField(null=True)
    priority = models.IntegerField(default=0)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "workflow_transitions"
        indexes = [
            models.Index(fields=["from_step"]),
            models.Index(fields=["to_step"]),
        ]


class WorkflowExecution(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    workflow = models.ForeignKey("Workflow", on_delete=models.CASCADE, related_name="executions", db_column="workflowId", db_index=False)
    contact = models.ForeignKey("Contact", on_delete=models.CASCADE, related_name="workflow_executions", db_column="contactId", db_index=False)
    status = models.TextField(choices=WorkflowExecutionStatus.choices, default=WorkflowExecutionStatus.RUNNING)
    current_step = models.ForeignKey("WorkflowStep", on_delete=models.SET_NULL, related_name="current_workflow_executions", db_column="currentStepId", null=True, db_index=False)
    exit_reason = models.TextField(null=True, db_column="exitReason")
    context = models.JSONField(null=True)
    started_at = UtcDateTimeField(db_column="startedAt", default=timezone.now)
    completed_at = UtcDateTimeField(null=True, db_column="completedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "workflow_executions"
        indexes = [
            models.Index(fields=["workflow", "contact"]),
            models.Index(fields=["workflow", "status"]),
            models.Index(fields=["contact", "status"]),
            models.Index(fields=["status", "current_step"]),
        ]


class WorkflowStepExecution(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    execution = models.ForeignKey("WorkflowExecution", on_delete=models.CASCADE, related_name="step_executions", db_column="executionId", db_index=False)
    step = models.ForeignKey("WorkflowStep", on_delete=models.CASCADE, related_name="executions", db_column="stepId", db_index=False)
    status = models.TextField(choices=StepExecutionStatus.choices, default=StepExecutionStatus.PENDING)
    scheduled_for = UtcDateTimeField(null=True, db_column="scheduledFor")
    execute_after = UtcDateTimeField(null=True, db_column="executeAfter")
    output = models.JSONField(null=True)
    error = models.TextField(null=True)
    started_at = UtcDateTimeField(null=True, db_column="startedAt")
    completed_at = UtcDateTimeField(null=True, db_column="completedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "workflow_step_executions"
        indexes = [
            models.Index(fields=["execution", "status"]),
            models.Index(fields=["step"]),
            models.Index(fields=["status", "scheduled_for"]),
            models.Index(fields=["scheduled_for"]),
        ]


class Email(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    contact = models.ForeignKey("Contact", on_delete=models.CASCADE, related_name="emails", db_column="contactId", db_index=False)
    to_name = models.TextField(null=True, db_column="toName")
    subject = models.TextField()
    body = models.TextField()
    from_field = models.TextField(db_column="from")
    from_name = models.TextField(null=True, db_column="fromName")
    reply_to = models.TextField(null=True, db_column="replyTo")
    headers = models.JSONField(null=True)
    attachments = models.JSONField(null=True)
    message_id = models.TextField(null=True, unique=True, db_column="messageId")
    source_type = models.TextField(choices=EmailSourceType.choices, db_column="sourceType")
    template = models.ForeignKey("Template", on_delete=models.SET_NULL, related_name="emails", db_column="templateId", null=True, db_index=False)
    campaign = models.ForeignKey("Campaign", on_delete=models.CASCADE, related_name="emails", db_column="campaignId", null=True, db_index=False)
    workflow_execution = models.ForeignKey("WorkflowExecution", on_delete=models.CASCADE, related_name="emails", db_column="workflowExecutionId", null=True, db_index=False)
    workflow_step_execution = models.ForeignKey("WorkflowStepExecution", on_delete=models.CASCADE, related_name="emails", db_column="workflowStepExecutionId", null=True, db_index=False)
    status = models.TextField(choices=EmailStatus.choices, default=EmailStatus.PENDING)
    scheduled_for = UtcDateTimeField(null=True, db_column="scheduledFor")
    sent_at = UtcDateTimeField(null=True, db_column="sentAt")
    delivered_at = UtcDateTimeField(null=True, db_column="deliveredAt")
    opened_at = UtcDateTimeField(null=True, db_column="openedAt")
    clicked_at = UtcDateTimeField(null=True, db_column="clickedAt")
    bounced_at = UtcDateTimeField(null=True, db_column="bouncedAt")
    complained_at = UtcDateTimeField(null=True, db_column="complainedAt")
    unsubscribed_at = UtcDateTimeField(null=True, db_column="unsubscribedAt")
    opens = models.IntegerField(default=0)
    clicks = models.IntegerField(default=0)
    simulated = models.BooleanField(default=False)
    error = models.TextField(null=True)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="emails", db_column="projectId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "emails"
        indexes = [
            models.Index(fields=["project", "contact"]),
            models.Index(fields=["contact"]),
            models.Index(fields=["campaign"]),
            models.Index(fields=["campaign", "status"]),
            models.Index(fields=["workflow_execution"]),
            models.Index(fields=["workflow_step_execution"]),
            models.Index(fields=["status"]),
            models.Index(fields=["created_at"]),
            models.Index(fields=["project", "source_type", "created_at"]),
            models.Index(fields=["contact", "opened_at"]),
            models.Index(fields=["contact", "clicked_at"]),
            models.Index(fields=["contact", "bounced_at"]),
            models.Index(fields=["contact", "complained_at"]),
        ]


class Event(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    name = models.TextField()
    data = models.JSONField(null=True)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="events", db_column="projectId", db_index=False)
    contact = models.ForeignKey("Contact", on_delete=models.CASCADE, related_name="events", db_column="contactId", null=True, db_index=False)
    email = models.ForeignKey("Email", on_delete=models.CASCADE, related_name="events", db_column="emailId", null=True, db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "events"
        indexes = [
            models.Index(fields=["project", "name"]),
            models.Index(fields=["contact"]),
            models.Index(fields=["email"]),
            models.Index(fields=["created_at"]),
            models.Index(fields=["project", "contact", "name", "created_at"]),
            models.Index(fields=["contact", "name", "created_at"]),
            models.Index(fields=["project", "name", "created_at"]),
            models.Index(fields=["contact", "created_at"]),
            GinIndex(fields=["data"]),
        ]


class IdempotencyKey(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    key = models.TextField()
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="idempotency_keys", db_column="projectId", db_index=False)
    method = models.TextField()
    path = models.TextField()
    status_code = models.IntegerField(null=True, db_column="statusCode")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    expires_at = UtcDateTimeField(db_column="expiresAt")

    class Meta:
        db_table = "idempotency_keys"
        constraints = [
            models.UniqueConstraint(fields=["project", "key"], name="idempotency_keys_projectId_key_key"),
        ]
        indexes = [
            models.Index(fields=["expires_at"]),
        ]


class ThrottleClaim(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="throttle_claims", db_column="projectId", db_index=False)
    key = models.TextField()
    recipient_key = models.TextField(db_column="recipientKey")
    contact = models.ForeignKey("Contact", on_delete=models.CASCADE, related_name="throttle_claims", db_column="contactId", null=True, db_index=False)
    email_id = models.TextField(null=True, db_column="emailId")
    workflow = models.ForeignKey("Workflow", on_delete=models.CASCADE, related_name="throttle_claims", db_column="workflowId", null=True, db_index=False)
    window_seconds = models.IntegerField(null=True, db_column="windowSeconds")
    expires_at = UtcDateTimeField(null=True, db_column="expiresAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "throttle_claims"
        constraints = [
            models.UniqueConstraint(fields=["project", "key", "recipient_key"], name="throttle_claims_projectId_key_recipientKey_key"),
        ]
        indexes = [
            models.Index(fields=["email_id"]),
            models.Index(fields=["workflow"]),
            models.Index(fields=["contact"]),
            models.Index(fields=["expires_at"]),
        ]


class ApiRequest(models.Model):
    id = models.TextField(primary_key=True, default=None)
    method = models.TextField()
    path = models.TextField()
    status_code = models.IntegerField(db_column="statusCode")
    duration = models.IntegerField()
    project = models.ForeignKey("Project", on_delete=models.SET_NULL, related_name="api_requests", db_column="projectId", null=True, db_index=False)
    user_id = models.TextField(null=True, db_column="userId")
    auth_type = models.TextField(null=True, db_column="authType")
    ip = models.TextField(null=True)
    user_agent = models.TextField(null=True, db_column="userAgent")
    error_code = models.TextField(null=True, db_column="errorCode")
    error_message = models.TextField(null=True, db_column="errorMessage")
    request_size = models.IntegerField(null=True, db_column="requestSize")
    response_size = models.IntegerField(null=True, db_column="responseSize")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "api_requests"
        indexes = [
            models.Index(fields=["project", "-created_at"]),
            models.Index(fields=["user_id", "-created_at"]),
            models.Index(fields=["status_code", "-created_at"]),
            models.Index(fields=["path", "-created_at"]),
            models.Index(fields=["-created_at"]),
            models.Index(fields=["project", "status_code", "-created_at"]),
        ]
