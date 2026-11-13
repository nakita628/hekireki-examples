package superproductivity.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class PendingPasskeyRegistrationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, PendingPasskeyRegistrationEntity>(PendingPasskeyRegistrationTable)

    var verificationToken by PendingPasskeyRegistrationTable.verificationToken
    var verificationTokenExpiresAt by PendingPasskeyRegistrationTable.verificationTokenExpiresAt
    var credentialId by PendingPasskeyRegistrationTable.credentialId
    var publicKey by PendingPasskeyRegistrationTable.publicKey
    var counter by PendingPasskeyRegistrationTable.counter
    var transports by PendingPasskeyRegistrationTable.transports
    var createdAt by PendingPasskeyRegistrationTable.createdAt
    var userId by PendingPasskeyRegistrationTable.userId
    var user by UserEntity referencedOn PendingPasskeyRegistrationTable.userId
}
