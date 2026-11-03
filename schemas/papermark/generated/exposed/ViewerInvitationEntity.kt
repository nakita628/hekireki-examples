package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ViewerInvitationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ViewerInvitationEntity>(ViewerInvitationTable)

    var viewerId by ViewerInvitationTable.viewerId
    var viewer by ViewerEntity referencedOn ViewerInvitationTable.viewerId
    var linkId by ViewerInvitationTable.linkId
    var link by LinkEntity referencedOn ViewerInvitationTable.linkId
    var groupId by ViewerInvitationTable.groupId
    var group by ViewerGroupEntity optionalReferencedOn ViewerInvitationTable.groupId
    var invitedBy by ViewerInvitationTable.invitedBy
    var customMessage by ViewerInvitationTable.customMessage
    var sentAt by ViewerInvitationTable.sentAt
    var status by ViewerInvitationTable.status
    var createdAt by ViewerInvitationTable.createdAt
}
