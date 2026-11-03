package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object BatchTaskRunTable : IdTable<String>("\"BatchTaskRun\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val friendlyId = text("friendlyId").uniqueIndex("\"BatchTaskRun_friendlyId_key\"")
    val idempotencyKey = text("idempotencyKey").nullable()
    val idempotencyKeyExpiresAt = pgTimestamp("idempotencyKeyExpiresAt", 3).nullable()
    val status =
        pgEnum("status", "\"BatchTaskRunStatus\"", BatchTaskRunStatus.entries, BatchTaskRunStatus::dbName).default(
            BatchTaskRunStatus.PENDING,
        )
    val runtimeEnvironmentId = text("runtimeEnvironmentId")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val runIds = array("runIds", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val runCount = integer("runCount").default(0)
    val payload = text("payload").nullable()
    val payloadType = text("payloadType").default("application/json")
    val options1 = jsonb<String>("options", { it }, { it }).nullable()
    val batchVersion = text("batchVersion").default("v1")
    val sealed = bool("sealed").default(false)
    val sealedAt = pgTimestamp("sealedAt", 3).nullable()
    val expectedCount = integer("expectedCount").default(0)
    val completedCount = integer("completedCount").default(0)
    val completedAt = pgTimestamp("completedAt", 3).nullable()
    val resumedAt = pgTimestamp("resumedAt", 3).nullable()
    val processingJobsCount = integer("processingJobsCount").default(0)
    val processingJobsExpectedCount = integer("processingJobsExpectedCount").default(0)
    val oneTimeUseToken = text("oneTimeUseToken").nullable().uniqueIndex("\"BatchTaskRun_oneTimeUseToken_key\"")
    val processingStartedAt = pgTimestamp("processingStartedAt", 3).nullable()
    val processingCompletedAt = pgTimestamp("processingCompletedAt", 3).nullable()
    val successfulRunCount = integer("successfulRunCount").nullable()
    val failedRunCount = integer("failedRunCount").nullable()
    val taskIdentifier = text("taskIdentifier").nullable()
    val checkpointEventId =
        optReference(
            "checkpointEventId",
            CheckpointRestoreEventTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"BatchTaskRun_checkpointEventId_fkey\"",
        ).uniqueIndex("\"BatchTaskRun_checkpointEventId_key\"")
    val dependentTaskAttemptId =
        optReference(
            "dependentTaskAttemptId",
            TaskRunAttemptTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"BatchTaskRun_dependentTaskAttemptId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"BatchTaskRun_pkey\"")

    init {
        index("\"BatchTaskRun_dependentTaskAttemptId_idx\"", false, dependentTaskAttemptId)
        index(
            "\"BatchTaskRun_runtimeEnvironmentId_id_idx\"",
            false,
            functions = listOf(indexExpression("\"runtimeEnvironmentId\""), indexExpression("\"id\" DESC")),
        )
        index(
            "\"BatchTaskRun_runtimeEnvironmentId_createdAt_id_idx\"",
            false,
            functions =
                listOf(indexExpression("\"runtimeEnvironmentId\""), indexExpression("\"createdAt\" DESC"), indexExpression("\"id\" DESC")),
        )
        uniqueIndex("\"BatchTaskRun_runtimeEnvironmentId_idempotencyKey_key\"", runtimeEnvironmentId, idempotencyKey)
    }
}
