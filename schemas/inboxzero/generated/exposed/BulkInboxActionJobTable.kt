package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object BulkInboxActionJobTable : IdTable<String>("\"BulkInboxActionJob\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val status =
        pgEnum("status", "\"BulkInboxActionJobStatus\"", BulkInboxActionJobStatus.entries, BulkInboxActionJobStatus::dbName).default(
            BulkInboxActionJobStatus.RUNNING,
        )
    val search = jsonb<String>("search", { it }, { it })
    val action = text("action")
    val labelId = text("labelId").nullable()
    val labelName = text("labelName").nullable()
    val instruction = text("instruction").nullable()
    val pageToken = text("pageToken").nullable()
    val rateLimitedUntil = pgTimestamp("rateLimitedUntil", 3).nullable()
    val leaseExpiresAt = pgTimestamp("leaseExpiresAt", 3).nullable()
    val chunkToken = text("chunkToken").nullable()
    val recoveryCount = integer("recoveryCount").default(0)
    val processedCount = integer("processedCount").default(0)
    val matchedCount = integer("matchedCount").default(0)
    val actedCount = integer("actedCount").default(0)
    val skippedCount = integer("skippedCount").default(0)
    val failedCount = integer("failedCount").default(0)
    val limitReached = bool("limitReached").default(false)
    val error = text("error").nullable()
    val completedAt = pgTimestamp("completedAt", 3).nullable()
    val confirmationKey = text("confirmationKey")
    val chatId =
        optReference(
            "chatId",
            ChatTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"BulkInboxActionJob_chatId_fkey\"",
        )
    val emailAccountId =
        reference(
            "emailAccountId",
            EmailAccountTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"BulkInboxActionJob_emailAccountId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"BulkInboxActionJob_pkey\"")

    init {
        index("\"BulkInboxActionJob_chatId_updatedAt_idx\"", false, chatId, updatedAt)
        uniqueIndex("\"BulkInboxActionJob_emailAccountId_confirmationKey_key\"", emailAccountId, confirmationKey)
    }
}
