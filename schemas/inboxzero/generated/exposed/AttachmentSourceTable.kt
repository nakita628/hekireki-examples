package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object AttachmentSourceTable : IdTable<String>("\"AttachmentSource\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val name = text("name")
    val type = pgEnum("type", "\"AttachmentSourceType\"", AttachmentSourceType.entries, AttachmentSourceType::dbName)
    val sourceId = text("sourceId")
    val sourcePath = text("sourcePath").nullable()
    val ruleId =
        reference(
            "ruleId",
            RuleTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"AttachmentSource_ruleId_fkey\"",
        )
    val driveConnectionId =
        reference(
            "driveConnectionId",
            DriveConnectionTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"AttachmentSource_driveConnectionId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"AttachmentSource_pkey\"")

    init {
        index("\"AttachmentSource_ruleId_idx\"", false, ruleId)
        index("\"AttachmentSource_driveConnectionId_idx\"", false, driveConnectionId)
        uniqueIndex("\"AttachmentSource_ruleId_driveConnectionId_type_sourceId_key\"", ruleId, driveConnectionId, type, sourceId)
    }
}
