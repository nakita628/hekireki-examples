package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class AnnotationQueueScoresEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<AnnotationQueueScoresEntity>(AnnotationQueueScoresTable)

    val annotationQueueId by AnnotationQueueScoresTable.annotationQueueId
    val annotationScoreId by AnnotationQueueScoresTable.annotationScoreId
}
