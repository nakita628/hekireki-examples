package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class DocumentRedactionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, DocumentRedactionEntity>(DocumentRedactionTable)

    var jobId by DocumentRedactionTable.jobId
    var job by DocumentRedactionJobEntity referencedOn DocumentRedactionTable.jobId
    var pageNumber by DocumentRedactionTable.pageNumber
    var x by DocumentRedactionTable.x
    var y by DocumentRedactionTable.y
    var width by DocumentRedactionTable.width
    var height by DocumentRedactionTable.height
    var textPreview by DocumentRedactionTable.textPreview
    var category by DocumentRedactionTable.category
    var confidence by DocumentRedactionTable.confidence
    var reason by DocumentRedactionTable.reason
    var source by DocumentRedactionTable.source1
    var status by DocumentRedactionTable.status
    var createdAt by DocumentRedactionTable.createdAt
    var updatedAt by DocumentRedactionTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(DocumentRedactionTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
