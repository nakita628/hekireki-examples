package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ModelDefaultConfigScopeEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ModelDefaultConfigScopeEntity>(ModelDefaultConfigScopeTable)

    var configId by ModelDefaultConfigScopeTable.configId
    var scopeType by ModelDefaultConfigScopeTable.scopeType
    var scopeId by ModelDefaultConfigScopeTable.scopeId
    var createdAt by ModelDefaultConfigScopeTable.createdAt
}
