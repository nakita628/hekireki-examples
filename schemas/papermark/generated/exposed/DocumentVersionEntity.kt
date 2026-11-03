package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class DocumentVersionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, DocumentVersionEntity>(DocumentVersionTable)

    var versionNumber by DocumentVersionTable.versionNumber
    var document by DocumentEntity referencedOn DocumentVersionTable.documentId
    var documentId by DocumentVersionTable.documentId
    var file by DocumentVersionTable.file
    var originalFile by DocumentVersionTable.originalFile
    var type by DocumentVersionTable.type
    var contentType by DocumentVersionTable.contentType
    var fileSize by DocumentVersionTable.fileSize
    var storageType by DocumentVersionTable.storageType
    var numPages by DocumentVersionTable.numPages
    var isPrimary by DocumentVersionTable.isPrimary
    var isVertical by DocumentVersionTable.isVertical
    var fileId by DocumentVersionTable.fileId
    var vectorStoreFileId by DocumentVersionTable.vectorStoreFileId
    val pages by DocumentPageEntity referrersOn DocumentPageTable.versionId
    val redactionJobs by DocumentRedactionJobEntity referrersOn DocumentRedactionJobTable.documentVersionId
    var hasPages by DocumentVersionTable.hasPages
    var length by DocumentVersionTable.length
    var createdAt by DocumentVersionTable.createdAt
    var updatedAt by DocumentVersionTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(DocumentVersionTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
