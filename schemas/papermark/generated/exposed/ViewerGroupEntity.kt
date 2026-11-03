package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ViewerGroupEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ViewerGroupEntity>(ViewerGroupTable)

    var name by ViewerGroupTable.name
    val members by ViewerGroupMembershipEntity referrersOn ViewerGroupMembershipTable.groupId
    var domains by ViewerGroupTable.domains
    val links by LinkEntity optionalReferrersOn LinkTable.groupId
    val accessControls by ViewerGroupAccessControlsEntity referrersOn ViewerGroupAccessControlsTable.groupId
    var allowAll by ViewerGroupTable.allowAll
    var dataroomId by ViewerGroupTable.dataroomId
    var dataroom by DataroomEntity referencedOn ViewerGroupTable.dataroomId
    var teamId by ViewerGroupTable.teamId
    var team by TeamEntity referencedOn ViewerGroupTable.teamId
    val views by ViewEntity optionalReferrersOn ViewTable.groupId
    val conversations by ConversationEntity optionalReferrersOn ConversationTable.viewerGroupId
    val invitations by ViewerInvitationEntity optionalReferrersOn ViewerInvitationTable.groupId
    val questionAssignments by DataroomQuestionAssignmentEntity optionalReferrersOn DataroomQuestionAssignmentTable.groupId
    val taskAssignments by TaskAssignmentEntity optionalReferrersOn TaskAssignmentTable.groupId
    var createdAt by ViewerGroupTable.createdAt
    var updatedAt by ViewerGroupTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ViewerGroupTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
