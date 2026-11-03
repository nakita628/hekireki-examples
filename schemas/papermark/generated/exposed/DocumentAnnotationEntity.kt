package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class DocumentAnnotationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, DocumentAnnotationEntity>(DocumentAnnotationTable)

    var title by DocumentAnnotationTable.title
    var content by DocumentAnnotationTable.content
    var pages by DocumentAnnotationTable.pages
    var documentId by DocumentAnnotationTable.documentId
    var document by DocumentEntity referencedOn DocumentAnnotationTable.documentId
    var teamId by DocumentAnnotationTable.teamId
    var team by TeamEntity referencedOn DocumentAnnotationTable.teamId
    var createdById by DocumentAnnotationTable.createdById
    var createdBy by UserEntity referencedOn DocumentAnnotationTable.createdById
    var isVisible by DocumentAnnotationTable.isVisible
    val images by AnnotationImageEntity referrersOn AnnotationImageTable.annotationId
    var createdAt by DocumentAnnotationTable.createdAt
    var updatedAt by DocumentAnnotationTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(DocumentAnnotationTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
