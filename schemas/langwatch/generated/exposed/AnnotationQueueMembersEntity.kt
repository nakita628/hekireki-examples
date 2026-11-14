package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class AnnotationQueueMembersEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<AnnotationQueueMembersEntity>(AnnotationQueueMembersTable)

    val annotationQueueId by AnnotationQueueMembersTable.annotationQueueId
    val userId by AnnotationQueueMembersTable.userId
}
