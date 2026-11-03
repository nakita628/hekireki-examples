package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class CommentReactionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, CommentReactionEntity>(CommentReactionTable)

    var projectId by CommentReactionTable.projectId
    var project by ProjectEntity referencedOn CommentReactionTable.projectId
    var commentId by CommentReactionTable.commentId
    var comment by CommentEntity referencedOn CommentReactionTable.commentId
    var userId by CommentReactionTable.userId
    var user by UserEntity referencedOn CommentReactionTable.userId
    var emoji by CommentReactionTable.emoji
    var createdAt by CommentReactionTable.createdAt
}
