import uuid
from datetime import datetime
from datetime import timezone as dt_timezone
from typing import Any, TypeVar

from django.conf import settings
from django.contrib.postgres.fields import ArrayField
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


def survey_hidden_fields_default() -> dict[str, Any]:
    return {"enabled": False}


def survey_welcome_card_default() -> dict[str, Any]:
    return {"enabled": False}


def survey_single_use_default() -> dict[str, Any]:
    return {"enabled": False, "isEncrypted": True}


def survey_recaptcha_default() -> dict[str, Any]:
    return {"enabled": False, "threshold": 0.1}


def workspace_styling_default() -> dict[str, Any]:
    return {"allowStyleOverwrite": True}


def dashboard_widget_layout_default() -> dict[str, Any]:
    return {"x": 0, "y": 0, "w": 4, "h": 3}


class CreativeType(models.TextChoices):
    feedbackButton = "feedbackButton"
    banner = "banner"


class CreativeStatus(models.TextChoices):
    draft = "draft"
    inProgress = "inProgress"
    paused = "paused"
    completed = "completed"


class CreativeShowOn(models.TextChoices):
    pageView = "pageView"
    action = "action"


class CreativeDeviceType(models.TextChoices):
    phone = "phone"
    tablet = "tablet"
    desktop = "desktop"


class PipelineTriggers(models.TextChoices):
    responseCreated = "responseCreated"
    responseUpdated = "responseUpdated"
    responseFinished = "responseFinished"


class WebhookSource(models.TextChoices):
    user = "user"
    zapier = "zapier"
    make = "make"
    n8n = "n8n"
    activepieces = "activepieces"


class ContactAttributeType(models.TextChoices):
    default = "default"
    custom = "custom"


class ContactAttributeDataType(models.TextChoices):
    string = "string"
    number = "number"
    date = "date"


class ResponseOutcome(models.TextChoices):
    lowQuality = "lowQuality"


class SurveyStatus(models.TextChoices):
    draft = "draft"
    inProgress = "inProgress"
    paused = "paused"
    completed = "completed"


class SurveyAttributeFilterCondition(models.TextChoices):
    equals = "equals"
    notEquals = "notEquals"


class SurveyQuotaAction(models.TextChoices):
    endSurvey = "endSurvey"
    continueSurvey = "continueSurvey"


class ResponseQuotaLinkStatus(models.TextChoices):
    screenedIn = "screenedIn"
    screenedOut = "screenedOut"


class SurveyType(models.TextChoices):
    link = "link"
    app = "app"


class DisplayOptions(models.TextChoices):
    displayOnce = "displayOnce"
    displayMultiple = "displayMultiple"
    displaySome = "displaySome"
    respondMultiple = "respondMultiple"


class SurveyVisibility(models.TextChoices):
    restricted = "restricted"
    workspace = "workspace"


class SurveyScriptMode(models.TextChoices):
    add = "add"
    replace_ = "replace"


class ActionType(models.TextChoices):
    code = "code"
    noCode = "noCode"


class IntegrationType(models.TextChoices):
    googleSheets = "googleSheets"
    notion = "notion"
    airtable = "airtable"
    slack = "slack"


class DataMigrationStatus(models.TextChoices):
    pending = "pending"
    applied = "applied"
    failed = "failed"


class WidgetPlacement(models.TextChoices):
    bottomLeft = "bottomLeft"
    bottomRight = "bottomRight"
    topLeft = "topLeft"
    topRight = "topRight"
    center_ = "center"


class SurveyOverlay(models.TextChoices):
    none = "none"
    light = "light"
    dark = "dark"


class BootstrapAdminMarker(models.TextChoices):
    bootstrapAdmin = "bootstrapAdmin"


class OrganizationRole(models.TextChoices):
    owner = "owner"
    manager = "manager"
    member = "member"
    billing = "billing"


class ApiKeyPermission(models.TextChoices):
    read = "read"
    write = "write"
    manage = "manage"


class IdentityProvider(models.TextChoices):
    email = "email"
    github = "github"
    google = "google"
    azuread = "azuread"
    openid = "openid"
    saml = "saml"


class EmbeddedDataSource(models.TextChoices):
    computed = "computed"
    ingested = "ingested"
    reserved = "reserved"


class EmbeddedDataType(models.TextChoices):
    string = "string"
    number = "number"
    boolean = "boolean"
    date = "date"


class TeamUserRole(models.TextChoices):
    admin = "admin"
    contributor = "contributor"


class WorkspaceTeamPermission(models.TextChoices):
    read = "read"
    readWrite = "readWrite"
    manage = "manage"


class ChartType(models.TextChoices):
    area = "area"
    bar = "bar"
    pie = "pie"
    big_number = "big_number"
    matrix = "matrix"


class FeedbackSourceType(models.TextChoices):
    formbricks_survey = "formbricks_survey"
    csv = "csv"


class FeedbackSourceStatus(models.TextChoices):
    active = "active"
    paused = "paused"
    error = "error"


class FeedbackSourceImportMode(models.TextChoices):
    completedOnly = "completedOnly"
    all = "all"


class FeedbackSourceElementScope(models.TextChoices):
    all = "all"
    specific = "specific"


class HubFieldType(models.TextChoices):
    text = "text"
    categorical = "categorical"
    nps = "nps"
    csat = "csat"
    ces = "ces"
    rating = "rating"
    number = "number"
    boolean = "boolean"
    date = "date"


class DeletionCleanupKind(models.TextChoices):
    hubSurvey = "hubSurvey"
    hubResponses = "hubResponses"
    storageFiles = "storageFiles"
    storageSurveyFolder = "storageSurveyFolder"
    hubResponseOutcome = "hubResponseOutcome"


class AttachmentExportStatus(models.TextChoices):
    queued = "queued"
    running = "running"
    completed = "completed"
    failed = "failed"
    expired = "expired"


class RetentionEntity(models.TextChoices):
    responses = "responses"
    surveys = "surveys"
    members = "members"


class RetentionSurveyCondition(models.TextChoices):
    noResponse = "noResponse"
    noChange = "noChange"
    createdBefore = "createdBefore"


class RetentionRunKind(models.TextChoices):
    sweep = "sweep"
    purge = "purge"


class RetentionExemptionSource(models.TextChoices):
    manual = "manual"
    restore = "restore"


class RetentionTargetType(models.TextChoices):
    survey = "survey"
    user = "user"


class RetentionRunItemAction(models.TextChoices):
    notified = "notified"
    archived = "archived"
    deactivated = "deactivated"
    deleted = "deleted"
    skipped = "skipped"


class RetentionSkipReason(models.TextChoices):
    exempt = "exempt"
    lastOwner = "lastOwner"
    otherOrganization = "otherOrganization"
    noRecipient = "noRecipient"


class WorkflowStatus(models.TextChoices):
    draft = "draft"
    enabled = "enabled"
    disabled = "disabled"
    archived = "archived"


class WorkflowRunStatus(models.TextChoices):
    queued = "queued"
    running = "running"
    completed = "completed"
    failed = "failed"
    canceled = "canceled"


class WorkflowRunLogStatus(models.TextChoices):
    pending = "pending"
    running = "running"
    succeeded = "succeeded"
    failed = "failed"
    skipped = "skipped"


class Creative(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    name = models.TextField()
    type = models.TextField(choices=CreativeType.choices)
    status = models.TextField(choices=CreativeStatus.choices, default=CreativeStatus.draft)
    published_at = UtcDateTimeField(null=True, db_column="publishedAt")
    archived_at = UtcDateTimeField(null=True, db_column="archivedAt")
    workspace = models.ForeignKey("Workspace", on_delete=models.CASCADE, related_name="creatives", db_column="workspaceId", db_index=False)
    creator = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="creatives", db_column="createdBy", null=True, db_index=False)
    content = models.JSONField()
    styling = models.JSONField()
    survey_id = models.TextField(null=True, db_column="surveyId")
    link_url = models.TextField(null=True, db_column="linkUrl")
    show_on = models.TextField(choices=CreativeShowOn.choices, db_column="showOn", default=CreativeShowOn.pageView)
    show_conditions = models.JSONField(null=True, db_column="showConditions")
    display_percentage = models.FloatField(null=True, db_column="displayPercentage")
    start_at = UtcDateTimeField(null=True, db_column="startAt")
    end_at = UtcDateTimeField(null=True, db_column="endAt")
    auto_select_language = models.BooleanField(null=True, db_column="autoSelectLanguage")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Creative"
        indexes = [
            models.Index(fields=["workspace", "updated_at"]),
            models.Index(fields=["workspace", "created_at"]),
            models.Index(fields=["workspace", "name"]),
            models.Index(fields=["survey_id"]),
        ]


class CreativeDailyStat(models.Model):
    pk = models.CompositePrimaryKey("creative_id", "date", "device_type")
    creative = models.ForeignKey("Creative", on_delete=models.CASCADE, related_name="daily_stats", db_column="creativeId", db_index=False)
    date = models.DateField()
    device_type = models.TextField(choices=CreativeDeviceType.choices, db_column="deviceType")
    impressions = models.IntegerField(default=0)
    clicks = models.IntegerField(default=0)

    class Meta:
        db_table = "CreativeDailyStat"


class CreativeDailyPageStat(models.Model):
    pk = models.CompositePrimaryKey("creative_id", "date", "page")
    creative = models.ForeignKey("Creative", on_delete=models.CASCADE, related_name="daily_page_stats", db_column="creativeId", db_index=False)
    date = models.DateField()
    page = models.TextField()
    impressions = models.IntegerField(default=0)
    clicks = models.IntegerField(default=0)

    class Meta:
        db_table = "CreativeDailyPageStat"


class CreativeTrigger(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    creative = models.ForeignKey("Creative", on_delete=models.CASCADE, related_name="triggers", db_column="creativeId", db_index=False)
    action_class = models.ForeignKey("ActionClass", on_delete=models.CASCADE, related_name="creative_triggers", db_column="actionClassId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "CreativeTrigger"
        constraints = [
            models.UniqueConstraint(fields=["creative", "action_class"], name="CreativeTrigger_creativeId_actionClassId_key"),
        ]
        indexes = [
            models.Index(fields=["action_class"]),
        ]


class CreativeLanguage(models.Model):
    pk = models.CompositePrimaryKey("language_id", "creative_id")
    language = models.ForeignKey("Language", on_delete=models.CASCADE, related_name="creative_languages", db_column="languageId", db_index=False)
    creative = models.ForeignKey("Creative", on_delete=models.CASCADE, related_name="languages", db_column="creativeId", db_index=False)
    default = models.BooleanField(default=False)
    enabled = models.BooleanField(default=True)

    class Meta:
        db_table = "CreativeLanguage"
        indexes = [
            models.Index(fields=["creative"]),
        ]


class OutboundEmailAudit(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    expires_at = UtcDateTimeField(db_column="expiresAt")
    content_expires_at = UtcDateTimeField(db_column="contentExpiresAt")
    environment = models.TextField()
    email_type = models.TextField(db_column="emailType")
    trigger = models.TextField()
    actor_kind = models.TextField(db_column="actorKind")
    actor_user_id = models.TextField(null=True, db_column="actorUserId")
    configured_by_user_id = models.TextField(null=True, db_column="configuredByUserId")
    actor_user_hash = models.TextField(null=True, db_column="actorUserHash")
    configured_by_user_hash = models.TextField(null=True, db_column="configuredByUserHash")
    organization_id = models.TextField(null=True, db_column="organizationId")
    survey_id = models.TextField(null=True, db_column="surveyId")
    response_id = models.TextField(null=True, db_column="responseId")
    follow_up_id = models.TextField(null=True, db_column="followUpId")
    workflow_id = models.TextField(null=True, db_column="workflowId")
    workflow_run_id = models.TextField(null=True, db_column="workflowRunId")
    invite_id = models.TextField(null=True, db_column="inviteId")
    recipient_mac = models.TextField(null=True, db_column="recipientMac")
    ses_message_hash = models.TextField(null=True, db_column="sesMessageHash")
    snapshot_key = models.TextField(null=True, db_column="snapshotKey")
    snapshot_sha256 = models.TextField(null=True, db_column="snapshotSha256")
    status = models.TextField(default="prepared")
    status_recorded_at = UtcDateTimeField(null=True, db_column="statusRecordedAt")
    audit_gap = models.BooleanField(db_column="auditGap", default=False)
    erasure_requested_at = UtcDateTimeField(null=True, db_column="erasureRequestedAt")
    erased_at = UtcDateTimeField(null=True, db_column="erasedAt")
    content_delete_retry_at = UtcDateTimeField(null=True, db_column="contentDeleteRetryAt")
    content_delete_failures = models.IntegerField(db_column="contentDeleteFailures", default=0)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "OutboundEmailAudit"
        indexes = [
            models.Index(fields=["environment", "created_at", "id"]),
            models.Index(fields=["organization_id", "created_at", "id"]),
            models.Index(fields=["survey_id", "created_at", "id"]),
            models.Index(fields=["response_id", "created_at", "id"]),
            models.Index(fields=["actor_user_id", "created_at", "id"]),
            models.Index(fields=["configured_by_user_id", "created_at", "id"]),
            models.Index(fields=["actor_user_hash", "created_at", "id"]),
            models.Index(fields=["configured_by_user_hash", "created_at", "id"]),
            models.Index(fields=["recipient_mac", "created_at", "id"]),
            models.Index(fields=["ses_message_hash"]),
            models.Index(fields=["status", "created_at", "id"]),
            models.Index(fields=["content_expires_at", "id"]),
            models.Index(fields=["expires_at", "id"]),
            models.Index(fields=["erasure_requested_at", "id"]),
            models.Index(fields=["content_delete_retry_at", "id"]),
        ]


class OutboundEmailAuditErasureIntent(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    user_id = models.TextField(null=True, db_column="userId")
    user_hash = models.TextField(db_column="userHash")
    recipient_mac = models.TextField(db_column="recipientMac")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    activated_at = UtcDateTimeField(null=True, db_column="activatedAt")
    checked_at = UtcDateTimeField(null=True, db_column="checkedAt")
    expires_at = UtcDateTimeField(db_column="expiresAt")

    class Meta:
        db_table = "OutboundEmailAuditErasureIntent"
        indexes = [
            models.Index(fields=["user_id"]),
            models.Index(fields=["recipient_mac", "activated_at"]),
            models.Index(fields=["user_hash", "activated_at"]),
            models.Index(fields=["checked_at", "id"]),
            models.Index(fields=["expires_at"]),
        ]


class Webhook(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField(null=True)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    url = models.TextField()
    source = models.TextField(choices=WebhookSource.choices, default=WebhookSource.user)
    workspace = models.ForeignKey("Workspace", on_delete=models.CASCADE, related_name="webhooks", db_column="workspaceId", db_index=False)
    triggers = ArrayField(models.TextField(choices=PipelineTriggers.choices))
    survey_ids = ArrayField(models.TextField(), db_column="surveyIds")
    secret = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Webhook"
        indexes = [
            models.Index(fields=["workspace"]),
        ]


class ContactAttribute(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    attribute_key = models.ForeignKey("ContactAttributeKey", on_delete=models.CASCADE, related_name="attributes", db_column="attributeKeyId", db_index=False)
    contact = models.ForeignKey("Contact", on_delete=models.CASCADE, related_name="attributes", db_column="contactId", db_index=False)
    value = models.TextField()
    value_number = models.FloatField(null=True, db_column="valueNumber")
    value_date = UtcDateTimeField(null=True, db_column="valueDate")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ContactAttribute"
        constraints = [
            models.UniqueConstraint(fields=["contact", "attribute_key"], name="ContactAttribute_contactId_attributeKeyId_key"),
        ]
        indexes = [
            models.Index(fields=["attribute_key", "value"]),
            models.Index(fields=["attribute_key", "value_number"]),
            models.Index(fields=["attribute_key", "value_date"]),
        ]


class ContactAttributeKey(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    is_unique = models.BooleanField(db_column="isUnique", default=False)
    key = models.TextField()
    name = models.TextField(null=True)
    description = models.TextField(null=True)
    type = models.TextField(choices=ContactAttributeType.choices, default=ContactAttributeType.custom)
    data_type = models.TextField(choices=ContactAttributeDataType.choices, db_column="dataType", default=ContactAttributeDataType.string)
    workspace = models.ForeignKey("Workspace", on_delete=models.CASCADE, related_name="contact_attribute_keys", db_column="workspaceId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ContactAttributeKey"
        constraints = [
            models.UniqueConstraint(fields=["key", "workspace"], name="ContactAttributeKey_key_workspaceId_key"),
        ]
        indexes = [
            models.Index(fields=["workspace", "created_at"]),
        ]


class Contact(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    workspace = models.ForeignKey("Workspace", on_delete=models.CASCADE, related_name="contacts", db_column="workspaceId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Contact"
        indexes = [
            models.Index(fields=["workspace"]),
        ]


class Response(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    finished = models.BooleanField(default=False)
    survey = models.ForeignKey("Survey", on_delete=models.CASCADE, related_name="responses", db_column="surveyId", db_index=False)
    contact = models.ForeignKey("Contact", on_delete=models.CASCADE, related_name="responses", db_column="contactId", null=True, db_index=False)
    ending_id = models.TextField(null=True, db_column="endingId")
    data = models.JSONField(default=dict)
    variables = models.JSONField(default=dict)
    ttc = models.JSONField(default=dict)
    meta = models.JSONField(default=dict)
    ingest_flags = models.JSONField(null=True)
    outcome = models.TextField(choices=ResponseOutcome.choices, null=True)
    contact_attributes = models.JSONField(null=True, db_column="contactAttributes")
    single_use_id = models.TextField(null=True, db_column="singleUseId")
    language = models.TextField(null=True)
    display = models.OneToOneField("Display", on_delete=models.SET_NULL, related_name="response", db_column="displayId", null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Response"
        constraints = [
            models.UniqueConstraint(fields=["survey", "single_use_id"], name="Response_surveyId_singleUseId_key"),
        ]
        indexes = [
            models.Index(fields=["created_at"]),
            models.Index(fields=["created_at", "id"]),
            models.Index(fields=["survey", "created_at"]),
            models.Index(fields=["survey", "created_at", "id"]),
            models.Index(fields=["contact", "created_at"]),
            models.Index(fields=["contact", "created_at", "id"]),
            models.Index(fields=["survey", "finished"]),
        ]


class Tag(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    name = models.TextField()
    workspace = models.ForeignKey("Workspace", on_delete=models.CASCADE, related_name="tags", db_column="workspaceId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Tag"
        constraints = [
            models.UniqueConstraint(fields=["workspace", "name"], name="Tag_workspaceId_name_key"),
        ]
        indexes = [
            models.Index(fields=["workspace"]),
        ]


class TagsOnResponses(models.Model):
    pk = models.CompositePrimaryKey("response_id", "tag_id")
    response = models.ForeignKey("Response", on_delete=models.CASCADE, related_name="tags", db_column="responseId", db_index=False)
    tag = models.ForeignKey("Tag", on_delete=models.CASCADE, related_name="responses", db_column="tagId", db_index=False)

    class Meta:
        db_table = "TagsOnResponses"


class Display(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    survey = models.ForeignKey("Survey", on_delete=models.CASCADE, related_name="displays", db_column="surveyId", db_index=False)
    contact = models.ForeignKey("Contact", on_delete=models.CASCADE, related_name="displays", db_column="contactId", null=True, db_index=False)
    creative_id = models.TextField(null=True, db_column="creativeId")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Display"
        indexes = [
            models.Index(fields=["survey"]),
            models.Index(fields=["contact", "created_at"]),
        ]


class SurveyTrigger(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    survey = models.ForeignKey("Survey", on_delete=models.CASCADE, related_name="triggers", db_column="surveyId", db_index=False)
    action_class = models.ForeignKey("ActionClass", on_delete=models.CASCADE, related_name="survey_triggers", db_column="actionClassId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "SurveyTrigger"
        constraints = [
            models.UniqueConstraint(fields=["survey", "action_class"], name="SurveyTrigger_surveyId_actionClassId_key"),
        ]


class SurveyAttributeFilter(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    attribute_key = models.ForeignKey("ContactAttributeKey", on_delete=models.CASCADE, related_name="attribute_filters", db_column="attributeKeyId", db_index=False)
    survey = models.ForeignKey("Survey", on_delete=models.CASCADE, related_name="attribute_filters", db_column="surveyId", db_index=False)
    condition = models.TextField(choices=SurveyAttributeFilterCondition.choices)
    value = models.TextField()
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "SurveyAttributeFilter"
        constraints = [
            models.UniqueConstraint(fields=["survey", "attribute_key"], name="SurveyAttributeFilter_surveyId_attributeKeyId_key"),
        ]
        indexes = [
            models.Index(fields=["attribute_key"]),
        ]


class Survey(models.Model):
    hidden_fields = models.JSONField(db_column="hiddenFields", default=survey_hidden_fields_default)
    variables = models.JSONField(default=list)
    ownership_initialized = models.BooleanField(db_column="ownershipInitialized", default=False)
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    name = models.TextField()
    redirect_url = models.TextField(null=True, db_column="redirectUrl")
    type = models.TextField(choices=SurveyType.choices, default=SurveyType.app)
    workspace = models.ForeignKey("Workspace", on_delete=models.CASCADE, related_name="surveys", db_column="workspaceId", db_index=False)
    creator = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="surveys", db_column="createdBy", null=True, db_index=False)
    visibility = models.TextField(choices=SurveyVisibility.choices, default=SurveyVisibility.workspace)
    owner = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="owned_surveys", db_column="ownerId", null=True, db_index=False)
    visibility_version = models.IntegerField(db_column="visibilityVersion", default=0)
    visibility_projected_version = models.IntegerField(db_column="visibilityProjectedVersion", default=0)
    visibility_pending = models.BooleanField(db_column="visibilityPending", default=False)
    visibility_changed_at = UtcDateTimeField(null=True, db_column="visibilityChangedAt")
    visibility_changed_by_id = models.TextField(null=True, db_column="visibilityChangedById")
    status = models.TextField(choices=SurveyStatus.choices, default=SurveyStatus.draft)
    welcome_card = models.JSONField(db_column="welcomeCard", default=survey_welcome_card_default)
    questions = models.JSONField(default=list)
    blocks = ArrayField(models.JSONField(), default=list)
    endings = ArrayField(models.JSONField(), default=list)
    display_option = models.TextField(choices=DisplayOptions.choices, db_column="displayOption", default=DisplayOptions.displayOnce)
    recontact_days = models.IntegerField(null=True, db_column="recontactDays")
    display_limit = models.IntegerField(null=True, db_column="displayLimit")
    inline_triggers = models.JSONField(null=True, db_column="inlineTriggers")
    auto_close = models.IntegerField(null=True, db_column="autoClose")
    keep_ending_card_open = models.BooleanField(db_column="keepEndingCardOpen", default=False)
    auto_complete = models.IntegerField(null=True, db_column="autoComplete")
    delay = models.IntegerField(default=0)
    publish_on = UtcDateTimeField(null=True, db_column="publishOn")
    close_on = UtcDateTimeField(null=True, db_column="closeOn")
    archived_at = UtcDateTimeField(null=True, db_column="archivedAt")
    survey_closed_message = models.JSONField(null=True, db_column="surveyClosedMessage")
    segment = models.ForeignKey("Segment", on_delete=models.SET_NULL, related_name="surveys", db_column="segmentId", null=True, db_index=False)
    workspace_overwrites = models.JSONField(null=True, db_column="workspaceOverwrites")
    styling = models.JSONField(null=True)
    single_use = models.JSONField(null=True, db_column="singleUse", default=survey_single_use_default)
    is_verify_email_enabled = models.BooleanField(db_column="isVerifyEmailEnabled", default=False)
    is_back_button_hidden = models.BooleanField(db_column="isBackButtonHidden", default=False)
    is_auto_progressing_enabled = models.BooleanField(db_column="isAutoProgressingEnabled", default=False)
    is_capture_ip_enabled = models.BooleanField(db_column="isCaptureIpEnabled", default=False)
    is_anonymize_responses_enabled = models.BooleanField(default=False)
    pin = models.TextField(null=True)
    display_percentage = models.DecimalField(max_digits=65, decimal_places=30, null=True, db_column="displayPercentage")
    show_language_switch = models.BooleanField(null=True, db_column="showLanguageSwitch")
    auto_select_language = models.BooleanField(null=True, db_column="autoSelectLanguage")
    recaptcha = models.JSONField(null=True, default=survey_recaptcha_default)
    metadata = models.JSONField(default=dict)
    slug = models.TextField(null=True, unique=True)
    custom_head_scripts = models.TextField(null=True, db_column="customHeadScripts")
    custom_head_scripts_mode = models.TextField(choices=SurveyScriptMode.choices, null=True, db_column="customHeadScriptsMode", default=SurveyScriptMode.add)
    custom_css = models.JSONField(null=True, db_column="customCss")
    show_conditions = models.JSONField(null=True, db_column="showConditions")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Survey"
        constraints = [
            models.UniqueConstraint(fields=["id", "workspace"], name="Survey_id_workspaceId_key"),
        ]
        indexes = [
            models.Index(fields=["workspace", "updated_at"]),
            models.Index(fields=["workspace", "owner"]),
            models.Index(fields=["segment"]),
            models.Index(fields=["status", "publish_on"]),
            models.Index(fields=["status", "close_on"]),
            models.Index(fields=["archived_at"]),
        ]


class SurveyQuota(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    survey = models.ForeignKey("Survey", on_delete=models.CASCADE, related_name="quotas", db_column="surveyId", db_index=False)
    name = models.TextField()
    limit = models.IntegerField()
    logic = models.JSONField(default=dict)
    action = models.TextField(choices=SurveyQuotaAction.choices)
    ending_card_id = models.TextField(null=True, db_column="endingCardId")
    count_partial_submissions = models.BooleanField(db_column="countPartialSubmissions", default=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "SurveyQuota"
        constraints = [
            models.UniqueConstraint(fields=["survey", "name"], name="SurveyQuota_surveyId_name_key"),
        ]


class ResponseQuotaLink(models.Model):
    pk = models.CompositePrimaryKey("response_id", "quota_id")
    response = models.ForeignKey("Response", on_delete=models.CASCADE, related_name="quota_links", db_column="responseId", db_index=False)
    quota = models.ForeignKey("SurveyQuota", on_delete=models.CASCADE, related_name="quota_links", db_column="quotaId", db_index=False)
    status = models.TextField(choices=ResponseQuotaLinkStatus.choices)

    class Meta:
        db_table = "ResponseQuotaLink"
        indexes = [
            models.Index(fields=["quota", "status"]),
        ]


class SurveyFollowUp(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    survey = models.ForeignKey("Survey", on_delete=models.CASCADE, related_name="follow_ups", db_column="surveyId", db_index=False)
    name = models.TextField()
    trigger = models.JSONField()
    action = models.JSONField()
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "SurveyFollowUp"


class ActionClass(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    name = models.TextField()
    description = models.TextField(null=True)
    type = models.TextField(choices=ActionType.choices)
    key = models.TextField(null=True)
    no_code_config = models.JSONField(null=True, db_column="noCodeConfig")
    workspace = models.ForeignKey("Workspace", on_delete=models.CASCADE, related_name="action_classes", db_column="workspaceId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ActionClass"
        constraints = [
            models.UniqueConstraint(fields=["key", "workspace"], name="ActionClass_key_workspaceId_key"),
            models.UniqueConstraint(fields=["name", "workspace"], name="ActionClass_name_workspaceId_key"),
        ]
        indexes = [
            models.Index(fields=["workspace", "created_at"]),
        ]


class Integration(models.Model):
    id = models.TextField(primary_key=True, default=None)
    type = models.TextField(choices=IntegrationType.choices)
    config = models.JSONField()
    workspace = models.ForeignKey("Workspace", on_delete=models.CASCADE, related_name="integrations", db_column="workspaceId", db_index=False)

    class Meta:
        db_table = "Integration"
        constraints = [
            models.UniqueConstraint(fields=["type", "workspace"], name="Integration_type_workspaceId_key"),
        ]
        indexes = [
            models.Index(fields=["workspace"]),
        ]


class DataMigration(models.Model):
    id = models.TextField(primary_key=True, default=None)
    started_at = UtcDateTimeField(default=timezone.now)
    finished_at = UtcDateTimeField(null=True)
    name = models.TextField(unique=True)
    status = models.TextField(choices=DataMigrationStatus.choices)

    class Meta:
        db_table = "DataMigration"


class Workspace(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    name = models.TextField()
    legacy_environment_id = models.TextField(null=True, unique=True, db_column="legacyEnvironmentId")
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="workspaces", db_column="organizationId", db_index=False)
    styling = models.JSONField(default=workspace_styling_default)
    config = models.JSONField(default=dict)
    recontact_days = models.IntegerField(db_column="recontactDays", default=7)
    link_survey_branding = models.BooleanField(db_column="linkSurveyBranding", default=True)
    in_app_survey_branding = models.BooleanField(db_column="inAppSurveyBranding", default=True)
    placement = models.TextField(choices=WidgetPlacement.choices, default=WidgetPlacement.bottomRight)
    click_outside_close = models.BooleanField(db_column="clickOutsideClose", default=True)
    overlay = models.TextField(choices=SurveyOverlay.choices, default=SurveyOverlay.none)
    overlay_color = models.TextField(null=True, db_column="overlayColor")
    overlay_opacity = models.IntegerField(null=True, db_column="overlayOpacity")
    logo = models.JSONField(null=True)
    app_setup_completed = models.BooleanField(db_column="appSetupCompleted", default=False)
    custom_head_scripts = models.TextField(null=True, db_column="customHeadScripts")
    custom_css = models.JSONField(null=True, db_column="customCss")
    custom_css_previous = models.JSONField(null=True, db_column="customCssPrevious")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Workspace"
        constraints = [
            models.UniqueConstraint(fields=["organization", "name"], name="Workspace_organizationId_name_key"),
        ]


class Organization(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    name = models.TextField()
    whitelabel = models.JSONField(default=dict)
    is_aismart_tools_enabled = models.BooleanField(db_column="isAISmartToolsEnabled", default=False)
    display_time_zone = models.TextField(null=True, db_column="displayTimeZone")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Organization"


class OrganizationBilling(models.Model):
    organization = models.OneToOneField("Organization", on_delete=models.CASCADE, related_name="billing", primary_key=True)
    stripe_customer_id = models.TextField(null=True, unique=True)
    limits = models.JSONField()
    usage_cycle_anchor = UtcDateTimeField(null=True)
    stripe = models.JSONField(null=True)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "OrganizationBilling"


class Membership(models.Model):
    pk = models.CompositePrimaryKey("user_id", "organization_id")
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="memberships", db_column="organizationId", db_index=False)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="memberships", db_column="userId", db_index=False)
    accepted = models.BooleanField(default=False)
    role = models.TextField(choices=OrganizationRole.choices, default=OrganizationRole.member)

    class Meta:
        db_table = "Membership"
        indexes = [
            models.Index(fields=["organization"]),
        ]


class Invite(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    email = models.TextField()
    name = models.TextField(null=True)
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="invites", db_column="organizationId", db_index=False)
    creator = models.ForeignKey("User", on_delete=models.RESTRICT, related_name="invites_created", db_column="creatorId", db_index=False)
    acceptor = models.ForeignKey("User", on_delete=models.CASCADE, related_name="invites_accepted", db_column="acceptorId", null=True, db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    expires_at = UtcDateTimeField(db_column="expiresAt")
    role = models.TextField(choices=OrganizationRole.choices, default=OrganizationRole.member)
    team_ids = ArrayField(models.TextField(), db_column="teamIds", default=list)

    class Meta:
        db_table = "Invite"
        indexes = [
            models.Index(fields=["email", "organization"]),
            models.Index(fields=["organization"]),
        ]


class ApiKey(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    created_by = models.TextField(null=True, db_column="createdBy")
    last_used_at = UtcDateTimeField(null=True, db_column="lastUsedAt")
    label = models.TextField()
    hashed_key = models.TextField(db_column="hashedKey")
    lookup_hash = models.TextField(null=True, unique=True, db_column="lookupHash")
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="api_keys", db_column="organizationId", db_index=False)
    organization_access = models.JSONField(db_column="organizationAccess", default=dict)

    class Meta:
        db_table = "ApiKey"
        indexes = [
            models.Index(fields=["organization"]),
        ]


class ApiKeyWorkspace(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    api_key = models.ForeignKey("ApiKey", on_delete=models.CASCADE, related_name="api_key_workspaces", db_column="apiKeyId", db_index=False)
    workspace = models.ForeignKey("Workspace", on_delete=models.CASCADE, related_name="api_key_workspaces", db_column="workspaceId", db_index=False)
    permission = models.TextField(choices=ApiKeyPermission.choices)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ApiKeyWorkspace"
        constraints = [
            models.UniqueConstraint(fields=["api_key", "workspace"], name="ApiKeyWorkspace_apiKeyId_workspaceId_key"),
        ]
        indexes = [
            models.Index(fields=["workspace"]),
        ]


class Account(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="accounts", db_column="userId", db_index=False)
    type = models.TextField(null=True)
    provider = models.TextField()
    provider_account_id = models.TextField(db_column="providerAccountId")
    access_token = models.TextField(null=True)
    refresh_token = models.TextField(null=True)
    expires_at = models.IntegerField(null=True)
    ext_expires_in = models.IntegerField(null=True)
    token_type = models.TextField(null=True)
    scope = models.TextField(null=True)
    id_token = models.TextField(null=True)
    session_state = models.TextField(null=True)
    password = models.TextField(null=True)
    access_token_expires_at = UtcDateTimeField(null=True, db_column="accessTokenExpiresAt")
    refresh_token_expires_at = UtcDateTimeField(null=True, db_column="refreshTokenExpiresAt")
    issuer = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Account"
        constraints = [
            models.UniqueConstraint(fields=["provider", "provider_account_id"], name="Account_provider_providerAccountId_key"),
            models.UniqueConstraint(fields=["issuer", "provider_account_id"], name="Account_issuer_providerAccountId_key"),
        ]
        indexes = [
            models.Index(fields=["user"]),
        ]


class Session(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    session_token = models.TextField(unique=True, db_column="sessionToken")
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="sessions", db_column="userId", db_index=False)
    expires = UtcDateTimeField()
    ip_address = models.TextField(null=True, db_column="ipAddress")
    user_agent = models.TextField(null=True, db_column="userAgent")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Session"
        indexes = [
            models.Index(fields=["user"]),
        ]


class VerificationToken(models.Model):
    pk = models.CompositePrimaryKey("identifier", "token")
    identifier = models.TextField()
    token = models.TextField()
    expires = UtcDateTimeField()

    class Meta:
        db_table = "VerificationToken"
        constraints = [
            models.UniqueConstraint(fields=["identifier", "token"], name="VerificationToken_identifier_token_key"),
        ]


class TwoFactor(models.Model):
    id = models.TextField(primary_key=True, default=None)
    secret = models.TextField()
    backup_codes = models.TextField(db_column="backupCodes")
    user = models.OneToOneField("User", on_delete=models.CASCADE, related_name="two_factor", db_column="userId")
    verified = models.BooleanField(default=True)
    failed_verification_count = models.IntegerField(db_column="failedVerificationCount", default=0)
    locked_until = UtcDateTimeField(null=True, db_column="lockedUntil")

    class Meta:
        db_table = "TwoFactor"
        constraints = [
            models.UniqueConstraint(fields=["user"], name="TwoFactor_userId_key"),
        ]


class Jwks(models.Model):
    id = models.TextField(primary_key=True, default=None)
    public_key = models.TextField(db_column="publicKey")
    private_key = models.TextField(db_column="privateKey")
    created_at = UtcDateTimeField(db_column="createdAt")
    expires_at = UtcDateTimeField(null=True, db_column="expiresAt")
    alg = models.TextField(null=True)
    crv = models.TextField(null=True)

    class Meta:
        db_table = "jwks"


class OauthClient(models.Model):
    id = models.TextField(primary_key=True, default=None)
    client_id = models.TextField(unique=True, db_column="clientId")
    client_secret = models.TextField(null=True, db_column="clientSecret")
    disabled = models.BooleanField(null=True, default=False)
    skip_consent = models.BooleanField(null=True, db_column="skipConsent")
    enable_end_session = models.BooleanField(null=True, db_column="enableEndSession")
    subject_type = models.TextField(null=True, db_column="subjectType")
    scopes = ArrayField(models.TextField(), default=list)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="oauth_clients", db_column="userId", null=True, db_index=False)
    created_at = UtcDateTimeField(null=True, db_column="createdAt")
    updated_at = UtcDateTimeField(null=True, db_column="updatedAt")
    name = models.TextField(null=True)
    uri = models.TextField(null=True)
    icon = models.TextField(null=True)
    contacts = ArrayField(models.TextField(), default=list)
    tos = models.TextField(null=True)
    policy = models.TextField(null=True)
    software_id = models.TextField(null=True, db_column="softwareId")
    software_version = models.TextField(null=True, db_column="softwareVersion")
    software_statement = models.TextField(null=True, db_column="softwareStatement")
    redirect_uris = ArrayField(models.TextField(), db_column="redirectUris")
    post_logout_redirect_uris = ArrayField(models.TextField(), db_column="postLogoutRedirectUris", default=list)
    token_endpoint_auth_method = models.TextField(null=True, db_column="tokenEndpointAuthMethod")
    grant_types = ArrayField(models.TextField(), db_column="grantTypes", default=list)
    response_types = ArrayField(models.TextField(), db_column="responseTypes", default=list)
    public = models.BooleanField(null=True)
    type = models.TextField(null=True)
    require_pkce = models.BooleanField(null=True, db_column="requirePKCE")
    reference_id = models.TextField(null=True, db_column="referenceId")
    metadata = models.JSONField(null=True)
    client_discovery_id = models.TextField(null=True, db_column="clientDiscoveryId")
    client_credentials_scopes = ArrayField(models.TextField(), db_column="clientCredentialsScopes", default=list)
    backchannel_logout_uri = models.TextField(null=True, db_column="backchannelLogoutUri")
    backchannel_logout_session_required = models.BooleanField(null=True, db_column="backchannelLogoutSessionRequired")
    application_type = models.TextField(null=True, db_column="applicationType")
    jwks = models.TextField(null=True)
    jwks_uri = models.TextField(null=True, db_column="jwksUri")
    dpop_bound_access_tokens = models.BooleanField(null=True, db_column="dpopBoundAccessTokens", default=False)

    class Meta:
        db_table = "oauthClient"
        indexes = [
            models.Index(fields=["user"]),
        ]


class OauthAccessToken(models.Model):
    id = models.TextField(primary_key=True, default=None)
    token = models.TextField(unique=True)
    client = models.ForeignKey("OauthClient", on_delete=models.CASCADE, related_name="access_tokens", to_field="client_id", db_column="clientId", db_index=False)
    session = models.ForeignKey("Session", on_delete=models.SET_NULL, related_name="oauth_access_tokens", db_column="sessionId", null=True, db_index=False)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="oauth_access_tokens", db_column="userId", null=True, db_index=False)
    reference_id = models.TextField(null=True, db_column="referenceId")
    refresh = models.ForeignKey("OauthRefreshToken", on_delete=models.CASCADE, related_name="access_tokens", db_column="refreshId", null=True, db_index=False)
    expires_at = UtcDateTimeField(db_column="expiresAt")
    created_at = UtcDateTimeField(db_column="createdAt")
    scopes = ArrayField(models.TextField())
    authorization_code_id = models.TextField(null=True, db_column="authorizationCodeId")
    resources = ArrayField(models.TextField(), default=list)
    requested_user_info_claims = ArrayField(models.TextField(), db_column="requestedUserInfoClaims", default=list)
    revoked = UtcDateTimeField(null=True)
    confirmation = models.JSONField(null=True)

    class Meta:
        db_table = "oauthAccessToken"
        indexes = [
            models.Index(fields=["client"]),
            models.Index(fields=["session"]),
            models.Index(fields=["user"]),
            models.Index(fields=["refresh"]),
            models.Index(fields=["authorization_code_id"]),
        ]


class OauthRefreshToken(models.Model):
    id = models.TextField(primary_key=True, default=None)
    token = models.TextField(unique=True)
    client = models.ForeignKey("OauthClient", on_delete=models.CASCADE, related_name="refresh_tokens", to_field="client_id", db_column="clientId", db_index=False)
    session = models.ForeignKey("Session", on_delete=models.SET_NULL, related_name="oauth_refresh_tokens", db_column="sessionId", null=True, db_index=False)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="oauth_refresh_tokens", db_column="userId", db_index=False)
    reference_id = models.TextField(null=True, db_column="referenceId")
    expires_at = UtcDateTimeField(db_column="expiresAt")
    created_at = UtcDateTimeField(db_column="createdAt")
    revoked = UtcDateTimeField(null=True)
    auth_time = UtcDateTimeField(null=True, db_column="authTime")
    scopes = ArrayField(models.TextField())
    authorization_code_id = models.TextField(null=True, db_column="authorizationCodeId")
    resources = ArrayField(models.TextField(), default=list)
    requested_user_info_claims = ArrayField(models.TextField(), db_column="requestedUserInfoClaims", default=list)
    rotated_at = UtcDateTimeField(null=True, db_column="rotatedAt")
    rotation_replay_response = models.TextField(null=True, db_column="rotationReplayResponse")
    rotation_replay_expires_at = UtcDateTimeField(null=True, db_column="rotationReplayExpiresAt")
    confirmation = models.JSONField(null=True)

    class Meta:
        db_table = "oauthRefreshToken"
        indexes = [
            models.Index(fields=["client"]),
            models.Index(fields=["session"]),
            models.Index(fields=["user"]),
            models.Index(fields=["authorization_code_id"]),
        ]


class OauthConsent(models.Model):
    id = models.TextField(primary_key=True, default=None)
    client = models.ForeignKey("OauthClient", on_delete=models.CASCADE, related_name="consents", to_field="client_id", db_column="clientId", db_index=False)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="oauth_consents", db_column="userId", null=True, db_index=False)
    reference_id = models.TextField(null=True, db_column="referenceId")
    scopes = ArrayField(models.TextField())
    created_at = UtcDateTimeField(db_column="createdAt")
    updated_at = UtcDateTimeField(db_column="updatedAt")
    resources = ArrayField(models.TextField(), default=list)
    requested_user_info_claims = ArrayField(models.TextField(), db_column="requestedUserInfoClaims", default=list)

    class Meta:
        db_table = "oauthConsent"
        indexes = [
            models.Index(fields=["client"]),
            models.Index(fields=["user"]),
        ]


class OauthResource(models.Model):
    id = models.TextField(primary_key=True, default=None)
    identifier = models.TextField(unique=True)
    name = models.TextField()
    access_token_ttl = models.IntegerField(null=True, db_column="accessTokenTtl")
    refresh_token_ttl = models.IntegerField(null=True, db_column="refreshTokenTtl")
    signing_algorithm = models.TextField(null=True, db_column="signingAlgorithm")
    signing_key_id = models.TextField(null=True, db_column="signingKeyId")
    allowed_scopes = ArrayField(models.TextField(), db_column="allowedScopes", default=list)
    custom_claims = models.JSONField(null=True, db_column="customClaims")
    dpop_bound_access_tokens_required = models.BooleanField(null=True, db_column="dpopBoundAccessTokensRequired", default=False)
    disabled = models.BooleanField(null=True, default=False)
    created_at = UtcDateTimeField(null=True, db_column="createdAt")
    updated_at = UtcDateTimeField(null=True, db_column="updatedAt")
    policy_version = models.IntegerField(null=True, db_column="policyVersion", default=1)
    metadata = models.JSONField(null=True)

    class Meta:
        db_table = "oauthResource"


class OauthClientResource(models.Model):
    id = models.TextField(primary_key=True, default=None)
    client = models.ForeignKey("OauthClient", on_delete=models.CASCADE, related_name="resources", to_field="client_id", db_column="clientId", db_index=False)
    resource = models.ForeignKey("OauthResource", on_delete=models.CASCADE, related_name="clients", to_field="identifier", db_column="resourceId", db_index=False)
    metadata = models.JSONField(null=True)
    created_at = UtcDateTimeField(null=True, db_column="createdAt")

    class Meta:
        db_table = "oauthClientResource"
        constraints = [
            models.UniqueConstraint(fields=["client", "resource"], name="oauthClientResource_clientId_resourceId_key"),
        ]
        indexes = [
            models.Index(fields=["resource"]),
        ]


class OauthClientAssertion(models.Model):
    id = models.TextField(primary_key=True, default=None)
    expires_at = UtcDateTimeField(db_column="expiresAt")

    class Meta:
        db_table = "oauthClientAssertion"
        indexes = [
            models.Index(fields=["expires_at"]),
        ]


class PasswordResetToken(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    token_hash = models.TextField(unique=True)
    expires_at = UtcDateTimeField()
    user = models.OneToOneField("User", on_delete=models.CASCADE, related_name="password_reset_token", db_column="userId")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "PasswordResetToken"
        indexes = [
            models.Index(fields=["expires_at"]),
        ]


class User(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    name = models.TextField()
    email = models.TextField(unique=True)
    email_verified = models.BooleanField(default=False)
    two_factor_secret = models.TextField(null=True, db_column="twoFactorSecret")
    two_factor_enabled = models.BooleanField(db_column="twoFactorEnabled", default=False)
    backup_codes = models.TextField(null=True, db_column="backupCodes")
    password = models.TextField(null=True)
    identity_provider = models.TextField(choices=IdentityProvider.choices, db_column="identityProvider", default=IdentityProvider.email)
    identity_provider_account_id = models.TextField(null=True, db_column="identityProviderAccountId")
    group_id = models.TextField(null=True, db_column="groupId")
    notification_settings = models.JSONField(db_column="notificationSettings", default=dict)
    locale = models.TextField(default="en-US")
    last_login_at = UtcDateTimeField(null=True, db_column="lastLoginAt")
    is_active = models.BooleanField(db_column="isActive", default=True)
    reactivated_at = UtcDateTimeField(null=True, db_column="reactivatedAt")
    last_active_at = UtcDateTimeField(null=True, db_column="lastActiveAt")
    is_bootstrap_admin = models.TextField(choices=BootstrapAdminMarker.choices, null=True, unique=True, db_column="isBootstrapAdmin")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "User"


class Segment(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    title = models.TextField()
    description = models.TextField(null=True)
    is_private = models.BooleanField(db_column="isPrivate", default=True)
    filters = models.JSONField(default=list)
    workspace = models.ForeignKey("Workspace", on_delete=models.CASCADE, related_name="segments", db_column="workspaceId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Segment"
        constraints = [
            models.UniqueConstraint(fields=["workspace", "title"], name="Segment_workspaceId_title_key"),
        ]
        indexes = [
            models.Index(fields=["workspace"]),
        ]


class Language(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    code = models.TextField()
    alias = models.TextField(null=True)
    workspace = models.ForeignKey("Workspace", on_delete=models.CASCADE, related_name="languages", db_column="workspaceId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Language"
        constraints = [
            models.UniqueConstraint(fields=["workspace", "code"], name="Language_workspaceId_code_key"),
        ]


class SurveyLanguage(models.Model):
    pk = models.CompositePrimaryKey("language_id", "survey_id")
    language = models.ForeignKey("Language", on_delete=models.CASCADE, related_name="survey_languages", db_column="languageId", db_index=False)
    survey = models.ForeignKey("Survey", on_delete=models.CASCADE, related_name="languages", db_column="surveyId", db_index=False)
    default = models.BooleanField(default=False)
    enabled = models.BooleanField(default=True)

    class Meta:
        db_table = "SurveyLanguage"
        indexes = [
            models.Index(fields=["survey"]),
        ]


class EmbeddedData(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    key = models.TextField(null=True)
    name = models.TextField()
    description = models.TextField(null=True)
    source = models.TextField(choices=EmbeddedDataSource.choices)
    data_type = models.TextField(choices=EmbeddedDataType.choices, db_column="dataType", default=EmbeddedDataType.string)
    default_value = models.JSONField(null=True, db_column="defaultValue")
    locked = models.BooleanField(default=False)
    survey_id = models.TextField(null=True, db_column="surveyId")
    workspace = models.ForeignKey("Workspace", on_delete=models.CASCADE, related_name="embedded_data", db_column="workspaceId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "EmbeddedData"
        constraints = [
            models.UniqueConstraint(fields=["workspace", "key"], name="EmbeddedData_workspaceId_key_key"),
            models.UniqueConstraint(fields=["id", "workspace"], name="EmbeddedData_id_workspaceId_key"),
        ]
        indexes = [
            models.Index(fields=["survey_id"]),
        ]


class SurveyEmbeddedData(models.Model):
    id = models.TextField(primary_key=True, default=None)
    workspace_id = models.TextField(db_column="workspaceId")
    survey_id = models.TextField(db_column="surveyId")
    embedded_data_id = models.TextField(db_column="embeddedDataId")
    storage_key = models.TextField(db_column="storageKey")
    order = models.IntegerField()

    class Meta:
        db_table = "SurveyEmbeddedData"
        constraints = [
            models.UniqueConstraint(fields=["survey_id", "embedded_data_id"], name="SurveyEmbeddedData_surveyId_embeddedDataId_key"),
            models.UniqueConstraint(fields=["survey_id", "storage_key"], name="SurveyEmbeddedData_surveyId_storageKey_key"),
        ]
        indexes = [
            models.Index(fields=["embedded_data_id"]),
            models.Index(fields=["survey_id", "order"]),
        ]


class Team(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    name = models.TextField()
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="teams", db_column="organizationId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Team"
        constraints = [
            models.UniqueConstraint(fields=["organization", "name"], name="Team_organizationId_name_key"),
        ]


class TeamUser(models.Model):
    pk = models.CompositePrimaryKey("team_id", "user_id")
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="team_users", db_column="teamId", db_index=False)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="team_users", db_column="userId", db_index=False)
    role = models.TextField(choices=TeamUserRole.choices)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "TeamUser"
        indexes = [
            models.Index(fields=["user"]),
        ]


class WorkspaceTeam(models.Model):
    pk = models.CompositePrimaryKey("workspace_id", "team_id")
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    workspace = models.ForeignKey("Workspace", on_delete=models.CASCADE, related_name="workspace_teams", db_column="workspaceId", db_index=False)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="workspace_teams", db_column="teamId", db_index=False)
    permission = models.TextField(choices=WorkspaceTeamPermission.choices, default=WorkspaceTeamPermission.read)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "WorkspaceTeam"
        indexes = [
            models.Index(fields=["team"]),
        ]


class Chart(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    name = models.TextField()
    type = models.TextField(choices=ChartType.choices)
    workspace = models.ForeignKey("Workspace", on_delete=models.CASCADE, related_name="charts", db_column="workspaceId", db_index=False)
    query = models.JSONField(default=dict)
    config = models.JSONField(default=dict)
    creator = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="charts", db_column="createdBy", null=True, db_index=False)
    feedback_directory = models.ForeignKey("FeedbackDirectory", on_delete=models.RESTRICT, related_name="charts", db_column="feedbackDirectoryId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Chart"
        constraints = [
            models.UniqueConstraint(fields=["workspace", "name"], name="Chart_workspaceId_name_key"),
        ]
        indexes = [
            models.Index(fields=["workspace", "created_at"]),
            models.Index(fields=["feedback_directory"]),
        ]


class Dashboard(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    name = models.TextField()
    workspace = models.ForeignKey("Workspace", on_delete=models.CASCADE, related_name="dashboards", db_column="workspaceId", db_index=False)
    creator = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="dashboards", db_column="createdBy", null=True, db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Dashboard"
        constraints = [
            models.UniqueConstraint(fields=["workspace", "name"], name="Dashboard_workspaceId_name_key"),
        ]
        indexes = [
            models.Index(fields=["workspace", "created_at"]),
        ]


class DashboardWidget(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    dashboard = models.ForeignKey("Dashboard", on_delete=models.CASCADE, related_name="widgets", db_column="dashboardId", db_index=False)
    chart = models.ForeignKey("Chart", on_delete=models.CASCADE, related_name="widgets", db_column="chartId", db_index=False)
    layout = models.JSONField(default=dashboard_widget_layout_default)
    order = models.IntegerField(default=0)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "DashboardWidget"
        constraints = [
            models.UniqueConstraint(fields=["dashboard", "chart"], name="DashboardWidget_dashboardId_chartId_key"),
        ]
        indexes = [
            models.Index(fields=["dashboard", "order"]),
        ]


class DeletionCleanup(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    kind = models.TextField(choices=DeletionCleanupKind.choices)
    organization_id = models.TextField(db_column="organizationId")
    workspace_id = models.TextField(db_column="workspaceId")
    survey_id = models.TextField(db_column="surveyId")
    tenant_ids = ArrayField(models.TextField(), db_column="tenantIds", default=list)
    response_ids = ArrayField(models.TextField(), db_column="responseIds", default=list)
    file_keys = ArrayField(models.TextField(), db_column="fileKeys", default=list)
    attempts = models.IntegerField(default=0)
    next_attempt_at = UtcDateTimeField(db_column="nextAttemptAt", default=timezone.now)
    last_error = models.TextField(null=True, db_column="lastError")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "DeletionCleanup"
        indexes = [
            models.Index(fields=["next_attempt_at"]),
            models.Index(fields=["organization_id", "created_at"]),
        ]


class AttachmentExport(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    survey_id = models.TextField(db_column="surveyId")
    workspace = models.ForeignKey("Workspace", on_delete=models.CASCADE, related_name="attachment_exports", db_column="workspaceId", db_index=False)
    requested_by = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="attachment_exports", db_column="requestedById", null=True, db_index=False)
    status = models.TextField(choices=AttachmentExportStatus.choices, default=AttachmentExportStatus.queued)
    filter_criteria = models.JSONField(null=True, db_column="filterCriteria")
    time_zone = models.TextField(db_column="timeZone")
    dispatch_count = models.IntegerField(db_column="dispatchCount", default=0)
    file_count = models.IntegerField(db_column="fileCount")
    response_count = models.IntegerField(db_column="responseCount")
    included_file_count = models.IntegerField(null=True, db_column="includedFileCount")
    total_bytes = models.BigIntegerField(null=True, db_column="totalBytes")
    truncated = models.BooleanField(default=False)
    storage_key = models.TextField(null=True, db_column="storageKey")
    error_code = models.TextField(null=True, db_column="errorCode")
    started_at = UtcDateTimeField(null=True, db_column="startedAt")
    finished_at = UtcDateTimeField(null=True, db_column="finishedAt")
    expires_at = UtcDateTimeField(null=True, db_column="expiresAt")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "AttachmentExport"
        indexes = [
            models.Index(fields=["survey_id", "requested_by", "created_at"]),
            models.Index(fields=["status", "created_at"]),
            models.Index(fields=["expires_at"]),
            models.Index(fields=["workspace"]),
        ]


class FeedbackSource(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    name = models.TextField()
    type = models.TextField(choices=FeedbackSourceType.choices)
    status = models.TextField(choices=FeedbackSourceStatus.choices, default=FeedbackSourceStatus.active)
    import_mode = models.TextField(choices=FeedbackSourceImportMode.choices, default=FeedbackSourceImportMode.completedOnly)
    element_scope = models.TextField(choices=FeedbackSourceElementScope.choices, db_column="elementScope", default=FeedbackSourceElementScope.specific)
    workspace = models.ForeignKey("Workspace", on_delete=models.CASCADE, related_name="feedback_sources", db_column="workspaceId", db_index=False)
    feedback_directory = models.ForeignKey("FeedbackDirectory", on_delete=models.CASCADE, related_name="feedback_sources", db_column="feedbackDirectoryId", db_index=False)
    last_sync_at = UtcDateTimeField(null=True)
    creator = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="feedback_sources", db_column="created_by", null=True, db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "FeedbackSource"
        constraints = [
            models.UniqueConstraint(fields=["id", "workspace"], name="FeedbackSource_id_workspaceId_key"),
            models.UniqueConstraint(fields=["workspace", "name"], name="FeedbackSource_workspaceId_name_key"),
        ]
        indexes = [
            models.Index(fields=["type"]),
            models.Index(fields=["feedback_directory", "workspace"]),
        ]


class FeedbackSourceFormbricksMapping(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    feedback_source_id = models.TextField()
    workspace_id = models.TextField(db_column="workspaceId")
    survey_id = models.TextField(db_column="surveyId")
    element_id = models.TextField(db_column="elementId")
    hub_field_type = models.TextField(choices=HubFieldType.choices, db_column="hubFieldType")
    custom_field_label = models.TextField(null=True)

    class Meta:
        db_table = "FeedbackSourceFormbricksMapping"
        constraints = [
            models.UniqueConstraint(fields=["workspace_id", "feedback_source_id", "survey_id", "element_id"], name="FeedbackSourceFormbricksMapping_workspaceId_feedbackSourceId_su"),
        ]
        indexes = [
            models.Index(fields=["workspace_id", "survey_id"]),
            models.Index(fields=["survey_id"]),
        ]


class FeedbackSourceFieldMapping(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    feedback_source_id = models.TextField()
    workspace_id = models.TextField(db_column="workspaceId")
    source_field_id = models.TextField()
    target_field_id = models.TextField()
    static_value = models.TextField(null=True)

    class Meta:
        db_table = "FeedbackSourceFieldMapping"
        constraints = [
            models.UniqueConstraint(fields=["workspace_id", "feedback_source_id", "source_field_id", "target_field_id"], name="FeedbackSourceFieldMapping_workspaceId_feedbackSourceId_sourceF"),
        ]


class FeedbackDirectory(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    name = models.TextField()
    is_archived = models.BooleanField(db_column="isArchived", default=False)
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="feedback_directories", db_column="organizationId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "FeedbackDirectory"
        constraints = [
            models.UniqueConstraint(fields=["organization", "name"], name="FeedbackDirectory_organizationId_name_key"),
        ]


class FeedbackDirectoryWorkspace(models.Model):
    pk = models.CompositePrimaryKey("feedback_directory_id", "workspace_id")
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    feedback_directory = models.ForeignKey("FeedbackDirectory", on_delete=models.CASCADE, related_name="workspaces", db_column="feedbackDirectoryId", db_index=False)
    workspace = models.ForeignKey("Workspace", on_delete=models.CASCADE, related_name="feedback_directory_workspaces", db_column="workspaceId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "FeedbackDirectoryWorkspace"
        indexes = [
            models.Index(fields=["workspace"]),
        ]


class AuthzedProjectionScopeState(models.Model):
    scope = models.TextField(primary_key=True, default=None)
    ready_at = UtcDateTimeField(null=True, db_column="readyAt")
    ready_by = models.TextField(null=True, db_column="readyBy")
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "AuthzedProjectionScopeState"


class AuthzedProjectionOutbox(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    target_type = models.TextField(db_column="targetType")
    primary_id = models.TextField(db_column="primaryId")
    secondary_id = models.TextField(null=True, db_column="secondaryId")
    is_revocation = models.BooleanField(db_column="isRevocation", default=False)
    attempts = models.IntegerField(default=0)
    permanent_failures = models.IntegerField(db_column="permanentFailures", default=0)
    available_at = UtcDateTimeField(db_column="availableAt", default=timezone.now)
    leased_at = UtcDateTimeField(null=True, db_column="leasedAt")
    lease_expires_at = UtcDateTimeField(null=True, db_column="leaseExpiresAt")
    lease_owner = models.TextField(null=True, db_column="leaseOwner")
    processed_at = UtcDateTimeField(null=True, db_column="processedAt")
    dead_lettered_at = UtcDateTimeField(null=True, db_column="deadLetteredAt")
    last_attempt_at = UtcDateTimeField(null=True, db_column="lastAttemptAt")
    last_error_code = models.TextField(null=True, db_column="lastErrorCode")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "AuthzedProjectionOutbox"


class OnlineUpgradeState(models.Model):
    release = models.TextField(primary_key=True, default=None)
    target_image = models.TextField(db_column="targetImage")
    phase = models.TextField()
    completed_phases = ArrayField(models.TextField(), db_column="completedPhases", default=list)
    survey_cursor = models.TextField(null=True, db_column="surveyCursor")
    survey_count = models.BigIntegerField(db_column="surveyCount", default=0)
    baseline_completed_at = UtcDateTimeField(null=True, db_column="baselineCompletedAt")
    forward_only_at = UtcDateTimeField(null=True, db_column="forwardOnlyAt")
    queue_paused_at = UtcDateTimeField(null=True, db_column="queuePausedAt")
    queue_pause_deadline_at = UtcDateTimeField(null=True, db_column="queuePauseDeadlineAt")
    queue_resumed_at = UtcDateTimeField(null=True, db_column="queueResumedAt")
    activated_at = UtcDateTimeField(null=True, db_column="activatedAt")
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "OnlineUpgradeState"


class RetentionPolicy(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="retention_policies", db_column="organizationId", db_index=False)
    entity = models.TextField(choices=RetentionEntity.choices)
    enabled = models.BooleanField(default=False)
    enabled_at = UtcDateTimeField(null=True, db_column="enabledAt")
    warn_days = models.IntegerField(db_column="warnDays", default=60)
    period_days = models.IntegerField(db_column="periodDays")
    conditions = ArrayField(models.TextField(choices=RetentionSurveyCondition.choices), default=list)
    deferred_at = UtcDateTimeField(null=True, db_column="deferredAt")
    updated_by = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="retention_policies_updated", db_column="updatedById", null=True, db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "RetentionPolicy"
        constraints = [
            models.UniqueConstraint(fields=["organization", "entity"], name="RetentionPolicy_organizationId_entity_key"),
        ]
        indexes = [
            models.Index(fields=["updated_by"]),
        ]


class RetentionExemption(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="retention_exemptions", db_column="organizationId", db_index=False)
    entity = models.TextField(choices=RetentionEntity.choices)
    survey = models.ForeignKey("Survey", on_delete=models.CASCADE, related_name="retention_exemptions", db_column="surveyId", null=True, db_index=False)
    until = UtcDateTimeField()
    source = models.TextField(choices=RetentionExemptionSource.choices, default=RetentionExemptionSource.manual)
    reason = models.TextField(null=True)
    created_by = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="exemptions_created", db_column="createdById", null=True, db_index=False)
    revoked_at = UtcDateTimeField(null=True, db_column="revokedAt")
    revoked_by = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="exemptions_revoked", db_column="revokedById", null=True, db_index=False)

    class Meta:
        db_table = "RetentionExemption"
        indexes = [
            models.Index(fields=["organization", "created_at", "id"]),
            models.Index(fields=["survey"]),
            models.Index(fields=["created_by"]),
            models.Index(fields=["revoked_by"]),
        ]


class RetentionRun(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="retention_runs", db_column="organizationId", db_index=False)
    entity = models.TextField(choices=RetentionEntity.choices)
    kind = models.TextField(choices=RetentionRunKind.choices, default=RetentionRunKind.sweep)
    started_at = UtcDateTimeField(db_column="startedAt", default=timezone.now)
    finished_at = UtcDateTimeField(null=True, db_column="finishedAt")
    notified_count = models.IntegerField(db_column="notifiedCount", default=0)
    archived_count = models.IntegerField(db_column="archivedCount", default=0)
    deleted_count = models.IntegerField(db_column="deletedCount", default=0)
    skipped_count = models.IntegerField(db_column="skippedCount", default=0)
    has_changes = models.BooleanField(db_column="hasChanges", default=False)
    scan_cursor = models.TextField(null=True, db_column="scanCursor")

    class Meta:
        db_table = "RetentionRun"
        indexes = [
            models.Index(fields=["organization", "started_at", "id"]),
        ]


class RetentionRunItem(models.Model):
    id = models.TextField(primary_key=True, default=None)
    run = models.ForeignKey("RetentionRun", on_delete=models.CASCADE, related_name="items", db_column="runId", db_index=False)
    target_type = models.TextField(choices=RetentionTargetType.choices, db_column="targetType")
    target_id = models.TextField(db_column="targetId")
    target_name = models.TextField(null=True, db_column="targetName")
    action = models.TextField(choices=RetentionRunItemAction.choices)
    count = models.IntegerField(default=1)
    recipient = models.TextField(null=True)
    skip_reason = models.TextField(choices=RetentionSkipReason.choices, null=True, db_column="skipReason")

    class Meta:
        db_table = "RetentionRunItem"
        indexes = [
            models.Index(fields=["run", "id"]),
            models.Index(fields=["target_id"]),
        ]


class RetentionNotice(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="retention_notices", db_column="organizationId", db_index=False)
    entity = models.TextField(choices=RetentionEntity.choices)
    survey = models.ForeignKey("Survey", on_delete=models.CASCADE, related_name="retention_notices", db_column="surveyId", null=True, db_index=False)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="retention_notices", db_column="userId", null=True, db_index=False)
    sent_at = UtcDateTimeField(db_column="sentAt", default=timezone.now)
    delivered_at = UtcDateTimeField(null=True, db_column="deliveredAt")
    email_sent = models.BooleanField(db_column="emailSent", default=False)
    claim_token = models.TextField(null=True, db_column="claimToken")
    clock_at = UtcDateTimeField(null=True, db_column="clockAt")

    class Meta:
        db_table = "RetentionNotice"
        constraints = [
            models.UniqueConstraint(fields=["survey", "entity"], name="RetentionNotice_surveyId_entity_key"),
            models.UniqueConstraint(fields=["user", "organization", "entity"], name="RetentionNotice_userId_organizationId_entity_key"),
        ]
        indexes = [
            models.Index(fields=["organization"]),
        ]


class RetentionSurveyRestore(models.Model):
    survey = models.OneToOneField("Survey", on_delete=models.CASCADE, related_name="retention_restore", primary_key=True, db_column="surveyId")
    restored_at = UtcDateTimeField(db_column="restoredAt")

    class Meta:
        db_table = "RetentionSurveyRestore"


class Workflow(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    name = models.TextField()
    description = models.TextField(null=True)
    status = models.TextField(choices=WorkflowStatus.choices, default=WorkflowStatus.draft)
    workspace = models.ForeignKey("Workspace", on_delete=models.CASCADE, related_name="workflows", db_column="workspaceId", db_index=False)
    creator = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="workflows", db_column="createdBy", null=True, db_index=False)
    definition = models.JSONField()
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Workflow"
        constraints = [
            models.UniqueConstraint(fields=["id", "workspace"], name="Workflow_id_workspaceId_key"),
        ]
        indexes = [
            models.Index(fields=["workspace", "status"]),
            models.Index(fields=["workspace", "updated_at"]),
        ]


class WorkflowVersion(models.Model):
    id = models.TextField(primary_key=True, default=None)
    workflow_id = models.TextField(db_column="workflowId")
    workspace_id = models.TextField(db_column="workspaceId")
    version = models.IntegerField()
    definition = models.JSONField()
    published_at = UtcDateTimeField(db_column="publishedAt", default=timezone.now)
    publisher = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="workflow_versions_published", db_column="publishedBy", null=True, db_index=False)

    class Meta:
        db_table = "WorkflowVersion"
        constraints = [
            models.UniqueConstraint(fields=["workflow_id", "version"], name="WorkflowVersion_workflowId_version_key"),
        ]
        indexes = [
            models.Index(fields=["workflow_id", "workspace_id"]),
            models.Index(fields=["publisher"]),
        ]


class WorkflowRun(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    workflow_id = models.TextField(db_column="workflowId")
    workspace = models.ForeignKey("Workspace", on_delete=models.CASCADE, related_name="workflow_runs", db_column="workspaceId", db_index=False)
    workflow_version = models.ForeignKey("WorkflowVersion", on_delete=models.SET_NULL, related_name="runs", db_column="workflowVersionId", null=True, db_index=False)
    response = models.ForeignKey("Response", on_delete=models.CASCADE, related_name="workflow_runs", db_column="responseId", null=True, db_index=False)
    status = models.TextField(choices=WorkflowRunStatus.choices, default=WorkflowRunStatus.queued)
    trigger_type = models.TextField(db_column="triggerType")
    survey_id = models.TextField(null=True, db_column="surveyId")
    is_dry_run = models.BooleanField(db_column="isDryRun", default=False)
    idempotency_key = models.TextField(null=True, db_column="idempotencyKey")
    attempt = models.IntegerField(default=0)
    next_attempt_at = UtcDateTimeField(null=True, db_column="nextAttemptAt")
    last_error_at = UtcDateTimeField(null=True, db_column="lastErrorAt")
    trigger_payload = models.JSONField(db_column="triggerPayload")
    data = models.JSONField(default=dict)
    error = models.TextField(null=True)
    dispatched_at = UtcDateTimeField(null=True, db_column="dispatchedAt")
    started_at = UtcDateTimeField(null=True, db_column="startedAt")
    finished_at = UtcDateTimeField(null=True, db_column="finishedAt")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "WorkflowRun"
        constraints = [
            models.UniqueConstraint(fields=["workflow_id", "idempotency_key"], name="WorkflowRun_workflowId_idempotencyKey_key"),
        ]
        indexes = [
            models.Index(fields=["workspace", "created_at"]),
            models.Index(fields=["workflow_id", "workspace", "created_at"]),
            models.Index(fields=["status", "created_at"]),
            models.Index(fields=["response"]),
            models.Index(fields=["is_dry_run"]),
            models.Index(fields=["next_attempt_at"]),
        ]


class WorkflowRunLog(models.Model):
    id = models.TextField(primary_key=True, default=None)
    run = models.ForeignKey("WorkflowRun", on_delete=models.CASCADE, related_name="logs", db_column="runId", db_index=False)
    sequence = models.IntegerField()
    step_id = models.TextField(db_column="stepId")
    step_type = models.TextField(db_column="stepType")
    status = models.TextField(choices=WorkflowRunLogStatus.choices, default=WorkflowRunLogStatus.pending)
    input = models.JSONField(default=dict)
    output = models.JSONField(default=dict)
    error = models.TextField(null=True)
    started_at = UtcDateTimeField(null=True, db_column="startedAt")
    finished_at = UtcDateTimeField(null=True, db_column="finishedAt")

    class Meta:
        db_table = "WorkflowRunLog"
        constraints = [
            models.UniqueConstraint(fields=["run", "step_id"], name="WorkflowRunLog_runId_stepId_key"),
        ]
        indexes = [
            models.Index(fields=["run", "sequence"]),
        ]
