package documenso.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object WebhookTable : IdTable<String>("\"Webhook\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val webhookUrl = text("webhookUrl")
    val eventTriggers =
        array("eventTriggers", PgEnumColumnType("\"WebhookTriggerEvents\"", WebhookTriggerEvents.entries, WebhookTriggerEvents::dbName))
            .nullable()
    val secret = text("secret").nullable()
    val enabled = bool("enabled").default(true)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Webhook_userId_fkey\"",
        )
    val teamId =
        reference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Webhook_teamId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"Webhook_pkey\"")
}
