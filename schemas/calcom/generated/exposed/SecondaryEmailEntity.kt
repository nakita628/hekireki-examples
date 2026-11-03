package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class SecondaryEmailEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, SecondaryEmailEntity>(SecondaryEmailTable)

    var user by UserEntity referencedOn SecondaryEmailTable.userId
    var userId by SecondaryEmailTable.userId
    var email by SecondaryEmailTable.email
    var emailVerified by SecondaryEmailTable.emailVerified
    val verificationTokens by VerificationTokenEntity optionalReferrersOn VerificationTokenTable.secondaryEmailId
    val eventTypes by EventTypeEntity optionalReferrersOn EventTypeTable.secondaryEmailId
}
