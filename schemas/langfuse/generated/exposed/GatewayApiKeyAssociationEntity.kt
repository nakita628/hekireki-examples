package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class GatewayApiKeyAssociationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, GatewayApiKeyAssociationEntity>(GatewayApiKeyAssociationTable)

    val apiKey by ApiKeyEntity referencedOn GatewayApiKeyAssociationTable.id
    var metadata by GatewayApiKeyAssociationTable.metadata
    var allowedModels by GatewayApiKeyAssociationTable.allowedModels
}
