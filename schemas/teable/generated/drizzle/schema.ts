import {
  bigint,
  boolean,
  doublePrecision,
  foreignKey,
  index,
  integer,
  jsonb,
  pgEnum,
  pgTable,
  primaryKey,
  text,
  timestamp,
  unique,
  uniqueIndex,
  varchar,
} from 'drizzle-orm/pg-core'
import { defineRelations, sql } from 'drizzle-orm'
import cuid from 'cuid'

const utcNow = (() => {
  let now: Date | undefined
  return () => {
    if (now === undefined) {
      now = new Date()
      queueMicrotask(() => {
        now = undefined
      })
    }
    return now
  }
})()

export const provisionStateEnum = pgEnum('ProvisionState', [
  'pending',
  'ready',
  'error',
  'deleting',
])

export const dataDbProviderEnum = pgEnum('DataDbProvider', ['postgres'])

export const dataDbConnectionStatusEnum = pgEnum('DataDbConnectionStatus', [
  'pending',
  'validating',
  'ready',
  'error',
  'migrating',
  'disabled',
])

export const dataDbConnectionHealthStateEnum = pgEnum('DataDbConnectionHealthState', [
  'healthy',
  'read_only',
  'unreachable',
  'degraded',
])

export const spaceDataDbBindingModeEnum = pgEnum('SpaceDataDbBindingMode', ['default', 'byodb'])

export const spaceDataDbBindingStateEnum = pgEnum('SpaceDataDbBindingState', [
  'ready',
  'validating',
  'initializing',
  'migrating',
  'error',
  'disabled',
])

export const spaceDataDbMigrationJobStateEnum = pgEnum('SpaceDataDbMigrationJobState', [
  'pending',
  'waiting_worker',
  'preflight',
  'freezing_writes',
  'copying',
  'validating',
  'switching',
  'succeeded',
  'failed',
  'canceled',
  'rolled_back',
])

export const baseDataDbMoveJobStateEnum = pgEnum('BaseDataDbMoveJobState', [
  'pending',
  'waiting_worker',
  'copying_base_schema',
  'copying_shared_rows',
  'validating',
  'switching',
  'succeeded',
  'failed',
  'cancelled',
])

export const space = pgTable('space', {
  id: text('id')
    .primaryKey()
    .$defaultFn(() => cuid()),
  name: text('name').notNull(),
  avatar: text('avatar'),
  credit: integer('credit'),
  deletedTime: timestamp('deleted_time', { precision: 3 }),
  createdTime: timestamp('created_time', { precision: 3 })
    .notNull()
    .default(sql`CURRENT_TIMESTAMP`)
    .$defaultFn(utcNow),
  createdBy: text('created_by').notNull(),
  lastModifiedBy: text('last_modified_by'),
  lastModifiedTime: timestamp('last_modified_time', { precision: 3 }).$onUpdate(utcNow),
  isTemplate: boolean('is_template'),
})

export const dataDbConnection = pgTable(
  'data_db_connection',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    provider: dataDbProviderEnum('provider').notNull().default('postgres'),
    encryptedUrl: text('encrypted_url').notNull(),
    urlFingerprint: text('url_fingerprint').notNull(),
    displayHost: text('display_host'),
    displayDatabase: text('display_database'),
    internalSchema: text('internal_schema').notNull(),
    status: dataDbConnectionStatusEnum('status').notNull().default('pending'),
    schemaVersion: text('schema_version'),
    capabilities: jsonb('capabilities'),
    lastValidatedAt: timestamp('last_validated_at', { precision: 3 }),
    lastError: text('last_error'),
    healthState: dataDbConnectionHealthStateEnum('health_state').notNull().default('healthy'),
    healthReason: text('health_reason'),
    healthChangedAt: timestamp('health_changed_at', { precision: 3 }),
    lastHealthCheckAt: timestamp('last_health_check_at', { precision: 3 }),
    consecutiveHealthFailures: integer('consecutive_health_failures').notNull().default(0),
    createdBy: text('created_by').notNull(),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    lastModifiedTime: timestamp('last_modified_time', { precision: 3 }).$onUpdate(utcNow),
  },
  (table) => [uniqueIndex('data_db_connection_url_fingerprint_key').on(table.urlFingerprint)],
)

export const spaceDataDbBinding = pgTable(
  'space_data_db_binding',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    spaceId: text('space_id').notNull(),
    dataDbConnectionId: text('data_db_connection_id'),
    mode: spaceDataDbBindingModeEnum('mode').notNull().default('default'),
    state: spaceDataDbBindingStateEnum('state').notNull().default('ready'),
    createdBy: text('created_by').notNull(),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    lastModifiedTime: timestamp('last_modified_time', { precision: 3 }).$onUpdate(utcNow),
    lastHistoryFlushedAt: timestamp('last_history_flushed_at', { precision: 3 }),
    lastRemovalFlushedAt: timestamp('last_removal_flushed_at', { precision: 3 }),
  },
  (table) => [
    uniqueIndex('space_data_db_binding_space_id_key').on(table.spaceId),
    index('space_data_db_binding_data_db_connection_id_idx').on(table.dataDbConnectionId),
    foreignKey({
      name: 'space_data_db_binding_space_id_fkey',
      columns: [table.spaceId],
      foreignColumns: [space.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'space_data_db_binding_data_db_connection_id_fkey',
      columns: [table.dataDbConnectionId],
      foreignColumns: [dataDbConnection.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const spaceDataDbMigrationJob = pgTable(
  'space_data_db_migration_job',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    spaceId: text('space_id').notNull(),
    sourceConnectionId: text('source_connection_id'),
    targetConnectionId: text('target_connection_id'),
    targetMode: text('target_mode').notNull().default('migrate-space'),
    switchOnCompletion: boolean('switch_on_completion').notNull().default(false),
    state: spaceDataDbMigrationJobStateEnum('state').notNull().default('pending'),
    targetUrlFingerprint: text('target_url_fingerprint').notNull(),
    targetInternalSchema: text('target_internal_schema').notNull(),
    inventory: jsonb('inventory'),
    copyStats: jsonb('copy_stats'),
    validationStats: jsonb('validation_stats'),
    lastError: text('last_error'),
    startedAt: timestamp('started_at', { precision: 3 }),
    completedAt: timestamp('completed_at', { precision: 3 }),
    createdBy: text('created_by').notNull(),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    lastModifiedTime: timestamp('last_modified_time', { precision: 3 }).$onUpdate(utcNow),
  },
  (table) => [
    index('space_data_db_migration_job_space_id_state_idx').on(table.spaceId, table.state),
    index('space_data_db_migration_job_source_connection_id_idx').on(table.sourceConnectionId),
    index('space_data_db_migration_job_target_connection_id_idx').on(table.targetConnectionId),
    foreignKey({
      name: 'space_data_db_migration_job_space_id_fkey',
      columns: [table.spaceId],
      foreignColumns: [space.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'space_data_db_migration_job_source_connection_id_fkey',
      columns: [table.sourceConnectionId],
      foreignColumns: [dataDbConnection.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
    foreignKey({
      name: 'space_data_db_migration_job_target_connection_id_fkey',
      columns: [table.targetConnectionId],
      foreignColumns: [dataDbConnection.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const baseDataDbMoveJob = pgTable(
  'base_data_db_move_job',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    baseId: text('base_id').notNull(),
    sourceSpaceId: text('source_space_id').notNull(),
    targetSpaceId: text('target_space_id').notNull(),
    sourceConnectionId: text('source_connection_id'),
    targetConnectionId: text('target_connection_id'),
    state: baseDataDbMoveJobStateEnum('state').notNull().default('pending'),
    inventory: jsonb('inventory'),
    copyStats: jsonb('copy_stats'),
    validationStats: jsonb('validation_stats'),
    lastError: text('last_error'),
    startedAt: timestamp('started_at', { precision: 3 }),
    completedAt: timestamp('completed_at', { precision: 3 }),
    createdBy: text('created_by').notNull(),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    lastModifiedTime: timestamp('last_modified_time', { precision: 3 }).$onUpdate(utcNow),
  },
  (table) => [
    index('base_data_db_move_job_base_id_state_idx').on(table.baseId, table.state),
    index('base_data_db_move_job_state_idx').on(table.state),
    index('base_data_db_move_job_source_space_id_state_idx').on(table.sourceSpaceId, table.state),
    index('base_data_db_move_job_target_space_id_state_idx').on(table.targetSpaceId, table.state),
    foreignKey({
      name: 'base_data_db_move_job_base_id_fkey',
      columns: [table.baseId],
      foreignColumns: [base.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'base_data_db_move_job_source_space_id_fkey',
      columns: [table.sourceSpaceId],
      foreignColumns: [space.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'base_data_db_move_job_target_space_id_fkey',
      columns: [table.targetSpaceId],
      foreignColumns: [space.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const pinResource = pgTable(
  'pin_resource',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    type: text('type').notNull(),
    resourceId: text('resource_id').notNull(),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    createdBy: text('created_by').notNull(),
    order: doublePrecision('order').notNull(),
  },
  (table) => [
    index('pin_resource_order_idx').on(table.order),
    uniqueIndex('pin_resource_created_by_resource_id_key').on(table.createdBy, table.resourceId),
  ],
)

export const base = pgTable(
  'base',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    spaceId: text('space_id').notNull(),
    name: text('name'),
    order: doublePrecision('order').notNull(),
    icon: text('icon'),
    schemaPass: text('schema_pass'),
    v2Enabled: boolean('v2_enabled').notNull().default(false),
    provisionState: provisionStateEnum('provision_state').notNull().default('ready'),
    deletedTime: timestamp('deleted_time', { precision: 3 }),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    createdBy: text('created_by').notNull(),
    lastModifiedBy: text('last_modified_by'),
    lastModifiedTime: timestamp('last_modified_time', { precision: 3 }).$onUpdate(utcNow),
  },
  (table) => [
    index('base_order_idx').on(table.order),
    index('base_space_id_idx').on(table.spaceId),
    foreignKey({ name: 'base_space_id_fkey', columns: [table.spaceId], foreignColumns: [space.id] })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const tableMeta = pgTable(
  'table_meta',
  {
    id: text('id').primaryKey(),
    baseId: text('base_id').notNull(),
    name: text('name').notNull(),
    description: text('description'),
    icon: text('icon'),
    dbTableName: text('db_table_name').notNull(),
    dbViewName: text('db_view_name'),
    searchIndex: jsonb('search_index'),
    searchIndexPolicy: jsonb('search_index_policy'),
    queryIndexPolicy: jsonb('query_index_policy'),
    provisionState: provisionStateEnum('provision_state').notNull().default('ready'),
    version: integer('version').notNull(),
    order: doublePrecision('order').notNull(),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    lastModifiedTime: timestamp('last_modified_time', { precision: 3 }).$onUpdate(utcNow),
    deletedTime: timestamp('deleted_time', { precision: 3 }),
    createdBy: text('created_by').notNull(),
    lastModifiedBy: text('last_modified_by'),
  },
  (table) => [
    index('table_meta_order_idx').on(table.order),
    index('table_meta_db_table_name_idx').on(table.dbTableName),
    index('table_meta_base_id_deleted_time_idx').on(table.baseId, table.deletedTime),
    foreignKey({
      name: 'table_meta_base_id_fkey',
      columns: [table.baseId],
      foreignColumns: [base.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const tableQueryObservationShard = pgTable(
  'table_query_observation_shard',
  {
    spaceId: text('space_id'),
    baseId: text('base_id').notNull(),
    tableId: text('table_id').notNull(),
    queryKind: text('query_kind').notNull(),
    shapeHash: text('shape_hash').notNull(),
    windowStart: timestamp('window_start', { withTimezone: true, precision: 6 }).notNull(),
    writerId: text('writer_id').notNull(),
    windowSizeSeconds: integer('window_size_seconds').notNull(),
    requestCount: integer('request_count').notNull(),
    slowCount: integer('slow_count').notNull(),
    timeoutCount: integer('timeout_count').notNull(),
    dbErrorCount: integer('db_error_count').notNull(),
    totalDurationMs: doublePrecision('total_duration_ms').notNull(),
    maxDurationMs: doublePrecision('max_duration_ms').notNull(),
    totalDbDurationMs: doublePrecision('total_db_duration_ms'),
    maxDbDurationMs: doublePrecision('max_db_duration_ms'),
    shape: jsonb('shape').notNull(),
    sqlDiagnostics: jsonb('sql_diagnostics'),
    createdTime: timestamp('created_time', { withTimezone: true, precision: 6 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    lastModifiedTime: timestamp('last_modified_time', { withTimezone: true, precision: 6 }),
  },
  (table) => [
    primaryKey({
      name: 'table_query_observation_shard_pkey',
      columns: [table.tableId, table.queryKind, table.shapeHash, table.windowStart, table.writerId],
    }),
    index('table_query_observation_shard_window_start_idx').on(table.windowStart),
    index('table_query_observation_shard_table_start_idx').on(table.tableId, table.windowStart),
    index('table_query_observation_shard_search_activity_idx').on(
      table.queryKind,
      table.tableId,
      table.windowStart,
    ),
    index('table_query_observation_shard_base_start_idx').on(table.baseId, table.windowStart),
  ],
)

export const schemaOperation = pgTable(
  'schema_operation',
  {
    id: text('id').primaryKey(),
    type: text('type').notNull(),
    status: text('status').notNull(),
    phase: text('phase').notNull(),
    resourceType: text('resource_type').notNull(),
    resourceId: text('resource_id').notNull(),
    baseId: text('base_id'),
    tableId: text('table_id'),
    idempotencyKey: text('idempotency_key').notNull(),
    payload: jsonb('payload'),
    result: jsonb('result'),
    attempts: integer('attempts').notNull().default(0),
    maxAttempts: integer('max_attempts').notNull().default(8),
    nextRunAt: timestamp('next_run_at', { withTimezone: true, precision: 6 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    lockedAt: timestamp('locked_at', { withTimezone: true, precision: 6 }),
    lockedBy: text('locked_by'),
    lastError: text('last_error'),
    createdTime: timestamp('created_time', { withTimezone: true, precision: 6 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    createdBy: text('created_by').notNull(),
    lastModifiedTime: timestamp('last_modified_time', {
      withTimezone: true,
      precision: 6,
    }).$onUpdate(utcNow),
    lastModifiedBy: text('last_modified_by'),
  },
  (table) => [
    uniqueIndex('schema_operation_idempotency_key_key').on(table.idempotencyKey),
    index('schema_operation_status_next_run_at_idx').on(table.status, table.nextRunAt),
    index('schema_operation_resource_status_idx').on(
      table.resourceType,
      table.resourceId,
      table.status,
    ),
    index('schema_operation_base_status_idx').on(table.baseId, table.status),
    index('schema_operation_table_status_idx').on(table.tableId, table.status),
  ],
)

export const field = pgTable(
  'field',
  {
    id: text('id').primaryKey(),
    name: text('name').notNull(),
    description: text('description'),
    options: text('options'),
    meta: text('meta'),
    aiConfig: text('ai_config'),
    type: text('type').notNull(),
    cellValueType: text('cell_value_type').notNull(),
    isMultipleCellValue: boolean('is_multiple_cell_value'),
    dbFieldType: text('db_field_type').notNull(),
    dbFieldName: text('db_field_name').notNull(),
    provisionState: provisionStateEnum('provision_state').notNull().default('ready'),
    notNull: boolean('not_null'),
    unique: boolean('unique'),
    isPrimary: boolean('is_primary'),
    isComputed: boolean('is_computed'),
    isLookup: boolean('is_lookup'),
    isConditionalLookup: boolean('is_conditional_lookup'),
    isPending: boolean('is_pending'),
    hasError: boolean('has_error'),
    lookupLinkedFieldId: text('lookup_linked_field_id'),
    lookupOptions: text('lookup_options'),
    tableId: text('table_id').notNull(),
    order: doublePrecision('order').notNull(),
    version: integer('version').notNull(),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    lastModifiedTime: timestamp('last_modified_time', { precision: 3 }).$onUpdate(utcNow),
    deletedTime: timestamp('deleted_time', { precision: 3 }),
    createdBy: text('created_by').notNull(),
    lastModifiedBy: text('last_modified_by'),
  },
  (table) => [
    index('field_lookup_linked_field_id_idx').on(table.lookupLinkedFieldId),
    index('field_table_id_deleted_time_idx').on(table.tableId, table.deletedTime),
    foreignKey({
      name: 'field_table_id_fkey',
      columns: [table.tableId],
      foreignColumns: [tableMeta.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const computedUpdateOutbox = pgTable(
  'computed_update_outbox',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    baseId: text('base_id').notNull(),
    workerLane: text('worker_lane').notNull().default('production'),
    seedTableId: text('seed_table_id').notNull(),
    seedRecordIds: jsonb('seed_record_ids'),
    changeType: text('change_type').notNull(),
    steps: jsonb('steps'),
    edges: jsonb('edges'),
    status: text('status').notNull(),
    attempts: integer('attempts').notNull().default(0),
    maxAttempts: integer('max_attempts').notNull().default(8),
    nextRunAt: timestamp('next_run_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    lockedAt: timestamp('locked_at', { precision: 3 }),
    lockedBy: text('locked_by'),
    lastError: text('last_error'),
    estimatedComplexity: integer('estimated_complexity').notNull().default(0),
    planHash: text('plan_hash').notNull(),
    dirtyStats: jsonb('dirty_stats'),
    runId: text('run_id').notNull(),
    originRunIds: text('origin_run_ids').array().default([]),
    runTotalSteps: integer('run_total_steps').notNull().default(0),
    runCompletedStepsBefore: integer('run_completed_steps_before').notNull().default(0),
    affectedTableIds: text('affected_table_ids').array().default([]),
    affectedFieldIds: text('affected_field_ids').array().default([]),
    syncMaxLevel: integer('sync_max_level'),
    sourceChangedAt: timestamp('source_changed_at', { precision: 3 }),
    stageDepth: integer('stage_depth').notNull().default(0),
    predecessorTaskId: text('predecessor_task_id'),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull().$onUpdate(utcNow),
  },
  (table) => [
    index('computed_update_outbox_status_next_run_at_idx').on(table.status, table.nextRunAt),
    index('computed_update_outbox_worker_lane_status_next_run_at_idx').on(
      table.workerLane,
      table.status,
      table.nextRunAt,
    ),
    index('computed_update_outbox_base_id_seed_table_id_idx').on(table.baseId, table.seedTableId),
    index('computed_update_outbox_plan_hash_idx').on(table.planHash),
    index('computed_update_outbox_run_id_idx').on(table.runId),
  ],
)

export const computedUpdateOutboxSeed = pgTable(
  'computed_update_outbox_seed',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    taskId: text('task_id').notNull(),
    tableId: text('table_id').notNull(),
    recordId: text('record_id').notNull(),
  },
  (table) => [
    index('computed_update_outbox_seed_task_id_idx').on(table.taskId),
    uniqueIndex('computed_update_outbox_seed_task_id_table_id_record_id_key').on(
      table.taskId,
      table.tableId,
      table.recordId,
    ),
    foreignKey({
      name: 'computed_update_outbox_seed_task_id_fkey',
      columns: [table.taskId],
      foreignColumns: [computedUpdateOutbox.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const computedUpdateChangeFrontier = pgTable(
  'computed_update_change_frontier',
  {
    scopeId: text('scope_id').notNull(),
    kind: text('kind').notNull(),
    tableId: text('table_id').notNull(),
    recordId: text('record_id').notNull(),
    fieldId: text('field_id').notNull(),
  },
  (table) => [
    primaryKey({
      name: 'computed_update_change_frontier_pkey',
      columns: [table.scopeId, table.kind, table.tableId, table.recordId, table.fieldId],
    }),
  ],
)

export const computedUpdateStageLedger = pgTable(
  'computed_update_stage_ledger',
  {
    scopeId: text('scope_id').notNull(),
    kind: text('kind').notNull(),
    tableId: text('table_id').notNull(),
    recordId: text('record_id').notNull(),
    seq: bigint('seq', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
  },
  (table) => [
    primaryKey({
      name: 'computed_update_stage_ledger_pkey',
      columns: [table.scopeId, table.kind, table.tableId, table.recordId],
    }),
    index('computed_update_stage_ledger_scope_id_kind_seq_idx').on(
      table.scopeId,
      table.kind,
      table.seq,
    ),
  ],
)

export const computedUpdateDeadLetter = pgTable(
  'computed_update_dead_letter',
  {
    id: text('id').primaryKey(),
    baseId: text('base_id').notNull(),
    workerLane: text('worker_lane').notNull().default('production'),
    seedTableId: text('seed_table_id').notNull(),
    seedRecordIds: jsonb('seed_record_ids'),
    changeType: text('change_type').notNull(),
    steps: jsonb('steps'),
    edges: jsonb('edges'),
    status: text('status').notNull(),
    attempts: integer('attempts').notNull().default(0),
    maxAttempts: integer('max_attempts').notNull().default(8),
    nextRunAt: timestamp('next_run_at', { precision: 3 }).notNull(),
    lockedAt: timestamp('locked_at', { precision: 3 }),
    lockedBy: text('locked_by'),
    lastError: text('last_error'),
    estimatedComplexity: integer('estimated_complexity').notNull().default(0),
    planHash: text('plan_hash').notNull(),
    dirtyStats: jsonb('dirty_stats'),
    runId: text('run_id').notNull(),
    originRunIds: text('origin_run_ids').array().default([]),
    runTotalSteps: integer('run_total_steps').notNull().default(0),
    runCompletedStepsBefore: integer('run_completed_steps_before').notNull().default(0),
    affectedTableIds: text('affected_table_ids').array().default([]),
    affectedFieldIds: text('affected_field_ids').array().default([]),
    syncMaxLevel: integer('sync_max_level'),
    sourceChangedAt: timestamp('source_changed_at', { precision: 3 }),
    stageDepth: integer('stage_depth').notNull().default(0),
    predecessorTaskId: text('predecessor_task_id'),
    traceData: jsonb('trace_data'),
    failedAt: timestamp('failed_at', { precision: 3 }).notNull(),
    createdAt: timestamp('created_at', { precision: 3 }).notNull(),
    updatedAt: timestamp('updated_at', { precision: 3 }).notNull(),
  },
  (table) => [
    index('computed_update_dead_letter_base_id_seed_table_id_idx').on(
      table.baseId,
      table.seedTableId,
    ),
    index('computed_update_dead_letter_plan_hash_idx').on(table.planHash),
    index('computed_update_dead_letter_run_id_idx').on(table.runId),
  ],
)

export const computedUpdateDeadLetterSeed = pgTable(
  'computed_update_dead_letter_seed',
  {
    taskId: text('task_id').notNull(),
    tableId: text('table_id').notNull(),
    recordId: text('record_id').notNull(),
  },
  (table) => [
    primaryKey({
      name: 'computed_update_dead_letter_seed_pkey',
      columns: [table.taskId, table.tableId, table.recordId],
    }),
    foreignKey({
      name: 'computed_update_dead_letter_seed_task_id_fkey',
      columns: [table.taskId],
      foreignColumns: [computedUpdateDeadLetter.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const computedUpdateRunHistory = pgTable(
  'computed_update_run_history',
  {
    taskId: text('task_id').primaryKey(),
    baseId: text('base_id').notNull(),
    seedTableId: text('seed_table_id').notNull(),
    changeType: text('change_type').notNull(),
    runId: text('run_id').notNull(),
    originRunIds: text('origin_run_ids').array().default([]),
    steps: jsonb('steps'),
    edges: jsonb('edges'),
    affectedTableIds: text('affected_table_ids').array().default([]),
    affectedFieldIds: text('affected_field_ids').array().default([]),
    sourceFieldIds: text('source_field_ids').array().default([]),
    seedRecordCount: integer('seed_record_count').notNull().default(0),
    stageDepth: integer('stage_depth').notNull().default(0),
    predecessorTaskId: text('predecessor_task_id'),
    runTotalSteps: integer('run_total_steps').notNull().default(0),
    runCompletedStepsBefore: integer('run_completed_steps_before').notNull().default(0),
    syncMaxLevel: integer('sync_max_level'),
    estimatedComplexity: integer('estimated_complexity').notNull().default(0),
    attempts: integer('attempts').notNull().default(0),
    outcome: text('outcome').notNull(),
    sourceChangedAt: timestamp('source_changed_at', { precision: 3 }),
    enqueuedAt: timestamp('enqueued_at', { precision: 3 }).notNull(),
    startedAt: timestamp('started_at', { precision: 3 }),
    completedAt: timestamp('completed_at', { precision: 3 }).notNull(),
    durationMs: integer('duration_ms').notNull().default(0),
  },
  (table) => [
    index('computed_update_run_history_run_id_idx').on(table.runId),
    index('computed_update_run_history_base_id_completed_at_idx').on(
      table.baseId,
      table.completedAt,
    ),
    index('computed_update_run_history_completed_at_idx').on(table.completedAt),
    index('computed_update_run_history_origin_run_ids_gin').on(table.originRunIds),
  ],
)

export const computedUpdatePauseScope = pgTable(
  'computed_update_pause_scope',
  {
    id: text('id').primaryKey(),
    scopeType: text('scope_type').notNull(),
    scopeId: text('scope_id').notNull(),
    pausedAt: timestamp('paused_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    pausedBy: text('paused_by'),
    resumeAt: timestamp('resume_at', { precision: 3 }),
    reason: text('reason'),
    writePolicy: text('write_policy').notNull().default('allow_bounded'),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow)
      .$onUpdate(utcNow),
    updatedBy: text('updated_by'),
  },
  (table) => [
    index('computed_update_pause_scope_resume_at_idx').on(table.resumeAt),
    uniqueIndex('computed_update_pause_scope_scope_type_scope_id_key').on(
      table.scopeType,
      table.scopeId,
    ),
  ],
)

export const computedFieldActivity = pgTable(
  'computed_field_activity',
  {
    fieldId: text('field_id').primaryKey(),
    tableId: text('table_id').notNull(),
    baseId: text('base_id').notNull(),
    status: text('status').notNull(),
    activeTaskCount: integer('active_task_count').notNull().default(0),
    processingTaskCount: integer('processing_task_count').notNull().default(0),
    generation: bigint('generation', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    estimatedComplexity: bigint('estimated_complexity', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    estimatedDirtyRecords: bigint('estimated_dirty_records', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    hasAllTargetRecords: boolean('has_all_target_records').notNull().default(false),
    queuedAt: timestamp('queued_at', { precision: 3 }),
    startedAt: timestamp('started_at', { precision: 3 }),
    lastCompletedAt: timestamp('last_completed_at', { precision: 3 }),
    lastDurationMs: integer('last_duration_ms'),
    lastError: jsonb('last_error'),
    extensions: jsonb('extensions'),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('computed_field_activity_table_id_status_idx').on(table.tableId, table.status),
    index('computed_field_activity_base_id_status_idx').on(table.baseId, table.status),
  ],
)

export const computedTableActivity = pgTable(
  'computed_table_activity',
  {
    tableId: text('table_id').primaryKey(),
    baseId: text('base_id').notNull(),
    status: text('status').notNull(),
    calculatingFieldCount: integer('calculating_field_count').notNull().default(0),
    queuedFieldCount: integer('queued_field_count').notNull().default(0),
    estimatedComplexity: bigint('estimated_complexity', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    recentCompletions: jsonb('recent_completions').notNull().default([]),
    generation: bigint('generation', { mode: 'bigint' })
      .notNull()
      .default(sql`0`),
    updatedAt: timestamp('updated_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [index('computed_table_activity_base_id_status_idx').on(table.baseId, table.status)],
)

export const computedTaskFieldRef = pgTable(
  'computed_task_field_ref',
  {
    taskId: text('task_id').notNull(),
    fieldId: text('field_id').notNull(),
    tableId: text('table_id').notNull(),
    baseId: text('base_id').notNull(),
    wasProcessing: boolean('was_processing').notNull().default(false),
    createdAt: timestamp('created_at', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    primaryKey({ name: 'computed_task_field_ref_pkey', columns: [table.taskId, table.fieldId] }),
    index('computed_task_field_ref_field_id_idx').on(table.fieldId),
    index('computed_task_field_ref_table_id_idx').on(table.tableId),
  ],
)

export const view = pgTable(
  'view',
  {
    id: text('id').primaryKey(),
    name: text('name').notNull(),
    description: text('description'),
    tableId: text('table_id').notNull(),
    type: text('type').notNull(),
    sort: text('sort'),
    filter: text('filter'),
    group: text('group'),
    options: text('options'),
    order: doublePrecision('order').notNull(),
    version: integer('version').notNull(),
    columnMeta: text('column_meta').notNull(),
    isLocked: boolean('is_locked'),
    enableShare: boolean('enable_share'),
    shareId: text('share_id'),
    shareMeta: text('share_meta'),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    lastModifiedTime: timestamp('last_modified_time', { precision: 3 }).$onUpdate(utcNow),
    deletedTime: timestamp('deleted_time', { precision: 3 }),
    createdBy: text('created_by').notNull(),
    lastModifiedBy: text('last_modified_by'),
  },
  (table) => [
    uniqueIndex('view_share_id_key').on(table.shareId),
    index('view_order_idx').on(table.order),
    index('view_table_id_deleted_time_idx').on(table.tableId, table.deletedTime),
    foreignKey({
      name: 'view_table_id_fkey',
      columns: [table.tableId],
      foreignColumns: [tableMeta.id],
    })
      .onDelete('restrict')
      .onUpdate('cascade'),
  ],
)

export const ops = pgTable(
  'ops',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    collection: text('collection').notNull(),
    docId: text('doc_id').notNull(),
    docType: text('doc_type').notNull(),
    version: integer('version').notNull(),
    operation: text('operation').notNull(),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    createdBy: text('created_by').notNull(),
  },
  (table) => [
    index('ops_collection_created_time_idx').on(table.collection, table.createdTime),
    uniqueIndex('ops_collection_doc_id_version_key').on(
      table.collection,
      table.docId,
      table.version,
    ),
  ],
)

export const reference = pgTable(
  'reference',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    fromFieldId: text('from_field_id').notNull(),
    toFieldId: text('to_field_id').notNull(),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('reference_from_field_id_idx').on(table.fromFieldId),
    index('reference_to_field_id_idx').on(table.toFieldId),
    uniqueIndex('reference_to_field_id_from_field_id_key').on(table.toFieldId, table.fromFieldId),
  ],
)

export const users = pgTable(
  'users',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    name: text('name').notNull(),
    password: text('password'),
    salt: text('salt'),
    phone: text('phone'),
    email: text('email').notNull(),
    avatar: text('avatar'),
    isSystem: boolean('is_system'),
    isAdmin: boolean('is_admin'),
    isTrialUsed: boolean('is_trial_used'),
    lang: text('lang'),
    notifyMeta: text('notify_meta'),
    lastSignTime: timestamp('last_sign_time', { precision: 3 }),
    deactivatedTime: timestamp('deactivated_time', { precision: 3 }),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    deletedTime: timestamp('deleted_time', { precision: 3 }),
    lastModifiedTime: timestamp('last_modified_time', { precision: 3 }).$onUpdate(utcNow),
    permanentDeletedTime: timestamp('permanent_deleted_time', { precision: 3 }),
    refMeta: text('ref_meta'),
  },
  (table) => [
    uniqueIndex('users_phone_key').on(table.phone),
    uniqueIndex('users_email_key').on(table.email),
  ],
)

export const account = pgTable(
  'account',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    userId: text('user_id').notNull(),
    type: text('type').notNull(),
    provider: text('provider').notNull(),
    providerId: text('provider_id').notNull(),
    connectionId: text('connection_id').notNull().default(''),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    uniqueIndex('account_provider_provider_id_connection_id_key').on(
      table.provider,
      table.providerId,
      table.connectionId,
    ),
    foreignKey({
      name: 'account_user_id_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const attachments = pgTable(
  'attachments',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    token: text('token').notNull(),
    hash: text('hash').notNull(),
    size: bigint('size', { mode: 'bigint' }).notNull(),
    mimetype: text('mimetype').notNull(),
    path: text('path').notNull(),
    width: integer('width'),
    height: integer('height'),
    deletedTime: timestamp('deleted_time', { precision: 3 }),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    createdBy: text('created_by').notNull(),
    lastModifiedBy: text('last_modified_by'),
    thumbnailPath: text('thumbnail_path'),
  },
  (table) => [uniqueIndex('attachments_token_key').on(table.token)],
)

export const attachmentsTable = pgTable(
  'attachments_table',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    attachmentId: text('attachment_id').notNull(),
    name: text('name').notNull(),
    token: text('token').notNull(),
    tableId: text('table_id').notNull(),
    recordId: text('record_id').notNull(),
    fieldId: text('field_id').notNull(),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    createdBy: text('created_by').notNull(),
    lastModifiedBy: text('last_modified_by'),
    lastModifiedTime: timestamp('last_modified_time', { precision: 3 }).$onUpdate(utcNow),
  },
  (table) => [
    index('attachments_table_table_id_record_id_idx').on(table.tableId, table.recordId),
    index('attachments_table_table_id_field_id_idx').on(table.tableId, table.fieldId),
    index('attachments_table_attachment_id_idx').on(table.attachmentId),
  ],
)

export const collaborator = pgTable(
  'collaborator',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    roleName: text('role_name').notNull(),
    resourceType: text('resource_type').notNull(),
    resourceId: text('resource_id').notNull(),
    principalId: text('principal_id').notNull(),
    principalType: text('principal_type').notNull(),
    createdBy: text('created_by').notNull(),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    lastModifiedTime: timestamp('last_modified_time', { precision: 3 }).$onUpdate(utcNow),
    lastModifiedBy: text('last_modified_by'),
  },
  (table) => [
    index('collaborator_resource_id_idx').on(table.resourceId),
    index('collaborator_principal_id_idx').on(table.principalId),
    uniqueIndex('collaborator_resource_type_resource_id_principal_id_princip_key').on(
      table.resourceType,
      table.resourceId,
      table.principalId,
      table.principalType,
    ),
  ],
)

export const invitation = pgTable(
  'invitation',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    baseId: text('base_id'),
    spaceId: text('space_id'),
    type: text('type').notNull(),
    role: text('role').notNull(),
    invitationCode: text('invitation_code').notNull(),
    expiredTime: timestamp('expired_time', { precision: 3 }),
    createdBy: text('create_by').notNull(),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    lastModifiedTime: timestamp('last_modified_time', { precision: 3 }).$onUpdate(utcNow),
    lastModifiedBy: text('last_modified_by'),
    deletedTime: timestamp('deleted_time', { precision: 3 }),
  },
  (table) => [
    index('invitation_base_id_idx').on(table.baseId),
    index('invitation_space_id_idx').on(table.spaceId),
  ],
)

export const invitationRecord = pgTable(
  'invitation_record',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    invitationId: text('invitation_id').notNull(),
    baseId: text('base_id'),
    spaceId: text('space_id'),
    type: text('type').notNull(),
    inviter: text('inviter').notNull(),
    accepter: text('accepter').notNull(),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('invitation_record_invitation_id_idx').on(table.invitationId),
    index('invitation_record_base_id_idx').on(table.baseId),
    index('invitation_record_space_id_idx').on(table.spaceId),
  ],
)

export const notification = pgTable(
  'notification',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    fromUserId: text('from_user_id').notNull(),
    toUserId: text('to_user_id').notNull(),
    type: text('type').notNull(),
    message: text('message').notNull(),
    messageI18n: text('message_i18n'),
    severity: text('severity').notNull().default('info'),
    urlPath: text('url_path'),
    isRead: boolean('is_read').notNull().default(false),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    createdBy: text('created_by').notNull(),
  },
  (table) => [
    index('notification_to_user_id_is_read_created_time_idx').on(
      table.toUserId,
      table.isRead,
      table.createdTime,
    ),
    index('notification_created_time_idx').on(table.createdTime),
  ],
)

export const accessToken = pgTable(
  'access_token',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    name: text('name').notNull(),
    description: text('description'),
    userId: text('user_id').notNull(),
    scopes: text('scopes').notNull(),
    spaceIds: text('space_ids'),
    baseIds: text('base_ids'),
    sign: text('sign').notNull(),
    clientId: text('client_id'),
    hasFullAccess: boolean('has_full_access'),
    expiredTime: timestamp('expired_time', { precision: 3 }).notNull(),
    lastUsedTime: timestamp('last_used_time', { precision: 3 }),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    lastModifiedTime: timestamp('last_modified_time', { precision: 3 }).$onUpdate(utcNow),
  },
  (table) => [
    index('access_token_user_id_idx').on(table.userId),
    index('access_token_client_id_idx').on(table.clientId),
  ],
)

export const setting = pgTable(
  'setting',
  {
    name: text('name').notNull(),
    content: text('content'),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    lastModifiedTime: timestamp('last_modified_time', { precision: 3 }).$onUpdate(utcNow),
    createdBy: text('created_by').notNull(),
    lastModifiedBy: text('last_modified_by'),
  },
  (table) => [uniqueIndex('setting_name_key').on(table.name)],
)

export const oauthApp = pgTable(
  'oauth_app',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    name: text('name').notNull(),
    logo: text('logo'),
    homepage: text('homepage').notNull(),
    description: text('description'),
    clientId: text('client_id').notNull(),
    redirectUris: text('redirect_uris'),
    scopes: text('scopes'),
    allowDeviceFlow: boolean('allow_device_flow').notNull().default(false),
    dynamicRegistration: boolean('dynamic_registration').notNull().default(false),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    lastModifiedTime: timestamp('last_modified_time', { precision: 3 }).$onUpdate(utcNow),
    createdBy: text('created_by').notNull(),
  },
  (table) => [unique('oauth_app_client_id_key').on(table.clientId)],
)

export const oauthAppAuthorized = pgTable(
  'oauth_app_authorized',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    clientId: text('client_id').notNull(),
    userId: text('user_id').notNull(),
    authorizedTime: timestamp('authorized_time', { precision: 3 }).notNull(),
  },
  (table) => [
    uniqueIndex('oauth_app_authorized_client_id_user_id_key').on(table.clientId, table.userId),
    foreignKey({
      name: 'oauth_app_authorized_client_id_fkey',
      columns: [table.clientId],
      foreignColumns: [oauthApp.clientId],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const oauthAppSecret = pgTable(
  'oauth_app_secret',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    clientId: text('client_id').notNull(),
    secret: text('secret').notNull(),
    maskedSecret: text('masked_secret').notNull(),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    createdBy: text('created_by').notNull(),
    lastUsedTime: timestamp('last_used_time', { precision: 3 }),
  },
  (table) => [
    uniqueIndex('oauth_app_secret_secret_key').on(table.secret),
    foreignKey({
      name: 'oauth_app_secret_client_id_fkey',
      columns: [table.clientId],
      foreignColumns: [oauthApp.clientId],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const oauthAppToken = pgTable(
  'oauth_app_token',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    clientId: text('client_id').notNull(),
    appSecretId: text('app_secret_id'),
    refreshTokenSign: text('refresh_token_sign').notNull(),
    expiredTime: timestamp('expired_time', { precision: 3 }).notNull(),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    createdBy: text('created_by').notNull(),
  },
  (table) => [
    uniqueIndex('oauth_app_token_refresh_token_sign_key').on(table.refreshTokenSign),
    index('oauth_app_token_client_id_idx').on(table.clientId),
    foreignKey({
      name: 'oauth_app_token_app_secret_id_fkey',
      columns: [table.appSecretId],
      foreignColumns: [oauthAppSecret.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'oauth_app_token_client_id_fkey',
      columns: [table.clientId],
      foreignColumns: [oauthApp.clientId],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const recordHistory = pgTable(
  'record_history',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    tableId: text('table_id').notNull(),
    recordId: text('record_id').notNull(),
    fieldId: text('field_id').notNull(),
    before: text('before').notNull(),
    after: text('after').notNull(),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    createdBy: text('created_by').notNull(),
  },
  (table) => [
    index('record_history_table_id_record_id_created_time_idx').on(
      table.tableId,
      table.recordId,
      table.createdTime,
    ),
    index('record_history_table_id_created_time_idx').on(table.tableId, table.createdTime),
  ],
)

export const trash = pgTable(
  'trash',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    resourceType: text('resource_type').notNull(),
    resourceId: text('resource_id').notNull(),
    parentId: text('parent_id'),
    deletedTime: timestamp('deleted_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    deletedBy: text('deleted_by').notNull(),
  },
  (table) => [
    uniqueIndex('trash_resource_type_resource_id_key').on(table.resourceType, table.resourceId),
  ],
)

export const tableTrash = pgTable(
  'table_trash',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    tableId: text('table_id').notNull(),
    resourceType: text('resource_type').notNull(),
    snapshot: text('snapshot').notNull(),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    createdBy: text('created_by').notNull(),
  },
  (table) => [index('table_trash_table_id_idx').on(table.tableId)],
)

export const recordTrash = pgTable(
  'record_trash',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    tableId: text('table_id').notNull(),
    recordId: text('record_id').notNull(),
    snapshot: text('snapshot').notNull(),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    createdBy: text('created_by').notNull(),
    reason: text('reason').notNull().default('deleted'),
    recordCreatedTime: timestamp('record_created_time', { precision: 3 }),
    recordCreatedBy: text('record_created_by'),
    recordLastModifiedTime: timestamp('record_last_modified_time', { precision: 3 }),
    recordLastModifiedBy: text('record_last_modified_by'),
    operationId: text('operation_id'),
  },
  (table) => [index('record_trash_table_id_record_id_idx').on(table.tableId, table.recordId)],
)

export const recordRemovalTombstone = pgTable(
  'record_removal_tombstone',
  {
    id: text('id').primaryKey(),
    tableId: text('table_id').notNull(),
    recordId: text('record_id').notNull(),
    type: text('type').notNull(),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('record_removal_tombstone_table_id_record_id_idx').on(table.tableId, table.recordId),
  ],
)

export const plugin = pgTable(
  'plugin',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    name: text('name').notNull(),
    description: text('description'),
    detailDesc: text('detail_desc'),
    logo: text('logo').notNull(),
    helpUrl: text('help_url'),
    status: text('status').notNull(),
    positions: text('positions').notNull(),
    url: text('url'),
    secret: text('secret').notNull(),
    maskedSecret: text('masked_secret').notNull(),
    i18n: text('i18n'),
    config: text('config'),
    pluginUser: text('plugin_user'),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    lastModifiedTime: timestamp('last_modified_time', { precision: 3 }).$onUpdate(utcNow),
    createdBy: text('created_by').notNull(),
    lastModifiedBy: text('last_modified_by'),
  },
  (table) => [uniqueIndex('plugin_secret_key').on(table.secret)],
)

export const pluginInstall = pgTable(
  'plugin_install',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    pluginId: text('plugin_id').notNull(),
    baseId: text('base_id').notNull(),
    name: text('name').notNull(),
    positionId: text('position_id').notNull(),
    position: text('position').notNull(),
    storage: text('storage'),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    createdBy: text('created_by').notNull(),
    lastModifiedTime: timestamp('last_modified_time', { precision: 3 }).$onUpdate(utcNow),
    lastModifiedBy: text('last_modified_by'),
  },
  (table) => [
    index('plugin_install_position_id_idx').on(table.positionId),
    index('plugin_install_base_id_idx').on(table.baseId),
    foreignKey({
      name: 'plugin_install_plugin_id_fkey',
      columns: [table.pluginId],
      foreignColumns: [plugin.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const dashboard = pgTable(
  'dashboard',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    name: text('name').notNull(),
    baseId: text('base_id').notNull(),
    layout: text('layout'),
    createdBy: text('created_by').notNull(),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    lastModifiedTime: timestamp('last_modified_time', { precision: 3 }).$onUpdate(utcNow),
    lastModifiedBy: text('last_modified_by'),
    deletedTime: timestamp('deleted_time', { precision: 3 }),
  },
  (table) => [index('dashboard_base_id_idx').on(table.baseId)],
)

export const comment = pgTable(
  'comment',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    tableId: text('table_id').notNull(),
    recordId: text('record_id').notNull(),
    quoteId: text('quote_Id'),
    content: text('content'),
    reaction: text('reaction'),
    deletedTime: timestamp('deleted_time', { precision: 3 }),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    createdBy: text('created_by').notNull(),
    lastModifiedTime: timestamp('last_modified_time', { precision: 3 }).$onUpdate(utcNow),
  },
  (table) => [index('comment_table_id_record_id_idx').on(table.tableId, table.recordId)],
)

export const commentSubscription = pgTable(
  'comment_subscription',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    tableId: text('table_id').notNull(),
    recordId: text('record_id').notNull(),
    createdBy: text('created_by').notNull(),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('comment_subscription_table_id_record_id_idx').on(table.tableId, table.recordId),
    uniqueIndex('comment_subscription_table_id_record_id_key').on(table.tableId, table.recordId),
  ],
)

export const integration = pgTable(
  'integration',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    resourceId: text('resource_id').notNull(),
    config: text('config').notNull(),
    type: text('type').notNull(),
    enable: boolean('enable'),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    lastModifiedTime: timestamp('last_modified_time', { precision: 3 }).$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('integration_resource_id_key').on(table.resourceId),
    index('integration_resource_id_idx').on(table.resourceId),
  ],
)

export const pluginPanel = pgTable(
  'plugin_panel',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    name: text('name').notNull(),
    tableId: text('table_id').notNull(),
    layout: text('layout'),
    createdBy: text('created_by').notNull(),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    lastModifiedTime: timestamp('last_modified_time', { precision: 3 }).$onUpdate(utcNow),
    lastModifiedBy: text('last_modified_by'),
  },
  (table) => [
    foreignKey({
      name: 'plugin_panel_table_id_fkey',
      columns: [table.tableId],
      foreignColumns: [tableMeta.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const pluginContextMenu = pgTable(
  'plugin_context_menu',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    tableId: text('table_id').notNull(),
    pluginInstallId: text('plugin_install_id').notNull(),
    order: doublePrecision('order').notNull(),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    createdBy: text('created_by').notNull(),
    lastModifiedTime: timestamp('last_modified_time', { precision: 3 }).$onUpdate(utcNow),
    lastModifiedBy: text('last_modified_by'),
  },
  (table) => [
    uniqueIndex('plugin_context_menu_plugin_install_id_key').on(table.pluginInstallId),
    foreignKey({
      name: 'plugin_context_menu_table_id_fkey',
      columns: [table.tableId],
      foreignColumns: [tableMeta.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const userLastVisit = pgTable(
  'user_last_visit',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    userId: text('user_id').notNull(),
    resourceType: text('resource_type').notNull(),
    resourceId: text('resource_id').notNull(),
    parentResourceId: text('parent_resource_id').notNull(),
    lastVisitTime: timestamp('last_visit_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('user_last_visit_user_id_resource_type_parent_resource_id_idx').on(
      table.userId,
      table.resourceType,
      table.parentResourceId,
    ),
    uniqueIndex('user_last_visit_user_id_resource_type_resource_id_key').on(
      table.userId,
      table.resourceType,
      table.resourceId,
    ),
  ],
)

export const userBaseOrder = pgTable(
  'user_base_order',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    userId: text('user_id').notNull(),
    baseId: text('base_id').notNull(),
    order: doublePrecision('order').notNull(),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    lastModifiedTime: timestamp('last_modified_time', { precision: 3 }).$onUpdate(utcNow),
  },
  (table) => [
    index('user_base_order_user_id_order_idx').on(table.userId, table.order),
    index('user_base_order_base_id_idx').on(table.baseId),
    uniqueIndex('user_base_order_user_id_base_id_key').on(table.userId, table.baseId),
    foreignKey({
      name: 'user_base_order_user_id_fkey',
      columns: [table.userId],
      foreignColumns: [users.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
    foreignKey({
      name: 'user_base_order_base_id_fkey',
      columns: [table.baseId],
      foreignColumns: [base.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const template = pgTable(
  'template',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    baseId: text('base_id'),
    cover: text('cover'),
    name: text('name'),
    description: text('description'),
    markdownDescription: text('markdown_description'),
    categoryId: text('category_id').array(),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    createdBy: text('created_by').notNull(),
    lastModifiedTime: timestamp('last_modified_time', { precision: 3 }).$onUpdate(utcNow),
    lastModifiedBy: text('last_modified_by'),
    isSystem: boolean('is_system'),
    isPublished: boolean('is_published'),
    featured: boolean('featured'),
    snapshot: text('snapshot'),
    order: doublePrecision('order').notNull(),
    usageCount: integer('usage_count').notNull().default(0),
    publishInfo: jsonb('publish_info'),
    visitCount: integer('visit_count').notNull().default(0),
    kind: text('kind'),
    version: integer('version').notNull().default(1),
    manifest: jsonb('manifest'),
    review: jsonb('review'),
    history: jsonb('history'),
    parentTemplateId: text('parent_template_id'),
    slug: text('slug'),
  },
  (table) => [
    uniqueIndex('template_slug_key').on(table.slug),
    uniqueIndex('template_base_id_key').on(table.baseId),
  ],
)

export const templateCategory = pgTable(
  'template_category',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    name: text('name').notNull(),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    createdBy: text('created_by').notNull(),
    lastModifiedTime: timestamp('last_modified_time', { precision: 3 }).$onUpdate(utcNow),
    lastModifiedBy: text('last_modified_by'),
    order: doublePrecision('order').notNull(),
    parentId: text('parent_id'),
    kind: text('kind').notNull().default('industry'),
    key: text('key'),
  },
  (table) => [
    uniqueIndex('template_category_name_key').on(table.name),
    uniqueIndex('template_category_key_key').on(table.key),
    index('template_category_parent_id_idx').on(table.parentId),
  ],
)

export const task = pgTable(
  'task',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    type: text('type').notNull(),
    status: text('status').notNull(),
    snapshot: text('snapshot'),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    lastModifiedTime: timestamp('last_modified_time', { precision: 3 }).$onUpdate(utcNow),
    createdBy: text('created_by').notNull(),
    lastModifiedBy: text('last_modified_by'),
  },
  (table) => [index('task_type_status_idx').on(table.type, table.status)],
)

export const taskRun = pgTable(
  'task_run',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    taskId: text('task_id').notNull(),
    baseId: text('base_id').notNull(),
    status: text('status').notNull(),
    snapshot: text('snapshot').notNull(),
    dependsOnRunIds: text('depends_on_run_ids').array().default([]),
    spent: integer('spent'),
    log: text('log'),
    errorMsg: text('error_msg'),
    startedTime: timestamp('started_time', { precision: 3 }),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    lastModifiedTime: timestamp('last_modified_time', { precision: 3 }).$onUpdate(utcNow),
  },
  (table) => [
    index('task_run_task_id_status_idx').on(table.taskId, table.status),
    index('task_run_status_base_id_created_time_idx').on(
      table.status,
      table.baseId,
      table.createdTime,
    ),
    index('task_run_status_last_modified_time_idx').on(table.status, table.lastModifiedTime),
    foreignKey({
      name: 'task_run_task_id_fkey',
      columns: [table.taskId],
      foreignColumns: [task.id],
    })
      .onDelete('cascade')
      .onUpdate('cascade'),
  ],
)

export const taskReference = pgTable(
  'task_reference',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    fromFieldId: text('from_field_id').notNull(),
    toFieldId: text('to_field_id').notNull(),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [
    index('task_reference_from_field_id_idx').on(table.fromFieldId),
    index('task_reference_to_field_id_idx').on(table.toFieldId),
    uniqueIndex('task_reference_to_field_id_from_field_id_key').on(
      table.toFieldId,
      table.fromFieldId,
    ),
  ],
)

export const waitlist = pgTable(
  'waitlist',
  {
    email: text('email').notNull(),
    invite: boolean('invite'),
    inviteTime: timestamp('invite_time', { precision: 3 }),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
  },
  (table) => [uniqueIndex('waitlist_email_key').on(table.email)],
)

export const baseNode = pgTable(
  'base_node',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    parentId: text('parent_id'),
    baseId: text('base_id').notNull(),
    resourceType: text('resource_type').notNull(),
    resourceId: text('resource_id').notNull(),
    order: doublePrecision('order').notNull(),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    createdBy: text('created_by').notNull(),
    lastModifiedTime: timestamp('last_modified_time', { precision: 3 }).$onUpdate(utcNow),
    lastModifiedBy: text('last_modified_by'),
  },
  (table) => [
    uniqueIndex('base_node_base_id_resource_type_resource_id_key').on(
      table.baseId,
      table.resourceType,
      table.resourceId,
    ),
    foreignKey({
      name: 'base_node_parent_id_fkey',
      columns: [table.parentId],
      foreignColumns: [table.id],
    })
      .onDelete('set null')
      .onUpdate('cascade'),
  ],
)

export const baseNodeFolder = pgTable(
  'base_node_folder',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    baseId: text('base_id').notNull(),
    name: text('name').notNull(),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    createdBy: text('created_by').notNull(),
    lastModifiedTime: timestamp('last_modified_time', { precision: 3 }).$onUpdate(utcNow),
    lastModifiedBy: text('last_modified_by'),
    deletedTime: timestamp('deleted_time', { precision: 3 }),
  },
  (table) => [index('base_node_folder_base_id_idx').on(table.baseId)],
)

export const baseShare = pgTable(
  'base_share',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    baseId: text('base_id').notNull(),
    shareId: text('share_id').notNull(),
    password: text('password'),
    nodeId: text('node_id'),
    allowSave: boolean('allow_save'),
    allowCopy: boolean('allow_copy'),
    allowEdit: boolean('allow_edit'),
    enabled: boolean('enabled').notNull().default(true),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    createdBy: text('created_by').notNull(),
    lastModifiedTime: timestamp('last_modified_time', { precision: 3 }).$onUpdate(utcNow),
  },
  (table) => [
    uniqueIndex('base_share_share_id_key').on(table.shareId),
    index('base_share_base_id_idx').on(table.baseId),
    uniqueIndex('base_share_base_id_node_id_key').on(table.baseId, table.nodeId),
  ],
)

export const shortLink = pgTable(
  'short_link',
  {
    id: text('id')
      .primaryKey()
      .$defaultFn(() => cuid()),
    code: varchar('code', { length: 32 }).notNull(),
    type: varchar('type', { length: 32 }).notNull(),
    resourceId: text('resource_id').notNull(),
    createdTime: timestamp('created_time', { precision: 3 })
      .notNull()
      .default(sql`CURRENT_TIMESTAMP`)
      .$defaultFn(utcNow),
    createdBy: text('created_by').notNull(),
    deletedTime: timestamp('deleted_time', { precision: 3 }),
  },
  (table) => [
    uniqueIndex('short_link_code_key').on(table.code),
    index('short_link_deleted_time_idx').on(table.deletedTime),
    uniqueIndex('short_link_type_resource_id_key').on(table.type, table.resourceId),
  ],
)

export const relations = defineRelations(
  {
    space,
    dataDbConnection,
    spaceDataDbBinding,
    spaceDataDbMigrationJob,
    baseDataDbMoveJob,
    pinResource,
    base,
    tableMeta,
    tableQueryObservationShard,
    schemaOperation,
    field,
    computedUpdateOutbox,
    computedUpdateOutboxSeed,
    computedUpdateChangeFrontier,
    computedUpdateStageLedger,
    computedUpdateDeadLetter,
    computedUpdateDeadLetterSeed,
    computedUpdateRunHistory,
    computedUpdatePauseScope,
    computedFieldActivity,
    computedTableActivity,
    computedTaskFieldRef,
    view,
    ops,
    reference,
    users,
    account,
    attachments,
    attachmentsTable,
    collaborator,
    invitation,
    invitationRecord,
    notification,
    accessToken,
    setting,
    oauthApp,
    oauthAppAuthorized,
    oauthAppSecret,
    oauthAppToken,
    recordHistory,
    trash,
    tableTrash,
    recordTrash,
    recordRemovalTombstone,
    plugin,
    pluginInstall,
    dashboard,
    comment,
    commentSubscription,
    integration,
    pluginPanel,
    pluginContextMenu,
    userLastVisit,
    userBaseOrder,
    template,
    templateCategory,
    task,
    taskRun,
    taskReference,
    waitlist,
    baseNode,
    baseNodeFolder,
    baseShare,
    shortLink,
  },
  (r) => ({
    space: {
      baseGroup: r.many.base(),
      dataDbBinding: r.one.spaceDataDbBinding({
        from: r.space.id,
        to: r.spaceDataDbBinding.spaceId,
      }),
      dataDbMigrationJobs: r.many.spaceDataDbMigrationJob(),
      baseDataDbMoveJobsAsSource: r.many.baseDataDbMoveJob({ alias: 'BaseDataDbMoveSourceSpace' }),
      baseDataDbMoveJobsAsTarget: r.many.baseDataDbMoveJob({ alias: 'BaseDataDbMoveTargetSpace' }),
    },
    dataDbConnection: {
      spaceBindings: r.many.spaceDataDbBinding(),
      sourceMigrationJobs: r.many.spaceDataDbMigrationJob({
        alias: 'SpaceDataDbMigrationSourceConnection',
      }),
      targetMigrationJobs: r.many.spaceDataDbMigrationJob({
        alias: 'SpaceDataDbMigrationTargetConnection',
      }),
    },
    spaceDataDbBinding: {
      space: r.one.space({ from: r.spaceDataDbBinding.spaceId, to: r.space.id, optional: false }),
      dataDbConnection: r.one.dataDbConnection({
        from: r.spaceDataDbBinding.dataDbConnectionId,
        to: r.dataDbConnection.id,
      }),
    },
    spaceDataDbMigrationJob: {
      space: r.one.space({
        from: r.spaceDataDbMigrationJob.spaceId,
        to: r.space.id,
        optional: false,
      }),
      sourceConnection: r.one.dataDbConnection({
        from: r.spaceDataDbMigrationJob.sourceConnectionId,
        to: r.dataDbConnection.id,
        alias: 'SpaceDataDbMigrationSourceConnection',
      }),
      targetConnection: r.one.dataDbConnection({
        from: r.spaceDataDbMigrationJob.targetConnectionId,
        to: r.dataDbConnection.id,
        alias: 'SpaceDataDbMigrationTargetConnection',
      }),
    },
    baseDataDbMoveJob: {
      base: r.one.base({ from: r.baseDataDbMoveJob.baseId, to: r.base.id, optional: false }),
      sourceSpace: r.one.space({
        from: r.baseDataDbMoveJob.sourceSpaceId,
        to: r.space.id,
        optional: false,
        alias: 'BaseDataDbMoveSourceSpace',
      }),
      targetSpace: r.one.space({
        from: r.baseDataDbMoveJob.targetSpaceId,
        to: r.space.id,
        optional: false,
        alias: 'BaseDataDbMoveTargetSpace',
      }),
    },
    base: {
      space: r.one.space({ from: r.base.spaceId, to: r.space.id, optional: false }),
      tables: r.many.tableMeta(),
      dataDbMoveJobs: r.many.baseDataDbMoveJob(),
      userOrders: r.many.userBaseOrder(),
    },
    tableMeta: {
      base: r.one.base({ from: r.tableMeta.baseId, to: r.base.id, optional: false }),
      fields: r.many.field(),
      views: r.many.view(),
      pluginPanel: r.many.pluginPanel(),
      pluginContextMenu: r.many.pluginContextMenu(),
    },
    field: {
      table: r.one.tableMeta({ from: r.field.tableId, to: r.tableMeta.id, optional: false }),
    },
    computedUpdateOutbox: { seeds: r.many.computedUpdateOutboxSeed() },
    computedUpdateOutboxSeed: {
      task: r.one.computedUpdateOutbox({
        from: r.computedUpdateOutboxSeed.taskId,
        to: r.computedUpdateOutbox.id,
        optional: false,
      }),
    },
    computedUpdateDeadLetter: { seeds: r.many.computedUpdateDeadLetterSeed() },
    computedUpdateDeadLetterSeed: {
      task: r.one.computedUpdateDeadLetter({
        from: r.computedUpdateDeadLetterSeed.taskId,
        to: r.computedUpdateDeadLetter.id,
        optional: false,
      }),
    },
    view: { table: r.one.tableMeta({ from: r.view.tableId, to: r.tableMeta.id, optional: false }) },
    users: { accounts: r.many.account(), baseOrders: r.many.userBaseOrder() },
    account: { user: r.one.users({ from: r.account.userId, to: r.users.id, optional: false }) },
    oauthApp: {
      secrets: r.many.oauthAppSecret(),
      authorized: r.many.oauthAppAuthorized(),
      tokens: r.many.oauthAppToken(),
    },
    oauthAppAuthorized: {
      oAuthApp: r.one.oauthApp({
        from: r.oauthAppAuthorized.clientId,
        to: r.oauthApp.clientId,
        optional: false,
      }),
    },
    oauthAppSecret: {
      tokens: r.many.oauthAppToken(),
      oAuthApp: r.one.oauthApp({
        from: r.oauthAppSecret.clientId,
        to: r.oauthApp.clientId,
        optional: false,
      }),
    },
    oauthAppToken: {
      oAuthAppSecret: r.one.oauthAppSecret({
        from: r.oauthAppToken.appSecretId,
        to: r.oauthAppSecret.id,
      }),
      oAuthApp: r.one.oauthApp({
        from: r.oauthAppToken.clientId,
        to: r.oauthApp.clientId,
        optional: false,
      }),
    },
    plugin: { pluginInstall: r.many.pluginInstall() },
    pluginInstall: {
      plugin: r.one.plugin({ from: r.pluginInstall.pluginId, to: r.plugin.id, optional: false }),
    },
    pluginPanel: {
      table: r.one.tableMeta({ from: r.pluginPanel.tableId, to: r.tableMeta.id, optional: false }),
    },
    pluginContextMenu: {
      table: r.one.tableMeta({
        from: r.pluginContextMenu.tableId,
        to: r.tableMeta.id,
        optional: false,
      }),
    },
    userBaseOrder: {
      user: r.one.users({ from: r.userBaseOrder.userId, to: r.users.id, optional: false }),
      base: r.one.base({ from: r.userBaseOrder.baseId, to: r.base.id, optional: false }),
    },
    task: { runs: r.many.taskRun() },
    taskRun: { task: r.one.task({ from: r.taskRun.taskId, to: r.task.id, optional: false }) },
    baseNode: {
      parent: r.one.baseNode({
        from: r.baseNode.parentId,
        to: r.baseNode.id,
        alias: 'NodeToChildren',
      }),
      children: r.many.baseNode({ alias: 'NodeToChildren' }),
    },
  }),
)
