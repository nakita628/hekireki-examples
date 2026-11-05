package openpanel.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object NotificationRuleTable : IdTable<UUID>("notification_rules") {
    override val id = javaUUID("id").databaseDefault("gen_random_uuid()").entityId()
    val name = text("name")
    val projectId =
        reference(
            "projectId",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"notification_rules_projectId_fkey\"",
        )
    val sendToApp = bool("sendToApp").default(false)
    val sendToEmail = bool("sendToEmail").default(false)
    val config = jsonb<String>("config", { it }, { it })
    val template = text("template").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "notification_rules_pkey")
}
