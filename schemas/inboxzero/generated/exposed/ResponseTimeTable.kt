package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ResponseTimeTable : IdTable<String>("\"ResponseTime\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val threadId = text("threadId")
    val sentMessageId = text("sentMessageId")
    val receivedMessageId = text("receivedMessageId")
    val responseTimeMins = integer("responseTimeMins")
    val receivedAt = pgTimestamp("receivedAt", 3)
    val sentAt = pgTimestamp("sentAt", 3)
    val emailAccountId =
        reference(
            "emailAccountId",
            EmailAccountTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ResponseTime_emailAccountId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"ResponseTime_pkey\"")

    init {
        index("\"ResponseTime_emailAccountId_sentAt_idx\"", false, emailAccountId, sentAt)
        uniqueIndex("\"ResponseTime_emailAccountId_sentMessageId_key\"", emailAccountId, sentMessageId)
    }
}
