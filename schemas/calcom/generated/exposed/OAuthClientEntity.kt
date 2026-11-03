package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class OAuthClientEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OAuthClientEntity>(OAuthClientTable)

    var redirectUri by OAuthClientTable.redirectUri
    var clientSecret by OAuthClientTable.clientSecret
    var clientType by OAuthClientTable.clientType
    var name by OAuthClientTable.name
    var purpose by OAuthClientTable.purpose
    var logo by OAuthClientTable.logo
    var websiteUrl by OAuthClientTable.websiteUrl
    var rejectionReason by OAuthClientTable.rejectionReason
    var isTrusted by OAuthClientTable.isTrusted
    val accessCodes by AccessCodeEntity optionalReferrersOn AccessCodeTable.clientId
    var status by OAuthClientTable.status
    var userId by OAuthClientTable.userId
    var user by UserEntity optionalReferencedOn OAuthClientTable.userId
    var createdAt by OAuthClientTable.createdAt
}
