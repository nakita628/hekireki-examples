package typebot.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object SuppressedEmailTable : IdTable<String>("\"SuppressedEmail\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val email = text("email").uniqueIndex("\"SuppressedEmail_email_key\"")
    val transientGeneralBounceCount = integer("transientGeneralBounceCount").default(0)
    val suppressedAt = pgTimestamp("suppressedAt", 3).nullable()
    val lastWebhookId = text("lastWebhookId").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"SuppressedEmail_pkey\"")
}
