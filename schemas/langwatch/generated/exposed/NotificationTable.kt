package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object NotificationTable : IdTable<String>("\"Notification\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val organizationId = text("organizationId").nullable()
    val projectId = text("projectId").nullable()
    val metadata = jsonb<String>("metadata", { it }, { it })
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val sentAt = pgTimestamp("sentAt", 3)
    override val primaryKey = PrimaryKey(id, name = "\"Notification_pkey\"")

    init {
        index("\"Notification_organizationId_idx\"", false, organizationId)
        index("\"Notification_projectId_idx\"", false, projectId)
        index("\"Notification_createdAt_idx\"", false, createdAt)
        index("\"Notification_sentAt_idx\"", false, sentAt)
    }
}
