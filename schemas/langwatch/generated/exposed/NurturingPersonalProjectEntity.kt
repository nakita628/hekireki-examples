package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class NurturingPersonalProjectEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, NurturingPersonalProjectEntity>(NurturingPersonalProjectTable)

    var organizationId by NurturingPersonalProjectTable.organizationId
    var ownerUserId by NurturingPersonalProjectTable.ownerUserId
}
