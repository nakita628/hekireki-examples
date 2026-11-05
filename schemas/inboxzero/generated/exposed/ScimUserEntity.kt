package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ScimUserEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ScimUserEntity>(ScimUserTable)

    var connectionId by ScimUserTable.connectionId
    var provisioningDomainId by ScimUserTable.provisioningDomainId
    var userId by ScimUserTable.userId
    var user by UserEntity referencedOn ScimUserTable.userId
    var connectionUserKey by ScimUserTable.connectionUserKey
    var userName by ScimUserTable.userName
    var userNameKey by ScimUserTable.userNameKey
    var primaryEmail by ScimUserTable.primaryEmail
    var workEmailValueIndex by ScimUserTable.workEmailValueIndex
    var emailValueIndex by ScimUserTable.emailValueIndex
    var displayName by ScimUserTable.displayName
    var formattedName by ScimUserTable.formattedName
    var givenName by ScimUserTable.givenName
    var familyName by ScimUserTable.familyName
    var serializedEmails by ScimUserTable.serializedEmails
    var serializedAttributes by ScimUserTable.serializedAttributes
    var externalId by ScimUserTable.externalId
    var externalIdKey by ScimUserTable.externalIdKey
    var active by ScimUserTable.active
    var orderKey by ScimUserTable.orderKey
    var createdAt by ScimUserTable.createdAt
    var updatedAt by ScimUserTable.updatedAt
    val scimProjectionGrants by ScimProjectionGrantEntity referrersOn ScimProjectionGrantTable.scimUserId
    val scimGroupMembers by ScimGroupMemberEntity referrersOn ScimGroupMemberTable.scimUserId
}
