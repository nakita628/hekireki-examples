package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object TaskRunDependencyTable : IdTable<String>("\"TaskRunDependency\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val taskRunId =
        reference(
            "taskRunId",
            TaskRunTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TaskRunDependency_taskRunId_fkey\"",
        ).uniqueIndex("\"TaskRunDependency_taskRunId_key\"")
    val checkpointEventId =
        optReference(
            "checkpointEventId",
            CheckpointRestoreEventTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TaskRunDependency_checkpointEventId_fkey\"",
        ).uniqueIndex("\"TaskRunDependency_checkpointEventId_key\"")
    val dependentAttemptId =
        optReference(
            "dependentAttemptId",
            TaskRunAttemptTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TaskRunDependency_dependentAttemptId_fkey\"",
        )
    val dependentBatchRunId =
        optReference(
            "dependentBatchRunId",
            BatchTaskRunTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TaskRunDependency_dependentBatchRunId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val resumedAt = pgTimestamp("resumedAt", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"TaskRunDependency_pkey\"")

    init {
        index("\"TaskRunDependency_dependentAttemptId_idx\"", false, dependentAttemptId)
        index("\"TaskRunDependency_dependentBatchRunId_idx\"", false, dependentBatchRunId)
    }
}
