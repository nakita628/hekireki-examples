package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class MastraMcpClientVersionsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MastraMcpClientVersionsEntity>(MastraMcpClientVersionsTable)

    var mcpClientId by MastraMcpClientVersionsTable.mcpClientId
    var versionNumber by MastraMcpClientVersionsTable.versionNumber
    var name by MastraMcpClientVersionsTable.name
    var description by MastraMcpClientVersionsTable.description
    var servers by MastraMcpClientVersionsTable.servers
    var changedFields by MastraMcpClientVersionsTable.changedFields
    var changeMessage by MastraMcpClientVersionsTable.changeMessage
    var createdAt by MastraMcpClientVersionsTable.createdAt
    var createdAtZ by MastraMcpClientVersionsTable.createdAtZ
}
