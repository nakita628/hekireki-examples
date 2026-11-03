from sqlalchemy import ARRAY, BigInteger, Dialect, Enum, ForeignKey, Index, MetaData, String, Text, TypeDecorator, text
from sqlalchemy.dialects.postgresql import JSONB, TIMESTAMP
from sqlalchemy.orm import DeclarativeBase, Mapped, mapped_column, relationship
from typing import Any, Optional
from datetime import datetime, timezone


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


class Space(Base):
    __tablename__ = "space"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    avatar: Mapped[Optional[str]]
    credit: Mapped[Optional[int]]
    deleted_time: Mapped[Optional[datetime]]
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[str]
    last_modified_by: Mapped[Optional[str]]
    last_modified_time: Mapped[Optional[datetime]] = mapped_column(default=utc_now, onupdate=utc_now)
    is_template: Mapped[Optional[bool]]

    base_group: Mapped[list["Base_"]] = relationship(passive_deletes="all", back_populates="space")
    data_db_migration_jobs: Mapped[list["SpaceDataDbMigrationJob"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="space")
    base_data_db_move_jobs_as_source: Mapped[list["BaseDataDbMoveJob"]] = relationship(foreign_keys="BaseDataDbMoveJob.source_space_id", cascade="all, delete", passive_deletes=True, back_populates="source_space")
    base_data_db_move_jobs_as_target: Mapped[list["BaseDataDbMoveJob"]] = relationship(foreign_keys="BaseDataDbMoveJob.target_space_id", cascade="all, delete", passive_deletes=True, back_populates="target_space")
    data_db_binding: Mapped[Optional["SpaceDataDbBinding"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="space")

class DataDbConnection(Base):
    __tablename__ = "data_db_connection"

    id: Mapped[str] = mapped_column(primary_key=True)
    provider: Mapped[str] = mapped_column(Enum("postgres", name="DataDbProvider"), default="postgres", server_default=text("'postgres'"))
    encrypted_url: Mapped[str]
    url_fingerprint: Mapped[str]
    display_host: Mapped[Optional[str]]
    display_database: Mapped[Optional[str]]
    internal_schema: Mapped[str]
    status: Mapped[str] = mapped_column(Enum("pending", "validating", "ready", "error", "migrating", "disabled", name="DataDbConnectionStatus"), default="pending", server_default=text("'pending'"))
    schema_version: Mapped[Optional[str]]
    capabilities: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    last_validated_at: Mapped[Optional[datetime]]
    last_error: Mapped[Optional[str]]
    health_state: Mapped[str] = mapped_column(Enum("healthy", "read_only", "unreachable", "degraded", name="DataDbConnectionHealthState"), default="healthy", server_default=text("'healthy'"))
    health_reason: Mapped[Optional[str]]
    health_changed_at: Mapped[Optional[datetime]]
    last_health_check_at: Mapped[Optional[datetime]]
    consecutive_health_failures: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    created_by: Mapped[str]
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_modified_time: Mapped[Optional[datetime]] = mapped_column(default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("data_db_connection_url_fingerprint_key", "url_fingerprint", unique=True),
    )

    space_bindings: Mapped[list["SpaceDataDbBinding"]] = relationship(back_populates="data_db_connection")
    source_migration_jobs: Mapped[list["SpaceDataDbMigrationJob"]] = relationship(foreign_keys="SpaceDataDbMigrationJob.source_connection_id", back_populates="source_connection")
    target_migration_jobs: Mapped[list["SpaceDataDbMigrationJob"]] = relationship(foreign_keys="SpaceDataDbMigrationJob.target_connection_id", back_populates="target_connection")

class SpaceDataDbBinding(Base):
    __tablename__ = "space_data_db_binding"

    id: Mapped[str] = mapped_column(primary_key=True)
    space_id: Mapped[str] = mapped_column(ForeignKey("space.id", ondelete="CASCADE", onupdate="CASCADE", name="space_data_db_binding_space_id_fkey"))
    data_db_connection_id: Mapped[Optional[str]] = mapped_column(ForeignKey("data_db_connection.id", ondelete="SET NULL", onupdate="CASCADE", name="space_data_db_binding_data_db_connection_id_fkey"))
    mode: Mapped[str] = mapped_column(Enum("default", "byodb", name="SpaceDataDbBindingMode"), default="default", server_default=text("'default'"))
    state: Mapped[str] = mapped_column(Enum("ready", "validating", "initializing", "migrating", "error", "disabled", name="SpaceDataDbBindingState"), default="ready", server_default=text("'ready'"))
    created_by: Mapped[str]
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_modified_time: Mapped[Optional[datetime]] = mapped_column(default=utc_now, onupdate=utc_now)
    last_history_flushed_at: Mapped[Optional[datetime]]
    last_removal_flushed_at: Mapped[Optional[datetime]]

    __table_args__ = (
        Index("space_data_db_binding_space_id_key", "space_id", unique=True),
        Index("space_data_db_binding_data_db_connection_id_idx", "data_db_connection_id"),
    )

    space: Mapped["Space"] = relationship(back_populates="data_db_binding")
    data_db_connection: Mapped[Optional["DataDbConnection"]] = relationship(back_populates="space_bindings")

class SpaceDataDbMigrationJob(Base):
    __tablename__ = "space_data_db_migration_job"

    id: Mapped[str] = mapped_column(primary_key=True)
    space_id: Mapped[str] = mapped_column(ForeignKey("space.id", ondelete="CASCADE", onupdate="CASCADE", name="space_data_db_migration_job_space_id_fkey"))
    source_connection_id: Mapped[Optional[str]] = mapped_column(ForeignKey("data_db_connection.id", ondelete="SET NULL", onupdate="CASCADE", name="space_data_db_migration_job_source_connection_id_fkey"))
    target_connection_id: Mapped[Optional[str]] = mapped_column(ForeignKey("data_db_connection.id", ondelete="SET NULL", onupdate="CASCADE", name="space_data_db_migration_job_target_connection_id_fkey"))
    target_mode: Mapped[str] = mapped_column(default="migrate-space", server_default=text("'migrate-space'"))
    switch_on_completion: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    state: Mapped[str] = mapped_column(Enum("pending", "waiting_worker", "preflight", "freezing_writes", "copying", "validating", "switching", "succeeded", "failed", "canceled", "rolled_back", name="SpaceDataDbMigrationJobState"), default="pending", server_default=text("'pending'"))
    target_url_fingerprint: Mapped[str]
    target_internal_schema: Mapped[str]
    inventory: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    copy_stats: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    validation_stats: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    last_error: Mapped[Optional[str]]
    started_at: Mapped[Optional[datetime]]
    completed_at: Mapped[Optional[datetime]]
    created_by: Mapped[str]
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_modified_time: Mapped[Optional[datetime]] = mapped_column(default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("space_data_db_migration_job_space_id_state_idx", "space_id", "state"),
        Index("space_data_db_migration_job_source_connection_id_idx", "source_connection_id"),
        Index("space_data_db_migration_job_target_connection_id_idx", "target_connection_id"),
    )

    space: Mapped["Space"] = relationship(back_populates="data_db_migration_jobs")
    source_connection: Mapped[Optional["DataDbConnection"]] = relationship(foreign_keys=[source_connection_id], back_populates="source_migration_jobs")
    target_connection: Mapped[Optional["DataDbConnection"]] = relationship(foreign_keys=[target_connection_id], back_populates="target_migration_jobs")

class BaseDataDbMoveJob(Base):
    __tablename__ = "base_data_db_move_job"

    id: Mapped[str] = mapped_column(primary_key=True)
    base_id: Mapped[str] = mapped_column(ForeignKey("base.id", ondelete="CASCADE", onupdate="CASCADE", name="base_data_db_move_job_base_id_fkey"))
    source_space_id: Mapped[str] = mapped_column(ForeignKey("space.id", ondelete="CASCADE", onupdate="CASCADE", name="base_data_db_move_job_source_space_id_fkey"))
    target_space_id: Mapped[str] = mapped_column(ForeignKey("space.id", ondelete="CASCADE", onupdate="CASCADE", name="base_data_db_move_job_target_space_id_fkey"))
    source_connection_id: Mapped[Optional[str]]
    target_connection_id: Mapped[Optional[str]]
    state: Mapped[str] = mapped_column(Enum("pending", "waiting_worker", "copying_base_schema", "copying_shared_rows", "validating", "switching", "succeeded", "failed", "cancelled", name="BaseDataDbMoveJobState"), default="pending", server_default=text("'pending'"))
    inventory: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    copy_stats: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    validation_stats: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    last_error: Mapped[Optional[str]]
    started_at: Mapped[Optional[datetime]]
    completed_at: Mapped[Optional[datetime]]
    created_by: Mapped[str]
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_modified_time: Mapped[Optional[datetime]] = mapped_column(default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("base_data_db_move_job_base_id_state_idx", "base_id", "state"),
        Index("base_data_db_move_job_state_idx", "state"),
        Index("base_data_db_move_job_source_space_id_state_idx", "source_space_id", "state"),
        Index("base_data_db_move_job_target_space_id_state_idx", "target_space_id", "state"),
    )

    base: Mapped["Base_"] = relationship(back_populates="data_db_move_jobs")
    source_space: Mapped["Space"] = relationship(foreign_keys=[source_space_id], back_populates="base_data_db_move_jobs_as_source")
    target_space: Mapped["Space"] = relationship(foreign_keys=[target_space_id], back_populates="base_data_db_move_jobs_as_target")

class PinResource(Base):
    __tablename__ = "pin_resource"

    id: Mapped[str] = mapped_column(primary_key=True)
    type: Mapped[str]
    resource_id: Mapped[str]
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[str]
    order: Mapped[float]

    __table_args__ = (
        Index("pin_resource_order_idx", "order"),
        Index("pin_resource_created_by_resource_id_key", "created_by", "resource_id", unique=True),
    )

class Base_(Base):
    __tablename__ = "base"

    id: Mapped[str] = mapped_column(primary_key=True)
    space_id: Mapped[str] = mapped_column(ForeignKey("space.id", ondelete="RESTRICT", onupdate="CASCADE", name="base_space_id_fkey"))
    name: Mapped[str]
    order: Mapped[float]
    icon: Mapped[Optional[str]]
    schema_pass: Mapped[Optional[str]]
    v2_enabled: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    provision_state: Mapped[str] = mapped_column(Enum("pending", "ready", "error", "deleting", name="ProvisionState"), default="ready", server_default=text("'ready'"))
    deleted_time: Mapped[Optional[datetime]]
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[str]
    last_modified_by: Mapped[Optional[str]]
    last_modified_time: Mapped[Optional[datetime]] = mapped_column(default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("base_order_idx", "order"),
        Index("base_space_id_idx", "space_id"),
    )

    space: Mapped["Space"] = relationship(back_populates="base_group")
    tables: Mapped[list["TableMeta"]] = relationship(passive_deletes="all", back_populates="base")
    data_db_move_jobs: Mapped[list["BaseDataDbMoveJob"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="base")
    user_orders: Mapped[list["UserBaseOrder"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="base")

class TableMeta(Base):
    __tablename__ = "table_meta"

    id: Mapped[str] = mapped_column(primary_key=True)
    base_id: Mapped[str] = mapped_column(ForeignKey("base.id", ondelete="RESTRICT", onupdate="CASCADE", name="table_meta_base_id_fkey"))
    name: Mapped[str]
    description: Mapped[Optional[str]]
    icon: Mapped[Optional[str]]
    db_table_name: Mapped[str]
    db_view_name: Mapped[Optional[str]]
    search_index: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    search_index_policy: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    query_index_policy: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    provision_state: Mapped[str] = mapped_column(Enum("pending", "ready", "error", "deleting", name="ProvisionState"), default="ready", server_default=text("'ready'"))
    version: Mapped[int]
    order: Mapped[float]
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_modified_time: Mapped[Optional[datetime]] = mapped_column(default=utc_now, onupdate=utc_now)
    deleted_time: Mapped[Optional[datetime]]
    created_by: Mapped[str]
    last_modified_by: Mapped[Optional[str]]

    __table_args__ = (
        Index("table_meta_order_idx", "order"),
        Index("table_meta_db_table_name_idx", "db_table_name"),
        Index("table_meta_base_id_deleted_time_idx", "base_id", "deleted_time"),
    )

    base: Mapped["Base_"] = relationship(back_populates="tables")
    fields: Mapped[list["Field"]] = relationship(passive_deletes="all", back_populates="table")
    views: Mapped[list["View"]] = relationship(passive_deletes="all", back_populates="table")
    plugin_panel: Mapped[list["PluginPanel"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="table")
    plugin_context_menu: Mapped[list["PluginContextMenu"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="table")

class TableQueryObservationShard(Base):
    __tablename__ = "table_query_observation_shard"

    space_id: Mapped[Optional[str]]
    base_id: Mapped[str]
    table_id: Mapped[str] = mapped_column(primary_key=True)
    query_kind: Mapped[str] = mapped_column(primary_key=True)
    shape_hash: Mapped[str] = mapped_column(primary_key=True)
    window_start: Mapped[datetime] = mapped_column(UtcDateTimeTz(precision=6), primary_key=True)
    writer_id: Mapped[str] = mapped_column(primary_key=True)
    window_size_seconds: Mapped[int]
    request_count: Mapped[int]
    slow_count: Mapped[int]
    timeout_count: Mapped[int]
    db_error_count: Mapped[int]
    total_duration_ms: Mapped[float]
    max_duration_ms: Mapped[float]
    total_db_duration_ms: Mapped[Optional[float]]
    max_db_duration_ms: Mapped[Optional[float]]
    shape: Mapped[dict[str, Any]] = mapped_column(JSONB)
    sql_diagnostics: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    created_time: Mapped[datetime] = mapped_column(UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_modified_time: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6))

    __table_args__ = (
        Index("table_query_observation_shard_window_start_idx", "window_start"),
        Index("table_query_observation_shard_table_start_idx", "table_id", "window_start"),
        Index("table_query_observation_shard_search_activity_idx", "query_kind", "table_id", "window_start"),
        Index("table_query_observation_shard_base_start_idx", "base_id", "window_start"),
    )

class SchemaOperation(Base):
    __tablename__ = "schema_operation"

    id: Mapped[str] = mapped_column(primary_key=True)
    type: Mapped[str]
    status: Mapped[str]
    phase: Mapped[str]
    resource_type: Mapped[str]
    resource_id: Mapped[str]
    base_id: Mapped[Optional[str]]
    table_id: Mapped[Optional[str]]
    idempotency_key: Mapped[str]
    payload: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    result: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    attempts: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    max_attempts: Mapped[int] = mapped_column(default=8, server_default=text("8"))
    next_run_at: Mapped[datetime] = mapped_column(UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    locked_at: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6))
    locked_by: Mapped[Optional[str]]
    last_error: Mapped[Optional[str]]
    created_time: Mapped[datetime] = mapped_column(UtcDateTimeTz(precision=6), default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[str]
    last_modified_time: Mapped[Optional[datetime]] = mapped_column(UtcDateTimeTz(precision=6), default=utc_now, onupdate=utc_now)
    last_modified_by: Mapped[Optional[str]]

    __table_args__ = (
        Index("schema_operation_idempotency_key_key", "idempotency_key", unique=True),
        Index("schema_operation_status_next_run_at_idx", "status", "next_run_at"),
        Index("schema_operation_resource_status_idx", "resource_type", "resource_id", "status"),
        Index("schema_operation_base_status_idx", "base_id", "status"),
        Index("schema_operation_table_status_idx", "table_id", "status"),
    )

class Field(Base):
    __tablename__ = "field"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    description: Mapped[Optional[str]]
    options: Mapped[Optional[str]]
    meta: Mapped[Optional[str]]
    ai_config: Mapped[Optional[str]]
    type: Mapped[str]
    cell_value_type: Mapped[str]
    is_multiple_cell_value: Mapped[Optional[bool]]
    db_field_type: Mapped[str]
    db_field_name: Mapped[str]
    provision_state: Mapped[str] = mapped_column(Enum("pending", "ready", "error", "deleting", name="ProvisionState"), default="ready", server_default=text("'ready'"))
    not_null: Mapped[Optional[bool]]
    unique: Mapped[Optional[bool]]
    is_primary: Mapped[Optional[bool]]
    is_computed: Mapped[Optional[bool]]
    is_lookup: Mapped[Optional[bool]]
    is_conditional_lookup: Mapped[Optional[bool]]
    is_pending: Mapped[Optional[bool]]
    has_error: Mapped[Optional[bool]]
    lookup_linked_field_id: Mapped[Optional[str]]
    lookup_options: Mapped[Optional[str]]
    table_id: Mapped[str] = mapped_column(ForeignKey("table_meta.id", ondelete="RESTRICT", onupdate="CASCADE", name="field_table_id_fkey"))
    order: Mapped[float]
    version: Mapped[int]
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_modified_time: Mapped[Optional[datetime]] = mapped_column(default=utc_now, onupdate=utc_now)
    deleted_time: Mapped[Optional[datetime]]
    created_by: Mapped[str]
    last_modified_by: Mapped[Optional[str]]

    __table_args__ = (
        Index("field_lookup_linked_field_id_idx", "lookup_linked_field_id"),
        Index("field_table_id_deleted_time_idx", "table_id", "deleted_time"),
    )

    table: Mapped["TableMeta"] = relationship(back_populates="fields")

class ComputedUpdateOutbox(Base):
    __tablename__ = "computed_update_outbox"

    id: Mapped[str] = mapped_column(primary_key=True)
    base_id: Mapped[str]
    seed_table_id: Mapped[str]
    seed_record_ids: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    change_type: Mapped[str]
    steps: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    edges: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    status: Mapped[str]
    attempts: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    max_attempts: Mapped[int] = mapped_column(default=8, server_default=text("8"))
    next_run_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    locked_at: Mapped[Optional[datetime]]
    locked_by: Mapped[Optional[str]]
    last_error: Mapped[Optional[str]]
    estimated_complexity: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    plan_hash: Mapped[str]
    dirty_stats: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    run_id: Mapped[str]
    origin_run_ids: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    run_total_steps: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    run_completed_steps_before: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    affected_table_ids: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    affected_field_ids: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    sync_max_level: Mapped[Optional[int]]
    source_changed_at: Mapped[Optional[datetime]]
    stage_depth: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    predecessor_task_id: Mapped[Optional[str]]
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("computed_update_outbox_status_next_run_at_idx", "status", "next_run_at"),
        Index("computed_update_outbox_base_id_seed_table_id_idx", "base_id", "seed_table_id"),
        Index("computed_update_outbox_plan_hash_idx", "plan_hash"),
        Index("computed_update_outbox_run_id_idx", "run_id"),
    )

    seeds: Mapped[list["ComputedUpdateOutboxSeed"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="task")

class ComputedUpdateOutboxSeed(Base):
    __tablename__ = "computed_update_outbox_seed"

    id: Mapped[str] = mapped_column(primary_key=True)
    task_id: Mapped[str] = mapped_column(ForeignKey("computed_update_outbox.id", ondelete="CASCADE", onupdate="CASCADE", name="computed_update_outbox_seed_task_id_fkey"))
    table_id: Mapped[str]
    record_id: Mapped[str]

    __table_args__ = (
        Index("computed_update_outbox_seed_task_id_idx", "task_id"),
        Index("computed_update_outbox_seed_task_id_table_id_record_id_key", "task_id", "table_id", "record_id", unique=True),
    )

    task: Mapped["ComputedUpdateOutbox"] = relationship(back_populates="seeds")

class ComputedUpdateChangeFrontier(Base):
    __tablename__ = "computed_update_change_frontier"

    scope_id: Mapped[str] = mapped_column(primary_key=True)
    kind: Mapped[str] = mapped_column(primary_key=True)
    table_id: Mapped[str] = mapped_column(primary_key=True)
    record_id: Mapped[str] = mapped_column(primary_key=True)
    field_id: Mapped[str] = mapped_column(primary_key=True)

class ComputedUpdateStageLedger(Base):
    __tablename__ = "computed_update_stage_ledger"

    scope_id: Mapped[str] = mapped_column(primary_key=True)
    kind: Mapped[str] = mapped_column(primary_key=True)
    table_id: Mapped[str] = mapped_column(primary_key=True)
    record_id: Mapped[str] = mapped_column(primary_key=True)
    seq: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))

    __table_args__ = (
        Index("computed_update_stage_ledger_scope_id_kind_seq_idx", "scope_id", "kind", "seq"),
    )

class ComputedUpdateDeadLetter(Base):
    __tablename__ = "computed_update_dead_letter"

    id: Mapped[str] = mapped_column(primary_key=True)
    base_id: Mapped[str]
    seed_table_id: Mapped[str]
    seed_record_ids: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    change_type: Mapped[str]
    steps: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    edges: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    status: Mapped[str]
    attempts: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    max_attempts: Mapped[int] = mapped_column(default=8, server_default=text("8"))
    next_run_at: Mapped[datetime]
    locked_at: Mapped[Optional[datetime]]
    locked_by: Mapped[Optional[str]]
    last_error: Mapped[Optional[str]]
    estimated_complexity: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    plan_hash: Mapped[str]
    dirty_stats: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    run_id: Mapped[str]
    origin_run_ids: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    run_total_steps: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    run_completed_steps_before: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    affected_table_ids: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    affected_field_ids: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    sync_max_level: Mapped[Optional[int]]
    source_changed_at: Mapped[Optional[datetime]]
    stage_depth: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    predecessor_task_id: Mapped[Optional[str]]
    trace_data: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    failed_at: Mapped[datetime]
    created_at: Mapped[datetime]
    updated_at: Mapped[datetime]

    __table_args__ = (
        Index("computed_update_dead_letter_base_id_seed_table_id_idx", "base_id", "seed_table_id"),
        Index("computed_update_dead_letter_plan_hash_idx", "plan_hash"),
        Index("computed_update_dead_letter_run_id_idx", "run_id"),
    )

class ComputedUpdateRunHistory(Base):
    __tablename__ = "computed_update_run_history"

    task_id: Mapped[str] = mapped_column(primary_key=True)
    base_id: Mapped[str]
    seed_table_id: Mapped[str]
    change_type: Mapped[str]
    run_id: Mapped[str]
    origin_run_ids: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    steps: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    edges: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    affected_table_ids: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    affected_field_ids: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    source_field_ids: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    seed_record_count: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    stage_depth: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    predecessor_task_id: Mapped[Optional[str]]
    run_total_steps: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    run_completed_steps_before: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    sync_max_level: Mapped[Optional[int]]
    estimated_complexity: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    attempts: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    outcome: Mapped[str]
    source_changed_at: Mapped[Optional[datetime]]
    enqueued_at: Mapped[datetime]
    started_at: Mapped[Optional[datetime]]
    completed_at: Mapped[datetime]
    duration_ms: Mapped[int] = mapped_column(default=0, server_default=text("0"))

    __table_args__ = (
        Index("computed_update_run_history_run_id_idx", "run_id"),
        Index("computed_update_run_history_base_id_completed_at_idx", "base_id", "completed_at"),
        Index("computed_update_run_history_completed_at_idx", "completed_at"),
        Index("computed_update_run_history_origin_run_ids_gin", "origin_run_ids"),
    )

class ComputedUpdatePauseScope(Base):
    __tablename__ = "computed_update_pause_scope"

    id: Mapped[str] = mapped_column(primary_key=True)
    scope_type: Mapped[str]
    scope_id: Mapped[str]
    paused_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    paused_by: Mapped[Optional[str]]
    resume_at: Mapped[Optional[datetime]]
    reason: Mapped[Optional[str]]
    write_policy: Mapped[str] = mapped_column(default="allow_bounded", server_default=text("'allow_bounded'"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"), onupdate=utc_now)
    updated_by: Mapped[Optional[str]]

    __table_args__ = (
        Index("computed_update_pause_scope_resume_at_idx", "resume_at"),
        Index("computed_update_pause_scope_scope_type_scope_id_key", "scope_type", "scope_id", unique=True),
    )

class ComputedFieldActivity(Base):
    __tablename__ = "computed_field_activity"

    field_id: Mapped[str] = mapped_column(primary_key=True)
    table_id: Mapped[str]
    base_id: Mapped[str]
    status: Mapped[str]
    active_task_count: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    processing_task_count: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    generation: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    estimated_complexity: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    estimated_dirty_records: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    has_all_target_records: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    queued_at: Mapped[Optional[datetime]]
    started_at: Mapped[Optional[datetime]]
    last_completed_at: Mapped[Optional[datetime]]
    last_duration_ms: Mapped[Optional[int]]
    last_error: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    extensions: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("computed_field_activity_table_id_status_idx", "table_id", "status"),
        Index("computed_field_activity_base_id_status_idx", "base_id", "status"),
    )

class ComputedTableActivity(Base):
    __tablename__ = "computed_table_activity"

    table_id: Mapped[str] = mapped_column(primary_key=True)
    base_id: Mapped[str]
    status: Mapped[str]
    calculating_field_count: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    queued_field_count: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    estimated_complexity: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    recent_completions: Mapped[dict[str, Any]] = mapped_column(JSONB, default=lambda: [], server_default=text("'[]'"))
    generation: Mapped[int] = mapped_column(BigInteger, default=0, server_default=text("0"))
    updated_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("computed_table_activity_base_id_status_idx", "base_id", "status"),
    )

class ComputedTaskFieldRef(Base):
    __tablename__ = "computed_task_field_ref"

    task_id: Mapped[str] = mapped_column(primary_key=True)
    field_id: Mapped[str] = mapped_column(primary_key=True)
    table_id: Mapped[str]
    base_id: Mapped[str]
    was_processing: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    created_at: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("computed_task_field_ref_field_id_idx", "field_id"),
        Index("computed_task_field_ref_table_id_idx", "table_id"),
    )

class View(Base):
    __tablename__ = "view"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    description: Mapped[Optional[str]]
    table_id: Mapped[str] = mapped_column(ForeignKey("table_meta.id", ondelete="RESTRICT", onupdate="CASCADE", name="view_table_id_fkey"))
    type: Mapped[str]
    sort: Mapped[Optional[str]]
    filter: Mapped[Optional[str]]
    group: Mapped[Optional[str]]
    options: Mapped[Optional[str]]
    order: Mapped[float]
    version: Mapped[int]
    column_meta: Mapped[str]
    is_locked: Mapped[Optional[bool]]
    enable_share: Mapped[Optional[bool]]
    share_id: Mapped[Optional[str]]
    share_meta: Mapped[Optional[str]]
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_modified_time: Mapped[Optional[datetime]] = mapped_column(default=utc_now, onupdate=utc_now)
    deleted_time: Mapped[Optional[datetime]]
    created_by: Mapped[str]
    last_modified_by: Mapped[Optional[str]]

    __table_args__ = (
        Index("view_share_id_key", "share_id", unique=True),
        Index("view_order_idx", "order"),
        Index("view_table_id_deleted_time_idx", "table_id", "deleted_time"),
    )

    table: Mapped["TableMeta"] = relationship(back_populates="views")

class Ops(Base):
    __tablename__ = "ops"

    id: Mapped[str] = mapped_column(primary_key=True)
    collection: Mapped[str]
    doc_id: Mapped[str]
    doc_type: Mapped[str]
    version: Mapped[int]
    operation: Mapped[str]
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[str]

    __table_args__ = (
        Index("ops_collection_created_time_idx", "collection", "created_time"),
        Index("ops_collection_doc_id_version_key", "collection", "doc_id", "version", unique=True),
    )

class Reference(Base):
    __tablename__ = "reference"

    id: Mapped[str] = mapped_column(primary_key=True)
    from_field_id: Mapped[str]
    to_field_id: Mapped[str]
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("reference_from_field_id_idx", "from_field_id"),
        Index("reference_to_field_id_idx", "to_field_id"),
        Index("reference_to_field_id_from_field_id_key", "to_field_id", "from_field_id", unique=True),
    )

class User(Base):
    __tablename__ = "users"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    password: Mapped[Optional[str]]
    salt: Mapped[Optional[str]]
    phone: Mapped[Optional[str]]
    email: Mapped[str]
    avatar: Mapped[Optional[str]]
    is_system: Mapped[Optional[bool]]
    is_admin: Mapped[Optional[bool]]
    is_trial_used: Mapped[Optional[bool]]
    lang: Mapped[Optional[str]]
    notify_meta: Mapped[Optional[str]]
    last_sign_time: Mapped[Optional[datetime]]
    deactivated_time: Mapped[Optional[datetime]]
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    deleted_time: Mapped[Optional[datetime]]
    last_modified_time: Mapped[Optional[datetime]] = mapped_column(default=utc_now, onupdate=utc_now)
    permanent_deleted_time: Mapped[Optional[datetime]]
    ref_meta: Mapped[Optional[str]]

    __table_args__ = (
        Index("users_phone_key", "phone", unique=True),
        Index("users_email_key", "email", unique=True),
    )

    accounts: Mapped[list["Account"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    base_orders: Mapped[list["UserBaseOrder"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")

class Account(Base):
    __tablename__ = "account"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[str] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="account_user_id_fkey"))
    type: Mapped[str]
    provider: Mapped[str]
    provider_id: Mapped[str]
    connection_id: Mapped[str] = mapped_column(default="", server_default=text("''"))
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("account_provider_provider_id_connection_id_key", "provider", "provider_id", "connection_id", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="accounts")

class Attachments(Base):
    __tablename__ = "attachments"

    id: Mapped[str] = mapped_column(primary_key=True)
    token: Mapped[str]
    hash: Mapped[str]
    size: Mapped[int] = mapped_column(BigInteger)
    mimetype: Mapped[str]
    path: Mapped[str]
    width: Mapped[Optional[int]]
    height: Mapped[Optional[int]]
    deleted_time: Mapped[Optional[datetime]]
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[str]
    last_modified_by: Mapped[Optional[str]]
    thumbnail_path: Mapped[Optional[str]]

    __table_args__ = (
        Index("attachments_token_key", "token", unique=True),
    )

class AttachmentsTable(Base):
    __tablename__ = "attachments_table"

    id: Mapped[str] = mapped_column(primary_key=True)
    attachment_id: Mapped[str]
    name: Mapped[str]
    token: Mapped[str]
    table_id: Mapped[str]
    record_id: Mapped[str]
    field_id: Mapped[str]
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[str]
    last_modified_by: Mapped[Optional[str]]
    last_modified_time: Mapped[Optional[datetime]] = mapped_column(default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("attachments_table_table_id_record_id_idx", "table_id", "record_id"),
        Index("attachments_table_table_id_field_id_idx", "table_id", "field_id"),
        Index("attachments_table_attachment_id_idx", "attachment_id"),
    )

class Collaborator(Base):
    __tablename__ = "collaborator"

    id: Mapped[str] = mapped_column(primary_key=True)
    role_name: Mapped[str]
    resource_type: Mapped[str]
    resource_id: Mapped[str]
    principal_id: Mapped[str]
    principal_type: Mapped[str]
    created_by: Mapped[str]
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_modified_time: Mapped[Optional[datetime]] = mapped_column(default=utc_now, onupdate=utc_now)
    last_modified_by: Mapped[Optional[str]]

    __table_args__ = (
        Index("collaborator_resource_id_idx", "resource_id"),
        Index("collaborator_principal_id_idx", "principal_id"),
        Index("collaborator_resource_type_resource_id_principal_id_princip_key", "resource_type", "resource_id", "principal_id", "principal_type", unique=True),
    )

class Invitation(Base):
    __tablename__ = "invitation"

    id: Mapped[str] = mapped_column(primary_key=True)
    base_id: Mapped[Optional[str]]
    space_id: Mapped[Optional[str]]
    type: Mapped[str]
    role: Mapped[str]
    invitation_code: Mapped[str]
    expired_time: Mapped[Optional[datetime]]
    created_by: Mapped[str] = mapped_column("create_by")
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_modified_time: Mapped[Optional[datetime]] = mapped_column(default=utc_now, onupdate=utc_now)
    last_modified_by: Mapped[Optional[str]]
    deleted_time: Mapped[Optional[datetime]]

    __table_args__ = (
        Index("invitation_base_id_idx", "base_id"),
        Index("invitation_space_id_idx", "space_id"),
    )

class InvitationRecord(Base):
    __tablename__ = "invitation_record"

    id: Mapped[str] = mapped_column(primary_key=True)
    invitation_id: Mapped[str]
    base_id: Mapped[Optional[str]]
    space_id: Mapped[Optional[str]]
    type: Mapped[str]
    inviter: Mapped[str]
    accepter: Mapped[str]
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("invitation_record_invitation_id_idx", "invitation_id"),
        Index("invitation_record_base_id_idx", "base_id"),
        Index("invitation_record_space_id_idx", "space_id"),
    )

class Notification(Base):
    __tablename__ = "notification"

    id: Mapped[str] = mapped_column(primary_key=True)
    from_user_id: Mapped[str]
    to_user_id: Mapped[str]
    type: Mapped[str]
    message: Mapped[str]
    message_i18n: Mapped[Optional[str]]
    severity: Mapped[str] = mapped_column(default="info", server_default=text("'info'"))
    url_path: Mapped[Optional[str]]
    is_read: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[str]

    __table_args__ = (
        Index("notification_to_user_id_is_read_created_time_idx", "to_user_id", "is_read", "created_time"),
        Index("notification_created_time_idx", "created_time"),
    )

class AccessToken(Base):
    __tablename__ = "access_token"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    description: Mapped[Optional[str]]
    user_id: Mapped[str]
    scopes: Mapped[str]
    space_ids: Mapped[Optional[str]]
    base_ids: Mapped[Optional[str]]
    sign: Mapped[str]
    client_id: Mapped[Optional[str]]
    has_full_access: Mapped[Optional[bool]]
    expired_time: Mapped[datetime]
    last_used_time: Mapped[Optional[datetime]]
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_modified_time: Mapped[Optional[datetime]] = mapped_column(default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("access_token_user_id_idx", "user_id"),
        Index("access_token_client_id_idx", "client_id"),
    )

class Setting(Base):
    __tablename__ = "setting"

    name: Mapped[str] = mapped_column()
    content: Mapped[Optional[str]]
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_modified_time: Mapped[Optional[datetime]] = mapped_column(default=utc_now, onupdate=utc_now)
    created_by: Mapped[str]
    last_modified_by: Mapped[Optional[str]]

    __table_args__ = (
        Index("setting_name_key", "name", unique=True),
    )

    __mapper_args__ = {"primary_key": [name]}

class OAuthApp(Base):
    __tablename__ = "oauth_app"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    logo: Mapped[Optional[str]]
    homepage: Mapped[str]
    description: Mapped[Optional[str]]
    client_id: Mapped[str]
    redirect_uris: Mapped[Optional[str]]
    scopes: Mapped[Optional[str]]
    allow_device_flow: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    dynamic_registration: Mapped[bool] = mapped_column(default=False, server_default=text("false"))
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_modified_time: Mapped[Optional[datetime]] = mapped_column(default=utc_now, onupdate=utc_now)
    created_by: Mapped[str]

    __table_args__ = (
        Index("oauth_app_client_id_key", "client_id", unique=True),
    )

    secrets: Mapped[list["OAuthAppSecret"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="o_auth_app")
    authorized: Mapped[list["OAuthAppAuthorized"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="o_auth_app")
    tokens: Mapped[list["OAuthAppToken"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="o_auth_app")

class OAuthAppAuthorized(Base):
    __tablename__ = "oauth_app_authorized"

    id: Mapped[str] = mapped_column(primary_key=True)
    client_id: Mapped[str] = mapped_column(ForeignKey("oauth_app.client_id", ondelete="CASCADE", onupdate="CASCADE", name="oauth_app_authorized_client_id_fkey"))
    user_id: Mapped[str]
    authorized_time: Mapped[datetime]

    __table_args__ = (
        Index("oauth_app_authorized_client_id_user_id_key", "client_id", "user_id", unique=True),
    )

    o_auth_app: Mapped["OAuthApp"] = relationship(back_populates="authorized")

class OAuthAppSecret(Base):
    __tablename__ = "oauth_app_secret"

    id: Mapped[str] = mapped_column(primary_key=True)
    client_id: Mapped[str] = mapped_column(ForeignKey("oauth_app.client_id", ondelete="CASCADE", onupdate="CASCADE", name="oauth_app_secret_client_id_fkey"))
    secret: Mapped[str]
    masked_secret: Mapped[str]
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[str]
    last_used_time: Mapped[Optional[datetime]]

    __table_args__ = (
        Index("oauth_app_secret_secret_key", "secret", unique=True),
    )

    o_auth_app: Mapped["OAuthApp"] = relationship(back_populates="secrets")
    tokens: Mapped[list["OAuthAppToken"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="o_auth_app_secret")

class OAuthAppToken(Base):
    __tablename__ = "oauth_app_token"

    id: Mapped[str] = mapped_column(primary_key=True)
    client_id: Mapped[str] = mapped_column(ForeignKey("oauth_app.client_id", ondelete="CASCADE", onupdate="CASCADE", name="oauth_app_token_client_id_fkey"))
    app_secret_id: Mapped[Optional[str]] = mapped_column(ForeignKey("oauth_app_secret.id", ondelete="CASCADE", onupdate="CASCADE", name="oauth_app_token_app_secret_id_fkey"))
    refresh_token_sign: Mapped[str]
    expired_time: Mapped[datetime]
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[str]

    __table_args__ = (
        Index("oauth_app_token_refresh_token_sign_key", "refresh_token_sign", unique=True),
        Index("oauth_app_token_client_id_idx", "client_id"),
    )

    o_auth_app_secret: Mapped[Optional["OAuthAppSecret"]] = relationship(back_populates="tokens")
    o_auth_app: Mapped["OAuthApp"] = relationship(back_populates="tokens")

class RecordHistory(Base):
    __tablename__ = "record_history"

    id: Mapped[str] = mapped_column(primary_key=True)
    table_id: Mapped[str]
    record_id: Mapped[str]
    field_id: Mapped[str]
    before: Mapped[str]
    after: Mapped[str]
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[str]

    __table_args__ = (
        Index("record_history_table_id_record_id_created_time_idx", "table_id", "record_id", "created_time"),
        Index("record_history_table_id_created_time_idx", "table_id", "created_time"),
    )

class Trash(Base):
    __tablename__ = "trash"

    id: Mapped[str] = mapped_column(primary_key=True)
    resource_type: Mapped[str]
    resource_id: Mapped[str]
    parent_id: Mapped[Optional[str]]
    deleted_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    deleted_by: Mapped[str]

    __table_args__ = (
        Index("trash_resource_type_resource_id_key", "resource_type", "resource_id", unique=True),
    )

class TableTrash(Base):
    __tablename__ = "table_trash"

    id: Mapped[str] = mapped_column(primary_key=True)
    table_id: Mapped[str]
    resource_type: Mapped[str]
    snapshot: Mapped[str]
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[str]

    __table_args__ = (
        Index("table_trash_table_id_idx", "table_id"),
    )

class RecordTrash(Base):
    __tablename__ = "record_trash"

    id: Mapped[str] = mapped_column(primary_key=True)
    table_id: Mapped[str]
    record_id: Mapped[str]
    snapshot: Mapped[str]
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[str]
    reason: Mapped[str] = mapped_column(default="deleted", server_default=text("'deleted'"))
    record_created_time: Mapped[Optional[datetime]]
    record_created_by: Mapped[Optional[str]]
    record_last_modified_time: Mapped[Optional[datetime]]
    record_last_modified_by: Mapped[Optional[str]]
    operation_id: Mapped[Optional[str]]

    __table_args__ = (
        Index("record_trash_table_id_record_id_idx", "table_id", "record_id"),
    )

class RecordRemovalTombstone(Base):
    __tablename__ = "record_removal_tombstone"

    id: Mapped[str] = mapped_column(primary_key=True)
    table_id: Mapped[str]
    record_id: Mapped[str]
    type: Mapped[str]
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("record_removal_tombstone_table_id_record_id_idx", "table_id", "record_id"),
    )

class Plugin(Base):
    __tablename__ = "plugin"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    description: Mapped[Optional[str]]
    detail_desc: Mapped[Optional[str]]
    logo: Mapped[str]
    help_url: Mapped[Optional[str]]
    status: Mapped[str]
    positions: Mapped[str]
    url: Mapped[Optional[str]]
    secret: Mapped[str]
    masked_secret: Mapped[str]
    i18n: Mapped[Optional[str]]
    config: Mapped[Optional[str]]
    plugin_user: Mapped[Optional[str]]
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_modified_time: Mapped[Optional[datetime]] = mapped_column(default=utc_now, onupdate=utc_now)
    created_by: Mapped[str]
    last_modified_by: Mapped[Optional[str]]

    __table_args__ = (
        Index("plugin_secret_key", "secret", unique=True),
    )

    plugin_install: Mapped[list["PluginInstall"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="plugin")

class PluginInstall(Base):
    __tablename__ = "plugin_install"

    id: Mapped[str] = mapped_column(primary_key=True)
    plugin_id: Mapped[str] = mapped_column(ForeignKey("plugin.id", ondelete="CASCADE", onupdate="CASCADE", name="plugin_install_plugin_id_fkey"))
    base_id: Mapped[str]
    name: Mapped[str]
    position_id: Mapped[str]
    position: Mapped[str]
    storage: Mapped[Optional[str]]
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[str]
    last_modified_time: Mapped[Optional[datetime]] = mapped_column(default=utc_now, onupdate=utc_now)
    last_modified_by: Mapped[Optional[str]]

    __table_args__ = (
        Index("plugin_install_position_id_idx", "position_id"),
        Index("plugin_install_base_id_idx", "base_id"),
    )

    plugin: Mapped["Plugin"] = relationship(back_populates="plugin_install")

class Dashboard(Base):
    __tablename__ = "dashboard"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    base_id: Mapped[str]
    layout: Mapped[Optional[str]]
    created_by: Mapped[str]
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_modified_time: Mapped[Optional[datetime]] = mapped_column(default=utc_now, onupdate=utc_now)
    last_modified_by: Mapped[Optional[str]]

    __table_args__ = (
        Index("dashboard_base_id_idx", "base_id"),
    )

class Comment(Base):
    __tablename__ = "comment"

    id: Mapped[str] = mapped_column(primary_key=True)
    table_id: Mapped[str]
    record_id: Mapped[str]
    quote_id: Mapped[Optional[str]] = mapped_column("quote_Id")
    content: Mapped[Optional[str]]
    reaction: Mapped[Optional[str]]
    deleted_time: Mapped[Optional[datetime]]
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[str]
    last_modified_time: Mapped[Optional[datetime]] = mapped_column(default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("comment_table_id_record_id_idx", "table_id", "record_id"),
    )

class CommentSubscription(Base):
    __tablename__ = "comment_subscription"

    id: Mapped[str] = mapped_column(primary_key=True)
    table_id: Mapped[str]
    record_id: Mapped[str]
    created_by: Mapped[str]
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("comment_subscription_table_id_record_id_idx", "table_id", "record_id"),
        Index("comment_subscription_table_id_record_id_key", "table_id", "record_id", unique=True),
    )

class Integration(Base):
    __tablename__ = "integration"

    id: Mapped[str] = mapped_column(primary_key=True)
    resource_id: Mapped[str]
    config: Mapped[str]
    type: Mapped[str]
    enable: Mapped[Optional[bool]]
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_modified_time: Mapped[Optional[datetime]] = mapped_column(default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("integration_resource_id_key", "resource_id", unique=True),
        Index("integration_resource_id_idx", "resource_id"),
    )

class PluginPanel(Base):
    __tablename__ = "plugin_panel"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    table_id: Mapped[str] = mapped_column(ForeignKey("table_meta.id", ondelete="CASCADE", onupdate="CASCADE", name="plugin_panel_table_id_fkey"))
    layout: Mapped[Optional[str]]
    created_by: Mapped[str]
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_modified_time: Mapped[Optional[datetime]] = mapped_column(default=utc_now, onupdate=utc_now)
    last_modified_by: Mapped[Optional[str]]

    table: Mapped["TableMeta"] = relationship(back_populates="plugin_panel")

class PluginContextMenu(Base):
    __tablename__ = "plugin_context_menu"

    id: Mapped[str] = mapped_column(primary_key=True)
    table_id: Mapped[str] = mapped_column(ForeignKey("table_meta.id", ondelete="CASCADE", onupdate="CASCADE", name="plugin_context_menu_table_id_fkey"))
    plugin_install_id: Mapped[str]
    order: Mapped[float]
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[str]
    last_modified_time: Mapped[Optional[datetime]] = mapped_column(default=utc_now, onupdate=utc_now)
    last_modified_by: Mapped[Optional[str]]

    __table_args__ = (
        Index("plugin_context_menu_plugin_install_id_key", "plugin_install_id", unique=True),
    )

    table: Mapped["TableMeta"] = relationship(back_populates="plugin_context_menu")

class UserLastVisit(Base):
    __tablename__ = "user_last_visit"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[str]
    resource_type: Mapped[str]
    resource_id: Mapped[str]
    parent_resource_id: Mapped[str]
    last_visit_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("user_last_visit_user_id_resource_type_parent_resource_id_idx", "user_id", "resource_type", "parent_resource_id"),
        Index("user_last_visit_user_id_resource_type_resource_id_key", "user_id", "resource_type", "resource_id", unique=True),
    )

class UserBaseOrder(Base):
    __tablename__ = "user_base_order"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[str] = mapped_column(ForeignKey("users.id", ondelete="CASCADE", onupdate="CASCADE", name="user_base_order_user_id_fkey"))
    base_id: Mapped[str] = mapped_column(ForeignKey("base.id", ondelete="CASCADE", onupdate="CASCADE", name="user_base_order_base_id_fkey"))
    order: Mapped[float]
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_modified_time: Mapped[Optional[datetime]] = mapped_column(default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("user_base_order_user_id_order_idx", "user_id", "order"),
        Index("user_base_order_base_id_idx", "base_id"),
        Index("user_base_order_user_id_base_id_key", "user_id", "base_id", unique=True),
    )

    user: Mapped["User"] = relationship(back_populates="base_orders")
    base: Mapped["Base_"] = relationship(back_populates="user_orders")

class Template(Base):
    __tablename__ = "template"

    id: Mapped[str] = mapped_column(primary_key=True)
    base_id: Mapped[Optional[str]]
    cover: Mapped[Optional[str]]
    name: Mapped[Optional[str]]
    description: Mapped[Optional[str]]
    markdown_description: Mapped[Optional[str]]
    category_id: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True)
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[str]
    last_modified_time: Mapped[Optional[datetime]] = mapped_column(default=utc_now, onupdate=utc_now)
    last_modified_by: Mapped[Optional[str]]
    is_system: Mapped[Optional[bool]]
    is_published: Mapped[Optional[bool]]
    featured: Mapped[Optional[bool]]
    snapshot: Mapped[Optional[str]]
    order: Mapped[float]
    usage_count: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    publish_info: Mapped[Optional[dict[str, Any]]] = mapped_column(JSONB(none_as_null=True))
    visit_count: Mapped[int] = mapped_column(default=0, server_default=text("0"))
    kind: Mapped[Optional[str]]

    __table_args__ = (
        Index("template_base_id_key", "base_id", unique=True),
    )

class TemplateCategory(Base):
    __tablename__ = "template_category"

    id: Mapped[str] = mapped_column(primary_key=True)
    name: Mapped[str]
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[str]
    last_modified_time: Mapped[Optional[datetime]] = mapped_column(default=utc_now, onupdate=utc_now)
    last_modified_by: Mapped[Optional[str]]
    order: Mapped[float]

    __table_args__ = (
        Index("template_category_name_key", "name", unique=True),
    )

class Task(Base):
    __tablename__ = "task"

    id: Mapped[str] = mapped_column(primary_key=True)
    type: Mapped[str]
    status: Mapped[str]
    snapshot: Mapped[Optional[str]]
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_modified_time: Mapped[Optional[datetime]] = mapped_column(default=utc_now, onupdate=utc_now)
    created_by: Mapped[str]
    last_modified_by: Mapped[Optional[str]]

    __table_args__ = (
        Index("task_type_status_idx", "type", "status"),
    )

    runs: Mapped[list["TaskRun"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="task")

class TaskRun(Base):
    __tablename__ = "task_run"

    id: Mapped[str] = mapped_column(primary_key=True)
    task_id: Mapped[str] = mapped_column(ForeignKey("task.id", ondelete="CASCADE", onupdate="CASCADE", name="task_run_task_id_fkey"))
    base_id: Mapped[str]
    status: Mapped[str]
    snapshot: Mapped[str]
    depends_on_run_ids: Mapped[list[str]] = mapped_column(ARRAY(Text), nullable=True, default=lambda: [], server_default=text("ARRAY[]::text[]"))
    spent: Mapped[Optional[int]]
    log: Mapped[Optional[str]]
    error_msg: Mapped[Optional[str]]
    started_time: Mapped[Optional[datetime]]
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    last_modified_time: Mapped[Optional[datetime]] = mapped_column(default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("task_run_task_id_status_idx", "task_id", "status"),
        Index("task_run_status_base_id_created_time_idx", "status", "base_id", "created_time"),
        Index("task_run_status_last_modified_time_idx", "status", "last_modified_time"),
    )

    task: Mapped["Task"] = relationship(back_populates="runs")

class TaskReference(Base):
    __tablename__ = "task_reference"

    id: Mapped[str] = mapped_column(primary_key=True)
    from_field_id: Mapped[str]
    to_field_id: Mapped[str]
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("task_reference_from_field_id_idx", "from_field_id"),
        Index("task_reference_to_field_id_idx", "to_field_id"),
        Index("task_reference_to_field_id_from_field_id_key", "to_field_id", "from_field_id", unique=True),
    )

class Waitlist(Base):
    __tablename__ = "waitlist"

    email: Mapped[str] = mapped_column()
    invite: Mapped[Optional[bool]]
    invite_time: Mapped[Optional[datetime]]
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))

    __table_args__ = (
        Index("waitlist_email_key", "email", unique=True),
    )

    __mapper_args__ = {"primary_key": [email]}

class BaseNode(Base):
    __tablename__ = "base_node"

    id: Mapped[str] = mapped_column(primary_key=True)
    parent_id: Mapped[Optional[str]] = mapped_column(ForeignKey("base_node.id", ondelete="SET NULL", onupdate="CASCADE", name="base_node_parent_id_fkey"))
    base_id: Mapped[str]
    resource_type: Mapped[str]
    resource_id: Mapped[str]
    order: Mapped[float]
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[str]
    last_modified_time: Mapped[Optional[datetime]] = mapped_column(default=utc_now, onupdate=utc_now)
    last_modified_by: Mapped[Optional[str]]

    __table_args__ = (
        Index("base_node_base_id_resource_type_resource_id_key", "base_id", "resource_type", "resource_id", unique=True),
    )

    parent: Mapped[Optional["BaseNode"]] = relationship(remote_side=[id], back_populates="children")
    children: Mapped[list["BaseNode"]] = relationship(back_populates="parent")

class BaseNodeFolder(Base):
    __tablename__ = "base_node_folder"

    id: Mapped[str] = mapped_column(primary_key=True)
    base_id: Mapped[str]
    name: Mapped[str]
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[str]
    last_modified_time: Mapped[Optional[datetime]] = mapped_column(default=utc_now, onupdate=utc_now)
    last_modified_by: Mapped[Optional[str]]

    __table_args__ = (
        Index("base_node_folder_base_id_name_key", "base_id", "name", unique=True),
    )

class BaseShare(Base):
    __tablename__ = "base_share"

    id: Mapped[str] = mapped_column(primary_key=True)
    base_id: Mapped[str]
    share_id: Mapped[str]
    password: Mapped[Optional[str]]
    node_id: Mapped[Optional[str]]
    allow_save: Mapped[Optional[bool]]
    allow_copy: Mapped[Optional[bool]]
    allow_edit: Mapped[Optional[bool]]
    enabled: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[str]
    last_modified_time: Mapped[Optional[datetime]] = mapped_column(default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("base_share_share_id_key", "share_id", unique=True),
        Index("base_share_base_id_idx", "base_id"),
        Index("base_share_base_id_node_id_key", "base_id", "node_id", unique=True),
    )

class ShortLink(Base):
    __tablename__ = "short_link"

    id: Mapped[str] = mapped_column(primary_key=True)
    code: Mapped[str] = mapped_column(String(32))
    type: Mapped[str] = mapped_column(String(32))
    resource_id: Mapped[str]
    created_time: Mapped[datetime] = mapped_column(default=utc_now, server_default=text("CURRENT_TIMESTAMP"))
    created_by: Mapped[str]
    deleted_time: Mapped[Optional[datetime]]

    __table_args__ = (
        Index("short_link_code_key", "code", unique=True),
        Index("short_link_deleted_time_idx", "deleted_time"),
        Index("short_link_type_resource_id_key", "type", "resource_id", unique=True),
    )
