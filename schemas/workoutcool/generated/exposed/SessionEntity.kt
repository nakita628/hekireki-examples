package workoutcool.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class SessionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SessionEntity>(SessionTable)

    var expiresAt by SessionTable.expiresAt
    var token by SessionTable.token
    var createdAt by SessionTable.createdAt
    var updatedAt by SessionTable.updatedAt
    var ipAddress by SessionTable.ipAddress
    var userAgent by SessionTable.userAgent
    var userId by SessionTable.userId
    var user by UserEntity referencedOn SessionTable.userId
    var impersonatedBy by SessionTable.impersonatedBy
}
