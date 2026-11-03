package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class WaitlistEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, WaitlistEntity>(WaitlistTable)

    var invite by WaitlistTable.invite
    var inviteTime by WaitlistTable.inviteTime
    var createdTime by WaitlistTable.createdTime
}
