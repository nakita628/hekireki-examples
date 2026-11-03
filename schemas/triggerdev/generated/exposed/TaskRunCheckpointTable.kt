package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object TaskRunCheckpointTable : IdTable<String>("\"TaskRunCheckpoint\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val friendlyId = text("friendlyId").uniqueIndex("\"TaskRunCheckpoint_friendlyId_key\"")
    val type = pgEnum("type", "\"TaskRunCheckpointType\"", TaskRunCheckpointType.entries, TaskRunCheckpointType::dbName)
    val location = text("location")
    val imageRef = text("imageRef").nullable()
    val reason = text("reason").nullable()
    val metadata = text("metadata").nullable()
    val projectId =
        reference(
            "projectId",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TaskRunCheckpoint_projectId_fkey\"",
        )
    val runtimeEnvironmentId =
        reference(
            "runtimeEnvironmentId",
            RuntimeEnvironmentTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TaskRunCheckpoint_runtimeEnvironmentId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"TaskRunCheckpoint_pkey\"")
}
