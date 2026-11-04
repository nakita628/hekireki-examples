from datetime import datetime
from datetime import timezone as dt_timezone
from typing import Any, TypeVar

from django.conf import settings
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


class ChatProvider(models.TextChoices):
    WHATSAPP = "WHATSAPP"
    DIALOG360 = "DIALOG360"


class WorkspaceRole(models.TextChoices):
    ADMIN = "ADMIN"
    MEMBER = "MEMBER"
    GUEST = "GUEST"


class GraphNavigation(models.TextChoices):
    MOUSE = "MOUSE"
    TRACKPAD = "TRACKPAD"


class Plan(models.TextChoices):
    FREE = "FREE"
    STARTER = "STARTER"
    PRO = "PRO"
    LIFETIME = "LIFETIME"
    OFFERED = "OFFERED"
    CUSTOM = "CUSTOM"
    UNLIMITED = "UNLIMITED"
    ENTERPRISE = "ENTERPRISE"


class CollaborationType(models.TextChoices):
    READ = "READ"
    WRITE = "WRITE"
    FULL_ACCESS = "FULL_ACCESS"


class Account(models.Model):
    id = models.TextField(primary_key=True, default=None)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="accounts", db_column="userId", db_index=False)
    type = models.TextField()
    provider = models.TextField()
    provider_account_id = models.TextField(db_column="providerAccountId")
    refresh_token = models.TextField(null=True)
    access_token = models.TextField(null=True)
    expires_at = models.IntegerField(null=True)
    token_type = models.TextField(null=True)
    scope = models.TextField(null=True)
    id_token = models.TextField(null=True)
    session_state = models.TextField(null=True)
    oauth_token_secret = models.TextField(null=True)
    oauth_token = models.TextField(null=True)
    refresh_token_expires_in = models.IntegerField(null=True)

    class Meta:
        db_table = "Account"
        constraints = [
            models.UniqueConstraint(fields=["provider", "provider_account_id"], name="Account_provider_providerAccountId_key"),
        ]


class Session(models.Model):
    id = models.TextField(primary_key=True, default=None)
    session_token = models.TextField(unique=True, db_column="sessionToken")
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="sessions", db_column="userId", db_index=False)
    expires = UtcDateTimeField()

    class Meta:
        db_table = "Session"


class User(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    last_activity_at = UtcDateTimeField(db_column="lastActivityAt", default=timezone.now)
    name = models.TextField(null=True)
    email = models.TextField(null=True, unique=True)
    email_verified = UtcDateTimeField(null=True, db_column="emailVerified")
    image = models.TextField(null=True)
    company = models.TextField(null=True)
    onboarding_categories = models.JSONField(db_column="onboardingCategories")
    referral = models.TextField(null=True)
    graph_navigation = models.TextField(choices=GraphNavigation.choices, null=True, db_column="graphNavigation")
    preferred_app_appearance = models.TextField(null=True, db_column="preferredAppAppearance")
    displayed_in_app_notifications = models.JSONField(null=True, db_column="displayedInAppNotifications")
    group_titles_auto_generation = models.JSONField(null=True, db_column="groupTitlesAutoGeneration")
    preferred_language = models.TextField(null=True, db_column="preferredLanguage")
    terms_accepted_at = UtcDateTimeField(null=True, db_column="termsAcceptedAt")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "User"


class ApiToken(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    token = models.TextField(unique=True)
    name = models.TextField()
    owner = models.ForeignKey("User", on_delete=models.CASCADE, related_name="api_tokens", db_column="ownerId", db_index=False)

    class Meta:
        db_table = "ApiToken"


class Workspace(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    name = models.TextField()
    icon = models.TextField(null=True)
    plan = models.TextField(choices=Plan.choices, default=Plan.FREE)
    stripe_id = models.TextField(null=True, unique=True, db_column="stripeId")
    additional_chats_index = models.IntegerField(db_column="additionalChatsIndex", default=0)
    additional_storage_index = models.IntegerField(db_column="additionalStorageIndex", default=0)
    chats_limit_first_email_sent_at = UtcDateTimeField(null=True, db_column="chatsLimitFirstEmailSentAt")
    storage_limit_first_email_sent_at = UtcDateTimeField(null=True, db_column="storageLimitFirstEmailSentAt")
    chats_limit_second_email_sent_at = UtcDateTimeField(null=True, db_column="chatsLimitSecondEmailSentAt")
    storage_limit_second_email_sent_at = UtcDateTimeField(null=True, db_column="storageLimitSecondEmailSentAt")
    custom_chats_limit = models.IntegerField(null=True, db_column="customChatsLimit")
    custom_storage_limit = models.IntegerField(null=True, db_column="customStorageLimit")
    custom_seats_limit = models.IntegerField(null=True, db_column="customSeatsLimit")
    is_quarantined = models.BooleanField(db_column="isQuarantined", default=False)
    is_suspended = models.BooleanField(db_column="isSuspended", default=False)
    is_past_due = models.BooleanField(db_column="isPastDue", default=False)
    is_verified = models.BooleanField(null=True, db_column="isVerified")
    chats_hard_limit = models.IntegerField(null=True, db_column="chatsHardLimit")
    settings = models.JSONField(null=True)
    last_activity_at = UtcDateTimeField(null=True, db_column="lastActivityAt")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Workspace"


class Space(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    name = models.TextField()
    icon = models.TextField(null=True)
    workspace = models.ForeignKey("Workspace", on_delete=models.CASCADE, related_name="spaces", db_column="workspaceId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Space"
        constraints = [
            models.UniqueConstraint(fields=["id", "workspace"], name="Space_id_workspaceId_key"),
        ]


class MemberInWorkspace(models.Model):
    pk = models.CompositePrimaryKey("user_id", "workspace_id")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="workspaces", db_column="userId", db_index=False)
    workspace = models.ForeignKey("Workspace", on_delete=models.CASCADE, related_name="members", db_column="workspaceId", db_index=False)
    role = models.TextField(choices=WorkspaceRole.choices)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "MemberInWorkspace"
        constraints = [
            models.UniqueConstraint(fields=["user", "workspace"], name="MemberInWorkspace_userId_workspaceId_key"),
        ]


class WorkspaceInvitation(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    email = models.TextField()
    workspace = models.ForeignKey("Workspace", on_delete=models.CASCADE, related_name="invitations", db_column="workspaceId", db_index=False)
    type = models.TextField(choices=WorkspaceRole.choices)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "WorkspaceInvitation"


class CustomDomain(models.Model):
    name = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    workspace = models.ForeignKey("Workspace", on_delete=models.CASCADE, related_name="custom_domains", db_column="workspaceId", db_index=False)

    class Meta:
        db_table = "CustomDomain"


class Credentials(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    workspace = models.ForeignKey("Workspace", on_delete=models.CASCADE, related_name="credentials", db_column="workspaceId", db_index=False)
    data = models.TextField()
    name = models.TextField()
    type = models.TextField()
    iv = models.TextField()

    class Meta:
        db_table = "Credentials"


class UserCredentials(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="credentials", db_column="userId", db_index=False)
    data = models.TextField()
    name = models.TextField()
    type = models.TextField()
    iv = models.TextField()

    class Meta:
        db_table = "UserCredentials"


class VerificationToken(models.Model):
    identifier = models.TextField()
    token = models.TextField(primary_key=True, default=None)
    value = models.TextField(null=True)
    failed_attempts = models.IntegerField(db_column="failedAttempts", default=0)
    expires = UtcDateTimeField()

    class Meta:
        db_table = "VerificationToken"
        constraints = [
            models.UniqueConstraint(fields=["identifier", "token"], name="VerificationToken_identifier_token_key"),
        ]


class DashboardFolder(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    name = models.TextField()
    parent_folder = models.ForeignKey("self", on_delete=models.SET_NULL, related_name="children_folder", db_column="parentFolderId", null=True, db_index=False)
    workspace = models.ForeignKey("Workspace", on_delete=models.CASCADE, related_name="folders", db_column="workspaceId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "DashboardFolder"


class Typebot(models.Model):
    id = models.TextField(primary_key=True, default=None)
    version = models.TextField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    icon = models.TextField(null=True)
    name = models.TextField()
    folder = models.ForeignKey("DashboardFolder", on_delete=models.SET_NULL, related_name="typebots", db_column="folderId", null=True, db_index=False)
    groups = models.JSONField()
    events = models.JSONField(null=True)
    variables = models.JSONField()
    edges = models.JSONField()
    theme = models.JSONField()
    selected_theme_template_id = models.TextField(null=True, db_column="selectedThemeTemplateId")
    settings = models.JSONField()
    public_id = models.TextField(null=True, unique=True, db_column="publicId")
    custom_domain = models.TextField(null=True, unique=True, db_column="customDomain")
    workspace = models.ForeignKey("Workspace", on_delete=models.CASCADE, related_name="typebots", db_column="workspaceId", db_index=False)
    results_table_preferences = models.JSONField(null=True, db_column="resultsTablePreferences")
    is_archived = models.BooleanField(db_column="isArchived", default=False)
    is_closed = models.BooleanField(db_column="isClosed", default=False)
    whats_app_credentials_id = models.TextField(null=True, db_column="whatsAppCredentialsId")
    risk_level = models.IntegerField(null=True, db_column="riskLevel")
    space = models.ForeignKey("Space", on_delete=models.SET_NULL, related_name="typebots", db_column="spaceId", null=True, db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Typebot"
        indexes = [
            models.Index(fields=["workspace"]),
            models.Index(fields=["is_archived", "-created_at"]),
        ]


class Invitation(models.Model):
    pk = models.CompositePrimaryKey("email", "typebot_id")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    email = models.TextField()
    typebot = models.ForeignKey("Typebot", on_delete=models.CASCADE, related_name="invitations", db_column="typebotId", db_index=False)
    type = models.TextField(choices=CollaborationType.choices)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Invitation"
        constraints = [
            models.UniqueConstraint(fields=["email", "typebot"], name="Invitation_email_typebotId_key"),
        ]


class CollaboratorsOnTypebots(models.Model):
    pk = models.CompositePrimaryKey("user_id", "typebot_id")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="collaborators_on_typebots", db_column="userId", db_index=False)
    typebot = models.ForeignKey("Typebot", on_delete=models.CASCADE, related_name="collaborators", db_column="typebotId", db_index=False)
    type = models.TextField(choices=CollaborationType.choices)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "CollaboratorsOnTypebots"
        constraints = [
            models.UniqueConstraint(fields=["user", "typebot"], name="CollaboratorsOnTypebots_userId_typebotId_key"),
        ]


class PublicTypebot(models.Model):
    id = models.TextField(primary_key=True, default=None)
    version = models.TextField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", default=timezone.now)
    last_activity_at = UtcDateTimeField(null=True, db_column="lastActivityAt")
    typebot = models.OneToOneField("Typebot", on_delete=models.CASCADE, related_name="published_typebot", db_column="typebotId")
    groups = models.JSONField()
    events = models.JSONField(null=True)
    variables = models.JSONField()
    edges = models.JSONField()
    theme = models.JSONField()
    settings = models.JSONField()

    class Meta:
        db_table = "PublicTypebot"
        indexes = [
            models.Index(fields=["last_activity_at"]),
        ]


class RuntimeMediaIdCache(models.Model):
    pk = models.CompositePrimaryKey("public_typebot_id", "provider", "url")
    provider = models.TextField(choices=ChatProvider.choices)
    url = models.TextField()
    media_id = models.TextField(db_column="mediaId")
    expires_at = UtcDateTimeField(null=True, db_column="expiresAt")
    public_typebot = models.ForeignKey("PublicTypebot", on_delete=models.CASCADE, related_name="media_ids_cache", db_column="publicTypebotId", db_index=False)

    class Meta:
        db_table = "RuntimeMediaIdCache"
        constraints = [
            models.UniqueConstraint(fields=["public_typebot", "provider", "url"], name="RuntimeMediaIdCache_publicTypebotId_provider_url_key"),
        ]
        indexes = [
            models.Index(fields=["expires_at"]),
        ]


class Result(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    typebot = models.ForeignKey("Typebot", on_delete=models.CASCADE, related_name="results", db_column="typebotId", db_index=False)
    variables = models.JSONField()
    is_completed = models.BooleanField(db_column="isCompleted")
    has_started = models.BooleanField(null=True, db_column="hasStarted")
    is_archived = models.BooleanField(null=True, db_column="isArchived", default=False)
    last_chat_session_id = models.TextField(null=True, db_column="lastChatSessionId")

    class Meta:
        db_table = "Result"
        indexes = [
            models.Index(fields=["typebot", "has_started", "-created_at"]),
            models.Index(fields=["typebot", "is_completed"]),
        ]


class SetVariableHistoryItem(models.Model):
    pk = models.CompositePrimaryKey("result_id", "index")
    result = models.ForeignKey("Result", on_delete=models.CASCADE, related_name="set_variable_history", db_column="resultId", db_index=False)
    index = models.IntegerField()
    variable_id = models.TextField(db_column="variableId")
    block_id = models.TextField(db_column="blockId")
    block_index = models.IntegerField(null=True, db_column="blockIndex")
    value = models.JSONField()

    class Meta:
        db_table = "SetVariableHistoryItem"
        constraints = [
            models.UniqueConstraint(fields=["result", "index"], name="SetVariableHistoryItem_resultId_index_key"),
        ]


class VisitedEdge(models.Model):
    pk = models.CompositePrimaryKey("result_id", "index")
    result = models.ForeignKey("Result", on_delete=models.CASCADE, related_name="edges", db_column="resultId", db_index=False)
    edge_id = models.TextField(db_column="edgeId")
    index = models.IntegerField()

    class Meta:
        db_table = "VisitedEdge"
        constraints = [
            models.UniqueConstraint(fields=["result", "index"], name="VisitedEdge_resultId_index_key"),
        ]


class Log(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    result = models.ForeignKey("Result", on_delete=models.CASCADE, related_name="logs", db_column="resultId", db_index=False)
    status = models.TextField()
    context = models.TextField(null=True)
    description = models.TextField()
    details = models.TextField(null=True)

    class Meta:
        db_table = "Log"
        indexes = [
            models.Index(fields=["result"]),
        ]


class Answer(models.Model):
    pk = models.CompositePrimaryKey("result_id", "block_id", "group_id")
    created_at = UtcDateTimeField(db_column="createdAt", auto_now=True)
    result = models.ForeignKey("Result", on_delete=models.CASCADE, related_name="answers", db_column="resultId", db_index=False)
    block_id = models.TextField(db_column="blockId")
    group_id = models.TextField(db_column="groupId")
    variable_id = models.TextField(null=True, db_column="variableId")
    content = models.TextField()
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Answer"
        constraints = [
            models.UniqueConstraint(fields=["result", "block_id", "group_id"], name="Answer_resultId_blockId_groupId_key"),
        ]


class AnswerV2(models.Model):
    id = models.AutoField(primary_key=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    block_id = models.TextField(db_column="blockId")
    content = models.TextField()
    attached_file_urls = models.JSONField(null=True, db_column="attachedFileUrls")
    result = models.ForeignKey("Result", on_delete=models.CASCADE, related_name="answers_v2", db_column="resultId", db_index=False)

    class Meta:
        db_table = "AnswerV2"
        indexes = [
            models.Index(fields=["block_id"]),
        ]


class Coupon(models.Model):
    user_properties_to_update = models.JSONField(db_column="userPropertiesToUpdate")
    code = models.TextField(primary_key=True, default=None)
    date_redeemed = UtcDateTimeField(null=True, db_column="dateRedeemed")

    class Meta:
        db_table = "Coupon"


class Webhook(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    url = models.TextField(null=True)
    method = models.TextField()
    query_params = models.JSONField(db_column="queryParams")
    headers = models.JSONField()
    body = models.TextField(null=True)
    typebot = models.ForeignKey("Typebot", on_delete=models.CASCADE, related_name="webhooks", db_column="typebotId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Webhook"


class ClaimableCustomPlan(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    claimed_at = UtcDateTimeField(null=True, db_column="claimedAt")
    name = models.TextField()
    description = models.TextField(null=True)
    price = models.IntegerField()
    currency = models.TextField()
    workspace = models.OneToOneField("Workspace", on_delete=models.CASCADE, related_name="claimable_custom_plan", db_column="workspaceId")
    chats_limit = models.IntegerField(db_column="chatsLimit")
    storage_limit = models.IntegerField(db_column="storageLimit")
    seats_limit = models.IntegerField(db_column="seatsLimit")
    is_yearly = models.BooleanField(db_column="isYearly", default=False)
    company_name = models.TextField(null=True, db_column="companyName")
    vat_type = models.TextField(null=True, db_column="vatType")
    vat_value = models.TextField(null=True, db_column="vatValue")

    class Meta:
        db_table = "ClaimableCustomPlan"


class ChatSession(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    state = models.JSONField()
    is_replying = models.BooleanField(null=True, db_column="isReplying")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ChatSession"


class ThemeTemplate(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    name = models.TextField()
    theme = models.JSONField()
    workspace = models.ForeignKey("Workspace", on_delete=models.CASCADE, related_name="theme_templates", db_column="workspaceId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ThemeTemplate"


class BannedIp(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    ip = models.TextField(unique=True)
    responsible_typebot = models.ForeignKey("Typebot", on_delete=models.RESTRICT, related_name="banned_ips", db_column="responsibleTypebotId", db_index=False)
    user = models.ForeignKey("User", on_delete=models.RESTRICT, related_name="banned_ips", db_column="userId", db_index=False)

    class Meta:
        db_table = "BannedIp"


class SuppressedEmail(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    email = models.TextField(unique=True)
    transient_general_bounce_count = models.IntegerField(db_column="transientGeneralBounceCount", default=0)
    suppressed_at = UtcDateTimeField(null=True, db_column="suppressedAt")
    last_webhook_id = models.TextField(null=True, db_column="lastWebhookId")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "SuppressedEmail"
