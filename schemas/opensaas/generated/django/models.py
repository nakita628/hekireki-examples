import uuid
from datetime import datetime
from datetime import timezone as dt_timezone
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


def uuid4_str() -> str:
    return str(uuid.uuid4())


class User(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    email = models.TextField(null=True, unique=True)
    username = models.TextField(null=True, unique=True)
    is_admin = models.BooleanField(db_column="isAdmin", default=False)
    payment_processor_user_id = models.TextField(null=True, unique=True, db_column="paymentProcessorUserId")
    subscription_status = models.TextField(null=True, db_column="subscriptionStatus")
    subscription_plan = models.TextField(null=True, db_column="subscriptionPlan")
    current_period_end = UtcDateTimeField(null=True, db_column="currentPeriodEnd")
    cancel_at_period_end = models.BooleanField(db_column="cancelAtPeriodEnd", default=False)
    date_paid = UtcDateTimeField(null=True, db_column="datePaid")
    credits = models.IntegerField(default=3)

    class Meta:
        db_table = "User"


class ProcessedWebhookEvent(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    payment_processor_event_id = models.TextField(unique=True, db_column="paymentProcessorEventId")
    event_type = models.TextField(db_column="eventType")

    class Meta:
        db_table = "ProcessedWebhookEvent"


class GptResponse(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    updated_at = UtcDateTimeField(db_column="updatedAt", auto_now=True)
    user = models.ForeignKey("User", on_delete=models.RESTRICT, related_name="gpt_responses", db_column="userId", db_index=False)
    content = models.TextField()
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "GptResponse"


class Task(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    user = models.ForeignKey("User", on_delete=models.RESTRICT, related_name="tasks", db_column="userId", db_index=False)
    description = models.TextField()
    time = models.TextField(default="1")
    is_done = models.BooleanField(db_column="isDone", default=False)

    class Meta:
        db_table = "Task"


class File(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    user = models.ForeignKey("User", on_delete=models.RESTRICT, related_name="files", db_column="userId", db_index=False)
    name = models.TextField()
    type = models.TextField()
    s3_key = models.TextField(db_column="s3Key")

    class Meta:
        db_table = "File"


class DailyStats(models.Model):
    id = models.AutoField(primary_key=True)
    date = UtcDateTimeField(unique=True, default=timezone.now)
    total_views = models.IntegerField(db_column="totalViews", default=0)
    prev_day_views_change_percent = models.TextField(db_column="prevDayViewsChangePercent", default="0")
    user_count = models.IntegerField(db_column="userCount", default=0)
    paid_user_count = models.IntegerField(db_column="paidUserCount", default=0)
    user_delta = models.IntegerField(db_column="userDelta", default=0)
    paid_user_delta = models.IntegerField(db_column="paidUserDelta", default=0)
    total_revenue = models.FloatField(db_column="totalRevenue", default=0)
    total_profit = models.FloatField(db_column="totalProfit", default=0)

    class Meta:
        db_table = "DailyStats"


class PageViewSource(models.Model):
    pk = models.CompositePrimaryKey("date", "name")
    name = models.TextField()
    date = UtcDateTimeField(default=timezone.now)
    daily_stats = models.ForeignKey("DailyStats", on_delete=models.SET_NULL, related_name="sources", db_column="dailyStatsId", null=True, db_index=False)
    visitors = models.IntegerField()

    class Meta:
        db_table = "PageViewSource"


class Logs(models.Model):
    id = models.AutoField(primary_key=True)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    message = models.TextField()
    level = models.TextField()

    class Meta:
        db_table = "Logs"


class ContactFormMessage(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    created_at = UtcDateTimeField(db_column="createdAt", default=timezone.now)
    user = models.ForeignKey("User", on_delete=models.RESTRICT, related_name="contact_form_messages", db_column="userId", db_index=False)
    content = models.TextField()
    is_read = models.BooleanField(db_column="isRead", default=False)
    replied_at = UtcDateTimeField(null=True, db_column="repliedAt")

    class Meta:
        db_table = "ContactFormMessage"
