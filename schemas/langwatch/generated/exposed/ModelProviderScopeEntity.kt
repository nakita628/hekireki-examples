package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ModelProviderScopeEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ModelProviderScopeEntity>(ModelProviderScopeTable)

    var modelProviderId by ModelProviderScopeTable.modelProviderId
    var scopeType by ModelProviderScopeTable.scopeType
    var scopeId by ModelProviderScopeTable.scopeId
    var createdAt by ModelProviderScopeTable.createdAt
}
