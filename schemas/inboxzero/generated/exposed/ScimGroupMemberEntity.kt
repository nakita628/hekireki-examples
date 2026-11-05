package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ScimGroupMemberEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ScimGroupMemberEntity>(ScimGroupMemberTable)

    var connectionId by ScimGroupMemberTable.connectionId
    var groupId by ScimGroupMemberTable.groupId
    var scimGroup by ScimGroupEntity referencedOn ScimGroupMemberTable.groupId
    var scimUserId by ScimGroupMemberTable.scimUserId
    var scimUser by ScimUserEntity referencedOn ScimGroupMemberTable.scimUserId
    var membershipKey by ScimGroupMemberTable.membershipKey
    var createdAt by ScimGroupMemberTable.createdAt
}
