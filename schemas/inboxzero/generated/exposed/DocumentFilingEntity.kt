package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class DocumentFilingEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, DocumentFilingEntity>(DocumentFilingTable)

    var createdAt by DocumentFilingTable.createdAt
    var updatedAt by DocumentFilingTable.updatedAt
    var messageId by DocumentFilingTable.messageId
    var attachmentId by DocumentFilingTable.attachmentId
    var filename by DocumentFilingTable.filename
    var folderId by DocumentFilingTable.folderId
    var folderPath by DocumentFilingTable.folderPath
    var fileId by DocumentFilingTable.fileId
    var reasoning by DocumentFilingTable.reasoning
    var confidence by DocumentFilingTable.confidence
    var status by DocumentFilingTable.status
    var wasAsked by DocumentFilingTable.wasAsked
    var wasCorrected by DocumentFilingTable.wasCorrected
    var originalPath by DocumentFilingTable.originalPath
    var correctedAt by DocumentFilingTable.correctedAt
    var feedbackPositive by DocumentFilingTable.feedbackPositive
    var feedbackAt by DocumentFilingTable.feedbackAt
    var notificationMessageId by DocumentFilingTable.notificationMessageId
    var notificationSentAt by DocumentFilingTable.notificationSentAt
    var notificationBatchId by DocumentFilingTable.notificationBatchId
    var driveConnectionId by DocumentFilingTable.driveConnectionId
    var driveConnection by DriveConnectionEntity referencedOn DocumentFilingTable.driveConnectionId
    var emailAccountId by DocumentFilingTable.emailAccountId
    var emailAccount by EmailAccountEntity referencedOn DocumentFilingTable.emailAccountId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(DocumentFilingTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
