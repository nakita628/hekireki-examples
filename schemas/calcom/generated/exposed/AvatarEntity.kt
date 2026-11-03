package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class AvatarEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AvatarEntity>(AvatarTable)

    var teamId by AvatarTable.teamId
    var userId by AvatarTable.userId
    var data by AvatarTable.data
    var isBanner by AvatarTable.isBanner
}
