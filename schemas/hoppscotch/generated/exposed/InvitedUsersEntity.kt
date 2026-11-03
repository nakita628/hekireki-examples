package hoppscotch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class InvitedUsersEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, InvitedUsersEntity>(InvitedUsersTable)

    var adminUid by InvitedUsersTable.adminUid
    var adminEmail by InvitedUsersTable.adminEmail
    var invitedOn by InvitedUsersTable.invitedOn
    var user by UserEntity referencedOn InvitedUsersTable.adminUid
}
