package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class SigningKeyEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SigningKeyEntity>(SigningKeyTable)

    var organizationId by SigningKeyTable.organizationId
    var projectId by SigningKeyTable.projectId
    var name by SigningKeyTable.name
    var publicId by SigningKeyTable.publicId
    var secretCiphertext by SigningKeyTable.secretCiphertext
    var createdByUserId by SigningKeyTable.createdByUserId
    var createdAt by SigningKeyTable.createdAt
    var rotatedAt by SigningKeyTable.rotatedAt
    var verifiesUntil by SigningKeyTable.verifiesUntil
    var revokedAt by SigningKeyTable.revokedAt
    var lastUsedAt by SigningKeyTable.lastUsedAt
}
