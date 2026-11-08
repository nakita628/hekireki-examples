from datetime import datetime
from datetime import timezone as dt_timezone
from typing import Any, TypeVar

from django.conf import settings
from django.contrib.postgres.fields import ArrayField
from django.db import models
from django.db.backends.base.base import BaseDatabaseWrapper
from django.db.models.expressions import RawSQL
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


class ProjectType(models.TextChoices):
    website = "website"
    app = "app"
    backend = "backend"


class ProjectScope(models.TextChoices):
    all = "all"
    selected = "selected"


class AccessLevel(models.TextChoices):
    read = "read"
    write = "write"
    admin = "admin"


class ClientType(models.TextChoices):
    read = "read"
    write = "write"
    root = "root"


class Interval(models.TextChoices):
    hour = "hour"
    day = "day"
    month = "month"
    minute = "minute"
    week = "week"


class ChartType(models.TextChoices):
    linear = "linear"
    bar = "bar"
    histogram = "histogram"
    pie = "pie"
    metric = "metric"
    area = "area"
    map = "map"
    funnel = "funnel"
    retention = "retention"
    conversion = "conversion"
    sankey = "sankey"
    distribution = "distribution"


class Metric(models.TextChoices):
    sum = "sum"
    average = "average"
    min = "min"
    max = "max"
    count_ = "count"


class IntegrationType(models.TextChoices):
    app = "app"
    mail = "mail"
    custom = "custom"


class ImportStatus(models.TextChoices):
    pending = "pending"
    processing = "processing"
    completed = "completed"
    failed = "failed"


class InsightState(models.TextChoices):
    active = "active"
    suppressed = "suppressed"
    closed = "closed"


class CodeMigration(models.Model):
    id = models.TextField(primary_key=True, db_default=RawSQL("gen_random_uuid()", []))
    name = models.TextField(unique=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "__code_migrations"


class Conversation(models.Model):
    id = models.TextField(primary_key=True, db_default=RawSQL("gen_random_uuid()", []))
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="conversations", db_column="projectId", db_index=False)
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="conversations", db_column="organizationId", db_index=False)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="conversations", db_column="userId", db_index=False)
    title = models.TextField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "conversations"
        indexes = [
            models.Index(fields=["project", "updated_at"]),
            models.Index(fields=["user", "updated_at"]),
        ]


class ChatMessage(models.Model):
    id = models.TextField(primary_key=True, db_default=RawSQL("gen_random_uuid()", []))
    conversation = models.ForeignKey("Conversation", on_delete=models.CASCADE, related_name="messages", db_column="conversationId", db_index=False)
    role = models.TextField()
    parts = models.JSONField()
    metadata = models.JSONField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "chat_messages"
        indexes = [
            models.Index(fields=["conversation", "created_at"]),
        ]


class Organization(models.Model):
    id = models.TextField(primary_key=True, db_default=RawSQL("gen_random_uuid()", []))
    name = models.TextField()
    slug = models.TextField(null=True, unique=True)
    created_by = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="created_organizations", db_column="createdByUserId", null=True, db_index=False)
    timezone_field = models.TextField(null=True, db_column="timezone")
    onboarding = models.TextField(null=True, default="completed")
    attribution = models.JSONField(null=True)
    subscription_id = models.TextField(null=True, db_column="subscriptionId")
    subscription_customer_id = models.TextField(null=True, db_column="subscriptionCustomerId")
    subscription_price_id = models.TextField(null=True, db_column="subscriptionPriceId")
    subscription_product_id = models.TextField(null=True, db_column="subscriptionProductId")
    subscription_status = models.TextField(null=True, db_column="subscriptionStatus")
    subscription_starts_at = UtcDateTimeField(null=True, db_column="subscriptionStartsAt")
    subscription_ends_at = UtcDateTimeField(null=True, db_column="subscriptionEndsAt")
    subscription_canceled_at = UtcDateTimeField(null=True, db_column="subscriptionCanceledAt")
    subscription_created_by = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="subscriptions", db_column="subscriptionCreatedByUserId", null=True, db_index=False)
    subscription_period_events_count = models.IntegerField(db_column="subscriptionPeriodEventsCount", default=0)
    subscription_period_events_count_exceeded_at = UtcDateTimeField(null=True, db_column="subscriptionPeriodEventsCountExceededAt")
    subscription_period_events_limit = models.IntegerField(db_column="subscriptionPeriodEventsLimit", default=0)
    subscription_interval = models.TextField(null=True, db_column="subscriptionInterval")
    subscription_cancel_reason = models.TextField(null=True, db_column="subscriptionCancelReason")
    subscription_cancel_comment = models.TextField(null=True, db_column="subscriptionCancelComment")
    subscription_save_discount_applied_at = UtcDateTimeField(null=True, db_column="subscriptionSaveDiscountAppliedAt")
    subscription_pause_at_period_end = models.BooleanField(db_column="subscriptionPauseAtPeriodEnd", default=False)
    subscription_resumes_at = UtcDateTimeField(null=True, db_column="subscriptionResumesAt")
    subscription_first_started_at = UtcDateTimeField(null=True, db_column="subscriptionFirstStartedAt")
    usage_warning_sent_at = UtcDateTimeField(null=True, db_column="usageWarningSentAt")
    usage_exceeded_sent_at = UtcDateTimeField(null=True, db_column="usageExceededSentAt")
    subscription_discount = models.JSONField(null=True, db_column="subscriptionDiscount")
    wind_down_started_at = UtcDateTimeField(null=True, db_column="windDownStartedAt")
    wind_down_step = models.TextField(null=True, db_column="windDownStep")
    delete_at = UtcDateTimeField(null=True, db_column="deleteAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "organizations"
        indexes = [
            models.Index(fields=["wind_down_started_at"]),
        ]


class User(models.Model):
    id = models.TextField(primary_key=True, db_default=RawSQL("gen_random_uuid()", []))
    email = models.TextField(unique=True)
    first_name = models.TextField(null=True, db_column="firstName")
    last_name = models.TextField(null=True, db_column="lastName")
    name = models.TextField(null=True)
    email_verified = models.BooleanField(db_column="emailVerified", default=False)
    image = models.TextField(null=True)
    two_factor_enabled = models.BooleanField(db_column="twoFactorEnabled", default=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    deleted_at = UtcDateTimeField(null=True, db_column="deletedAt")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "users"


class UserTotp(models.Model):
    id = models.TextField(primary_key=True, db_default=RawSQL("gen_random_uuid()", []))
    user = models.OneToOneField("User", on_delete=models.CASCADE, related_name="totp", db_column="userId")
    secret = models.TextField()
    recovery_codes = ArrayField(models.TextField(), db_column="recoveryCodes")
    enabled_at = UtcDateTimeField(null=True, db_column="enabledAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "user_totp"


class TwoFactor(models.Model):
    id = models.TextField(primary_key=True, db_default=RawSQL("gen_random_uuid()", []))
    user = models.OneToOneField("User", on_delete=models.CASCADE, related_name="two_factor", db_column="userId")
    secret = models.TextField()
    backup_codes = models.TextField(db_column="backupCodes")
    verified = models.BooleanField(default=True)
    failed_verification_count = models.IntegerField(db_column="failedVerificationCount", default=0)
    locked_until = UtcDateTimeField(null=True, db_column="lockedUntil")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "two_factor"


class TwoFactorChallenge(models.Model):
    id = models.TextField(primary_key=True, default=None)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="two_factor_challenges", db_column="userId", db_index=False)
    expires_at = UtcDateTimeField(db_column="expiresAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "two_factor_challenges"
        indexes = [
            models.Index(fields=["user"]),
        ]


class Account(models.Model):
    id = models.TextField(primary_key=True, db_default=RawSQL("gen_random_uuid()", []))
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="accounts", db_column="userId", db_index=False)
    email = models.TextField(null=True)
    provider = models.TextField()
    provider_id = models.TextField(null=True, db_column="providerId")
    account_id = models.TextField(null=True, db_column="accountId")
    id_token = models.TextField(null=True, db_column="idToken")
    access_token = models.TextField(null=True, db_column="accessToken")
    refresh_token = models.TextField(null=True, db_column="refreshToken")
    access_token_expires_at = UtcDateTimeField(null=True, db_column="accessTokenExpiresAt")
    refresh_token_expires_at = UtcDateTimeField(null=True, db_column="refreshTokenExpiresAt")
    scope = models.TextField(null=True)
    password = models.TextField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "accounts"


class Session(models.Model):
    id = models.TextField(primary_key=True, default=None)
    token = models.TextField(null=True, unique=True)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="sessions", db_column="userId", db_index=False)
    expires_at = UtcDateTimeField(db_column="expiresAt")
    ip_address = models.TextField(null=True, db_column="ipAddress")
    user_agent = models.TextField(null=True, db_column="userAgent")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "sessions"


class Verification(models.Model):
    id = models.TextField(primary_key=True, default=None)
    identifier = models.TextField()
    value = models.TextField()
    expires_at = UtcDateTimeField(db_column="expiresAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "verifications"
        indexes = [
            models.Index(fields=["identifier"]),
        ]


class Member(models.Model):
    id = models.UUIDField(primary_key=True, db_default=RawSQL("gen_random_uuid()", []))
    role = models.TextField()
    email = models.TextField()
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="membership", db_column="userId", null=True, db_index=False)
    invited_by = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="sent_invites", db_column="invitedById", null=True, db_index=False)
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="members", db_column="organizationId", db_index=False)
    project_scope = models.TextField(choices=ProjectScope.choices, db_column="projectScope", default=ProjectScope.all)
    meta = models.JSONField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "members"
        constraints = [
            models.UniqueConstraint(fields=["organization", "user"], name="members_organizationId_userId_key"),
        ]


class MemberProject(models.Model):
    pk = models.CompositePrimaryKey("member_id", "project_id")
    member = models.ForeignKey("Member", on_delete=models.CASCADE, related_name="projects", db_column="memberId", db_index=False)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="member_projects", db_column="projectId", db_index=False)
    level = models.TextField(choices=AccessLevel.choices, null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "member_projects"
        indexes = [
            models.Index(fields=["project"]),
        ]


class Invite(models.Model):
    id = models.TextField(primary_key=True, default=None)
    email = models.TextField()
    created_by = models.ForeignKey("User", on_delete=models.CASCADE, related_name="invites", db_column="createdById", db_index=False)
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="invites", db_column="organizationId", db_index=False)
    project_access = models.JSONField(db_column="projectAccess", default=list)
    project_scope = models.TextField(choices=ProjectScope.choices, db_column="projectScope", default=ProjectScope.all)
    expires_at = UtcDateTimeField(db_column="expiresAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    role = models.TextField()
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "invites"


class Project(models.Model):
    id = models.TextField(primary_key=True, db_default=RawSQL("gen_random_uuid()", []))
    name = models.TextField()
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="projects", db_column="organizationId", db_index=False)
    events_count = models.IntegerField(db_column="eventsCount", default=0)
    types = ArrayField(models.TextField(choices=ProjectType.choices), default=list)
    domain = models.TextField(null=True)
    cors = ArrayField(models.TextField(), default=list)
    cross_domain = models.BooleanField(db_column="crossDomain", default=False)
    allow_unsafe_revenue_tracking = models.BooleanField(db_column="allowUnsafeRevenueTracking", default=False)
    ai_store_content = models.BooleanField(db_column="aiStoreContent", default=True)
    filters = models.JSONField(default=list)
    first_event_at = UtcDateTimeField(null=True, db_column="firstEventAt")
    no_data_notified_at = UtcDateTimeField(null=True, db_column="noDataNotifiedAt")
    data_stopped_notified_at = UtcDateTimeField(null=True, db_column="dataStoppedNotifiedAt")
    delete_at = UtcDateTimeField(null=True, db_column="deleteAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "projects"


class Cohort(models.Model):
    id = models.UUIDField(primary_key=True, db_default=RawSQL("gen_random_uuid()", []))
    name = models.TextField()
    description = models.TextField(null=True)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="cohorts", db_column="projectId", db_index=False)
    definition = models.JSONField(default=dict)
    is_static = models.BooleanField(db_column="isStatic", default=False)
    profile_count = models.IntegerField(db_column="profileCount", default=0)
    last_computed_at = UtcDateTimeField(null=True, db_column="lastComputedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "cohorts"
        indexes = [
            models.Index(fields=["project"]),
        ]


class Salt(models.Model):
    salt = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "salts"


class Client(models.Model):
    id = models.UUIDField(primary_key=True, db_default=RawSQL("gen_random_uuid()", []))
    name = models.TextField()
    secret = models.TextField(null=True)
    type = models.TextField(choices=ClientType.choices, default=ClientType.write)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="clients", db_column="projectId", null=True, db_index=False)
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="client", db_column="organizationId", db_index=False)
    ignore_cors_and_secret = models.BooleanField(db_column="ignoreCorsAndSecret", default=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "clients"


class Dashboard(models.Model):
    id = models.TextField(primary_key=True, db_default=RawSQL("gen_random_uuid()", []))
    name = models.TextField()
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="dashboard", db_column="organizationId", db_index=False)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="dashboards", db_column="projectId", db_index=False)
    filters = models.JSONField(default=list)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "dashboards"


class Report(models.Model):
    id = models.UUIDField(primary_key=True, db_default=RawSQL("gen_random_uuid()", []))
    name = models.TextField()
    interval = models.TextField(choices=Interval.choices)
    range = models.TextField(default="30d")
    chart_type = models.TextField(choices=ChartType.choices, db_column="chartType")
    line_type = models.TextField(db_column="lineType", default="monotone")
    breakdowns = models.JSONField()
    events = models.JSONField()
    global_filters = models.JSONField(db_column="globalFilters", default=list)
    formula = models.TextField(null=True)
    unit = models.TextField(null=True)
    metric = models.TextField(choices=Metric.choices, default=Metric.sum)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="reports", db_column="projectId", db_index=False)
    previous = models.BooleanField(default=False)
    criteria = models.TextField(null=True)
    funnel_group = models.TextField(null=True, db_column="funnelGroup")
    funnel_window = models.FloatField(null=True, db_column="funnelWindow")
    options = models.JSONField(null=True)
    display = models.JSONField(null=True)
    visible_series = ArrayField(models.TextField(), db_column="visibleSeries")
    start_date = models.TextField(null=True, db_column="startDate")
    end_date = models.TextField(null=True, db_column="endDate")
    dashboard = models.ForeignKey("Dashboard", on_delete=models.CASCADE, related_name="reports", db_column="dashboardId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "reports"


class ReportLayout(models.Model):
    id = models.UUIDField(primary_key=True, db_default=RawSQL("gen_random_uuid()", []))
    report = models.OneToOneField("Report", on_delete=models.CASCADE, related_name="layout", db_column="reportId")
    grid_columns = models.IntegerField(db_column="gridColumns", default=12)
    x = models.IntegerField(default=0)
    y = models.IntegerField(default=0)
    w = models.IntegerField(default=8)
    h = models.IntegerField(default=6)
    min_w = models.IntegerField(null=True, db_column="minW", default=4)
    min_h = models.IntegerField(null=True, db_column="minH", default=4)
    max_w = models.IntegerField(null=True, db_column="maxW")
    max_h = models.IntegerField(null=True, db_column="maxH")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "report_layouts"


class ShareOverview(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project = models.OneToOneField("Project", on_delete=models.CASCADE, related_name="share", db_column="projectId")
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="share_overview", db_column="organizationId", db_index=False)
    public = models.BooleanField(default=False)
    password = models.TextField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "shares"


class ShareDashboard(models.Model):
    id = models.TextField(primary_key=True, default=None)
    dashboard = models.OneToOneField("Dashboard", on_delete=models.CASCADE, related_name="share", db_column="dashboardId")
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="share_dashboard", db_column="organizationId", db_index=False)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="share_dashboards", db_column="projectId", db_index=False)
    public = models.BooleanField(default=False)
    password = models.TextField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "share_dashboards"


class ShareReport(models.Model):
    id = models.TextField(primary_key=True, default=None)
    report = models.OneToOneField("Report", on_delete=models.CASCADE, related_name="share", db_column="reportId")
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="share_report", db_column="organizationId", db_index=False)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="share_reports", db_column="projectId", db_index=False)
    public = models.BooleanField(default=False)
    password = models.TextField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "share_reports"


class ShareWidget(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="share_widgets", db_column="projectId", db_index=False)
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="share_widget", db_column="organizationId", db_index=False)
    public = models.BooleanField(default=True)
    options = models.JSONField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "share_widgets"


class EventMeta(models.Model):
    id = models.UUIDField(primary_key=True, db_default=RawSQL("gen_random_uuid()", []))
    name = models.TextField()
    conversion = models.BooleanField(null=True)
    color = models.TextField(null=True)
    icon = models.TextField(null=True)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="meta", db_column="projectId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "event_meta"
        constraints = [
            models.UniqueConstraint(fields=["name", "project"], name="event_meta_name_projectId_key"),
        ]


class CustomEvent(models.Model):
    id = models.UUIDField(primary_key=True, db_default=RawSQL("gen_random_uuid()", []))
    name = models.TextField()
    items = models.JSONField()
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="custom_events", db_column="projectId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "custom_events"
        constraints = [
            models.UniqueConstraint(fields=["project", "name"], name="custom_events_projectId_name_key"),
        ]


class Reference(models.Model):
    id = models.UUIDField(primary_key=True, db_default=RawSQL("gen_random_uuid()", []))
    title = models.TextField()
    description = models.TextField(null=True)
    date = UtcDateTimeField(default=timezone.now)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="references", db_column="projectId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "references"


class NotificationRule(models.Model):
    id = models.UUIDField(primary_key=True, db_default=RawSQL("gen_random_uuid()", []))
    name = models.TextField()
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="notification_rules", db_column="projectId", db_index=False)
    send_to_app = models.BooleanField(db_column="sendToApp", default=False)
    send_to_email = models.BooleanField(db_column="sendToEmail", default=False)
    config = models.JSONField()
    template = models.TextField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "notification_rules"


class Notification(models.Model):
    id = models.UUIDField(primary_key=True, db_default=RawSQL("gen_random_uuid()", []))
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="notifications", db_column="projectId", db_index=False)
    title = models.TextField()
    message = models.TextField()
    is_read_at = UtcDateTimeField(null=True, db_column="isReadAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    send_to_app = models.BooleanField(db_column="sendToApp", default=False)
    send_to_email = models.BooleanField(db_column="sendToEmail", default=False)
    integration = models.ForeignKey("Integration", on_delete=models.CASCADE, related_name="notifications", db_column="integrationId", null=True, db_index=False)
    notification_rule = models.ForeignKey("NotificationRule", on_delete=models.CASCADE, related_name="notifications", db_column="notificationRuleId", null=True, db_index=False)
    payload = models.JSONField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "notifications"


class Integration(models.Model):
    id = models.UUIDField(primary_key=True, db_default=RawSQL("gen_random_uuid()", []))
    name = models.TextField()
    config = models.JSONField()
    organization = models.ForeignKey("Organization", on_delete=models.CASCADE, related_name="integrations", db_column="organizationId", db_index=False)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="integrations", db_column="projectId", null=True, db_index=False)
    notification_rules: "models.ManyToManyField[NotificationRule, IntegrationToNotificationRule]" = models.ManyToManyField("NotificationRule", through="IntegrationToNotificationRule", related_name="integrations")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "integrations"
        indexes = [
            models.Index(fields=["organization"]),
            models.Index(fields=["project"]),
        ]


class ExportWatermark(models.Model):
    id = models.UUIDField(primary_key=True, db_default=RawSQL("gen_random_uuid()", []))
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="export_watermarks", db_column="projectId", db_index=False)
    integration = models.ForeignKey("Integration", on_delete=models.CASCADE, related_name="export_watermarks", db_column="integrationId", db_index=False)
    last_inserted_at = UtcDateTimeField(db_column="lastInsertedAt")
    last_event_id = models.TextField(db_column="lastEventId", default="")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "export_watermarks"
        constraints = [
            models.UniqueConstraint(fields=["project", "integration"], name="export_watermarks_projectId_integrationId_key"),
        ]
        indexes = [
            models.Index(fields=["integration"]),
        ]


class ResetPassword(models.Model):
    id = models.TextField(primary_key=True, default=None)
    account = models.ForeignKey("Account", on_delete=models.CASCADE, related_name="reset_passwords", db_column="accountId", db_index=False)
    expires_at = UtcDateTimeField(db_column="expiresAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "reset_password"


class Import(models.Model):
    id = models.UUIDField(primary_key=True, db_default=RawSQL("gen_random_uuid()", []))
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="imports", db_column="projectId", db_index=False)
    job_id = models.TextField(null=True, db_column="jobId")
    status = models.TextField(choices=ImportStatus.choices)
    status_message = models.TextField(null=True, db_column="statusMessage")
    error_message = models.TextField(null=True, db_column="errorMessage")
    config = models.JSONField()
    total_events = models.IntegerField(db_column="totalEvents", default=0)
    processed_events = models.IntegerField(db_column="processedEvents", default=0)
    current_step = models.TextField(null=True, db_column="currentStep")
    current_batch = models.TextField(null=True, db_column="currentBatch")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    completed_at = UtcDateTimeField(null=True, db_column="completedAt")
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "imports"


class ProjectInsight(models.Model):
    id = models.UUIDField(primary_key=True, db_default=RawSQL("gen_random_uuid()", []))
    project_id = models.TextField(db_column="projectId")
    module_key = models.TextField(db_column="moduleKey")
    dimension_key = models.TextField(db_column="dimensionKey")
    window_kind = models.TextField(db_column="windowKind")
    state = models.TextField(choices=InsightState.choices, default=InsightState.active)
    title = models.TextField()
    summary = models.TextField(null=True)
    display_name = models.TextField(db_column="displayName", default="")
    payload = models.JSONField(default=dict)
    direction = models.TextField(null=True)
    impact_score = models.FloatField(db_column="impactScore", default=0)
    severity_band = models.TextField(null=True, db_column="severityBand")
    relevance_score = models.FloatField(null=True, db_column="relevanceScore")
    ai_summary = models.TextField(null=True, db_column="aiSummary")
    ai_category = models.TextField(null=True, db_column="aiCategory")
    email_worthy = models.BooleanField(db_column="emailWorthy", default=False)
    reference_worthy = models.BooleanField(db_column="referenceWorthy", default=False)
    enriched_at = UtcDateTimeField(null=True, db_column="enrichedAt")
    enrich_version = models.IntegerField(null=True, db_column="enrichVersion")
    version = models.IntegerField(default=1)
    thread_id = models.UUIDField(db_column="threadId", db_default=RawSQL("gen_random_uuid()", []))
    window_start = UtcDateTimeField(null=True, db_column="windowStart")
    window_end = UtcDateTimeField(null=True, db_column="windowEnd")
    first_detected_at = UtcDateTimeField(db_column="firstDetectedAt", default=timezone.now)
    last_updated_at = UtcDateTimeField(db_column="lastUpdatedAt", auto_now=True)
    last_seen_at = UtcDateTimeField(db_column="lastSeenAt", default=timezone.now)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "project_insights"
        constraints = [
            models.UniqueConstraint(fields=["project_id", "module_key", "dimension_key", "window_kind", "state"], name="project_insights_projectId_moduleKey_dimensionKey_windowKind_state_key"),
        ]
        indexes = [
            models.Index(fields=["project_id", "-impact_score"]),
            models.Index(fields=["project_id", "module_key", "window_kind", "state"]),
            models.Index(fields=["project_id", "state", "-relevance_score"]),
        ]


class InsightEvent(models.Model):
    id = models.UUIDField(primary_key=True, db_default=RawSQL("gen_random_uuid()", []))
    insight = models.ForeignKey("ProjectInsight", on_delete=models.CASCADE, related_name="events", db_column="insightId", db_index=False)
    event_kind = models.TextField(db_column="eventKind")
    change_from = models.JSONField(null=True, db_column="changeFrom")
    change_to = models.JSONField(null=True, db_column="changeTo")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "insight_events"
        indexes = [
            models.Index(fields=["insight", "created_at"]),
        ]


class GscConnection(models.Model):
    id = models.UUIDField(primary_key=True, db_default=RawSQL("gen_random_uuid()", []))
    project = models.OneToOneField("Project", on_delete=models.CASCADE, related_name="gsc_connection", db_column="projectId")
    site_url = models.TextField(db_column="siteUrl", default="")
    access_token = models.TextField(db_column="accessToken")
    refresh_token = models.TextField(db_column="refreshToken")
    access_token_expires_at = UtcDateTimeField(null=True, db_column="accessTokenExpiresAt")
    last_synced_at = UtcDateTimeField(null=True, db_column="lastSyncedAt")
    last_sync_status = models.TextField(null=True, db_column="lastSyncStatus")
    last_sync_error = models.TextField(null=True, db_column="lastSyncError")
    backfill_status = models.TextField(null=True, db_column="backfillStatus")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "gsc_connections"


class EmailUnsubscribe(models.Model):
    id = models.UUIDField(primary_key=True, db_default=RawSQL("gen_random_uuid()", []))
    email = models.TextField()
    category = models.TextField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "email_unsubscribes"
        constraints = [
            models.UniqueConstraint(fields=["email", "category"], name="email_unsubscribes_email_category_key"),
        ]


class IntegrationToNotificationRule(models.Model):
    pk = models.CompositePrimaryKey("a_id", "b_id")
    a = models.ForeignKey("Integration", on_delete=models.CASCADE, related_name="+", db_column="A", db_index=False)
    b = models.ForeignKey("NotificationRule", on_delete=models.CASCADE, related_name="+", db_column="B", db_index=False)

    class Meta:
        db_table = "_IntegrationToNotificationRule"
        indexes = [
            models.Index(fields=["b"]),
        ]
