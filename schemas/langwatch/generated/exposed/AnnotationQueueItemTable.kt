package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object AnnotationQueueItemTable : IdTable<String>("\"AnnotationQueueItem\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val annotationQueueId = text("annotationQueueId").nullable()
    val userId = text("userId").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdByUserId = text("createdByUserId").nullable()
    val traceId = text("traceId")
    val projectId = text("projectId")
    val doneAt = pgTimestamp("doneAt", 3).nullable()
    val markedForDatasetAt = pgTimestamp("markedForDatasetAt", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"AnnotationQueueItem_pkey\"")

    init {
        index("\"AnnotationQueueItem_annotationQueueId_idx\"", false, annotationQueueId)
        index("\"AnnotationQueueItem_userId_idx\"", false, userId)
        index("\"AnnotationQueueItem_projectId_idx\"", false, projectId)
        index("\"AnnotationQueueItem_projectId_doneAt_idx\"", false, projectId, doneAt)
        index("\"AnnotationQueueItem_projectId_userId_doneAt_idx\"", false, projectId, userId, doneAt)
        index("\"AnnotationQueueItem_annotationQueueId_doneAt_idx\"", false, annotationQueueId, doneAt)
        uniqueIndex("\"AnnotationQueueItem_traceId_userId_projectId_key\"", traceId, userId, projectId)
        uniqueIndex("\"AnnotationQueueItem_traceId_annotationQueueId_projectId_key\"", traceId, annotationQueueId, projectId)
    }
}
