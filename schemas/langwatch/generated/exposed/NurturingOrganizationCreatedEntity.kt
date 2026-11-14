package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class NurturingOrganizationCreatedEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, NurturingOrganizationCreatedEntity>(NurturingOrganizationCreatedTable)

    var createdAt by NurturingOrganizationCreatedTable.createdAt
}
