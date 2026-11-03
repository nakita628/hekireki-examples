package postiz.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object ClippingTable : IdTable<String>("\"Clipping\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val organizationId =
        reference(
            "organizationId",
            OrganizationTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Clipping_organizationId_fkey\"",
        )
    val url = text("url")
    val status = text("status").default("analysing")
    val error = text("error").nullable()
    val title = text("title").nullable()
    val thumbnail = text("thumbnail").nullable()
    val duration = integer("duration").nullable()
    val maxClips = integer("maxClips").default(5)
    val fit = text("fit").default("blur")
    val integrations = text("integrations")
    val creditsId = text("creditsId").nullable()
    val deletedAt = pgTimestamp("deletedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"Clipping_pkey\"")

    init {
        index("\"Clipping_organizationId_idx\"", false, organizationId)
        index("\"Clipping_deletedAt_idx\"", false, deletedAt)
    }
}
