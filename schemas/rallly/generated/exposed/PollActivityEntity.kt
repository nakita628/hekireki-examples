package rallly.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class PollActivityEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, PollActivityEntity>(PollActivityTable)

    var pollId by PollActivityTable.pollId
    var type by PollActivityTable.type
    var userId by PollActivityTable.userId
    var participantId by PollActivityTable.participantId
    var inviteId by PollActivityTable.inviteId
    var optionId by PollActivityTable.optionId
    var payload by PollActivityTable.payload
    var createdAt by PollActivityTable.createdAt
    var poll by PollEntity referencedOn PollActivityTable.pollId
}
