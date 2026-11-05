package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object EmailMessageTable : IdTable<String>("\"EmailMessage\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val threadId = text("threadId")
    val messageId = text("messageId")
    val date = pgTimestamp("date", 3)
    val from = text("from")
    val fromName = text("fromName").nullable()
    val fromDomain = text("fromDomain")
    val to = text("to")
    val unsubscribeLink = text("unsubscribeLink").nullable()
    val read = bool("read")
    val sent = bool("sent")
    val draft = bool("draft")
    val inbox = bool("inbox")
    val emailAccountId =
        reference(
            "emailAccountId",
            EmailAccountTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"EmailMessage_emailAccountId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"EmailMessage_pkey\"")

    init {
        index("\"EmailMessage_emailAccountId_threadId_idx\"", false, emailAccountId, threadId)
        index("\"EmailMessage_emailAccountId_date_idx\"", false, emailAccountId, date)
        index("\"EmailMessage_emailAccountId_from_idx\"", false, emailAccountId, from)
        index("\"EmailMessage_emailAccountId_fromName_idx\"", false, emailAccountId, fromName)
        uniqueIndex("\"EmailMessage_emailAccountId_threadId_messageId_key\"", emailAccountId, threadId, messageId)
    }
}
