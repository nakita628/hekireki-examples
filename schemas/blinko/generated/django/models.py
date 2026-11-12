from datetime import datetime
from datetime import timezone as dt_timezone
from decimal import Decimal
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


class Accounts(models.Model):
    id = models.AutoField(primary_key=True)
    name = models.TextField(default="")
    nickname = models.TextField(default="")
    password = models.TextField(default="")
    image = models.TextField(default="")
    api_token = models.TextField(db_column="apiToken", default="")
    description = models.TextField(default="")
    note = models.IntegerField(default=0)
    role = models.TextField(default="")
    login_type = models.TextField(db_column="loginType", default="")
    link_account_id = models.IntegerField(null=True, db_column="linkAccountId")
    created_at = models.DateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = models.DateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "accounts"


class Attachments(models.Model):
    id = models.AutoField(primary_key=True)
    is_share = models.BooleanField(db_column="isShare", default=False)
    share_password = models.TextField(db_column="sharePassword", default="")
    name = models.TextField(default="")
    path = models.TextField(default="")
    size = models.DecimalField(max_digits=65, decimal_places=30, default=Decimal("0"))
    type = models.TextField(default="")
    note = models.ForeignKey("Notes", on_delete=models.SET_NULL, related_name="attachments", db_column="noteId", null=True, db_index=False)
    account = models.ForeignKey("Accounts", on_delete=models.SET_NULL, related_name="attachments", db_column="accountId", null=True, db_index=False)
    sort_order = models.IntegerField(db_column="sortOrder", default=0)
    created_at = models.DateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = models.DateTimeField(db_column="updatedAt", auto_now=True)
    perfix_path = models.TextField(null=True, db_column="perfixPath", default="")
    depth = models.IntegerField(null=True)
    metadata = models.JSONField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "attachments"


class Config(models.Model):
    id = models.AutoField(primary_key=True)
    key = models.TextField(default="")
    config = models.JSONField(null=True)
    user = models.ForeignKey("Accounts", on_delete=models.SET_NULL, related_name="configs", db_column="userId", null=True, db_index=False)

    class Meta:
        db_table = "config"


class Notes(models.Model):
    id = models.AutoField(primary_key=True)
    type = models.IntegerField(default=0)
    content = models.TextField(default="")
    is_archived = models.BooleanField(db_column="isArchived", default=False)
    is_recycle = models.BooleanField(db_column="isRecycle", default=False)
    is_share = models.BooleanField(db_column="isShare", default=False)
    is_top = models.BooleanField(db_column="isTop", default=False)
    is_reviewed = models.BooleanField(db_column="isReviewed", default=False)
    share_password = models.TextField(db_column="sharePassword", default="")
    share_encrypted_url = models.TextField(null=True, db_column="shareEncryptedUrl")
    share_expiry_date = models.DateTimeField(null=True, db_column="shareExpiryDate")
    share_max_view = models.IntegerField(null=True, db_column="shareMaxView", default=0)
    share_view_count = models.IntegerField(null=True, db_column="shareViewCount", default=0)
    metadata = models.JSONField(null=True)
    account = models.ForeignKey("Accounts", on_delete=models.SET_NULL, related_name="notes", db_column="accountId", null=True, db_index=False)
    sort_order = models.IntegerField(db_column="sortOrder", default=0)
    created_at = models.DateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = models.DateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "notes"


class Comments(models.Model):
    id = models.AutoField(primary_key=True)
    content = models.TextField()
    account = models.ForeignKey("Accounts", on_delete=models.SET_NULL, related_name="comments", db_column="accountId", null=True, db_index=False)
    guest_name = models.TextField(null=True, db_column="guestName")
    guest_ip = models.TextField(null=True, db_column="guestIP")
    guest_ua = models.TextField(null=True, db_column="guestUA")
    note = models.ForeignKey("Notes", on_delete=models.RESTRICT, related_name="comments", db_column="noteId", db_index=False)
    parent = models.ForeignKey("self", on_delete=models.SET_NULL, related_name="replies", db_column="parentId", null=True, db_index=False)
    created_at = models.DateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = models.DateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "comments"


class Tag(models.Model):
    id = models.AutoField(primary_key=True)
    name = models.TextField(default="")
    icon = models.TextField(default="")
    parent = models.IntegerField(default=0)
    account = models.ForeignKey("Accounts", on_delete=models.SET_NULL, related_name="tags", db_column="accountId", null=True, db_index=False)
    created_at = models.DateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = models.DateTimeField(db_column="updatedAt", auto_now=True)
    sort_order = models.IntegerField(db_column="sortOrder", default=0)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "tag"


class TagsToNote(models.Model):
    pk = models.CompositePrimaryKey("note_id", "tag_id")
    id = models.IntegerField()
    note = models.ForeignKey("Notes", on_delete=models.RESTRICT, related_name="tags", db_column="noteId", db_index=False, default=0)
    tag = models.ForeignKey("Tag", on_delete=models.RESTRICT, related_name="tags_to_note", db_column="tagId", db_index=False, default=0)

    class Meta:
        db_table = "tagsToNote"


class NoteReference(models.Model):
    id = models.AutoField(primary_key=True)
    from_note = models.ForeignKey("Notes", on_delete=models.RESTRICT, related_name="references", db_column="fromNoteId", db_index=False)
    to_note = models.ForeignKey("Notes", on_delete=models.RESTRICT, related_name="referenced_by", db_column="toNoteId", db_index=False)
    created_at = models.DateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "noteReference"
        constraints = [
            models.UniqueConstraint(fields=["from_note", "to_note"], name="noteReference_fromNoteId_toNoteId_key"),
        ]


class Follows(models.Model):
    id = models.AutoField(primary_key=True)
    site_name = models.TextField(null=True, db_column="siteName")
    site_url = models.TextField(db_column="siteUrl")
    site_avatar = models.TextField(null=True, db_column="siteAvatar")
    description = models.TextField(null=True)
    created_at = models.DateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = models.DateTimeField(db_column="updatedAt", auto_now=True)
    follow_type = models.TextField(db_column="followType", default="following")
    account = models.ForeignKey("Accounts", on_delete=models.RESTRICT, related_name="follows", db_column="accountId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "follows"


class Notifications(models.Model):
    id = models.AutoField(primary_key=True)
    type = models.TextField()
    title = models.TextField()
    content = models.TextField()
    metadata = models.JSONField(null=True)
    is_read = models.BooleanField(db_column="isRead", default=False)
    account = models.ForeignKey("Accounts", on_delete=models.RESTRICT, related_name="notifications", db_column="accountId", db_index=False)
    created_at = models.DateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = models.DateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "notifications"


class Cache(models.Model):
    id = models.AutoField(primary_key=True)
    key = models.TextField(unique=True)
    value = models.JSONField()
    created_at = models.DateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = models.DateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "cache"


class Plugin(models.Model):
    id = models.AutoField(primary_key=True)
    metadata = models.JSONField()
    path = models.TextField()
    is_use = models.BooleanField(db_column="isUse", default=True)
    is_dev = models.BooleanField(db_column="isDev", default=False)
    created_at = models.DateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = models.DateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "plugin"


class Conversation(models.Model):
    id = models.AutoField(primary_key=True)
    title = models.CharField(max_length=255, default="")
    is_share = models.BooleanField(db_column="isShare", default=False)
    account = models.ForeignKey("Accounts", on_delete=models.RESTRICT, related_name="conversations", db_column="accountId", db_index=False)
    created_at = models.DateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = models.DateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "conversation"
        indexes = [
            models.Index(fields=["account"]),
        ]


class Message(models.Model):
    id = models.AutoField(primary_key=True)
    content = models.TextField()
    role = models.CharField(max_length=50)
    conversation = models.ForeignKey("Conversation", on_delete=models.RESTRICT, related_name="messages", db_column="conversationId", db_index=False)
    created_at = models.DateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = models.DateTimeField(db_column="updatedAt", auto_now=True)
    metadata = models.JSONField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "message"
        indexes = [
            models.Index(fields=["conversation"]),
        ]


class NoteHistory(models.Model):
    id = models.AutoField(primary_key=True)
    note = models.ForeignKey("Notes", on_delete=models.CASCADE, related_name="histories", db_column="noteId", db_index=False)
    content = models.TextField()
    metadata = models.JSONField(null=True)
    version = models.IntegerField()
    account_id = models.IntegerField(null=True, db_column="accountId")
    created_at = models.DateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "noteHistory"
        indexes = [
            models.Index(fields=["note"]),
            models.Index(fields=["account_id"]),
        ]


class NoteInternalShare(models.Model):
    id = models.AutoField(primary_key=True)
    note = models.ForeignKey("Notes", on_delete=models.CASCADE, related_name="internal_shares", db_column="noteId", db_index=False)
    account = models.ForeignKey("Accounts", on_delete=models.RESTRICT, related_name="shared_notes", db_column="accountId", db_index=False)
    can_edit = models.BooleanField(db_column="canEdit", default=True)
    created_at = models.DateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = models.DateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "noteInternalShare"
        constraints = [
            models.UniqueConstraint(fields=["note", "account"], name="noteInternalShare_noteId_accountId_key"),
        ]
        indexes = [
            models.Index(fields=["note"]),
            models.Index(fields=["account"]),
        ]


class Session(models.Model):
    id = models.TextField(primary_key=True, default=None)
    sid = models.TextField(unique=True)
    data = models.TextField()
    expires_at = UtcDateTimeField(db_column="expiresAt")
    created_at = models.DateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = models.DateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "session"
        indexes = [
            models.Index(fields=["expires_at"]),
        ]


class AiProviders(models.Model):
    id = models.AutoField(primary_key=True)
    title = models.CharField(max_length=255)
    provider = models.CharField(max_length=50)
    base_url = models.CharField(max_length=500, null=True, db_column="baseURL")
    api_key = models.CharField(max_length=500, null=True, db_column="apiKey")
    config = models.JSONField(null=True)
    sort_order = models.IntegerField(db_column="sortOrder", default=0)
    created_at = models.DateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = models.DateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "aiProviders"


class AiModels(models.Model):
    id = models.AutoField(primary_key=True)
    provider = models.ForeignKey("AiProviders", on_delete=models.CASCADE, related_name="models", db_column="providerId", db_index=False)
    title = models.CharField(max_length=255)
    model_key = models.CharField(max_length=255, db_column="modelKey")
    capabilities = models.JSONField()
    config = models.JSONField(null=True)
    sort_order = models.IntegerField(db_column="sortOrder", default=0)
    created_at = models.DateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = models.DateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "aiModels"
        indexes = [
            models.Index(fields=["provider"]),
        ]


class AiScheduledTask(models.Model):
    id = models.AutoField(primary_key=True)
    name = models.CharField(max_length=255)
    prompt = models.TextField()
    schedule = models.CharField(max_length=100)
    is_enabled = models.BooleanField(db_column="isEnabled", default=True)
    last_run = models.DateTimeField(null=True, db_column="lastRun")
    last_result = models.JSONField(null=True, db_column="lastResult")
    account = models.ForeignKey("Accounts", on_delete=models.RESTRICT, related_name="ai_scheduled_tasks", db_column="accountId", db_index=False)
    created_at = models.DateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = models.DateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "aiScheduledTask"
        indexes = [
            models.Index(fields=["account"]),
        ]


class McpServers(models.Model):
    id = models.AutoField(primary_key=True)
    name = models.CharField(max_length=255)
    description = models.TextField(null=True)
    type = models.CharField(max_length=50)
    command = models.TextField(null=True)
    args = models.JSONField(null=True)
    url = models.CharField(max_length=500, null=True)
    env = models.JSONField(null=True)
    headers = models.JSONField(null=True)
    is_enabled = models.BooleanField(db_column="isEnabled", default=True)
    created_at = models.DateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = models.DateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "mcpServers"


class Fonts(models.Model):
    id = models.AutoField(primary_key=True)
    name = models.CharField(max_length=255, unique=True)
    display_name = models.CharField(max_length=255, db_column="displayName")
    url = models.TextField(null=True)
    file_data = models.BinaryField(null=True, db_column="fileData")
    is_local = models.BooleanField(db_column="isLocal", default=False)
    is_system = models.BooleanField(db_column="isSystem", default=False)
    weights = models.JSONField()
    category = models.CharField(max_length=50, default="sans-serif")
    sort_order = models.IntegerField(db_column="sortOrder", default=0)
    created_at = models.DateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = models.DateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "fonts"
        constraints = [
            models.UniqueConstraint(fields=["name"], name="fonts_name_key"),
        ]
