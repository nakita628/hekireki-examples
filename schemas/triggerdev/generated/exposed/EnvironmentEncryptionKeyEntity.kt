package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class EnvironmentEncryptionKeyEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, EnvironmentEncryptionKeyEntity>(EnvironmentEncryptionKeyTable)

    var environmentId by EnvironmentEncryptionKeyTable.environmentId
    var environment by RuntimeEnvironmentEntity referencedOn EnvironmentEncryptionKeyTable.environmentId
    var wrappedKey by EnvironmentEncryptionKeyTable.wrappedKey
    var wrappingProvider by EnvironmentEncryptionKeyTable.wrappingProvider
    var wrappingKeyReference by EnvironmentEncryptionKeyTable.wrappingKeyReference
    var revision by EnvironmentEncryptionKeyTable.revision
    var createdAt by EnvironmentEncryptionKeyTable.createdAt
}
