package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class MastraThreadsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MastraThreadsEntity>(MastraThreadsTable)

    var resourceId by MastraThreadsTable.resourceId
    var title by MastraThreadsTable.title
    var metadata by MastraThreadsTable.metadata
    var createdAt by MastraThreadsTable.createdAt
    var updatedAt by MastraThreadsTable.updatedAt
    var createdAtZ by MastraThreadsTable.createdAtZ
    var updatedAtZ by MastraThreadsTable.updatedAtZ
}
