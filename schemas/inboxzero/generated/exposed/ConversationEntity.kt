package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ConversationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ConversationEntity>(ConversationTable)

    var createdAt by ConversationTable.createdAt
    var updatedAt by ConversationTable.updatedAt
    var organizationId by ConversationTable.organizationId
    var organization by OrganizationEntity referencedOn ConversationTable.organizationId
    var publisherId by ConversationTable.publisherId
    var publisher by MemberEntity referencedOn ConversationTable.publisherId
    var threadId by ConversationTable.threadId
    var status by ConversationTable.status
    var revision by ConversationTable.revision
    var generation by ConversationTable.generation
    val participants by ConversationParticipantEntity referrersOn ConversationParticipantTable.conversationId
    val comments by ConversationCommentEntity referrersOn ConversationCommentTable.conversationId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ConversationTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
