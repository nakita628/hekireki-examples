package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object ExecutedRuleTable : IdTable<String>("\"ExecutedRule\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val threadId = text("threadId")
    val messageId = text("messageId")
    val status = pgEnum("status", "\"ExecutedRuleStatus\"", ExecutedRuleStatus.entries, ExecutedRuleStatus::dbName)
    val automated = bool("automated")
    val reason = text("reason").nullable()
    val matchMetadata = jsonb<String>("matchMetadata", { it }, { it }).nullable()
    val ruleId =
        optReference(
            "ruleId",
            RuleTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ExecutedRule_ruleId_fkey\"",
        )
    val emailAccountId =
        reference(
            "emailAccountId",
            EmailAccountTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ExecutedRule_emailAccountId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"ExecutedRule_pkey\"")

    init {
        index("\"ExecutedRule_emailAccountId_threadId_messageId_ruleId_idx\"", false, emailAccountId, threadId, messageId, ruleId)
        index("\"ExecutedRule_emailAccountId_messageId_idx\"", false, emailAccountId, messageId)
        index("\"ExecutedRule_emailAccountId_status_createdAt_idx\"", false, emailAccountId, status, createdAt)
        index("\"ExecutedRule_emailAccountId_createdAt_idx\"", false, emailAccountId, createdAt)
        index("\"ExecutedRule_emailAccountId_id_idx\"", false, emailAccountId, id)
        index("\"ExecutedRule_emailAccountId_updatedAt_id_idx\"", false, emailAccountId, updatedAt, id)
        index("\"ExecutedRule_ruleId_idx\"", false, ruleId)
    }
}
