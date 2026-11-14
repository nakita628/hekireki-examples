package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable

object AnnotationQueueMembersTable : CompositeIdTable("\"AnnotationQueueMembers\"") {
    val annotationQueueId = text("annotationQueueId").entityId()
    val userId = text("userId").entityId()
    override val primaryKey = PrimaryKey(annotationQueueId, userId, name = "\"AnnotationQueueMembers_pkey\"")

    init {
        index("\"AnnotationQueueMembers_annotationQueueId_idx\"", false, annotationQueueId)
        index("\"AnnotationQueueMembers_userId_idx\"", false, userId)
    }
}
