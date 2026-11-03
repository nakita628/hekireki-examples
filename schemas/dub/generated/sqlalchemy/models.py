from sqlalchemy import BigInteger, BindParameter, ColumnElement, DateTime, Dialect, Enum, ForeignKey, Index, JSON, String, Text, TypeDecorator, func, text
from sqlalchemy.dialects.mysql import DATETIME, LONGTEXT, TIMESTAMP
from sqlalchemy.orm import DeclarativeBase, Mapped, mapped_column, relationship
from typing import Any, Optional
from datetime import datetime, timezone


class UtcDateTime(TypeDecorator[datetime]):
    impl: type[DateTime] = DATETIME
    cache_ok = True

    def process_bind_param(self, value: Optional[datetime], dialect: Dialect) -> Optional[datetime]:
        if value is None or value.tzinfo is None:
            return value
        return value.astimezone(timezone.utc).replace(tzinfo=None)

    def process_result_value(self, value: Optional[datetime], dialect: Dialect) -> Optional[datetime]:
        return None if value is None else value.replace(tzinfo=timezone.utc)


class UtcTimestamp(UtcDateTime):
    impl = TIMESTAMP
    cache_ok = True

    def bind_expression(self, value: BindParameter[datetime]) -> ColumnElement[datetime]:
        return func.convert_tz(value, "+00:00", text("@@session.time_zone"), type_=self)

    def column_expression(self, column: ColumnElement[datetime]) -> ColumnElement[datetime]:
        return func.convert_tz(column, text("@@session.time_zone"), "+00:00", type_=self)


def utc_now() -> datetime:
    return datetime.now(timezone.utc)


class Base(DeclarativeBase):
    type_annotation_map = {datetime: UtcDateTime(fsp=3), str: String(191)}


class Customer(Base):
    __tablename__ = "Customer"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[Optional[str]]
    email: Mapped[Optional[str]]
    normalized_email: Mapped[Optional[str]] = mapped_column("normalizedEmail")
    avatar: Mapped[Optional[str]] = mapped_column(Text)
    external_id: Mapped[Optional[str]] = mapped_column("externalId")
    stripe_customer_id: Mapped[Optional[str]] = mapped_column("stripeCustomerId")
    link_id: Mapped[Optional[str]] = mapped_column("linkId", ForeignKey("Link.id", ondelete="SET NULL", onupdate="CASCADE", name="Customer_linkId_fkey"))
    click_id: Mapped[Optional[str]] = mapped_column("clickId")
    clicked_at: Mapped[Optional[datetime]] = mapped_column("clickedAt")
    country: Mapped[Optional[str]]
    sales: Mapped[int] = mapped_column(default=0, server_default=text("'0'"))
    sale_amount: Mapped[int] = mapped_column("saleAmount", BigInteger, default=0, server_default=text("'0'"))
    first_sale_at: Mapped[Optional[datetime]] = mapped_column("firstSaleAt")
    subscription_canceled_at: Mapped[Optional[datetime]] = mapped_column("subscriptionCanceledAt")
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="RESTRICT", onupdate="CASCADE", name="Customer_projectId_fkey"))
    project_connect_id: Mapped[Optional[str]] = mapped_column("projectConnectId")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("Customer_stripeCustomerId_key", "stripeCustomerId", unique=True),
        Index("Customer_projectId_email_idx", "projectId", "email"),
        Index("Customer_projectId_normalizedEmail_idx", "projectId", "normalizedEmail"),
        Index("Customer_projectId_createdAt_idx", "projectId", "createdAt"),
        Index("Customer_projectId_saleAmount_idx", "projectId", "saleAmount"),
        Index("Customer_projectId_firstSaleAt_idx", "projectId", "firstSaleAt"),
        Index("Customer_projectId_subscriptionCanceledAt_idx", "projectId", "subscriptionCanceledAt"),
        Index("Customer_linkId_idx", "linkId"),
        Index("Customer_country_idx", "country"),
        Index("Customer_projectId_externalId_key", "projectId", "externalId", unique=True),
        Index("Customer_email_name_idx", "email", "name"),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    project: Mapped["Project"] = relationship(back_populates="customers")
    link: Mapped[Optional["Link"]] = relationship(back_populates="customers")

class Dashboard(Base):
    __tablename__ = "Dashboard"

    id: Mapped[str] = mapped_column(primary_key=True)
    link_id: Mapped[Optional[str]] = mapped_column("linkId", ForeignKey("Link.id", ondelete="CASCADE", onupdate="CASCADE", name="Dashboard_linkId_fkey"))
    folder_id: Mapped[Optional[str]] = mapped_column("folderId", ForeignKey("Folder.id", ondelete="CASCADE", onupdate="CASCADE", name="Dashboard_folderId_fkey"))
    project_id: Mapped[Optional[str]] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="Dashboard_projectId_fkey"))
    user_id: Mapped[Optional[str]] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="Dashboard_userId_fkey"))
    do_index: Mapped[bool] = mapped_column("doIndex", default=False, server_default=text("false"))
    password: Mapped[Optional[str]]
    show_conversions: Mapped[bool] = mapped_column("showConversions", default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("Dashboard_linkId_key", "linkId", unique=True),
        Index("Dashboard_folderId_key", "folderId", unique=True),
        Index("Dashboard_projectId_idx", "projectId"),
        Index("Dashboard_userId_idx", "userId"),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    link: Mapped[Optional["Link"]] = relationship(back_populates="dashboard")
    folder: Mapped[Optional["Folder"]] = relationship(back_populates="dashboard")
    project: Mapped[Optional["Project"]] = relationship(back_populates="dashboards")
    user: Mapped[Optional["User"]] = relationship(back_populates="dashboards")

class Domain(Base):
    __tablename__ = "Domain"

    id: Mapped[str] = mapped_column(primary_key=True)
    slug: Mapped[str]
    verified: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    placeholder: Mapped[Optional[str]]
    expired_url: Mapped[Optional[str]] = mapped_column("expiredUrl", LONGTEXT)
    not_found_url: Mapped[Optional[str]] = mapped_column("notFoundUrl", LONGTEXT)
    primary: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    archived: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    last_checked: Mapped[datetime] = mapped_column("lastChecked", default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))
    logo: Mapped[Optional[str]]
    apple_app_site_association: Mapped[Optional[dict[str, Any]]] = mapped_column("appleAppSiteAssociation", JSON(none_as_null=True))
    asset_links: Mapped[Optional[dict[str, Any]]] = mapped_column("assetLinks", JSON(none_as_null=True))
    deepview_data: Mapped[Optional[dict[str, Any]]] = mapped_column("deepviewData", JSON(none_as_null=True), default=lambda: {}, server_default=text("'{}'"))
    link_retention_days: Mapped[Optional[int]] = mapped_column("linkRetentionDays")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    project_id: Mapped[Optional[str]] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="Domain_projectId_fkey"))

    __table_args__ = (
        Index("Domain_slug_key", "slug", unique=True),
        Index("Domain_projectId_idx", "projectId"),
        Index("Domain_lastChecked_idx", "lastChecked"),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    project: Mapped[Optional["Project"]] = relationship(back_populates="domains")
    links: Mapped[list["Link"]] = relationship(passive_deletes="all", back_populates="short_domain")
    registered_domain: Mapped[Optional["RegisteredDomain"]] = relationship(back_populates="domain")

class RegisteredDomain(Base):
    __tablename__ = "RegisteredDomain"

    id: Mapped[str] = mapped_column(primary_key=True)
    slug: Mapped[str]
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="RESTRICT", onupdate="CASCADE", name="RegisteredDomain_projectId_fkey"))
    domain_id: Mapped[Optional[str]] = mapped_column("domainId", ForeignKey("Domain.id", ondelete="SET NULL", onupdate="CASCADE", name="RegisteredDomain_domainId_fkey"))
    auto_renewal_disabled_at: Mapped[Optional[datetime]] = mapped_column("autoRenewalDisabledAt")
    renewal_fee: Mapped[int] = mapped_column("renewalFee", default=1200, server_default=text("'1200'"))
    expires_at: Mapped[datetime] = mapped_column("expiresAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("RegisteredDomain_domainId_key", "domainId", unique=True),
        Index("RegisteredDomain_projectId_idx", "projectId"),
        Index("RegisteredDomain_expiresAt_idx", "expiresAt"),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    project: Mapped["Project"] = relationship(back_populates="registered_domains")
    domain: Mapped[Optional["Domain"]] = relationship(back_populates="registered_domain")

class DefaultDomains(Base):
    __tablename__ = "DefaultDomains"

    id: Mapped[str] = mapped_column(primary_key=True)
    dubsh: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    chatgpt: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    sptifi: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    gitnew: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    callink: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    amznid: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    ggllink: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    figpage: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    loooooooong: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="DefaultDomains_projectId_fkey"))

    __table_args__ = (
        Index("DefaultDomains_projectId_key", "projectId", unique=True),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    project: Mapped["Project"] = relationship(back_populates="default_domains")

class Folder(Base):
    __tablename__ = "Folder"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    description: Mapped[Optional[str]] = mapped_column(Text)
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="RESTRICT", onupdate="CASCADE", name="Folder_projectId_fkey"))
    type: Mapped[str] = mapped_column(Enum("default", "mega", name="FolderType"), default="default", server_default=text("'default'"))
    access_level: Mapped[Optional[str]] = mapped_column("accessLevel", Enum("read", "write", name="FolderAccessLevel"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("Folder_projectId_idx", "projectId"),
        Index("Folder_name_projectId_key", "name", "projectId", unique=True),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    project: Mapped["Project"] = relationship(back_populates="folders")
    links: Mapped[list["Link"]] = relationship(back_populates="folder")
    users: Mapped[list["FolderUser"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="folder")
    access_requests: Mapped[list["FolderAccessRequest"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="folder")
    webhooks: Mapped[list["FolderWebhook"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="folder")
    dashboard: Mapped[Optional["Dashboard"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="folder")

class FolderUser(Base):
    __tablename__ = "FolderUser"

    id: Mapped[str] = mapped_column(primary_key=True)
    folder_id: Mapped[str] = mapped_column("folderId", ForeignKey("Folder.id", ondelete="CASCADE", onupdate="CASCADE", name="FolderUser_folderId_fkey"))
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="FolderUser_userId_fkey"))
    role: Mapped[Optional[str]] = mapped_column(Enum("owner", "editor", "viewer", name="FolderUserRole"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("FolderUser_userId_idx", "userId"),
        Index("FolderUser_folderId_userId_key", "folderId", "userId", unique=True),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    folder: Mapped["Folder"] = relationship(back_populates="users")
    user: Mapped["User"] = relationship(back_populates="folders")

class FolderAccessRequest(Base):
    __tablename__ = "FolderAccessRequest"

    id: Mapped[str] = mapped_column(primary_key=True)
    folder_id: Mapped[str] = mapped_column("folderId", ForeignKey("Folder.id", ondelete="CASCADE", onupdate="CASCADE", name="FolderAccessRequest_folderId_fkey"))
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="FolderAccessRequest_userId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))

    __table_args__ = (
        Index("FolderAccessRequest_userId_idx", "userId"),
        Index("FolderAccessRequest_folderId_userId_key", "folderId", "userId", unique=True),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    folder: Mapped["Folder"] = relationship(back_populates="access_requests")
    user: Mapped["User"] = relationship(back_populates="folder_access_requests")

class Integration(Base):
    __tablename__ = "Integration"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[Optional[str]] = mapped_column("userId", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="Integration_userId_fkey"))
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="Integration_projectId_fkey"))
    name: Mapped[str]
    slug: Mapped[str]
    description: Mapped[Optional[str]]
    readme: Mapped[Optional[str]] = mapped_column(LONGTEXT)
    developer: Mapped[str]
    website: Mapped[str]
    logo: Mapped[Optional[str]]
    screenshots: Mapped[Optional[dict[str, Any]]] = mapped_column(JSON(none_as_null=True))
    verified: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    install_url: Mapped[Optional[str]] = mapped_column("installUrl", Text)
    guide_url: Mapped[Optional[str]] = mapped_column("guideUrl", Text)
    category: Mapped[Optional[str]]
    coming_soon: Mapped[bool] = mapped_column("comingSoon", default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("Integration_slug_key", "slug", unique=True),
        Index("Integration_projectId_idx", "projectId"),
        Index("Integration_userId_idx", "userId"),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    user: Mapped[Optional["User"]] = relationship(back_populates="integrations")
    project: Mapped["Project"] = relationship(back_populates="integrations")
    installations: Mapped[list["InstalledIntegration"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="integration")
    o_auth_app: Mapped[Optional["OAuthApp"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="integration")

class InstalledIntegration(Base):
    __tablename__ = "InstalledIntegration"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="InstalledIntegration_userId_fkey"))
    integration_id: Mapped[str] = mapped_column("integrationId", ForeignKey("Integration.id", ondelete="CASCADE", onupdate="CASCADE", name="InstalledIntegration_integrationId_fkey"))
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="InstalledIntegration_projectId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    credentials: Mapped[Optional[dict[str, Any]]] = mapped_column(JSON(none_as_null=True))
    settings: Mapped[Optional[dict[str, Any]]] = mapped_column(JSON(none_as_null=True))

    __table_args__ = (
        Index("InstalledIntegration_projectId_idx", "projectId"),
        Index("InstalledIntegration_integrationId_idx", "integrationId"),
        Index("InstalledIntegration_userId_integrationId_projectId_key", "userId", "integrationId", "projectId", unique=True),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    user: Mapped["User"] = relationship(back_populates="installed_integrations")
    integration: Mapped["Integration"] = relationship(back_populates="installations")
    project: Mapped["Project"] = relationship(back_populates="installed_integrations")
    refresh_tokens: Mapped[list["OAuthRefreshToken"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="installed_integration")
    access_tokens: Mapped[list["RestrictedToken"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="installed_integration")
    webhooks: Mapped[list["Webhook"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="installed_integration")

class Invoice(Base):
    __tablename__ = "Invoice"

    id: Mapped[str] = mapped_column(primary_key=True)
    workspace_id: Mapped[str] = mapped_column("workspaceId", ForeignKey("Project.id", ondelete="RESTRICT", onupdate="CASCADE", name="Invoice_workspaceId_fkey"))
    number: Mapped[Optional[str]]
    status: Mapped[str] = mapped_column(Enum("processing", "completed", "failed", name="InvoiceStatus"), default="processing", server_default=text("'processing'"))
    type: Mapped[str] = mapped_column(Enum("domainRenewal", name="InvoiceType"), default="domainRenewal", server_default=text("'domainRenewal'"))
    payment_method: Mapped[Optional[str]] = mapped_column("paymentMethod", Enum("card", "ach", "ach_fast", "sepa", "acss", name="PaymentMethod"))
    amount: Mapped[int] = mapped_column(default=0, server_default=text("'0'"))
    fee: Mapped[int] = mapped_column(default=0, server_default=text("'0'"))
    refunded_amount: Mapped[int] = mapped_column("refundedAmount", default=0, server_default=text("'0'"))
    total: Mapped[int] = mapped_column(default=0, server_default=text("'0'"))
    receipt_url: Mapped[Optional[str]] = mapped_column("receiptUrl", Text)
    refund_reason: Mapped[Optional[str]] = mapped_column("refundReason", Text)
    failed_reason: Mapped[Optional[str]] = mapped_column("failedReason", Text)
    registered_domains: Mapped[Optional[dict[str, Any]]] = mapped_column("registeredDomains", JSON(none_as_null=True))
    stripe_charge_metadata: Mapped[Optional[dict[str, Any]]] = mapped_column("stripeChargeMetadata", JSON(none_as_null=True))
    failed_attempts: Mapped[int] = mapped_column("failedAttempts", default=0, server_default=text("'0'"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))
    paid_at: Mapped[Optional[datetime]] = mapped_column("paidAt")

    __table_args__ = (
        Index("Invoice_number_key", "number", unique=True),
        Index("Invoice_workspaceId_idx", "workspaceId"),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    workspace: Mapped["Project"] = relationship(back_populates="invoices")

class JacksonIndex(Base):
    __tablename__ = "jackson_index"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    key: Mapped[str] = mapped_column(String(250))
    store_key: Mapped[str] = mapped_column("storeKey", String(250))

    __table_args__ = (
        Index("_jackson_index_key_store", "key", "storeKey"),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

class JacksonStore(Base):
    __tablename__ = "jackson_store"

    key: Mapped[str] = mapped_column(String(250), primary_key=True)
    value: Mapped[str] = mapped_column(Text)
    iv: Mapped[Optional[str]] = mapped_column(String(64))
    tag: Mapped[Optional[str]] = mapped_column(String(64))
    namespace: Mapped[Optional[str]] = mapped_column(String(64))
    created_at: Mapped[datetime] = mapped_column("createdAt", UtcTimestamp(fsp=0), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    modified_at: Mapped[Optional[datetime]] = mapped_column("modifiedAt", UtcTimestamp(fsp=0))

    __table_args__ = (
        Index("_jackson_store_namespace", "namespace"),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

class JacksonTtl(Base):
    __tablename__ = "jackson_ttl"

    key: Mapped[str] = mapped_column(String(250), primary_key=True)
    expires_at: Mapped[int] = mapped_column("expiresAt", BigInteger)

    __table_args__ = (
        Index("_jackson_ttl_expires_at", "expiresAt"),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

class Job(Base):
    __tablename__ = "Job"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    payload: Mapped[dict[str, Any]] = mapped_column(JSON)
    options: Mapped[Optional[dict[str, Any]]] = mapped_column(JSON(none_as_null=True))
    scheduled_at: Mapped[datetime] = mapped_column("scheduledAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))
    attempts: Mapped[int] = mapped_column(default=0, server_default=text("'0'"))
    last_error: Mapped[Optional[str]] = mapped_column("lastError", Text)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("Job_scheduledAt_attempts_idx", "scheduledAt", "attempts"),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

class Link(Base):
    __tablename__ = "Link"

    id: Mapped[str] = mapped_column(primary_key=True)
    domain: Mapped[str] = mapped_column(ForeignKey("Domain.slug", ondelete="RESTRICT", onupdate="CASCADE", name="Link_domain_fkey"))
    key: Mapped[str]
    url: Mapped[str] = mapped_column(LONGTEXT)
    short_link: Mapped[str] = mapped_column("shortLink", String(400))
    archived: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    expires_at: Mapped[Optional[datetime]] = mapped_column("expiresAt")
    expired_url: Mapped[Optional[str]] = mapped_column("expiredUrl", Text)
    disabled_at: Mapped[Optional[datetime]] = mapped_column("disabledAt")
    password: Mapped[Optional[str]]
    track_conversion: Mapped[bool] = mapped_column("trackConversion", default=False, server_default=text("false"))
    proxy: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    title: Mapped[Optional[str]]
    description: Mapped[Optional[str]] = mapped_column(String(280))
    image: Mapped[Optional[str]] = mapped_column(LONGTEXT)
    video: Mapped[Optional[str]] = mapped_column(Text)
    utm_source: Mapped[Optional[str]] = mapped_column(String(255))
    utm_medium: Mapped[Optional[str]] = mapped_column(String(255))
    utm_campaign: Mapped[Optional[str]] = mapped_column(String(255))
    utm_term: Mapped[Optional[str]] = mapped_column(String(255))
    utm_content: Mapped[Optional[str]] = mapped_column(String(255))
    rewrite: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    link_retention_cleanup_disabled_at: Mapped[Optional[datetime]] = mapped_column("linkRetentionCleanupDisabledAt")
    do_index: Mapped[bool] = mapped_column("doIndex", default=False, server_default=text("false"))
    ios: Mapped[Optional[str]] = mapped_column(Text)
    android: Mapped[Optional[str]] = mapped_column(Text)
    geo: Mapped[Optional[dict[str, Any]]] = mapped_column(JSON(none_as_null=True))
    test_variants: Mapped[Optional[dict[str, Any]]] = mapped_column("testVariants", JSON(none_as_null=True))
    test_started_at: Mapped[Optional[datetime]] = mapped_column("testStartedAt")
    test_completed_at: Mapped[Optional[datetime]] = mapped_column("testCompletedAt")
    user_id: Mapped[Optional[str]] = mapped_column("userId", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="Link_userId_fkey"))
    project_id: Mapped[Optional[str]] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="Link_projectId_fkey"))
    folder_id: Mapped[Optional[str]] = mapped_column("folderId", ForeignKey("Folder.id", ondelete="SET NULL", onupdate="CASCADE", name="Link_folderId_fkey"))
    external_id: Mapped[Optional[str]] = mapped_column("externalId")
    tenant_id: Mapped[Optional[str]] = mapped_column("tenantId")
    public_stats: Mapped[bool] = mapped_column("publicStats", default=False, server_default=text("false"))
    clicks: Mapped[int] = mapped_column(default=0, server_default=text("'0'"))
    leads: Mapped[int] = mapped_column(default=0, server_default=text("'0'"))
    conversions: Mapped[int] = mapped_column(default=0, server_default=text("'0'"))
    sales: Mapped[int] = mapped_column(default=0, server_default=text("'0'"))
    sale_amount: Mapped[int] = mapped_column("saleAmount", BigInteger, default=0, server_default=text("'0'"))
    last_clicked: Mapped[Optional[datetime]] = mapped_column("lastClicked")
    last_lead_at: Mapped[Optional[datetime]] = mapped_column("lastLeadAt")
    last_conversion_at: Mapped[Optional[datetime]] = mapped_column("lastConversionAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    comments: Mapped[Optional[str]] = mapped_column(Text)

    __table_args__ = (
        Index("Link_shortLink_key", "shortLink", unique=True),
        Index("Link_projectId_tenantId_idx", "projectId", "tenantId"),
        Index("Link_projectId_url_idx", "projectId", "url"),
        Index("Link_projectId_folderId_archived_createdAt_idx", "projectId", "folderId", "archived", "createdAt"),
        Index("Link_domain_createdAt_idx", "domain", "createdAt"),
        Index("Link_folderId_idx", "folderId"),
        Index("Link_userId_idx", "userId"),
        Index("Link_domain_key_key", "domain", "key", unique=True),
        Index("Link_projectId_externalId_key", "projectId", "externalId", unique=True),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    user: Mapped[Optional["User"]] = relationship(back_populates="links")
    project: Mapped[Optional["Project"]] = relationship(back_populates="links")
    folder: Mapped[Optional["Folder"]] = relationship(back_populates="links")
    short_domain: Mapped["Domain"] = relationship(back_populates="links")
    tags: Mapped[list["LinkTag"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="link")
    webhooks: Mapped[list["LinkWebhook"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="link")
    customers: Mapped[list["Customer"]] = relationship(back_populates="link")
    dashboard: Mapped[Optional["Dashboard"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="link")

class YearInReview(Base):
    __tablename__ = "YearInReview"

    id: Mapped[str] = mapped_column(primary_key=True)
    year: Mapped[int]
    total_links: Mapped[int] = mapped_column("totalLinks")
    total_clicks: Mapped[int] = mapped_column("totalClicks")
    top_links: Mapped[dict[str, Any]] = mapped_column("topLinks", JSON)
    top_countries: Mapped[dict[str, Any]] = mapped_column("topCountries", JSON)
    workspace_id: Mapped[str] = mapped_column("workspaceId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="YearInReview_workspaceId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))
    sent_at: Mapped[Optional[datetime]] = mapped_column("sentAt")

    __table_args__ = (
        Index("YearInReview_workspaceId_idx", "workspaceId"),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    workspace: Mapped["Project"] = relationship(back_populates="year_in_reviews")

class UserNotificationPreferences(Base):
    __tablename__ = "UserNotificationPreferences"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="UserNotificationPreferences_userId_fkey"))
    dub_links: Mapped[bool] = mapped_column("dubLinks", default=True, server_default=text("true"))

    __table_args__ = (
        Index("UserNotificationPreferences_userId_key", "userId", unique=True),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    user: Mapped["User"] = relationship(back_populates="notification_preferences")

class NotificationPreference(Base):
    __tablename__ = "NotificationPreference"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_user_id: Mapped[str] = mapped_column("projectUserId", ForeignKey("ProjectUsers.id", ondelete="CASCADE", onupdate="CASCADE", name="NotificationPreference_projectUserId_fkey"))
    link_usage_summary: Mapped[bool] = mapped_column("linkUsageSummary", default=True, server_default=text("true"))
    domain_configuration_updates: Mapped[bool] = mapped_column("domainConfigurationUpdates", default=True, server_default=text("true"))

    __table_args__ = (
        Index("NotificationPreference_projectUserId_key", "projectUserId", unique=True),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    project_user: Mapped["ProjectUsers"] = relationship(back_populates="notification_preference")

class OAuthApp(Base):
    __tablename__ = "OAuthApp"

    id: Mapped[str] = mapped_column(primary_key=True)
    integration_id: Mapped[str] = mapped_column("integrationId", ForeignKey("Integration.id", ondelete="CASCADE", onupdate="CASCADE", name="OAuthApp_integrationId_fkey"))
    client_id: Mapped[str] = mapped_column("clientId")
    hashed_client_secret: Mapped[str] = mapped_column("hashedClientSecret")
    partial_client_secret: Mapped[str] = mapped_column("partialClientSecret")
    redirect_uris: Mapped[dict[str, Any]] = mapped_column("redirectUris", JSON)
    pkce: Mapped[bool] = mapped_column(default=False, server_default=text("false"))

    __table_args__ = (
        Index("OAuthApp_integrationId_key", "integrationId", unique=True),
        Index("OAuthApp_clientId_key", "clientId", unique=True),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    integration: Mapped[Optional["Integration"]] = relationship(back_populates="o_auth_app")
    o_auth_codes: Mapped[list["OAuthCode"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="o_auth_app")

class OAuthCode(Base):
    __tablename__ = "OAuthCode"

    id: Mapped[str] = mapped_column(primary_key=True)
    client_id: Mapped[str] = mapped_column("clientId", ForeignKey("OAuthApp.clientId", ondelete="CASCADE", onupdate="CASCADE", name="OAuthCode_clientId_fkey"))
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="OAuthCode_userId_fkey"))
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="OAuthCode_projectId_fkey"))
    code: Mapped[str]
    scopes: Mapped[Optional[str]] = mapped_column(Text)
    redirect_uri: Mapped[str] = mapped_column("redirectUri")
    code_challenge: Mapped[Optional[str]] = mapped_column("codeChallenge")
    code_challenge_method: Mapped[Optional[str]] = mapped_column("codeChallengeMethod")
    expires_at: Mapped[datetime] = mapped_column("expiresAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))

    __table_args__ = (
        Index("OAuthCode_code_key", "code", unique=True),
        Index("OAuthCode_clientId_idx", "clientId"),
        Index("OAuthCode_userId_idx", "userId"),
        Index("OAuthCode_projectId_idx", "projectId"),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    o_auth_app: Mapped["OAuthApp"] = relationship(back_populates="o_auth_codes")
    user: Mapped["User"] = relationship(back_populates="o_auth_codes")
    project: Mapped["Project"] = relationship(back_populates="o_auth_codes")

class OAuthRefreshToken(Base):
    __tablename__ = "OAuthRefreshToken"

    id: Mapped[str] = mapped_column(primary_key=True)
    installation_id: Mapped[str] = mapped_column("installationId", ForeignKey("InstalledIntegration.id", ondelete="CASCADE", onupdate="CASCADE", name="OAuthRefreshToken_installationId_fkey"))
    access_token_id: Mapped[str] = mapped_column("accessTokenId", ForeignKey("RestrictedToken.id", ondelete="CASCADE", onupdate="CASCADE", name="OAuthRefreshToken_accessTokenId_fkey"))
    hashed_refresh_token: Mapped[str] = mapped_column("hashedRefreshToken")
    expires_at: Mapped[datetime] = mapped_column("expiresAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))

    __table_args__ = (
        Index("OAuthRefreshToken_hashedRefreshToken_key", "hashedRefreshToken", unique=True),
        Index("OAuthRefreshToken_installationId_idx", "installationId"),
        Index("OAuthRefreshToken_accessTokenId_idx", "accessTokenId"),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    access_token: Mapped["RestrictedToken"] = relationship(back_populates="refresh_tokens")
    installed_integration: Mapped["InstalledIntegration"] = relationship(back_populates="refresh_tokens")

class User(Base):
    __tablename__ = "User"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[Optional[str]]
    email: Mapped[Optional[str]]
    email_verified: Mapped[Optional[datetime]] = mapped_column("emailVerified")
    image: Mapped[Optional[str]]
    is_machine: Mapped[bool] = mapped_column("isMachine", default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))
    password_hash: Mapped[Optional[str]] = mapped_column("passwordHash")
    invalid_login_attempts: Mapped[int] = mapped_column("invalidLoginAttempts", default=0, server_default=text("'0'"))
    locked_at: Mapped[Optional[datetime]] = mapped_column("lockedAt")
    default_workspace: Mapped[Optional[str]] = mapped_column("defaultWorkspace")
    source: Mapped[Optional[str]]
    sent_mail: Mapped[bool] = mapped_column("sentMail", default=False, server_default=text("false"))

    __table_args__ = (
        Index("User_email_key", "email", unique=True),
        Index("User_createdAt_idx", "createdAt"),
        Index("User_defaultWorkspace_idx", "defaultWorkspace"),
        Index("User_source_idx", "source"),
        Index("User_sentMail_idx", "sentMail"),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    accounts: Mapped[list["Account"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    sessions: Mapped[list["Session"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    projects: Mapped[list["ProjectUsers"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    links: Mapped[list["Link"]] = relationship(back_populates="user")
    dashboards: Mapped[list["Dashboard"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    tokens: Mapped[list["Token"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    restricted_tokens: Mapped[list["RestrictedToken"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    o_auth_codes: Mapped[list["OAuthCode"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    integrations: Mapped[list["Integration"]] = relationship(back_populates="user")
    installed_integrations: Mapped[list["InstalledIntegration"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    folders: Mapped[list["FolderUser"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    folder_access_requests: Mapped[list["FolderAccessRequest"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    utm_templates: Mapped[list["UtmTemplate"]] = relationship(back_populates="user")
    notification_preferences: Mapped[Optional["UserNotificationPreferences"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")

class Account(Base):
    __tablename__ = "Account"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="Account_userId_fkey"))
    type: Mapped[str]
    provider: Mapped[str]
    provider_account_id: Mapped[str] = mapped_column("providerAccountId")
    refresh_token: Mapped[Optional[str]] = mapped_column(Text)
    refresh_token_expires_in: Mapped[Optional[int]]
    access_token: Mapped[Optional[str]] = mapped_column(Text)
    expires_at: Mapped[Optional[int]]
    token_type: Mapped[Optional[str]]
    scope: Mapped[Optional[str]]
    id_token: Mapped[Optional[str]] = mapped_column(Text)
    session_state: Mapped[Optional[str]]

    __table_args__ = (
        Index("Account_userId_idx", "userId"),
        Index("Account_provider_providerAccountId_key", "provider", "providerAccountId", unique=True),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    user: Mapped["User"] = relationship(back_populates="accounts")

class Session(Base):
    __tablename__ = "Session"

    id: Mapped[str] = mapped_column(primary_key=True)
    session_token: Mapped[str] = mapped_column("sessionToken")
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="Session_userId_fkey"))
    expires: Mapped[datetime]

    __table_args__ = (
        Index("Session_sessionToken_key", "sessionToken", unique=True),
        Index("Session_userId_idx", "userId"),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    user: Mapped["User"] = relationship(back_populates="sessions")

class Tag(Base):
    __tablename__ = "Tag"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    color: Mapped[str] = mapped_column(default="blue", server_default=text("'blue'"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    project_id: Mapped[Optional[str]] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="Tag_projectId_fkey"))

    __table_args__ = (
        Index("Tag_projectId_idx", "projectId"),
        Index("Tag_name_projectId_key", "name", "projectId", unique=True),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    project: Mapped[Optional["Project"]] = relationship(back_populates="tags")
    links: Mapped[list["LinkTag"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="tag")

class LinkTag(Base):
    __tablename__ = "LinkTag"

    id: Mapped[str] = mapped_column(primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    link_id: Mapped[str] = mapped_column("linkId", ForeignKey("Link.id", ondelete="CASCADE", onupdate="CASCADE", name="LinkTag_linkId_fkey"))
    tag_id: Mapped[str] = mapped_column("tagId", ForeignKey("Tag.id", ondelete="CASCADE", onupdate="CASCADE", name="LinkTag_tagId_fkey"))

    __table_args__ = (
        Index("LinkTag_tagId_idx", "tagId"),
        Index("LinkTag_linkId_tagId_key", "linkId", "tagId", unique=True),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    link: Mapped["Link"] = relationship(back_populates="tags")
    tag: Mapped["Tag"] = relationship(back_populates="links")

class Token(Base):
    __tablename__ = "Token"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    hashed_key: Mapped[str] = mapped_column("hashedKey")
    partial_key: Mapped[str] = mapped_column("partialKey")
    expires: Mapped[Optional[datetime]]
    last_used: Mapped[Optional[datetime]] = mapped_column("lastUsed")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="Token_userId_fkey"))

    __table_args__ = (
        Index("Token_hashedKey_key", "hashedKey", unique=True),
        Index("Token_userId_idx", "userId"),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    user: Mapped["User"] = relationship(back_populates="tokens")

class RestrictedToken(Base):
    __tablename__ = "RestrictedToken"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    hashed_key: Mapped[str] = mapped_column("hashedKey")
    partial_key: Mapped[str] = mapped_column("partialKey")
    scopes: Mapped[Optional[str]] = mapped_column(Text)
    expires: Mapped[Optional[datetime]]
    last_used: Mapped[Optional[datetime]] = mapped_column("lastUsed")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="RestrictedToken_userId_fkey"))
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="RestrictedToken_projectId_fkey"))
    installation_id: Mapped[Optional[str]] = mapped_column("installationId", ForeignKey("InstalledIntegration.id", ondelete="CASCADE", onupdate="CASCADE", name="RestrictedToken_installationId_fkey"))

    __table_args__ = (
        Index("RestrictedToken_hashedKey_key", "hashedKey", unique=True),
        Index("RestrictedToken_userId_idx", "userId"),
        Index("RestrictedToken_projectId_idx", "projectId"),
        Index("RestrictedToken_installationId_idx", "installationId"),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    user: Mapped["User"] = relationship(back_populates="restricted_tokens")
    project: Mapped["Project"] = relationship(back_populates="restricted_tokens")
    installed_integration: Mapped[Optional["InstalledIntegration"]] = relationship(back_populates="access_tokens")
    refresh_tokens: Mapped[list["OAuthRefreshToken"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="access_token")

class UtmTemplate(Base):
    __tablename__ = "UtmTemplate"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    utm_source: Mapped[Optional[str]] = mapped_column(String(255))
    utm_medium: Mapped[Optional[str]] = mapped_column(String(255))
    utm_campaign: Mapped[Optional[str]] = mapped_column(String(255))
    utm_term: Mapped[Optional[str]] = mapped_column(String(255))
    utm_content: Mapped[Optional[str]] = mapped_column(String(255))
    ref: Mapped[Optional[str]] = mapped_column(String(255))
    user_id: Mapped[Optional[str]] = mapped_column("userId", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="UtmTemplate_userId_fkey"))
    project_id: Mapped[Optional[str]] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="UtmTemplate_projectId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("UtmTemplate_userId_idx", "userId"),
        Index("UtmTemplate_projectId_name_key", "projectId", "name", unique=True),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    user: Mapped[Optional["User"]] = relationship(back_populates="utm_templates")
    project: Mapped[Optional["Project"]] = relationship(back_populates="utm_templates")

class Webhook(Base):
    __tablename__ = "Webhook"

    id: Mapped[str] = mapped_column(primary_key=True)
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="Webhook_projectId_fkey"))
    installation_id: Mapped[Optional[str]] = mapped_column("installationId", ForeignKey("InstalledIntegration.id", ondelete="CASCADE", onupdate="CASCADE", name="Webhook_installationId_fkey"))
    receiver: Mapped[str] = mapped_column(Enum("user", "zapier", "make", "slack", "segment", name="WebhookReceiver"), default="user", server_default=text("'user'"))
    name: Mapped[str]
    url: Mapped[str] = mapped_column(LONGTEXT)
    secret: Mapped[str]
    triggers: Mapped[dict[str, Any]] = mapped_column(JSON)
    link_scope: Mapped[Optional[str]] = mapped_column("linkScope", Enum("workspace", "folders", "links", name="LinkScope"))
    consecutive_failures: Mapped[int] = mapped_column("consecutiveFailures", default=0, server_default=text("'0'"))
    last_failed_at: Mapped[Optional[datetime]] = mapped_column("lastFailedAt")
    disabled_at: Mapped[Optional[datetime]] = mapped_column("disabledAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("Webhook_projectId_idx", "projectId"),
        Index("Webhook_installationId_idx", "installationId"),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    project: Mapped["Project"] = relationship(back_populates="webhooks")
    installed_integration: Mapped[Optional["InstalledIntegration"]] = relationship(back_populates="webhooks")
    links: Mapped[list["LinkWebhook"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="webhook")
    folders: Mapped[list["FolderWebhook"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="webhook")

class LinkWebhook(Base):
    __tablename__ = "LinkWebhook"

    id: Mapped[str] = mapped_column(primary_key=True)
    link_id: Mapped[str] = mapped_column("linkId", ForeignKey("Link.id", ondelete="CASCADE", onupdate="CASCADE", name="LinkWebhook_linkId_fkey"))
    webhook_id: Mapped[str] = mapped_column("webhookId", ForeignKey("Webhook.id", ondelete="CASCADE", onupdate="CASCADE", name="LinkWebhook_webhookId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))

    __table_args__ = (
        Index("LinkWebhook_webhookId_idx", "webhookId"),
        Index("LinkWebhook_linkId_webhookId_key", "linkId", "webhookId", unique=True),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    link: Mapped["Link"] = relationship(back_populates="webhooks")
    webhook: Mapped["Webhook"] = relationship(back_populates="links")

class FolderWebhook(Base):
    __tablename__ = "FolderWebhook"

    id: Mapped[str] = mapped_column(primary_key=True)
    folder_id: Mapped[str] = mapped_column("folderId", ForeignKey("Folder.id", ondelete="CASCADE", onupdate="CASCADE", name="FolderWebhook_folderId_fkey"))
    webhook_id: Mapped[str] = mapped_column("webhookId", ForeignKey("Webhook.id", ondelete="CASCADE", onupdate="CASCADE", name="FolderWebhook_webhookId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))

    __table_args__ = (
        Index("FolderWebhook_webhookId_idx", "webhookId"),
        Index("FolderWebhook_folderId_webhookId_key", "folderId", "webhookId", unique=True),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    folder: Mapped["Folder"] = relationship(back_populates="webhooks")
    webhook: Mapped["Webhook"] = relationship(back_populates="folders")

class Project(Base):
    __tablename__ = "Project"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    slug: Mapped[str]
    logo: Mapped[Optional[str]]
    invite_code: Mapped[Optional[str]] = mapped_column("inviteCode")
    plan: Mapped[str] = mapped_column(default="free", server_default=text("'free'"))
    plan_tier: Mapped[int] = mapped_column("planTier", default=1, server_default=text("'1'"))
    plan_period: Mapped[Optional[str]] = mapped_column("planPeriod", Enum("monthly", "yearly", "quarterly", "biweekly", name="PlanPeriod"))
    stripe_id: Mapped[Optional[str]] = mapped_column("stripeId")
    billing_cycle_start: Mapped[int] = mapped_column("billingCycleStart")
    billing_cycle_ends_at: Mapped[Optional[datetime]] = mapped_column("billingCycleEndsAt")
    trial_ends_at: Mapped[Optional[datetime]] = mapped_column("trialEndsAt")
    subscription_canceled_at: Mapped[Optional[datetime]] = mapped_column("subscriptionCanceledAt")
    payment_failed_at: Mapped[Optional[datetime]] = mapped_column("paymentFailedAt")
    invoice_prefix: Mapped[Optional[str]] = mapped_column("invoicePrefix")
    stripe_connect_id: Mapped[Optional[str]] = mapped_column("stripeConnectId")
    stripe_connect_mode: Mapped[Optional[str]] = mapped_column("stripeConnectMode", Enum("live", "test", "sandbox", name="StripeConnectMode"))
    shopify_store_id: Mapped[Optional[str]] = mapped_column("shopifyStoreId")
    total_links: Mapped[int] = mapped_column("totalLinks", default=0, server_default=text("'0'"))
    total_clicks: Mapped[int] = mapped_column("totalClicks", default=0, server_default=text("'0'"))
    usage: Mapped[int] = mapped_column(default=0, server_default=text("'0'"))
    usage_limit: Mapped[int] = mapped_column("usageLimit", default=1000, server_default=text("'1000'"))
    links_usage: Mapped[int] = mapped_column("linksUsage", default=0, server_default=text("'0'"))
    links_limit: Mapped[int] = mapped_column("linksLimit", default=25, server_default=text("'25'"))
    domains_limit: Mapped[int] = mapped_column("domainsLimit", default=3, server_default=text("'3'"))
    tags_limit: Mapped[int] = mapped_column("tagsLimit", default=5, server_default=text("'5'"))
    folders_usage: Mapped[int] = mapped_column("foldersUsage", default=0, server_default=text("'0'"))
    folders_limit: Mapped[int] = mapped_column("foldersLimit", default=0, server_default=text("'0'"))
    users_limit: Mapped[int] = mapped_column("usersLimit", default=1, server_default=text("'1'"))
    ai_usage: Mapped[int] = mapped_column("aiUsage", default=0, server_default=text("'0'"))
    ai_limit: Mapped[int] = mapped_column("aiLimit", default=10, server_default=text("'10'"))
    store: Mapped[Optional[dict[str, Any]]] = mapped_column(JSON(none_as_null=True))
    site_visit_tracking_settings: Mapped[Optional[dict[str, Any]]] = mapped_column("siteVisitTrackingSettings", JSON(none_as_null=True))
    allowed_hostnames: Mapped[Optional[dict[str, Any]]] = mapped_column("allowedHostnames", JSON(none_as_null=True))
    publishable_key: Mapped[Optional[str]] = mapped_column("publishableKey")
    conversion_enabled: Mapped[bool] = mapped_column("conversionEnabled", default=False, server_default=text("false"))
    webhook_enabled: Mapped[bool] = mapped_column("webhookEnabled", default=False, server_default=text("false"))
    dot_link_claimed: Mapped[bool] = mapped_column("dotLinkClaimed", default=False, server_default=text("false"))
    sso_email_domain: Mapped[Optional[str]] = mapped_column("ssoEmailDomain")
    sso_enforced_at: Mapped[Optional[datetime]] = mapped_column("ssoEnforcedAt")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    usage_last_checked: Mapped[datetime] = mapped_column("usageLastChecked", default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))
    disabled_at: Mapped[Optional[datetime]] = mapped_column("disabledAt")

    __table_args__ = (
        Index("Project_slug_key", "slug", unique=True),
        Index("Project_inviteCode_key", "inviteCode", unique=True),
        Index("Project_stripeId_key", "stripeId", unique=True),
        Index("Project_invoicePrefix_key", "invoicePrefix", unique=True),
        Index("Project_shopifyStoreId_key", "shopifyStoreId", unique=True),
        Index("Project_publishableKey_key", "publishableKey", unique=True),
        Index("Project_ssoEmailDomain_key", "ssoEmailDomain", unique=True),
        Index("Project_usageLastChecked_idx", "usageLastChecked"),
        Index("Project_trialEndsAt_idx", "trialEndsAt"),
        Index("Project_stripeConnectId_stripeConnectMode_key", "stripeConnectId", "stripeConnectMode", unique=True),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    users: Mapped[list["ProjectUsers"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    invites: Mapped[list["ProjectInvite"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    sent_emails: Mapped[list["SentEmail"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    links: Mapped[list["Link"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    domains: Mapped[list["Domain"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    tags: Mapped[list["Tag"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    invoices: Mapped[list["Invoice"]] = relationship(passive_deletes="all", back_populates="workspace")
    customers: Mapped[list["Customer"]] = relationship(passive_deletes="all", back_populates="project")
    default_domains: Mapped[list["DefaultDomains"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    restricted_tokens: Mapped[list["RestrictedToken"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    o_auth_codes: Mapped[list["OAuthCode"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    integrations: Mapped[list["Integration"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    installed_integrations: Mapped[list["InstalledIntegration"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    webhooks: Mapped[list["Webhook"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    folders: Mapped[list["Folder"]] = relationship(passive_deletes="all", back_populates="project")
    registered_domains: Mapped[list["RegisteredDomain"]] = relationship(passive_deletes="all", back_populates="project")
    dashboards: Mapped[list["Dashboard"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    utm_templates: Mapped[list["UtmTemplate"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project")
    year_in_reviews: Mapped[list["YearInReview"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="workspace")

class ProjectUsers(Base):
    __tablename__ = "ProjectUsers"

    id: Mapped[str] = mapped_column(primary_key=True)
    role: Mapped[str] = mapped_column(Enum("owner", "member", "viewer", "billing", name="WorkspaceRole"), default="member", server_default=text("'member'"))
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="ProjectUsers_userId_fkey"))
    project_id: Mapped[str] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="ProjectUsers_projectId_fkey"))
    workspace_preferences: Mapped[Optional[dict[str, Any]]] = mapped_column("workspacePreferences", JSON(none_as_null=True))
    default_folder_id: Mapped[Optional[str]] = mapped_column("defaultFolderId")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("ProjectUsers_projectId_idx", "projectId"),
        Index("ProjectUsers_userId_projectId_key", "userId", "projectId", unique=True),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    user: Mapped["User"] = relationship(back_populates="projects")
    project: Mapped["Project"] = relationship(back_populates="users")
    notification_preference: Mapped[Optional["NotificationPreference"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="project_user")

class SentEmail(Base):
    __tablename__ = "SentEmail"

    id: Mapped[str] = mapped_column(primary_key=True)
    type: Mapped[str]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))
    project_id: Mapped[Optional[str]] = mapped_column("projectId", ForeignKey("Project.id", ondelete="CASCADE", onupdate="CASCADE", name="SentEmail_projectId_fkey"))

    __table_args__ = (
        Index("SentEmail_projectId_idx", "projectId"),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    project: Mapped[Optional["Project"]] = relationship(back_populates="sent_emails")
