package superproductivity.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class CaptureAuthEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, CaptureAuthEntity>(CaptureAuthTable)

    var userId by CaptureAuthTable.userId
    var clientId by CaptureAuthTable.clientId
    var clientName by CaptureAuthTable.clientName
    var redirectUri by CaptureAuthTable.redirectUri
    var codeChallenge by CaptureAuthTable.codeChallenge
    var resource by CaptureAuthTable.resource
    var state by CaptureAuthTable.state
    var scope by CaptureAuthTable.scope
    var codeHash by CaptureAuthTable.codeHash
    var tokenVersion by CaptureAuthTable.tokenVersion
    var expiresAt by CaptureAuthTable.expiresAt
    var user by UserEntity optionalReferencedOn CaptureAuthTable.userId
}
