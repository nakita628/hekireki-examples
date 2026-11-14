package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class VirtualKeyScopeEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, VirtualKeyScopeEntity>(VirtualKeyScopeTable)

    var virtualKeyId by VirtualKeyScopeTable.virtualKeyId
    var scopeType by VirtualKeyScopeTable.scopeType
    var scopeId by VirtualKeyScopeTable.scopeId
    var createdAt by VirtualKeyScopeTable.createdAt
}
