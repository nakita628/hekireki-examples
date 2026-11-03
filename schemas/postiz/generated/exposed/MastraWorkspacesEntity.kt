package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class MastraWorkspacesEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MastraWorkspacesEntity>(MastraWorkspacesTable)

    var status by MastraWorkspacesTable.status
    var activeVersionId by MastraWorkspacesTable.activeVersionId
    var authorId by MastraWorkspacesTable.authorId
    var metadata by MastraWorkspacesTable.metadata
    var createdAt by MastraWorkspacesTable.createdAt
    var updatedAt by MastraWorkspacesTable.updatedAt
    var createdAtZ by MastraWorkspacesTable.createdAtZ
    var updatedAtZ by MastraWorkspacesTable.updatedAtZ
}
