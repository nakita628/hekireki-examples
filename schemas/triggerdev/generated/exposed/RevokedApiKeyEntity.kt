package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class RevokedApiKeyEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, RevokedApiKeyEntity>(RevokedApiKeyTable)

    var apiKey by RevokedApiKeyTable.apiKey
    var runtimeEnvironment by RuntimeEnvironmentEntity referencedOn RevokedApiKeyTable.runtimeEnvironmentId
    var runtimeEnvironmentId by RevokedApiKeyTable.runtimeEnvironmentId
    var expiresAt by RevokedApiKeyTable.expiresAt
    var createdAt by RevokedApiKeyTable.createdAt
}
