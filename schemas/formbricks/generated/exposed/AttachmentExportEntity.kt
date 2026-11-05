package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class AttachmentExportEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AttachmentExportEntity>(AttachmentExportTable)

    var createdAt by AttachmentExportTable.createdAt
    var updatedAt by AttachmentExportTable.updatedAt
    var surveyId by AttachmentExportTable.surveyId
    var workspace by WorkspaceEntity referencedOn AttachmentExportTable.workspaceId
    var workspaceId by AttachmentExportTable.workspaceId
    var requestedBy by UserEntity optionalReferencedOn AttachmentExportTable.requestedById
    var requestedById by AttachmentExportTable.requestedById
    var status by AttachmentExportTable.status
    var filterCriteria by AttachmentExportTable.filterCriteria
    var timeZone by AttachmentExportTable.timeZone
    var dispatchCount by AttachmentExportTable.dispatchCount
    var fileCount by AttachmentExportTable.fileCount
    var responseCount by AttachmentExportTable.responseCount
    var includedFileCount by AttachmentExportTable.includedFileCount
    var totalBytes by AttachmentExportTable.totalBytes
    var truncated by AttachmentExportTable.truncated
    var storageKey by AttachmentExportTable.storageKey
    var errorCode by AttachmentExportTable.errorCode
    var startedAt by AttachmentExportTable.startedAt
    var finishedAt by AttachmentExportTable.finishedAt
    var expiresAt by AttachmentExportTable.expiresAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AttachmentExportTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
