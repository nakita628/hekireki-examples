package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class TagItemEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TagItemEntity>(TagItemTable)

    var tagId by TagItemTable.tagId
    var tag by TagEntity referencedOn TagItemTable.tagId
    var itemType by TagItemTable.itemType
    var linkId by TagItemTable.linkId
    var link by LinkEntity optionalReferencedOn TagItemTable.linkId
    var documentId by TagItemTable.documentId
    var document by DocumentEntity optionalReferencedOn TagItemTable.documentId
    var dataroomId by TagItemTable.dataroomId
    var dataroom by DataroomEntity optionalReferencedOn TagItemTable.dataroomId
    var taggedBy by TagItemTable.taggedBy
    var createdAt by TagItemTable.createdAt
    var updatedAt by TagItemTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TagItemTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
