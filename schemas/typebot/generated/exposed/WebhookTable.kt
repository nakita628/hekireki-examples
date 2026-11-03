package typebot.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object WebhookTable : IdTable<String>("\"Webhook\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val url = text("url").nullable()
    val method = text("method")
    val queryParams = jsonb<String>("queryParams", { it }, { it })
    val headers = jsonb<String>("headers", { it }, { it })
    val body = text("body").nullable()
    val typebotId =
        reference(
            "typebotId",
            TypebotTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Webhook_typebotId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"Webhook_pkey\"")
}
