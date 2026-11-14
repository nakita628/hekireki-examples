package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ScimSsoConnectionViewEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ScimSsoConnectionViewEntity>(ScimSsoConnectionViewTable)

    var organizationId by ScimSsoConnectionViewTable.organizationId
    var folded by ScimSsoConnectionViewTable.folded
    var appliedEventIds by ScimSsoConnectionViewTable.appliedEventIds
    var projectionVersion by ScimSsoConnectionViewTable.projectionVersion
    var createdAt by ScimSsoConnectionViewTable.createdAt
    var updatedAt by ScimSsoConnectionViewTable.updatedAt
}
