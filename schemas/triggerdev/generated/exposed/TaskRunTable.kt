package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object TaskRunTable : IdTable<String>("\"TaskRun\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val number = integer("number").default(0)
    val friendlyId = text("friendlyId").uniqueIndex("\"TaskRun_friendlyId_key\"")
    val engine = pgEnum("engine", "\"RunEngineVersion\"", RunEngineVersion.entries, RunEngineVersion::dbName).default(RunEngineVersion.V1)
    val status = pgEnum("status", "\"TaskRunStatus\"", TaskRunStatus.entries, TaskRunStatus::dbName).default(TaskRunStatus.PENDING)
    val statusReason = text("statusReason").nullable()
    val idempotencyKey = text("idempotencyKey").nullable()
    val idempotencyKeyExpiresAt = pgTimestamp("idempotencyKeyExpiresAt", 3).nullable()
    val idempotencyKeyOptions = jsonb<String>("idempotencyKeyOptions", { it }, { it }).nullable()
    val debounce = jsonb<String>("debounce", { it }, { it }).nullable()
    val taskIdentifier = text("taskIdentifier")
    val isTest = bool("isTest").default(false)
    val payload = text("payload")
    val payloadType = text("payloadType").default("application/json")
    val context = jsonb<String>("context", { it }, { it }).nullable()
    val traceContext = jsonb<String>("traceContext", { it }, { it }).nullable()
    val traceId = text("traceId")
    val spanId = text("spanId")
    val runtimeEnvironmentId =
        reference(
            "runtimeEnvironmentId",
            RuntimeEnvironmentTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TaskRun_runtimeEnvironmentId_fkey\"",
        )
    val environmentType =
        pgEnum("environmentType", "\"RuntimeEnvironmentType\"", RuntimeEnvironmentType.entries, RuntimeEnvironmentType::dbName).nullable()
    val projectId =
        reference(
            "projectId",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TaskRun_projectId_fkey\"",
        )
    val organizationId = text("organizationId").nullable()
    val queue = text("queue")
    val lockedQueueId = text("lockedQueueId").nullable()
    val workerQueue = text("masterQueue").default("main")
    val region = text("region").nullable()
    val secondaryMasterQueue = text("secondaryMasterQueue").nullable()
    val attemptNumber = integer("attemptNumber").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val runTags = array("runTags", TextColumnType()).nullable()
    val taskVersion = text("taskVersion").nullable()
    val sdkVersion = text("sdkVersion").nullable()
    val cliVersion = text("cliVersion").nullable()
    val startedAt = pgTimestamp("startedAt", 3).nullable()
    val executedAt = pgTimestamp("executedAt", 3).nullable()
    val completedAt = pgTimestamp("completedAt", 3).nullable()
    val machinePreset = text("machinePreset").nullable()
    val usageDurationMs = integer("usageDurationMs").default(0)
    val costInCents = double("costInCents").default(0.0)
    val baseCostInCents = double("baseCostInCents").default(0.0)
    val lockedAt = pgTimestamp("lockedAt", 3).nullable()
    val lockedById =
        optReference(
            "lockedById",
            BackgroundWorkerTaskTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TaskRun_lockedById_fkey\"",
        )
    val lockedToVersionId =
        optReference(
            "lockedToVersionId",
            BackgroundWorkerTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TaskRun_lockedToVersionId_fkey\"",
        )
    val priorityMs = integer("priorityMs").default(0)
    val concurrencyKey = text("concurrencyKey").nullable()
    val gates = jsonb<String>("gates", { it }, { it }).nullable()
    val delayUntil = pgTimestamp("delayUntil", 3).nullable()
    val queuedAt = pgTimestamp("queuedAt", 3).nullable()
    val ttl = text("ttl").nullable()
    val expiredAt = pgTimestamp("expiredAt", 3).nullable()
    val maxAttempts = integer("maxAttempts").nullable()
    val lockedRetryConfig = jsonb<String>("lockedRetryConfig", { it }, { it }).nullable()
    val oneTimeUseToken = text("oneTimeUseToken").nullable().uniqueIndex("\"TaskRun_oneTimeUseToken_key\"")
    val taskEventStore = text("taskEventStore").default("taskEvent")
    val queueTimestamp = pgTimestamp("queueTimestamp", 3).nullable()
    val scheduleInstanceId = text("scheduleInstanceId").nullable()
    val scheduleId = text("scheduleId").nullable()
    val bulkActionGroupIds = array("bulkActionGroupIds", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val logsDeletedAt = pgTimestamp("logsDeletedAt", 3).nullable()
    val replayedFromTaskRunFriendlyId = text("replayedFromTaskRunFriendlyId").nullable()
    val rootTaskRunId =
        optReference(
            "rootTaskRunId",
            TaskRunTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.NO_ACTION,
            fkName = "\"TaskRun_rootTaskRunId_fkey\"",
        )
    val parentTaskRunId =
        optReference(
            "parentTaskRunId",
            TaskRunTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.NO_ACTION,
            fkName = "\"TaskRun_parentTaskRunId_fkey\"",
        )
    val parentTaskRunAttemptId =
        optReference(
            "parentTaskRunAttemptId",
            TaskRunAttemptTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.NO_ACTION,
            fkName = "\"TaskRun_parentTaskRunAttemptId_fkey\"",
        )
    val batchId =
        optReference(
            "batchId",
            BatchTaskRunTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.NO_ACTION,
            fkName = "\"TaskRun_batchId_fkey\"",
        )
    val resumeParentOnCompletion = bool("resumeParentOnCompletion").default(false)
    val depth = integer("depth").default(0)
    val parentSpanId = text("parentSpanId").nullable()
    val runChainState = jsonb<String>("runChainState", { it }, { it }).nullable()
    val seedMetadata = text("seedMetadata").nullable()
    val seedMetadataType = text("seedMetadataType").default("application/json")
    val metadata = text("metadata").nullable()
    val metadataType = text("metadataType").default("application/json")
    val metadataVersion = integer("metadataVersion").default(1)
    val annotations = jsonb<String>("annotations", { it }, { it }).nullable()
    val isWarmStart = bool("isWarmStart").nullable()
    val output = text("output").nullable()
    val outputType = text("outputType").default("application/json")
    val error = jsonb<String>("error", { it }, { it }).nullable()
    val planType = text("planType").nullable()
    val maxDurationInSeconds = integer("maxDurationInSeconds").nullable()
    val realtimeStreamsVersion = text("realtimeStreamsVersion").default("v1")
    val realtimeStreams = array("realtimeStreams", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val streamBasinName = text("streamBasinName").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"TaskRun_pkey\"")

    init {
        index("\"TaskRun_parentTaskRunId_idx\"", false, parentTaskRunId)
        index("\"TaskRun_spanId_idx\"", false, spanId)
        index("\"TaskRun_parentSpanId_idx\"", false, parentSpanId)
        index("\"TaskRun_runTags_idx\"", false, functions = listOf(indexExpression("\"runTags\" array_ops")), indexType = "gin")
        index("\"TaskRun_runtimeEnvironmentId_batchId_idx\"", false, runtimeEnvironmentId, batchId)
        index(
            "\"TaskRun_runtimeEnvironmentId_createdAt_idx\"",
            false,
            functions = listOf(indexExpression("\"runtimeEnvironmentId\""), indexExpression("\"createdAt\" DESC")),
        )
        index("\"TaskRun_createdAt_idx\"", false, createdAt, indexType = "brin")
        uniqueIndex("TaskRun_runtimeEnvironmentId_taskIdentifier_idempotencyKey_key", runtimeEnvironmentId, taskIdentifier, idempotencyKey)
    }
}
