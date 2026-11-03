package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class TwoFactorEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TwoFactorEntity>(TwoFactorTable)

    var secret by TwoFactorTable.secret
    var backupCodes by TwoFactorTable.backupCodes
    var userId by TwoFactorTable.userId
    var user by UserEntity referencedOn TwoFactorTable.userId
    var verified by TwoFactorTable.verified
    var failedVerificationCount by TwoFactorTable.failedVerificationCount
    var lockedUntil by TwoFactorTable.lockedUntil
}
