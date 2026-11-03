package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class AvatarImageEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AvatarImageEntity>(AvatarImageTable)

    var bytes by AvatarImageTable.bytes
    val team by TeamEntity optionalReferrersOn TeamTable.avatarImageId
    val user by UserEntity optionalReferrersOn UserTable.avatarImageId
    val organisation by OrganisationEntity optionalReferrersOn OrganisationTable.avatarImageId
}
