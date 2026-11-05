package openpanel.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class TwoFactorChallengeEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TwoFactorChallengeEntity>(TwoFactorChallengeTable)

    var userId by TwoFactorChallengeTable.userId
    var user by UserEntity referencedOn TwoFactorChallengeTable.userId
    var expiresAt by TwoFactorChallengeTable.expiresAt
    var createdAt by TwoFactorChallengeTable.createdAt
}
