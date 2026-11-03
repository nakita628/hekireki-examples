package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object BatchTaskRunItemTable : IdTable<String>("\"BatchTaskRunItem\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val status =
        pgEnum("status", "\"BatchTaskRunItemStatus\"", BatchTaskRunItemStatus.entries, BatchTaskRunItemStatus::dbName).default(
            BatchTaskRunItemStatus.PENDING,
        )
    val batchTaskRunId =
        reference(
            "batchTaskRunId",
            BatchTaskRunTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"BatchTaskRunItem_batchTaskRunId_fkey\"",
        )
    val taskRunId =
        reference(
            "taskRunId",
            TaskRunTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"BatchTaskRunItem_taskRunId_fkey\"",
        )
    val taskRunAttemptId =
        optReference(
            "taskRunAttemptId",
            TaskRunAttemptTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"BatchTaskRunItem_taskRunAttemptId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val completedAt = pgTimestamp("completedAt", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"BatchTaskRunItem_pkey\"")

    init {
        index("idx_batchtaskrunitem_taskrunattempt", false, taskRunAttemptId)
        index("idx_batchtaskrunitem_taskrun", false, taskRunId)
        uniqueIndex("\"BatchTaskRunItem_batchTaskRunId_taskRunId_key\"", batchTaskRunId, taskRunId)
    }
}
