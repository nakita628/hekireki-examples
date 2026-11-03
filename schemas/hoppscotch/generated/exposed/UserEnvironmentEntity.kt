package hoppscotch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class UserEnvironmentEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, UserEnvironmentEntity>(UserEnvironmentTable)

    var userUid by UserEnvironmentTable.userUid
    var name by UserEnvironmentTable.name
    var variables by UserEnvironmentTable.variables
    var isGlobal by UserEnvironmentTable.isGlobal
    var user by UserEntity referencedOn UserEnvironmentTable.userUid
}
