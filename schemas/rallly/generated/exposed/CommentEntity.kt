package rallly.models

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

    var content by CommentTable.content
    var pollId by CommentTable.pollId
    var authorName by CommentTable.authorName
    var userId by CommentTable.userId
    var createdAt by CommentTable.createdAt
    var updatedAt by CommentTable.updatedAt
    var poll by PollEntity referencedOn CommentTable.pollId
    var user by UserEntity optionalReferencedOn CommentTable.userId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(CommentTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
