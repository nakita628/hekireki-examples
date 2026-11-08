import uuid
from datetime import date, datetime
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


def utc_today() -> date:
    return datetime.now(dt_timezone.utc).date()


class OrderStatus(models.TextChoices):
    PENDING = "PENDING"
    ACCEPTED = "ACCEPTED"
    CANCELED = "CANCELED"
    COMPLETED = "COMPLETED"


class From(models.TextChoices):
    BUYER = "BUYER"
    SELLER = "SELLER"


class State(models.TextChoices):
    QUEUE = "QUEUE"
    PUBLISHED = "PUBLISHED"
    ERROR = "ERROR"
    DRAFT = "DRAFT"


class SubscriptionTier(models.TextChoices):
    STANDARD = "STANDARD"
    PRO = "PRO"
    TEAM = "TEAM"
    ULTIMATE = "ULTIMATE"


class Period(models.TextChoices):
    MONTHLY = "MONTHLY"
    YEARLY = "YEARLY"


class Provider(models.TextChoices):
    LOCAL = "LOCAL"
    GITHUB = "GITHUB"
    GOOGLE = "GOOGLE"
    APPLE = "APPLE"
    FARCASTER = "FARCASTER"
    WALLET = "WALLET"
    GENERIC = "GENERIC"


class Role(models.TextChoices):
    SUPERADMIN = "SUPERADMIN"
    ADMIN = "ADMIN"
    USER = "USER"


class APPROVEDSUBMITFORORDER(models.TextChoices):
    NO = "NO"
    WAITING_CONFIRMATION = "WAITING_CONFIRMATION"
    YES = "YES"


class CreationMethod(models.TextChoices):
    UNKNOWN = "UNKNOWN"
    WEB = "WEB"
    MCP = "MCP"
    API = "API"
    AUTOPOST = "AUTOPOST"
    CLI = "CLI"


class ShortLinkPreference(models.TextChoices):
    ASK = "ASK"
    YES = "YES"
    NO = "NO"


class AnnouncementColor(models.TextChoices):
    INFO = "INFO"
    WARNING = "WARNING"
    ERROR = "ERROR"


class Organization(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    name = models.TextField()
    description = models.TextField(null=True)
    api_key = models.TextField(null=True, db_column="apiKey")
    payment_id = models.TextField(null=True, db_column="paymentId")
    streak_since = UtcDateTimeField(null=True, db_column="streakSince")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    deleted_at = UtcDateTimeField(null=True, db_column="deletedAt")
    allow_trial = models.BooleanField(db_column="allowTrial", default=False)
    is_trailing = models.BooleanField(db_column="isTrailing", default=False)
    shortlink = models.TextField(choices=ShortLinkPreference.choices, default=ShortLinkPreference.ASK)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Organization"
        indexes = [
            models.Index(fields=["api_key"]),
            models.Index(fields=["streak_since"]),
            models.Index(fields=["payment_id"]),
            models.Index(fields=["deleted_at"]),
        ]


class Tags(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    name = models.TextField()
    color = models.TextField()
    organization = models.ForeignKey("Organization", on_delete=models.RESTRICT, related_name="tags", db_column="orgId", db_index=False)
    deleted_at = UtcDateTimeField(null=True, db_column="deletedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Tags"
        indexes = [
            models.Index(fields=["organization"]),
            models.Index(fields=["deleted_at"]),
        ]


class TagsPosts(models.Model):
    pk = models.CompositePrimaryKey("post_id", "tag_id")
    post = models.ForeignKey("Post", on_delete=models.RESTRICT, related_name="tags", db_column="postId", db_index=False)
    tag = models.ForeignKey("Tags", on_delete=models.RESTRICT, related_name="posts", db_column="tagId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "TagsPosts"
        constraints = [
            models.UniqueConstraint(fields=["post", "tag"], name="TagsPosts_postId_tagId_key"),
        ]


class User(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    email = models.TextField()
    password = models.TextField(null=True)
    provider_name = models.TextField(choices=Provider.choices, db_column="providerName")
    name = models.TextField(null=True)
    last_name = models.TextField(null=True, db_column="lastName")
    is_super_admin = models.BooleanField(db_column="isSuperAdmin", default=False)
    bio = models.TextField(null=True)
    audience = models.IntegerField(default=0)
    picture = models.ForeignKey("Media", on_delete=models.SET_NULL, related_name="user_picture", db_column="pictureId", null=True, db_index=False)
    provider_id = models.TextField(null=True, db_column="providerId")
    timezone_field = models.IntegerField(db_column="timezone")
    timezone_name = models.TextField(null=True, db_column="timezoneName")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    last_read_notifications = UtcDateTimeField(db_column="lastReadNotifications", default=timezone.now)
    invite_id = models.TextField(null=True, db_column="inviteId")
    activated = models.BooleanField(default=True)
    account = models.TextField(null=True)
    connected_account = models.BooleanField(db_column="connectedAccount", default=False)
    last_online = UtcDateTimeField(db_column="lastOnline", default=timezone.now)
    ip = models.TextField(null=True)
    agent = models.TextField(null=True)
    deleted_at = UtcDateTimeField(null=True, db_column="deletedAt")
    send_success_emails = models.BooleanField(db_column="sendSuccessEmails", default=True)
    send_failure_emails = models.BooleanField(db_column="sendFailureEmails", default=True)
    send_streak_emails = models.BooleanField(db_column="sendStreakEmails", default=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "User"
        constraints = [
            models.UniqueConstraint(fields=["email", "provider_name"], name="User_email_providerName_key"),
        ]
        indexes = [
            models.Index(fields=["provider_id", "provider_name"]),
            models.Index(fields=["last_read_notifications"]),
            models.Index(fields=["invite_id"]),
            models.Index(fields=["account"]),
            models.Index(fields=["last_online"]),
            models.Index(fields=["picture"]),
            models.Index(fields=["deleted_at"]),
        ]


class UsedCodes(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    code = models.TextField()
    organization = models.ForeignKey("Organization", on_delete=models.RESTRICT, related_name="used_codes", db_column="orgId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "UsedCodes"
        indexes = [
            models.Index(fields=["code"]),
        ]


class UserOrganization(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    user = models.ForeignKey("User", on_delete=models.RESTRICT, related_name="organizations", db_column="userId", db_index=False)
    organization = models.ForeignKey("Organization", on_delete=models.RESTRICT, related_name="users", db_column="organizationId", db_index=False)
    disabled = models.BooleanField(default=False)
    role = models.TextField(choices=Role.choices, default=Role.USER)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "UserOrganization"
        constraints = [
            models.UniqueConstraint(fields=["user", "organization"], name="UserOrganization_userId_organizationId_key"),
        ]
        indexes = [
            models.Index(fields=["organization"]),
            models.Index(fields=["disabled"]),
        ]


class GitHub(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    login = models.TextField(null=True)
    name = models.TextField(null=True)
    token = models.TextField()
    job_id = models.TextField(null=True, db_column="jobId")
    organization = models.ForeignKey("Organization", on_delete=models.RESTRICT, related_name="github", db_column="organizationId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "GitHub"
        indexes = [
            models.Index(fields=["login"]),
            models.Index(fields=["organization"]),
        ]


class Trending(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    trending_list = models.TextField(db_column="trendingList")
    language = models.TextField(null=True, unique=True)
    hash = models.TextField()
    date = UtcDateTimeField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Trending"
        indexes = [
            models.Index(fields=["hash"]),
        ]


class TrendingLog(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    language = models.TextField(null=True)
    date = UtcDateTimeField()

    class Meta:
        db_table = "TrendingLog"


class ItemUser(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    user = models.ForeignKey("User", on_delete=models.RESTRICT, related_name="items", db_column="userId", db_index=False)
    key = models.TextField()

    class Meta:
        db_table = "ItemUser"
        constraints = [
            models.UniqueConstraint(fields=["user", "key"], name="ItemUser_userId_key_key"),
        ]
        indexes = [
            models.Index(fields=["user"]),
            models.Index(fields=["key"]),
        ]


class Star(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    stars = models.IntegerField()
    total_stars = models.IntegerField(db_column="totalStars")
    forks = models.IntegerField()
    total_forks = models.IntegerField(db_column="totalForks")
    login = models.TextField()
    date = models.DateField(default=utc_today)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Star"
        constraints = [
            models.UniqueConstraint(fields=["login", "date"], name="Star_login_date_key"),
        ]


class Media(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    name = models.TextField()
    original_name = models.TextField(null=True, db_column="originalName")
    path = models.TextField()
    organization = models.ForeignKey("Organization", on_delete=models.RESTRICT, related_name="media", db_column="organizationId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    deleted_at = UtcDateTimeField(null=True, db_column="deletedAt")
    file_size = models.IntegerField(db_column="fileSize", default=0)
    type = models.TextField(default="image")
    thumbnail = models.TextField(null=True)
    alt = models.TextField(null=True)
    thumbnail_timestamp = models.IntegerField(null=True, db_column="thumbnailTimestamp")
    status = models.TextField(default="ready")
    processing_error = models.TextField(null=True, db_column="processingError")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Media"
        indexes = [
            models.Index(fields=["name"]),
            models.Index(fields=["organization"]),
            models.Index(fields=["type"]),
        ]


class SocialMediaAgency(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    user = models.OneToOneField("User", on_delete=models.RESTRICT, related_name="agencies", db_column="userId")
    name = models.TextField()
    logo = models.ForeignKey("Media", on_delete=models.SET_NULL, related_name="agencies", db_column="logoId", null=True, db_index=False)
    website = models.TextField(null=True)
    slug = models.TextField(null=True)
    facebook = models.TextField(null=True)
    instagram = models.TextField(null=True)
    twitter = models.TextField(null=True)
    linked_in = models.TextField(null=True, db_column="linkedIn")
    youtube = models.TextField(null=True)
    tiktok = models.TextField(null=True)
    other_social_media = models.TextField(null=True, db_column="otherSocialMedia")
    short_description = models.TextField(db_column="shortDescription")
    description = models.TextField()
    approved = models.BooleanField(default=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    deleted_at = UtcDateTimeField(null=True, db_column="deletedAt")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "SocialMediaAgency"
        indexes = [
            models.Index(fields=["user"]),
            models.Index(fields=["deleted_at"]),
            models.Index(fields=["id"]),
        ]


class SocialMediaAgencyNiche(models.Model):
    pk = models.CompositePrimaryKey("agency_id", "niche")
    agency = models.ForeignKey("SocialMediaAgency", on_delete=models.RESTRICT, related_name="niches", db_column="agencyId", db_index=False)
    niche = models.TextField()

    class Meta:
        db_table = "SocialMediaAgencyNiche"


class Credits(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    organization = models.ForeignKey("Organization", on_delete=models.RESTRICT, related_name="credits", db_column="organizationId", db_index=False)
    credits = models.IntegerField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    type = models.TextField(default="ai_images")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Credits"
        indexes = [
            models.Index(fields=["organization"]),
            models.Index(fields=["created_at"]),
        ]


class Clipping(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    organization = models.ForeignKey("Organization", on_delete=models.RESTRICT, related_name="clippings", db_column="organizationId", db_index=False)
    url = models.TextField()
    status = models.TextField(default="analysing")
    error = models.TextField(null=True)
    title = models.TextField(null=True)
    thumbnail = models.TextField(null=True)
    duration = models.IntegerField(null=True)
    max_clips = models.IntegerField(db_column="maxClips", default=5)
    fit = models.TextField(default="blur")
    integrations = models.TextField()
    credits_id = models.TextField(null=True, db_column="creditsId")
    deleted_at = UtcDateTimeField(null=True, db_column="deletedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Clipping"
        indexes = [
            models.Index(fields=["organization"]),
            models.Index(fields=["deleted_at"]),
        ]


class ClippingClip(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    clipping = models.ForeignKey("Clipping", on_delete=models.RESTRICT, related_name="clips", db_column="clippingId", db_index=False)
    title = models.TextField()
    content = models.TextField()
    start = models.FloatField()
    end = models.FloatField()
    trim_start = models.IntegerField(null=True, db_column="trimStart")
    status = models.TextField(default="pending")
    error = models.TextField(null=True)
    media_id = models.TextField(null=True, db_column="mediaId")
    path = models.TextField(null=True)
    thumbnail = models.TextField(null=True)
    drafted_at = UtcDateTimeField(null=True, db_column="draftedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ClippingClip"
        indexes = [
            models.Index(fields=["clipping"]),
        ]


class Subscription(models.Model):
    id = models.TextField(primary_key=True, default=None)
    organization = models.OneToOneField("Organization", on_delete=models.RESTRICT, related_name="subscription", db_column="organizationId")
    subscription_tier = models.TextField(choices=SubscriptionTier.choices, db_column="subscriptionTier")
    provider = models.TextField(default="stripe")
    identifier = models.TextField(null=True)
    cancel_at = UtcDateTimeField(null=True, db_column="cancelAt")
    period = models.TextField(choices=Period.choices)
    total_channels = models.IntegerField(db_column="totalChannels")
    is_lifetime = models.BooleanField(db_column="isLifetime", default=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    deleted_at = UtcDateTimeField(null=True, db_column="deletedAt")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Subscription"
        indexes = [
            models.Index(fields=["organization"]),
            models.Index(fields=["deleted_at"]),
        ]


class Customer(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    name = models.TextField()
    organization = models.ForeignKey("Organization", on_delete=models.RESTRICT, related_name="customers", db_column="orgId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    deleted_at = UtcDateTimeField(null=True, db_column="deletedAt")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Customer"
        constraints = [
            models.UniqueConstraint(fields=["organization", "name", "deleted_at"], name="Customer_orgId_name_deletedAt_key"),
        ]


class Integration(models.Model):
    id = models.TextField(primary_key=True, default=None)
    internal_id = models.TextField(db_column="internalId")
    organization = models.ForeignKey("Organization", on_delete=models.RESTRICT, related_name="integration", db_column="organizationId", db_index=False)
    name = models.TextField()
    custom_name = models.TextField(null=True, db_column="customName")
    picture = models.TextField(null=True)
    provider_identifier = models.TextField(db_column="providerIdentifier")
    type = models.TextField()
    token = models.TextField()
    disabled = models.BooleanField(default=False)
    token_expiration = UtcDateTimeField(null=True, db_column="tokenExpiration")
    refresh_token = models.TextField(null=True, db_column="refreshToken")
    profile = models.TextField(null=True)
    deleted_at = UtcDateTimeField(null=True, db_column="deletedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(null=True, db_column="updatedAt", auto_now=True)
    in_between_steps = models.BooleanField(db_column="inBetweenSteps", default=False)
    refresh_needed = models.BooleanField(db_column="refreshNeeded", default=False)
    posting_times = models.TextField(db_column="postingTimes", default="[{\"time\":120}, {\"time\":400}, {\"time\":700}]")
    custom_instance_details = models.TextField(null=True, db_column="customInstanceDetails")
    customer = models.ForeignKey("Customer", on_delete=models.SET_NULL, related_name="integrations", db_column="customerId", null=True, db_index=False)
    root_internal_id = models.TextField(null=True, db_column="rootInternalId")
    additional_settings = models.TextField(null=True, db_column="additionalSettings", default="[]")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Integration"
        constraints = [
            models.UniqueConstraint(fields=["organization", "internal_id"], name="Integration_organizationId_internalId_key"),
        ]
        indexes = [
            models.Index(fields=["root_internal_id"]),
            models.Index(fields=["organization"]),
            models.Index(fields=["provider_identifier"]),
            models.Index(fields=["updated_at"]),
            models.Index(fields=["created_at"]),
            models.Index(fields=["deleted_at"]),
            models.Index(fields=["customer"]),
            models.Index(fields=["in_between_steps"]),
            models.Index(fields=["refresh_needed"]),
            models.Index(fields=["disabled"]),
        ]


class Signatures(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    organization = models.ForeignKey("Organization", on_delete=models.RESTRICT, related_name="signatures", db_column="organizationId", db_index=False)
    content = models.TextField()
    auto_add = models.BooleanField(db_column="autoAdd")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    deleted_at = UtcDateTimeField(null=True, db_column="deletedAt")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Signatures"
        indexes = [
            models.Index(fields=["created_at"]),
            models.Index(fields=["organization"]),
            models.Index(fields=["deleted_at"]),
        ]


class Comments(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    content = models.TextField()
    organization = models.ForeignKey("Organization", on_delete=models.RESTRICT, related_name="comments", db_column="organizationId", db_index=False)
    post = models.ForeignKey("Post", on_delete=models.RESTRICT, related_name="comments", db_column="postId", db_index=False)
    user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="comments", db_column="userId", null=True, db_index=False)
    display_name = models.TextField(null=True, db_column="displayName")
    parent = models.ForeignKey("self", on_delete=models.SET_NULL, related_name="replies", db_column="parentId", null=True, db_index=False)
    anchor_start = models.IntegerField(null=True, db_column="anchorStart")
    anchor_end = models.IntegerField(null=True, db_column="anchorEnd")
    anchor_quote = models.TextField(null=True, db_column="anchorQuote")
    resolved_at = UtcDateTimeField(null=True, db_column="resolvedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    deleted_at = UtcDateTimeField(null=True, db_column="deletedAt")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Comments"
        indexes = [
            models.Index(fields=["created_at"]),
            models.Index(fields=["organization"]),
            models.Index(fields=["user"]),
            models.Index(fields=["post"]),
            models.Index(fields=["parent"]),
            models.Index(fields=["resolved_at"]),
            models.Index(fields=["deleted_at"]),
        ]


class Post(models.Model):
    id = models.TextField(primary_key=True, default=None)
    state = models.TextField(choices=State.choices, default=State.QUEUE)
    publish_date = UtcDateTimeField(db_column="publishDate")
    organization = models.ForeignKey("Organization", on_delete=models.RESTRICT, related_name="post", db_column="organizationId", db_index=False)
    integration = models.ForeignKey("Integration", on_delete=models.RESTRICT, related_name="posts", db_column="integrationId", db_index=False)
    content = models.TextField()
    delay = models.IntegerField(default=0)
    group = models.TextField()
    batch_id = models.TextField(null=True, db_column="batchId")
    title = models.TextField(null=True)
    description = models.TextField(null=True)
    parent_post = models.ForeignKey("self", on_delete=models.SET_NULL, related_name="children_post", db_column="parentPostId", null=True, db_index=False)
    release_id = models.TextField(null=True, db_column="releaseId")
    release_url = models.TextField(null=True, db_column="releaseURL")
    settings = models.TextField(null=True)
    image = models.TextField(null=True)
    submitted_for_order = models.ForeignKey("Orders", on_delete=models.SET_NULL, related_name="posts", db_column="submittedForOrderId", null=True, db_index=False)
    submitted_for_organization = models.ForeignKey("Organization", on_delete=models.SET_NULL, related_name="submitted_post", db_column="submittedForOrganizationId", null=True, db_index=False)
    approved_submit_for_order = models.TextField(choices=APPROVEDSUBMITFORORDER.choices, db_column="approvedSubmitForOrder", default=APPROVEDSUBMITFORORDER.NO)
    creation_method = models.TextField(choices=CreationMethod.choices, db_column="creationMethod", default=CreationMethod.UNKNOWN)
    last_message = models.ForeignKey("Messages", on_delete=models.SET_NULL, related_name="posts", db_column="lastMessageId", null=True, db_index=False)
    interval_in_days = models.IntegerField(null=True, db_column="intervalInDays")
    error = models.TextField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    deleted_at = UtcDateTimeField(null=True, db_column="deletedAt")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Post"
        indexes = [
            models.Index(fields=["group"]),
            models.Index(fields=["batch_id"]),
            models.Index(fields=["deleted_at"]),
            models.Index(fields=["publish_date"]),
            models.Index(fields=["publish_date", "state", "deleted_at", "parent_post", "integration", "organization", "creation_method"]),
            models.Index(fields=["state"]),
            models.Index(fields=["organization"]),
            models.Index(fields=["organization", "publish_date"]),
            models.Index(fields=["parent_post"]),
            models.Index(fields=["submitted_for_order"]),
            models.Index(fields=["interval_in_days"]),
            models.Index(fields=["approved_submit_for_order"]),
            models.Index(fields=["creation_method"]),
            models.Index(fields=["last_message"]),
            models.Index(fields=["created_at"]),
            models.Index(fields=["updated_at"]),
            models.Index(fields=["release_url"]),
            models.Index(fields=["integration"]),
        ]


class Notifications(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    organization = models.ForeignKey("Organization", on_delete=models.RESTRICT, related_name="notifications", db_column="organizationId", db_index=False)
    content = models.TextField()
    link = models.TextField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    deleted_at = UtcDateTimeField(null=True, db_column="deletedAt")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Notifications"
        indexes = [
            models.Index(fields=["created_at"]),
            models.Index(fields=["organization"]),
            models.Index(fields=["organization", "created_at", "deleted_at"]),
            models.Index(fields=["deleted_at"]),
        ]


class MessagesGroup(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    buyer_organization = models.ForeignKey("Organization", on_delete=models.RESTRICT, related_name="buyer_organization", db_column="buyerOrganizationId", db_index=False)
    buyer = models.ForeignKey("User", on_delete=models.RESTRICT, related_name="group_buyer", db_column="buyerId", db_index=False)
    seller = models.ForeignKey("User", on_delete=models.RESTRICT, related_name="group_seller", db_column="sellerId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "MessagesGroup"
        constraints = [
            models.UniqueConstraint(fields=["buyer", "seller"], name="MessagesGroup_buyerId_sellerId_key"),
        ]
        indexes = [
            models.Index(fields=["created_at"]),
            models.Index(fields=["updated_at"]),
            models.Index(fields=["buyer_organization"]),
        ]


class PayoutProblems(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    status = models.TextField()
    order = models.ForeignKey("Orders", on_delete=models.RESTRICT, related_name="payout_problems", db_column="orderId", db_index=False)
    user = models.ForeignKey("User", on_delete=models.RESTRICT, related_name="payout_problems", db_column="userId", db_index=False)
    post = models.ForeignKey("Post", on_delete=models.SET_NULL, related_name="payout_problems", db_column="postId", null=True, db_index=False)
    amount = models.IntegerField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "PayoutProblems"


class Orders(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    buyer = models.ForeignKey("User", on_delete=models.RESTRICT, related_name="order_buyer", db_column="buyerId", db_index=False)
    seller = models.ForeignKey("User", on_delete=models.RESTRICT, related_name="order_seller", db_column="sellerId", db_index=False)
    status = models.TextField(choices=OrderStatus.choices)
    message_group = models.ForeignKey("MessagesGroup", on_delete=models.RESTRICT, related_name="orders", db_column="messageGroupId", db_index=False)
    capture_id = models.TextField(null=True, db_column="captureId")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Orders"
        indexes = [
            models.Index(fields=["buyer"]),
            models.Index(fields=["seller"]),
            models.Index(fields=["updated_at"]),
            models.Index(fields=["created_at"]),
            models.Index(fields=["message_group"]),
        ]


class OrderItems(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    order = models.ForeignKey("Orders", on_delete=models.RESTRICT, related_name="orders_items", db_column="orderId", db_index=False)
    integration = models.ForeignKey("Integration", on_delete=models.RESTRICT, related_name="order_items", db_column="integrationId", db_index=False)
    quantity = models.IntegerField()
    price = models.IntegerField()

    class Meta:
        db_table = "OrderItems"
        indexes = [
            models.Index(fields=["order"]),
            models.Index(fields=["integration"]),
        ]


class Messages(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    from_field = models.TextField(choices=From.choices, db_column="from")
    content = models.TextField(null=True)
    group = models.ForeignKey("MessagesGroup", on_delete=models.RESTRICT, related_name="messages", db_column="groupId", db_index=False)
    special = models.TextField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    deleted_at = UtcDateTimeField(null=True, db_column="deletedAt")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Messages"
        indexes = [
            models.Index(fields=["group"]),
            models.Index(fields=["created_at"]),
            models.Index(fields=["deleted_at"]),
        ]


class Plugs(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    organization = models.ForeignKey("Organization", on_delete=models.RESTRICT, related_name="plugs", db_column="organizationId", db_index=False)
    plug_function = models.TextField(db_column="plugFunction")
    data = models.TextField()
    integration = models.ForeignKey("Integration", on_delete=models.RESTRICT, related_name="plugs", db_column="integrationId", db_index=False)
    activated = models.BooleanField(default=True)

    class Meta:
        db_table = "Plugs"
        constraints = [
            models.UniqueConstraint(fields=["plug_function", "integration"], name="Plugs_plugFunction_integrationId_key"),
        ]
        indexes = [
            models.Index(fields=["organization"]),
        ]


class ExisingPlugData(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    integration = models.ForeignKey("Integration", on_delete=models.RESTRICT, related_name="exising_plug_data", db_column="integrationId", db_index=False)
    method_name = models.TextField(db_column="methodName")
    value = models.TextField()

    class Meta:
        db_table = "ExisingPlugData"
        constraints = [
            models.UniqueConstraint(fields=["integration", "method_name", "value"], name="ExisingPlugData_integrationId_methodName_value_key"),
        ]


class PopularPosts(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    category = models.TextField()
    topic = models.TextField()
    content = models.TextField()
    hook = models.TextField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "PopularPosts"


class IntegrationsWebhooks(models.Model):
    pk = models.CompositePrimaryKey("integration_id", "webhook_id")
    integration = models.ForeignKey("Integration", on_delete=models.RESTRICT, related_name="webhooks", db_column="integrationId", db_index=False)
    webhook = models.ForeignKey("Webhooks", on_delete=models.RESTRICT, related_name="integrations", db_column="webhookId", db_index=False)

    class Meta:
        db_table = "IntegrationsWebhooks"
        constraints = [
            models.UniqueConstraint(fields=["integration", "webhook"], name="IntegrationsWebhooks_integrationId_webhookId_key"),
        ]
        indexes = [
            models.Index(fields=["integration"]),
            models.Index(fields=["webhook"]),
        ]


class Webhooks(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    name = models.TextField()
    organization = models.ForeignKey("Organization", on_delete=models.RESTRICT, related_name="webhooks", db_column="organizationId", db_index=False)
    url = models.TextField()
    deleted_at = UtcDateTimeField(null=True, db_column="deletedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Webhooks"
        indexes = [
            models.Index(fields=["organization"]),
            models.Index(fields=["deleted_at"]),
        ]


class AutoPost(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    organization = models.ForeignKey("Organization", on_delete=models.RESTRICT, related_name="auto_post", db_column="organizationId", db_index=False)
    title = models.TextField()
    content = models.TextField(null=True)
    on_slot = models.BooleanField(db_column="onSlot")
    sync_last = models.BooleanField(db_column="syncLast")
    url = models.TextField()
    last_url = models.TextField(db_column="lastUrl")
    active = models.BooleanField()
    add_picture = models.BooleanField(db_column="addPicture")
    generate_content = models.BooleanField(db_column="generateContent")
    integrations = models.TextField()
    deleted_at = UtcDateTimeField(null=True, db_column="deletedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "AutoPost"
        indexes = [
            models.Index(fields=["deleted_at"]),
        ]


class Sets(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    organization = models.ForeignKey("Organization", on_delete=models.RESTRICT, related_name="sets", db_column="organizationId", db_index=False)
    name = models.TextField()
    content = models.TextField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Sets"
        indexes = [
            models.Index(fields=["organization"]),
        ]


class ThirdParty(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    organization = models.ForeignKey("Organization", on_delete=models.RESTRICT, related_name="third_party", db_column="organizationId", db_index=False)
    identifier = models.TextField()
    name = models.TextField()
    internal_id = models.TextField(db_column="internalId")
    api_key = models.TextField(db_column="apiKey")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    deleted_at = UtcDateTimeField(null=True, db_column="deletedAt")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ThirdParty"
        constraints = [
            models.UniqueConstraint(fields=["organization", "internal_id"], name="ThirdParty_organizationId_internalId_key"),
        ]
        indexes = [
            models.Index(fields=["organization"]),
            models.Index(fields=["deleted_at"]),
        ]


class Errors(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    message = models.TextField()
    platform = models.TextField()
    organization = models.ForeignKey("Organization", on_delete=models.RESTRICT, related_name="errors", db_column="organizationId", db_index=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    post = models.ForeignKey("Post", on_delete=models.RESTRICT, related_name="errors", db_column="postId", db_index=False)
    body = models.TextField(default="{}")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Errors"
        indexes = [
            models.Index(fields=["organization"]),
            models.Index(fields=["created_at"]),
            models.Index(fields=["created_at", "platform"]),
        ]


class Mentions(models.Model):
    pk = models.CompositePrimaryKey("name", "username", "platform", "image")
    name = models.TextField()
    username = models.TextField()
    platform = models.TextField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    image = models.TextField()
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Mentions"
        indexes = [
            models.Index(fields=["created_at"]),
        ]


class MastraMessages(models.Model):
    id = models.TextField(primary_key=True, default=None)
    thread_id = models.TextField()
    content = models.TextField()
    role = models.TextField()
    type = models.TextField()
    created_at = UtcDateTimeField(db_column="createdAt")
    resource_id = models.TextField(null=True, db_column="resourceId")
    created_at_z = models.DateTimeField(null=True, db_column="createdAtZ", default=timezone.now)

    class Meta:
        db_table = "mastra_messages"
        indexes = [
            models.Index(fields=["thread_id", "-created_at"]),
        ]


class MastraResources(models.Model):
    id = models.TextField(primary_key=True, default=None)
    working_memory = models.TextField(null=True, db_column="workingMemory")
    metadata = models.JSONField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt")
    updated_at = UtcDateTimeField(db_column="updatedAt")
    created_at_z = models.DateTimeField(null=True, db_column="createdAtZ", default=timezone.now)
    updated_at_z = models.DateTimeField(null=True, db_column="updatedAtZ", default=timezone.now)

    class Meta:
        db_table = "mastra_resources"


class MastraScorers(models.Model):
    id = models.TextField(primary_key=True, default=None)
    scorer_id = models.TextField(db_column="scorerId")
    trace_id = models.TextField(null=True, db_column="traceId")
    run_id = models.TextField(db_column="runId")
    scorer = models.JSONField()
    preprocess_step_result = models.JSONField(null=True, db_column="preprocessStepResult")
    extract_step_result = models.JSONField(null=True, db_column="extractStepResult")
    analyze_step_result = models.JSONField(null=True, db_column="analyzeStepResult")
    score = models.FloatField()
    reason = models.TextField(null=True)
    metadata = models.JSONField(null=True)
    preprocess_prompt = models.TextField(null=True, db_column="preprocessPrompt")
    extract_prompt = models.TextField(null=True, db_column="extractPrompt")
    generate_score_prompt = models.TextField(null=True, db_column="generateScorePrompt")
    generate_reason_prompt = models.TextField(null=True, db_column="generateReasonPrompt")
    analyze_prompt = models.TextField(null=True, db_column="analyzePrompt")
    reason_prompt = models.TextField(null=True, db_column="reasonPrompt")
    input = models.JSONField()
    output = models.JSONField()
    additional_context = models.JSONField(null=True, db_column="additionalContext")
    runtime_context = models.JSONField(null=True, db_column="runtimeContext")
    entity_type = models.TextField(null=True, db_column="entityType")
    entity = models.JSONField(null=True)
    entity_id = models.TextField(null=True, db_column="entityId")
    source = models.TextField()
    resource_id = models.TextField(null=True, db_column="resourceId")
    thread_id = models.TextField(null=True, db_column="threadId")
    created_at = UtcDateTimeField(db_column="createdAt")
    updated_at = UtcDateTimeField(db_column="updatedAt")
    created_at_z = models.DateTimeField(null=True, db_column="createdAtZ", default=timezone.now)
    updated_at_z = models.DateTimeField(null=True, db_column="updatedAtZ", default=timezone.now)
    span_id = models.TextField(null=True, db_column="spanId")
    request_context = models.JSONField(null=True, db_column="requestContext")
    organization_id = models.TextField(null=True, db_column="organizationId")
    project_id = models.TextField(null=True, db_column="projectId")
    batch_id = models.TextField(null=True, db_column="batchId")
    dataset_id = models.TextField(null=True, db_column="datasetId")
    dataset_item_id = models.TextField(null=True, db_column="datasetItemId")

    class Meta:
        db_table = "mastra_scorers"
        indexes = [
            models.Index(fields=["trace_id", "span_id", "-created_at"]),
        ]


class MastraThreads(models.Model):
    id = models.TextField(primary_key=True, default=None)
    resource_id = models.TextField(db_column="resourceId")
    title = models.TextField()
    metadata = models.TextField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt")
    updated_at = UtcDateTimeField(db_column="updatedAt")
    created_at_z = models.DateTimeField(null=True, db_column="createdAtZ", default=timezone.now)
    updated_at_z = models.DateTimeField(null=True, db_column="updatedAtZ", default=timezone.now)

    class Meta:
        db_table = "mastra_threads"
        indexes = [
            models.Index(fields=["resource_id", "-created_at"]),
        ]


class MastraTraces(models.Model):
    id = models.TextField(primary_key=True, default=None)
    parent_span_id = models.TextField(null=True, db_column="parentSpanId")
    name = models.TextField()
    trace_id = models.TextField(db_column="traceId")
    scope = models.TextField()
    kind = models.IntegerField()
    attributes = models.JSONField(null=True)
    status = models.JSONField(null=True)
    events = models.JSONField(null=True)
    links = models.JSONField(null=True)
    other = models.TextField(null=True)
    start_time = models.BigIntegerField(db_column="startTime")
    end_time = models.BigIntegerField(db_column="endTime")
    created_at = UtcDateTimeField(db_column="createdAt")
    created_at_z = models.DateTimeField(null=True, db_column="createdAtZ", default=timezone.now)

    class Meta:
        db_table = "mastra_traces"
        indexes = [
            models.Index(fields=["name", "-start_time"]),
        ]


class MastraWorkflowSnapshot(models.Model):
    pk = models.CompositePrimaryKey("workflow_name", "run_id")
    workflow_name = models.TextField()
    run_id = models.TextField()
    resource_id = models.TextField(null=True, db_column="resourceId")
    snapshot = models.TextField()
    created_at = UtcDateTimeField(db_column="createdAt")
    updated_at = UtcDateTimeField(db_column="updatedAt")
    created_at_z = models.DateTimeField(null=True, db_column="createdAtZ", default=timezone.now)
    updated_at_z = models.DateTimeField(null=True, db_column="updatedAtZ", default=timezone.now)

    class Meta:
        db_table = "mastra_workflow_snapshot"
        constraints = [
            models.UniqueConstraint(fields=["workflow_name", "run_id"], name="public_mastra_workflow_snapshot_workflow_name_run_id_key"),
        ]
        indexes = [
            models.Index(fields=["workflow_name", "-created_at"]),
        ]


class MastraAgentVersions(models.Model):
    id = models.TextField(primary_key=True, default=None)
    agent_id = models.TextField(db_column="agentId")
    version_number = models.IntegerField(db_column="versionNumber")
    name = models.TextField()
    description = models.TextField(null=True)
    instructions = models.TextField()
    model = models.JSONField()
    tools = models.JSONField(null=True)
    default_options = models.JSONField(null=True, db_column="defaultOptions")
    workflows = models.JSONField(null=True)
    agents = models.JSONField(null=True)
    integration_tools = models.JSONField(null=True, db_column="integrationTools")
    input_processors = models.JSONField(null=True, db_column="inputProcessors")
    output_processors = models.JSONField(null=True, db_column="outputProcessors")
    memory = models.JSONField(null=True)
    scorers = models.JSONField(null=True)
    mcp_clients = models.JSONField(null=True, db_column="mcpClients")
    request_context_schema = models.JSONField(null=True, db_column="requestContextSchema")
    workspace = models.JSONField(null=True)
    skills = models.JSONField(null=True)
    skills_format = models.TextField(null=True, db_column="skillsFormat")
    changed_fields = models.JSONField(null=True, db_column="changedFields")
    change_message = models.TextField(null=True, db_column="changeMessage")
    created_at = UtcDateTimeField(db_column="createdAt")
    created_at_z = models.DateTimeField(null=True, db_column="createdAtZ", default=timezone.now)
    durable = models.JSONField(null=True)
    browser = models.JSONField(null=True)
    tool_providers = models.JSONField(null=True, db_column="toolProviders")

    class Meta:
        db_table = "mastra_agent_versions"


class MastraAgents(models.Model):
    id = models.TextField(primary_key=True, default=None)
    status = models.TextField()
    active_version_id = models.TextField(null=True, db_column="activeVersionId")
    author_id = models.TextField(null=True, db_column="authorId")
    metadata = models.JSONField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt")
    updated_at = UtcDateTimeField(db_column="updatedAt")
    created_at_z = models.DateTimeField(null=True, db_column="createdAtZ", default=timezone.now)
    updated_at_z = models.DateTimeField(null=True, db_column="updatedAtZ", default=timezone.now)
    visibility = models.TextField(null=True)
    favorite_count = models.IntegerField(null=True, db_column="favoriteCount")

    class Meta:
        db_table = "mastra_agents"


class MastraBackgroundTasks(models.Model):
    id = models.TextField(primary_key=True, default=None)
    tool_call_id = models.TextField()
    tool_name = models.TextField()
    agent_id = models.TextField()
    run_id = models.TextField()
    thread_id = models.TextField(null=True)
    resource_id = models.TextField(null=True)
    status = models.TextField()
    args = models.JSONField()
    result = models.JSONField(null=True)
    error = models.JSONField(null=True)
    suspend_payload = models.JSONField(null=True)
    retry_count = models.IntegerField()
    max_retries = models.IntegerField()
    timeout_ms = models.IntegerField()
    created_at = UtcDateTimeField(db_column="createdAt")
    started_at = UtcDateTimeField(null=True, db_column="startedAt")
    suspended_at = UtcDateTimeField(null=True, db_column="suspendedAt")
    completed_at = UtcDateTimeField(null=True, db_column="completedAt")
    created_at_z = models.DateTimeField(null=True, db_column="createdAtZ", default=timezone.now)
    started_at_z = models.DateTimeField(null=True, db_column="startedAtZ", default=timezone.now)
    suspended_at_z = models.DateTimeField(null=True, db_column="suspendedAtZ", default=timezone.now)
    completed_at_z = models.DateTimeField(null=True, db_column="completedAtZ", default=timezone.now)

    class Meta:
        db_table = "mastra_background_tasks"
        indexes = [
            models.Index(fields=["agent_id", "status"]),
            models.Index(fields=["status", "created_at"]),
            models.Index(fields=["thread_id", "created_at"], name="mastra_bg_tasks_thread_idx"),
            models.Index(fields=["tool_call_id"], name="mastra_bg_tasks_tool_call_idx"),
        ]


class MastraChannelConfig(models.Model):
    platform = models.TextField(primary_key=True, default=None)
    data = models.JSONField()
    updated_at = UtcDateTimeField(db_column="updatedAt")
    updated_at_z = models.DateTimeField(null=True, db_column="updatedAtZ", default=timezone.now)

    class Meta:
        db_table = "mastra_channel_config"


class MastraChannelInstallations(models.Model):
    id = models.TextField(primary_key=True, default=None)
    platform = models.TextField()
    agent_id = models.TextField(db_column="agentId")
    status = models.TextField()
    webhook_id = models.TextField(null=True, unique=True, db_column="webhookId")
    data = models.JSONField()
    config_hash = models.TextField(null=True, db_column="configHash")
    error = models.TextField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt")
    updated_at = UtcDateTimeField(db_column="updatedAt")
    created_at_z = models.DateTimeField(null=True, db_column="createdAtZ", default=timezone.now)
    updated_at_z = models.DateTimeField(null=True, db_column="updatedAtZ", default=timezone.now)

    class Meta:
        db_table = "mastra_channel_installations"
        indexes = [
            models.Index(fields=["platform", "agent_id"]),
        ]


class MastraDatasetItems(models.Model):
    pk = models.CompositePrimaryKey("id", "dataset_version")
    id = models.TextField()
    dataset_id = models.TextField(db_column="datasetId")
    dataset_version = models.IntegerField(db_column="datasetVersion")
    valid_to = models.IntegerField(null=True, db_column="validTo")
    is_deleted = models.BooleanField(db_column="isDeleted")
    input = models.JSONField()
    ground_truth = models.JSONField(null=True, db_column="groundTruth")
    request_context = models.JSONField(null=True, db_column="requestContext")
    metadata = models.JSONField(null=True)
    source = models.JSONField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt")
    updated_at = UtcDateTimeField(db_column="updatedAt")
    created_at_z = models.DateTimeField(null=True, db_column="createdAtZ", default=timezone.now)
    updated_at_z = models.DateTimeField(null=True, db_column="updatedAtZ", default=timezone.now)
    expected_trajectory = models.JSONField(null=True, db_column="expectedTrajectory")
    organization_id = models.TextField(null=True, db_column="organizationId")
    project_id = models.TextField(null=True, db_column="projectId")
    tool_mocks = models.JSONField(null=True, db_column="toolMocks")
    unmocked_tool_policy = models.TextField(null=True, db_column="unmockedToolPolicy")
    scorer_ids = models.JSONField(null=True, db_column="scorerIds")
    external_id = models.TextField(null=True, db_column="externalId")

    class Meta:
        db_table = "mastra_dataset_items"
        indexes = [
            models.Index(fields=["dataset_id", "valid_to"]),
            models.Index(fields=["dataset_id", "valid_to", "is_deleted"]),
            models.Index(fields=["dataset_id", "dataset_version"]),
            models.Index(fields=["dataset_id", "external_id", "dataset_version"]),
            models.Index(fields=["organization_id", "project_id"], name="idx_dataset_items_org_project"),
        ]


class MastraDatasetVersions(models.Model):
    id = models.TextField(primary_key=True, default=None)
    dataset_id = models.TextField(db_column="datasetId")
    version = models.IntegerField()
    created_at = UtcDateTimeField(db_column="createdAt")
    created_at_z = models.DateTimeField(null=True, db_column="createdAtZ", default=timezone.now)

    class Meta:
        db_table = "mastra_dataset_versions"
        constraints = [
            models.UniqueConstraint(fields=["dataset_id", "version"], name="idx_dataset_versions_dataset_version_unique"),
        ]
        indexes = [
            models.Index(fields=["dataset_id", "version"]),
        ]


class MastraDatasets(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    description = models.TextField(null=True)
    metadata = models.JSONField(null=True)
    input_schema = models.JSONField(null=True, db_column="inputSchema")
    ground_truth_schema = models.JSONField(null=True, db_column="groundTruthSchema")
    request_context_schema = models.JSONField(null=True, db_column="requestContextSchema")
    tags = models.JSONField(null=True)
    target_type = models.TextField(null=True, db_column="targetType")
    target_ids = models.JSONField(null=True, db_column="targetIds")
    scorer_ids = models.JSONField(null=True, db_column="scorerIds")
    version = models.IntegerField()
    created_at = UtcDateTimeField(db_column="createdAt")
    updated_at = UtcDateTimeField(db_column="updatedAt")
    created_at_z = models.DateTimeField(null=True, db_column="createdAtZ", default=timezone.now)
    updated_at_z = models.DateTimeField(null=True, db_column="updatedAtZ", default=timezone.now)
    organization_id = models.TextField(null=True, db_column="organizationId")
    project_id = models.TextField(null=True, db_column="projectId")
    candidate_key = models.TextField(null=True, db_column="candidateKey")
    candidate_id = models.TextField(null=True, db_column="candidateId")

    class Meta:
        db_table = "mastra_datasets"
        indexes = [
            models.Index(fields=["candidate_key", "candidate_id"], name="idx_datasets_candidate"),
            models.Index(fields=["organization_id", "project_id"], name="idx_datasets_org_project"),
        ]


class MastraExperimentResults(models.Model):
    id = models.TextField(primary_key=True, default=None)
    experiment_id = models.TextField(db_column="experimentId")
    item_id = models.TextField(db_column="itemId")
    item_dataset_version = models.IntegerField(null=True, db_column="itemDatasetVersion")
    input = models.JSONField()
    output = models.JSONField(null=True)
    ground_truth = models.JSONField(null=True, db_column="groundTruth")
    error = models.JSONField(null=True)
    started_at = UtcDateTimeField(db_column="startedAt")
    completed_at = UtcDateTimeField(db_column="completedAt")
    retry_count = models.IntegerField(db_column="retryCount")
    trace_id = models.TextField(null=True, db_column="traceId")
    status = models.TextField(null=True)
    tags = models.JSONField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt")
    started_at_z = models.DateTimeField(null=True, db_column="startedAtZ", default=timezone.now)
    completed_at_z = models.DateTimeField(null=True, db_column="completedAtZ", default=timezone.now)
    created_at_z = models.DateTimeField(null=True, db_column="createdAtZ", default=timezone.now)
    comment = models.TextField(null=True)
    tool_mock_report = models.JSONField(null=True, db_column="toolMockReport")
    metadata = models.JSONField(null=True)
    organization_id = models.TextField(null=True, db_column="organizationId")
    project_id = models.TextField(null=True, db_column="projectId")
    attempt = models.IntegerField(null=True)

    class Meta:
        db_table = "mastra_experiment_results"
        constraints = [
            models.UniqueConstraint(fields=["experiment_id", "item_id", "attempt"], name="idx_experiment_results_exp_item_attempt"),
        ]
        indexes = [
            models.Index(fields=["experiment_id"]),
            models.Index(fields=["organization_id", "project_id"]),
            GinIndex(fields=["tags"]),
        ]


class MastraExperiments(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField(null=True)
    description = models.TextField(null=True)
    metadata = models.JSONField(null=True)
    dataset_id = models.TextField(null=True, db_column="datasetId")
    dataset_version = models.IntegerField(null=True, db_column="datasetVersion")
    target_type = models.TextField(db_column="targetType")
    target_id = models.TextField(db_column="targetId")
    status = models.TextField()
    total_items = models.IntegerField(db_column="totalItems")
    succeeded_count = models.IntegerField(db_column="succeededCount")
    failed_count = models.IntegerField(db_column="failedCount")
    skipped_count = models.IntegerField(db_column="skippedCount")
    started_at = UtcDateTimeField(null=True, db_column="startedAt")
    completed_at = UtcDateTimeField(null=True, db_column="completedAt")
    agent_version = models.TextField(null=True, db_column="agentVersion")
    created_at = UtcDateTimeField(db_column="createdAt")
    updated_at = UtcDateTimeField(db_column="updatedAt")
    started_at_z = models.DateTimeField(null=True, db_column="startedAtZ", default=timezone.now)
    completed_at_z = models.DateTimeField(null=True, db_column="completedAtZ", default=timezone.now)
    created_at_z = models.DateTimeField(null=True, db_column="createdAtZ", default=timezone.now)
    updated_at_z = models.DateTimeField(null=True, db_column="updatedAtZ", default=timezone.now)
    organization_id = models.TextField(null=True, db_column="organizationId")
    project_id = models.TextField(null=True, db_column="projectId")
    provenance = models.JSONField(null=True)
    runner_attestation = models.JSONField(null=True, db_column="runnerAttestation")
    experiment_set_id = models.TextField(null=True, db_column="experimentSetId")
    comparison_id = models.TextField(null=True, db_column="comparisonId")
    variant_id = models.TextField(null=True, db_column="variantId")
    trial_index = models.IntegerField(null=True, db_column="trialIndex")
    scorer_ids = models.JSONField(null=True, db_column="scorerIds")

    class Meta:
        db_table = "mastra_experiments"
        indexes = [
            models.Index(fields=["dataset_id"], name="idx_experiments_datasetid"),
            models.Index(fields=["experiment_set_id", "comparison_id", "variant_id", "trial_index"], name="idx_experiments_grouping"),
            models.Index(fields=["organization_id", "project_id"], name="idx_experiments_org_project"),
        ]


class MastraFavorites(models.Model):
    pk = models.CompositePrimaryKey("user_id", "entity_type", "entity_id")
    user_id = models.TextField(db_column="userId")
    entity_type = models.TextField(db_column="entityType")
    entity_id = models.TextField(db_column="entityId")
    created_at = UtcDateTimeField(db_column="createdAt")
    created_at_z = models.DateTimeField(null=True, db_column="createdAtZ", default=timezone.now)

    class Meta:
        db_table = "mastra_favorites"
        indexes = [
            models.Index(fields=["entity_type", "entity_id"], name="idx_favorites_entity"),
        ]


class MastraKnowledgeActivity(models.Model):
    id = models.TextField(primary_key=True, default=None)
    action = models.TextField()
    record_type = models.TextField(db_column="recordType")
    record_id = models.TextField(db_column="recordId")
    scope = models.JSONField()
    scope_key = models.TextField(db_column="scopeKey")
    source_thread_id = models.TextField(null=True, db_column="sourceThreadId")
    created_at = UtcDateTimeField(db_column="createdAt")
    created_at_z = models.DateTimeField(null=True, db_column="createdAtZ", default=timezone.now)

    class Meta:
        db_table = "mastra_knowledge_activity"
        indexes = [
            models.Index(fields=["-id"], name="idx_knowledge_activity_latest"),
        ]


class MastraKnowledgeCursors(models.Model):
    pk = models.CompositePrimaryKey("source_thread_id", "agent")
    source_thread_id = models.TextField(db_column="sourceThreadId")
    agent = models.TextField()
    last_knowledge_id = models.TextField(db_column="lastKnowledgeId")
    updated_at = UtcDateTimeField(db_column="updatedAt")
    updated_at_z = models.DateTimeField(null=True, db_column="updatedAtZ", default=timezone.now)

    class Meta:
        db_table = "mastra_knowledge_cursors"


class MastraKnowledgeMentions(models.Model):
    pk = models.CompositePrimaryKey("source_type", "source_id", "record_id")
    source_type = models.TextField(db_column="sourceType")
    source_id = models.TextField(db_column="sourceId")
    record_id = models.TextField(db_column="recordId")

    class Meta:
        db_table = "mastra_knowledge_mentions"
        indexes = [
            models.Index(fields=["record_id", "source_type", "source_id"], name="idx_knowledge_mentions_record"),
        ]


class MastraKnowledgeNodes(models.Model):
    id = models.TextField(primary_key=True, default=None)
    type = models.TextField()
    name = models.TextField()
    canonical_name = models.TextField(db_column="canonicalName")
    kind = models.TextField(null=True)
    content = models.TextField(null=True)
    description = models.TextField(null=True)
    scope = models.JSONField()
    scope_key = models.TextField(db_column="scopeKey")
    version = models.IntegerField()
    merged_into = models.TextField(null=True, db_column="mergedInto")
    created_at = UtcDateTimeField(db_column="createdAt")
    updated_at = UtcDateTimeField(db_column="updatedAt")
    created_at_z = models.DateTimeField(null=True, db_column="createdAtZ", default=timezone.now)
    updated_at_z = models.DateTimeField(null=True, db_column="updatedAtZ", default=timezone.now)

    class Meta:
        db_table = "mastra_knowledge_nodes"
        constraints = [
            models.UniqueConstraint(fields=["type", "scope_key", "canonical_name"], name="idx_knowledge_nodes_identity"),
        ]
        indexes = [
            models.Index(fields=["scope_key", "type"], name="idx_knowledge_nodes_scope"),
        ]


class MastraKnowledgeRecords(models.Model):
    id = models.TextField(primary_key=True, default=None)
    node = models.TextField()
    text = models.TextField()
    scope = models.JSONField()
    scope_key = models.TextField(db_column="scopeKey")
    source_thread_id = models.TextField(db_column="sourceThreadId")
    captured_at = UtcDateTimeField(db_column="capturedAt")
    when = UtcDateTimeField(null=True)
    max_scope = models.TextField(null=True, db_column="maxScope")
    metadata = models.JSONField(null=True)
    deleted_at = UtcDateTimeField(null=True, db_column="deletedAt")
    deleted_by = models.TextField(null=True, db_column="deletedBy")
    captured_at_z = models.DateTimeField(null=True, db_column="capturedAtZ", default=timezone.now)
    when_z = models.DateTimeField(null=True, db_column="whenZ", default=timezone.now)
    deleted_at_z = models.DateTimeField(null=True, db_column="deletedAtZ", default=timezone.now)

    class Meta:
        db_table = "mastra_knowledge_records"
        indexes = [
            models.Index(fields=["node", "-id"]),
            models.Index(fields=["source_thread_id", "-id"]),
        ]


class MastraKnowledgeSemanticOutbox(models.Model):
    id = models.TextField(primary_key=True, default=None)
    idempotency_key = models.TextField(unique=True, db_column="idempotencyKey")
    document_id = models.TextField(db_column="documentId")
    document_type = models.TextField(db_column="documentType")
    operation = models.TextField()
    scope = models.JSONField()
    scope_key = models.TextField(db_column="scopeKey")
    status = models.TextField()
    attempts = models.IntegerField()
    available_at = UtcDateTimeField(db_column="availableAt")
    claimed_at = UtcDateTimeField(null=True, db_column="claimedAt")
    claimed_by = models.TextField(null=True, db_column="claimedBy")
    created_at = UtcDateTimeField(db_column="createdAt")
    completed_at = UtcDateTimeField(null=True, db_column="completedAt")
    available_at_z = models.DateTimeField(null=True, db_column="availableAtZ", default=timezone.now)
    claimed_at_z = models.DateTimeField(null=True, db_column="claimedAtZ", default=timezone.now)
    created_at_z = models.DateTimeField(null=True, db_column="createdAtZ", default=timezone.now)
    completed_at_z = models.DateTimeField(null=True, db_column="completedAtZ", default=timezone.now)

    class Meta:
        db_table = "mastra_knowledge_semantic_outbox"
        indexes = [
            models.Index(fields=["status", "available_at", "created_at"], name="idx_knowledge_outbox_claim"),
        ]


class MastraMcpClientVersions(models.Model):
    id = models.TextField(primary_key=True, default=None)
    mcp_client_id = models.TextField(db_column="mcpClientId")
    version_number = models.IntegerField(db_column="versionNumber")
    name = models.TextField()
    description = models.TextField(null=True)
    servers = models.JSONField()
    changed_fields = models.JSONField(null=True, db_column="changedFields")
    change_message = models.TextField(null=True, db_column="changeMessage")
    created_at = UtcDateTimeField(db_column="createdAt")
    created_at_z = models.DateTimeField(null=True, db_column="createdAtZ", default=timezone.now)

    class Meta:
        db_table = "mastra_mcp_client_versions"
        constraints = [
            models.UniqueConstraint(fields=["mcp_client_id", "version_number"], name="idx_mcp_client_versions_client_version"),
        ]


class MastraMcpClients(models.Model):
    id = models.TextField(primary_key=True, default=None)
    status = models.TextField()
    active_version_id = models.TextField(null=True, db_column="activeVersionId")
    author_id = models.TextField(null=True, db_column="authorId")
    metadata = models.JSONField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt")
    updated_at = UtcDateTimeField(db_column="updatedAt")
    created_at_z = models.DateTimeField(null=True, db_column="createdAtZ", default=timezone.now)
    updated_at_z = models.DateTimeField(null=True, db_column="updatedAtZ", default=timezone.now)

    class Meta:
        db_table = "mastra_mcp_clients"


class MastraMcpServerVersions(models.Model):
    id = models.TextField(primary_key=True, default=None)
    mcp_server_id = models.TextField(db_column="mcpServerId")
    version_number = models.IntegerField(db_column="versionNumber")
    name = models.TextField()
    version = models.TextField()
    description = models.TextField(null=True)
    instructions = models.TextField(null=True)
    repository = models.JSONField(null=True)
    release_date = models.TextField(null=True, db_column="releaseDate")
    is_latest = models.BooleanField(null=True, db_column="isLatest")
    package_canonical = models.TextField(null=True, db_column="packageCanonical")
    tools = models.JSONField(null=True)
    agents = models.JSONField(null=True)
    workflows = models.JSONField(null=True)
    changed_fields = models.JSONField(null=True, db_column="changedFields")
    change_message = models.TextField(null=True, db_column="changeMessage")
    created_at = UtcDateTimeField(db_column="createdAt")
    created_at_z = models.DateTimeField(null=True, db_column="createdAtZ", default=timezone.now)

    class Meta:
        db_table = "mastra_mcp_server_versions"
        constraints = [
            models.UniqueConstraint(fields=["mcp_server_id", "version_number"], name="idx_mcp_server_versions_server_version"),
        ]


class MastraMcpServers(models.Model):
    id = models.TextField(primary_key=True, default=None)
    status = models.TextField()
    active_version_id = models.TextField(null=True, db_column="activeVersionId")
    author_id = models.TextField(null=True, db_column="authorId")
    metadata = models.JSONField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt")
    updated_at = UtcDateTimeField(db_column="updatedAt")
    created_at_z = models.DateTimeField(null=True, db_column="createdAtZ", default=timezone.now)
    updated_at_z = models.DateTimeField(null=True, db_column="updatedAtZ", default=timezone.now)

    class Meta:
        db_table = "mastra_mcp_servers"


class MastraObservationalMemory(models.Model):
    id = models.TextField(primary_key=True, default=None)
    lookup_key = models.TextField(db_column="lookupKey")
    scope = models.TextField()
    resource_id = models.TextField(null=True, db_column="resourceId")
    thread_id = models.TextField(null=True, db_column="threadId")
    active_observations = models.TextField(db_column="activeObservations")
    active_observations_pending_update = models.TextField(null=True, db_column="activeObservationsPendingUpdate")
    origin_type = models.TextField(db_column="originType")
    config = models.TextField()
    generation_count = models.IntegerField(db_column="generationCount")
    last_observed_at = UtcDateTimeField(null=True, db_column="lastObservedAt")
    last_reflection_at = UtcDateTimeField(null=True, db_column="lastReflectionAt")
    pending_message_tokens = models.IntegerField(db_column="pendingMessageTokens")
    total_tokens_observed = models.IntegerField(db_column="totalTokensObserved")
    observation_token_count = models.IntegerField(db_column="observationTokenCount")
    is_observing = models.BooleanField(db_column="isObserving")
    is_reflecting = models.BooleanField(db_column="isReflecting")
    observed_message_ids = models.JSONField(null=True, db_column="observedMessageIds")
    observed_timezone = models.TextField(null=True, db_column="observedTimezone")
    buffered_observations = models.TextField(null=True, db_column="bufferedObservations")
    buffered_observation_tokens = models.IntegerField(null=True, db_column="bufferedObservationTokens")
    buffered_message_ids = models.JSONField(null=True, db_column="bufferedMessageIds")
    buffered_reflection = models.TextField(null=True, db_column="bufferedReflection")
    buffered_reflection_tokens = models.IntegerField(null=True, db_column="bufferedReflectionTokens")
    buffered_reflection_input_tokens = models.IntegerField(null=True, db_column="bufferedReflectionInputTokens")
    reflected_observation_line_count = models.IntegerField(null=True, db_column="reflectedObservationLineCount")
    buffered_observation_chunks = models.JSONField(null=True, db_column="bufferedObservationChunks")
    is_buffering_observation = models.BooleanField(db_column="isBufferingObservation")
    is_buffering_reflection = models.BooleanField(db_column="isBufferingReflection")
    last_buffered_at_tokens = models.IntegerField(db_column="lastBufferedAtTokens")
    last_buffered_at_time = UtcDateTimeField(null=True, db_column="lastBufferedAtTime")
    metadata = models.JSONField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt")
    updated_at = UtcDateTimeField(db_column="updatedAt")
    last_observed_at_z = models.DateTimeField(null=True, db_column="lastObservedAtZ", default=timezone.now)
    last_reflection_at_z = models.DateTimeField(null=True, db_column="lastReflectionAtZ", default=timezone.now)
    last_buffered_at_time_z = models.DateTimeField(null=True, db_column="lastBufferedAtTimeZ", default=timezone.now)
    created_at_z = models.DateTimeField(null=True, db_column="createdAtZ", default=timezone.now)
    updated_at_z = models.DateTimeField(null=True, db_column="updatedAtZ", default=timezone.now)

    class Meta:
        db_table = "mastra_observational_memory"
        indexes = [
            models.Index(fields=["lookup_key"], name="idx_om_lookup_key"),
        ]


class MastraPromptBlockVersions(models.Model):
    id = models.TextField(primary_key=True, default=None)
    block_id = models.TextField(db_column="blockId")
    version_number = models.IntegerField(db_column="versionNumber")
    name = models.TextField()
    description = models.TextField(null=True)
    content = models.TextField()
    rules = models.JSONField(null=True)
    request_context_schema = models.JSONField(null=True, db_column="requestContextSchema")
    changed_fields = models.JSONField(null=True, db_column="changedFields")
    change_message = models.TextField(null=True, db_column="changeMessage")
    created_at = UtcDateTimeField(db_column="createdAt")
    created_at_z = models.DateTimeField(null=True, db_column="createdAtZ", default=timezone.now)

    class Meta:
        db_table = "mastra_prompt_block_versions"
        constraints = [
            models.UniqueConstraint(fields=["block_id", "version_number"], name="idx_prompt_block_versions_block_version"),
        ]


class MastraPromptBlocks(models.Model):
    id = models.TextField(primary_key=True, default=None)
    status = models.TextField()
    active_version_id = models.TextField(null=True, db_column="activeVersionId")
    author_id = models.TextField(null=True, db_column="authorId")
    metadata = models.JSONField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt")
    updated_at = UtcDateTimeField(db_column="updatedAt")
    created_at_z = models.DateTimeField(null=True, db_column="createdAtZ", default=timezone.now)
    updated_at_z = models.DateTimeField(null=True, db_column="updatedAtZ", default=timezone.now)

    class Meta:
        db_table = "mastra_prompt_blocks"


class MastraScheduleTriggers(models.Model):
    id = models.TextField(primary_key=True, default=None)
    schedule_id = models.TextField()
    run_id = models.TextField(null=True)
    scheduled_fire_at = models.BigIntegerField()
    actual_fire_at = models.BigIntegerField()
    outcome = models.TextField()
    error = models.TextField(null=True)
    trigger_kind = models.TextField()
    parent_trigger_id = models.TextField(null=True)
    metadata = models.JSONField(null=True)

    class Meta:
        db_table = "mastra_schedule_triggers"
        indexes = [
            models.Index(fields=["schedule_id", "-actual_fire_at"]),
        ]


class MastraSchedules(models.Model):
    id = models.TextField(primary_key=True, default=None)
    target = models.JSONField()
    cron = models.TextField()
    timezone = models.TextField(null=True)
    status = models.TextField()
    next_fire_at = models.BigIntegerField()
    last_fire_at = models.BigIntegerField(null=True)
    last_run_id = models.TextField(null=True)
    created_at = models.BigIntegerField()
    updated_at = models.BigIntegerField()
    metadata = models.JSONField(null=True)
    owner_type = models.TextField(null=True)
    owner_id = models.TextField(null=True)

    class Meta:
        db_table = "mastra_schedules"
        indexes = [
            models.Index(fields=["status", "next_fire_at"]),
        ]


class MastraScorerDefinitionVersions(models.Model):
    id = models.TextField(primary_key=True, default=None)
    scorer_definition_id = models.TextField(db_column="scorerDefinitionId")
    version_number = models.IntegerField(db_column="versionNumber")
    name = models.TextField()
    description = models.TextField(null=True)
    type = models.TextField()
    model = models.JSONField(null=True)
    instructions = models.TextField(null=True)
    score_range = models.JSONField(null=True, db_column="scoreRange")
    preset_config = models.JSONField(null=True, db_column="presetConfig")
    default_sampling = models.JSONField(null=True, db_column="defaultSampling")
    changed_fields = models.JSONField(null=True, db_column="changedFields")
    change_message = models.TextField(null=True, db_column="changeMessage")
    created_at = UtcDateTimeField(db_column="createdAt")
    created_at_z = models.DateTimeField(null=True, db_column="createdAtZ", default=timezone.now)

    class Meta:
        db_table = "mastra_scorer_definition_versions"
        constraints = [
            models.UniqueConstraint(fields=["scorer_definition_id", "version_number"], name="idx_scorer_definition_versions_def_version"),
        ]


class MastraScorerDefinitions(models.Model):
    id = models.TextField(primary_key=True, default=None)
    status = models.TextField()
    active_version_id = models.TextField(null=True, db_column="activeVersionId")
    author_id = models.TextField(null=True, db_column="authorId")
    metadata = models.JSONField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt")
    updated_at = UtcDateTimeField(db_column="updatedAt")
    created_at_z = models.DateTimeField(null=True, db_column="createdAtZ", default=timezone.now)
    updated_at_z = models.DateTimeField(null=True, db_column="updatedAtZ", default=timezone.now)
    organization_id = models.TextField(null=True, db_column="organizationId")
    project_id = models.TextField(null=True, db_column="projectId")

    class Meta:
        db_table = "mastra_scorer_definitions"


class MastraSkillBlobs(models.Model):
    hash = models.TextField(primary_key=True, default=None)
    content = models.TextField()
    size = models.IntegerField()
    mime_type = models.TextField(null=True, db_column="mimeType")
    created_at = UtcDateTimeField(db_column="createdAt")
    created_at_z = models.DateTimeField(null=True, db_column="createdAtZ", default=timezone.now)

    class Meta:
        db_table = "mastra_skill_blobs"


class MastraSkillVersions(models.Model):
    id = models.TextField(primary_key=True, default=None)
    skill_id = models.TextField(db_column="skillId")
    version_number = models.IntegerField(db_column="versionNumber")
    name = models.TextField()
    description = models.TextField()
    instructions = models.TextField()
    license = models.TextField(null=True)
    compatibility = models.JSONField(null=True)
    source = models.JSONField(null=True)
    references = models.JSONField(null=True)
    scripts = models.JSONField(null=True)
    assets = models.JSONField(null=True)
    metadata = models.JSONField(null=True)
    tree = models.JSONField(null=True)
    changed_fields = models.JSONField(null=True, db_column="changedFields")
    change_message = models.TextField(null=True, db_column="changeMessage")
    created_at = UtcDateTimeField(db_column="createdAt")
    created_at_z = models.DateTimeField(null=True, db_column="createdAtZ", default=timezone.now)
    files = models.JSONField(null=True)

    class Meta:
        db_table = "mastra_skill_versions"
        constraints = [
            models.UniqueConstraint(fields=["skill_id", "version_number"], name="idx_skill_versions_skill_version"),
        ]


class MastraSkills(models.Model):
    id = models.TextField(primary_key=True, default=None)
    status = models.TextField()
    active_version_id = models.TextField(null=True, db_column="activeVersionId")
    author_id = models.TextField(null=True, db_column="authorId")
    created_at = UtcDateTimeField(db_column="createdAt")
    updated_at = UtcDateTimeField(db_column="updatedAt")
    created_at_z = models.DateTimeField(null=True, db_column="createdAtZ", default=timezone.now)
    updated_at_z = models.DateTimeField(null=True, db_column="updatedAtZ", default=timezone.now)
    visibility = models.TextField(null=True)
    favorite_count = models.IntegerField(null=True, db_column="favoriteCount")

    class Meta:
        db_table = "mastra_skills"


class MastraThreadState(models.Model):
    pk = models.CompositePrimaryKey("thread_id", "type")
    thread_id = models.TextField(db_column="threadId")
    type = models.TextField()
    value = models.JSONField()
    created_at = UtcDateTimeField(db_column="createdAt")
    updated_at = UtcDateTimeField(db_column="updatedAt")
    created_at_z = models.DateTimeField(null=True, db_column="createdAtZ", default=timezone.now)
    updated_at_z = models.DateTimeField(null=True, db_column="updatedAtZ", default=timezone.now)

    class Meta:
        db_table = "mastra_thread_state"


class MastraToolProviderConnections(models.Model):
    pk = models.CompositePrimaryKey("author_id", "provider_id", "connection_id")
    author_id = models.TextField(db_column="authorId")
    provider_id = models.TextField(db_column="providerId")
    connection_id = models.TextField(db_column="connectionId")
    toolkit = models.TextField()
    label = models.TextField(null=True)
    scope = models.TextField()
    created_at = UtcDateTimeField(db_column="createdAt")
    updated_at = UtcDateTimeField(db_column="updatedAt")
    created_at_z = models.DateTimeField(null=True, db_column="createdAtZ", default=timezone.now)
    updated_at_z = models.DateTimeField(null=True, db_column="updatedAtZ", default=timezone.now)

    class Meta:
        db_table = "mastra_tool_provider_connections"
        indexes = [
            models.Index(fields=["author_id", "provider_id", "toolkit"]),
        ]


class MastraWorkflowDefinitions(models.Model):
    id = models.TextField(primary_key=True, default=None)
    description = models.TextField(null=True)
    metadata = models.JSONField(null=True)
    input_schema = models.JSONField(db_column="inputSchema")
    output_schema = models.JSONField(db_column="outputSchema")
    state_schema = models.JSONField(null=True, db_column="stateSchema")
    request_context_schema = models.JSONField(null=True, db_column="requestContextSchema")
    graph = models.JSONField()
    schedule = models.JSONField(null=True)
    status = models.TextField()
    source = models.TextField()
    author_id = models.TextField(null=True, db_column="authorId")
    created_at = UtcDateTimeField(db_column="createdAt")
    updated_at = UtcDateTimeField(db_column="updatedAt")
    created_at_z = models.DateTimeField(null=True, db_column="createdAtZ", default=timezone.now)
    updated_at_z = models.DateTimeField(null=True, db_column="updatedAtZ", default=timezone.now)

    class Meta:
        db_table = "mastra_workflow_definitions"
        indexes = [
            models.Index(fields=["status"]),
        ]


class MastraWorkspaceVersions(models.Model):
    id = models.TextField(primary_key=True, default=None)
    workspace_id = models.TextField(db_column="workspaceId")
    version_number = models.IntegerField(db_column="versionNumber")
    name = models.TextField()
    description = models.TextField(null=True)
    filesystem = models.JSONField(null=True)
    sandbox = models.JSONField(null=True)
    mounts = models.JSONField(null=True)
    search = models.JSONField(null=True)
    skills = models.JSONField(null=True)
    tools = models.JSONField(null=True)
    auto_sync = models.BooleanField(null=True, db_column="autoSync")
    operation_timeout = models.IntegerField(null=True, db_column="operationTimeout")
    changed_fields = models.JSONField(null=True, db_column="changedFields")
    change_message = models.TextField(null=True, db_column="changeMessage")
    created_at = UtcDateTimeField(db_column="createdAt")
    created_at_z = models.DateTimeField(null=True, db_column="createdAtZ", default=timezone.now)

    class Meta:
        db_table = "mastra_workspace_versions"
        constraints = [
            models.UniqueConstraint(fields=["workspace_id", "version_number"], name="idx_workspace_versions_workspace_version"),
        ]


class MastraWorkspaces(models.Model):
    id = models.TextField(primary_key=True, default=None)
    status = models.TextField()
    active_version_id = models.TextField(null=True, db_column="activeVersionId")
    author_id = models.TextField(null=True, db_column="authorId")
    metadata = models.JSONField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt")
    updated_at = UtcDateTimeField(db_column="updatedAt")
    created_at_z = models.DateTimeField(null=True, db_column="createdAtZ", default=timezone.now)
    updated_at_z = models.DateTimeField(null=True, db_column="updatedAtZ", default=timezone.now)

    class Meta:
        db_table = "mastra_workspaces"


class OAuthApp(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    organization = models.ForeignKey("Organization", on_delete=models.SET_NULL, related_name="oauth_app", db_column="organizationId", null=True, db_index=False)
    name = models.TextField()
    description = models.TextField(null=True)
    picture = models.ForeignKey("Media", on_delete=models.SET_NULL, related_name="oauth_apps", db_column="pictureId", null=True, db_index=False)
    redirect_url = models.TextField(db_column="redirectUrl")
    redirect_uris = models.TextField(null=True, db_column="redirectUris")
    client_id = models.TextField(unique=True, db_column="clientId")
    client_secret = models.TextField(null=True, db_column="clientSecret")
    dynamic = models.BooleanField(default=False)
    token_endpoint_auth_method = models.TextField(null=True, db_column="tokenEndpointAuthMethod")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    deleted_at = UtcDateTimeField(null=True, db_column="deletedAt")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "OAuthApp"
        constraints = [
            models.UniqueConstraint(fields=["organization", "deleted_at"], name="OAuthApp_organizationId_deletedAt_key"),
        ]
        indexes = [
            models.Index(fields=["client_id"]),
            models.Index(fields=["organization"]),
            models.Index(fields=["deleted_at"]),
            models.Index(fields=["dynamic"]),
        ]


class OAuthAuthorization(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    oauth_app = models.ForeignKey("OAuthApp", on_delete=models.RESTRICT, related_name="authorizations", db_column="oauthAppId", db_index=False)
    user = models.ForeignKey("User", on_delete=models.RESTRICT, related_name="oauth_authorizations", db_column="userId", db_index=False)
    organization = models.ForeignKey("Organization", on_delete=models.RESTRICT, related_name="oauth_authorizations", db_column="organizationId", db_index=False)
    access_token = models.TextField(null=True, db_column="accessToken")
    authorization_code = models.TextField(null=True, db_column="authorizationCode")
    code_expires_at = UtcDateTimeField(null=True, db_column="codeExpiresAt")
    code_challenge = models.TextField(null=True, db_column="codeChallenge")
    code_challenge_method = models.TextField(null=True, db_column="codeChallengeMethod")
    redirect_uri = models.TextField(null=True, db_column="redirectUri")
    revoked_at = UtcDateTimeField(null=True, db_column="revokedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "OAuthAuthorization"
        constraints = [
            models.UniqueConstraint(fields=["oauth_app", "user", "organization"], name="OAuthAuthorization_oauthAppId_userId_organizationId_key"),
        ]
        indexes = [
            models.Index(fields=["access_token"]),
            models.Index(fields=["authorization_code"]),
            models.Index(fields=["oauth_app"]),
            models.Index(fields=["user"]),
            models.Index(fields=["organization"]),
            models.Index(fields=["revoked_at"]),
        ]


class OAuthSelfHostedAuthorization(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    oauth_app = models.ForeignKey("OAuthApp", on_delete=models.RESTRICT, related_name="self_hosted_authorizations", db_column="oauthAppId", db_index=False)
    mcp_url = models.TextField(db_column="mcpUrl")
    api_key = models.TextField(db_column="apiKey")
    email = models.TextField(null=True)
    access_token = models.TextField(null=True, db_column="accessToken")
    authorization_code = models.TextField(null=True, db_column="authorizationCode")
    code_expires_at = UtcDateTimeField(null=True, db_column="codeExpiresAt")
    code_challenge = models.TextField(null=True, db_column="codeChallenge")
    code_challenge_method = models.TextField(null=True, db_column="codeChallengeMethod")
    redirect_uri = models.TextField(null=True, db_column="redirectUri")
    revoked_at = UtcDateTimeField(null=True, db_column="revokedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "OAuthSelfHostedAuthorization"
        indexes = [
            models.Index(fields=["access_token"]),
            models.Index(fields=["authorization_code"]),
            models.Index(fields=["oauth_app"]),
        ]


class Announcement(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    title = models.TextField()
    description = models.TextField()
    color = models.TextField(choices=AnnouncementColor.choices, default=AnnouncementColor.INFO)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "Announcement"
