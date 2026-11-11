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


class Theme(models.TextChoices):
    dark = "dark"
    light = "light"
    auto = "auto"


class AiTaggingMethod(models.TextChoices):
    DISABLED = "DISABLED"
    GENERATE = "GENERATE"
    EXISTING = "EXISTING"
    PREDEFINED = "PREDEFINED"


class LinksRouteTo(models.TextChoices):
    ORIGINAL = "ORIGINAL"
    PDF = "PDF"
    READABLE = "READABLE"
    MONOLITH = "MONOLITH"
    SCREENSHOT = "SCREENSHOT"
    DETAILS = "DETAILS"


class SubscriptionProvider(models.TextChoices):
    STRIPE = "STRIPE"
    APPLE = "APPLE"
    GOOGLE = "GOOGLE"


class DashboardSectionType(models.TextChoices):
    STATS = "STATS"
    RECENT_LINKS = "RECENT_LINKS"
    PINNED_LINKS = "PINNED_LINKS"
    COLLECTION = "COLLECTION"


class AppMigrationStatus(models.TextChoices):
    APPLIED = "APPLIED"
    PENDING = "PENDING"
    FAILED = "FAILED"


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

    class Meta:
        db_table = "Account"
        constraints = [
            models.UniqueConstraint(fields=["provider", "provider_account_id"], name="Account_provider_providerAccountId_key"),
        ]


class User(models.Model):
    id = models.AutoField(primary_key=True)
    uuid = models.UUIDField(unique=True, default=uuid.uuid4)
    name = models.TextField(null=True)
    username = models.TextField(null=True, unique=True)
    email = models.TextField(null=True, unique=True)
    email_verified = UtcDateTimeField(null=True, db_column="emailVerified")
    unverified_new_email = models.TextField(null=True, db_column="unverifiedNewEmail")
    image = models.TextField(null=True)
    password = models.TextField(null=True)
    locale = models.TextField(default="en")
    parent_subscription = models.ForeignKey("Subscription", on_delete=models.SET_NULL, related_name="child_users", db_column="parentSubscriptionId", null=True, db_index=False)
    collection_order = ArrayField(models.IntegerField(), db_column="collectionOrder", default=list)
    links_route_to = models.TextField(choices=LinksRouteTo.choices, db_column="linksRouteTo", default=LinksRouteTo.ORIGINAL)
    ai_tagging_method = models.TextField(choices=AiTaggingMethod.choices, db_column="aiTaggingMethod", default=AiTaggingMethod.DISABLED)
    ai_predefined_tags = ArrayField(models.TextField(), db_column="aiPredefinedTags", default=list)
    ai_tag_existing_links = models.BooleanField(db_column="aiTagExistingLinks", default=False)
    theme = models.TextField(choices=Theme.choices, default=Theme.dark)
    dismissed_announcement_id = models.TextField(null=True, db_column="dismissedAnnouncementId")
    readable_font_family = models.TextField(null=True, db_column="readableFontFamily", default="sans-serif")
    readable_font_size = models.TextField(null=True, db_column="readableFontSize", default="20px")
    readable_line_height = models.TextField(null=True, db_column="readableLineHeight", default="1.8")
    readable_line_width = models.TextField(null=True, db_column="readableLineWidth", default="normal")
    prevent_duplicate_links = models.BooleanField(db_column="preventDuplicateLinks", default=False)
    archive_as_screenshot = models.BooleanField(db_column="archiveAsScreenshot", default=True)
    archive_as_monolith = models.BooleanField(db_column="archiveAsMonolith", default=True)
    archive_as_pdf = models.BooleanField(db_column="archiveAsPDF", default=True)
    archive_as_readable = models.BooleanField(db_column="archiveAsReadable", default=True)
    archive_as_wayback_machine = models.BooleanField(db_column="archiveAsWaybackMachine", default=False)
    referred_by = models.TextField(null=True, db_column="referredBy")
    last_picked_at = UtcDateTimeField(null=True, db_column="lastPickedAt")
    accept_promotional_emails = models.BooleanField(db_column="acceptPromotionalEmails", default=False)
    share_usage_analytics = models.BooleanField(db_column="shareUsageAnalytics", default=True)
    trial_end_email_sent = models.BooleanField(db_column="trialEndEmailSent", default=False)
    session_version = models.IntegerField(db_column="sessionVersion", default=0)
    password_retries = models.IntegerField(db_column="passwordRetries", default=0)
    password_retry_at = UtcDateTimeField(null=True, db_column="passwordRetryAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "User"


class VerificationToken(models.Model):
    identifier = models.TextField()
    token = models.TextField(primary_key=True, default=None)
    expires = UtcDateTimeField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "VerificationToken"
        constraints = [
            models.UniqueConstraint(fields=["identifier", "token"], name="VerificationToken_identifier_token_key"),
        ]


class EmailVerificationCode(models.Model):
    identifier = models.TextField(primary_key=True, default=None)
    code = models.TextField()
    attempts = models.IntegerField(default=0)
    expires = UtcDateTimeField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "EmailVerificationCode"


class PasswordResetToken(models.Model):
    identifier = models.TextField()
    token = models.TextField(primary_key=True, default=None)
    expires = UtcDateTimeField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "PasswordResetToken"


class Collection(models.Model):
    id = models.AutoField(primary_key=True)
    name = models.TextField()
    description = models.TextField(default="")
    icon = models.TextField(null=True)
    icon_weight = models.TextField(null=True, db_column="iconWeight")
    color = models.TextField(default="#0ea5e9")
    parent = models.ForeignKey("self", on_delete=models.CASCADE, related_name="sub_collections", db_column="parentId", null=True, db_index=False)
    is_public = models.BooleanField(db_column="isPublic", default=False)
    owner = models.ForeignKey("User", on_delete=models.CASCADE, related_name="collections", db_column="ownerId", db_index=False)
    created_by = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="created_collections", db_column="createdById", null=True, db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Collection"
        indexes = [
            models.Index(fields=["owner"]),
        ]


class UsersAndCollections(models.Model):
    pk = models.CompositePrimaryKey("user_id", "collection_id")
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="collections_joined", db_column="userId", db_index=False)
    collection = models.ForeignKey("Collection", on_delete=models.CASCADE, related_name="members", db_column="collectionId", db_index=False)
    can_create = models.BooleanField(db_column="canCreate")
    can_update = models.BooleanField(db_column="canUpdate")
    can_delete = models.BooleanField(db_column="canDelete")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "UsersAndCollections"
        indexes = [
            models.Index(fields=["user"]),
        ]


class Link(models.Model):
    id = models.AutoField(primary_key=True)
    name = models.TextField(default="")
    type = models.TextField(default="url")
    description = models.TextField(default="")
    pinned_by: "models.ManyToManyField[User, PinnedLinks]" = models.ManyToManyField("User", through="PinnedLinks", related_name="pinned_links")
    created_by = models.ForeignKey("User", on_delete=models.CASCADE, related_name="created_links", db_column="createdById", null=True, db_index=False)
    collection = models.ForeignKey("Collection", on_delete=models.CASCADE, related_name="links", db_column="collectionId", db_index=False)
    collection_order = models.FloatField(db_column="collectionOrder")
    tags: "models.ManyToManyField[Tag, LinkToTag]" = models.ManyToManyField("Tag", through="LinkToTag", related_name="links")
    icon = models.TextField(null=True)
    icon_weight = models.TextField(null=True, db_column="iconWeight")
    color = models.TextField(null=True)
    url = models.TextField(null=True)
    text_content = models.TextField(null=True, db_column="textContent")
    preview = models.TextField(null=True)
    image = models.TextField(null=True)
    pdf = models.TextField(null=True)
    readable = models.TextField(null=True)
    monolith = models.TextField(null=True)
    client_side = models.BooleanField(db_column="clientSide", default=False)
    ai_tagged = models.BooleanField(db_column="aiTagged", default=False)
    meta_description = models.TextField(null=True, db_column="metaDescription")
    index_version = models.IntegerField(null=True, db_column="indexVersion")
    last_preserved = UtcDateTimeField(null=True, db_column="lastPreserved")
    import_date = UtcDateTimeField(null=True, db_column="importDate")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Link"
        indexes = [
            models.Index(fields=["collection"]),
            models.Index(fields=["collection", "collection_order", "id"]),
        ]


class Tag(models.Model):
    id = models.AutoField(primary_key=True)
    name = models.TextField()
    owner = models.ForeignKey("User", on_delete=models.CASCADE, related_name="tags", db_column="ownerId", db_index=False)
    archive_as_screenshot = models.BooleanField(null=True, db_column="archiveAsScreenshot")
    archive_as_monolith = models.BooleanField(null=True, db_column="archiveAsMonolith")
    archive_as_pdf = models.BooleanField(null=True, db_column="archiveAsPDF")
    archive_as_readable = models.BooleanField(null=True, db_column="archiveAsReadable")
    archive_as_wayback_machine = models.BooleanField(null=True, db_column="archiveAsWaybackMachine")
    ai_tag = models.BooleanField(null=True, db_column="aiTag")
    ai_generated = models.BooleanField(db_column="aiGenerated", default=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Tag"
        constraints = [
            models.UniqueConstraint(fields=["name", "owner"], name="Tag_name_ownerId_key"),
        ]
        indexes = [
            models.Index(fields=["owner"]),
        ]


class Subscription(models.Model):
    id = models.AutoField(primary_key=True)
    active = models.BooleanField()
    provider = models.TextField(choices=SubscriptionProvider.choices)
    current_period_start = UtcDateTimeField(db_column="currentPeriodStart")
    current_period_end = UtcDateTimeField(db_column="currentPeriodEnd")
    quantity = models.IntegerField(default=1)
    store_original_transaction_id = models.TextField(null=True, unique=True, db_column="storeOriginalTransactionId")
    store_product_id = models.TextField(null=True, db_column="storeProductId")
    google_purchase_token = models.TextField(null=True, unique=True, db_column="googlePurchaseToken")
    store_metadata = models.JSONField(null=True, db_column="storeMetadata")
    stripe_subscription_id = models.TextField(null=True, unique=True, db_column="stripeSubscriptionId")
    user = models.OneToOneField("User", on_delete=models.CASCADE, related_name="subscriptions", db_column="userId")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Subscription"


class AccessToken(models.Model):
    id = models.AutoField(primary_key=True)
    name = models.TextField()
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="access_tokens", db_column="userId", db_index=False)
    token = models.TextField(unique=True)
    revoked = models.BooleanField(default=False)
    is_session = models.BooleanField(db_column="isSession", default=False)
    expires = UtcDateTimeField()
    last_used_at = UtcDateTimeField(null=True, db_column="lastUsedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "AccessToken"


class RssSubscription(models.Model):
    id = models.AutoField(primary_key=True)
    url = models.TextField()
    name = models.TextField()
    last_build_date = UtcDateTimeField(null=True, db_column="lastBuildDate")
    collection = models.ForeignKey("Collection", on_delete=models.CASCADE, related_name="rss_subscriptions", db_column="collectionId", db_index=False)
    owner_id = models.IntegerField(db_column="ownerId")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "RssSubscription"


class Highlight(models.Model):
    id = models.AutoField(primary_key=True)
    color = models.TextField()
    comment = models.TextField(null=True)
    link = models.ForeignKey("Link", on_delete=models.CASCADE, related_name="highlight", db_column="linkId", db_index=False)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="highlights", db_column="userId", db_index=False)
    start_offset = models.IntegerField(db_column="startOffset")
    end_offset = models.IntegerField(db_column="endOffset")
    text = models.TextField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Highlight"


class DashboardSection(models.Model):
    id = models.AutoField(primary_key=True)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="dashboard_sections", db_column="userId", db_index=False)
    collection = models.ForeignKey("Collection", on_delete=models.CASCADE, related_name="dashboard_section", db_column="collectionId", null=True, db_index=False)
    type = models.TextField(choices=DashboardSectionType.choices)
    order = models.IntegerField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "DashboardSection"
        constraints = [
            models.UniqueConstraint(fields=["user", "collection"], name="DashboardSection_userId_collectionId_key"),
        ]


class AppMigration(models.Model):
    id = models.AutoField(primary_key=True)
    name = models.TextField(unique=True)
    status = models.TextField(choices=AppMigrationStatus.choices)
    finished_at = UtcDateTimeField(null=True, db_column="finishedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "AppMigration"


class PinnedLinks(models.Model):
    pk = models.CompositePrimaryKey("a_id", "b_id")
    a = models.ForeignKey("Link", on_delete=models.CASCADE, related_name="+", db_column="A", db_index=False)
    b = models.ForeignKey("User", on_delete=models.CASCADE, related_name="+", db_column="B", db_index=False)

    class Meta:
        db_table = "_PinnedLinks"
        indexes = [
            models.Index(fields=["b"]),
        ]


class LinkToTag(models.Model):
    pk = models.CompositePrimaryKey("a_id", "b_id")
    a = models.ForeignKey("Link", on_delete=models.CASCADE, related_name="+", db_column="A", db_index=False)
    b = models.ForeignKey("Tag", on_delete=models.CASCADE, related_name="+", db_column="B", db_index=False)

    class Meta:
        db_table = "_LinkToTag"
        indexes = [
            models.Index(fields=["b"]),
        ]
