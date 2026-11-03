package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object WebhookTable : IdTable<String>("\"Webhook\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val pId = text("pId").uniqueIndex("\"Webhook_pId_key\"")
    val name = text("name")
    val url = text("url")
    val secret = text("secret")
    val triggers = jsonb<String>("triggers", { it }, { it })
    val teamId =
        reference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Webhook_teamId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"Webhook_pkey\"")

    init {
        index("\"Webhook_teamId_idx\"", false, teamId)
    }
}
