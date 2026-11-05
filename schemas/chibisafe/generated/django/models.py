from datetime import datetime
from datetime import timezone as dt_timezone
from typing import Any

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


class Users(models.Model):
    id = models.AutoField(primary_key=True)
    uuid = models.TextField(unique=True)
    username = models.TextField(unique=True)
    password = models.TextField()
    enabled = models.BooleanField(default=True)
    storage_quota = models.TextField(db_column="storageQuota", default="0")
    api_key = models.TextField(null=True, unique=True, db_column="apiKey")
    password_edited_at = UtcDateTimeField(null=True, db_column="passwordEditedAt")
    api_key_edited_at = UtcDateTimeField(null=True, db_column="apiKeyEditedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    edited_at = UtcDateTimeField(null=True, db_column="editedAt")

    class Meta:
        db_table = "users"


class Roles(models.Model):
    id = models.AutoField(primary_key=True)
    name = models.TextField(unique=True)
    users: "models.ManyToManyField[Users, RolesTousers]" = models.ManyToManyField("Users", through="RolesTousers", related_name="roles")

    class Meta:
        db_table = "roles"


class Files(models.Model):
    id = models.AutoField(primary_key=True)
    uuid = models.TextField(unique=True)
    user = models.ForeignKey("Users", on_delete=models.SET_NULL, related_name="files", db_column="userId", null=True, db_index=False)
    name = models.TextField()
    original = models.TextField()
    type = models.TextField()
    size = models.TextField()
    hash = models.TextField()
    ip = models.TextField()
    source_url = models.TextField(null=True, db_column="sourceUrl")
    is_s3 = models.BooleanField(db_column="isS3", default=False)
    is_watched = models.BooleanField(db_column="isWatched", default=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    edited_at = UtcDateTimeField(null=True, db_column="editedAt")
    tags: "models.ManyToManyField[Tags, FilesTotags]" = models.ManyToManyField("Tags", through="FilesTotags", related_name="files")
    quarantine = models.BooleanField(default=False)

    class Meta:
        db_table = "files"


class QuarantineFiles(models.Model):
    file = models.OneToOneField("Files", on_delete=models.RESTRICT, related_name="quarantine_file", to_field="uuid", primary_key=True, db_column="fileId")
    name = models.TextField()
    reason = models.TextField(null=True)

    class Meta:
        db_table = "quarantineFiles"


class Snippets(models.Model):
    id = models.AutoField(primary_key=True)
    uuid = models.TextField(unique=True)
    user_id = models.IntegerField(null=True, db_column="userId")
    identifier = models.TextField(unique=True)
    name = models.TextField()
    description = models.TextField(null=True)
    content = models.TextField()
    language = models.TextField()
    parent_uuid = models.TextField(null=True, db_column="parentUuid")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    edited_at = UtcDateTimeField(null=True, db_column="editedAt")

    class Meta:
        db_table = "snippets"


class ShortenedLinks(models.Model):
    id = models.AutoField(primary_key=True)
    uuid = models.TextField(unique=True)
    user = models.ForeignKey("Users", on_delete=models.SET_NULL, related_name="links", db_column="userId", null=True, db_index=False)
    identifier = models.TextField(unique=True)
    destination = models.TextField()
    views = models.IntegerField(default=0)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "shortenedLinks"


class Albums(models.Model):
    id = models.AutoField(primary_key=True)
    uuid = models.TextField(unique=True)
    user_id = models.IntegerField(db_column="userId")
    name = models.TextField()
    description = models.TextField(null=True)
    zipped_at = UtcDateTimeField(null=True, db_column="zippedAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    edited_at = UtcDateTimeField(null=True, db_column="editedAt")
    nsfw = models.BooleanField(default=False)
    sort_order = models.TextField(null=True, db_column="sortOrder")
    files: "models.ManyToManyField[Files, AlbumsTofiles]" = models.ManyToManyField("Files", through="AlbumsTofiles", related_name="albums")

    class Meta:
        db_table = "albums"


class Bans(models.Model):
    id = models.AutoField(primary_key=True)
    ip = models.TextField()
    reason = models.TextField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "bans"


class Links(models.Model):
    id = models.AutoField(primary_key=True)
    uuid = models.TextField(unique=True)
    user_id = models.IntegerField(db_column="userId")
    album_id = models.IntegerField(db_column="albumId")
    identifier = models.TextField(unique=True)
    views = models.IntegerField(default=0)
    enabled = models.BooleanField(default=True)
    enable_download = models.BooleanField(db_column="enableDownload", default=False)
    expires_at = UtcDateTimeField(null=True, db_column="expiresAt")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    edited_at = UtcDateTimeField(null=True, db_column="editedAt")

    class Meta:
        db_table = "links"


class Settings(models.Model):
    id = models.AutoField(primary_key=True)
    rate_limit_window = models.IntegerField(db_column="rateLimitWindow")
    rate_limit_max = models.IntegerField(db_column="rateLimitMax")
    secret = models.TextField()
    service_name = models.TextField(db_column="serviceName")
    chunk_size = models.TextField(db_column="chunkSize")
    chunked_uploads_timeout = models.IntegerField(db_column="chunkedUploadsTimeout")
    max_size = models.TextField(db_column="maxSize")
    generate_zips = models.BooleanField(db_column="generateZips")
    generate_original_file_name_with_identifier = models.BooleanField(db_column="generateOriginalFileNameWithIdentifier", default=False)
    generated_filename_length = models.IntegerField(db_column="generatedFilenameLength")
    generated_album_length = models.IntegerField(db_column="generatedAlbumLength")
    generated_links_length = models.IntegerField(db_column="generatedLinksLength", default=8)
    save_duplicates_to_album = models.BooleanField(db_column="saveDuplicatesToAlbum", default=False)
    blocked_extensions = models.TextField(db_column="blockedExtensions")
    block_no_extension = models.BooleanField(db_column="blockNoExtension")
    public_mode = models.BooleanField(db_column="publicMode")
    user_accounts = models.BooleanField(db_column="userAccounts")
    disable_statistics_cron = models.BooleanField(db_column="disableStatisticsCron")
    disable_update_check = models.BooleanField(db_column="disableUpdateCheck", default=False)
    background_image_url = models.TextField(db_column="backgroundImageURL")
    logo_url = models.TextField(db_column="logoURL")
    meta_description = models.TextField(db_column="metaDescription")
    meta_keywords = models.TextField(db_column="metaKeywords")
    meta_twitter_handle = models.TextField(db_column="metaTwitterHandle")
    meta_domain = models.TextField(db_column="metaDomain", default="")
    serve_uploads_from = models.TextField(db_column="serveUploadsFrom", default="")
    enable_mixed_case_filenames = models.BooleanField(db_column="enableMixedCaseFilenames", default=True)
    users_storage_quota = models.TextField(db_column="usersStorageQuota", default="0")
    use_network_storage = models.BooleanField(db_column="useNetworkStorage", default=False)
    use_minimal_homepage = models.BooleanField(db_column="useMinimalHomepage", default=False)
    use_url_shortener = models.BooleanField(db_column="useUrlShortener", default=False)
    generate_thumbnails = models.BooleanField(db_column="generateThumbnails", default=True)
    privacy_policy_page_content = models.TextField(db_column="privacyPolicyPageContent", default="")
    terms_of_service_page_content = models.TextField(db_column="termsOfServicePageContent", default="")
    rules_page_content = models.TextField(db_column="rulesPageContent", default="")
    s3_region = models.TextField(db_column="S3Region", default="")
    s3_bucket = models.TextField(db_column="S3Bucket", default="")
    s3_access_key = models.TextField(db_column="S3AccessKey", default="")
    s3_secret_key = models.TextField(db_column="S3SecretKey", default="")
    s3_endpoint = models.TextField(db_column="S3Endpoint", default="")
    s3_path_style = models.BooleanField(db_column="S3PathStyle", default=False)
    s3_public_url = models.TextField(db_column="S3PublicUrl", default="")
    default_sort_order = models.TextField(db_column="defaultSortOrder", default="createdAt:desc")

    class Meta:
        db_table = "settings"


class Statistics(models.Model):
    id = models.AutoField(primary_key=True)
    batch_id = models.IntegerField(null=True, db_column="batchId")
    type = models.TextField(null=True)
    data = models.TextField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)

    class Meta:
        db_table = "statistics"


class Tags(models.Model):
    id = models.AutoField(primary_key=True)
    uuid = models.TextField(unique=True)
    user_id = models.IntegerField(db_column="userId")
    name = models.TextField()
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    edited_at = UtcDateTimeField(null=True, db_column="editedAt")

    class Meta:
        db_table = "tags"


class Invites(models.Model):
    id = models.AutoField(primary_key=True)
    created_by = models.TextField(db_column="createdBy")
    used_by = models.TextField(null=True, db_column="usedBy")
    code = models.TextField(unique=True)
    used = models.BooleanField(default=False)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    edited_at = UtcDateTimeField(null=True, db_column="editedAt")

    class Meta:
        db_table = "invites"


class RolesTousers(models.Model):
    pk = models.CompositePrimaryKey("a_id", "b_id")
    a = models.ForeignKey("Roles", on_delete=models.CASCADE, related_name="+", db_column="A", db_index=False)
    b = models.ForeignKey("Users", on_delete=models.CASCADE, related_name="+", db_column="B", db_index=False)

    class Meta:
        db_table = "_rolesTousers"
        indexes = [
            models.Index(fields=["b"]),
        ]


class AlbumsTofiles(models.Model):
    pk = models.CompositePrimaryKey("a_id", "b_id")
    a = models.ForeignKey("Albums", on_delete=models.CASCADE, related_name="+", db_column="A", db_index=False)
    b = models.ForeignKey("Files", on_delete=models.CASCADE, related_name="+", db_column="B", db_index=False)

    class Meta:
        db_table = "_albumsTofiles"
        indexes = [
            models.Index(fields=["b"]),
        ]


class FilesTotags(models.Model):
    pk = models.CompositePrimaryKey("a_id", "b_id")
    a = models.ForeignKey("Files", on_delete=models.CASCADE, related_name="+", db_column="A", db_index=False)
    b = models.ForeignKey("Tags", on_delete=models.CASCADE, related_name="+", db_column="B", db_index=False)

    class Meta:
        db_table = "_filesTotags"
        indexes = [
            models.Index(fields=["b"]),
        ]
