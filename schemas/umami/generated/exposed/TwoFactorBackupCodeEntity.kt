package umami.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class TwoFactorBackupCodeEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TwoFactorBackupCodeEntity>(TwoFactorBackupCodeTable)

    var userId by TwoFactorBackupCodeTable.userId
    var codeHash by TwoFactorBackupCodeTable.codeHash
    var used by TwoFactorBackupCodeTable.used
    var createdAt by TwoFactorBackupCodeTable.createdAt
}
