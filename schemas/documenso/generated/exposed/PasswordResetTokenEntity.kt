package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class PasswordResetTokenEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, PasswordResetTokenEntity>(PasswordResetTokenTable)

    var token by PasswordResetTokenTable.token
    var createdAt by PasswordResetTokenTable.createdAt
    var expiry by PasswordResetTokenTable.expiry
    var userId by PasswordResetTokenTable.userId
    var user by UserEntity referencedOn PasswordResetTokenTable.userId
}
