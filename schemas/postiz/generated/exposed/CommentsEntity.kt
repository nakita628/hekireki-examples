package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class CommentsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, CommentsEntity>(CommentsTable)

    var content by CommentsTable.content
    var organizationId by CommentsTable.organizationId
    var postId by CommentsTable.postId
    var userId by CommentsTable.userId
    var displayName by CommentsTable.displayName
    var parentId by CommentsTable.parentId
    var anchorStart by CommentsTable.anchorStart
    var anchorEnd by CommentsTable.anchorEnd
    var anchorQuote by CommentsTable.anchorQuote
    var resolvedAt by CommentsTable.resolvedAt
    var createdAt by CommentsTable.createdAt
    var updatedAt by CommentsTable.updatedAt
    var deletedAt by CommentsTable.deletedAt
    var organization by OrganizationEntity referencedOn CommentsTable.organizationId
    var post by PostEntity referencedOn CommentsTable.postId
    var user by UserEntity optionalReferencedOn CommentsTable.userId
    var parent by CommentsEntity optionalReferencedOn CommentsTable.parentId
    val replies by CommentsEntity optionalReferrersOn CommentsTable.parentId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(CommentsTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
