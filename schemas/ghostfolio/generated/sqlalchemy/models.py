from sqlalchemy import ARRAY, Column, Dialect, Enum, ForeignKey, ForeignKeyConstraint, Index, MetaData, PrimaryKeyConstraint, Table, Text, TypeDecorator, text
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

order_to_tag = Table(
    "_OrderToTag",
    Base.metadata,
    Column("A", Text, ForeignKey("Order.id", ondelete="CASCADE", onupdate="CASCADE", name="_OrderToTag_A_fkey"), nullable=False),
    Column("B", Text, ForeignKey("Tag.id", ondelete="CASCADE", onupdate="CASCADE", name="_OrderToTag_B_fkey"), nullable=False),
    PrimaryKeyConstraint("A", "B", name="_OrderToTag_AB_pkey"),
    Index("_OrderToTag_B_index", "B"),
)

user_watchlist = Table(
    "_UserWatchlist",
    Base.metadata,
    Column("A", Text, ForeignKey("SymbolProfile.id", ondelete="CASCADE", onupdate="CASCADE", name="_UserWatchlist_A_fkey"), nullable=False),
    Column("B", Text, ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="_UserWatchlist_B_fkey"), nullable=False),
    PrimaryKeyConstraint("A", "B", name="_UserWatchlist_AB_pkey"),
    Index("_UserWatchlist_B_index", "B"),
)


class Access(Base):
    __tablename__ = "Access"

    alias: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    expires_at: Mapped[datetime] = mapped_column("expiresAt")
    grantee_user_id: Mapped[Optional[str]] = mapped_column("granteeUserId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="Access_granteeUserId_fkey"))
    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    last_used_at: Mapped[Optional[datetime]] = mapped_column("lastUsedAt")
    scopes: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    settings: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: {}, server_default=text("'{}'"))
    type: Mapped[str] = mapped_column(Enum("MCP", "PRIVATE", "PUBLIC", name="AccessType"), default="PRIVATE", server_default=text("'PRIVATE'"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="Access_userId_fkey"))

    __table_args__ = (
        Index("Access_alias_idx", "alias"),
        Index("Access_granteeUserId_idx", "granteeUserId"),
        Index("Access_userId_idx", "userId"),
    )

    grantee_user: Mapped[Optional["User"]] = relationship(foreign_keys=[grantee_user_id], back_populates="accesses_get")
    user: Mapped["User"] = relationship(foreign_keys=[user_id], back_populates="accesses_give")

class Account(Base):
    __tablename__ = "Account"

    comment: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    currency: Mapped[Optional[str]]
    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    name: Mapped[Optional[str]]
    platform_id: Mapped[Optional[str]] = mapped_column("platformId", ForeignKey("Platform.id", ondelete="SET NULL", onupdate="CASCADE", name="Account_platformId_fkey"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="Account_userId_fkey"), primary_key=True)

    __table_args__ = (
        Index("Account_currency_idx", "currency"),
        Index("Account_id_idx", "id"),
        Index("Account_name_idx", "name"),
        Index("Account_userId_idx", "userId"),
    )

    platform: Mapped[Optional["Platform"]] = relationship(back_populates="accounts")
    user: Mapped["User"] = relationship(back_populates="accounts")
    activities: Mapped[list["Order"]] = relationship(back_populates="account")
    balances: Mapped[list["AccountBalance"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="account")
    tags: Mapped[list["TagsOnAccounts"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="account")

class AccountBalance(Base):
    __tablename__ = "AccountBalance"

    account_id: Mapped[str] = mapped_column("accountId")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    date: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    user_id: Mapped[str] = mapped_column("userId")
    value: Mapped[float]

    __table_args__ = (
        Index("AccountBalance_accountId_idx", "accountId"),
        Index("AccountBalance_date_idx", "date"),
        Index("AccountBalance_accountId_userId_date_key", "accountId", "userId", "date", unique=True),
        ForeignKeyConstraint(["accountId", "userId"], ["Account.id", "Account.userId"], ondelete="CASCADE", onupdate="CASCADE", name="AccountBalance_accountId_userId_fkey"),
    )

    account: Mapped["Account"] = relationship(back_populates="balances")

class Analytics(Base):
    __tablename__ = "Analytics"

    activity_count: Mapped[int] = mapped_column("activityCount", default=0, server_default=text("0"))
    country: Mapped[Optional[str]]
    data_provider_ghostfolio_daily_requests: Mapped[int] = mapped_column("dataProviderGhostfolioDailyRequests", default=0, server_default=text("0"))
    last_request_at: Mapped[datetime] = mapped_column("lastRequestAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_request_client: Mapped[Optional[str]] = mapped_column("lastRequestClient", Enum("API", "MCP", "SELF_HOSTING", "WEB", name="RequestClient"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="Analytics_userId_fkey"), primary_key=True)

    __table_args__ = (
        Index("Analytics_lastRequestAt_idx", "lastRequestAt"),
        Index("Analytics_updatedAt_idx", "updatedAt"),
    )

    user: Mapped["User"] = relationship(back_populates="analytics")

class ApiKey(Base):
    __tablename__ = "ApiKey"

    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    hashed_key: Mapped[str] = mapped_column("hashedKey")
    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="ApiKey_userId_fkey"))

    __table_args__ = (
        Index("ApiKey_hashedKey_key", "hashedKey", unique=True),
        Index("ApiKey_userId_idx", "userId"),
    )

    user: Mapped["User"] = relationship(back_populates="api_keys")

class AssetProfileOverrides(Base):
    __tablename__ = "SymbolProfileOverrides"

    asset_class: Mapped[Optional[str]] = mapped_column("assetClass", Enum("ALTERNATIVE_INVESTMENT", "COMMODITY", "EQUITY", "FIXED_INCOME", "LIQUIDITY", "REAL_ESTATE", name="AssetClass"))
    asset_sub_class: Mapped[Optional[str]] = mapped_column("assetSubClass", Enum("BOND", "CASH", "COLLECTIBLE", "COMMODITY", "CRYPTOCURRENCY", "ETF", "LOAN", "MUTUALFUND", "PRECIOUS_METAL", "PRIVATE_EQUITY", "STOCK", name="AssetSubClass"))
    countries: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True), default=lambda: [], server_default=text("'[]'"))
    holdings: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True), default=lambda: [], server_default=text("'[]'"))
    name: Mapped[Optional[str]]
    sectors: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True), default=lambda: [], server_default=text("'[]'"))
    symbol_profile_id: Mapped[str] = mapped_column("symbolProfileId", ForeignKey("SymbolProfile.id", ondelete="CASCADE", onupdate="CASCADE", name="SymbolProfileOverrides_symbolProfileId_fkey"), primary_key=True)
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    url: Mapped[Optional[str]]

    symbol_profile: Mapped["SymbolProfile"] = relationship(back_populates="asset_profile_overrides")

class AssetProfileResolution(Base):
    __tablename__ = "AssetProfileResolution"

    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    currency: Mapped[str]
    data_source_origin: Mapped[str] = mapped_column("dataSourceOrigin", Enum("ALPHA_VANTAGE", "COINGECKO", "EOD_HISTORICAL_DATA", "FINANCIAL_MODELING_PREP", "GHOSTFOLIO", "GOOGLE_SHEETS", "MANUAL", "RAPID_API", "YAHOO", name="DataSource"))
    data_source_target: Mapped[str] = mapped_column("dataSourceTarget", Enum("ALPHA_VANTAGE", "COINGECKO", "EOD_HISTORICAL_DATA", "FINANCIAL_MODELING_PREP", "GHOSTFOLIO", "GOOGLE_SHEETS", "MANUAL", "RAPID_API", "YAHOO", name="DataSource"))
    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    request_count: Mapped[int] = mapped_column("requestCount", default=1, server_default=text("1"))
    symbol_origin: Mapped[str] = mapped_column("symbolOrigin")
    symbol_target: Mapped[str] = mapped_column("symbolTarget")
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("AssetProfileResolution_dataSourceOrigin_symbolOrigin_key", "dataSourceOrigin", "symbolOrigin", unique=True),
    )

class AssetProfileSplit(Base):
    __tablename__ = "AssetProfileSplit"

    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    date: Mapped[datetime]
    denominator: Mapped[int]
    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    numerator: Mapped[int]
    symbol_profile_id: Mapped[str] = mapped_column("symbolProfileId", ForeignKey("SymbolProfile.id", ondelete="CASCADE", onupdate="CASCADE", name="AssetProfileSplit_symbolProfileId_fkey"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("AssetProfileSplit_date_idx", "date"),
        Index("AssetProfileSplit_symbolProfileId_date_key", "symbolProfileId", "date", unique=True),
    )

    symbol_profile: Mapped["SymbolProfile"] = relationship(back_populates="asset_profile_splits")

class AuthDevice(Base):
    __tablename__ = "AuthDevice"

    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    credential_id: Mapped[bytes] = mapped_column("credentialId")
    credential_public_key: Mapped[bytes] = mapped_column("credentialPublicKey")
    counter: Mapped[int]
    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="AuthDevice_userId_fkey"))

    __table_args__ = (
        Index("AuthDevice_userId_idx", "userId"),
    )

    user: Mapped["User"] = relationship(back_populates="auth_devices")

class MarketData(Base):
    __tablename__ = "MarketData"

    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    data_source: Mapped[str] = mapped_column("dataSource", Enum("ALPHA_VANTAGE", "COINGECKO", "EOD_HISTORICAL_DATA", "FINANCIAL_MODELING_PREP", "GHOSTFOLIO", "GOOGLE_SHEETS", "MANUAL", "RAPID_API", "YAHOO", name="DataSource"))
    date: Mapped[datetime]
    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    is_carried_forward: Mapped[bool] = mapped_column("isCarriedForward", default=False, server_default=text("false"))
    market_price: Mapped[float] = mapped_column("marketPrice")
    state: Mapped[str] = mapped_column(Enum("CLOSE", "INTRADAY", name="MarketDataState"), default="CLOSE", server_default=text("'CLOSE'"))
    symbol: Mapped[str]

    __table_args__ = (
        Index("MarketData_date_idx", "date"),
        Index("MarketData_state_idx", "state"),
        Index("MarketData_symbol_idx", "symbol"),
        Index("MarketData_dataSource_symbol_date_key", "dataSource", "symbol", "date", unique=True),
    )

class Order(Base):
    __tablename__ = "Order"

    account_id: Mapped[Optional[str]] = mapped_column("accountId")
    account_user_id: Mapped[Optional[str]] = mapped_column("accountUserId")
    comment: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    currency: Mapped[Optional[str]]
    date: Mapped[datetime]
    fee: Mapped[float]
    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    quantity: Mapped[float]
    symbol_profile_id: Mapped[str] = mapped_column("symbolProfileId", ForeignKey("SymbolProfile.id", ondelete="RESTRICT", onupdate="CASCADE", name="Order_symbolProfileId_fkey"))
    type: Mapped[str] = mapped_column(Enum("BUY", "DIVIDEND", "FEE", "INTEREST", "LIABILITY", "SELL", name="Type"))
    unit_price: Mapped[float] = mapped_column("unitPrice")
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="Order_userId_fkey"))

    __table_args__ = (
        Index("Order_accountId_idx", "accountId"),
        Index("Order_date_idx", "date"),
        Index("Order_symbolProfileId_date_idx", "symbolProfileId", "date"),
        Index("Order_userId_date_idx", "userId", "date"),
        Index("Order_userId_type_date_idx", "userId", "type", "date"),
        ForeignKeyConstraint(["accountId", "accountUserId"], ["Account.id", "Account.userId"], ondelete="SET NULL", onupdate="CASCADE", name="Order_accountId_accountUserId_fkey"),
    )

    account: Mapped[Optional["Account"]] = relationship(back_populates="activities")
    user: Mapped["User"] = relationship(back_populates="activities")
    symbol_profile: Mapped["SymbolProfile"] = relationship(back_populates="activities")
    tags: Mapped[list["Tag"]] = relationship(secondary=order_to_tag, back_populates="activities")

class Platform(Base):
    __tablename__ = "Platform"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    name: Mapped[Optional[str]]
    url: Mapped[str]

    __table_args__ = (
        Index("Platform_url_key", "url", unique=True),
        Index("Platform_name_idx", "name"),
    )

    accounts: Mapped[list["Account"]] = relationship(back_populates="platform")

class Property(Base):
    __tablename__ = "Property"

    key: Mapped[str] = mapped_column(primary_key=True)
    value: Mapped[str]

class Settings(Base):
    __tablename__ = "Settings"

    settings: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="Settings_userId_fkey"), primary_key=True)

    user: Mapped["User"] = relationship(back_populates="settings")

class SymbolProfile(Base):
    __tablename__ = "SymbolProfile"

    asset_class: Mapped[Optional[str]] = mapped_column("assetClass", Enum("ALTERNATIVE_INVESTMENT", "COMMODITY", "EQUITY", "FIXED_INCOME", "LIQUIDITY", "REAL_ESTATE", name="AssetClass"))
    asset_sub_class: Mapped[Optional[str]] = mapped_column("assetSubClass", Enum("BOND", "CASH", "COLLECTIBLE", "COMMODITY", "CRYPTOCURRENCY", "ETF", "LOAN", "MUTUALFUND", "PRECIOUS_METAL", "PRIVATE_EQUITY", "STOCK", name="AssetSubClass"))
    comment: Mapped[Optional[str]]
    countries: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    currency: Mapped[str]
    cusip: Mapped[Optional[str]]
    data_gathering_frequency: Mapped[str] = mapped_column("dataGatheringFrequency", Enum("DAILY", "HOURLY", name="DataGatheringFrequency"), default="DAILY", server_default=text("'DAILY'"))
    data_source: Mapped[str] = mapped_column("dataSource", Enum("ALPHA_VANTAGE", "COINGECKO", "EOD_HISTORICAL_DATA", "FINANCIAL_MODELING_PREP", "GHOSTFOLIO", "GOOGLE_SHEETS", "MANUAL", "RAPID_API", "YAHOO", name="DataSource"))
    figi: Mapped[Optional[str]]
    figi_composite: Mapped[Optional[str]] = mapped_column("figiComposite")
    figi_share_class: Mapped[Optional[str]] = mapped_column("figiShareClass")
    holdings: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True), default=lambda: [], server_default=text("'[]'"))
    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    is_active: Mapped[bool] = mapped_column("isActive", default=True, server_default=text("true"))
    isin: Mapped[Optional[str]]
    name: Mapped[Optional[str]]
    scraper_configuration: Mapped[Optional[dict[str, Any]]] = mapped_column("scraperConfiguration", JSONB(none_as_null=True))
    sectors: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    symbol: Mapped[str]
    symbol_mapping: Mapped[Optional[dict[str, Any]]] = mapped_column("symbolMapping", JSONB(none_as_null=True))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    url: Mapped[Optional[str]]
    user_id: Mapped[Optional[str]] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="SymbolProfile_userId_fkey"))

    __table_args__ = (
        Index("SymbolProfile_assetClass_idx", "assetClass"),
        Index("SymbolProfile_currency_idx", "currency"),
        Index("SymbolProfile_cusip_idx", "cusip"),
        Index("SymbolProfile_dataGatheringFrequency_idx", "dataGatheringFrequency"),
        Index("SymbolProfile_dataSource_idx", "dataSource"),
        Index("SymbolProfile_isActive_idx", "isActive"),
        Index("SymbolProfile_isin_idx", "isin"),
        Index("SymbolProfile_name_idx", "name"),
        Index("SymbolProfile_symbol_idx", "symbol"),
        Index("SymbolProfile_dataSource_symbol_key", "dataSource", "symbol", unique=True),
    )

    user: Mapped[Optional["User"]] = relationship(back_populates="symbol_profile")
    activities: Mapped[list["Order"]] = relationship(passive_deletes="all", back_populates="symbol_profile")
    asset_profile_splits: Mapped[list["AssetProfileSplit"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="symbol_profile")
    asset_profile_overrides: Mapped[Optional["AssetProfileOverrides"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="symbol_profile")
    watched_by: Mapped[list["User"]] = relationship(secondary=user_watchlist, back_populates="watchlist")

class Subscription(Base):
    __tablename__ = "Subscription"

    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    expires_at: Mapped[datetime] = mapped_column("expiresAt")
    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    price: Mapped[Optional[float]]
    stripe_checkout_session_id: Mapped[Optional[str]] = mapped_column("stripeCheckoutSessionId")
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="Subscription_userId_fkey"))

    __table_args__ = (
        Index("Subscription_stripeCheckoutSessionId_key", "stripeCheckoutSessionId", unique=True),
        Index("Subscription_userId_idx", "userId"),
    )

    user: Mapped["User"] = relationship(back_populates="subscriptions")

class Tag(Base):
    __tablename__ = "Tag"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    name: Mapped[str]
    user_id: Mapped[Optional[str]] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="Tag_userId_fkey"))

    __table_args__ = (
        Index("Tag_name_idx", "name"),
        Index("Tag_name_userId_key", "name", "userId", unique=True),
    )

    user: Mapped[Optional["User"]] = relationship(back_populates="tags")
    accounts: Mapped[list["TagsOnAccounts"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="tag")
    activities: Mapped[list["Order"]] = relationship(secondary=order_to_tag, back_populates="tags")

class TagsOnAccounts(Base):
    __tablename__ = "TagsOnAccounts"

    account_id: Mapped[str] = mapped_column("accountId", primary_key=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    tag_id: Mapped[str] = mapped_column("tagId", ForeignKey("Tag.id", ondelete="CASCADE", onupdate="CASCADE", name="TagsOnAccounts_tagId_fkey"), primary_key=True)
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    user_id: Mapped[str] = mapped_column("userId", primary_key=True)

    __table_args__ = (
        Index("TagsOnAccounts_tagId_idx", "tagId"),
        ForeignKeyConstraint(["accountId", "userId"], ["Account.id", "Account.userId"], ondelete="CASCADE", onupdate="CASCADE", name="TagsOnAccounts_accountId_userId_fkey"),
    )

    account: Mapped["Account"] = relationship(back_populates="tags")
    tag: Mapped["Tag"] = relationship(back_populates="accounts")

class User(Base):
    __tablename__ = "User"

    access_token: Mapped[Optional[str]] = mapped_column("accessToken")
    auth_challenge: Mapped[Optional[str]] = mapped_column("authChallenge")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    provider: Mapped[str] = mapped_column(Enum("ANONYMOUS", "GOOGLE", "INTERNET_IDENTITY", "OIDC", name="Provider"), default="ANONYMOUS", server_default=text("'ANONYMOUS'"))
    role: Mapped[str] = mapped_column(Enum("ADMIN", "DEMO", "INACTIVE", "USER", name="Role"), default="USER", server_default=text("'USER'"))
    third_party_id: Mapped[Optional[str]] = mapped_column("thirdPartyId")
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("User_accessToken_idx", "accessToken"),
        Index("User_createdAt_idx", "createdAt"),
        Index("User_provider_idx", "provider"),
        Index("User_role_idx", "role"),
        Index("User_thirdPartyId_idx", "thirdPartyId"),
    )

    accesses_get: Mapped[list["Access"]] = relationship(foreign_keys="Access.grantee_user_id", cascade="all, delete", passive_deletes=True, back_populates="grantee_user")
    accesses_give: Mapped[list["Access"]] = relationship(foreign_keys="Access.user_id", cascade="all, delete", passive_deletes=True, back_populates="user")
    accounts: Mapped[list["Account"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    activities: Mapped[list["Order"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    api_keys: Mapped[list["ApiKey"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    auth_devices: Mapped[list["AuthDevice"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    subscriptions: Mapped[list["Subscription"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    tags: Mapped[list["Tag"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    symbol_profile: Mapped[list["SymbolProfile"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    analytics: Mapped[Optional["Analytics"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    settings: Mapped[Optional["Settings"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    watchlist: Mapped[list["SymbolProfile"]] = relationship(secondary=user_watchlist, back_populates="watched_by")
