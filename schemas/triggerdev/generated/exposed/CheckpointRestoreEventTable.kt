package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object CheckpointRestoreEventTable : IdTable<String>("\"CheckpointRestoreEvent\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val type = pgEnum("type", "\"CheckpointRestoreEventType\"", CheckpointRestoreEventType.entries, CheckpointRestoreEventType::dbName)
    val reason = text("reason").nullable()
    val metadata = text("metadata").nullable()
    val checkpointId =
        reference(
            "checkpointId",
            CheckpointTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"CheckpointRestoreEvent_checkpointId_fkey\"",
        )
    val runId =
        reference(
            "runId",
            TaskRunTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"CheckpointRestoreEvent_runId_fkey\"",
        )
    val attemptId =
        reference(
            "attemptId",
            TaskRunAttemptTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"CheckpointRestoreEvent_attemptId_fkey\"",
        )
    val projectId =
        reference(
            "projectId",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"CheckpointRestoreEvent_projectId_fkey\"",
        )
    val runtimeEnvironmentId =
        reference(
            "runtimeEnvironmentId",
            RuntimeEnvironmentTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"CheckpointRestoreEvent_runtimeEnvironmentId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"CheckpointRestoreEvent_pkey\"")

    init {
        index("\"CheckpointRestoreEvent_checkpointId_idx\"", false, checkpointId)
        index("\"CheckpointRestoreEvent_runId_idx\"", false, runId)
    }
}
