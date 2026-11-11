package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LiteLLMAuditLogEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMAuditLogEntity>(LiteLLMAuditLogTable)

    var updatedAt by LiteLLMAuditLogTable.updatedAt
    var changedBy by LiteLLMAuditLogTable.changedBy
    var changedByApiKey by LiteLLMAuditLogTable.changedByApiKey
    var action by LiteLLMAuditLogTable.action
    var tableName by LiteLLMAuditLogTable.tableName1
    var objectId by LiteLLMAuditLogTable.objectId
    var beforeValue by LiteLLMAuditLogTable.beforeValue
    var updatedValues by LiteLLMAuditLogTable.updatedValues
}
