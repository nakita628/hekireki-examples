package postiz.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object CreditsTable : IdTable<String>("\"Credits\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val organizationId =
        reference(
            "organizationId",
            OrganizationTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Credits_organizationId_fkey\"",
        )
    val credits = integer("credits")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val type = text("type").default("ai_images")
    override val primaryKey = PrimaryKey(id, name = "\"Credits_pkey\"")

    init {
        index("\"Credits_organizationId_idx\"", false, organizationId)
        index("\"Credits_createdAt_idx\"", false, createdAt)
    }
}
