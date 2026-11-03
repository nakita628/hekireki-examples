package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class CommentEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, CommentEntity>(CommentTable)

    var projectId by CommentTable.projectId
    var project by ProjectEntity referencedOn CommentTable.projectId
    var objectType by CommentTable.objectType
    var objectId by CommentTable.objectId
    var createdAt by CommentTable.createdAt
    var updatedAt by CommentTable.updatedAt
    var content by CommentTable.content
    var authorUserId by CommentTable.authorUserId
    val reactions by CommentReactionEntity referrersOn CommentReactionTable.commentId
    var dataField by CommentTable.dataField
    var path by CommentTable.path
    var rangeStart by CommentTable.rangeStart
    var rangeEnd by CommentTable.rangeEnd

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(CommentTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
