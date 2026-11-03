package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class MastraResourcesEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MastraResourcesEntity>(MastraResourcesTable)

    var workingMemory by MastraResourcesTable.workingMemory
    var metadata by MastraResourcesTable.metadata
    var createdAt by MastraResourcesTable.createdAt
    var updatedAt by MastraResourcesTable.updatedAt
    var createdAtZ by MastraResourcesTable.createdAtZ
    var updatedAtZ by MastraResourcesTable.updatedAtZ
}
