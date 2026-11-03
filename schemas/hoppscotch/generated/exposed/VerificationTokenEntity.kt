package hoppscotch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class VerificationTokenEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, VerificationTokenEntity>(VerificationTokenTable)

    var deviceIdentifier by VerificationTokenTable.deviceIdentifier
    var userUid by VerificationTokenTable.userUid
    var expiresOn by VerificationTokenTable.expiresOn
    var user by UserEntity referencedOn VerificationTokenTable.userUid
}
