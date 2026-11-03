package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ViewerEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ViewerEntity>(ViewerTable)

    var email by ViewerTable.email
    var verified by ViewerTable.verified
    var invitedAt by ViewerTable.invitedAt
    var notificationPreferences by ViewerTable.notificationPreferences
    var dataroomId by ViewerTable.dataroomId
    var dataroom by DataroomEntity optionalReferencedOn ViewerTable.dataroomId
    var teamId by ViewerTable.teamId
    var team by TeamEntity referencedOn ViewerTable.teamId
    val views by ViewEntity optionalReferrersOn ViewTable.viewerId
    val groups by ViewerGroupMembershipEntity referrersOn ViewerGroupMembershipTable.viewerId
    val invitations by ViewerInvitationEntity referrersOn ViewerInvitationTable.viewerId
    val participatedConversations by ConversationParticipantEntity optionalReferrersOn ConversationParticipantTable.viewerId
    val messages by MessageEntity optionalReferrersOn MessageTable.viewerId
    val recipientConversations by ConversationEntity optionalReferrersOn ConversationTable.recipientViewerId
    val questionAssignments by DataroomQuestionAssignmentEntity optionalReferrersOn DataroomQuestionAssignmentTable.viewerId
    val uploadedDocuments by DocumentUploadEntity optionalReferrersOn DocumentUploadTable.viewerId
    val taskAssignments by TaskAssignmentEntity optionalReferrersOn TaskAssignmentTable.viewerId
    val taskActivities by TaskActivityEntity optionalReferrersOn TaskActivityTable.viewerId
    val chats by ChatEntity optionalReferrersOn ChatTable.viewerId
    var createdAt by ViewerTable.createdAt
    var updatedAt by ViewerTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ViewerTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
