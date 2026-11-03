package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class UserDataroomEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<UserDataroomEntity>(UserDataroomTable)

    val userId by UserDataroomTable.userId
    var teamId by UserDataroomTable.teamId
    val dataroomId by UserDataroomTable.dataroomId
    val user by UserEntity referencedOn UserDataroomTable.userId
    var team by TeamEntity referencedOn UserDataroomTable.teamId
    val dataroom by DataroomEntity referencedOn UserDataroomTable.dataroomId
    var createdAt by UserDataroomTable.createdAt
}
