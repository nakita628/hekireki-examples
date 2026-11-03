package postiz.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object NotificationsTable : IdTable<String>("\"Notifications\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val organizationId =
        reference(
            "organizationId",
            OrganizationTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Notifications_organizationId_fkey\"",
        )
    val content = text("content")
    val link = text("link").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val deletedAt = pgTimestamp("deletedAt", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Notifications_pkey\"")

    init {
        index("\"Notifications_createdAt_idx\"", false, createdAt)
        index("\"Notifications_organizationId_idx\"", false, organizationId)
        index("\"Notifications_deletedAt_idx\"", false, deletedAt)
    }
}
