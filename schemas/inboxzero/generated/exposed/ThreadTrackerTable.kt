package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object ThreadTrackerTable : IdTable<String>("\"ThreadTracker\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val sentAt = pgTimestamp("sentAt", 3)
    val threadId = text("threadId")
    val messageId = text("messageId")
    val resolved = bool("resolved").default(false)
    val type = pgEnum("type", "\"ThreadTrackerType\"", ThreadTrackerType.entries, ThreadTrackerType::dbName)
    val followUpAppliedAt = pgTimestamp("followUpAppliedAt", 3).nullable()
    val followUpDraftId = text("followUpDraftId").nullable()
    val followUpDraftContent = text("followUpDraftContent").nullable()
    val followUpDraftCreatedAt = pgTimestamp("followUpDraftCreatedAt", 3).nullable()
    val followUpNotifications = jsonb<String>("followUpNotifications", { it }, { it }).nullable()
    val emailAccountId =
        reference(
            "emailAccountId",
            EmailAccountTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ThreadTracker_emailAccountId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"ThreadTracker_pkey\"")

    init {
        index("\"ThreadTracker_emailAccountId_resolved_idx\"", false, emailAccountId, resolved)
        index("\"ThreadTracker_emailAccountId_resolved_sentAt_type_idx\"", false, emailAccountId, resolved, sentAt, type)
        index("\"ThreadTracker_emailAccountId_type_resolved_sentAt_idx\"", false, emailAccountId, type, resolved, sentAt)
        index(
            "ThreadTracker_emailAccountId_type_resolved_followUpAppliedA_idx",
            false,
            emailAccountId,
            type,
            resolved,
            followUpAppliedAt,
            sentAt,
        )
        index(
            "\"ThreadTracker_followUpDraft_cleanup_idx\"",
            false,
            emailAccountId,
            followUpDraftCreatedAt,
            filterCondition = sqlCondition("(\"followUpDraftContent\" IS NOT NULL)"),
        )
        uniqueIndex("\"ThreadTracker_emailAccountId_threadId_messageId_key\"", emailAccountId, threadId, messageId)
    }
}
