package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class PostEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, PostEntity>(PostTable)

    var state by PostTable.state
    var publishDate by PostTable.publishDate
    var organizationId by PostTable.organizationId
    var integrationId by PostTable.integrationId
    var content by PostTable.content
    var delay by PostTable.delay
    var group by PostTable.group
    var batchId by PostTable.batchId
    var title by PostTable.title
    var description by PostTable.description
    var parentPostId by PostTable.parentPostId
    var releaseId by PostTable.releaseId
    var releaseURL by PostTable.releaseURL
    var settings by PostTable.settings
    var image by PostTable.image
    var submittedForOrderId by PostTable.submittedForOrderId
    var submittedForOrganizationId by PostTable.submittedForOrganizationId
    var approvedSubmitForOrder by PostTable.approvedSubmitForOrder
    var creationMethod by PostTable.creationMethod
    var lastMessageId by PostTable.lastMessageId
    var intervalInDays by PostTable.intervalInDays
    var error by PostTable.error
    var createdAt by PostTable.createdAt
    var updatedAt by PostTable.updatedAt
    var deletedAt by PostTable.deletedAt
    val comments by CommentsEntity referrersOn CommentsTable.postId
    val errors by ErrorsEntity referrersOn ErrorsTable.postId
    val payoutProblems by PayoutProblemsEntity optionalReferrersOn PayoutProblemsTable.postId
    var integration by IntegrationEntity referencedOn PostTable.integrationId
    var lastMessage by MessagesEntity optionalReferencedOn PostTable.lastMessageId
    var organization by OrganizationEntity referencedOn PostTable.organizationId
    var parentPost by PostEntity optionalReferencedOn PostTable.parentPostId
    val childrenPost by PostEntity optionalReferrersOn PostTable.parentPostId
    var submittedForOrder by OrdersEntity optionalReferencedOn PostTable.submittedForOrderId
    var submittedForOrganization by OrganizationEntity optionalReferencedOn PostTable.submittedForOrganizationId
    val tags by TagsPostsEntity referrersOn TagsPostsTable.postId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(PostTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
