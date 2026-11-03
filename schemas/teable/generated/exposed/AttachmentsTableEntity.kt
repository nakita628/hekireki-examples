package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class AttachmentsTableEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AttachmentsTableEntity>(AttachmentsTableTable)

    var attachmentId by AttachmentsTableTable.attachmentId
    var name by AttachmentsTableTable.name
    var token by AttachmentsTableTable.token
    var tableId by AttachmentsTableTable.tableId
    var recordId by AttachmentsTableTable.recordId
    var fieldId by AttachmentsTableTable.fieldId
    var createdTime by AttachmentsTableTable.createdTime
    var createdBy by AttachmentsTableTable.createdBy
    var lastModifiedBy by AttachmentsTableTable.lastModifiedBy
    var lastModifiedTime by AttachmentsTableTable.lastModifiedTime

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AttachmentsTableTable.lastModifiedTime)) {
                lastModifiedTime = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
