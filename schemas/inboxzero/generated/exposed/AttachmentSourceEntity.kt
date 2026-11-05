package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class AttachmentSourceEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AttachmentSourceEntity>(AttachmentSourceTable)

    var createdAt by AttachmentSourceTable.createdAt
    var updatedAt by AttachmentSourceTable.updatedAt
    var name by AttachmentSourceTable.name
    var type by AttachmentSourceTable.type
    var sourceId by AttachmentSourceTable.sourceId
    var sourcePath by AttachmentSourceTable.sourcePath
    var ruleId by AttachmentSourceTable.ruleId
    var rule by RuleEntity referencedOn AttachmentSourceTable.ruleId
    var driveConnectionId by AttachmentSourceTable.driveConnectionId
    var driveConnection by DriveConnectionEntity referencedOn AttachmentSourceTable.driveConnectionId
    val documents by AttachmentDocumentEntity referrersOn AttachmentDocumentTable.attachmentSourceId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AttachmentSourceTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
