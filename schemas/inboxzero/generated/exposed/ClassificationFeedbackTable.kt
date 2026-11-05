package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ClassificationFeedbackTable : IdTable<String>("\"ClassificationFeedback\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val sender = text("sender")
    val eventType =
        pgEnum(
            "eventType",
            "\"ClassificationFeedbackEventType\"",
            ClassificationFeedbackEventType.entries,
            ClassificationFeedbackEventType::dbName,
        )
    val ruleId =
        reference(
            "ruleId",
            RuleTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ClassificationFeedback_ruleId_fkey\"",
        )
    val emailAccountId =
        reference(
            "emailAccountId",
            EmailAccountTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ClassificationFeedback_emailAccountId_fkey\"",
        )
    val threadId = text("threadId")
    val messageId = text("messageId")
    override val primaryKey = PrimaryKey(id, name = "\"ClassificationFeedback_pkey\"")

    init {
        index("\"ClassificationFeedback_ruleId_idx\"", false, ruleId)
        uniqueIndex("ClassificationFeedback_emailAccountId_sender_ruleId_message_key", emailAccountId, sender, ruleId, messageId, eventType)
    }
}
