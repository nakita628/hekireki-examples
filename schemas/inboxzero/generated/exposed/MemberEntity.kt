package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class MemberEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MemberEntity>(MemberTable)

    var organizationId by MemberTable.organizationId
    var emailAccountId by MemberTable.emailAccountId
    var role by MemberTable.role
    var allowOrgAdminAnalytics by MemberTable.allowOrgAdminAnalytics
    var createdAt by MemberTable.createdAt
    var updatedAt by MemberTable.updatedAt
    var organization by OrganizationEntity referencedOn MemberTable.organizationId
    var emailAccount by EmailAccountEntity referencedOn MemberTable.emailAccountId
    val conversations by ConversationEntity referrersOn ConversationTable.publisherId
    val conversationParticipants by ConversationParticipantEntity referrersOn ConversationParticipantTable.memberId
    val conversationComments by ConversationCommentEntity optionalReferrersOn ConversationCommentTable.authorMemberId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(MemberTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
