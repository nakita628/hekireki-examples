package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object SentMessageOpenTable : IdTable<String>("\"SentMessageOpen\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val token = text("token").uniqueIndex("\"SentMessageOpen_token_key\"")
    val emailAccountId =
        reference(
            "emailAccountId",
            EmailAccountTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"SentMessageOpen_emailAccountId_fkey\"",
        )
    val threadId = text("threadId").nullable()
    val messageId = text("messageId").nullable()
    val firstOpenedAt = pgTimestamp("firstOpenedAt", 3).nullable()
    val lastOpenedAt = pgTimestamp("lastOpenedAt", 3).nullable()
    val openCount = integer("openCount").default(0)
    override val primaryKey = PrimaryKey(id, name = "\"SentMessageOpen_pkey\"")

    init {
        index("\"SentMessageOpen_emailAccountId_threadId_idx\"", false, emailAccountId, threadId)
        uniqueIndex("\"SentMessageOpen_emailAccountId_messageId_key\"", emailAccountId, messageId)
    }
}
