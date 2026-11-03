package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object TaskEventTable : IdTable<String>("\"TaskEvent\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val message = text("message")
    val traceId = text("traceId")
    val spanId = text("spanId")
    val parentId = text("parentId").nullable()
    val tracestate = text("tracestate").nullable()
    val isError = bool("isError").default(false)
    val isPartial = bool("isPartial").default(false)
    val isCancelled = bool("isCancelled").default(false)
    val isDebug = bool("isDebug").default(false)
    val serviceName = text("serviceName")
    val serviceNamespace = text("serviceNamespace")
    val level = pgEnum("level", "\"TaskEventLevel\"", TaskEventLevel.entries, TaskEventLevel::dbName).default(TaskEventLevel.TRACE)
    val kind = pgEnum("kind", "\"TaskEventKind\"", TaskEventKind.entries, TaskEventKind::dbName).default(TaskEventKind.INTERNAL)
    val status = pgEnum("status", "\"TaskEventStatus\"", TaskEventStatus.entries, TaskEventStatus::dbName).default(TaskEventStatus.UNSET)
    val links = jsonb<String>("links", { it }, { it }).nullable()
    val events = jsonb<String>("events", { it }, { it }).nullable()
    val startTime = long("startTime")
    val duration = long("duration").default(0L)
    val attemptId = text("attemptId").nullable()
    val attemptNumber = integer("attemptNumber").nullable()
    val environmentId = text("environmentId")
    val environmentType =
        pgEnum("environmentType", "\"RuntimeEnvironmentType\"", RuntimeEnvironmentType.entries, RuntimeEnvironmentType::dbName)
    val organizationId = text("organizationId")
    val projectId = text("projectId")
    val projectRef = text("projectRef")
    val runId = text("runId")
    val runIsTest = bool("runIsTest").default(false)
    val idempotencyKey = text("idempotencyKey").nullable()
    val taskSlug = text("taskSlug")
    val taskPath = text("taskPath").nullable()
    val taskExportName = text("taskExportName").nullable()
    val workerId = text("workerId").nullable()
    val workerVersion = text("workerVersion").nullable()
    val queueId = text("queueId").nullable()
    val queueName = text("queueName").nullable()
    val batchId = text("batchId").nullable()
    val properties = jsonb<String>("properties", { it }, { it })
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable()
    val style = jsonb<String>("style", { it }, { it }).nullable()
    val output = jsonb<String>("output", { it }, { it }).nullable()
    val outputType = text("outputType").nullable()
    val payload = jsonb<String>("payload", { it }, { it }).nullable()
    val payloadType = text("payloadType").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val usageDurationMs = integer("usageDurationMs").default(0)
    val usageCostInCents = double("usageCostInCents").default(0.0)
    val machinePreset = text("machinePreset").nullable()
    val machinePresetCpu = double("machinePresetCpu").nullable()
    val machinePresetMemory = double("machinePresetMemory").nullable()
    val machinePresetCentsPerMs = double("machinePresetCentsPerMs").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"TaskEvent_pkey\"")

    init {
        index("\"TaskEvent_traceId_idx\"", false, traceId)
        index("\"TaskEvent_spanId_idx\"", false, spanId)
        index("\"TaskEvent_runId_idx\"", false, runId)
    }
}
