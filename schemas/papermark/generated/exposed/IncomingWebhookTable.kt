package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object IncomingWebhookTable : IdTable<String>("\"IncomingWebhook\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val externalId = text("externalId").uniqueIndex("\"IncomingWebhook_externalId_key\"")
    val name = text("name")
    val secret = text("secret").nullable()
    val source1 = text("source").nullable()
    val actions = text("actions").nullable()
    val consecutiveFailures = integer("consecutiveFailures").default(0)
    val lastFailedAt = pgTimestamp("lastFailedAt", 3).nullable()
    val disabledAt = pgTimestamp("disabledAt", 3).nullable()
    val teamId =
        reference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"IncomingWebhook_teamId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"IncomingWebhook_pkey\"")

    init {
        index("\"IncomingWebhook_teamId_idx\"", false, teamId)
    }
}
