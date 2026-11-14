package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable

object AnnotationQueueScoresTable : CompositeIdTable("\"AnnotationQueueScores\"") {
    val annotationQueueId = text("annotationQueueId").entityId()
    val annotationScoreId = text("annotationScoreId").entityId()
    override val primaryKey = PrimaryKey(annotationQueueId, annotationScoreId, name = "\"AnnotationQueueScores_pkey\"")

    init {
        index("\"AnnotationQueueScores_annotationQueueId_idx\"", false, annotationQueueId)
        index("\"AnnotationQueueScores_annotationScoreId_idx\"", false, annotationScoreId)
    }
}
