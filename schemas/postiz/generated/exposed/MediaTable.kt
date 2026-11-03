package postiz.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object MediaTable : IdTable<String>("\"Media\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val name = text("name")
    val originalName = text("originalName").nullable()
    val path = text("path")
    val organizationId =
        reference(
            "organizationId",
            OrganizationTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Media_organizationId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val deletedAt = pgTimestamp("deletedAt", 3).nullable()
    val fileSize = integer("fileSize").default(0)
    val type = text("type").default("image")
    val thumbnail = text("thumbnail").nullable()
    val alt = text("alt").nullable()
    val thumbnailTimestamp = integer("thumbnailTimestamp").nullable()
    val status = text("status").default("ready")
    val processingError = text("processingError").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Media_pkey\"")

    init {
        index("\"Media_name_idx\"", false, name)
        index("\"Media_organizationId_idx\"", false, organizationId)
        index("\"Media_type_idx\"", false, type)
    }
}
