package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ScimManagedCredentialEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ScimManagedCredentialEntity>(ScimManagedCredentialTable)

    var connectionRecordId by ScimManagedCredentialTable.connectionRecordId
    var scimmanagedconnection by ScimManagedConnectionEntity referencedOn ScimManagedCredentialTable.connectionRecordId
    var credentialId by ScimManagedCredentialTable.credentialId
    var tokenDigest by ScimManagedCredentialTable.tokenDigest
    var hashVersion by ScimManagedCredentialTable.hashVersion
    var activeSlotKey by ScimManagedCredentialTable.activeSlotKey
    var status by ScimManagedCredentialTable.status
    var serializedScopes by ScimManagedCredentialTable.serializedScopes
    var expiresAt by ScimManagedCredentialTable.expiresAt
    var createdAt by ScimManagedCredentialTable.createdAt
    var createdBy by ScimManagedCredentialTable.createdBy
    var lastUsedAt by ScimManagedCredentialTable.lastUsedAt
    var revokedAt by ScimManagedCredentialTable.revokedAt
    var revokedBy by ScimManagedCredentialTable.revokedBy
    var decommissionedAt by ScimManagedCredentialTable.decommissionedAt
}
