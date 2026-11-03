package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class AnnotationImageEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AnnotationImageEntity>(AnnotationImageTable)

    var filename by AnnotationImageTable.filename
    var url by AnnotationImageTable.url
    var size by AnnotationImageTable.size
    var mimeType by AnnotationImageTable.mimeType
    var annotationId by AnnotationImageTable.annotationId
    var annotation by DocumentAnnotationEntity referencedOn AnnotationImageTable.annotationId
    var createdAt by AnnotationImageTable.createdAt
}
