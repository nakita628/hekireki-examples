package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ScimRequestLogEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ScimRequestLogEntity>(ScimRequestLogTable)

    var organizationId by ScimRequestLogTable.organizationId
    var connectionId by ScimRequestLogTable.connectionId
    var method by ScimRequestLogTable.method
    var resource by ScimRequestLogTable.resource
    var status by ScimRequestLogTable.status
    var reason by ScimRequestLogTable.reason
    var detail by ScimRequestLogTable.detail
    var occurredAt by ScimRequestLogTable.occurredAt
}
