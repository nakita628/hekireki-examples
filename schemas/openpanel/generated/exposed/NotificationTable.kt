package openpanel.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object NotificationTable : IdTable<UUID>("notifications") {
    override val id = javaUUID("id").databaseDefault("gen_random_uuid()").entityId()
    val projectId =
        reference(
            "projectId",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"notifications_projectId_fkey\"",
        )
    val title = text("title")
    val message = text("message")
    val isReadAt = pgTimestamp("isReadAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val sendToApp = bool("sendToApp").default(false)
    val sendToEmail = bool("sendToEmail").default(false)
    val integrationId =
        optReference(
            "integrationId",
            IntegrationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"notifications_integrationId_fkey\"",
        )
    val notificationRuleId =
        optReference(
            "notificationRuleId",
            NotificationRuleTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"notifications_notificationRuleId_fkey\"",
        )
    val payload = jsonb<String>("payload", { it }, { it }).nullable()
    override val primaryKey = PrimaryKey(id, name = "notifications_pkey")
}
