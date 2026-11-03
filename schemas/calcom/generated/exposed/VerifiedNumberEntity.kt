package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class VerifiedNumberEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, VerifiedNumberEntity>(VerifiedNumberTable)

    var userId by VerifiedNumberTable.userId
    var user by UserEntity optionalReferencedOn VerifiedNumberTable.userId
    var teamId by VerifiedNumberTable.teamId
    var team by TeamEntity optionalReferencedOn VerifiedNumberTable.teamId
    var phoneNumber by VerifiedNumberTable.phoneNumber
}
