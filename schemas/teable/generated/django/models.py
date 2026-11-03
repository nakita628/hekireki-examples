from datetime import datetime
from datetime import timezone as dt_timezone
from typing import Any, TypeVar

from django.conf import settings
from django.contrib.postgres.fields import ArrayField
from django.contrib.postgres.indexes import GinIndex
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


class ProvisionState(models.TextChoices):
    pending = "pending"
    ready = "ready"
    error = "error"
    deleting = "deleting"


class DataDbProvider(models.TextChoices):
    postgres = "postgres"


class DataDbConnectionStatus(models.TextChoices):
    pending = "pending"
    validating = "validating"
    ready = "ready"
    error = "error"
    migrating = "migrating"
    disabled = "disabled"


class DataDbConnectionHealthState(models.TextChoices):
    healthy = "healthy"
    read_only = "read_only"
    unreachable = "unreachable"
    degraded = "degraded"


class SpaceDataDbBindingMode(models.TextChoices):
    default = "default"
    byodb = "byodb"


class SpaceDataDbBindingState(models.TextChoices):
    ready = "ready"
    validating = "validating"
    initializing = "initializing"
    migrating = "migrating"
    error = "error"
    disabled = "disabled"


class SpaceDataDbMigrationJobState(models.TextChoices):
    pending = "pending"
    waiting_worker = "waiting_worker"
    preflight = "preflight"
    freezing_writes = "freezing_writes"
    copying = "copying"
    validating = "validating"
    switching = "switching"
    succeeded = "succeeded"
    failed = "failed"
    canceled = "canceled"
    rolled_back = "rolled_back"


class BaseDataDbMoveJobState(models.TextChoices):
    pending = "pending"
    waiting_worker = "waiting_worker"
    copying_base_schema = "copying_base_schema"
    copying_shared_rows = "copying_shared_rows"
    validating = "validating"
    switching = "switching"
    succeeded = "succeeded"
    failed = "failed"
    cancelled = "cancelled"


class Space(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    avatar = models.TextField(null=True)
    credit = models.IntegerField(null=True)
    deleted_time = UtcDateTimeField(null=True)
    created_time = UtcDateTimeField(default=timezone.now)
    created_by = models.TextField()
    last_modified_by = models.TextField(null=True)
    last_modified_time = UtcDateTimeField(null=True, auto_now=True)
    is_template = models.BooleanField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "space"


class DataDbConnection(models.Model):
    id = models.TextField(primary_key=True, default=None)
    provider = models.TextField(choices=DataDbProvider.choices, default=DataDbProvider.postgres)
    encrypted_url = models.TextField()
    url_fingerprint = models.TextField(unique=True)
    display_host = models.TextField(null=True)
    display_database = models.TextField(null=True)
    internal_schema = models.TextField()
    status = models.TextField(choices=DataDbConnectionStatus.choices, default=DataDbConnectionStatus.pending)
    schema_version = models.TextField(null=True)
    capabilities = models.JSONField(null=True)
    last_validated_at = UtcDateTimeField(null=True)
    last_error = models.TextField(null=True)
    health_state = models.TextField(choices=DataDbConnectionHealthState.choices, default=DataDbConnectionHealthState.healthy)
    health_reason = models.TextField(null=True)
    health_changed_at = UtcDateTimeField(null=True)
    last_health_check_at = UtcDateTimeField(null=True)
    consecutive_health_failures = models.IntegerField(default=0)
    created_by = models.TextField()
    created_time = UtcDateTimeField(default=timezone.now)
    last_modified_time = UtcDateTimeField(null=True, auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "data_db_connection"


class SpaceDataDbBinding(models.Model):
    id = models.TextField(primary_key=True, default=None)
    space = models.OneToOneField("Space", on_delete=models.CASCADE, related_name="data_db_binding")
    data_db_connection = models.ForeignKey("DataDbConnection", on_delete=models.SET_NULL, related_name="space_bindings", null=True, db_index=False)
    mode = models.TextField(choices=SpaceDataDbBindingMode.choices, default=SpaceDataDbBindingMode.default)
    state = models.TextField(choices=SpaceDataDbBindingState.choices, default=SpaceDataDbBindingState.ready)
    created_by = models.TextField()
    created_time = UtcDateTimeField(default=timezone.now)
    last_modified_time = UtcDateTimeField(null=True, auto_now=True)
    last_history_flushed_at = UtcDateTimeField(null=True)
    last_removal_flushed_at = UtcDateTimeField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "space_data_db_binding"
        indexes = [
            models.Index(fields=["data_db_connection"]),
        ]


class SpaceDataDbMigrationJob(models.Model):
    id = models.TextField(primary_key=True, default=None)
    space = models.ForeignKey("Space", on_delete=models.CASCADE, related_name="data_db_migration_jobs", db_index=False)
    source_connection = models.ForeignKey("DataDbConnection", on_delete=models.SET_NULL, related_name="source_migration_jobs", null=True, db_index=False)
    target_connection = models.ForeignKey("DataDbConnection", on_delete=models.SET_NULL, related_name="target_migration_jobs", null=True, db_index=False)
    target_mode = models.TextField(default="migrate-space")
    switch_on_completion = models.BooleanField(default=False)
    state = models.TextField(choices=SpaceDataDbMigrationJobState.choices, default=SpaceDataDbMigrationJobState.pending)
    target_url_fingerprint = models.TextField()
    target_internal_schema = models.TextField()
    inventory = models.JSONField(null=True)
    copy_stats = models.JSONField(null=True)
    validation_stats = models.JSONField(null=True)
    last_error = models.TextField(null=True)
    started_at = UtcDateTimeField(null=True)
    completed_at = UtcDateTimeField(null=True)
    created_by = models.TextField()
    created_time = UtcDateTimeField(default=timezone.now)
    last_modified_time = UtcDateTimeField(null=True, auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "space_data_db_migration_job"
        indexes = [
            models.Index(fields=["space", "state"]),
            models.Index(fields=["source_connection"]),
            models.Index(fields=["target_connection"]),
        ]


class BaseDataDbMoveJob(models.Model):
    id = models.TextField(primary_key=True, default=None)
    base = models.ForeignKey("Base", on_delete=models.CASCADE, related_name="data_db_move_jobs", db_index=False)
    source_space = models.ForeignKey("Space", on_delete=models.CASCADE, related_name="base_data_db_move_jobs_as_source", db_index=False)
    target_space = models.ForeignKey("Space", on_delete=models.CASCADE, related_name="base_data_db_move_jobs_as_target", db_index=False)
    source_connection_id = models.TextField(null=True)
    target_connection_id = models.TextField(null=True)
    state = models.TextField(choices=BaseDataDbMoveJobState.choices, default=BaseDataDbMoveJobState.pending)
    inventory = models.JSONField(null=True)
    copy_stats = models.JSONField(null=True)
    validation_stats = models.JSONField(null=True)
    last_error = models.TextField(null=True)
    started_at = UtcDateTimeField(null=True)
    completed_at = UtcDateTimeField(null=True)
    created_by = models.TextField()
    created_time = UtcDateTimeField(default=timezone.now)
    last_modified_time = UtcDateTimeField(null=True, auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "base_data_db_move_job"
        indexes = [
            models.Index(fields=["base", "state"]),
            models.Index(fields=["state"]),
            models.Index(fields=["source_space", "state"]),
            models.Index(fields=["target_space", "state"]),
        ]


class PinResource(models.Model):
    id = models.TextField(primary_key=True, default=None)
    type = models.TextField()
    resource_id = models.TextField()
    created_time = UtcDateTimeField(default=timezone.now)
    created_by = models.TextField()
    order = models.FloatField()

    class Meta:
        db_table = "pin_resource"
        constraints = [
            models.UniqueConstraint(fields=["created_by", "resource_id"], name="pin_resource_created_by_resource_id_key"),
        ]
        indexes = [
            models.Index(fields=["order"]),
        ]


class Base(models.Model):
    id = models.TextField(primary_key=True, default=None)
    space = models.ForeignKey("Space", on_delete=models.RESTRICT, related_name="base_group", db_index=False)
    name = models.TextField()
    order = models.FloatField()
    icon = models.TextField(null=True)
    schema_pass = models.TextField(null=True)
    v2_enabled = models.BooleanField(default=False)
    provision_state = models.TextField(choices=ProvisionState.choices, default=ProvisionState.ready)
    deleted_time = UtcDateTimeField(null=True)
    created_time = UtcDateTimeField(default=timezone.now)
    created_by = models.TextField()
    last_modified_by = models.TextField(null=True)
    last_modified_time = UtcDateTimeField(null=True, auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "base"
        indexes = [
            models.Index(fields=["order"]),
            models.Index(fields=["space"]),
        ]


class TableMeta(models.Model):
    id = models.TextField(primary_key=True, default=None)
    base = models.ForeignKey("Base", on_delete=models.RESTRICT, related_name="tables", db_index=False)
    name = models.TextField()
    description = models.TextField(null=True)
    icon = models.TextField(null=True)
    db_table_name = models.TextField()
    db_view_name = models.TextField(null=True)
    search_index = models.JSONField(null=True)
    search_index_policy = models.JSONField(null=True)
    query_index_policy = models.JSONField(null=True)
    provision_state = models.TextField(choices=ProvisionState.choices, default=ProvisionState.ready)
    version = models.IntegerField()
    order = models.FloatField()
    created_time = UtcDateTimeField(default=timezone.now)
    last_modified_time = UtcDateTimeField(null=True, auto_now=True)
    deleted_time = UtcDateTimeField(null=True)
    created_by = models.TextField()
    last_modified_by = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "table_meta"
        indexes = [
            models.Index(fields=["order"]),
            models.Index(fields=["db_table_name"]),
            models.Index(fields=["base", "deleted_time"]),
        ]


class TableQueryObservationShard(models.Model):
    pk = models.CompositePrimaryKey("table_id", "query_kind", "shape_hash", "window_start", "writer_id")
    space_id = models.TextField(null=True)
    base_id = models.TextField()
    table_id = models.TextField()
    query_kind = models.TextField()
    shape_hash = models.TextField()
    window_start = models.DateTimeField()
    writer_id = models.TextField()
    window_size_seconds = models.IntegerField()
    request_count = models.IntegerField()
    slow_count = models.IntegerField()
    timeout_count = models.IntegerField()
    db_error_count = models.IntegerField()
    total_duration_ms = models.FloatField()
    max_duration_ms = models.FloatField()
    total_db_duration_ms = models.FloatField(null=True)
    max_db_duration_ms = models.FloatField(null=True)
    shape = models.JSONField()
    sql_diagnostics = models.JSONField(null=True)
    created_time = models.DateTimeField(default=timezone.now)
    last_modified_time = models.DateTimeField(null=True)

    class Meta:
        db_table = "table_query_observation_shard"
        indexes = [
            models.Index(fields=["window_start"]),
            models.Index(fields=["table_id", "window_start"]),
            models.Index(fields=["query_kind", "table_id", "window_start"]),
            models.Index(fields=["base_id", "window_start"]),
        ]


class SchemaOperation(models.Model):
    id = models.TextField(primary_key=True, default=None)
    type = models.TextField()
    status = models.TextField()
    phase = models.TextField()
    resource_type = models.TextField()
    resource_id = models.TextField()
    base_id = models.TextField(null=True)
    table_id = models.TextField(null=True)
    idempotency_key = models.TextField(unique=True)
    payload = models.JSONField(null=True)
    result = models.JSONField(null=True)
    attempts = models.IntegerField(default=0)
    max_attempts = models.IntegerField(default=8)
    next_run_at = models.DateTimeField(default=timezone.now)
    locked_at = models.DateTimeField(null=True)
    locked_by = models.TextField(null=True)
    last_error = models.TextField(null=True)
    created_time = models.DateTimeField(default=timezone.now)
    created_by = models.TextField()
    last_modified_time = models.DateTimeField(null=True, auto_now=True)
    last_modified_by = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "schema_operation"
        indexes = [
            models.Index(fields=["status", "next_run_at"]),
            models.Index(fields=["resource_type", "resource_id", "status"]),
            models.Index(fields=["base_id", "status"]),
            models.Index(fields=["table_id", "status"]),
        ]


class Field(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    description = models.TextField(null=True)
    options = models.TextField(null=True)
    meta = models.TextField(null=True)
    ai_config = models.TextField(null=True)
    type = models.TextField()
    cell_value_type = models.TextField()
    is_multiple_cell_value = models.BooleanField(null=True)
    db_field_type = models.TextField()
    db_field_name = models.TextField()
    provision_state = models.TextField(choices=ProvisionState.choices, default=ProvisionState.ready)
    not_null = models.BooleanField(null=True)
    unique = models.BooleanField(null=True)
    is_primary = models.BooleanField(null=True)
    is_computed = models.BooleanField(null=True)
    is_lookup = models.BooleanField(null=True)
    is_conditional_lookup = models.BooleanField(null=True)
    is_pending = models.BooleanField(null=True)
    has_error = models.BooleanField(null=True)
    lookup_linked_field_id = models.TextField(null=True)
    lookup_options = models.TextField(null=True)
    table = models.ForeignKey("TableMeta", on_delete=models.RESTRICT, related_name="fields", db_index=False)
    order = models.FloatField()
    version = models.IntegerField()
    created_time = UtcDateTimeField(default=timezone.now)
    last_modified_time = UtcDateTimeField(null=True, auto_now=True)
    deleted_time = UtcDateTimeField(null=True)
    created_by = models.TextField()
    last_modified_by = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "field"
        indexes = [
            models.Index(fields=["lookup_linked_field_id"]),
            models.Index(fields=["table", "deleted_time"]),
        ]


class ComputedUpdateOutbox(models.Model):
    id = models.TextField(primary_key=True, default=None)
    base_id = models.TextField()
    seed_table_id = models.TextField()
    seed_record_ids = models.JSONField(null=True)
    change_type = models.TextField()
    steps = models.JSONField(null=True)
    edges = models.JSONField(null=True)
    status = models.TextField()
    attempts = models.IntegerField(default=0)
    max_attempts = models.IntegerField(default=8)
    next_run_at = UtcDateTimeField(default=timezone.now)
    locked_at = UtcDateTimeField(null=True)
    locked_by = models.TextField(null=True)
    last_error = models.TextField(null=True)
    estimated_complexity = models.IntegerField(default=0)
    plan_hash = models.TextField()
    dirty_stats = models.JSONField(null=True)
    run_id = models.TextField()
    origin_run_ids = ArrayField(models.TextField(), default=list)
    run_total_steps = models.IntegerField(default=0)
    run_completed_steps_before = models.IntegerField(default=0)
    affected_table_ids = ArrayField(models.TextField(), default=list)
    affected_field_ids = ArrayField(models.TextField(), default=list)
    sync_max_level = models.IntegerField(null=True)
    source_changed_at = UtcDateTimeField(null=True)
    stage_depth = models.IntegerField(default=0)
    predecessor_task_id = models.TextField(null=True)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "computed_update_outbox"
        indexes = [
            models.Index(fields=["status", "next_run_at"]),
            models.Index(fields=["base_id", "seed_table_id"]),
            models.Index(fields=["plan_hash"]),
            models.Index(fields=["run_id"]),
        ]


class ComputedUpdateOutboxSeed(models.Model):
    id = models.TextField(primary_key=True, default=None)
    task = models.ForeignKey("ComputedUpdateOutbox", on_delete=models.CASCADE, related_name="seeds", db_index=False)
    table_id = models.TextField()
    record_id = models.TextField()

    class Meta:
        db_table = "computed_update_outbox_seed"
        constraints = [
            models.UniqueConstraint(fields=["task", "table_id", "record_id"], name="computed_update_outbox_seed_task_id_table_id_record_id_key"),
        ]
        indexes = [
            models.Index(fields=["task"]),
        ]


class ComputedUpdateChangeFrontier(models.Model):
    pk = models.CompositePrimaryKey("scope_id", "kind", "table_id", "record_id", "field_id")
    scope_id = models.TextField()
    kind = models.TextField()
    table_id = models.TextField()
    record_id = models.TextField()
    field_id = models.TextField()

    class Meta:
        db_table = "computed_update_change_frontier"


class ComputedUpdateStageLedger(models.Model):
    pk = models.CompositePrimaryKey("scope_id", "kind", "table_id", "record_id")
    scope_id = models.TextField()
    kind = models.TextField()
    table_id = models.TextField()
    record_id = models.TextField()
    seq = models.BigIntegerField(default=0)

    class Meta:
        db_table = "computed_update_stage_ledger"
        indexes = [
            models.Index(fields=["scope_id", "kind", "seq"]),
        ]


class ComputedUpdateDeadLetter(models.Model):
    id = models.TextField(primary_key=True, default=None)
    base_id = models.TextField()
    seed_table_id = models.TextField()
    seed_record_ids = models.JSONField(null=True)
    change_type = models.TextField()
    steps = models.JSONField(null=True)
    edges = models.JSONField(null=True)
    status = models.TextField()
    attempts = models.IntegerField(default=0)
    max_attempts = models.IntegerField(default=8)
    next_run_at = UtcDateTimeField()
    locked_at = UtcDateTimeField(null=True)
    locked_by = models.TextField(null=True)
    last_error = models.TextField(null=True)
    estimated_complexity = models.IntegerField(default=0)
    plan_hash = models.TextField()
    dirty_stats = models.JSONField(null=True)
    run_id = models.TextField()
    origin_run_ids = ArrayField(models.TextField(), default=list)
    run_total_steps = models.IntegerField(default=0)
    run_completed_steps_before = models.IntegerField(default=0)
    affected_table_ids = ArrayField(models.TextField(), default=list)
    affected_field_ids = ArrayField(models.TextField(), default=list)
    sync_max_level = models.IntegerField(null=True)
    source_changed_at = UtcDateTimeField(null=True)
    stage_depth = models.IntegerField(default=0)
    predecessor_task_id = models.TextField(null=True)
    trace_data = models.JSONField(null=True)
    failed_at = UtcDateTimeField()
    created_at = UtcDateTimeField()
    updated_at = UtcDateTimeField()

    class Meta:
        db_table = "computed_update_dead_letter"
        indexes = [
            models.Index(fields=["base_id", "seed_table_id"]),
            models.Index(fields=["plan_hash"]),
            models.Index(fields=["run_id"]),
        ]


class ComputedUpdateRunHistory(models.Model):
    task_id = models.TextField(primary_key=True, default=None)
    base_id = models.TextField()
    seed_table_id = models.TextField()
    change_type = models.TextField()
    run_id = models.TextField()
    origin_run_ids = ArrayField(models.TextField(), default=list)
    steps = models.JSONField(null=True)
    edges = models.JSONField(null=True)
    affected_table_ids = ArrayField(models.TextField(), default=list)
    affected_field_ids = ArrayField(models.TextField(), default=list)
    source_field_ids = ArrayField(models.TextField(), default=list)
    seed_record_count = models.IntegerField(default=0)
    stage_depth = models.IntegerField(default=0)
    predecessor_task_id = models.TextField(null=True)
    run_total_steps = models.IntegerField(default=0)
    run_completed_steps_before = models.IntegerField(default=0)
    sync_max_level = models.IntegerField(null=True)
    estimated_complexity = models.IntegerField(default=0)
    attempts = models.IntegerField(default=0)
    outcome = models.TextField()
    source_changed_at = UtcDateTimeField(null=True)
    enqueued_at = UtcDateTimeField()
    started_at = UtcDateTimeField(null=True)
    completed_at = UtcDateTimeField()
    duration_ms = models.IntegerField(default=0)

    class Meta:
        db_table = "computed_update_run_history"
        indexes = [
            models.Index(fields=["run_id"]),
            models.Index(fields=["base_id", "completed_at"]),
            models.Index(fields=["completed_at"]),
            GinIndex(fields=["origin_run_ids"]),
        ]


class ComputedUpdatePauseScope(models.Model):
    id = models.TextField(primary_key=True, default=None)
    scope_type = models.TextField()
    scope_id = models.TextField()
    paused_at = UtcDateTimeField(default=timezone.now)
    paused_by = models.TextField(null=True)
    resume_at = UtcDateTimeField(null=True)
    reason = models.TextField(null=True)
    write_policy = models.TextField(default="allow_bounded")
    updated_at = UtcDateTimeField(auto_now=True)
    updated_by = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "computed_update_pause_scope"
        constraints = [
            models.UniqueConstraint(fields=["scope_type", "scope_id"], name="computed_update_pause_scope_scope_type_scope_id_key"),
        ]
        indexes = [
            models.Index(fields=["resume_at"]),
        ]


class ComputedFieldActivity(models.Model):
    field_id = models.TextField(primary_key=True, default=None)
    table_id = models.TextField()
    base_id = models.TextField()
    status = models.TextField()
    active_task_count = models.IntegerField(default=0)
    processing_task_count = models.IntegerField(default=0)
    generation = models.BigIntegerField(default=0)
    estimated_complexity = models.BigIntegerField(default=0)
    estimated_dirty_records = models.BigIntegerField(default=0)
    has_all_target_records = models.BooleanField(default=False)
    queued_at = UtcDateTimeField(null=True)
    started_at = UtcDateTimeField(null=True)
    last_completed_at = UtcDateTimeField(null=True)
    last_duration_ms = models.IntegerField(null=True)
    last_error = models.JSONField(null=True)
    extensions = models.JSONField(null=True)
    updated_at = UtcDateTimeField(default=timezone.now)

    class Meta:
        db_table = "computed_field_activity"
        indexes = [
            models.Index(fields=["table_id", "status"]),
            models.Index(fields=["base_id", "status"]),
        ]


class ComputedTableActivity(models.Model):
    table_id = models.TextField(primary_key=True, default=None)
    base_id = models.TextField()
    status = models.TextField()
    calculating_field_count = models.IntegerField(default=0)
    queued_field_count = models.IntegerField(default=0)
    estimated_complexity = models.BigIntegerField(default=0)
    recent_completions = models.JSONField(default=list)
    generation = models.BigIntegerField(default=0)
    updated_at = UtcDateTimeField(default=timezone.now)

    class Meta:
        db_table = "computed_table_activity"
        indexes = [
            models.Index(fields=["base_id", "status"]),
        ]


class ComputedTaskFieldRef(models.Model):
    pk = models.CompositePrimaryKey("task_id", "field_id")
    task_id = models.TextField()
    field_id = models.TextField()
    table_id = models.TextField()
    base_id = models.TextField()
    was_processing = models.BooleanField(default=False)
    created_at = UtcDateTimeField(default=timezone.now)

    class Meta:
        db_table = "computed_task_field_ref"
        indexes = [
            models.Index(fields=["field_id"]),
            models.Index(fields=["table_id"]),
        ]


class View(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    description = models.TextField(null=True)
    table = models.ForeignKey("TableMeta", on_delete=models.RESTRICT, related_name="views", db_index=False)
    type = models.TextField()
    sort = models.TextField(null=True)
    filter = models.TextField(null=True)
    group = models.TextField(null=True)
    options = models.TextField(null=True)
    order = models.FloatField()
    version = models.IntegerField()
    column_meta = models.TextField()
    is_locked = models.BooleanField(null=True)
    enable_share = models.BooleanField(null=True)
    share_id = models.TextField(null=True, unique=True)
    share_meta = models.TextField(null=True)
    created_time = UtcDateTimeField(default=timezone.now)
    last_modified_time = UtcDateTimeField(null=True, auto_now=True)
    deleted_time = UtcDateTimeField(null=True)
    created_by = models.TextField()
    last_modified_by = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "view"
        indexes = [
            models.Index(fields=["order"]),
            models.Index(fields=["table", "deleted_time"]),
        ]


class Ops(models.Model):
    id = models.TextField(primary_key=True, default=None)
    collection = models.TextField()
    doc_id = models.TextField()
    doc_type = models.TextField()
    version = models.IntegerField()
    operation = models.TextField()
    created_time = UtcDateTimeField(default=timezone.now)
    created_by = models.TextField()

    class Meta:
        db_table = "ops"
        constraints = [
            models.UniqueConstraint(fields=["collection", "doc_id", "version"], name="ops_collection_doc_id_version_key"),
        ]
        indexes = [
            models.Index(fields=["collection", "created_time"]),
        ]


class Reference(models.Model):
    id = models.TextField(primary_key=True, default=None)
    from_field_id = models.TextField()
    to_field_id = models.TextField()
    created_time = UtcDateTimeField(default=timezone.now)

    class Meta:
        db_table = "reference"
        constraints = [
            models.UniqueConstraint(fields=["to_field_id", "from_field_id"], name="reference_to_field_id_from_field_id_key"),
        ]
        indexes = [
            models.Index(fields=["from_field_id"]),
            models.Index(fields=["to_field_id"]),
        ]


class User(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    password = models.TextField(null=True)
    salt = models.TextField(null=True)
    phone = models.TextField(null=True, unique=True)
    email = models.TextField(unique=True)
    avatar = models.TextField(null=True)
    is_system = models.BooleanField(null=True)
    is_admin = models.BooleanField(null=True)
    is_trial_used = models.BooleanField(null=True)
    lang = models.TextField(null=True)
    notify_meta = models.TextField(null=True)
    last_sign_time = UtcDateTimeField(null=True)
    deactivated_time = UtcDateTimeField(null=True)
    created_time = UtcDateTimeField(default=timezone.now)
    deleted_time = UtcDateTimeField(null=True)
    last_modified_time = UtcDateTimeField(null=True, auto_now=True)
    permanent_deleted_time = UtcDateTimeField(null=True)
    ref_meta = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "users"


class Account(models.Model):
    id = models.TextField(primary_key=True, default=None)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="accounts", db_index=False)
    type = models.TextField()
    provider = models.TextField()
    provider_id = models.TextField()
    connection_id = models.TextField(default="")
    created_time = UtcDateTimeField(default=timezone.now)

    class Meta:
        db_table = "account"
        constraints = [
            models.UniqueConstraint(fields=["provider", "provider_id", "connection_id"], name="account_provider_provider_id_connection_id_key"),
        ]


class Attachments(models.Model):
    id = models.TextField(primary_key=True, default=None)
    token = models.TextField(unique=True)
    hash = models.TextField()
    size = models.BigIntegerField()
    mimetype = models.TextField()
    path = models.TextField()
    width = models.IntegerField(null=True)
    height = models.IntegerField(null=True)
    deleted_time = UtcDateTimeField(null=True)
    created_time = UtcDateTimeField(default=timezone.now)
    created_by = models.TextField()
    last_modified_by = models.TextField(null=True)
    thumbnail_path = models.TextField(null=True)

    class Meta:
        db_table = "attachments"


class AttachmentsTable(models.Model):
    id = models.TextField(primary_key=True, default=None)
    attachment_id = models.TextField()
    name = models.TextField()
    token = models.TextField()
    table_id = models.TextField()
    record_id = models.TextField()
    field_id = models.TextField()
    created_time = UtcDateTimeField(default=timezone.now)
    created_by = models.TextField()
    last_modified_by = models.TextField(null=True)
    last_modified_time = UtcDateTimeField(null=True, auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "attachments_table"
        indexes = [
            models.Index(fields=["table_id", "record_id"]),
            models.Index(fields=["table_id", "field_id"]),
            models.Index(fields=["attachment_id"]),
        ]


class Collaborator(models.Model):
    id = models.TextField(primary_key=True, default=None)
    role_name = models.TextField()
    resource_type = models.TextField()
    resource_id = models.TextField()
    principal_id = models.TextField()
    principal_type = models.TextField()
    created_by = models.TextField()
    created_time = UtcDateTimeField(default=timezone.now)
    last_modified_time = UtcDateTimeField(null=True, auto_now=True)
    last_modified_by = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "collaborator"
        constraints = [
            models.UniqueConstraint(fields=["resource_type", "resource_id", "principal_id", "principal_type"], name="collaborator_resource_type_resource_id_principal_id_principal_type_key"),
        ]
        indexes = [
            models.Index(fields=["resource_id"]),
            models.Index(fields=["principal_id"]),
        ]


class Invitation(models.Model):
    id = models.TextField(primary_key=True, default=None)
    base_id = models.TextField(null=True)
    space_id = models.TextField(null=True)
    type = models.TextField()
    role = models.TextField()
    invitation_code = models.TextField()
    expired_time = UtcDateTimeField(null=True)
    created_by = models.TextField(db_column="create_by")
    created_time = UtcDateTimeField(default=timezone.now)
    last_modified_time = UtcDateTimeField(null=True, auto_now=True)
    last_modified_by = models.TextField(null=True)
    deleted_time = UtcDateTimeField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "invitation"
        indexes = [
            models.Index(fields=["base_id"]),
            models.Index(fields=["space_id"]),
        ]


class InvitationRecord(models.Model):
    id = models.TextField(primary_key=True, default=None)
    invitation_id = models.TextField()
    base_id = models.TextField(null=True)
    space_id = models.TextField(null=True)
    type = models.TextField()
    inviter = models.TextField()
    accepter = models.TextField()
    created_time = UtcDateTimeField(default=timezone.now)

    class Meta:
        db_table = "invitation_record"
        indexes = [
            models.Index(fields=["invitation_id"]),
            models.Index(fields=["base_id"]),
            models.Index(fields=["space_id"]),
        ]


class Notification(models.Model):
    id = models.TextField(primary_key=True, default=None)
    from_user_id = models.TextField()
    to_user_id = models.TextField()
    type = models.TextField()
    message = models.TextField()
    message_i18n = models.TextField(null=True)
    severity = models.TextField(default="info")
    url_path = models.TextField(null=True)
    is_read = models.BooleanField(default=False)
    created_time = UtcDateTimeField(default=timezone.now)
    created_by = models.TextField()

    class Meta:
        db_table = "notification"
        indexes = [
            models.Index(fields=["to_user_id", "is_read", "created_time"]),
            models.Index(fields=["created_time"]),
        ]


class AccessToken(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    description = models.TextField(null=True)
    user_id = models.TextField()
    scopes = models.TextField()
    space_ids = models.TextField(null=True)
    base_ids = models.TextField(null=True)
    sign = models.TextField()
    client_id = models.TextField(null=True)
    has_full_access = models.BooleanField(null=True)
    expired_time = UtcDateTimeField()
    last_used_time = UtcDateTimeField(null=True)
    created_time = UtcDateTimeField(default=timezone.now)
    last_modified_time = UtcDateTimeField(null=True, auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "access_token"
        indexes = [
            models.Index(fields=["user_id"]),
            models.Index(fields=["client_id"]),
        ]


class Setting(models.Model):
    name = models.TextField(primary_key=True, default=None)
    content = models.TextField(null=True)
    created_time = UtcDateTimeField(default=timezone.now)
    last_modified_time = UtcDateTimeField(null=True, auto_now=True)
    created_by = models.TextField()
    last_modified_by = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "setting"


class OAuthApp(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    logo = models.TextField(null=True)
    homepage = models.TextField()
    description = models.TextField(null=True)
    client_id = models.TextField(unique=True)
    redirect_uris = models.TextField(null=True)
    scopes = models.TextField(null=True)
    allow_device_flow = models.BooleanField(default=False)
    dynamic_registration = models.BooleanField(default=False)
    created_time = UtcDateTimeField(default=timezone.now)
    last_modified_time = UtcDateTimeField(null=True, auto_now=True)
    created_by = models.TextField()
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "oauth_app"


class OAuthAppAuthorized(models.Model):
    id = models.TextField(primary_key=True, default=None)
    o_auth_app = models.ForeignKey("OAuthApp", on_delete=models.CASCADE, related_name="authorized", to_field="client_id", db_column="client_id", db_index=False)
    user_id = models.TextField()
    authorized_time = UtcDateTimeField()

    class Meta:
        db_table = "oauth_app_authorized"
        constraints = [
            models.UniqueConstraint(fields=["o_auth_app", "user_id"], name="oauth_app_authorized_client_id_user_id_key"),
        ]


class OAuthAppSecret(models.Model):
    id = models.TextField(primary_key=True, default=None)
    o_auth_app = models.ForeignKey("OAuthApp", on_delete=models.CASCADE, related_name="secrets", to_field="client_id", db_column="client_id", db_index=False)
    secret = models.TextField(unique=True)
    masked_secret = models.TextField()
    created_time = UtcDateTimeField(default=timezone.now)
    created_by = models.TextField()
    last_used_time = UtcDateTimeField(null=True)

    class Meta:
        db_table = "oauth_app_secret"


class OAuthAppToken(models.Model):
    id = models.TextField(primary_key=True, default=None)
    o_auth_app = models.ForeignKey("OAuthApp", on_delete=models.CASCADE, related_name="tokens", to_field="client_id", db_column="client_id", db_index=False)
    o_auth_app_secret = models.ForeignKey("OAuthAppSecret", on_delete=models.CASCADE, related_name="tokens", db_column="app_secret_id", null=True, db_index=False)
    refresh_token_sign = models.TextField(unique=True)
    expired_time = UtcDateTimeField()
    created_time = UtcDateTimeField(default=timezone.now)
    created_by = models.TextField()

    class Meta:
        db_table = "oauth_app_token"
        indexes = [
            models.Index(fields=["o_auth_app"]),
        ]


class RecordHistory(models.Model):
    id = models.TextField(primary_key=True, default=None)
    table_id = models.TextField()
    record_id = models.TextField()
    field_id = models.TextField()
    before = models.TextField()
    after = models.TextField()
    created_time = UtcDateTimeField(default=timezone.now)
    created_by = models.TextField()

    class Meta:
        db_table = "record_history"
        indexes = [
            models.Index(fields=["table_id", "record_id", "created_time"]),
            models.Index(fields=["table_id", "created_time"]),
        ]


class Trash(models.Model):
    id = models.TextField(primary_key=True, default=None)
    resource_type = models.TextField()
    resource_id = models.TextField()
    parent_id = models.TextField(null=True)
    deleted_time = UtcDateTimeField(default=timezone.now)
    deleted_by = models.TextField()

    class Meta:
        db_table = "trash"
        constraints = [
            models.UniqueConstraint(fields=["resource_type", "resource_id"], name="trash_resource_type_resource_id_key"),
        ]


class TableTrash(models.Model):
    id = models.TextField(primary_key=True, default=None)
    table_id = models.TextField()
    resource_type = models.TextField()
    snapshot = models.TextField()
    created_time = UtcDateTimeField(default=timezone.now)
    created_by = models.TextField()

    class Meta:
        db_table = "table_trash"
        indexes = [
            models.Index(fields=["table_id"]),
        ]


class RecordTrash(models.Model):
    id = models.TextField(primary_key=True, default=None)
    table_id = models.TextField()
    record_id = models.TextField()
    snapshot = models.TextField()
    created_time = UtcDateTimeField(default=timezone.now)
    created_by = models.TextField()
    reason = models.TextField(default="deleted")
    record_created_time = UtcDateTimeField(null=True)
    record_created_by = models.TextField(null=True)
    record_last_modified_time = UtcDateTimeField(null=True)
    record_last_modified_by = models.TextField(null=True)
    operation_id = models.TextField(null=True)

    class Meta:
        db_table = "record_trash"
        indexes = [
            models.Index(fields=["table_id", "record_id"]),
        ]


class RecordRemovalTombstone(models.Model):
    id = models.TextField(primary_key=True, default=None)
    table_id = models.TextField()
    record_id = models.TextField()
    type = models.TextField()
    created_time = UtcDateTimeField(default=timezone.now)

    class Meta:
        db_table = "record_removal_tombstone"
        indexes = [
            models.Index(fields=["table_id", "record_id"]),
        ]


class Plugin(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    description = models.TextField(null=True)
    detail_desc = models.TextField(null=True)
    logo = models.TextField()
    help_url = models.TextField(null=True)
    status = models.TextField()
    positions = models.TextField()
    url = models.TextField(null=True)
    secret = models.TextField(unique=True)
    masked_secret = models.TextField()
    i18n = models.TextField(null=True)
    config = models.TextField(null=True)
    plugin_user = models.TextField(null=True)
    created_time = UtcDateTimeField(default=timezone.now)
    last_modified_time = UtcDateTimeField(null=True, auto_now=True)
    created_by = models.TextField()
    last_modified_by = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "plugin"


class PluginInstall(models.Model):
    id = models.TextField(primary_key=True, default=None)
    plugin = models.ForeignKey("Plugin", on_delete=models.CASCADE, related_name="plugin_install", db_index=False)
    base_id = models.TextField()
    name = models.TextField()
    position_id = models.TextField()
    position = models.TextField()
    storage = models.TextField(null=True)
    created_time = UtcDateTimeField(default=timezone.now)
    created_by = models.TextField()
    last_modified_time = UtcDateTimeField(null=True, auto_now=True)
    last_modified_by = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "plugin_install"
        indexes = [
            models.Index(fields=["position_id"]),
            models.Index(fields=["base_id"]),
        ]


class Dashboard(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    base_id = models.TextField()
    layout = models.TextField(null=True)
    created_by = models.TextField()
    created_time = UtcDateTimeField(default=timezone.now)
    last_modified_time = UtcDateTimeField(null=True, auto_now=True)
    last_modified_by = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "dashboard"
        indexes = [
            models.Index(fields=["base_id"]),
        ]


class Comment(models.Model):
    id = models.TextField(primary_key=True, default=None)
    table_id = models.TextField()
    record_id = models.TextField()
    quote_id = models.TextField(null=True, db_column="quote_Id")
    content = models.TextField(null=True)
    reaction = models.TextField(null=True)
    deleted_time = UtcDateTimeField(null=True)
    created_time = UtcDateTimeField(default=timezone.now)
    created_by = models.TextField()
    last_modified_time = UtcDateTimeField(null=True, auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "comment"
        indexes = [
            models.Index(fields=["table_id", "record_id"]),
        ]


class CommentSubscription(models.Model):
    id = models.TextField(primary_key=True, default=None)
    table_id = models.TextField()
    record_id = models.TextField()
    created_by = models.TextField()
    created_time = UtcDateTimeField(default=timezone.now)

    class Meta:
        db_table = "comment_subscription"
        constraints = [
            models.UniqueConstraint(fields=["table_id", "record_id"], name="comment_subscription_table_id_record_id_key"),
        ]
        indexes = [
            models.Index(fields=["table_id", "record_id"]),
        ]


class Integration(models.Model):
    id = models.TextField(primary_key=True, default=None)
    resource_id = models.TextField(unique=True)
    config = models.TextField()
    type = models.TextField()
    enable = models.BooleanField(null=True)
    created_time = UtcDateTimeField(default=timezone.now)
    last_modified_time = UtcDateTimeField(null=True, auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "integration"
        indexes = [
            models.Index(fields=["resource_id"]),
        ]


class PluginPanel(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    table = models.ForeignKey("TableMeta", on_delete=models.CASCADE, related_name="plugin_panel", db_index=False)
    layout = models.TextField(null=True)
    created_by = models.TextField()
    created_time = UtcDateTimeField(default=timezone.now)
    last_modified_time = UtcDateTimeField(null=True, auto_now=True)
    last_modified_by = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "plugin_panel"


class PluginContextMenu(models.Model):
    id = models.TextField(primary_key=True, default=None)
    table = models.ForeignKey("TableMeta", on_delete=models.CASCADE, related_name="plugin_context_menu", db_index=False)
    plugin_install_id = models.TextField(unique=True)
    order = models.FloatField()
    created_time = UtcDateTimeField(default=timezone.now)
    created_by = models.TextField()
    last_modified_time = UtcDateTimeField(null=True, auto_now=True)
    last_modified_by = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "plugin_context_menu"


class UserLastVisit(models.Model):
    id = models.TextField(primary_key=True, default=None)
    user_id = models.TextField()
    resource_type = models.TextField()
    resource_id = models.TextField()
    parent_resource_id = models.TextField()
    last_visit_time = UtcDateTimeField(default=timezone.now)

    class Meta:
        db_table = "user_last_visit"
        constraints = [
            models.UniqueConstraint(fields=["user_id", "resource_type", "resource_id"], name="user_last_visit_user_id_resource_type_resource_id_key"),
        ]
        indexes = [
            models.Index(fields=["user_id", "resource_type", "parent_resource_id"]),
        ]


class UserBaseOrder(models.Model):
    id = models.TextField(primary_key=True, default=None)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="base_orders", db_index=False)
    base = models.ForeignKey("Base", on_delete=models.CASCADE, related_name="user_orders", db_index=False)
    order = models.FloatField()
    created_time = UtcDateTimeField(default=timezone.now)
    last_modified_time = UtcDateTimeField(null=True, auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "user_base_order"
        constraints = [
            models.UniqueConstraint(fields=["user", "base"], name="user_base_order_user_id_base_id_key"),
        ]
        indexes = [
            models.Index(fields=["user", "order"]),
            models.Index(fields=["base"]),
        ]


class Template(models.Model):
    id = models.TextField(primary_key=True, default=None)
    base_id = models.TextField(null=True, unique=True)
    cover = models.TextField(null=True)
    name = models.TextField(null=True)
    description = models.TextField(null=True)
    markdown_description = models.TextField(null=True)
    category_id = ArrayField(models.TextField())
    created_time = UtcDateTimeField(default=timezone.now)
    created_by = models.TextField()
    last_modified_time = UtcDateTimeField(null=True, auto_now=True)
    last_modified_by = models.TextField(null=True)
    is_system = models.BooleanField(null=True)
    is_published = models.BooleanField(null=True)
    featured = models.BooleanField(null=True)
    snapshot = models.TextField(null=True)
    order = models.FloatField()
    usage_count = models.IntegerField(default=0)
    publish_info = models.JSONField(null=True)
    visit_count = models.IntegerField(default=0)
    kind = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "template"
        constraints = [
            models.UniqueConstraint(fields=["base_id"], name="template_base_id_key"),
        ]


class TemplateCategory(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField(unique=True)
    created_time = UtcDateTimeField(default=timezone.now)
    created_by = models.TextField()
    last_modified_time = UtcDateTimeField(null=True, auto_now=True)
    last_modified_by = models.TextField(null=True)
    order = models.FloatField()
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "template_category"


class Task(models.Model):
    id = models.TextField(primary_key=True, default=None)
    type = models.TextField()
    status = models.TextField()
    snapshot = models.TextField(null=True)
    created_time = UtcDateTimeField(default=timezone.now)
    last_modified_time = UtcDateTimeField(null=True, auto_now=True)
    created_by = models.TextField()
    last_modified_by = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "task"
        indexes = [
            models.Index(fields=["type", "status"]),
        ]


class TaskRun(models.Model):
    id = models.TextField(primary_key=True, default=None)
    task = models.ForeignKey("Task", on_delete=models.CASCADE, related_name="runs", db_index=False)
    base_id = models.TextField()
    status = models.TextField()
    snapshot = models.TextField()
    depends_on_run_ids = ArrayField(models.TextField(), default=list)
    spent = models.IntegerField(null=True)
    log = models.TextField(null=True)
    error_msg = models.TextField(null=True)
    started_time = UtcDateTimeField(null=True)
    created_time = UtcDateTimeField(default=timezone.now)
    last_modified_time = UtcDateTimeField(null=True, auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "task_run"
        indexes = [
            models.Index(fields=["task", "status"]),
            models.Index(fields=["status", "base_id", "created_time"]),
            models.Index(fields=["status", "last_modified_time"]),
        ]


class TaskReference(models.Model):
    id = models.TextField(primary_key=True, default=None)
    from_field_id = models.TextField()
    to_field_id = models.TextField()
    created_time = UtcDateTimeField(default=timezone.now)

    class Meta:
        db_table = "task_reference"
        constraints = [
            models.UniqueConstraint(fields=["to_field_id", "from_field_id"], name="task_reference_to_field_id_from_field_id_key"),
        ]
        indexes = [
            models.Index(fields=["from_field_id"]),
            models.Index(fields=["to_field_id"]),
        ]


class Waitlist(models.Model):
    email = models.TextField(primary_key=True, default=None)
    invite = models.BooleanField(null=True)
    invite_time = UtcDateTimeField(null=True)
    created_time = UtcDateTimeField(default=timezone.now)

    class Meta:
        db_table = "waitlist"


class BaseNode(models.Model):
    id = models.TextField(primary_key=True, default=None)
    parent = models.ForeignKey("self", on_delete=models.SET_NULL, related_name="children", null=True, db_index=False)
    base_id = models.TextField()
    resource_type = models.TextField()
    resource_id = models.TextField()
    order = models.FloatField()
    created_time = UtcDateTimeField(default=timezone.now)
    created_by = models.TextField()
    last_modified_time = UtcDateTimeField(null=True, auto_now=True)
    last_modified_by = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "base_node"
        constraints = [
            models.UniqueConstraint(fields=["base_id", "resource_type", "resource_id"], name="base_node_base_id_resource_type_resource_id_key"),
        ]


class BaseNodeFolder(models.Model):
    id = models.TextField(primary_key=True, default=None)
    base_id = models.TextField()
    name = models.TextField()
    created_time = UtcDateTimeField(default=timezone.now)
    created_by = models.TextField()
    last_modified_time = UtcDateTimeField(null=True, auto_now=True)
    last_modified_by = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "base_node_folder"
        constraints = [
            models.UniqueConstraint(fields=["base_id", "name"], name="base_node_folder_base_id_name_key"),
        ]


class BaseShare(models.Model):
    id = models.TextField(primary_key=True, default=None)
    base_id = models.TextField()
    share_id = models.TextField(unique=True)
    password = models.TextField(null=True)
    node_id = models.TextField(null=True)
    allow_save = models.BooleanField(null=True)
    allow_copy = models.BooleanField(null=True)
    allow_edit = models.BooleanField(null=True)
    enabled = models.BooleanField(default=True)
    created_time = UtcDateTimeField(default=timezone.now)
    created_by = models.TextField()
    last_modified_time = UtcDateTimeField(null=True, auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "base_share"
        constraints = [
            models.UniqueConstraint(fields=["base_id", "node_id"], name="base_share_base_id_node_id_key"),
        ]
        indexes = [
            models.Index(fields=["base_id"]),
        ]


class ShortLink(models.Model):
    id = models.TextField(primary_key=True, default=None)
    code = models.CharField(max_length=32, unique=True)
    type = models.CharField(max_length=32)
    resource_id = models.TextField()
    created_time = UtcDateTimeField(default=timezone.now)
    created_by = models.TextField()
    deleted_time = UtcDateTimeField(null=True)

    class Meta:
        db_table = "short_link"
        constraints = [
            models.UniqueConstraint(fields=["type", "resource_id"], name="short_link_type_resource_id_key"),
        ]
        indexes = [
            models.Index(fields=["deleted_time"]),
        ]
