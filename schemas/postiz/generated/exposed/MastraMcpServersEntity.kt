package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class MastraMcpServersEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MastraMcpServersEntity>(MastraMcpServersTable)

    var status by MastraMcpServersTable.status
    var activeVersionId by MastraMcpServersTable.activeVersionId
    var authorId by MastraMcpServersTable.authorId
    var metadata by MastraMcpServersTable.metadata
    var createdAt by MastraMcpServersTable.createdAt
    var updatedAt by MastraMcpServersTable.updatedAt
    var createdAtZ by MastraMcpServersTable.createdAtZ
    var updatedAtZ by MastraMcpServersTable.updatedAtZ
}
