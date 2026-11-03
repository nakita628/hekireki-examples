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


class User(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField(null=True)
    email = models.TextField(unique=True)
    email_verified = UtcDateTimeField(null=True, db_column="emailVerified")
    image = models.TextField(null=True)

    class Meta:
        db_table = "User"


class Account(models.Model):
    pk = models.CompositePrimaryKey("provider", "provider_account_id")
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


class VerificationToken(models.Model):
    pk = models.CompositePrimaryKey("identifier", "token")
    identifier = models.TextField()
    token = models.TextField(unique=True)
    expires = UtcDateTimeField()

    class Meta:
        db_table = "VerificationToken"


class Authenticator(models.Model):
    pk = models.CompositePrimaryKey("user_id", "credential_id")
    credential_id = models.TextField(unique=True, db_column="credentialID")
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="authenticator", db_column="userId", db_index=False)
    provider_account_id = models.TextField(db_column="providerAccountId")
    credential_public_key = models.TextField(db_column="credentialPublicKey")
    counter = models.IntegerField()
    credential_device_type = models.TextField(db_column="credentialDeviceType")
    credential_backed_up = models.BooleanField(db_column="credentialBackedUp")
    transports = models.TextField(null=True)

    class Meta:
        db_table = "Authenticator"
