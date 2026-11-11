package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object AnnotationQueueItemTable : IdTable<String>("annotation_queue_items") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val queueId =
        reference(
            "queue_id",
            AnnotationQueueTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "annotation_queue_items_queue_id_fkey",
        )
    val objectId = text("object_id")
    val objectType =
        pgEnum("object_type", "\"AnnotationQueueObjectType\"", AnnotationQueueObjectType.entries, AnnotationQueueObjectType::dbName)
    val status =
        pgEnum("status", "\"AnnotationQueueStatus\"", AnnotationQueueStatus.entries, AnnotationQueueStatus::dbName).default(
            AnnotationQueueStatus.PENDING,
        )
    val lockedAt = pgTimestamp("locked_at", 3).nullable()
    val lockedByUserId =
        optReference(
            "locked_by_user_id",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "annotation_queue_items_locked_by_user_id_fkey",
        )
    val annotatorUserId =
        optReference(
            "annotator_user_id",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "annotation_queue_items_annotator_user_id_fkey",
        )
    val completedAt = pgTimestamp("completed_at", 3).nullable()
    val projectId =
        reference(
            "project_id",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "annotation_queue_items_project_id_fkey",
        )
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "annotation_queue_items_pkey")

    init {
        index("annotation_queue_items_project_id_queue_id_status_idx", false, projectId, queueId, status)
        index("annotation_queue_items_project_id_queue_id_created_at_id_idx", false, projectId, queueId, createdAt, id)
        index("annotation_queue_items_object_id_object_type_project_id_que_idx", false, objectId, objectType, projectId, queueId)
        index("annotation_queue_items_annotator_user_id_idx", false, annotatorUserId)
        index("annotation_queue_items_created_at_idx", false, createdAt)
    }
}
