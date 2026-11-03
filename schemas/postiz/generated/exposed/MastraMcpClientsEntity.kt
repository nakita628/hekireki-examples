package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class MastraMcpClientsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MastraMcpClientsEntity>(MastraMcpClientsTable)

    var status by MastraMcpClientsTable.status
    var activeVersionId by MastraMcpClientsTable.activeVersionId
    var authorId by MastraMcpClientsTable.authorId
    var metadata by MastraMcpClientsTable.metadata
    var createdAt by MastraMcpClientsTable.createdAt
    var updatedAt by MastraMcpClientsTable.updatedAt
    var createdAtZ by MastraMcpClientsTable.createdAtZ
    var updatedAtZ by MastraMcpClientsTable.updatedAtZ
}
