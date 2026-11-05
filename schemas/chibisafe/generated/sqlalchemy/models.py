from sqlalchemy import Column, Dialect, ForeignKey, Index, Integer, String, Table, Text, TypeDecorator, text
from sqlalchemy.ext.compiler import compiles
from sqlalchemy.orm import DeclarativeBase, Mapped, mapped_column, relationship
from typing import Any, Optional
from datetime import datetime, timezone


class UtcDateTime(TypeDecorator[datetime]):
    impl = String
    cache_ok = True

    def process_bind_param(self, value: Optional[datetime], dialect: Dialect) -> Optional[str]:
        if value is None:
            return None
        aware = value if value.tzinfo else value.replace(tzinfo=timezone.utc)
        return aware.astimezone(timezone.utc).isoformat(timespec="milliseconds")

    def process_result_value(self, value: Optional[str], dialect: Dialect) -> Optional[datetime]:
        if value is None:
            return None
        read = datetime.fromisoformat(value)
        return read.replace(tzinfo=timezone.utc) if read.tzinfo is None else read.astimezone(timezone.utc)


@compiles(UtcDateTime)
def utc_date_time_ddl(type_: UtcDateTime, compiler: Any, **kw: Any) -> str:
    return "DATETIME"


def utc_now() -> datetime:
    return datetime.now(timezone.utc)


class Base(DeclarativeBase):
    type_annotation_map = {datetime: UtcDateTime, str: Text}

roles_tousers = Table(
    "_rolesTousers",
    Base.metadata,
    Column("A", Integer, ForeignKey("roles.id", ondelete="CASCADE", onupdate="CASCADE", name="_rolesTousers_A_fkey"), nullable=False),
    Column("B", Integer, ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="_rolesTousers_B_fkey"), nullable=False),
    Index("_rolesTousers_AB_unique", "A", "B", unique=True),
    Index("_rolesTousers_B_index", "B"),
)

albums_tofiles = Table(
    "_albumsTofiles",
    Base.metadata,
    Column("A", Integer, ForeignKey("albums.id", ondelete="CASCADE", onupdate="CASCADE", name="_albumsTofiles_A_fkey"), nullable=False),
    Column("B", Integer, ForeignKey("files.id", ondelete="CASCADE", onupdate="CASCADE", name="_albumsTofiles_B_fkey"), nullable=False),
    Index("_albumsTofiles_AB_unique", "A", "B", unique=True),
    Index("_albumsTofiles_B_index", "B"),
)

files_totags = Table(
    "_filesTotags",
    Base.metadata,
    Column("A", Integer, ForeignKey("files.id", ondelete="CASCADE", onupdate="CASCADE", name="_filesTotags_A_fkey"), nullable=False),
    Column("B", Integer, ForeignKey("tags.id", ondelete="CASCADE", onupdate="CASCADE", name="_filesTotags_B_fkey"), nullable=False),
    Index("_filesTotags_AB_unique", "A", "B", unique=True),
    Index("_filesTotags_B_index", "B"),
)


class Users(Base):
    __tablename__ = "users"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    uuid: Mapped[str]
    username: Mapped[str]
    password: Mapped[str]
    enabled: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    storage_quota: Mapped[str] = mapped_column("storageQuota", default="0", server_default=text("'0'"))
    api_key: Mapped[Optional[str]] = mapped_column("apiKey")
    password_edited_at: Mapped[Optional[datetime]] = mapped_column("passwordEditedAt")
    api_key_edited_at: Mapped[Optional[datetime]] = mapped_column("apiKeyEditedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    edited_at: Mapped[Optional[datetime]] = mapped_column("editedAt")

    __table_args__ = (
        Index("users_uuid_key", "uuid", unique=True),
        Index("users_username_key", "username", unique=True),
        Index("users_apiKey_key", "apiKey", unique=True),
        {"sqlite_autoincrement": True},
    )

    files: Mapped[list["Files"]] = relationship(back_populates="user")
    links: Mapped[list["ShortenedLinks"]] = relationship(back_populates="user")
    roles: Mapped[list["Roles"]] = relationship(secondary=roles_tousers, back_populates="users")

class Roles(Base):
    __tablename__ = "roles"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    name: Mapped[str]

    __table_args__ = (
        Index("roles_name_key", "name", unique=True),
        {"sqlite_autoincrement": True},
    )

    users: Mapped[list["Users"]] = relationship(secondary=roles_tousers, back_populates="roles")

class Files(Base):
    __tablename__ = "files"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    uuid: Mapped[str]
    user_id: Mapped[Optional[int]] = mapped_column("userId", ForeignKey("users.id", ondelete="SET NULL", onupdate="CASCADE", name="files_userId_fkey"))
    name: Mapped[str]
    original: Mapped[str]
    type: Mapped[str]
    size: Mapped[str]
    hash: Mapped[str]
    ip: Mapped[str]
    source_url: Mapped[Optional[str]] = mapped_column("sourceUrl")
    is_s3: Mapped[bool] = mapped_column("isS3", default=False, server_default=text("false"))
    is_watched: Mapped[bool] = mapped_column("isWatched", default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    edited_at: Mapped[Optional[datetime]] = mapped_column("editedAt")
    quarantine: Mapped[bool] = mapped_column(default=False, server_default=text("false"))

    __table_args__ = (
        Index("files_uuid_key", "uuid", unique=True),
        {"sqlite_autoincrement": True},
    )

    user: Mapped[Optional["Users"]] = relationship(back_populates="files")
    quarantine_file: Mapped[Optional["QuarantineFiles"]] = relationship(passive_deletes="all", back_populates="file")
    albums: Mapped[list["Albums"]] = relationship(secondary=albums_tofiles, back_populates="files")
    tags: Mapped[list["Tags"]] = relationship(secondary=files_totags, back_populates="files")

class QuarantineFiles(Base):
    __tablename__ = "quarantineFiles"

    file_id: Mapped[str] = mapped_column("fileId", ForeignKey("files.uuid", ondelete="RESTRICT", onupdate="CASCADE", name="quarantineFiles_fileId_fkey"))
    name: Mapped[str]
    reason: Mapped[Optional[str]]

    __table_args__ = (
        Index("quarantineFiles_fileId_key", "fileId", unique=True),
    )

    __mapper_args__ = {"primary_key": [file_id]}

    file: Mapped["Files"] = relationship(back_populates="quarantine_file")

class Snippets(Base):
    __tablename__ = "snippets"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    uuid: Mapped[str]
    user_id: Mapped[Optional[int]] = mapped_column("userId")
    identifier: Mapped[str]
    name: Mapped[str]
    description: Mapped[Optional[str]]
    content: Mapped[str]
    language: Mapped[str]
    parent_uuid: Mapped[Optional[str]] = mapped_column("parentUuid")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    edited_at: Mapped[Optional[datetime]] = mapped_column("editedAt")

    __table_args__ = (
        Index("snippets_uuid_key", "uuid", unique=True),
        Index("snippets_identifier_key", "identifier", unique=True),
        {"sqlite_autoincrement": True},
    )

class ShortenedLinks(Base):
    __tablename__ = "shortenedLinks"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    uuid: Mapped[str]
    user_id: Mapped[Optional[int]] = mapped_column("userId", ForeignKey("users.id", ondelete="SET NULL", onupdate="CASCADE", name="shortenedLinks_userId_fkey"))
    identifier: Mapped[str]
    destination: Mapped[str]
    views: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("shortenedLinks_uuid_key", "uuid", unique=True),
        Index("shortenedLinks_identifier_key", "identifier", unique=True),
        {"sqlite_autoincrement": True},
    )

    user: Mapped[Optional["Users"]] = relationship(back_populates="links")

class Albums(Base):
    __tablename__ = "albums"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    uuid: Mapped[str]
    user_id: Mapped[int] = mapped_column("userId")
    name: Mapped[str]
    description: Mapped[Optional[str]]
    zipped_at: Mapped[Optional[datetime]] = mapped_column("zippedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    edited_at: Mapped[Optional[datetime]] = mapped_column("editedAt")
    nsfw: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    sort_order: Mapped[Optional[str]] = mapped_column("sortOrder")

    __table_args__ = (
        Index("albums_uuid_key", "uuid", unique=True),
        {"sqlite_autoincrement": True},
    )

    files: Mapped[list["Files"]] = relationship(secondary=albums_tofiles, back_populates="albums")

class Bans(Base):
    __tablename__ = "bans"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    ip: Mapped[str]
    reason: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        {"sqlite_autoincrement": True},
    )

class Links(Base):
    __tablename__ = "links"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    uuid: Mapped[str]
    user_id: Mapped[int] = mapped_column("userId")
    album_id: Mapped[int] = mapped_column("albumId")
    identifier: Mapped[str]
    views: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    enabled: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    enable_download: Mapped[bool] = mapped_column("enableDownload", default=False, server_default=text("false"))
    expires_at: Mapped[Optional[datetime]] = mapped_column("expiresAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    edited_at: Mapped[Optional[datetime]] = mapped_column("editedAt")

    __table_args__ = (
        Index("links_uuid_key", "uuid", unique=True),
        Index("links_identifier_key", "identifier", unique=True),
        {"sqlite_autoincrement": True},
    )

class Settings(Base):
    __tablename__ = "settings"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    rate_limit_window: Mapped[int] = mapped_column("rateLimitWindow")
    rate_limit_max: Mapped[int] = mapped_column("rateLimitMax")
    secret: Mapped[str]
    service_name: Mapped[str] = mapped_column("serviceName")
    chunk_size: Mapped[str] = mapped_column("chunkSize")
    chunked_uploads_timeout: Mapped[int] = mapped_column("chunkedUploadsTimeout")
    max_size: Mapped[str] = mapped_column("maxSize")
    generate_zips: Mapped[bool] = mapped_column("generateZips")
    generate_original_file_name_with_identifier: Mapped[bool] = mapped_column("generateOriginalFileNameWithIdentifier", default=False, server_default=text("false"))
    generated_filename_length: Mapped[int] = mapped_column("generatedFilenameLength")
    generated_album_length: Mapped[int] = mapped_column("generatedAlbumLength")
    generated_links_length: Mapped[int] = mapped_column("generatedLinksLength", default=8, server_default=text("8"))
    save_duplicates_to_album: Mapped[bool] = mapped_column("saveDuplicatesToAlbum", default=False, server_default=text("false"))
    blocked_extensions: Mapped[str] = mapped_column("blockedExtensions")
    block_no_extension: Mapped[bool] = mapped_column("blockNoExtension")
    public_mode: Mapped[bool] = mapped_column("publicMode")
    user_accounts: Mapped[bool] = mapped_column("userAccounts")
    disable_statistics_cron: Mapped[bool] = mapped_column("disableStatisticsCron")
    disable_update_check: Mapped[bool] = mapped_column("disableUpdateCheck", default=False, server_default=text("false"))
    background_image_url: Mapped[str] = mapped_column("backgroundImageURL")
    logo_url: Mapped[str] = mapped_column("logoURL")
    meta_description: Mapped[str] = mapped_column("metaDescription")
    meta_keywords: Mapped[str] = mapped_column("metaKeywords")
    meta_twitter_handle: Mapped[str] = mapped_column("metaTwitterHandle")
    meta_domain: Mapped[str] = mapped_column("metaDomain", default="", server_default=text("''"))
    serve_uploads_from: Mapped[str] = mapped_column("serveUploadsFrom", default="", server_default=text("''"))
    enable_mixed_case_filenames: Mapped[bool] = mapped_column("enableMixedCaseFilenames", default=True, server_default=text("true"))
    users_storage_quota: Mapped[str] = mapped_column("usersStorageQuota", default="0", server_default=text("'0'"))
    use_network_storage: Mapped[bool] = mapped_column("useNetworkStorage", default=False, server_default=text("false"))
    use_minimal_homepage: Mapped[bool] = mapped_column("useMinimalHomepage", default=False, server_default=text("false"))
    use_url_shortener: Mapped[bool] = mapped_column("useUrlShortener", default=False, server_default=text("false"))
    generate_thumbnails: Mapped[bool] = mapped_column("generateThumbnails", default=True, server_default=text("true"))
    privacy_policy_page_content: Mapped[str] = mapped_column("privacyPolicyPageContent", default="", server_default=text("''"))
    terms_of_service_page_content: Mapped[str] = mapped_column("termsOfServicePageContent", default="", server_default=text("''"))
    rules_page_content: Mapped[str] = mapped_column("rulesPageContent", default="", server_default=text("''"))
    s3_region: Mapped[str] = mapped_column("S3Region", default="", server_default=text("''"))
    s3_bucket: Mapped[str] = mapped_column("S3Bucket", default="", server_default=text("''"))
    s3_access_key: Mapped[str] = mapped_column("S3AccessKey", default="", server_default=text("''"))
    s3_secret_key: Mapped[str] = mapped_column("S3SecretKey", default="", server_default=text("''"))
    s3_endpoint: Mapped[str] = mapped_column("S3Endpoint", default="", server_default=text("''"))
    s3_path_style: Mapped[bool] = mapped_column("S3PathStyle", default=False, server_default=text("false"))
    s3_public_url: Mapped[str] = mapped_column("S3PublicUrl", default="", server_default=text("''"))
    default_sort_order: Mapped[str] = mapped_column("defaultSortOrder", default="createdAt:desc", server_default=text("'createdAt:desc'"))

    __table_args__ = (
        {"sqlite_autoincrement": True},
    )

class Statistics(Base):
    __tablename__ = "statistics"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    batch_id: Mapped[Optional[int]] = mapped_column("batchId")
    type: Mapped[Optional[str]]
    data: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        {"sqlite_autoincrement": True},
    )

class Tags(Base):
    __tablename__ = "tags"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    uuid: Mapped[str]
    user_id: Mapped[int] = mapped_column("userId")
    name: Mapped[str]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    edited_at: Mapped[Optional[datetime]] = mapped_column("editedAt")

    __table_args__ = (
        Index("tags_uuid_key", "uuid", unique=True),
        {"sqlite_autoincrement": True},
    )

    files: Mapped[list["Files"]] = relationship(secondary=files_totags, back_populates="tags")

class Invites(Base):
    __tablename__ = "invites"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    created_by: Mapped[str] = mapped_column("createdBy")
    used_by: Mapped[Optional[str]] = mapped_column("usedBy")
    code: Mapped[str]
    used: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    edited_at: Mapped[Optional[datetime]] = mapped_column("editedAt")

    __table_args__ = (
        Index("invites_code_key", "code", unique=True),
        {"sqlite_autoincrement": True},
    )
