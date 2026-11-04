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


class FolderType(models.TextChoices):
    default = "default"
    mega = "mega"


class FolderAccessLevel(models.TextChoices):
    read = "read"
    write = "write"


class FolderUserRole(models.TextChoices):
    owner = "owner"
    editor = "editor"
    viewer = "viewer"


class InvoiceStatus(models.TextChoices):
    processing = "processing"
    completed = "completed"
    failed = "failed"


class InvoiceType(models.TextChoices):
    domainRenewal = "domainRenewal"


class PaymentMethod(models.TextChoices):
    card = "card"
    ach = "ach"
    ach_fast = "ach_fast"
    sepa = "sepa"
    acss = "acss"


class WebhookReceiver(models.TextChoices):
    user = "user"
    zapier = "zapier"
    make = "make"
    slack = "slack"
    segment = "segment"


class LinkScope(models.TextChoices):
    workspace = "workspace"
    folders = "folders"
    links = "links"


class PlanPeriod(models.TextChoices):
    monthly = "monthly"
    yearly = "yearly"
    quarterly = "quarterly"
    biweekly = "biweekly"


class StripeConnectMode(models.TextChoices):
    live = "live"
    test = "test"
    sandbox = "sandbox"


class WorkspaceRole(models.TextChoices):
    owner = "owner"
    member = "member"
    viewer = "viewer"
    billing = "billing"


class Customer(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField(null=True)
    email = models.TextField(null=True)
    normalized_email = models.TextField(null=True, db_column="normalizedEmail")
    avatar = models.TextField(null=True)
    external_id = models.TextField(null=True, db_column="externalId")
    stripe_customer_id = models.TextField(null=True, unique=True, db_column="stripeCustomerId")
    link = models.ForeignKey("Link", on_delete=models.SET_NULL, related_name="customers", db_column="linkId", null=True, db_index=False)
    click_id = models.TextField(null=True, db_column="clickId")
    clicked_at = UtcDateTimeField(null=True, db_column="clickedAt")
    country = models.TextField(null=True)
    sales = models.IntegerField(default=0)
    sale_amount = models.BigIntegerField(db_column="saleAmount", default=0)
    first_sale_at = UtcDateTimeField(null=True, db_column="firstSaleAt")
    subscription_canceled_at = UtcDateTimeField(null=True, db_column="subscriptionCanceledAt")
    project = models.ForeignKey("Project", on_delete=models.RESTRICT, related_name="customers", db_column="projectId", db_index=False)
    project_connect_id = models.TextField(null=True, db_column="projectConnectId")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Customer"
        constraints = [
            models.UniqueConstraint(fields=["project", "external_id"], name="Customer_projectId_externalId_key"),
        ]
        indexes = [
            models.Index(fields=["project", "email"]),
            models.Index(fields=["project", "normalized_email"]),
            models.Index(fields=["project", "created_at"]),
            models.Index(fields=["project", "sale_amount"]),
            models.Index(fields=["project", "first_sale_at"]),
            models.Index(fields=["project", "subscription_canceled_at"]),
            models.Index(fields=["link"]),
            models.Index(fields=["country"]),
            models.Index(fields=["email", "name"]),
        ]


class Dashboard(models.Model):
    id = models.TextField(primary_key=True, default=None)
    link = models.OneToOneField("Link", on_delete=models.CASCADE, related_name="dashboard", db_column="linkId", null=True)
    folder = models.OneToOneField("Folder", on_delete=models.CASCADE, related_name="dashboard", db_column="folderId", null=True)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="dashboards", db_column="projectId", null=True, db_index=False)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="dashboards", db_column="userId", null=True, db_index=False)
    do_index = models.BooleanField(db_column="doIndex", default=False)
    password = models.TextField(null=True)
    show_conversions = models.BooleanField(db_column="showConversions", default=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Dashboard"
        indexes = [
            models.Index(fields=["project"]),
            models.Index(fields=["user"]),
        ]


class Domain(models.Model):
    id = models.TextField(primary_key=True, default=None)
    slug = models.TextField(unique=True)
    verified = models.BooleanField(default=False)
    placeholder = models.TextField(null=True)
    expired_url = models.TextField(null=True, db_column="expiredUrl")
    not_found_url = models.TextField(null=True, db_column="notFoundUrl")
    primary = models.BooleanField(default=False)
    archived = models.BooleanField(default=False)
    last_checked = UtcDateTimeField(db_column="lastChecked", default=timezone.now)
    logo = models.TextField(null=True)
    apple_app_site_association = models.JSONField(null=True, db_column="appleAppSiteAssociation")
    asset_links = models.JSONField(null=True, db_column="assetLinks")
    deepview_data = models.JSONField(null=True, db_column="deepviewData", default=dict)
    link_retention_days = models.IntegerField(null=True, db_column="linkRetentionDays")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="domains", db_column="projectId", null=True, db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Domain"
        indexes = [
            models.Index(fields=["project"]),
            models.Index(fields=["last_checked"]),
        ]


class RegisteredDomain(models.Model):
    id = models.TextField(primary_key=True, default=None)
    slug = models.TextField()
    project = models.ForeignKey("Project", on_delete=models.RESTRICT, related_name="registered_domains", db_column="projectId", db_index=False)
    domain = models.OneToOneField("Domain", on_delete=models.SET_NULL, related_name="registered_domain", db_column="domainId", null=True)
    auto_renewal_disabled_at = UtcDateTimeField(null=True, db_column="autoRenewalDisabledAt")
    renewal_fee = models.IntegerField(db_column="renewalFee", default=1200)
    expires_at = UtcDateTimeField(db_column="expiresAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "RegisteredDomain"
        indexes = [
            models.Index(fields=["project"]),
            models.Index(fields=["expires_at"]),
        ]


class DefaultDomains(models.Model):
    id = models.TextField(primary_key=True, default=None)
    dubsh = models.BooleanField(default=True)
    chatgpt = models.BooleanField(default=True)
    sptifi = models.BooleanField(default=True)
    gitnew = models.BooleanField(default=True)
    callink = models.BooleanField(default=True)
    amznid = models.BooleanField(default=True)
    ggllink = models.BooleanField(default=True)
    figpage = models.BooleanField(default=True)
    loooooooong = models.BooleanField(default=False)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="default_domains", db_column="projectId", db_index=False)

    class Meta:
        db_table = "DefaultDomains"


class Folder(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    description = models.TextField(null=True)
    project = models.ForeignKey("Project", on_delete=models.RESTRICT, related_name="folders", db_column="projectId", db_index=False)
    type = models.TextField(choices=FolderType.choices, default=FolderType.default)
    access_level = models.TextField(choices=FolderAccessLevel.choices, null=True, db_column="accessLevel")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Folder"
        constraints = [
            models.UniqueConstraint(fields=["name", "project"], name="Folder_name_projectId_key"),
        ]
        indexes = [
            models.Index(fields=["project"]),
        ]


class FolderUser(models.Model):
    id = models.TextField(primary_key=True, default=None)
    folder = models.ForeignKey("Folder", on_delete=models.CASCADE, related_name="users", db_column="folderId", db_index=False)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="folders", db_column="userId", db_index=False)
    role = models.TextField(choices=FolderUserRole.choices, null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "FolderUser"
        constraints = [
            models.UniqueConstraint(fields=["folder", "user"], name="FolderUser_folderId_userId_key"),
        ]
        indexes = [
            models.Index(fields=["user"]),
        ]


class FolderAccessRequest(models.Model):
    id = models.TextField(primary_key=True, default=None)
    folder = models.ForeignKey("Folder", on_delete=models.CASCADE, related_name="access_requests", db_column="folderId", db_index=False)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="folder_access_requests", db_column="userId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "FolderAccessRequest"
        constraints = [
            models.UniqueConstraint(fields=["folder", "user"], name="FolderAccessRequest_folderId_userId_key"),
        ]
        indexes = [
            models.Index(fields=["user"]),
        ]


class Integration(models.Model):
    id = models.TextField(primary_key=True, default=None)
    user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="integrations", db_column="userId", null=True, db_index=False)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="integrations", db_column="projectId", db_index=False)
    name = models.TextField()
    slug = models.TextField(unique=True)
    description = models.TextField(null=True)
    readme = models.TextField(null=True)
    developer = models.TextField()
    website = models.TextField()
    logo = models.TextField(null=True)
    screenshots = models.JSONField(null=True)
    verified = models.BooleanField(default=False)
    install_url = models.TextField(null=True, db_column="installUrl")
    guide_url = models.TextField(null=True, db_column="guideUrl")
    category = models.TextField(null=True)
    coming_soon = models.BooleanField(db_column="comingSoon", default=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Integration"
        indexes = [
            models.Index(fields=["project"]),
            models.Index(fields=["user"]),
        ]


class InstalledIntegration(models.Model):
    id = models.TextField(primary_key=True, default=None)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="installed_integrations", db_column="userId", db_index=False)
    integration = models.ForeignKey("Integration", on_delete=models.CASCADE, related_name="installations", db_column="integrationId", db_index=False)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="installed_integrations", db_column="projectId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    credentials = models.JSONField(null=True)
    settings = models.JSONField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "InstalledIntegration"
        constraints = [
            models.UniqueConstraint(fields=["user", "integration", "project"], name="InstalledIntegration_userId_integrationId_projectId_key"),
        ]
        indexes = [
            models.Index(fields=["project"]),
            models.Index(fields=["integration"]),
        ]


class Invoice(models.Model):
    id = models.TextField(primary_key=True, default=None)
    workspace = models.ForeignKey("Project", on_delete=models.RESTRICT, related_name="invoices", db_column="workspaceId", db_index=False)
    number = models.TextField(null=True, unique=True)
    status = models.TextField(choices=InvoiceStatus.choices, default=InvoiceStatus.processing)
    type = models.TextField(choices=InvoiceType.choices, default=InvoiceType.domainRenewal)
    payment_method = models.TextField(choices=PaymentMethod.choices, null=True, db_column="paymentMethod")
    amount = models.IntegerField(default=0)
    fee = models.IntegerField(default=0)
    refunded_amount = models.IntegerField(db_column="refundedAmount", default=0)
    total = models.IntegerField(default=0)
    receipt_url = models.TextField(null=True, db_column="receiptUrl")
    refund_reason = models.TextField(null=True, db_column="refundReason")
    failed_reason = models.TextField(null=True, db_column="failedReason")
    registered_domains = models.JSONField(null=True, db_column="registeredDomains")
    stripe_charge_metadata = models.JSONField(null=True, db_column="stripeChargeMetadata")
    failed_attempts = models.IntegerField(db_column="failedAttempts", default=0)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    paid_at = UtcDateTimeField(null=True, db_column="paidAt")

    class Meta:
        db_table = "Invoice"
        indexes = [
            models.Index(fields=["workspace"]),
        ]


class JacksonIndex(models.Model):
    id = models.AutoField(primary_key=True)
    key = models.CharField(max_length=250)
    store_key = models.CharField(max_length=250, db_column="storeKey")

    class Meta:
        db_table = "jackson_index"
        indexes = [
            models.Index(fields=["key", "store_key"]),
        ]


class JacksonStore(models.Model):
    key = models.CharField(max_length=250, primary_key=True, default=None)
    value = models.TextField()
    iv = models.CharField(max_length=64, null=True)
    tag = models.CharField(max_length=64, null=True)
    namespace = models.CharField(max_length=64, null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    modified_at = UtcDateTimeField(null=True, db_column="modifiedAt")

    class Meta:
        db_table = "jackson_store"
        indexes = [
            models.Index(fields=["namespace"]),
        ]


class JacksonTtl(models.Model):
    key = models.CharField(max_length=250, primary_key=True, default=None)
    expires_at = models.BigIntegerField(db_column="expiresAt")

    class Meta:
        db_table = "jackson_ttl"
        indexes = [
            models.Index(fields=["expires_at"]),
        ]


class Job(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    payload = models.JSONField()
    options = models.JSONField(null=True)
    scheduled_at = UtcDateTimeField(db_column="scheduledAt", default=timezone.now)
    attempts = models.IntegerField(default=0)
    last_error = models.TextField(null=True, db_column="lastError")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Job"
        indexes = [
            models.Index(fields=["scheduled_at", "attempts"]),
        ]


class Link(models.Model):
    id = models.TextField(primary_key=True, default=None)
    short_domain = models.ForeignKey("Domain", on_delete=models.RESTRICT, related_name="links", to_field="slug", db_column="domain", db_index=False)
    key = models.TextField()
    url = models.TextField()
    short_link = models.CharField(max_length=400, unique=True, db_column="shortLink")
    archived = models.BooleanField(default=False)
    expires_at = UtcDateTimeField(null=True, db_column="expiresAt")
    expired_url = models.TextField(null=True, db_column="expiredUrl")
    disabled_at = UtcDateTimeField(null=True, db_column="disabledAt")
    password = models.TextField(null=True)
    track_conversion = models.BooleanField(db_column="trackConversion", default=False)
    proxy = models.BooleanField(default=False)
    title = models.TextField(null=True)
    description = models.CharField(max_length=280, null=True)
    image = models.TextField(null=True)
    video = models.TextField(null=True)
    utm_source = models.CharField(max_length=255, null=True)
    utm_medium = models.CharField(max_length=255, null=True)
    utm_campaign = models.CharField(max_length=255, null=True)
    utm_term = models.CharField(max_length=255, null=True)
    utm_content = models.CharField(max_length=255, null=True)
    rewrite = models.BooleanField(default=False)
    link_retention_cleanup_disabled_at = UtcDateTimeField(null=True, db_column="linkRetentionCleanupDisabledAt")
    do_index = models.BooleanField(db_column="doIndex", default=False)
    ios = models.TextField(null=True)
    android = models.TextField(null=True)
    geo = models.JSONField(null=True)
    test_variants = models.JSONField(null=True, db_column="testVariants")
    test_started_at = UtcDateTimeField(null=True, db_column="testStartedAt")
    test_completed_at = UtcDateTimeField(null=True, db_column="testCompletedAt")
    user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="links", db_column="userId", null=True, db_index=False)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="links", db_column="projectId", null=True, db_index=False)
    folder = models.ForeignKey("Folder", on_delete=models.SET_NULL, related_name="links", db_column="folderId", null=True, db_index=False)
    external_id = models.TextField(null=True, db_column="externalId")
    tenant_id = models.TextField(null=True, db_column="tenantId")
    public_stats = models.BooleanField(db_column="publicStats", default=False)
    clicks = models.IntegerField(default=0)
    leads = models.IntegerField(default=0)
    conversions = models.IntegerField(default=0)
    sales = models.IntegerField(default=0)
    sale_amount = models.BigIntegerField(db_column="saleAmount", default=0)
    last_clicked = UtcDateTimeField(null=True, db_column="lastClicked")
    last_lead_at = UtcDateTimeField(null=True, db_column="lastLeadAt")
    last_conversion_at = UtcDateTimeField(null=True, db_column="lastConversionAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    comments = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Link"
        constraints = [
            models.UniqueConstraint(fields=["short_domain", "key"], name="Link_domain_key_key"),
            models.UniqueConstraint(fields=["project", "external_id"], name="Link_projectId_externalId_key"),
        ]
        indexes = [
            models.Index(fields=["project", "tenant_id"]),
            models.Index(fields=["project", "url"]),
            models.Index(fields=["project", "folder", "archived", "-created_at"]),
            models.Index(fields=["short_domain", "created_at"]),
            models.Index(fields=["folder"]),
            models.Index(fields=["user"]),
        ]


class YearInReview(models.Model):
    id = models.TextField(primary_key=True, default=None)
    year = models.IntegerField()
    total_links = models.IntegerField(db_column="totalLinks")
    total_clicks = models.IntegerField(db_column="totalClicks")
    top_links = models.JSONField(db_column="topLinks")
    top_countries = models.JSONField(db_column="topCountries")
    workspace = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="year_in_reviews", db_column="workspaceId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    sent_at = UtcDateTimeField(null=True, db_column="sentAt")

    class Meta:
        db_table = "YearInReview"
        indexes = [
            models.Index(fields=["workspace"]),
        ]


class UserNotificationPreferences(models.Model):
    id = models.TextField(primary_key=True, default=None)
    user = models.OneToOneField("User", on_delete=models.CASCADE, related_name="notification_preferences", db_column="userId")
    dub_links = models.BooleanField(db_column="dubLinks", default=True)

    class Meta:
        db_table = "UserNotificationPreferences"


class NotificationPreference(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project_user = models.OneToOneField("ProjectUsers", on_delete=models.CASCADE, related_name="notification_preference", db_column="projectUserId")
    link_usage_summary = models.BooleanField(db_column="linkUsageSummary", default=True)
    domain_configuration_updates = models.BooleanField(db_column="domainConfigurationUpdates", default=True)

    class Meta:
        db_table = "NotificationPreference"


class OAuthApp(models.Model):
    id = models.TextField(primary_key=True, default=None)
    integration = models.OneToOneField("Integration", on_delete=models.CASCADE, related_name="o_auth_app", db_column="integrationId")
    client_id = models.TextField(unique=True, db_column="clientId")
    hashed_client_secret = models.TextField(db_column="hashedClientSecret")
    partial_client_secret = models.TextField(db_column="partialClientSecret")
    redirect_uris = models.JSONField(db_column="redirectUris")
    pkce = models.BooleanField(default=False)

    class Meta:
        db_table = "OAuthApp"


class OAuthCode(models.Model):
    id = models.TextField(primary_key=True, default=None)
    o_auth_app = models.ForeignKey("OAuthApp", on_delete=models.CASCADE, related_name="o_auth_codes", to_field="client_id", db_column="clientId", db_index=False)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="o_auth_codes", db_column="userId", db_index=False)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="o_auth_codes", db_column="projectId", db_index=False)
    code = models.TextField(unique=True)
    scopes = models.TextField(null=True)
    redirect_uri = models.TextField(db_column="redirectUri")
    code_challenge = models.TextField(null=True, db_column="codeChallenge")
    code_challenge_method = models.TextField(null=True, db_column="codeChallengeMethod")
    expires_at = UtcDateTimeField(db_column="expiresAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "OAuthCode"
        indexes = [
            models.Index(fields=["o_auth_app"]),
            models.Index(fields=["user"]),
            models.Index(fields=["project"]),
        ]


class OAuthRefreshToken(models.Model):
    id = models.TextField(primary_key=True, default=None)
    installed_integration = models.ForeignKey("InstalledIntegration", on_delete=models.CASCADE, related_name="refresh_tokens", db_column="installationId", db_index=False)
    access_token = models.ForeignKey("RestrictedToken", on_delete=models.CASCADE, related_name="refresh_tokens", db_column="accessTokenId", db_index=False)
    hashed_refresh_token = models.TextField(unique=True, db_column="hashedRefreshToken")
    expires_at = UtcDateTimeField(db_column="expiresAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "OAuthRefreshToken"
        indexes = [
            models.Index(fields=["installed_integration"]),
            models.Index(fields=["access_token"]),
        ]


class User(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField(null=True)
    email = models.TextField(null=True, unique=True)
    email_verified = UtcDateTimeField(null=True, db_column="emailVerified")
    image = models.TextField(null=True)
    is_machine = models.BooleanField(db_column="isMachine", default=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    password_hash = models.TextField(null=True, db_column="passwordHash")
    invalid_login_attempts = models.IntegerField(db_column="invalidLoginAttempts", default=0)
    locked_at = UtcDateTimeField(null=True, db_column="lockedAt")
    default_workspace = models.TextField(null=True, db_column="defaultWorkspace")
    source = models.TextField(null=True)
    sent_mail = models.BooleanField(db_column="sentMail", default=False)

    class Meta:
        db_table = "User"
        indexes = [
            models.Index(fields=["created_at"]),
            models.Index(fields=["default_workspace"]),
            models.Index(fields=["source"]),
            models.Index(fields=["sent_mail"]),
        ]


class Account(models.Model):
    id = models.TextField(primary_key=True, default=None)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="accounts", db_column="userId", db_index=False)
    type = models.TextField()
    provider = models.TextField()
    provider_account_id = models.TextField(db_column="providerAccountId")
    refresh_token = models.TextField(null=True)
    refresh_token_expires_in = models.IntegerField(null=True)
    access_token = models.TextField(null=True)
    expires_at = models.IntegerField(null=True)
    token_type = models.TextField(null=True)
    scope = models.TextField(null=True)
    id_token = models.TextField(null=True)
    session_state = models.TextField(null=True)

    class Meta:
        db_table = "Account"
        constraints = [
            models.UniqueConstraint(fields=["provider", "provider_account_id"], name="Account_provider_providerAccountId_key"),
        ]
        indexes = [
            models.Index(fields=["user"]),
        ]


class Session(models.Model):
    id = models.TextField(primary_key=True, default=None)
    session_token = models.TextField(unique=True, db_column="sessionToken")
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="sessions", db_column="userId", db_index=False)
    expires = UtcDateTimeField()

    class Meta:
        db_table = "Session"
        indexes = [
            models.Index(fields=["user"]),
        ]


class Tag(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    color = models.TextField(default="blue")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="tags", db_column="projectId", null=True, db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Tag"
        constraints = [
            models.UniqueConstraint(fields=["name", "project"], name="Tag_name_projectId_key"),
        ]
        indexes = [
            models.Index(fields=["project"]),
        ]


class LinkTag(models.Model):
    id = models.TextField(primary_key=True, default=None)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    link = models.ForeignKey("Link", on_delete=models.CASCADE, related_name="tags", db_column="linkId", db_index=False)
    tag = models.ForeignKey("Tag", on_delete=models.CASCADE, related_name="links", db_column="tagId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LinkTag"
        constraints = [
            models.UniqueConstraint(fields=["link", "tag"], name="LinkTag_linkId_tagId_key"),
        ]
        indexes = [
            models.Index(fields=["tag"]),
        ]


class Token(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    hashed_key = models.TextField(unique=True, db_column="hashedKey")
    partial_key = models.TextField(db_column="partialKey")
    expires = UtcDateTimeField(null=True)
    last_used = UtcDateTimeField(null=True, db_column="lastUsed")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="tokens", db_column="userId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Token"
        indexes = [
            models.Index(fields=["user"]),
        ]


class RestrictedToken(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    hashed_key = models.TextField(unique=True, db_column="hashedKey")
    partial_key = models.TextField(db_column="partialKey")
    scopes = models.TextField(null=True)
    expires = UtcDateTimeField(null=True)
    last_used = UtcDateTimeField(null=True, db_column="lastUsed")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="restricted_tokens", db_column="userId", db_index=False)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="restricted_tokens", db_column="projectId", db_index=False)
    installed_integration = models.ForeignKey("InstalledIntegration", on_delete=models.CASCADE, related_name="access_tokens", db_column="installationId", null=True, db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "RestrictedToken"
        indexes = [
            models.Index(fields=["user"]),
            models.Index(fields=["project"]),
            models.Index(fields=["installed_integration"]),
        ]


class VerificationToken(models.Model):
    identifier = models.TextField()
    token = models.TextField(primary_key=True, default=None)
    expires = UtcDateTimeField()

    class Meta:
        db_table = "VerificationToken"
        constraints = [
            models.UniqueConstraint(fields=["identifier", "token"], name="VerificationToken_identifier_token_key"),
        ]


class EmailVerificationToken(models.Model):
    identifier = models.TextField()
    token = models.TextField(primary_key=True, default=None)
    expires = UtcDateTimeField()

    class Meta:
        db_table = "EmailVerificationToken"
        constraints = [
            models.UniqueConstraint(fields=["identifier", "token"], name="EmailVerificationToken_identifier_token_key"),
        ]


class PasswordResetToken(models.Model):
    identifier = models.TextField()
    token = models.TextField(primary_key=True, default=None)
    expires = UtcDateTimeField()

    class Meta:
        db_table = "PasswordResetToken"
        constraints = [
            models.UniqueConstraint(fields=["identifier", "token"], name="PasswordResetToken_identifier_token_key"),
        ]


class UtmTemplate(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    utm_source = models.CharField(max_length=255, null=True)
    utm_medium = models.CharField(max_length=255, null=True)
    utm_campaign = models.CharField(max_length=255, null=True)
    utm_term = models.CharField(max_length=255, null=True)
    utm_content = models.CharField(max_length=255, null=True)
    ref = models.CharField(max_length=255, null=True)
    user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="utm_templates", db_column="userId", null=True, db_index=False)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="utm_templates", db_column="projectId", null=True, db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "UtmTemplate"
        constraints = [
            models.UniqueConstraint(fields=["project", "name"], name="UtmTemplate_projectId_name_key"),
        ]
        indexes = [
            models.Index(fields=["user"]),
        ]


class Webhook(models.Model):
    id = models.TextField(primary_key=True, default=None)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="webhooks", db_column="projectId", db_index=False)
    installed_integration = models.ForeignKey("InstalledIntegration", on_delete=models.CASCADE, related_name="webhooks", db_column="installationId", null=True, db_index=False)
    receiver = models.TextField(choices=WebhookReceiver.choices, default=WebhookReceiver.user)
    name = models.TextField()
    url = models.TextField()
    secret = models.TextField()
    triggers = models.JSONField()
    link_scope = models.TextField(choices=LinkScope.choices, null=True, db_column="linkScope")
    consecutive_failures = models.IntegerField(db_column="consecutiveFailures", default=0)
    last_failed_at = UtcDateTimeField(null=True, db_column="lastFailedAt")
    disabled_at = UtcDateTimeField(null=True, db_column="disabledAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Webhook"
        indexes = [
            models.Index(fields=["project"]),
            models.Index(fields=["installed_integration"]),
        ]


class LinkWebhook(models.Model):
    id = models.TextField(primary_key=True, default=None)
    link = models.ForeignKey("Link", on_delete=models.CASCADE, related_name="webhooks", db_column="linkId", db_index=False)
    webhook = models.ForeignKey("Webhook", on_delete=models.CASCADE, related_name="links", db_column="webhookId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "LinkWebhook"
        constraints = [
            models.UniqueConstraint(fields=["link", "webhook"], name="LinkWebhook_linkId_webhookId_key"),
        ]
        indexes = [
            models.Index(fields=["webhook"]),
        ]


class FolderWebhook(models.Model):
    id = models.TextField(primary_key=True, default=None)
    folder = models.ForeignKey("Folder", on_delete=models.CASCADE, related_name="webhooks", db_column="folderId", db_index=False)
    webhook = models.ForeignKey("Webhook", on_delete=models.CASCADE, related_name="folders", db_column="webhookId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "FolderWebhook"
        constraints = [
            models.UniqueConstraint(fields=["folder", "webhook"], name="FolderWebhook_folderId_webhookId_key"),
        ]
        indexes = [
            models.Index(fields=["webhook"]),
        ]


class Project(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    slug = models.TextField(unique=True)
    logo = models.TextField(null=True)
    invite_code = models.TextField(null=True, unique=True, db_column="inviteCode")
    plan = models.TextField(default="free")
    plan_tier = models.IntegerField(db_column="planTier", default=1)
    plan_period = models.TextField(choices=PlanPeriod.choices, null=True, db_column="planPeriod")
    stripe_id = models.TextField(null=True, unique=True, db_column="stripeId")
    billing_cycle_start = models.IntegerField(db_column="billingCycleStart")
    billing_cycle_ends_at = UtcDateTimeField(null=True, db_column="billingCycleEndsAt")
    trial_ends_at = UtcDateTimeField(null=True, db_column="trialEndsAt")
    subscription_canceled_at = UtcDateTimeField(null=True, db_column="subscriptionCanceledAt")
    payment_failed_at = UtcDateTimeField(null=True, db_column="paymentFailedAt")
    invoice_prefix = models.TextField(null=True, unique=True, db_column="invoicePrefix")
    stripe_connect_id = models.TextField(null=True, db_column="stripeConnectId")
    stripe_connect_mode = models.TextField(choices=StripeConnectMode.choices, null=True, db_column="stripeConnectMode")
    shopify_store_id = models.TextField(null=True, unique=True, db_column="shopifyStoreId")
    total_links = models.IntegerField(db_column="totalLinks", default=0)
    total_clicks = models.IntegerField(db_column="totalClicks", default=0)
    usage = models.IntegerField(default=0)
    usage_limit = models.IntegerField(db_column="usageLimit", default=1000)
    links_usage = models.IntegerField(db_column="linksUsage", default=0)
    links_limit = models.IntegerField(db_column="linksLimit", default=25)
    domains_limit = models.IntegerField(db_column="domainsLimit", default=3)
    tags_limit = models.IntegerField(db_column="tagsLimit", default=5)
    folders_usage = models.IntegerField(db_column="foldersUsage", default=0)
    folders_limit = models.IntegerField(db_column="foldersLimit", default=0)
    users_limit = models.IntegerField(db_column="usersLimit", default=1)
    ai_usage = models.IntegerField(db_column="aiUsage", default=0)
    ai_limit = models.IntegerField(db_column="aiLimit", default=10)
    store = models.JSONField(null=True)
    site_visit_tracking_settings = models.JSONField(null=True, db_column="siteVisitTrackingSettings")
    allowed_hostnames = models.JSONField(null=True, db_column="allowedHostnames")
    publishable_key = models.TextField(null=True, unique=True, db_column="publishableKey")
    conversion_enabled = models.BooleanField(db_column="conversionEnabled", default=False)
    webhook_enabled = models.BooleanField(db_column="webhookEnabled", default=False)
    dot_link_claimed = models.BooleanField(db_column="dotLinkClaimed", default=False)
    sso_email_domain = models.TextField(null=True, unique=True, db_column="ssoEmailDomain")
    sso_enforced_at = UtcDateTimeField(null=True, db_column="ssoEnforcedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    usage_last_checked = UtcDateTimeField(db_column="usageLastChecked", default=timezone.now)
    disabled_at = UtcDateTimeField(null=True, db_column="disabledAt")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Project"
        constraints = [
            models.UniqueConstraint(fields=["stripe_connect_id", "stripe_connect_mode"], name="Project_stripeConnectId_stripeConnectMode_key"),
        ]
        indexes = [
            models.Index(fields=["usage_last_checked"]),
            models.Index(fields=["trial_ends_at"]),
        ]


class ProjectInvite(models.Model):
    pk = models.CompositePrimaryKey("email", "project_id")
    email = models.TextField()
    expires = UtcDateTimeField()
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="invites", db_column="projectId", db_index=False)
    role = models.TextField(choices=WorkspaceRole.choices, default=WorkspaceRole.member)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "ProjectInvite"
        constraints = [
            models.UniqueConstraint(fields=["email", "project"], name="ProjectInvite_email_projectId_key"),
        ]
        indexes = [
            models.Index(fields=["project"]),
        ]


class ProjectUsers(models.Model):
    id = models.TextField(primary_key=True, default=None)
    role = models.TextField(choices=WorkspaceRole.choices, default=WorkspaceRole.member)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="projects", db_column="userId", db_index=False)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="users", db_column="projectId", db_index=False)
    workspace_preferences = models.JSONField(null=True, db_column="workspacePreferences")
    default_folder_id = models.TextField(null=True, db_column="defaultFolderId")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ProjectUsers"
        constraints = [
            models.UniqueConstraint(fields=["user", "project"], name="ProjectUsers_userId_projectId_key"),
        ]
        indexes = [
            models.Index(fields=["project"]),
        ]


class SentEmail(models.Model):
    id = models.TextField(primary_key=True, default=None)
    type = models.TextField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    project = models.ForeignKey("Project", on_delete=models.CASCADE, related_name="sent_emails", db_column="projectId", null=True, db_index=False)

    class Meta:
        db_table = "SentEmail"
        indexes = [
            models.Index(fields=["project"]),
        ]
