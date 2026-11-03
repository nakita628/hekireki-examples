package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class TeamEmailVerificationEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, TeamEmailVerificationEntity>(TeamEmailVerificationTable)

    var name by TeamEmailVerificationTable.name
    var email by TeamEmailVerificationTable.email
    var token by TeamEmailVerificationTable.token
    var completed by TeamEmailVerificationTable.completed
    var expiresAt by TeamEmailVerificationTable.expiresAt
    var createdAt by TeamEmailVerificationTable.createdAt
    val team by TeamEntity referencedOn TeamEmailVerificationTable.id
}
