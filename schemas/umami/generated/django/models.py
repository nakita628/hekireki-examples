from typing import Any, TypeVar

from django.db import models
from django.utils import timezone


_M = TypeVar("_M", bound=models.Model)


class AutoNowQuerySet(models.QuerySet[_M]):
    def update(self, **kwargs: Any) -> int:
        for field in self.model._meta.concrete_fields:
            if getattr(field, "auto_now", False):
                kwargs.setdefault(field.name, getattr(field, "stamp", timezone.now)())
        return super().update(**kwargs)


class User(models.Model):
    id = models.UUIDField(primary_key=True, db_column="user_id")
    username = models.CharField(max_length=255, unique=True)
    password = models.CharField(max_length=60)
    role = models.CharField(max_length=50)
    logo_url = models.CharField(max_length=2183, null=True)
    display_name = models.CharField(max_length=255, null=True)
    two_factor_required = models.BooleanField(default=False)
    created_at = models.DateTimeField(null=True, default=timezone.now)
    updated_at = models.DateTimeField(null=True, auto_now=True)
    deleted_at = models.DateTimeField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "user"


class ApiKey(models.Model):
    id = models.UUIDField(primary_key=True, db_column="api_key_id")
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="api_keys", db_index=False)
    name = models.CharField(max_length=255)
    key_hash = models.CharField(max_length=128, unique=True)
    key_prefix = models.CharField(max_length=20)
    last_used_at = models.DateTimeField(null=True)
    created_at = models.DateTimeField(null=True, default=timezone.now)

    class Meta:
        db_table = "api_key"
        indexes = [
            models.Index(fields=["user"]),
        ]


class Session(models.Model):
    id = models.UUIDField(primary_key=True, db_column="session_id")
    website_id = models.UUIDField()
    browser = models.CharField(max_length=20, null=True)
    os = models.CharField(max_length=20, null=True)
    device = models.CharField(max_length=20, null=True)
    screen = models.CharField(max_length=11, null=True)
    language = models.CharField(max_length=35, null=True)
    country = models.CharField(max_length=2, null=True)
    region = models.CharField(max_length=20, null=True)
    city = models.CharField(max_length=50, null=True)
    distinct_id = models.CharField(max_length=50, null=True)
    created_at = models.DateTimeField(null=True, default=timezone.now)

    class Meta:
        db_table = "session"
        indexes = [
            models.Index(fields=["created_at"]),
            models.Index(fields=["website_id"]),
            models.Index(fields=["website_id", "created_at"]),
            models.Index(fields=["website_id", "created_at", "browser"]),
            models.Index(fields=["website_id", "created_at", "os"]),
            models.Index(fields=["website_id", "created_at", "device"]),
            models.Index(fields=["website_id", "created_at", "screen"]),
            models.Index(fields=["website_id", "created_at", "language"]),
            models.Index(fields=["website_id", "created_at", "country"]),
            models.Index(fields=["website_id", "created_at", "region"]),
            models.Index(fields=["website_id", "created_at", "city"]),
        ]


class SessionLink(models.Model):
    pk = models.CompositePrimaryKey("website_id", "distinct_id", "session_id")
    website_id = models.UUIDField()
    session_id = models.UUIDField()
    distinct_id = models.CharField(max_length=50)
    created_at = models.DateTimeField(null=True, default=timezone.now)

    class Meta:
        db_table = "session_link"
        indexes = [
            models.Index(fields=["website_id", "session_id"]),
        ]


class Website(models.Model):
    id = models.UUIDField(primary_key=True, db_column="website_id")
    name = models.CharField(max_length=100)
    domain = models.CharField(max_length=500, null=True)
    reset_at = models.DateTimeField(null=True)
    user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="websites", null=True, db_index=False)
    team = models.ForeignKey("Team", on_delete=models.SET_NULL, related_name="websites", null=True, db_index=False)
    create_user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="created_by", db_column="created_by", null=True, db_index=False)
    created_at = models.DateTimeField(null=True, default=timezone.now)
    updated_at = models.DateTimeField(null=True, auto_now=True)
    deleted_at = models.DateTimeField(null=True)
    recorder_enabled = models.BooleanField(default=False)
    replay_config = models.JSONField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "website"
        indexes = [
            models.Index(fields=["user"]),
            models.Index(fields=["team"]),
            models.Index(fields=["created_at"]),
            models.Index(fields=["create_user"]),
        ]


class WebsiteEvent(models.Model):
    id = models.UUIDField(primary_key=True, db_column="event_id")
    website_id = models.UUIDField()
    session = models.ForeignKey("Session", on_delete=models.RESTRICT, related_name="website_events", db_index=False)
    visit_id = models.UUIDField()
    created_at = models.DateTimeField(null=True, default=timezone.now)
    url_path = models.CharField(max_length=500)
    url_query = models.CharField(max_length=500, null=True)
    utm_source = models.CharField(max_length=255, null=True)
    utm_medium = models.CharField(max_length=255, null=True)
    utm_campaign = models.CharField(max_length=255, null=True)
    utm_content = models.CharField(max_length=255, null=True)
    utm_term = models.CharField(max_length=255, null=True)
    referrer_path = models.CharField(max_length=500, null=True)
    referrer_query = models.CharField(max_length=500, null=True)
    referrer_domain = models.CharField(max_length=500, null=True)
    page_title = models.CharField(max_length=500, null=True)
    gclid = models.CharField(max_length=255, null=True)
    fbclid = models.CharField(max_length=255, null=True)
    msclkid = models.CharField(max_length=255, null=True)
    ttclid = models.CharField(max_length=255, null=True)
    lifatid = models.CharField(max_length=255, null=True, db_column="li_fat_id")
    twclid = models.CharField(max_length=255, null=True)
    event_type = models.IntegerField(default=1)
    event_name = models.CharField(max_length=50, null=True)
    tag = models.CharField(max_length=50, null=True)
    hostname = models.CharField(max_length=100, null=True)
    lcp = models.DecimalField(max_digits=10, decimal_places=1, null=True)
    inp = models.DecimalField(max_digits=10, decimal_places=1, null=True)
    cls_field = models.DecimalField(max_digits=10, decimal_places=4, null=True, db_column="cls")
    fcp = models.DecimalField(max_digits=10, decimal_places=1, null=True)
    ttfb = models.DecimalField(max_digits=10, decimal_places=1, null=True)

    class Meta:
        db_table = "website_event"
        indexes = [
            models.Index(fields=["created_at"]),
            models.Index(fields=["session"]),
            models.Index(fields=["visit_id"]),
            models.Index(fields=["website_id"]),
            models.Index(fields=["website_id", "created_at"]),
            models.Index(fields=["website_id", "created_at", "url_path"]),
            models.Index(fields=["website_id", "created_at", "url_query"]),
            models.Index(fields=["website_id", "created_at", "referrer_domain"]),
            models.Index(fields=["website_id", "created_at", "page_title"]),
            models.Index(fields=["website_id", "created_at", "event_name"]),
            models.Index(fields=["website_id", "created_at", "tag"]),
            models.Index(fields=["website_id", "session", "created_at"]),
            models.Index(fields=["website_id", "visit_id", "created_at"]),
            models.Index(fields=["website_id", "created_at", "hostname"]),
        ]


class EventData(models.Model):
    id = models.UUIDField(primary_key=True, db_column="event_data_id")
    website = models.ForeignKey("Website", on_delete=models.RESTRICT, related_name="event_data", db_index=False)
    website_event = models.ForeignKey("WebsiteEvent", on_delete=models.RESTRICT, related_name="event_data", db_index=False)
    data_key = models.CharField(max_length=500)
    string_value = models.CharField(max_length=500, null=True)
    number_value = models.DecimalField(max_digits=19, decimal_places=4, null=True)
    date_value = models.DateTimeField(null=True)
    data_type = models.IntegerField()
    created_at = models.DateTimeField(null=True, default=timezone.now)

    class Meta:
        db_table = "event_data"
        indexes = [
            models.Index(fields=["created_at"]),
            models.Index(fields=["website"]),
            models.Index(fields=["website_event"]),
            models.Index(fields=["website", "created_at"]),
            models.Index(fields=["website", "created_at", "data_key"]),
        ]


class SessionData(models.Model):
    id = models.UUIDField(primary_key=True, db_column="session_data_id")
    website = models.ForeignKey("Website", on_delete=models.RESTRICT, related_name="session_data", db_index=False)
    session = models.ForeignKey("Session", on_delete=models.RESTRICT, related_name="session_data", db_index=False)
    data_key = models.CharField(max_length=500)
    string_value = models.CharField(max_length=500, null=True)
    number_value = models.DecimalField(max_digits=19, decimal_places=4, null=True)
    date_value = models.DateTimeField(null=True)
    data_type = models.IntegerField()
    distinct_id = models.CharField(max_length=50, null=True)
    created_at = models.DateTimeField(null=True, default=timezone.now)

    class Meta:
        db_table = "session_data"
        constraints = [
            models.UniqueConstraint(fields=["session", "data_key"], name="session_data_session_id_data_key_key"),
        ]
        indexes = [
            models.Index(fields=["created_at"]),
            models.Index(fields=["website"]),
            models.Index(fields=["session"]),
            models.Index(fields=["session", "created_at"]),
            models.Index(fields=["website", "created_at", "data_key"]),
        ]


class Team(models.Model):
    id = models.UUIDField(primary_key=True, db_column="team_id")
    name = models.CharField(max_length=50)
    access_code = models.CharField(max_length=50, null=True, unique=True)
    logo_url = models.CharField(max_length=2183, null=True)
    two_factor_required = models.BooleanField(default=False)
    created_at = models.DateTimeField(null=True, default=timezone.now)
    updated_at = models.DateTimeField(null=True, auto_now=True)
    deleted_at = models.DateTimeField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "team"
        indexes = [
            models.Index(fields=["access_code"]),
        ]


class TeamUser(models.Model):
    id = models.UUIDField(primary_key=True, db_column="team_user_id")
    team = models.ForeignKey("Team", on_delete=models.RESTRICT, related_name="members", db_index=False)
    user = models.ForeignKey("User", on_delete=models.RESTRICT, related_name="teams", db_index=False)
    role = models.CharField(max_length=50)
    created_at = models.DateTimeField(null=True, default=timezone.now)
    updated_at = models.DateTimeField(null=True, auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "team_user"
        indexes = [
            models.Index(fields=["team"]),
            models.Index(fields=["user"]),
        ]


class Report(models.Model):
    id = models.UUIDField(primary_key=True, db_column="report_id")
    user = models.ForeignKey("User", on_delete=models.RESTRICT, related_name="reports", db_index=False)
    website = models.ForeignKey("Website", on_delete=models.RESTRICT, related_name="reports", db_index=False)
    type = models.CharField(max_length=50)
    name = models.CharField(max_length=200)
    description = models.CharField(max_length=500)
    parameters = models.JSONField()
    created_at = models.DateTimeField(null=True, default=timezone.now)
    updated_at = models.DateTimeField(null=True, auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "report"
        indexes = [
            models.Index(fields=["user"]),
            models.Index(fields=["website"]),
            models.Index(fields=["type"]),
            models.Index(fields=["name"]),
        ]


class Annotation(models.Model):
    id = models.UUIDField(primary_key=True, db_column="annotation_id")
    website = models.ForeignKey("Website", on_delete=models.RESTRICT, related_name="annotations", db_index=False)
    user_id = models.UUIDField(null=True)
    date = models.DateTimeField()
    all_day = models.BooleanField(default=True)
    note = models.CharField(max_length=500)
    created_at = models.DateTimeField(null=True, default=timezone.now)
    updated_at = models.DateTimeField(null=True, auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "annotation"
        indexes = [
            models.Index(fields=["website", "date"]),
        ]


class Segment(models.Model):
    id = models.UUIDField(primary_key=True, db_column="segment_id")
    website = models.ForeignKey("Website", on_delete=models.RESTRICT, related_name="segments", db_index=False)
    type = models.CharField(max_length=50)
    name = models.CharField(max_length=200)
    parameters = models.JSONField()
    created_at = models.DateTimeField(null=True, default=timezone.now)
    updated_at = models.DateTimeField(null=True, auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "segment"
        indexes = [
            models.Index(fields=["website"]),
        ]


class Revenue(models.Model):
    id = models.UUIDField(primary_key=True, db_column="revenue_id")
    website = models.ForeignKey("Website", on_delete=models.RESTRICT, related_name="revenue", db_index=False)
    session = models.ForeignKey("Session", on_delete=models.RESTRICT, related_name="revenue", db_index=False)
    event_id = models.UUIDField()
    event_name = models.CharField(max_length=50)
    currency = models.CharField(max_length=10)
    revenue = models.DecimalField(max_digits=19, decimal_places=4, null=True)
    created_at = models.DateTimeField(null=True, default=timezone.now)

    class Meta:
        db_table = "revenue"
        indexes = [
            models.Index(fields=["website"]),
            models.Index(fields=["session"]),
            models.Index(fields=["website", "created_at"]),
            models.Index(fields=["website", "session", "created_at"]),
        ]


class Link(models.Model):
    id = models.UUIDField(primary_key=True, db_column="link_id")
    name = models.CharField(max_length=100)
    url = models.CharField(max_length=500)
    slug = models.CharField(max_length=100, unique=True)
    user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="links", null=True, db_index=False)
    team = models.ForeignKey("Team", on_delete=models.SET_NULL, related_name="links", null=True, db_index=False)
    created_at = models.DateTimeField(null=True, default=timezone.now)
    updated_at = models.DateTimeField(null=True, auto_now=True)
    deleted_at = models.DateTimeField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "link"
        indexes = [
            models.Index(fields=["slug"]),
            models.Index(fields=["user"]),
            models.Index(fields=["team"]),
            models.Index(fields=["created_at"]),
        ]


class Pixel(models.Model):
    id = models.UUIDField(primary_key=True, db_column="pixel_id")
    name = models.CharField(max_length=100)
    slug = models.CharField(max_length=100, unique=True)
    user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="pixels", null=True, db_index=False)
    team = models.ForeignKey("Team", on_delete=models.SET_NULL, related_name="pixels", null=True, db_index=False)
    created_at = models.DateTimeField(null=True, default=timezone.now)
    updated_at = models.DateTimeField(null=True, auto_now=True)
    deleted_at = models.DateTimeField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "pixel"
        indexes = [
            models.Index(fields=["slug"]),
            models.Index(fields=["user"]),
            models.Index(fields=["team"]),
            models.Index(fields=["created_at"]),
        ]


class Board(models.Model):
    id = models.UUIDField(primary_key=True, db_column="board_id")
    type = models.CharField(max_length=50)
    name = models.CharField(max_length=200)
    description = models.CharField(max_length=500)
    parameters = models.JSONField()
    user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="boards", null=True, db_index=False)
    team = models.ForeignKey("Team", on_delete=models.SET_NULL, related_name="boards", null=True, db_index=False)
    created_at = models.DateTimeField(null=True, default=timezone.now)
    updated_at = models.DateTimeField(null=True, auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "board"
        indexes = [
            models.Index(fields=["user"]),
            models.Index(fields=["team"]),
            models.Index(fields=["created_at"]),
        ]


class Share(models.Model):
    id = models.UUIDField(primary_key=True, db_column="share_id")
    entity_id = models.UUIDField()
    name = models.CharField(max_length=200)
    share_type = models.IntegerField()
    slug = models.CharField(max_length=100, unique=True)
    parameters = models.JSONField()
    created_at = models.DateTimeField(null=True, default=timezone.now)
    updated_at = models.DateTimeField(null=True, auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "share"
        indexes = [
            models.Index(fields=["entity_id"]),
        ]


class SessionReplay(models.Model):
    id = models.UUIDField(primary_key=True, db_column="replay_id")
    website = models.ForeignKey("Website", on_delete=models.RESTRICT, related_name="session_replays", db_index=False)
    session_id = models.UUIDField()
    visit_id = models.UUIDField()
    chunk_index = models.IntegerField()
    events = models.BinaryField()
    event_count = models.IntegerField()
    started_at = models.DateTimeField()
    ended_at = models.DateTimeField()
    created_at = models.DateTimeField(null=True, default=timezone.now)

    class Meta:
        db_table = "session_replay"
        indexes = [
            models.Index(fields=["website"]),
            models.Index(fields=["session_id"]),
            models.Index(fields=["visit_id"]),
            models.Index(fields=["website", "session_id"]),
            models.Index(fields=["website", "visit_id"]),
            models.Index(fields=["website", "created_at"]),
            models.Index(fields=["session_id", "chunk_index"]),
        ]


class SessionReplaySaved(models.Model):
    id = models.UUIDField(primary_key=True, db_column="saved_replay_id")
    name = models.CharField(max_length=100)
    website = models.ForeignKey("Website", on_delete=models.RESTRICT, related_name="session_replays_saved", db_index=False)
    visit_id = models.UUIDField()
    created_at = models.DateTimeField(null=True, default=timezone.now)
    updated_at = models.DateTimeField(null=True, auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "session_replay_saved"
        constraints = [
            models.UniqueConstraint(fields=["website", "visit_id"], name="session_replay_saved_website_id_visit_id_key"),
        ]
        indexes = [
            models.Index(fields=["website"]),
            models.Index(fields=["visit_id"]),
            models.Index(fields=["website", "created_at"]),
        ]


class HeatmapEvent(models.Model):
    id = models.UUIDField(primary_key=True, db_column="heatmap_event_id")
    website = models.ForeignKey("Website", on_delete=models.RESTRICT, related_name="heatmap_events", db_index=False)
    session_id = models.UUIDField()
    visit_id = models.UUIDField()
    url_path = models.CharField(max_length=500)
    event_type = models.IntegerField()
    x = models.IntegerField(null=True)
    y = models.IntegerField(null=True)
    page_x = models.IntegerField(null=True)
    page_y = models.IntegerField(null=True)
    page_w = models.IntegerField(null=True)
    viewport_w = models.IntegerField(null=True)
    viewport_h = models.IntegerField(null=True)
    page_h = models.IntegerField(null=True)
    scroll_pct = models.IntegerField(null=True)
    created_at = models.DateTimeField(default=timezone.now)

    class Meta:
        db_table = "heatmap_event"
        indexes = [
            models.Index(fields=["website"]),
            models.Index(fields=["visit_id"]),
            models.Index(fields=["website", "created_at"]),
            models.Index(fields=["website", "url_path", "event_type", "created_at"]),
        ]


class TwoFactorAuth(models.Model):
    id = models.TextField(primary_key=True, default=None)
    user = models.OneToOneField("User", on_delete=models.CASCADE, related_name="two_factor_auth")
    secret = models.TextField()
    is_enabled = models.BooleanField(default=False)
    created_at = models.DateTimeField(default=timezone.now)
    updated_at = models.DateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "two_factor_auth"


class TwoFactorBackupCode(models.Model):
    id = models.TextField(primary_key=True, default=None)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="two_factor_backup_codes", db_index=False)
    code_hash = models.TextField()
    used = models.BooleanField(default=False)
    created_at = models.DateTimeField(default=timezone.now)

    class Meta:
        db_table = "two_factor_backup_code"
        indexes = [
            models.Index(fields=["user"]),
        ]


class TwoFactorOtpUsed(models.Model):
    id = models.TextField(primary_key=True, default=None)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="two_factor_otp_useds", db_index=False)
    otp = models.TextField()
    expires_at = models.DateTimeField()

    class Meta:
        db_table = "two_factor_otp_used"
        constraints = [
            models.UniqueConstraint(fields=["user", "otp"], name="two_factor_otp_used_user_id_otp_key"),
        ]


class TwoFactorRateLimit(models.Model):
    id = models.TextField(primary_key=True, default=None)
    user = models.OneToOneField("User", on_delete=models.CASCADE, related_name="two_factor_rate_limit")
    attempts = models.IntegerField(default=0)
    locked_until = models.DateTimeField(null=True)
    updated_at = models.DateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "two_factor_rate_limit"


class AppSetting(models.Model):
    key = models.TextField(primary_key=True, default=None)
    value = models.TextField()

    class Meta:
        db_table = "app_setting"
