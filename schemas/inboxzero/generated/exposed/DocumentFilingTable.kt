package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object DocumentFilingTable : IdTable<String>("\"DocumentFiling\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val messageId = text("messageId")
    val attachmentId = text("attachmentId")
    val filename = text("filename")
    val folderId = text("folderId").nullable()
    val folderPath = text("folderPath")
    val fileId = text("fileId").nullable()
    val reasoning = text("reasoning").nullable()
    val confidence = double("confidence").nullable()
    val status =
        pgEnum("status", "\"DocumentFilingStatus\"", DocumentFilingStatus.entries, DocumentFilingStatus::dbName).default(
            DocumentFilingStatus.FILED,
        )
    val wasAsked = bool("wasAsked").default(false)
    val wasCorrected = bool("wasCorrected").default(false)
    val originalPath = text("originalPath").nullable()
    val correctedAt = pgTimestamp("correctedAt", 3).nullable()
    val feedbackPositive = bool("feedbackPositive").nullable()
    val feedbackAt = pgTimestamp("feedbackAt", 3).nullable()
    val notificationMessageId = text("notificationMessageId").nullable().uniqueIndex("\"DocumentFiling_notificationMessageId_key\"")
    val notificationSentAt = pgTimestamp("notificationSentAt", 3).nullable()
    val notificationBatchId = text("notificationBatchId").nullable()
    val driveConnectionId =
        reference(
            "driveConnectionId",
            DriveConnectionTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DocumentFiling_driveConnectionId_fkey\"",
        )
    val emailAccountId =
        reference(
            "emailAccountId",
            EmailAccountTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DocumentFiling_emailAccountId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"DocumentFiling_pkey\"")

    init {
        index("\"DocumentFiling_emailAccountId_status_idx\"", false, emailAccountId, status)
        index("\"DocumentFiling_emailAccountId_notificationBatchId_idx\"", false, emailAccountId, notificationBatchId)
        index("\"DocumentFiling_driveConnectionId_idx\"", false, driveConnectionId)
        index("\"DocumentFiling_messageId_idx\"", false, messageId)
        uniqueIndex("\"DocumentFiling_emailAccountId_messageId_attachmentId_key\"", emailAccountId, messageId, attachmentId)
    }
}
