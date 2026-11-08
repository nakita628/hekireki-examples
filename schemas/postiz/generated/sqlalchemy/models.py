from sqlalchemy import BigInteger, Date, Dialect, Enum, ForeignKey, Index, MetaData, Text, TypeDecorator, text
from sqlalchemy.dialects.postgresql import JSONB, TIMESTAMP
from sqlalchemy.orm import DeclarativeBase, Mapped, mapped_column, relationship
from typing import Any, Optional
from datetime import datetime, date, timezone
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


class UtcDateTimeTz(TypeDecorator[datetime]):
    impl = TIMESTAMP
    cache_ok = True

    def __init__(self, precision: Optional[int] = None) -> None:
        super().__init__(timezone=True, precision=precision)

    def process_bind_param(self, value: Optional[datetime], dialect: Dialect) -> Optional[datetime]:
        return value if value is None or value.tzinfo else value.replace(tzinfo=timezone.utc)

    def process_result_value(self, value: Optional[datetime], dialect: Dialect) -> Optional[datetime]:
        return None if value is None else value.astimezone(timezone.utc)


def utc_now() -> datetime:
    return datetime.now(timezone.utc)


class Base(DeclarativeBase):
    metadata = MetaData(naming_convention={"pk": "%(table_name)s_pkey"})
    type_annotation_map = {datetime: UtcDateTime(precision=3), str: Text}


class Organization(Base):
    __tablename__ = "Organization"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    name: Mapped[str]
    description: Mapped[Optional[str]]
    api_key: Mapped[Optional[str]] = mapped_column("apiKey")
    payment_id: Mapped[Optional[str]] = mapped_column("paymentId")
    streak_since: Mapped[Optional[datetime]] = mapped_column("streakSince")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    deleted_at: Mapped[Optional[datetime]] = mapped_column("deletedAt")
    allow_trial: Mapped[bool] = mapped_column("allowTrial", default=False, server_default=text("false"))
    is_trailing: Mapped[bool] = mapped_column("isTrailing", default=False, server_default=text("false"))
    shortlink: Mapped[str] = mapped_column(Enum("ASK", "YES", "NO", name="ShortLinkPreference"), default="ASK", server_default=text("'ASK'"))

    __table_args__ = (
        Index("Organization_apiKey_idx", "apiKey"),
        Index("Organization_streakSince_idx", "streakSince"),
        Index("Organization_paymentId_idx", "paymentId"),
        Index("Organization_deletedAt_idx", "deletedAt"),
    )

    auto_post: Mapped[list["AutoPost"]] = relationship(passive_deletes="all", back_populates="organization")
    comments: Mapped[list["Comments"]] = relationship(passive_deletes="all", back_populates="organization")
    credits: Mapped[list["Credits"]] = relationship(passive_deletes="all", back_populates="organization")
    clippings: Mapped[list["Clipping"]] = relationship(passive_deletes="all", back_populates="organization")
    customers: Mapped[list["Customer"]] = relationship(passive_deletes="all", back_populates="organization")
    errors: Mapped[list["Errors"]] = relationship(passive_deletes="all", back_populates="organization")
    github: Mapped[list["GitHub"]] = relationship(passive_deletes="all", back_populates="organization")
    integration: Mapped[list["Integration"]] = relationship(passive_deletes="all", back_populates="organization")
    media: Mapped[list["Media"]] = relationship(passive_deletes="all", back_populates="organization")
    buyer_organization: Mapped[list["MessagesGroup"]] = relationship(passive_deletes="all", back_populates="buyer_organization")
    notifications: Mapped[list["Notifications"]] = relationship(passive_deletes="all", back_populates="organization")
    plugs: Mapped[list["Plugs"]] = relationship(passive_deletes="all", back_populates="organization")
    post: Mapped[list["Post"]] = relationship(foreign_keys="Post.organization_id", passive_deletes="all", back_populates="organization")
    submitted_post: Mapped[list["Post"]] = relationship(foreign_keys="Post.submitted_for_organization_id", back_populates="submitted_for_organization")
    sets: Mapped[list["Sets"]] = relationship(passive_deletes="all", back_populates="organization")
    signatures: Mapped[list["Signatures"]] = relationship(passive_deletes="all", back_populates="organization")
    tags: Mapped[list["Tags"]] = relationship(passive_deletes="all", back_populates="organization")
    third_party: Mapped[list["ThirdParty"]] = relationship(passive_deletes="all", back_populates="organization")
    used_codes: Mapped[list["UsedCodes"]] = relationship(passive_deletes="all", back_populates="organization")
    users: Mapped[list["UserOrganization"]] = relationship(passive_deletes="all", back_populates="organization")
    webhooks: Mapped[list["Webhooks"]] = relationship(passive_deletes="all", back_populates="organization")
    oauth_app: Mapped[list["OAuthApp"]] = relationship(back_populates="organization")
    oauth_authorizations: Mapped[list["OAuthAuthorization"]] = relationship(passive_deletes="all", back_populates="organization")
    subscription: Mapped[Optional["Subscription"]] = relationship(passive_deletes="all", back_populates="organization")

class Tags(Base):
    __tablename__ = "Tags"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    name: Mapped[str]
    color: Mapped[str]
    org_id: Mapped[str] = mapped_column("orgId", ForeignKey("Organization.id", ondelete="RESTRICT", onupdate="CASCADE", name="Tags_orgId_fkey"))
    deleted_at: Mapped[Optional[datetime]] = mapped_column("deletedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("Tags_orgId_idx", "orgId"),
        Index("Tags_deletedAt_idx", "deletedAt"),
    )

    organization: Mapped["Organization"] = relationship(back_populates="tags")
    posts: Mapped[list["TagsPosts"]] = relationship(passive_deletes="all", back_populates="tag")

class TagsPosts(Base):
    __tablename__ = "TagsPosts"

    post_id: Mapped[str] = mapped_column("postId", ForeignKey("Post.id", ondelete="RESTRICT", onupdate="CASCADE", name="TagsPosts_postId_fkey"), primary_key=True)
    tag_id: Mapped[str] = mapped_column("tagId", ForeignKey("Tags.id", ondelete="RESTRICT", onupdate="CASCADE", name="TagsPosts_tagId_fkey"), primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("TagsPosts_postId_tagId_key", "postId", "tagId", unique=True),
    )

    post: Mapped["Post"] = relationship(back_populates="tags")
    tag: Mapped["Tags"] = relationship(back_populates="posts")

class User(Base):
    __tablename__ = "User"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    email: Mapped[str]
    password: Mapped[Optional[str]]
    provider_name: Mapped[str] = mapped_column("providerName", Enum("LOCAL", "GITHUB", "GOOGLE", "APPLE", "FARCASTER", "WALLET", "GENERIC", name="Provider"))
    name: Mapped[Optional[str]]
    last_name: Mapped[Optional[str]] = mapped_column("lastName")
    is_super_admin: Mapped[bool] = mapped_column("isSuperAdmin", default=False, server_default=text("false"))
    bio: Mapped[Optional[str]]
    audience: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    picture_id: Mapped[Optional[str]] = mapped_column("pictureId", ForeignKey("Media.id", ondelete="SET NULL", onupdate="CASCADE", name="User_pictureId_fkey"))
    provider_id: Mapped[Optional[str]] = mapped_column("providerId")
    timezone_: Mapped[int] = mapped_column("timezone")
    timezone_name: Mapped[Optional[str]] = mapped_column("timezoneName")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    last_read_notifications: Mapped[datetime] = mapped_column("lastReadNotifications", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    invite_id: Mapped[Optional[str]] = mapped_column("inviteId")
    activated: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    account: Mapped[Optional[str]]
    connected_account: Mapped[bool] = mapped_column("connectedAccount", default=False, server_default=text("false"))
    last_online: Mapped[datetime] = mapped_column("lastOnline", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    ip: Mapped[Optional[str]]
    agent: Mapped[Optional[str]]
    deleted_at: Mapped[Optional[datetime]] = mapped_column("deletedAt")
    send_success_emails: Mapped[bool] = mapped_column("sendSuccessEmails", default=True, server_default=text("true"))
    send_failure_emails: Mapped[bool] = mapped_column("sendFailureEmails", default=True, server_default=text("true"))
    send_streak_emails: Mapped[bool] = mapped_column("sendStreakEmails", default=True, server_default=text("true"))

    __table_args__ = (
        Index("User_providerId_providerName_idx", "providerId", "providerName"),
        Index("User_lastReadNotifications_idx", "lastReadNotifications"),
        Index("User_inviteId_idx", "inviteId"),
        Index("User_account_idx", "account"),
        Index("User_lastOnline_idx", "lastOnline"),
        Index("User_pictureId_idx", "pictureId"),
        Index("User_deletedAt_idx", "deletedAt"),
        Index("User_email_providerName_key", "email", "providerName", unique=True),
    )

    picture: Mapped[Optional["Media"]] = relationship(back_populates="user_picture")
    comments: Mapped[list["Comments"]] = relationship(back_populates="user")
    items: Mapped[list["ItemUser"]] = relationship(passive_deletes="all", back_populates="user")
    group_buyer: Mapped[list["MessagesGroup"]] = relationship(foreign_keys="MessagesGroup.buyer_id", passive_deletes="all", back_populates="buyer")
    group_seller: Mapped[list["MessagesGroup"]] = relationship(foreign_keys="MessagesGroup.seller_id", passive_deletes="all", back_populates="seller")
    order_buyer: Mapped[list["Orders"]] = relationship(foreign_keys="Orders.buyer_id", passive_deletes="all", back_populates="buyer")
    order_seller: Mapped[list["Orders"]] = relationship(foreign_keys="Orders.seller_id", passive_deletes="all", back_populates="seller")
    payout_problems: Mapped[list["PayoutProblems"]] = relationship(passive_deletes="all", back_populates="user")
    organizations: Mapped[list["UserOrganization"]] = relationship(passive_deletes="all", back_populates="user")
    oauth_authorizations: Mapped[list["OAuthAuthorization"]] = relationship(passive_deletes="all", back_populates="user")
    agencies: Mapped[Optional["SocialMediaAgency"]] = relationship(passive_deletes="all", back_populates="user")

class UsedCodes(Base):
    __tablename__ = "UsedCodes"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    code: Mapped[str]
    org_id: Mapped[str] = mapped_column("orgId", ForeignKey("Organization.id", ondelete="RESTRICT", onupdate="CASCADE", name="UsedCodes_orgId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("UsedCodes_code_idx", "code"),
    )

    organization: Mapped["Organization"] = relationship(back_populates="used_codes")

class UserOrganization(Base):
    __tablename__ = "UserOrganization"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="RESTRICT", onupdate="CASCADE", name="UserOrganization_userId_fkey"))
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="RESTRICT", onupdate="CASCADE", name="UserOrganization_organizationId_fkey"))
    disabled: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    role: Mapped[str] = mapped_column(Enum("SUPERADMIN", "ADMIN", "USER", name="Role"), default="USER", server_default=text("'USER'"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("UserOrganization_organizationId_idx", "organizationId"),
        Index("UserOrganization_disabled_idx", "disabled"),
        Index("UserOrganization_userId_organizationId_key", "userId", "organizationId", unique=True),
    )

    organization: Mapped["Organization"] = relationship(back_populates="users")
    user: Mapped["User"] = relationship(back_populates="organizations")

class GitHub(Base):
    __tablename__ = "GitHub"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    login: Mapped[Optional[str]]
    name: Mapped[Optional[str]]
    token: Mapped[str]
    job_id: Mapped[Optional[str]] = mapped_column("jobId")
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="RESTRICT", onupdate="CASCADE", name="GitHub_organizationId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("GitHub_login_idx", "login"),
        Index("GitHub_organizationId_idx", "organizationId"),
    )

    organization: Mapped["Organization"] = relationship(back_populates="github")

class Trending(Base):
    __tablename__ = "Trending"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    trending_list: Mapped[str] = mapped_column("trendingList")
    language: Mapped[Optional[str]]
    hash: Mapped[str]
    date_: Mapped[datetime] = mapped_column("date")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("Trending_language_key", "language", unique=True),
        Index("Trending_hash_idx", "hash"),
    )

class TrendingLog(Base):
    __tablename__ = "TrendingLog"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    language: Mapped[Optional[str]]
    date_: Mapped[datetime] = mapped_column("date")

class ItemUser(Base):
    __tablename__ = "ItemUser"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="RESTRICT", onupdate="CASCADE", name="ItemUser_userId_fkey"))
    key: Mapped[str]

    __table_args__ = (
        Index("ItemUser_userId_idx", "userId"),
        Index("ItemUser_key_idx", "key"),
        Index("ItemUser_userId_key_key", "userId", "key", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="items")

class Star(Base):
    __tablename__ = "Star"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    stars: Mapped[int]
    total_stars: Mapped[int] = mapped_column("totalStars")
    forks: Mapped[int]
    total_forks: Mapped[int] = mapped_column("totalForks")
    login: Mapped[str]
    date_: Mapped[date] = mapped_column("date", Date, default=lambda: utc_now().date(), server_default=text("CURRENT_TIMESTAMP"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("Star_login_date_key", "login", "date", unique=True),
    )

class Media(Base):
    __tablename__ = "Media"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    name: Mapped[str]
    original_name: Mapped[Optional[str]] = mapped_column("originalName")
    path: Mapped[str]
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="RESTRICT", onupdate="CASCADE", name="Media_organizationId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    deleted_at: Mapped[Optional[datetime]] = mapped_column("deletedAt")
    file_size: Mapped[int] = mapped_column("fileSize", default=0, server_default=text("0"))
    type: Mapped[str] = mapped_column(default="image", server_default=text("'image'"))
    thumbnail: Mapped[Optional[str]]
    alt: Mapped[Optional[str]]
    thumbnail_timestamp: Mapped[Optional[int]] = mapped_column("thumbnailTimestamp")
    status: Mapped[str] = mapped_column(default="ready", server_default=text("'ready'"))
    processing_error: Mapped[Optional[str]] = mapped_column("processingError")

    __table_args__ = (
        Index("Media_name_idx", "name"),
        Index("Media_organizationId_idx", "organizationId"),
        Index("Media_type_idx", "type"),
    )

    organization: Mapped["Organization"] = relationship(back_populates="media")
    agencies: Mapped[list["SocialMediaAgency"]] = relationship(back_populates="logo")
    user_picture: Mapped[list["User"]] = relationship(back_populates="picture")
    oauth_apps: Mapped[list["OAuthApp"]] = relationship(back_populates="picture")

class SocialMediaAgency(Base):
    __tablename__ = "SocialMediaAgency"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="RESTRICT", onupdate="CASCADE", name="SocialMediaAgency_userId_fkey"))
    name: Mapped[str]
    logo_id: Mapped[Optional[str]] = mapped_column("logoId", ForeignKey("Media.id", ondelete="SET NULL", onupdate="CASCADE", name="SocialMediaAgency_logoId_fkey"))
    website: Mapped[Optional[str]]
    slug: Mapped[Optional[str]]
    facebook: Mapped[Optional[str]]
    instagram: Mapped[Optional[str]]
    twitter: Mapped[Optional[str]]
    linked_in: Mapped[Optional[str]] = mapped_column("linkedIn")
    youtube: Mapped[Optional[str]]
    tiktok: Mapped[Optional[str]]
    other_social_media: Mapped[Optional[str]] = mapped_column("otherSocialMedia")
    short_description: Mapped[str] = mapped_column("shortDescription")
    description: Mapped[str]
    approved: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    deleted_at: Mapped[Optional[datetime]] = mapped_column("deletedAt")

    __table_args__ = (
        Index("SocialMediaAgency_userId_key", "userId", unique=True),
        Index("SocialMediaAgency_userId_idx", "userId"),
        Index("SocialMediaAgency_deletedAt_idx", "deletedAt"),
        Index("SocialMediaAgency_id_idx", "id"),
    )

    logo: Mapped[Optional["Media"]] = relationship(back_populates="agencies")
    user: Mapped["User"] = relationship(back_populates="agencies")
    niches: Mapped[list["SocialMediaAgencyNiche"]] = relationship(passive_deletes="all", back_populates="agency")

class SocialMediaAgencyNiche(Base):
    __tablename__ = "SocialMediaAgencyNiche"

    agency_id: Mapped[str] = mapped_column("agencyId", ForeignKey("SocialMediaAgency.id", ondelete="RESTRICT", onupdate="CASCADE", name="SocialMediaAgencyNiche_agencyId_fkey"), primary_key=True)
    niche: Mapped[str] = mapped_column(primary_key=True)

    agency: Mapped["SocialMediaAgency"] = relationship(back_populates="niches")

class Credits(Base):
    __tablename__ = "Credits"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="RESTRICT", onupdate="CASCADE", name="Credits_organizationId_fkey"))
    credits: Mapped[int]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    type: Mapped[str] = mapped_column(default="ai_images", server_default=text("'ai_images'"))

    __table_args__ = (
        Index("Credits_organizationId_idx", "organizationId"),
        Index("Credits_createdAt_idx", "createdAt"),
    )

    organization: Mapped["Organization"] = relationship(back_populates="credits")

class Clipping(Base):
    __tablename__ = "Clipping"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="RESTRICT", onupdate="CASCADE", name="Clipping_organizationId_fkey"))
    url: Mapped[str]
    status: Mapped[str] = mapped_column(default="analysing", server_default=text("'analysing'"))
    error: Mapped[Optional[str]]
    title: Mapped[Optional[str]]
    thumbnail: Mapped[Optional[str]]
    duration: Mapped[Optional[int]]
    max_clips: Mapped[int] = mapped_column("maxClips", default=5, server_default=text("5"))
    fit: Mapped[str] = mapped_column(default="blur", server_default=text("'blur'"))
    integrations: Mapped[str]
    credits_id: Mapped[Optional[str]] = mapped_column("creditsId")
    deleted_at: Mapped[Optional[datetime]] = mapped_column("deletedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("Clipping_organizationId_idx", "organizationId"),
        Index("Clipping_deletedAt_idx", "deletedAt"),
    )

    organization: Mapped["Organization"] = relationship(back_populates="clippings")
    clips: Mapped[list["ClippingClip"]] = relationship(passive_deletes="all", back_populates="clipping")

class ClippingClip(Base):
    __tablename__ = "ClippingClip"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    clipping_id: Mapped[str] = mapped_column("clippingId", ForeignKey("Clipping.id", ondelete="RESTRICT", onupdate="CASCADE", name="ClippingClip_clippingId_fkey"))
    title: Mapped[str]
    content: Mapped[str]
    start: Mapped[float]
    end: Mapped[float]
    trim_start: Mapped[Optional[int]] = mapped_column("trimStart")
    status: Mapped[str] = mapped_column(default="pending", server_default=text("'pending'"))
    error: Mapped[Optional[str]]
    media_id: Mapped[Optional[str]] = mapped_column("mediaId")
    path: Mapped[Optional[str]]
    thumbnail: Mapped[Optional[str]]
    drafted_at: Mapped[Optional[datetime]] = mapped_column("draftedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("ClippingClip_clippingId_idx", "clippingId"),
    )

    clipping: Mapped["Clipping"] = relationship(back_populates="clips")

class Subscription(Base):
    __tablename__ = "Subscription"

    id: Mapped[str] = mapped_column(primary_key=True)
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="RESTRICT", onupdate="CASCADE", name="Subscription_organizationId_fkey"))
    subscription_tier: Mapped[str] = mapped_column("subscriptionTier", Enum("STANDARD", "PRO", "TEAM", "ULTIMATE", name="SubscriptionTier"))
    provider: Mapped[str] = mapped_column(default="stripe", server_default=text("'stripe'"))
    identifier: Mapped[Optional[str]]
    cancel_at: Mapped[Optional[datetime]] = mapped_column("cancelAt")
    period: Mapped[str] = mapped_column(Enum("MONTHLY", "YEARLY", name="Period"))
    total_channels: Mapped[int] = mapped_column("totalChannels")
    is_lifetime: Mapped[bool] = mapped_column("isLifetime", default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    deleted_at: Mapped[Optional[datetime]] = mapped_column("deletedAt")

    __table_args__ = (
        Index("Subscription_organizationId_key", "organizationId", unique=True),
        Index("Subscription_organizationId_idx", "organizationId"),
        Index("Subscription_deletedAt_idx", "deletedAt"),
    )

    organization: Mapped["Organization"] = relationship(back_populates="subscription")

class Customer(Base):
    __tablename__ = "Customer"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    name: Mapped[str]
    org_id: Mapped[str] = mapped_column("orgId", ForeignKey("Organization.id", ondelete="RESTRICT", onupdate="CASCADE", name="Customer_orgId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    deleted_at: Mapped[Optional[datetime]] = mapped_column("deletedAt")

    __table_args__ = (
        Index("Customer_orgId_name_deletedAt_key", "orgId", "name", "deletedAt", unique=True),
    )

    organization: Mapped["Organization"] = relationship(back_populates="customers")
    integrations: Mapped[list["Integration"]] = relationship(back_populates="customer")

class Integration(Base):
    __tablename__ = "Integration"

    id: Mapped[str] = mapped_column(primary_key=True)
    internal_id: Mapped[str] = mapped_column("internalId")
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="RESTRICT", onupdate="CASCADE", name="Integration_organizationId_fkey"))
    name: Mapped[str]
    custom_name: Mapped[Optional[str]] = mapped_column("customName")
    picture: Mapped[Optional[str]]
    provider_identifier: Mapped[str] = mapped_column("providerIdentifier")
    type: Mapped[str]
    token: Mapped[str]
    disabled: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    token_expiration: Mapped[Optional[datetime]] = mapped_column("tokenExpiration")
    refresh_token: Mapped[Optional[str]] = mapped_column("refreshToken")
    profile: Mapped[Optional[str]]
    deleted_at: Mapped[Optional[datetime]] = mapped_column("deletedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[Optional[datetime]] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    in_between_steps: Mapped[bool] = mapped_column("inBetweenSteps", default=False, server_default=text("false"))
    refresh_needed: Mapped[bool] = mapped_column("refreshNeeded", default=False, server_default=text("false"))
    posting_times: Mapped[str] = mapped_column("postingTimes", default="[{\"time\":120}, {\"time\":400}, {\"time\":700}]", server_default=text("'[{\"time\":120}, {\"time\":400}, {\"time\":700}]'"))
    custom_instance_details: Mapped[Optional[str]] = mapped_column("customInstanceDetails")
    customer_id: Mapped[Optional[str]] = mapped_column("customerId", ForeignKey("Customer.id", ondelete="SET NULL", onupdate="CASCADE", name="Integration_customerId_fkey"))
    root_internal_id: Mapped[Optional[str]] = mapped_column("rootInternalId")
    additional_settings: Mapped[Optional[str]] = mapped_column("additionalSettings", default="[]", server_default=text("'[]'"))

    __table_args__ = (
        Index("Integration_rootInternalId_idx", "rootInternalId"),
        Index("Integration_organizationId_idx", "organizationId"),
        Index("Integration_providerIdentifier_idx", "providerIdentifier"),
        Index("Integration_updatedAt_idx", "updatedAt"),
        Index("Integration_createdAt_idx", "createdAt"),
        Index("Integration_deletedAt_idx", "deletedAt"),
        Index("Integration_customerId_idx", "customerId"),
        Index("Integration_inBetweenSteps_idx", "inBetweenSteps"),
        Index("Integration_refreshNeeded_idx", "refreshNeeded"),
        Index("Integration_disabled_idx", "disabled"),
        Index("Integration_organizationId_internalId_key", "organizationId", "internalId", unique=True),
    )

    customer: Mapped[Optional["Customer"]] = relationship(back_populates="integrations")
    organization: Mapped["Organization"] = relationship(back_populates="integration")
    exising_plug_data: Mapped[list["ExisingPlugData"]] = relationship(passive_deletes="all", back_populates="integration")
    webhooks: Mapped[list["IntegrationsWebhooks"]] = relationship(passive_deletes="all", back_populates="integration")
    order_items: Mapped[list["OrderItems"]] = relationship(passive_deletes="all", back_populates="integration")
    plugs: Mapped[list["Plugs"]] = relationship(passive_deletes="all", back_populates="integration")
    posts: Mapped[list["Post"]] = relationship(passive_deletes="all", back_populates="integration")

class Signatures(Base):
    __tablename__ = "Signatures"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="RESTRICT", onupdate="CASCADE", name="Signatures_organizationId_fkey"))
    content: Mapped[str]
    auto_add: Mapped[bool] = mapped_column("autoAdd")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    deleted_at: Mapped[Optional[datetime]] = mapped_column("deletedAt")

    __table_args__ = (
        Index("Signatures_createdAt_idx", "createdAt"),
        Index("Signatures_organizationId_idx", "organizationId"),
        Index("Signatures_deletedAt_idx", "deletedAt"),
    )

    organization: Mapped["Organization"] = relationship(back_populates="signatures")

class Comments(Base):
    __tablename__ = "Comments"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    content: Mapped[str]
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="RESTRICT", onupdate="CASCADE", name="Comments_organizationId_fkey"))
    post_id: Mapped[str] = mapped_column("postId", ForeignKey("Post.id", ondelete="RESTRICT", onupdate="CASCADE", name="Comments_postId_fkey"))
    user_id: Mapped[Optional[str]] = mapped_column("userId", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="Comments_userId_fkey"))
    display_name: Mapped[Optional[str]] = mapped_column("displayName")
    parent_id: Mapped[Optional[str]] = mapped_column("parentId", ForeignKey("Comments.id", ondelete="SET NULL", onupdate="CASCADE", name="Comments_parentId_fkey"))
    anchor_start: Mapped[Optional[int]] = mapped_column("anchorStart")
    anchor_end: Mapped[Optional[int]] = mapped_column("anchorEnd")
    anchor_quote: Mapped[Optional[str]] = mapped_column("anchorQuote")
    resolved_at: Mapped[Optional[datetime]] = mapped_column("resolvedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    deleted_at: Mapped[Optional[datetime]] = mapped_column("deletedAt")

    __table_args__ = (
        Index("Comments_createdAt_idx", "createdAt"),
        Index("Comments_organizationId_idx", "organizationId"),
        Index("Comments_userId_idx", "userId"),
        Index("Comments_postId_idx", "postId"),
        Index("Comments_parentId_idx", "parentId"),
        Index("Comments_resolvedAt_idx", "resolvedAt"),
        Index("Comments_deletedAt_idx", "deletedAt"),
    )

    organization: Mapped["Organization"] = relationship(back_populates="comments")
    post: Mapped["Post"] = relationship(back_populates="comments")
    user: Mapped[Optional["User"]] = relationship(back_populates="comments")
    parent: Mapped[Optional["Comments"]] = relationship(remote_side=[id], back_populates="replies")
    replies: Mapped[list["Comments"]] = relationship(back_populates="parent")

class Post(Base):
    __tablename__ = "Post"

    id: Mapped[str] = mapped_column(primary_key=True)
    state: Mapped[str] = mapped_column(Enum("QUEUE", "PUBLISHED", "ERROR", "DRAFT", name="State"), default="QUEUE", server_default=text("'QUEUE'"))
    publish_date: Mapped[datetime] = mapped_column("publishDate")
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="RESTRICT", onupdate="CASCADE", name="Post_organizationId_fkey"))
    integration_id: Mapped[str] = mapped_column("integrationId", ForeignKey("Integration.id", ondelete="RESTRICT", onupdate="CASCADE", name="Post_integrationId_fkey"))
    content: Mapped[str]
    delay: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    group: Mapped[str]
    batch_id: Mapped[Optional[str]] = mapped_column("batchId")
    title: Mapped[Optional[str]]
    description: Mapped[Optional[str]]
    parent_post_id: Mapped[Optional[str]] = mapped_column("parentPostId", ForeignKey("Post.id", ondelete="SET NULL", onupdate="CASCADE", name="Post_parentPostId_fkey"))
    release_id: Mapped[Optional[str]] = mapped_column("releaseId")
    release_url: Mapped[Optional[str]] = mapped_column("releaseURL")
    settings: Mapped[Optional[str]]
    image: Mapped[Optional[str]]
    submitted_for_order_id: Mapped[Optional[str]] = mapped_column("submittedForOrderId", ForeignKey("Orders.id", ondelete="SET NULL", onupdate="CASCADE", name="Post_submittedForOrderId_fkey"))
    submitted_for_organization_id: Mapped[Optional[str]] = mapped_column("submittedForOrganizationId", ForeignKey("Organization.id", ondelete="SET NULL", onupdate="CASCADE", name="Post_submittedForOrganizationId_fkey"))
    approved_submit_for_order: Mapped[str] = mapped_column("approvedSubmitForOrder", Enum("NO", "WAITING_CONFIRMATION", "YES", name="APPROVED_SUBMIT_FOR_ORDER"), default="NO", server_default=text("'NO'"))
    creation_method: Mapped[str] = mapped_column("creationMethod", Enum("UNKNOWN", "WEB", "MCP", "API", "AUTOPOST", "CLI", name="CreationMethod"), default="UNKNOWN", server_default=text("'UNKNOWN'"))
    last_message_id: Mapped[Optional[str]] = mapped_column("lastMessageId", ForeignKey("Messages.id", ondelete="SET NULL", onupdate="CASCADE", name="Post_lastMessageId_fkey"))
    interval_in_days: Mapped[Optional[int]] = mapped_column("intervalInDays")
    error: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    deleted_at: Mapped[Optional[datetime]] = mapped_column("deletedAt")

    __table_args__ = (
        Index("Post_group_idx", "group"),
        Index("Post_batchId_idx", "batchId"),
        Index("Post_deletedAt_idx", "deletedAt"),
        Index("Post_publishDate_idx", "publishDate"),
        Index("Post_publishDate_state_deletedAt_parentPostId_integrationId_idx", "publishDate", "state", "deletedAt", "parentPostId", "integrationId", "organizationId", "creationMethod"),
        Index("Post_state_idx", "state"),
        Index("Post_organizationId_idx", "organizationId"),
        Index("Post_organizationId_publishDate_idx", "organizationId", "publishDate"),
        Index("Post_parentPostId_idx", "parentPostId"),
        Index("Post_submittedForOrderId_idx", "submittedForOrderId"),
        Index("Post_intervalInDays_idx", "intervalInDays"),
        Index("Post_approvedSubmitForOrder_idx", "approvedSubmitForOrder"),
        Index("Post_creationMethod_idx", "creationMethod"),
        Index("Post_lastMessageId_idx", "lastMessageId"),
        Index("Post_createdAt_idx", "createdAt"),
        Index("Post_updatedAt_idx", "updatedAt"),
        Index("Post_releaseURL_idx", "releaseURL"),
        Index("Post_integrationId_idx", "integrationId"),
    )

    integration: Mapped["Integration"] = relationship(back_populates="posts")
    last_message: Mapped[Optional["Messages"]] = relationship(back_populates="posts")
    organization: Mapped["Organization"] = relationship(foreign_keys=[organization_id], back_populates="post")
    parent_post: Mapped[Optional["Post"]] = relationship(remote_side=[id], back_populates="children_post")
    submitted_for_order: Mapped[Optional["Orders"]] = relationship(back_populates="posts")
    submitted_for_organization: Mapped[Optional["Organization"]] = relationship(foreign_keys=[submitted_for_organization_id], back_populates="submitted_post")
    comments: Mapped[list["Comments"]] = relationship(passive_deletes="all", back_populates="post")
    errors: Mapped[list["Errors"]] = relationship(passive_deletes="all", back_populates="post")
    payout_problems: Mapped[list["PayoutProblems"]] = relationship(back_populates="post")
    children_post: Mapped[list["Post"]] = relationship(back_populates="parent_post")
    tags: Mapped[list["TagsPosts"]] = relationship(passive_deletes="all", back_populates="post")

class Notifications(Base):
    __tablename__ = "Notifications"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="RESTRICT", onupdate="CASCADE", name="Notifications_organizationId_fkey"))
    content: Mapped[str]
    link: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    deleted_at: Mapped[Optional[datetime]] = mapped_column("deletedAt")

    __table_args__ = (
        Index("Notifications_createdAt_idx", "createdAt"),
        Index("Notifications_organizationId_idx", "organizationId"),
        Index("Notifications_organizationId_createdAt_deletedAt_idx", "organizationId", "createdAt", "deletedAt"),
        Index("Notifications_deletedAt_idx", "deletedAt"),
    )

    organization: Mapped["Organization"] = relationship(back_populates="notifications")

class MessagesGroup(Base):
    __tablename__ = "MessagesGroup"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    buyer_organization_id: Mapped[str] = mapped_column("buyerOrganizationId", ForeignKey("Organization.id", ondelete="RESTRICT", onupdate="CASCADE", name="MessagesGroup_buyerOrganizationId_fkey"))
    buyer_id: Mapped[str] = mapped_column("buyerId", ForeignKey("User.id", ondelete="RESTRICT", onupdate="CASCADE", name="MessagesGroup_buyerId_fkey"))
    seller_id: Mapped[str] = mapped_column("sellerId", ForeignKey("User.id", ondelete="RESTRICT", onupdate="CASCADE", name="MessagesGroup_sellerId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("MessagesGroup_createdAt_idx", "createdAt"),
        Index("MessagesGroup_updatedAt_idx", "updatedAt"),
        Index("MessagesGroup_buyerOrganizationId_idx", "buyerOrganizationId"),
        Index("MessagesGroup_buyerId_sellerId_key", "buyerId", "sellerId", unique=True),
    )

    buyer: Mapped["User"] = relationship(foreign_keys=[buyer_id], back_populates="group_buyer")
    buyer_organization: Mapped["Organization"] = relationship(back_populates="buyer_organization")
    seller: Mapped["User"] = relationship(foreign_keys=[seller_id], back_populates="group_seller")
    messages: Mapped[list["Messages"]] = relationship(passive_deletes="all", back_populates="group")
    orders: Mapped[list["Orders"]] = relationship(passive_deletes="all", back_populates="message_group")

class PayoutProblems(Base):
    __tablename__ = "PayoutProblems"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    status: Mapped[str]
    order_id: Mapped[str] = mapped_column("orderId", ForeignKey("Orders.id", ondelete="RESTRICT", onupdate="CASCADE", name="PayoutProblems_orderId_fkey"))
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="RESTRICT", onupdate="CASCADE", name="PayoutProblems_userId_fkey"))
    post_id: Mapped[Optional[str]] = mapped_column("postId", ForeignKey("Post.id", ondelete="SET NULL", onupdate="CASCADE", name="PayoutProblems_postId_fkey"))
    amount: Mapped[int]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    order: Mapped["Orders"] = relationship(back_populates="payout_problems")
    post: Mapped[Optional["Post"]] = relationship(back_populates="payout_problems")
    user: Mapped["User"] = relationship(back_populates="payout_problems")

class Orders(Base):
    __tablename__ = "Orders"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    buyer_id: Mapped[str] = mapped_column("buyerId", ForeignKey("User.id", ondelete="RESTRICT", onupdate="CASCADE", name="Orders_buyerId_fkey"))
    seller_id: Mapped[str] = mapped_column("sellerId", ForeignKey("User.id", ondelete="RESTRICT", onupdate="CASCADE", name="Orders_sellerId_fkey"))
    status: Mapped[str] = mapped_column(Enum("PENDING", "ACCEPTED", "CANCELED", "COMPLETED", name="OrderStatus"))
    message_group_id: Mapped[str] = mapped_column("messageGroupId", ForeignKey("MessagesGroup.id", ondelete="RESTRICT", onupdate="CASCADE", name="Orders_messageGroupId_fkey"))
    capture_id: Mapped[Optional[str]] = mapped_column("captureId")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("Orders_buyerId_idx", "buyerId"),
        Index("Orders_sellerId_idx", "sellerId"),
        Index("Orders_updatedAt_idx", "updatedAt"),
        Index("Orders_createdAt_idx", "createdAt"),
        Index("Orders_messageGroupId_idx", "messageGroupId"),
    )

    buyer: Mapped["User"] = relationship(foreign_keys=[buyer_id], back_populates="order_buyer")
    message_group: Mapped["MessagesGroup"] = relationship(back_populates="orders")
    seller: Mapped["User"] = relationship(foreign_keys=[seller_id], back_populates="order_seller")
    orders_items: Mapped[list["OrderItems"]] = relationship(passive_deletes="all", back_populates="order")
    payout_problems: Mapped[list["PayoutProblems"]] = relationship(passive_deletes="all", back_populates="order")
    posts: Mapped[list["Post"]] = relationship(back_populates="submitted_for_order")

class OrderItems(Base):
    __tablename__ = "OrderItems"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    order_id: Mapped[str] = mapped_column("orderId", ForeignKey("Orders.id", ondelete="RESTRICT", onupdate="CASCADE", name="OrderItems_orderId_fkey"))
    integration_id: Mapped[str] = mapped_column("integrationId", ForeignKey("Integration.id", ondelete="RESTRICT", onupdate="CASCADE", name="OrderItems_integrationId_fkey"))
    quantity: Mapped[int]
    price: Mapped[int]

    __table_args__ = (
        Index("OrderItems_orderId_idx", "orderId"),
        Index("OrderItems_integrationId_idx", "integrationId"),
    )

    integration: Mapped["Integration"] = relationship(back_populates="order_items")
    order: Mapped["Orders"] = relationship(back_populates="orders_items")

class Messages(Base):
    __tablename__ = "Messages"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    from_: Mapped[str] = mapped_column("from", Enum("BUYER", "SELLER", name="From"))
    content: Mapped[Optional[str]]
    group_id: Mapped[str] = mapped_column("groupId", ForeignKey("MessagesGroup.id", ondelete="RESTRICT", onupdate="CASCADE", name="Messages_groupId_fkey"))
    special: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    deleted_at: Mapped[Optional[datetime]] = mapped_column("deletedAt")

    __table_args__ = (
        Index("Messages_groupId_idx", "groupId"),
        Index("Messages_createdAt_idx", "createdAt"),
        Index("Messages_deletedAt_idx", "deletedAt"),
    )

    group: Mapped["MessagesGroup"] = relationship(back_populates="messages")
    posts: Mapped[list["Post"]] = relationship(back_populates="last_message")

class Plugs(Base):
    __tablename__ = "Plugs"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="RESTRICT", onupdate="CASCADE", name="Plugs_organizationId_fkey"))
    plug_function: Mapped[str] = mapped_column("plugFunction")
    data: Mapped[str]
    integration_id: Mapped[str] = mapped_column("integrationId", ForeignKey("Integration.id", ondelete="RESTRICT", onupdate="CASCADE", name="Plugs_integrationId_fkey"))
    activated: Mapped[bool] = mapped_column(default=True, server_default=text("true"))

    __table_args__ = (
        Index("Plugs_organizationId_idx", "organizationId"),
        Index("Plugs_plugFunction_integrationId_key", "plugFunction", "integrationId", unique=True),
    )

    integration: Mapped["Integration"] = relationship(back_populates="plugs")
    organization: Mapped["Organization"] = relationship(back_populates="plugs")

class ExisingPlugData(Base):
    __tablename__ = "ExisingPlugData"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    integration_id: Mapped[str] = mapped_column("integrationId", ForeignKey("Integration.id", ondelete="RESTRICT", onupdate="CASCADE", name="ExisingPlugData_integrationId_fkey"))
    method_name: Mapped[str] = mapped_column("methodName")
    value: Mapped[str]

    __table_args__ = (
        Index("ExisingPlugData_integrationId_methodName_value_key", "integrationId", "methodName", "value", unique=True),
    )

    integration: Mapped["Integration"] = relationship(back_populates="exising_plug_data")

class PopularPosts(Base):
    __tablename__ = "PopularPosts"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    category: Mapped[str]
    topic: Mapped[str]
    content: Mapped[str]
    hook: Mapped[str]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

class IntegrationsWebhooks(Base):
    __tablename__ = "IntegrationsWebhooks"

    integration_id: Mapped[str] = mapped_column("integrationId", ForeignKey("Integration.id", ondelete="RESTRICT", onupdate="CASCADE", name="IntegrationsWebhooks_integrationId_fkey"), primary_key=True)
    webhook_id: Mapped[str] = mapped_column("webhookId", ForeignKey("Webhooks.id", ondelete="RESTRICT", onupdate="CASCADE", name="IntegrationsWebhooks_webhookId_fkey"), primary_key=True)

    __table_args__ = (
        Index("IntegrationsWebhooks_integrationId_idx", "integrationId"),
        Index("IntegrationsWebhooks_webhookId_idx", "webhookId"),
        Index("IntegrationsWebhooks_integrationId_webhookId_key", "integrationId", "webhookId", unique=True),
    )

    integration: Mapped["Integration"] = relationship(back_populates="webhooks")
    webhook: Mapped["Webhooks"] = relationship(back_populates="integrations")

class Webhooks(Base):
    __tablename__ = "Webhooks"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    name: Mapped[str]
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="RESTRICT", onupdate="CASCADE", name="Webhooks_organizationId_fkey"))
    url: Mapped[str]
    deleted_at: Mapped[Optional[datetime]] = mapped_column("deletedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("Webhooks_organizationId_idx", "organizationId"),
        Index("Webhooks_deletedAt_idx", "deletedAt"),
    )

    organization: Mapped["Organization"] = relationship(back_populates="webhooks")
    integrations: Mapped[list["IntegrationsWebhooks"]] = relationship(passive_deletes="all", back_populates="webhook")

class AutoPost(Base):
    __tablename__ = "AutoPost"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="RESTRICT", onupdate="CASCADE", name="AutoPost_organizationId_fkey"))
    title: Mapped[str]
    content: Mapped[Optional[str]]
    on_slot: Mapped[bool] = mapped_column("onSlot")
    sync_last: Mapped[bool] = mapped_column("syncLast")
    url: Mapped[str]
    last_url: Mapped[str] = mapped_column("lastUrl")
    active: Mapped[bool]
    add_picture: Mapped[bool] = mapped_column("addPicture")
    generate_content: Mapped[bool] = mapped_column("generateContent")
    integrations: Mapped[str]
    deleted_at: Mapped[Optional[datetime]] = mapped_column("deletedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("AutoPost_deletedAt_idx", "deletedAt"),
    )

    organization: Mapped["Organization"] = relationship(back_populates="auto_post")

class Sets(Base):
    __tablename__ = "Sets"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="RESTRICT", onupdate="CASCADE", name="Sets_organizationId_fkey"))
    name: Mapped[str]
    content: Mapped[str]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("Sets_organizationId_idx", "organizationId"),
    )

    organization: Mapped["Organization"] = relationship(back_populates="sets")

class ThirdParty(Base):
    __tablename__ = "ThirdParty"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="RESTRICT", onupdate="CASCADE", name="ThirdParty_organizationId_fkey"))
    identifier: Mapped[str]
    name: Mapped[str]
    internal_id: Mapped[str] = mapped_column("internalId")
    api_key: Mapped[str] = mapped_column("apiKey")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    deleted_at: Mapped[Optional[datetime]] = mapped_column("deletedAt")

    __table_args__ = (
        Index("ThirdParty_organizationId_idx", "organizationId"),
        Index("ThirdParty_deletedAt_idx", "deletedAt"),
        Index("ThirdParty_organizationId_internalId_key", "organizationId", "internalId", unique=True),
    )

    organization: Mapped["Organization"] = relationship(back_populates="third_party")

class Errors(Base):
    __tablename__ = "Errors"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    message: Mapped[str]
    platform: Mapped[str]
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="RESTRICT", onupdate="CASCADE", name="Errors_organizationId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    post_id: Mapped[str] = mapped_column("postId", ForeignKey("Post.id", ondelete="RESTRICT", onupdate="CASCADE", name="Errors_postId_fkey"))
    body: Mapped[str] = mapped_column(default="{}", server_default=text("'{}'"))

    __table_args__ = (
        Index("Errors_organizationId_idx", "organizationId"),
        Index("Errors_createdAt_idx", "createdAt"),
        Index("Errors_createdAt_platform_idx", "createdAt", "platform"),
    )

    organization: Mapped["Organization"] = relationship(back_populates="errors")
    post: Mapped["Post"] = relationship(back_populates="errors")

class Mentions(Base):
    __tablename__ = "Mentions"

    name: Mapped[str] = mapped_column(primary_key=True)
    username: Mapped[str] = mapped_column(primary_key=True)
    platform: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    image: Mapped[str] = mapped_column(primary_key=True)

    __table_args__ = (
        Index("Mentions_createdAt_idx", "createdAt"),
    )

class MastraMessages(Base):
    __tablename__ = "mastra_messages"

    id: Mapped[str] = mapped_column(primary_key=True)
    thread_id: Mapped[str]
    content: Mapped[str]
    role: Mapped[str]
    type: Mapped[str]
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTime(precision=6))
    resource_id: Mapped[Optional[str]] = mapped_column("resourceId")
    created_at_z: Mapped[Optional[datetime]] = mapped_column("createdAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("public_mastra_messages_thread_id_createdat_idx", "thread_id", "createdAt"),
        Index("mastra_messages_thread_id_createdat_idx", "thread_id", "createdAt"),
    )

class MastraResources(Base):
    __tablename__ = "mastra_resources"

    id: Mapped[str] = mapped_column(primary_key=True)
    working_memory: Mapped[Optional[str]] = mapped_column("workingMemory")
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTime(precision=6))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", UtcDateTime(precision=6))
    created_at_z: Mapped[Optional[datetime]] = mapped_column("createdAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at_z: Mapped[Optional[datetime]] = mapped_column("updatedAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

class MastraScorers(Base):
    __tablename__ = "mastra_scorers"

    id: Mapped[str] = mapped_column(primary_key=True)
    scorer_id: Mapped[str] = mapped_column("scorerId")
    trace_id: Mapped[Optional[str]] = mapped_column("traceId")
    run_id: Mapped[str] = mapped_column("runId")
    scorer: Mapped[dict[str, Any]] = mapped_column(JSONB)
    preprocess_step_result: Mapped[Optional[dict[str, Any]]] = mapped_column("preprocessStepResult", JSONB(none_as_null=True))
    extract_step_result: Mapped[Optional[dict[str, Any]]] = mapped_column("extractStepResult", JSONB(none_as_null=True))
    analyze_step_result: Mapped[Optional[dict[str, Any]]] = mapped_column("analyzeStepResult", JSONB(none_as_null=True))
    score: Mapped[float]
    reason: Mapped[Optional[str]]
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))
    preprocess_prompt: Mapped[Optional[str]] = mapped_column("preprocessPrompt")
    extract_prompt: Mapped[Optional[str]] = mapped_column("extractPrompt")
    generate_score_prompt: Mapped[Optional[str]] = mapped_column("generateScorePrompt")
    generate_reason_prompt: Mapped[Optional[str]] = mapped_column("generateReasonPrompt")
    analyze_prompt: Mapped[Optional[str]] = mapped_column("analyzePrompt")
    reason_prompt: Mapped[Optional[str]] = mapped_column("reasonPrompt")
    input: Mapped[dict[str, Any]] = mapped_column(JSONB)
    output: Mapped[dict[str, Any]] = mapped_column(JSONB)
    additional_context: Mapped[Optional[dict[str, Any]]] = mapped_column("additionalContext", JSONB(none_as_null=True))
    runtime_context: Mapped[Optional[dict[str, Any]]] = mapped_column("runtimeContext", JSONB(none_as_null=True))
    entity_type: Mapped[Optional[str]] = mapped_column("entityType")
    entity: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    entity_id: Mapped[Optional[str]] = mapped_column("entityId")
    source: Mapped[str]
    resource_id: Mapped[Optional[str]] = mapped_column("resourceId")
    thread_id: Mapped[Optional[str]] = mapped_column("threadId")
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTime(precision=6))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", UtcDateTime(precision=6))
    created_at_z: Mapped[Optional[datetime]] = mapped_column("createdAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at_z: Mapped[Optional[datetime]] = mapped_column("updatedAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    span_id: Mapped[Optional[str]] = mapped_column("spanId")
    request_context: Mapped[Optional[dict[str, Any]]] = mapped_column("requestContext", JSONB(none_as_null=True))
    organization_id: Mapped[Optional[str]] = mapped_column("organizationId")
    project_id: Mapped[Optional[str]] = mapped_column("projectId")
    batch_id: Mapped[Optional[str]] = mapped_column("batchId")
    dataset_id: Mapped[Optional[str]] = mapped_column("datasetId")
    dataset_item_id: Mapped[Optional[str]] = mapped_column("datasetItemId")

    __table_args__ = (
        Index("public_mastra_scores_trace_id_span_id_created_at_idx", "traceId", "spanId", "createdAt"),
        Index("mastra_scores_trace_id_span_id_created_at_idx", "traceId", "spanId", "createdAt"),
    )

class MastraThreads(Base):
    __tablename__ = "mastra_threads"

    id: Mapped[str] = mapped_column(primary_key=True)
    resource_id: Mapped[str] = mapped_column("resourceId")
    title: Mapped[str]
    metadata_: Mapped[Optional[str]] = mapped_column("metadata")
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTime(precision=6))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", UtcDateTime(precision=6))
    created_at_z: Mapped[Optional[datetime]] = mapped_column("createdAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at_z: Mapped[Optional[datetime]] = mapped_column("updatedAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("public_mastra_threads_resourceid_createdat_idx", "resourceId", "createdAt"),
        Index("mastra_threads_resourceid_createdat_idx", "resourceId", "createdAt"),
    )

class MastraTraces(Base):
    __tablename__ = "mastra_traces"

    id: Mapped[str] = mapped_column(primary_key=True)
    parent_span_id: Mapped[Optional[str]] = mapped_column("parentSpanId")
    name: Mapped[str]
    trace_id: Mapped[str] = mapped_column("traceId")
    scope: Mapped[str]
    kind: Mapped[int]
    attributes: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    status: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    events: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    links: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    other: Mapped[Optional[str]]
    start_time: Mapped[int] = mapped_column("startTime", BigInteger)
    end_time: Mapped[int] = mapped_column("endTime", BigInteger)
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTime(precision=6))
    created_at_z: Mapped[Optional[datetime]] = mapped_column("createdAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("public_mastra_traces_name_starttime_idx", "name", "startTime"),
    )

class MastraWorkflowSnapshot(Base):
    __tablename__ = "mastra_workflow_snapshot"

    workflow_name: Mapped[str] = mapped_column()
    run_id: Mapped[str] = mapped_column()
    resource_id: Mapped[Optional[str]] = mapped_column("resourceId")
    snapshot: Mapped[str]
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTime(precision=6))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", UtcDateTime(precision=6))
    created_at_z: Mapped[Optional[datetime]] = mapped_column("createdAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at_z: Mapped[Optional[datetime]] = mapped_column("updatedAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("mastra_workflow_snapshot_name_createdat_idx", "workflow_name", "createdAt"),
        Index("public_mastra_workflow_snapshot_workflow_name_run_id_key", "workflow_name", "run_id", unique=True),
    )

    __mapper_args__ = {"primary_key": [workflow_name, run_id]}

class MastraAgentVersions(Base):
    __tablename__ = "mastra_agent_versions"

    id: Mapped[str] = mapped_column(primary_key=True)
    agent_id: Mapped[str] = mapped_column("agentId")
    version_number: Mapped[int] = mapped_column("versionNumber")
    name: Mapped[str]
    description: Mapped[Optional[str]]
    instructions: Mapped[str]
    model: Mapped[dict[str, Any]] = mapped_column(JSONB)
    tools: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    default_options: Mapped[Optional[dict[str, Any]]] = mapped_column("defaultOptions", JSONB(none_as_null=True))
    workflows: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    agents: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    integration_tools: Mapped[Optional[dict[str, Any]]] = mapped_column("integrationTools", JSONB(none_as_null=True))
    input_processors: Mapped[Optional[dict[str, Any]]] = mapped_column("inputProcessors", JSONB(none_as_null=True))
    output_processors: Mapped[Optional[dict[str, Any]]] = mapped_column("outputProcessors", JSONB(none_as_null=True))
    memory: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    scorers: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    mcp_clients: Mapped[Optional[dict[str, Any]]] = mapped_column("mcpClients", JSONB(none_as_null=True))
    request_context_schema: Mapped[Optional[dict[str, Any]]] = mapped_column("requestContextSchema", JSONB(none_as_null=True))
    workspace: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    skills: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    skills_format: Mapped[Optional[str]] = mapped_column("skillsFormat")
    changed_fields: Mapped[Optional[dict[str, Any]]] = mapped_column("changedFields", JSONB(none_as_null=True))
    change_message: Mapped[Optional[str]] = mapped_column("changeMessage")
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTime(precision=6))
    created_at_z: Mapped[Optional[datetime]] = mapped_column("createdAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    durable: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    browser: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    tool_providers: Mapped[Optional[dict[str, Any]]] = mapped_column("toolProviders", JSONB(none_as_null=True))

class MastraAgents(Base):
    __tablename__ = "mastra_agents"

    id: Mapped[str] = mapped_column(primary_key=True)
    status: Mapped[str]
    active_version_id: Mapped[Optional[str]] = mapped_column("activeVersionId")
    author_id: Mapped[Optional[str]] = mapped_column("authorId")
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTime(precision=6))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", UtcDateTime(precision=6))
    created_at_z: Mapped[Optional[datetime]] = mapped_column("createdAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at_z: Mapped[Optional[datetime]] = mapped_column("updatedAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    visibility: Mapped[Optional[str]]
    favorite_count: Mapped[Optional[int]] = mapped_column("favoriteCount")

class MastraBackgroundTasks(Base):
    __tablename__ = "mastra_background_tasks"

    id: Mapped[str] = mapped_column(primary_key=True)
    tool_call_id: Mapped[str]
    tool_name: Mapped[str]
    agent_id: Mapped[str]
    run_id: Mapped[str]
    thread_id: Mapped[Optional[str]]
    resource_id: Mapped[Optional[str]]
    status: Mapped[str]
    args: Mapped[dict[str, Any]] = mapped_column(JSONB)
    result: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    error: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    suspend_payload: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    retry_count: Mapped[int]
    max_retries: Mapped[int]
    timeout_ms: Mapped[int]
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTime(precision=6))
    started_at: Mapped[Optional[datetime]] = mapped_column("startedAt", UtcDateTime(precision=6))
    suspended_at: Mapped[Optional[datetime]] = mapped_column("suspendedAt", UtcDateTime(precision=6))
    completed_at: Mapped[Optional[datetime]] = mapped_column("completedAt", UtcDateTime(precision=6))
    created_at_z: Mapped[Optional[datetime]] = mapped_column("createdAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    started_at_z: Mapped[Optional[datetime]] = mapped_column("startedAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    suspended_at_z: Mapped[Optional[datetime]] = mapped_column("suspendedAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    completed_at_z: Mapped[Optional[datetime]] = mapped_column("completedAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("mastra_bg_tasks_agent_status_idx", "agent_id", "status"),
        Index("mastra_bg_tasks_status_created_at_idx", "status", "createdAt"),
        Index("mastra_bg_tasks_thread_idx", "thread_id", "createdAt"),
        Index("mastra_bg_tasks_tool_call_idx", "tool_call_id"),
    )

class MastraChannelConfig(Base):
    __tablename__ = "mastra_channel_config"

    platform: Mapped[str] = mapped_column(primary_key=True)
    data: Mapped[dict[str, Any]] = mapped_column(JSONB)
    updated_at: Mapped[datetime] = mapped_column("updatedAt", UtcDateTime(precision=6))
    updated_at_z: Mapped[Optional[datetime]] = mapped_column("updatedAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

class MastraChannelInstallations(Base):
    __tablename__ = "mastra_channel_installations"

    id: Mapped[str] = mapped_column(primary_key=True)
    platform: Mapped[str]
    agent_id: Mapped[str] = mapped_column("agentId")
    status: Mapped[str]
    webhook_id: Mapped[Optional[str]] = mapped_column("webhookId")
    data: Mapped[dict[str, Any]] = mapped_column(JSONB)
    config_hash: Mapped[Optional[str]] = mapped_column("configHash")
    error: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTime(precision=6))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", UtcDateTime(precision=6))
    created_at_z: Mapped[Optional[datetime]] = mapped_column("createdAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at_z: Mapped[Optional[datetime]] = mapped_column("updatedAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("idx_channel_installations_webhook", "webhookId", unique=True),
        Index("idx_channel_installations_platform_agent", "platform", "agentId"),
    )

class MastraDatasetItems(Base):
    __tablename__ = "mastra_dataset_items"

    id: Mapped[str] = mapped_column(primary_key=True)
    dataset_id: Mapped[str] = mapped_column("datasetId")
    dataset_version: Mapped[int] = mapped_column("datasetVersion", primary_key=True)
    valid_to: Mapped[Optional[int]] = mapped_column("validTo")
    is_deleted: Mapped[bool] = mapped_column("isDeleted")
    input: Mapped[dict[str, Any]] = mapped_column(JSONB)
    ground_truth: Mapped[Optional[dict[str, Any]]] = mapped_column("groundTruth", JSONB(none_as_null=True))
    request_context: Mapped[Optional[dict[str, Any]]] = mapped_column("requestContext", JSONB(none_as_null=True))
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))
    source: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTime(precision=6))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", UtcDateTime(precision=6))
    created_at_z: Mapped[Optional[datetime]] = mapped_column("createdAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at_z: Mapped[Optional[datetime]] = mapped_column("updatedAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    expected_trajectory: Mapped[Optional[dict[str, Any]]] = mapped_column("expectedTrajectory", JSONB(none_as_null=True))
    organization_id: Mapped[Optional[str]] = mapped_column("organizationId")
    project_id: Mapped[Optional[str]] = mapped_column("projectId")
    tool_mocks: Mapped[Optional[dict[str, Any]]] = mapped_column("toolMocks", JSONB(none_as_null=True))
    unmocked_tool_policy: Mapped[Optional[str]] = mapped_column("unmockedToolPolicy")
    scorer_ids: Mapped[Optional[dict[str, Any]]] = mapped_column("scorerIds", JSONB(none_as_null=True))
    external_id: Mapped[Optional[str]] = mapped_column("externalId")

    __table_args__ = (
        Index("idx_dataset_items_dataset_validto", "datasetId", "validTo"),
        Index("idx_dataset_items_dataset_validto_deleted", "datasetId", "validTo", "isDeleted"),
        Index("idx_dataset_items_dataset_version", "datasetId", "datasetVersion"),
        Index("idx_dataset_items_external_id_history", "datasetId", "externalId", "datasetVersion"),
        Index("idx_dataset_items_org_project", "organizationId", "projectId"),
    )

class MastraDatasetVersions(Base):
    __tablename__ = "mastra_dataset_versions"

    id: Mapped[str] = mapped_column(primary_key=True)
    dataset_id: Mapped[str] = mapped_column("datasetId")
    version: Mapped[int]
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTime(precision=6))
    created_at_z: Mapped[Optional[datetime]] = mapped_column("createdAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("idx_dataset_versions_dataset_version", "datasetId", "version"),
        Index("idx_dataset_versions_dataset_version_unique", "datasetId", "version", unique=True),
    )

class MastraDatasets(Base):
    __tablename__ = "mastra_datasets"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    description: Mapped[Optional[str]]
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))
    input_schema: Mapped[Optional[dict[str, Any]]] = mapped_column("inputSchema", JSONB(none_as_null=True))
    ground_truth_schema: Mapped[Optional[dict[str, Any]]] = mapped_column("groundTruthSchema", JSONB(none_as_null=True))
    request_context_schema: Mapped[Optional[dict[str, Any]]] = mapped_column("requestContextSchema", JSONB(none_as_null=True))
    tags: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    target_type: Mapped[Optional[str]] = mapped_column("targetType")
    target_ids: Mapped[Optional[dict[str, Any]]] = mapped_column("targetIds", JSONB(none_as_null=True))
    scorer_ids: Mapped[Optional[dict[str, Any]]] = mapped_column("scorerIds", JSONB(none_as_null=True))
    version: Mapped[int]
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTime(precision=6))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", UtcDateTime(precision=6))
    created_at_z: Mapped[Optional[datetime]] = mapped_column("createdAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at_z: Mapped[Optional[datetime]] = mapped_column("updatedAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    organization_id: Mapped[Optional[str]] = mapped_column("organizationId")
    project_id: Mapped[Optional[str]] = mapped_column("projectId")
    candidate_key: Mapped[Optional[str]] = mapped_column("candidateKey")
    candidate_id: Mapped[Optional[str]] = mapped_column("candidateId")

    __table_args__ = (
        Index("idx_datasets_candidate", "candidateKey", "candidateId"),
        Index("idx_datasets_org_project", "organizationId", "projectId"),
    )

class MastraExperimentResults(Base):
    __tablename__ = "mastra_experiment_results"

    id: Mapped[str] = mapped_column(primary_key=True)
    experiment_id: Mapped[str] = mapped_column("experimentId")
    item_id: Mapped[str] = mapped_column("itemId")
    item_dataset_version: Mapped[Optional[int]] = mapped_column("itemDatasetVersion")
    input: Mapped[dict[str, Any]] = mapped_column(JSONB)
    output: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    ground_truth: Mapped[Optional[dict[str, Any]]] = mapped_column("groundTruth", JSONB(none_as_null=True))
    error: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    started_at: Mapped[datetime] = mapped_column("startedAt", UtcDateTime(precision=6))
    completed_at: Mapped[datetime] = mapped_column("completedAt", UtcDateTime(precision=6))
    retry_count: Mapped[int] = mapped_column("retryCount")
    trace_id: Mapped[Optional[str]] = mapped_column("traceId")
    status: Mapped[Optional[str]]
    tags: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTime(precision=6))
    started_at_z: Mapped[Optional[datetime]] = mapped_column("startedAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    completed_at_z: Mapped[Optional[datetime]] = mapped_column("completedAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_at_z: Mapped[Optional[datetime]] = mapped_column("createdAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    comment: Mapped[Optional[str]]
    tool_mock_report: Mapped[Optional[dict[str, Any]]] = mapped_column("toolMockReport", JSONB(none_as_null=True))
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))
    organization_id: Mapped[Optional[str]] = mapped_column("organizationId")
    project_id: Mapped[Optional[str]] = mapped_column("projectId")
    attempt: Mapped[Optional[int]]

    __table_args__ = (
        Index("idx_experiment_results_experimentid", "experimentId"),
        Index("idx_experiment_results_org_project", "organizationId", "projectId"),
        Index("idx_experiment_results_tags_gin", "tags"),
        Index("idx_experiment_results_exp_item_attempt", "experimentId", "itemId", "attempt", unique=True),
    )

class MastraExperiments(Base):
    __tablename__ = "mastra_experiments"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[Optional[str]]
    description: Mapped[Optional[str]]
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))
    dataset_id: Mapped[Optional[str]] = mapped_column("datasetId")
    dataset_version: Mapped[Optional[int]] = mapped_column("datasetVersion")
    target_type: Mapped[str] = mapped_column("targetType")
    target_id: Mapped[str] = mapped_column("targetId")
    status: Mapped[str]
    total_items: Mapped[int] = mapped_column("totalItems")
    succeeded_count: Mapped[int] = mapped_column("succeededCount")
    failed_count: Mapped[int] = mapped_column("failedCount")
    skipped_count: Mapped[int] = mapped_column("skippedCount")
    started_at: Mapped[Optional[datetime]] = mapped_column("startedAt", UtcDateTime(precision=6))
    completed_at: Mapped[Optional[datetime]] = mapped_column("completedAt", UtcDateTime(precision=6))
    agent_version: Mapped[Optional[str]] = mapped_column("agentVersion")
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTime(precision=6))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", UtcDateTime(precision=6))
    started_at_z: Mapped[Optional[datetime]] = mapped_column("startedAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    completed_at_z: Mapped[Optional[datetime]] = mapped_column("completedAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_at_z: Mapped[Optional[datetime]] = mapped_column("createdAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at_z: Mapped[Optional[datetime]] = mapped_column("updatedAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    organization_id: Mapped[Optional[str]] = mapped_column("organizationId")
    project_id: Mapped[Optional[str]] = mapped_column("projectId")
    provenance: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    runner_attestation: Mapped[Optional[dict[str, Any]]] = mapped_column("runnerAttestation", JSONB(none_as_null=True))
    experiment_set_id: Mapped[Optional[str]] = mapped_column("experimentSetId")
    comparison_id: Mapped[Optional[str]] = mapped_column("comparisonId")
    variant_id: Mapped[Optional[str]] = mapped_column("variantId")
    trial_index: Mapped[Optional[int]] = mapped_column("trialIndex")
    scorer_ids: Mapped[Optional[dict[str, Any]]] = mapped_column("scorerIds", JSONB(none_as_null=True))

    __table_args__ = (
        Index("idx_experiments_datasetid", "datasetId"),
        Index("idx_experiments_grouping", "experimentSetId", "comparisonId", "variantId", "trialIndex"),
        Index("idx_experiments_org_project", "organizationId", "projectId"),
    )

class MastraFavorites(Base):
    __tablename__ = "mastra_favorites"

    user_id: Mapped[str] = mapped_column("userId", primary_key=True)
    entity_type: Mapped[str] = mapped_column("entityType", primary_key=True)
    entity_id: Mapped[str] = mapped_column("entityId", primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTime(precision=6))
    created_at_z: Mapped[Optional[datetime]] = mapped_column("createdAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("idx_favorites_entity", "entityType", "entityId"),
    )

class MastraKnowledgeActivity(Base):
    __tablename__ = "mastra_knowledge_activity"

    id: Mapped[str] = mapped_column(primary_key=True)
    action: Mapped[str]
    record_type: Mapped[str] = mapped_column("recordType")
    record_id: Mapped[str] = mapped_column("recordId")
    scope: Mapped[dict[str, Any]] = mapped_column(JSONB)
    scope_key: Mapped[str] = mapped_column("scopeKey")
    source_thread_id: Mapped[Optional[str]] = mapped_column("sourceThreadId")
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTime(precision=6))
    created_at_z: Mapped[Optional[datetime]] = mapped_column("createdAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("idx_knowledge_activity_latest", "id"),
    )

class MastraKnowledgeCursors(Base):
    __tablename__ = "mastra_knowledge_cursors"

    source_thread_id: Mapped[str] = mapped_column("sourceThreadId", primary_key=True)
    agent: Mapped[str] = mapped_column(primary_key=True)
    last_knowledge_id: Mapped[str] = mapped_column("lastKnowledgeId")
    updated_at: Mapped[datetime] = mapped_column("updatedAt", UtcDateTime(precision=6))
    updated_at_z: Mapped[Optional[datetime]] = mapped_column("updatedAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

class MastraKnowledgeMentions(Base):
    __tablename__ = "mastra_knowledge_mentions"

    source_type: Mapped[str] = mapped_column("sourceType", primary_key=True)
    source_id: Mapped[str] = mapped_column("sourceId", primary_key=True)
    record_id: Mapped[str] = mapped_column("recordId", primary_key=True)

    __table_args__ = (
        Index("idx_knowledge_mentions_record", "recordId", "sourceType", "sourceId"),
    )

class MastraKnowledgeNodes(Base):
    __tablename__ = "mastra_knowledge_nodes"

    id: Mapped[str] = mapped_column(primary_key=True)
    type: Mapped[str]
    name: Mapped[str]
    canonical_name: Mapped[str] = mapped_column("canonicalName")
    kind: Mapped[Optional[str]]
    content: Mapped[Optional[str]]
    description: Mapped[Optional[str]]
    scope: Mapped[dict[str, Any]] = mapped_column(JSONB)
    scope_key: Mapped[str] = mapped_column("scopeKey")
    version: Mapped[int]
    merged_into: Mapped[Optional[str]] = mapped_column("mergedInto")
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTime(precision=6))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", UtcDateTime(precision=6))
    created_at_z: Mapped[Optional[datetime]] = mapped_column("createdAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at_z: Mapped[Optional[datetime]] = mapped_column("updatedAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("idx_knowledge_nodes_scope", "scopeKey", "type"),
        Index("idx_knowledge_nodes_identity", "type", "scopeKey", "canonicalName", unique=True),
    )

class MastraKnowledgeRecords(Base):
    __tablename__ = "mastra_knowledge_records"

    id: Mapped[str] = mapped_column(primary_key=True)
    node: Mapped[str]
    text_: Mapped[str] = mapped_column("text")
    scope: Mapped[dict[str, Any]] = mapped_column(JSONB)
    scope_key: Mapped[str] = mapped_column("scopeKey")
    source_thread_id: Mapped[str] = mapped_column("sourceThreadId")
    captured_at: Mapped[datetime] = mapped_column("capturedAt", UtcDateTime(precision=6))
    when: Mapped[Optional[datetime]] = mapped_column(UtcDateTime(precision=6))
    max_scope: Mapped[Optional[str]] = mapped_column("maxScope")
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))
    deleted_at: Mapped[Optional[datetime]] = mapped_column("deletedAt", UtcDateTime(precision=6))
    deleted_by: Mapped[Optional[str]] = mapped_column("deletedBy")
    captured_at_z: Mapped[Optional[datetime]] = mapped_column("capturedAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    when_z: Mapped[Optional[datetime]] = mapped_column("whenZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    deleted_at_z: Mapped[Optional[datetime]] = mapped_column("deletedAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("idx_knowledge_records_node_latest", "node", "id"),
        Index("idx_knowledge_records_thread_latest", "sourceThreadId", "id"),
    )

class MastraKnowledgeSemanticOutbox(Base):
    __tablename__ = "mastra_knowledge_semantic_outbox"

    id: Mapped[str] = mapped_column(primary_key=True)
    idempotency_key: Mapped[str] = mapped_column("idempotencyKey")
    document_id: Mapped[str] = mapped_column("documentId")
    document_type: Mapped[str] = mapped_column("documentType")
    operation: Mapped[str]
    scope: Mapped[dict[str, Any]] = mapped_column(JSONB)
    scope_key: Mapped[str] = mapped_column("scopeKey")
    status: Mapped[str]
    attempts: Mapped[int]
    available_at: Mapped[datetime] = mapped_column("availableAt", UtcDateTime(precision=6))
    claimed_at: Mapped[Optional[datetime]] = mapped_column("claimedAt", UtcDateTime(precision=6))
    claimed_by: Mapped[Optional[str]] = mapped_column("claimedBy")
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTime(precision=6))
    completed_at: Mapped[Optional[datetime]] = mapped_column("completedAt", UtcDateTime(precision=6))
    available_at_z: Mapped[Optional[datetime]] = mapped_column("availableAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    claimed_at_z: Mapped[Optional[datetime]] = mapped_column("claimedAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_at_z: Mapped[Optional[datetime]] = mapped_column("createdAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    completed_at_z: Mapped[Optional[datetime]] = mapped_column("completedAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("idx_knowledge_outbox_idempotency", "idempotencyKey", unique=True),
        Index("idx_knowledge_outbox_claim", "status", "availableAt", "createdAt"),
    )

class MastraMcpClientVersions(Base):
    __tablename__ = "mastra_mcp_client_versions"

    id: Mapped[str] = mapped_column(primary_key=True)
    mcp_client_id: Mapped[str] = mapped_column("mcpClientId")
    version_number: Mapped[int] = mapped_column("versionNumber")
    name: Mapped[str]
    description: Mapped[Optional[str]]
    servers: Mapped[dict[str, Any]] = mapped_column(JSONB)
    changed_fields: Mapped[Optional[dict[str, Any]]] = mapped_column("changedFields", JSONB(none_as_null=True))
    change_message: Mapped[Optional[str]] = mapped_column("changeMessage")
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTime(precision=6))
    created_at_z: Mapped[Optional[datetime]] = mapped_column("createdAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("idx_mcp_client_versions_client_version", "mcpClientId", "versionNumber", unique=True),
    )

class MastraMcpClients(Base):
    __tablename__ = "mastra_mcp_clients"

    id: Mapped[str] = mapped_column(primary_key=True)
    status: Mapped[str]
    active_version_id: Mapped[Optional[str]] = mapped_column("activeVersionId")
    author_id: Mapped[Optional[str]] = mapped_column("authorId")
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTime(precision=6))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", UtcDateTime(precision=6))
    created_at_z: Mapped[Optional[datetime]] = mapped_column("createdAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at_z: Mapped[Optional[datetime]] = mapped_column("updatedAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

class MastraMcpServerVersions(Base):
    __tablename__ = "mastra_mcp_server_versions"

    id: Mapped[str] = mapped_column(primary_key=True)
    mcp_server_id: Mapped[str] = mapped_column("mcpServerId")
    version_number: Mapped[int] = mapped_column("versionNumber")
    name: Mapped[str]
    version: Mapped[str]
    description: Mapped[Optional[str]]
    instructions: Mapped[Optional[str]]
    repository: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    release_date: Mapped[Optional[str]] = mapped_column("releaseDate")
    is_latest: Mapped[Optional[bool]] = mapped_column("isLatest")
    package_canonical: Mapped[Optional[str]] = mapped_column("packageCanonical")
    tools: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    agents: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    workflows: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    changed_fields: Mapped[Optional[dict[str, Any]]] = mapped_column("changedFields", JSONB(none_as_null=True))
    change_message: Mapped[Optional[str]] = mapped_column("changeMessage")
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTime(precision=6))
    created_at_z: Mapped[Optional[datetime]] = mapped_column("createdAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("idx_mcp_server_versions_server_version", "mcpServerId", "versionNumber", unique=True),
    )

class MastraMcpServers(Base):
    __tablename__ = "mastra_mcp_servers"

    id: Mapped[str] = mapped_column(primary_key=True)
    status: Mapped[str]
    active_version_id: Mapped[Optional[str]] = mapped_column("activeVersionId")
    author_id: Mapped[Optional[str]] = mapped_column("authorId")
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTime(precision=6))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", UtcDateTime(precision=6))
    created_at_z: Mapped[Optional[datetime]] = mapped_column("createdAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at_z: Mapped[Optional[datetime]] = mapped_column("updatedAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

class MastraObservationalMemory(Base):
    __tablename__ = "mastra_observational_memory"

    id: Mapped[str] = mapped_column(primary_key=True)
    lookup_key: Mapped[str] = mapped_column("lookupKey")
    scope: Mapped[str]
    resource_id: Mapped[Optional[str]] = mapped_column("resourceId")
    thread_id: Mapped[Optional[str]] = mapped_column("threadId")
    active_observations: Mapped[str] = mapped_column("activeObservations")
    active_observations_pending_update: Mapped[Optional[str]] = mapped_column("activeObservationsPendingUpdate")
    origin_type: Mapped[str] = mapped_column("originType")
    config: Mapped[str]
    generation_count: Mapped[int] = mapped_column("generationCount")
    last_observed_at: Mapped[Optional[datetime]] = mapped_column("lastObservedAt", UtcDateTime(precision=6))
    last_reflection_at: Mapped[Optional[datetime]] = mapped_column("lastReflectionAt", UtcDateTime(precision=6))
    pending_message_tokens: Mapped[int] = mapped_column("pendingMessageTokens")
    total_tokens_observed: Mapped[int] = mapped_column("totalTokensObserved")
    observation_token_count: Mapped[int] = mapped_column("observationTokenCount")
    is_observing: Mapped[bool] = mapped_column("isObserving")
    is_reflecting: Mapped[bool] = mapped_column("isReflecting")
    observed_message_ids: Mapped[Optional[dict[str, Any]]] = mapped_column("observedMessageIds", JSONB(none_as_null=True))
    observed_timezone: Mapped[Optional[str]] = mapped_column("observedTimezone")
    buffered_observations: Mapped[Optional[str]] = mapped_column("bufferedObservations")
    buffered_observation_tokens: Mapped[Optional[int]] = mapped_column("bufferedObservationTokens")
    buffered_message_ids: Mapped[Optional[dict[str, Any]]] = mapped_column("bufferedMessageIds", JSONB(none_as_null=True))
    buffered_reflection: Mapped[Optional[str]] = mapped_column("bufferedReflection")
    buffered_reflection_tokens: Mapped[Optional[int]] = mapped_column("bufferedReflectionTokens")
    buffered_reflection_input_tokens: Mapped[Optional[int]] = mapped_column("bufferedReflectionInputTokens")
    reflected_observation_line_count: Mapped[Optional[int]] = mapped_column("reflectedObservationLineCount")
    buffered_observation_chunks: Mapped[Optional[dict[str, Any]]] = mapped_column("bufferedObservationChunks", JSONB(none_as_null=True))
    is_buffering_observation: Mapped[bool] = mapped_column("isBufferingObservation")
    is_buffering_reflection: Mapped[bool] = mapped_column("isBufferingReflection")
    last_buffered_at_tokens: Mapped[int] = mapped_column("lastBufferedAtTokens")
    last_buffered_at_time: Mapped[Optional[datetime]] = mapped_column("lastBufferedAtTime", UtcDateTime(precision=6))
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTime(precision=6))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", UtcDateTime(precision=6))
    last_observed_at_z: Mapped[Optional[datetime]] = mapped_column("lastObservedAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_reflection_at_z: Mapped[Optional[datetime]] = mapped_column("lastReflectionAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_buffered_at_time_z: Mapped[Optional[datetime]] = mapped_column("lastBufferedAtTimeZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_at_z: Mapped[Optional[datetime]] = mapped_column("createdAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at_z: Mapped[Optional[datetime]] = mapped_column("updatedAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("idx_om_lookup_key", "lookupKey"),
    )

class MastraPromptBlockVersions(Base):
    __tablename__ = "mastra_prompt_block_versions"

    id: Mapped[str] = mapped_column(primary_key=True)
    block_id: Mapped[str] = mapped_column("blockId")
    version_number: Mapped[int] = mapped_column("versionNumber")
    name: Mapped[str]
    description: Mapped[Optional[str]]
    content: Mapped[str]
    rules: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    request_context_schema: Mapped[Optional[dict[str, Any]]] = mapped_column("requestContextSchema", JSONB(none_as_null=True))
    changed_fields: Mapped[Optional[dict[str, Any]]] = mapped_column("changedFields", JSONB(none_as_null=True))
    change_message: Mapped[Optional[str]] = mapped_column("changeMessage")
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTime(precision=6))
    created_at_z: Mapped[Optional[datetime]] = mapped_column("createdAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("idx_prompt_block_versions_block_version", "blockId", "versionNumber", unique=True),
    )

class MastraPromptBlocks(Base):
    __tablename__ = "mastra_prompt_blocks"

    id: Mapped[str] = mapped_column(primary_key=True)
    status: Mapped[str]
    active_version_id: Mapped[Optional[str]] = mapped_column("activeVersionId")
    author_id: Mapped[Optional[str]] = mapped_column("authorId")
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTime(precision=6))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", UtcDateTime(precision=6))
    created_at_z: Mapped[Optional[datetime]] = mapped_column("createdAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at_z: Mapped[Optional[datetime]] = mapped_column("updatedAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

class MastraScheduleTriggers(Base):
    __tablename__ = "mastra_schedule_triggers"

    id: Mapped[str] = mapped_column(primary_key=True)
    schedule_id: Mapped[str]
    run_id: Mapped[Optional[str]]
    scheduled_fire_at: Mapped[int] = mapped_column(BigInteger)
    actual_fire_at: Mapped[int] = mapped_column(BigInteger)
    outcome: Mapped[str]
    error: Mapped[Optional[str]]
    trigger_kind: Mapped[str]
    parent_trigger_id: Mapped[Optional[str]]
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))

    __table_args__ = (
        Index("idx_mastra_schedule_triggers_schedule_fire", "schedule_id", "actual_fire_at"),
    )

class MastraSchedules(Base):
    __tablename__ = "mastra_schedules"

    id: Mapped[str] = mapped_column(primary_key=True)
    target: Mapped[dict[str, Any]] = mapped_column(JSONB)
    cron: Mapped[str]
    timezone_: Mapped[Optional[str]] = mapped_column("timezone")
    status: Mapped[str]
    next_fire_at: Mapped[int] = mapped_column(BigInteger)
    last_fire_at: Mapped[Optional[int]] = mapped_column(BigInteger)
    last_run_id: Mapped[Optional[str]]
    created_at: Mapped[int] = mapped_column(BigInteger)
    updated_at: Mapped[int] = mapped_column(BigInteger)
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))
    owner_type: Mapped[Optional[str]]
    owner_id: Mapped[Optional[str]]

    __table_args__ = (
        Index("idx_mastra_schedules_status_next_fire", "status", "next_fire_at"),
    )

class MastraScorerDefinitionVersions(Base):
    __tablename__ = "mastra_scorer_definition_versions"

    id: Mapped[str] = mapped_column(primary_key=True)
    scorer_definition_id: Mapped[str] = mapped_column("scorerDefinitionId")
    version_number: Mapped[int] = mapped_column("versionNumber")
    name: Mapped[str]
    description: Mapped[Optional[str]]
    type: Mapped[str]
    model: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    instructions: Mapped[Optional[str]]
    score_range: Mapped[Optional[dict[str, Any]]] = mapped_column("scoreRange", JSONB(none_as_null=True))
    preset_config: Mapped[Optional[dict[str, Any]]] = mapped_column("presetConfig", JSONB(none_as_null=True))
    default_sampling: Mapped[Optional[dict[str, Any]]] = mapped_column("defaultSampling", JSONB(none_as_null=True))
    changed_fields: Mapped[Optional[dict[str, Any]]] = mapped_column("changedFields", JSONB(none_as_null=True))
    change_message: Mapped[Optional[str]] = mapped_column("changeMessage")
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTime(precision=6))
    created_at_z: Mapped[Optional[datetime]] = mapped_column("createdAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("idx_scorer_definition_versions_def_version", "scorerDefinitionId", "versionNumber", unique=True),
    )

class MastraScorerDefinitions(Base):
    __tablename__ = "mastra_scorer_definitions"

    id: Mapped[str] = mapped_column(primary_key=True)
    status: Mapped[str]
    active_version_id: Mapped[Optional[str]] = mapped_column("activeVersionId")
    author_id: Mapped[Optional[str]] = mapped_column("authorId")
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTime(precision=6))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", UtcDateTime(precision=6))
    created_at_z: Mapped[Optional[datetime]] = mapped_column("createdAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at_z: Mapped[Optional[datetime]] = mapped_column("updatedAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    organization_id: Mapped[Optional[str]] = mapped_column("organizationId")
    project_id: Mapped[Optional[str]] = mapped_column("projectId")

class MastraSkillBlobs(Base):
    __tablename__ = "mastra_skill_blobs"

    hash: Mapped[str] = mapped_column(primary_key=True)
    content: Mapped[str]
    size: Mapped[int]
    mime_type: Mapped[Optional[str]] = mapped_column("mimeType")
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTime(precision=6))
    created_at_z: Mapped[Optional[datetime]] = mapped_column("createdAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

class MastraSkillVersions(Base):
    __tablename__ = "mastra_skill_versions"

    id: Mapped[str] = mapped_column(primary_key=True)
    skill_id: Mapped[str] = mapped_column("skillId")
    version_number: Mapped[int] = mapped_column("versionNumber")
    name: Mapped[str]
    description: Mapped[str]
    instructions: Mapped[str]
    license: Mapped[Optional[str]]
    compatibility: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    source: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    references: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    scripts: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    assets: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))
    tree: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    changed_fields: Mapped[Optional[dict[str, Any]]] = mapped_column("changedFields", JSONB(none_as_null=True))
    change_message: Mapped[Optional[str]] = mapped_column("changeMessage")
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTime(precision=6))
    created_at_z: Mapped[Optional[datetime]] = mapped_column("createdAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    files: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))

    __table_args__ = (
        Index("idx_skill_versions_skill_version", "skillId", "versionNumber", unique=True),
    )

class MastraSkills(Base):
    __tablename__ = "mastra_skills"

    id: Mapped[str] = mapped_column(primary_key=True)
    status: Mapped[str]
    active_version_id: Mapped[Optional[str]] = mapped_column("activeVersionId")
    author_id: Mapped[Optional[str]] = mapped_column("authorId")
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTime(precision=6))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", UtcDateTime(precision=6))
    created_at_z: Mapped[Optional[datetime]] = mapped_column("createdAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at_z: Mapped[Optional[datetime]] = mapped_column("updatedAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    visibility: Mapped[Optional[str]]
    favorite_count: Mapped[Optional[int]] = mapped_column("favoriteCount")

class MastraThreadState(Base):
    __tablename__ = "mastra_thread_state"

    thread_id: Mapped[str] = mapped_column("threadId", primary_key=True)
    type: Mapped[str] = mapped_column(primary_key=True)
    value: Mapped[dict[str, Any]] = mapped_column(JSONB)
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTime(precision=6))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", UtcDateTime(precision=6))
    created_at_z: Mapped[Optional[datetime]] = mapped_column("createdAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at_z: Mapped[Optional[datetime]] = mapped_column("updatedAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

class MastraToolProviderConnections(Base):
    __tablename__ = "mastra_tool_provider_connections"

    author_id: Mapped[str] = mapped_column("authorId", primary_key=True)
    provider_id: Mapped[str] = mapped_column("providerId", primary_key=True)
    connection_id: Mapped[str] = mapped_column("connectionId", primary_key=True)
    toolkit: Mapped[str]
    label: Mapped[Optional[str]]
    scope: Mapped[str]
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTime(precision=6))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", UtcDateTime(precision=6))
    created_at_z: Mapped[Optional[datetime]] = mapped_column("createdAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at_z: Mapped[Optional[datetime]] = mapped_column("updatedAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("idx_tool_provider_connections_author", "authorId", "providerId", "toolkit"),
    )

class MastraWorkflowDefinitions(Base):
    __tablename__ = "mastra_workflow_definitions"

    id: Mapped[str] = mapped_column(primary_key=True)
    description: Mapped[Optional[str]]
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))
    input_schema: Mapped[dict[str, Any]] = mapped_column("inputSchema", JSONB)
    output_schema: Mapped[dict[str, Any]] = mapped_column("outputSchema", JSONB)
    state_schema: Mapped[Optional[dict[str, Any]]] = mapped_column("stateSchema", JSONB(none_as_null=True))
    request_context_schema: Mapped[Optional[dict[str, Any]]] = mapped_column("requestContextSchema", JSONB(none_as_null=True))
    graph: Mapped[dict[str, Any]] = mapped_column(JSONB)
    schedule: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    status: Mapped[str]
    source: Mapped[str]
    author_id: Mapped[Optional[str]] = mapped_column("authorId")
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTime(precision=6))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", UtcDateTime(precision=6))
    created_at_z: Mapped[Optional[datetime]] = mapped_column("createdAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at_z: Mapped[Optional[datetime]] = mapped_column("updatedAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("idx_workflow_definitions_status", "status"),
    )

class MastraWorkspaceVersions(Base):
    __tablename__ = "mastra_workspace_versions"

    id: Mapped[str] = mapped_column(primary_key=True)
    workspace_id: Mapped[str] = mapped_column("workspaceId")
    version_number: Mapped[int] = mapped_column("versionNumber")
    name: Mapped[str]
    description: Mapped[Optional[str]]
    filesystem: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    sandbox: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    mounts: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    search: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    skills: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    tools: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    auto_sync: Mapped[Optional[bool]] = mapped_column("autoSync")
    operation_timeout: Mapped[Optional[int]] = mapped_column("operationTimeout")
    changed_fields: Mapped[Optional[dict[str, Any]]] = mapped_column("changedFields", JSONB(none_as_null=True))
    change_message: Mapped[Optional[str]] = mapped_column("changeMessage")
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTime(precision=6))
    created_at_z: Mapped[Optional[datetime]] = mapped_column("createdAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("idx_workspace_versions_workspace_version", "workspaceId", "versionNumber", unique=True),
    )

class MastraWorkspaces(Base):
    __tablename__ = "mastra_workspaces"

    id: Mapped[str] = mapped_column(primary_key=True)
    status: Mapped[str]
    active_version_id: Mapped[Optional[str]] = mapped_column("activeVersionId")
    author_id: Mapped[Optional[str]] = mapped_column("authorId")
    metadata_: Mapped[Optional[dict[str, Any]]] = mapped_column("metadata", JSONB(none_as_null=True))
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcDateTime(precision=6))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", UtcDateTime(precision=6))
    created_at_z: Mapped[Optional[datetime]] = mapped_column("createdAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at_z: Mapped[Optional[datetime]] = mapped_column("updatedAtZ", UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

class OAuthApp(Base):
    __tablename__ = "OAuthApp"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    organization_id: Mapped[Optional[str]] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="SET NULL", onupdate="CASCADE", name="OAuthApp_organizationId_fkey"))
    name: Mapped[str]
    description: Mapped[Optional[str]]
    picture_id: Mapped[Optional[str]] = mapped_column("pictureId", ForeignKey("Media.id", ondelete="SET NULL", onupdate="CASCADE", name="OAuthApp_pictureId_fkey"))
    redirect_url: Mapped[str] = mapped_column("redirectUrl")
    redirect_uris: Mapped[Optional[str]] = mapped_column("redirectUris")
    client_id: Mapped[str] = mapped_column("clientId")
    client_secret: Mapped[Optional[str]] = mapped_column("clientSecret")
    dynamic: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    token_endpoint_auth_method: Mapped[Optional[str]] = mapped_column("tokenEndpointAuthMethod")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    deleted_at: Mapped[Optional[datetime]] = mapped_column("deletedAt")

    __table_args__ = (
        Index("OAuthApp_clientId_key", "clientId", unique=True),
        Index("OAuthApp_clientId_idx", "clientId"),
        Index("OAuthApp_organizationId_idx", "organizationId"),
        Index("OAuthApp_deletedAt_idx", "deletedAt"),
        Index("OAuthApp_dynamic_idx", "dynamic"),
        Index("OAuthApp_organizationId_deletedAt_key", "organizationId", "deletedAt", unique=True),
    )

    organization: Mapped[Optional["Organization"]] = relationship(back_populates="oauth_app")
    picture: Mapped[Optional["Media"]] = relationship(back_populates="oauth_apps")
    authorizations: Mapped[list["OAuthAuthorization"]] = relationship(passive_deletes="all", back_populates="oauth_app")
    self_hosted_authorizations: Mapped[list["OAuthSelfHostedAuthorization"]] = relationship(passive_deletes="all", back_populates="oauth_app")

class OAuthAuthorization(Base):
    __tablename__ = "OAuthAuthorization"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    oauth_app_id: Mapped[str] = mapped_column("oauthAppId", ForeignKey("OAuthApp.id", ondelete="RESTRICT", onupdate="CASCADE", name="OAuthAuthorization_oauthAppId_fkey"))
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="RESTRICT", onupdate="CASCADE", name="OAuthAuthorization_userId_fkey"))
    organization_id: Mapped[str] = mapped_column("organizationId", ForeignKey("Organization.id", ondelete="RESTRICT", onupdate="CASCADE", name="OAuthAuthorization_organizationId_fkey"))
    access_token: Mapped[Optional[str]] = mapped_column("accessToken")
    authorization_code: Mapped[Optional[str]] = mapped_column("authorizationCode")
    code_expires_at: Mapped[Optional[datetime]] = mapped_column("codeExpiresAt")
    code_challenge: Mapped[Optional[str]] = mapped_column("codeChallenge")
    code_challenge_method: Mapped[Optional[str]] = mapped_column("codeChallengeMethod")
    redirect_uri: Mapped[Optional[str]] = mapped_column("redirectUri")
    revoked_at: Mapped[Optional[datetime]] = mapped_column("revokedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("OAuthAuthorization_accessToken_idx", "accessToken"),
        Index("OAuthAuthorization_authorizationCode_idx", "authorizationCode"),
        Index("OAuthAuthorization_oauthAppId_idx", "oauthAppId"),
        Index("OAuthAuthorization_userId_idx", "userId"),
        Index("OAuthAuthorization_organizationId_idx", "organizationId"),
        Index("OAuthAuthorization_revokedAt_idx", "revokedAt"),
        Index("OAuthAuthorization_oauthAppId_userId_organizationId_key", "oauthAppId", "userId", "organizationId", unique=True),
    )

    oauth_app: Mapped["OAuthApp"] = relationship(back_populates="authorizations")
    user: Mapped["User"] = relationship(back_populates="oauth_authorizations")
    organization: Mapped["Organization"] = relationship(back_populates="oauth_authorizations")

class OAuthSelfHostedAuthorization(Base):
    __tablename__ = "OAuthSelfHostedAuthorization"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    oauth_app_id: Mapped[str] = mapped_column("oauthAppId", ForeignKey("OAuthApp.id", ondelete="RESTRICT", onupdate="CASCADE", name="OAuthSelfHostedAuthorization_oauthAppId_fkey"))
    mcp_url: Mapped[str] = mapped_column("mcpUrl")
    api_key: Mapped[str] = mapped_column("apiKey")
    email: Mapped[Optional[str]]
    access_token: Mapped[Optional[str]] = mapped_column("accessToken")
    authorization_code: Mapped[Optional[str]] = mapped_column("authorizationCode")
    code_expires_at: Mapped[Optional[datetime]] = mapped_column("codeExpiresAt")
    code_challenge: Mapped[Optional[str]] = mapped_column("codeChallenge")
    code_challenge_method: Mapped[Optional[str]] = mapped_column("codeChallengeMethod")
    redirect_uri: Mapped[Optional[str]] = mapped_column("redirectUri")
    revoked_at: Mapped[Optional[datetime]] = mapped_column("revokedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("OAuthSelfHostedAuthorization_accessToken_idx", "accessToken"),
        Index("OAuthSelfHostedAuthorization_authorizationCode_idx", "authorizationCode"),
        Index("OAuthSelfHostedAuthorization_oauthAppId_idx", "oauthAppId"),
    )

    oauth_app: Mapped["OAuthApp"] = relationship(back_populates="self_hosted_authorizations")

class Announcement(Base):
    __tablename__ = "Announcement"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    title: Mapped[str]
    description: Mapped[str]
    color: Mapped[str] = mapped_column(Enum("INFO", "WARNING", "ERROR", name="AnnouncementColor"), default="INFO", server_default=text("'INFO'"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
