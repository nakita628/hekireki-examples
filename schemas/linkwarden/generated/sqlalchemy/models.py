from sqlalchemy import ARRAY, Column, Dialect, Enum, ForeignKey, Index, Integer, MetaData, PrimaryKeyConstraint, Table, Text, TypeDecorator, Uuid, text
from sqlalchemy.dialects.postgresql import JSONB, TIMESTAMP
from sqlalchemy.orm import DeclarativeBase, Mapped, mapped_column, relationship
from typing import Any, Optional
from datetime import datetime, timezone
import uuid as uuid_mod


class UtcDateTime(TypeDecorator[datetime]):
    impl = TIMESTAMP
    cache_ok = True

    def process_bind_param(self, value: Optional[datetime], dialect: Dialect) -> Optional[datetime]:
        if value is None or value.tzinfo is None:
            return value
        return value.astimezone(timezone.utc).replace(tzinfo=None)

    def process_result_value(self, value: Optional[datetime], dialect: Dialect) -> Optional[datetime]:
        return None if value is None else value.replace(tzinfo=timezone.utc)


def utc_now() -> datetime:
    return datetime.now(timezone.utc)


class Base(DeclarativeBase):
    metadata = MetaData(naming_convention={"pk": "%(table_name)s_pkey"})
    type_annotation_map = {datetime: UtcDateTime(precision=3), str: Text}

pinned_links = Table(
    "_PinnedLinks",
    Base.metadata,
    Column("A", Integer, ForeignKey("Link.id", ondelete="CASCADE", onupdate="CASCADE", name="_PinnedLinks_A_fkey"), nullable=False),
    Column("B", Integer, ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="_PinnedLinks_B_fkey"), nullable=False),
    PrimaryKeyConstraint("A", "B", name="_PinnedLinks_AB_pkey"),
    Index("_PinnedLinks_B_index", "B"),
)

link_to_tag = Table(
    "_LinkToTag",
    Base.metadata,
    Column("A", Integer, ForeignKey("Link.id", ondelete="CASCADE", onupdate="CASCADE", name="_LinkToTag_A_fkey"), nullable=False),
    Column("B", Integer, ForeignKey("Tag.id", ondelete="CASCADE", onupdate="CASCADE", name="_LinkToTag_B_fkey"), nullable=False),
    PrimaryKeyConstraint("A", "B", name="_LinkToTag_AB_pkey"),
    Index("_LinkToTag_B_index", "B"),
)


class Account(Base):
    __tablename__ = "Account"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[int] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="Account_userId_fkey"))
    type: Mapped[str]
    provider: Mapped[str]
    provider_account_id: Mapped[str] = mapped_column("providerAccountId")
    refresh_token: Mapped[Optional[str]]
    access_token: Mapped[Optional[str]]
    expires_at: Mapped[Optional[int]]
    token_type: Mapped[Optional[str]]
    scope: Mapped[Optional[str]]
    id_token: Mapped[Optional[str]]
    session_state: Mapped[Optional[str]]

    __table_args__ = (
        Index("Account_provider_providerAccountId_key", "provider", "providerAccountId", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="accounts")

class User(Base):
    __tablename__ = "User"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    uuid: Mapped[uuid_mod.UUID] = mapped_column(Uuid, default=uuid_mod.uuid4)
    name: Mapped[Optional[str]]
    username: Mapped[Optional[str]]
    email: Mapped[Optional[str]]
    email_verified: Mapped[Optional[datetime]] = mapped_column("emailVerified")
    unverified_new_email: Mapped[Optional[str]] = mapped_column("unverifiedNewEmail")
    image: Mapped[Optional[str]]
    password: Mapped[Optional[str]]
    locale: Mapped[str] = mapped_column(default="en", server_default=text("'en'"))
    parent_subscription_id: Mapped[Optional[int]] = mapped_column("parentSubscriptionId", ForeignKey("Subscription.id", ondelete="SET NULL", onupdate="CASCADE", name="User_parentSubscriptionId_fkey"))
    collection_order: Mapped[list[int]] = mapped_column("collectionOrder", ARRAY(Integer), nullable=True, default=lambda: [], server_default=text("ARRAY[]::integer[]"))
    links_route_to: Mapped[str] = mapped_column("linksRouteTo", Enum("ORIGINAL", "PDF", "READABLE", "MONOLITH", "SCREENSHOT", "DETAILS", name="LinksRouteTo"), default="ORIGINAL", server_default=text("'ORIGINAL'"))
    ai_tagging_method: Mapped[str] = mapped_column("aiTaggingMethod", Enum("DISABLED", "GENERATE", "EXISTING", "PREDEFINED", name="AiTaggingMethod"), default="DISABLED", server_default=text("'DISABLED'"))
    ai_predefined_tags: Mapped[list[str]] = mapped_column("aiPredefinedTags", ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    ai_tag_existing_links: Mapped[bool] = mapped_column("aiTagExistingLinks", default=False, server_default=text("false"))
    theme: Mapped[str] = mapped_column(Enum("dark", "light", "auto", name="Theme"), default="dark", server_default=text("'dark'"))
    dismissed_announcement_id: Mapped[Optional[str]] = mapped_column("dismissedAnnouncementId")
    readable_font_family: Mapped[Optional[str]] = mapped_column("readableFontFamily", default="sans-serif", server_default=text("'sans-serif'"))
    readable_font_size: Mapped[Optional[str]] = mapped_column("readableFontSize", default="20px", server_default=text("'20px'"))
    readable_line_height: Mapped[Optional[str]] = mapped_column("readableLineHeight", default="1.8", server_default=text("'1.8'"))
    readable_line_width: Mapped[Optional[str]] = mapped_column("readableLineWidth", default="normal", server_default=text("'normal'"))
    prevent_duplicate_links: Mapped[bool] = mapped_column("preventDuplicateLinks", default=False, server_default=text("false"))
    archive_as_screenshot: Mapped[bool] = mapped_column("archiveAsScreenshot", default=True, server_default=text("true"))
    archive_as_monolith: Mapped[bool] = mapped_column("archiveAsMonolith", default=True, server_default=text("true"))
    archive_as_pdf: Mapped[bool] = mapped_column("archiveAsPDF", default=True, server_default=text("true"))
    archive_as_readable: Mapped[bool] = mapped_column("archiveAsReadable", default=True, server_default=text("true"))
    archive_as_wayback_machine: Mapped[bool] = mapped_column("archiveAsWaybackMachine", default=False, server_default=text("false"))
    referred_by: Mapped[Optional[str]] = mapped_column("referredBy")
    last_picked_at: Mapped[Optional[datetime]] = mapped_column("lastPickedAt")
    accept_promotional_emails: Mapped[bool] = mapped_column("acceptPromotionalEmails", default=False, server_default=text("false"))
    share_usage_analytics: Mapped[bool] = mapped_column("shareUsageAnalytics", default=True, server_default=text("true"))
    trial_end_email_sent: Mapped[bool] = mapped_column("trialEndEmailSent", default=False, server_default=text("false"))
    session_version: Mapped[int] = mapped_column("sessionVersion", default=0, server_default=text("0"))
    password_retries: Mapped[int] = mapped_column("passwordRetries", default=0, server_default=text("0"))
    password_retry_at: Mapped[Optional[datetime]] = mapped_column("passwordRetryAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("User_uuid_key", "uuid", unique=True),
        Index("User_username_key", "username", unique=True),
        Index("User_email_key", "email", unique=True),
    )

    parent_subscription: Mapped[Optional["Subscription"]] = relationship(foreign_keys=[parent_subscription_id], back_populates="child_users")
    accounts: Mapped[list["Account"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    collections: Mapped[list["Collection"]] = relationship(foreign_keys="Collection.owner_id", cascade="all, delete", passive_deletes=True, back_populates="owner")
    tags: Mapped[list["Tag"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="owner")
    created_links: Mapped[list["Link"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="created_by")
    created_collections: Mapped[list["Collection"]] = relationship(foreign_keys="Collection.created_by_id", back_populates="created_by")
    highlights: Mapped[list["Highlight"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    collections_joined: Mapped[list["UsersAndCollections"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    access_tokens: Mapped[list["AccessToken"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    dashboard_sections: Mapped[list["DashboardSection"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    subscriptions: Mapped[Optional["Subscription"]] = relationship(foreign_keys="Subscription.user_id", cascade="all, delete", passive_deletes=True, back_populates="user")
    pinned_links: Mapped[list["Link"]] = relationship(secondary=pinned_links, back_populates="pinned_by")

class VerificationToken(Base):
    __tablename__ = "VerificationToken"

    identifier: Mapped[str]
    token: Mapped[str] = mapped_column()
    expires: Mapped[datetime]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("VerificationToken_token_key", "token", unique=True),
        Index("VerificationToken_identifier_token_key", "identifier", "token", unique=True),
    )

    __mapper_args__ = {"primary_key": [token]}

class EmailVerificationCode(Base):
    __tablename__ = "EmailVerificationCode"

    identifier: Mapped[str] = mapped_column()
    code: Mapped[str]
    attempts: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    expires: Mapped[datetime]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("EmailVerificationCode_identifier_key", "identifier", unique=True),
    )

    __mapper_args__ = {"primary_key": [identifier]}

class PasswordResetToken(Base):
    __tablename__ = "PasswordResetToken"

    identifier: Mapped[str]
    token: Mapped[str] = mapped_column()
    expires: Mapped[datetime]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("PasswordResetToken_token_key", "token", unique=True),
    )

    __mapper_args__ = {"primary_key": [token]}

class Collection(Base):
    __tablename__ = "Collection"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    name: Mapped[str]
    description: Mapped[str] = mapped_column(default="", server_default=text("''"))
    icon: Mapped[Optional[str]]
    icon_weight: Mapped[Optional[str]] = mapped_column("iconWeight")
    color: Mapped[str] = mapped_column(default="#0ea5e9", server_default=text("'#0ea5e9'"))
    parent_id: Mapped[Optional[int]] = mapped_column("parentId", ForeignKey("Collection.id", ondelete="CASCADE", onupdate="CASCADE", name="Collection_parentId_fkey"))
    is_public: Mapped[bool] = mapped_column("isPublic", default=False, server_default=text("false"))
    owner_id: Mapped[int] = mapped_column("ownerId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="Collection_ownerId_fkey"))
    created_by_id: Mapped[Optional[int]] = mapped_column("createdById", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="Collection_createdById_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("Collection_ownerId_idx", "ownerId"),
    )

    parent: Mapped[Optional["Collection"]] = relationship(remote_side=[id], back_populates="sub_collections")
    owner: Mapped["User"] = relationship(foreign_keys=[owner_id], back_populates="collections")
    created_by: Mapped[Optional["User"]] = relationship(foreign_keys=[created_by_id], back_populates="created_collections")
    sub_collections: Mapped[list["Collection"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="parent")
    members: Mapped[list["UsersAndCollections"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="collection")
    links: Mapped[list["Link"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="collection")
    rss_subscriptions: Mapped[list["RssSubscription"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="collection")
    dashboard_section: Mapped[list["DashboardSection"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="collection")

class UsersAndCollections(Base):
    __tablename__ = "UsersAndCollections"

    user_id: Mapped[int] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="UsersAndCollections_userId_fkey"), primary_key=True)
    collection_id: Mapped[int] = mapped_column("collectionId", ForeignKey("Collection.id", ondelete="CASCADE", onupdate="CASCADE", name="UsersAndCollections_collectionId_fkey"), primary_key=True)
    can_create: Mapped[bool] = mapped_column("canCreate")
    can_update: Mapped[bool] = mapped_column("canUpdate")
    can_delete: Mapped[bool] = mapped_column("canDelete")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("UsersAndCollections_userId_idx", "userId"),
    )

    user: Mapped["User"] = relationship(back_populates="collections_joined")
    collection: Mapped["Collection"] = relationship(back_populates="members")

class Link(Base):
    __tablename__ = "Link"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    name: Mapped[str] = mapped_column(default="", server_default=text("''"))
    type: Mapped[str] = mapped_column(default="url", server_default=text("'url'"))
    description: Mapped[str] = mapped_column(default="", server_default=text("''"))
    created_by_id: Mapped[Optional[int]] = mapped_column("createdById", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="Link_createdById_fkey"))
    collection_id: Mapped[int] = mapped_column("collectionId", ForeignKey("Collection.id", ondelete="CASCADE", onupdate="CASCADE", name="Link_collectionId_fkey"))
    collection_order: Mapped[float] = mapped_column("collectionOrder")
    icon: Mapped[Optional[str]]
    icon_weight: Mapped[Optional[str]] = mapped_column("iconWeight")
    color: Mapped[Optional[str]]
    url: Mapped[Optional[str]]
    text_content: Mapped[Optional[str]] = mapped_column("textContent")
    preview: Mapped[Optional[str]]
    image: Mapped[Optional[str]]
    pdf: Mapped[Optional[str]]
    readable: Mapped[Optional[str]]
    monolith: Mapped[Optional[str]]
    client_side: Mapped[bool] = mapped_column("clientSide", default=False, server_default=text("false"))
    ai_tagged: Mapped[bool] = mapped_column("aiTagged", default=False, server_default=text("false"))
    meta_description: Mapped[Optional[str]] = mapped_column("metaDescription")
    index_version: Mapped[Optional[int]] = mapped_column("indexVersion")
    last_preserved: Mapped[Optional[datetime]] = mapped_column("lastPreserved")
    import_date: Mapped[Optional[datetime]] = mapped_column("importDate")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("Link_collectionId_idx", "collectionId"),
        Index("Link_collectionId_collectionOrder_id_idx", "collectionId", "collectionOrder", "id"),
    )

    created_by: Mapped[Optional["User"]] = relationship(back_populates="created_links")
    collection: Mapped["Collection"] = relationship(back_populates="links")
    highlight: Mapped[list["Highlight"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="link")
    pinned_by: Mapped[list["User"]] = relationship(secondary=pinned_links, back_populates="pinned_links")
    tags: Mapped[list["Tag"]] = relationship(secondary=link_to_tag, back_populates="links")

class Tag(Base):
    __tablename__ = "Tag"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    name: Mapped[str]
    owner_id: Mapped[int] = mapped_column("ownerId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="Tag_ownerId_fkey"))
    archive_as_screenshot: Mapped[Optional[bool]] = mapped_column("archiveAsScreenshot")
    archive_as_monolith: Mapped[Optional[bool]] = mapped_column("archiveAsMonolith")
    archive_as_pdf: Mapped[Optional[bool]] = mapped_column("archiveAsPDF")
    archive_as_readable: Mapped[Optional[bool]] = mapped_column("archiveAsReadable")
    archive_as_wayback_machine: Mapped[Optional[bool]] = mapped_column("archiveAsWaybackMachine")
    ai_tag: Mapped[Optional[bool]] = mapped_column("aiTag")
    ai_generated: Mapped[bool] = mapped_column("aiGenerated", default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("Tag_ownerId_idx", "ownerId"),
        Index("Tag_name_ownerId_key", "name", "ownerId", unique=True),
    )

    owner: Mapped["User"] = relationship(back_populates="tags")
    links: Mapped[list["Link"]] = relationship(secondary=link_to_tag, back_populates="tags")

class Subscription(Base):
    __tablename__ = "Subscription"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    active: Mapped[bool]
    provider: Mapped[str] = mapped_column(Enum("STRIPE", "APPLE", "GOOGLE", name="SubscriptionProvider"))
    current_period_start: Mapped[datetime] = mapped_column("currentPeriodStart")
    current_period_end: Mapped[datetime] = mapped_column("currentPeriodEnd")
    quantity: Mapped[int] = mapped_column(default=1, server_default=text("1"))
    store_original_transaction_id: Mapped[Optional[str]] = mapped_column("storeOriginalTransactionId")
    store_product_id: Mapped[Optional[str]] = mapped_column("storeProductId")
    google_purchase_token: Mapped[Optional[str]] = mapped_column("googlePurchaseToken")
    store_metadata: Mapped[Optional[dict[str, Any]]] = mapped_column("storeMetadata", JSONB(none_as_null=True))
    stripe_subscription_id: Mapped[Optional[str]] = mapped_column("stripeSubscriptionId")
    user_id: Mapped[int] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="Subscription_userId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("Subscription_storeOriginalTransactionId_key", "storeOriginalTransactionId", unique=True),
        Index("Subscription_googlePurchaseToken_key", "googlePurchaseToken", unique=True),
        Index("Subscription_stripeSubscriptionId_key", "stripeSubscriptionId", unique=True),
        Index("Subscription_userId_key", "userId", unique=True),
    )

    user: Mapped["User"] = relationship(foreign_keys=[user_id], back_populates="subscriptions")
    child_users: Mapped[list["User"]] = relationship(foreign_keys="User.parent_subscription_id", back_populates="parent_subscription")

class AccessToken(Base):
    __tablename__ = "AccessToken"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    name: Mapped[str]
    user_id: Mapped[int] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="AccessToken_userId_fkey"))
    token: Mapped[str]
    revoked: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    is_session: Mapped[bool] = mapped_column("isSession", default=False, server_default=text("false"))
    expires: Mapped[datetime]
    last_used_at: Mapped[Optional[datetime]] = mapped_column("lastUsedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("AccessToken_token_key", "token", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="access_tokens")

class RssSubscription(Base):
    __tablename__ = "RssSubscription"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    url: Mapped[str]
    name: Mapped[str]
    last_build_date: Mapped[Optional[datetime]] = mapped_column("lastBuildDate")
    collection_id: Mapped[int] = mapped_column("collectionId", ForeignKey("Collection.id", ondelete="CASCADE", onupdate="CASCADE", name="RssSubscription_collectionId_fkey"))
    owner_id: Mapped[int] = mapped_column("ownerId")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    collection: Mapped["Collection"] = relationship(back_populates="rss_subscriptions")

class Highlight(Base):
    __tablename__ = "Highlight"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    color: Mapped[str]
    comment: Mapped[Optional[str]]
    link_id: Mapped[int] = mapped_column("linkId", ForeignKey("Link.id", ondelete="CASCADE", onupdate="CASCADE", name="Highlight_linkId_fkey"))
    user_id: Mapped[int] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="Highlight_userId_fkey"))
    start_offset: Mapped[int] = mapped_column("startOffset")
    end_offset: Mapped[int] = mapped_column("endOffset")
    text_: Mapped[str] = mapped_column("text")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    link: Mapped["Link"] = relationship(back_populates="highlight")
    user: Mapped["User"] = relationship(back_populates="highlights")

class DashboardSection(Base):
    __tablename__ = "DashboardSection"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    user_id: Mapped[int] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="DashboardSection_userId_fkey"))
    collection_id: Mapped[Optional[int]] = mapped_column("collectionId", ForeignKey("Collection.id", ondelete="CASCADE", onupdate="CASCADE", name="DashboardSection_collectionId_fkey"))
    type: Mapped[str] = mapped_column(Enum("STATS", "RECENT_LINKS", "PINNED_LINKS", "COLLECTION", name="DashboardSectionType"))
    order: Mapped[int]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)

    __table_args__ = (
        Index("DashboardSection_userId_collectionId_key", "userId", "collectionId", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="dashboard_sections")
    collection: Mapped[Optional["Collection"]] = relationship(back_populates="dashboard_section")

class AppMigration(Base):
    __tablename__ = "AppMigration"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    name: Mapped[str]
    status: Mapped[str] = mapped_column(Enum("APPLIED", "PENDING", "FAILED", name="AppMigrationStatus"))
    finished_at: Mapped[Optional[datetime]] = mapped_column("finishedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("AppMigration_name_key", "name", unique=True),
    )
