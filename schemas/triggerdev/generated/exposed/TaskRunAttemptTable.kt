package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object TaskRunAttemptTable : IdTable<String>("\"TaskRunAttempt\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val number = integer("number").default(0)
    val friendlyId = text("friendlyId").uniqueIndex("\"TaskRunAttempt_friendlyId_key\"")
    val taskRunId =
        reference(
            "taskRunId",
            TaskRunTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TaskRunAttempt_taskRunId_fkey\"",
        )
    val backgroundWorkerId =
        reference(
            "backgroundWorkerId",
            BackgroundWorkerTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TaskRunAttempt_backgroundWorkerId_fkey\"",
        )
    val backgroundWorkerTaskId =
        reference(
            "backgroundWorkerTaskId",
            BackgroundWorkerTaskTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TaskRunAttempt_backgroundWorkerTaskId_fkey\"",
        )
    val runtimeEnvironmentId =
        reference(
            "runtimeEnvironmentId",
            RuntimeEnvironmentTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TaskRunAttempt_runtimeEnvironmentId_fkey\"",
        )
    val queueId =
        reference(
            "queueId",
            TaskQueueTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TaskRunAttempt_queueId_fkey\"",
        )
    val status =
        pgEnum("status", "\"TaskRunAttemptStatus\"", TaskRunAttemptStatus.entries, TaskRunAttemptStatus::dbName).default(
            TaskRunAttemptStatus.PENDING,
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val startedAt = pgTimestamp("startedAt", 3).nullable()
    val completedAt = pgTimestamp("completedAt", 3).nullable()
    val usageDurationMs = integer("usageDurationMs").default(0)
    val error = jsonb<String>("error", { it }, { it }).nullable()
    val output = text("output").nullable()
    val outputType = text("outputType").default("application/json")
    override val primaryKey = PrimaryKey(id, name = "\"TaskRunAttempt_pkey\"")

    init {
        index("\"TaskRunAttempt_taskRunId_idx\"", false, taskRunId)
        uniqueIndex("\"TaskRunAttempt_taskRunId_number_key\"", taskRunId, number)
    }
}
