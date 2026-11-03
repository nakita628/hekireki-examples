package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class InvitationCodeEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, InvitationCodeEntity>(InvitationCodeTable)

    var code by InvitationCodeTable.code
    val users by UserEntity optionalReferrersOn UserTable.invitationCodeId
    var createdAt by InvitationCodeTable.createdAt
}
