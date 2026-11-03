package documenso.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object WebhookCallTable : IdTable<String>("\"WebhookCall\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val status = pgEnum("status", "\"WebhookCallStatus\"", WebhookCallStatus.entries, WebhookCallStatus::dbName)
    val url = text("url")
    val event = pgEnum("event", "\"WebhookTriggerEvents\"", WebhookTriggerEvents.entries, WebhookTriggerEvents::dbName)
    val requestBody = jsonb<String>("requestBody", { it }, { it })
    val responseCode = integer("responseCode")
    val responseHeaders = jsonb<String>("responseHeaders", { it }, { it }).nullable()
    val responseBody = jsonb<String>("responseBody", { it }, { it }).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val webhookId =
        reference(
            "webhookId",
            WebhookTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"WebhookCall_webhookId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"WebhookCall_pkey\"")
}
