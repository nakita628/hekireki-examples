package teable.models

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

    var tableId by CommentTable.tableId
    var recordId by CommentTable.recordId
    var quoteId by CommentTable.quoteId
    var content by CommentTable.content
    var reaction by CommentTable.reaction
    var deletedTime by CommentTable.deletedTime
    var createdTime by CommentTable.createdTime
    var createdBy by CommentTable.createdBy
    var lastModifiedTime by CommentTable.lastModifiedTime

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(CommentTable.lastModifiedTime)) {
                lastModifiedTime = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
