package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class EnvironmentEncryptionPolicyEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, EnvironmentEncryptionPolicyEntity>(EnvironmentEncryptionPolicyTable)

    val environment by RuntimeEnvironmentEntity referencedOn EnvironmentEncryptionPolicyTable.id
    var revision by EnvironmentEncryptionPolicyTable.revision
    var wrappingProvider by EnvironmentEncryptionPolicyTable.wrappingProvider
    var wrappingKeyReference by EnvironmentEncryptionPolicyTable.wrappingKeyReference
    var activeKeyId by EnvironmentEncryptionPolicyTable.activeKeyId
}
