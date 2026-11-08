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


def uuid4_str() -> str:
    return str(uuid.uuid4())


class AccessType(models.TextChoices):
    MCP = "MCP"
    PRIVATE = "PRIVATE"
    PUBLIC = "PUBLIC"


class AssetClass(models.TextChoices):
    ALTERNATIVE_INVESTMENT = "ALTERNATIVE_INVESTMENT"
    COMMODITY = "COMMODITY"
    EQUITY = "EQUITY"
    FIXED_INCOME = "FIXED_INCOME"
    LIQUIDITY = "LIQUIDITY"
    REAL_ESTATE = "REAL_ESTATE"


class AssetSubClass(models.TextChoices):
    BOND = "BOND"
    CASH = "CASH"
    COLLECTIBLE = "COLLECTIBLE"
    COMMODITY = "COMMODITY"
    CRYPTOCURRENCY = "CRYPTOCURRENCY"
    ETF = "ETF"
    LOAN = "LOAN"
    MUTUALFUND = "MUTUALFUND"
    PRECIOUS_METAL = "PRECIOUS_METAL"
    PRIVATE_EQUITY = "PRIVATE_EQUITY"
    STOCK = "STOCK"


class DataGatheringFrequency(models.TextChoices):
    DAILY = "DAILY"
    HOURLY = "HOURLY"


class DataSource(models.TextChoices):
    ALPHA_VANTAGE = "ALPHA_VANTAGE"
    COINGECKO = "COINGECKO"
    EOD_HISTORICAL_DATA = "EOD_HISTORICAL_DATA"
    FINANCIAL_MODELING_PREP = "FINANCIAL_MODELING_PREP"
    GHOSTFOLIO = "GHOSTFOLIO"
    GOOGLE_SHEETS = "GOOGLE_SHEETS"
    MANUAL = "MANUAL"
    RAPID_API = "RAPID_API"
    YAHOO = "YAHOO"


class MarketDataState(models.TextChoices):
    CLOSE = "CLOSE"
    INTRADAY = "INTRADAY"


class Provider(models.TextChoices):
    ANONYMOUS = "ANONYMOUS"
    GOOGLE = "GOOGLE"
    INTERNET_IDENTITY = "INTERNET_IDENTITY"
    OIDC = "OIDC"


class RequestClient(models.TextChoices):
    API = "API"
    MCP = "MCP"
    SELF_HOSTING = "SELF_HOSTING"
    WEB = "WEB"


class Role(models.TextChoices):
    ADMIN = "ADMIN"
    DEMO = "DEMO"
    INACTIVE = "INACTIVE"
    USER = "USER"


class Type(models.TextChoices):
    BUY = "BUY"
    DIVIDEND = "DIVIDEND"
    FEE = "FEE"
    INTEREST = "INTEREST"
    LIABILITY = "LIABILITY"
    SELL = "SELL"


class ViewMode(models.TextChoices):
    DEFAULT = "DEFAULT"
    ZEN = "ZEN"


class Access(models.Model):
    alias = models.TextField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    expires_at = UtcDateTimeField(db_column="expiresAt")
    grantee_user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="accesses_get", db_column="granteeUserId", null=True, db_index=False)
    id = models.TextField(primary_key=True, default=uuid4_str)
    last_used_at = UtcDateTimeField(null=True, db_column="lastUsedAt")
    scopes = ArrayField(models.TextField(), default=list)
    settings = models.JSONField(default=dict)
    type = models.TextField(choices=AccessType.choices, default=AccessType.PRIVATE)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="accesses_give", db_column="userId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Access"
        indexes = [
            models.Index(fields=["alias"]),
            models.Index(fields=["grantee_user"]),
            models.Index(fields=["user"]),
        ]


class Account(models.Model):
    pk = models.CompositePrimaryKey("id", "user_id")
    comment = models.TextField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    currency = models.TextField(null=True)
    id = models.TextField(default=uuid4_str)
    name = models.TextField(null=True)
    platform = models.ForeignKey("Platform", on_delete=models.SET_NULL, related_name="accounts", db_column="platformId", null=True, db_index=False)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="accounts", db_column="userId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Account"
        indexes = [
            models.Index(fields=["currency"]),
            models.Index(fields=["id"]),
            models.Index(fields=["name"]),
            models.Index(fields=["user"]),
        ]


class AccountBalance(models.Model):
    account_id = models.TextField(db_column="accountId")
    comment = models.TextField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    date = UtcDateTimeField(default=timezone.now)
    id = models.TextField(primary_key=True, default=uuid4_str)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    user_id = models.TextField(db_column="userId")
    value = models.FloatField()
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "AccountBalance"
        constraints = [
            models.UniqueConstraint(fields=["account_id", "user_id", "date"], name="AccountBalance_accountId_userId_date_key"),
        ]
        indexes = [
            models.Index(fields=["account_id"]),
            models.Index(fields=["date"]),
        ]


class Analytics(models.Model):
    activity_count = models.IntegerField(db_column="activityCount", default=0)
    country = models.TextField(null=True)
    data_provider_ghostfolio_daily_requests = models.IntegerField(db_column="dataProviderGhostfolioDailyRequests", default=0)
    last_request_at = UtcDateTimeField(db_column="lastRequestAt", default=timezone.now)
    last_request_client = models.TextField(choices=RequestClient.choices, null=True, db_column="lastRequestClient")
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    user = models.OneToOneField("User", on_delete=models.CASCADE, related_name="analytics", primary_key=True, db_column="userId")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Analytics"
        indexes = [
            models.Index(fields=["last_request_at"]),
            models.Index(fields=["updated_at"]),
        ]


class ApiKey(models.Model):
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    hashed_key = models.TextField(unique=True, db_column="hashedKey")
    id = models.TextField(primary_key=True, default=uuid4_str)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="api_keys", db_column="userId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "ApiKey"
        indexes = [
            models.Index(fields=["user"]),
        ]


class AssetProfileOverrides(models.Model):
    asset_class = models.TextField(choices=AssetClass.choices, null=True, db_column="assetClass")
    asset_sub_class = models.TextField(choices=AssetSubClass.choices, null=True, db_column="assetSubClass")
    countries = models.JSONField(null=True, default=list)
    holdings = models.JSONField(null=True, default=list)
    name = models.TextField(null=True)
    sectors = models.JSONField(null=True, default=list)
    symbol_profile = models.OneToOneField("SymbolProfile", on_delete=models.CASCADE, related_name="asset_profile_overrides", primary_key=True, db_column="symbolProfileId")
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    url = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "SymbolProfileOverrides"


class AssetProfileResolution(models.Model):
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    currency = models.TextField()
    data_source_origin = models.TextField(choices=DataSource.choices, db_column="dataSourceOrigin")
    data_source_target = models.TextField(choices=DataSource.choices, db_column="dataSourceTarget")
    id = models.TextField(primary_key=True, default=uuid4_str)
    request_count = models.IntegerField(db_column="requestCount", default=1)
    symbol_origin = models.TextField(db_column="symbolOrigin")
    symbol_target = models.TextField(db_column="symbolTarget")
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "AssetProfileResolution"
        constraints = [
            models.UniqueConstraint(fields=["data_source_origin", "symbol_origin"], name="AssetProfileResolution_dataSourceOrigin_symbolOrigin_key"),
        ]


class AssetProfileSplit(models.Model):
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    date = UtcDateTimeField()
    denominator = models.IntegerField()
    id = models.TextField(primary_key=True, default=uuid4_str)
    numerator = models.IntegerField()
    symbol_profile = models.ForeignKey("SymbolProfile", on_delete=models.CASCADE, related_name="asset_profile_splits", db_column="symbolProfileId", db_index=False)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "AssetProfileSplit"
        constraints = [
            models.UniqueConstraint(fields=["symbol_profile", "date"], name="AssetProfileSplit_symbolProfileId_date_key"),
        ]
        indexes = [
            models.Index(fields=["date"]),
        ]


class AuthDevice(models.Model):
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    credential_id = models.BinaryField(db_column="credentialId")
    credential_public_key = models.BinaryField(db_column="credentialPublicKey")
    counter = models.IntegerField()
    id = models.TextField(primary_key=True, default=uuid4_str)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="auth_devices", db_column="userId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "AuthDevice"
        indexes = [
            models.Index(fields=["user"]),
        ]


class MarketData(models.Model):
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    data_source = models.TextField(choices=DataSource.choices, db_column="dataSource")
    date = UtcDateTimeField()
    id = models.TextField(primary_key=True, default=uuid4_str)
    is_carried_forward = models.BooleanField(db_column="isCarriedForward", default=False)
    market_price = models.FloatField(db_column="marketPrice")
    state = models.TextField(choices=MarketDataState.choices, default=MarketDataState.CLOSE)
    symbol = models.TextField()

    class Meta:
        db_table = "MarketData"
        constraints = [
            models.UniqueConstraint(fields=["data_source", "symbol", "date"], name="MarketData_dataSource_symbol_date_key"),
        ]
        indexes = [
            models.Index(fields=["date"]),
            models.Index(fields=["state"]),
            models.Index(fields=["symbol"]),
        ]


class Order(models.Model):
    account_id = models.TextField(null=True, db_column="accountId")
    account_user_id = models.TextField(null=True, db_column="accountUserId")
    comment = models.TextField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    currency = models.TextField(null=True)
    date = UtcDateTimeField()
    fee = models.FloatField()
    id = models.TextField(primary_key=True, default=uuid4_str)
    quantity = models.FloatField()
    symbol_profile = models.ForeignKey("SymbolProfile", on_delete=models.RESTRICT, related_name="activities", db_column="symbolProfileId", db_index=False)
    tags: "models.ManyToManyField[Tag, OrderToTag]" = models.ManyToManyField("Tag", through="OrderToTag", related_name="activities")
    type = models.TextField(choices=Type.choices)
    unit_price = models.FloatField(db_column="unitPrice")
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="activities", db_column="userId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Order"
        indexes = [
            models.Index(fields=["account_id"]),
            models.Index(fields=["date"]),
            models.Index(fields=["symbol_profile", "date"]),
            models.Index(fields=["user", "date"]),
            models.Index(fields=["user", "type", "date"]),
        ]


class Platform(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    name = models.TextField(null=True)
    url = models.TextField(unique=True)

    class Meta:
        db_table = "Platform"
        indexes = [
            models.Index(fields=["name"]),
        ]


class Property(models.Model):
    key = models.TextField(primary_key=True, default=None)
    value = models.TextField()

    class Meta:
        db_table = "Property"


class Settings(models.Model):
    settings = models.JSONField(null=True)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    user = models.OneToOneField("User", on_delete=models.CASCADE, related_name="settings", primary_key=True, db_column="userId")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Settings"


class SymbolProfile(models.Model):
    asset_class = models.TextField(choices=AssetClass.choices, null=True, db_column="assetClass")
    asset_sub_class = models.TextField(choices=AssetSubClass.choices, null=True, db_column="assetSubClass")
    comment = models.TextField(null=True)
    countries = models.JSONField(null=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    currency = models.TextField()
    cusip = models.TextField(null=True)
    data_gathering_frequency = models.TextField(choices=DataGatheringFrequency.choices, db_column="dataGatheringFrequency", default=DataGatheringFrequency.DAILY)
    data_source = models.TextField(choices=DataSource.choices, db_column="dataSource")
    figi = models.TextField(null=True)
    figi_composite = models.TextField(null=True, db_column="figiComposite")
    figi_share_class = models.TextField(null=True, db_column="figiShareClass")
    holdings = models.JSONField(null=True, default=list)
    id = models.TextField(primary_key=True, default=uuid4_str)
    is_active = models.BooleanField(db_column="isActive", default=True)
    isin = models.TextField(null=True)
    name = models.TextField(null=True)
    scraper_configuration = models.JSONField(null=True, db_column="scraperConfiguration")
    sectors = models.JSONField(null=True)
    symbol = models.TextField()
    symbol_mapping = models.JSONField(null=True, db_column="symbolMapping")
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    url = models.TextField(null=True)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="symbol_profile", db_column="userId", null=True, db_index=False)
    watched_by: "models.ManyToManyField[User, UserWatchlist]" = models.ManyToManyField("User", through="UserWatchlist", related_name="watchlist")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "SymbolProfile"
        constraints = [
            models.UniqueConstraint(fields=["data_source", "symbol"], name="SymbolProfile_dataSource_symbol_key"),
        ]
        indexes = [
            models.Index(fields=["asset_class"]),
            models.Index(fields=["currency"]),
            models.Index(fields=["cusip"]),
            models.Index(fields=["data_gathering_frequency"]),
            models.Index(fields=["data_source"]),
            models.Index(fields=["is_active"]),
            models.Index(fields=["isin"]),
            models.Index(fields=["name"]),
            models.Index(fields=["symbol"]),
        ]


class Subscription(models.Model):
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    expires_at = UtcDateTimeField(db_column="expiresAt")
    id = models.TextField(primary_key=True, default=uuid4_str)
    price = models.FloatField(null=True)
    stripe_checkout_session_id = models.TextField(null=True, unique=True, db_column="stripeCheckoutSessionId")
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="subscriptions", db_column="userId", db_index=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Subscription"
        indexes = [
            models.Index(fields=["user"]),
        ]


class Tag(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    name = models.TextField()
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="tags", db_column="userId", null=True, db_index=False)

    class Meta:
        db_table = "Tag"
        constraints = [
            models.UniqueConstraint(fields=["name", "user"], name="Tag_name_userId_key"),
        ]
        indexes = [
            models.Index(fields=["name"]),
        ]


class TagsOnAccounts(models.Model):
    pk = models.CompositePrimaryKey("account_id", "tag_id", "user_id")
    account_id = models.TextField(db_column="accountId")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    tag = models.ForeignKey("Tag", on_delete=models.CASCADE, related_name="accounts", db_column="tagId", db_index=False)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    user_id = models.TextField(db_column="userId")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "TagsOnAccounts"
        indexes = [
            models.Index(fields=["tag"]),
        ]


class User(models.Model):
    access_token = models.TextField(null=True, db_column="accessToken")
    auth_challenge = models.TextField(null=True, db_column="authChallenge")
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    id = models.TextField(primary_key=True, default=uuid4_str)
    provider = models.TextField(choices=Provider.choices, default=Provider.ANONYMOUS)
    role = models.TextField(choices=Role.choices, default=Role.USER)
    third_party_id = models.TextField(null=True, db_column="thirdPartyId")
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "User"
        indexes = [
            models.Index(fields=["access_token"]),
            models.Index(fields=["created_at"]),
            models.Index(fields=["provider"]),
            models.Index(fields=["role"]),
            models.Index(fields=["third_party_id"]),
        ]


class OrderToTag(models.Model):
    pk = models.CompositePrimaryKey("a_id", "b_id")
    a = models.ForeignKey("Order", on_delete=models.CASCADE, related_name="+", db_column="A", db_index=False)
    b = models.ForeignKey("Tag", on_delete=models.CASCADE, related_name="+", db_column="B", db_index=False)

    class Meta:
        db_table = "_OrderToTag"
        indexes = [
            models.Index(fields=["b"]),
        ]


class UserWatchlist(models.Model):
    pk = models.CompositePrimaryKey("a_id", "b_id")
    a = models.ForeignKey("SymbolProfile", on_delete=models.CASCADE, related_name="+", db_column="A", db_index=False)
    b = models.ForeignKey("User", on_delete=models.CASCADE, related_name="+", db_column="B", db_index=False)

    class Meta:
        db_table = "_UserWatchlist"
        indexes = [
            models.Index(fields=["b"]),
        ]
