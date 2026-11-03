package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class InvitationRecordEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, InvitationRecordEntity>(InvitationRecordTable)

    var invitationId by InvitationRecordTable.invitationId
    var baseId by InvitationRecordTable.baseId
    var spaceId by InvitationRecordTable.spaceId
    var type by InvitationRecordTable.type
    var inviter by InvitationRecordTable.inviter
    var accepter by InvitationRecordTable.accepter
    var createdTime by InvitationRecordTable.createdTime
}
