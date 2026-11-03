package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class MastraMcpServerVersionsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MastraMcpServerVersionsEntity>(MastraMcpServerVersionsTable)

    var mcpServerId by MastraMcpServerVersionsTable.mcpServerId
    var versionNumber by MastraMcpServerVersionsTable.versionNumber
    var name by MastraMcpServerVersionsTable.name
    var version by MastraMcpServerVersionsTable.version
    var description by MastraMcpServerVersionsTable.description
    var instructions by MastraMcpServerVersionsTable.instructions
    var repository by MastraMcpServerVersionsTable.repository
    var releaseDate by MastraMcpServerVersionsTable.releaseDate
    var isLatest by MastraMcpServerVersionsTable.isLatest
    var packageCanonical by MastraMcpServerVersionsTable.packageCanonical
    var tools by MastraMcpServerVersionsTable.tools
    var agents by MastraMcpServerVersionsTable.agents
    var workflows by MastraMcpServerVersionsTable.workflows
    var changedFields by MastraMcpServerVersionsTable.changedFields
    var changeMessage by MastraMcpServerVersionsTable.changeMessage
    var createdAt by MastraMcpServerVersionsTable.createdAt
    var createdAtZ by MastraMcpServerVersionsTable.createdAtZ
}
