package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class DocumentPageEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, DocumentPageEntity>(DocumentPageTable)

    var version by DocumentVersionEntity referencedOn DocumentPageTable.versionId
    var versionId by DocumentPageTable.versionId
    var pageNumber by DocumentPageTable.pageNumber
    var embeddedLinks by DocumentPageTable.embeddedLinks
    var pageLinks by DocumentPageTable.pageLinks
    var metadata by DocumentPageTable.metadata
    var file by DocumentPageTable.file
    var storageType by DocumentPageTable.storageType
    var createdAt by DocumentPageTable.createdAt
    var updatedAt by DocumentPageTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(DocumentPageTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
