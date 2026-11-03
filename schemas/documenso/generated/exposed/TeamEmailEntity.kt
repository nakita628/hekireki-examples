package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class TeamEmailEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, TeamEmailEntity>(TeamEmailTable)

    var createdAt by TeamEmailTable.createdAt
    var name by TeamEmailTable.name
    var email by TeamEmailTable.email
    val team by TeamEntity referencedOn TeamEmailTable.id
}
