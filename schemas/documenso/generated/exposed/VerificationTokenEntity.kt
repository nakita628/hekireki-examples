package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class VerificationTokenEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, VerificationTokenEntity>(VerificationTokenTable)

    var secondaryId by VerificationTokenTable.secondaryId
    var identifier by VerificationTokenTable.identifier
    var token by VerificationTokenTable.token
    var completed by VerificationTokenTable.completed
    var expires by VerificationTokenTable.expires
    var createdAt by VerificationTokenTable.createdAt
    var metadata by VerificationTokenTable.metadata
    var userId by VerificationTokenTable.userId
    var user by UserEntity referencedOn VerificationTokenTable.userId
}
