package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class VerifiedEmailEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, VerifiedEmailEntity>(VerifiedEmailTable)

    var userId by VerifiedEmailTable.userId
    var user by UserEntity optionalReferencedOn VerifiedEmailTable.userId
    var teamId by VerifiedEmailTable.teamId
    var team by TeamEntity optionalReferencedOn VerifiedEmailTable.teamId
    var email by VerifiedEmailTable.email
}
