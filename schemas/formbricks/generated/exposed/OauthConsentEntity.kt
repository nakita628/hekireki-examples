package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class OauthConsentEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OauthConsentEntity>(OauthConsentTable)

    var clientId by OauthConsentTable.clientId
    var client by OauthClientEntity referencedOn OauthConsentTable.clientId
    var userId by OauthConsentTable.userId
    var user by UserEntity optionalReferencedOn OauthConsentTable.userId
    var referenceId by OauthConsentTable.referenceId
    var scopes by OauthConsentTable.scopes
    var createdAt by OauthConsentTable.createdAt
    var updatedAt by OauthConsentTable.updatedAt
    var resources by OauthConsentTable.resources
    var requestedUserInfoClaims by OauthConsentTable.requestedUserInfoClaims
}
