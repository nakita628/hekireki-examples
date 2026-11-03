package postiz.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object WebhooksTable : IdTable<String>("\"Webhooks\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val name = text("name")
    val organizationId =
        reference(
            "organizationId",
            OrganizationTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Webhooks_organizationId_fkey\"",
        )
    val url = text("url")
    val deletedAt = pgTimestamp("deletedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"Webhooks_pkey\"")

    init {
        index("\"Webhooks_organizationId_idx\"", false, organizationId)
        index("\"Webhooks_deletedAt_idx\"", false, deletedAt)
    }
}
