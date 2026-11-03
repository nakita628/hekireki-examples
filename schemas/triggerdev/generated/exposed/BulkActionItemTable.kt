package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object BulkActionItemTable : IdTable<String>("\"BulkActionItem\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val friendlyId = text("friendlyId").nullable()
    val groupId = text("groupId")
    val type = pgEnum("type", "\"BulkActionType\"", BulkActionType.entries, BulkActionType::dbName)
    val status =
        pgEnum("status", "\"BulkActionItemStatus\"", BulkActionItemStatus.entries, BulkActionItemStatus::dbName).default(
            BulkActionItemStatus.PENDING,
        )
    val sourceRunId =
        reference(
            "sourceRunId",
            TaskRunTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"BulkActionItem_sourceRunId_fkey\"",
        )
    val destinationRunId =
        optReference(
            "destinationRunId",
            TaskRunTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"BulkActionItem_destinationRunId_fkey\"",
        )
    val error = text("error").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"BulkActionItem_pkey\"")
}
