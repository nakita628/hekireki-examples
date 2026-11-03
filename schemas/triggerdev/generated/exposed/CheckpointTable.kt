package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object CheckpointTable : IdTable<String>("\"Checkpoint\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val friendlyId = text("friendlyId").uniqueIndex("\"Checkpoint_friendlyId_key\"")
    val type = pgEnum("type", "\"CheckpointType\"", CheckpointType.entries, CheckpointType::dbName)
    val location = text("location")
    val imageRef = text("imageRef")
    val reason = text("reason").nullable()
    val metadata = text("metadata").nullable()
    val runId =
        reference(
            "runId",
            TaskRunTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Checkpoint_runId_fkey\"",
        )
    val attemptId =
        reference(
            "attemptId",
            TaskRunAttemptTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Checkpoint_attemptId_fkey\"",
        )
    val attemptNumber = integer("attemptNumber").nullable()
    val projectId =
        reference(
            "projectId",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Checkpoint_projectId_fkey\"",
        )
    val runtimeEnvironmentId =
        reference(
            "runtimeEnvironmentId",
            RuntimeEnvironmentTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Checkpoint_runtimeEnvironmentId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"Checkpoint_pkey\"")

    init {
        index("\"Checkpoint_attemptId_idx\"", false, attemptId)
        index("\"Checkpoint_runId_idx\"", false, runId)
    }
}
