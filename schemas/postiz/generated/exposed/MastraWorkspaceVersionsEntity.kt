package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class MastraWorkspaceVersionsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MastraWorkspaceVersionsEntity>(MastraWorkspaceVersionsTable)

    var workspaceId by MastraWorkspaceVersionsTable.workspaceId
    var versionNumber by MastraWorkspaceVersionsTable.versionNumber
    var name by MastraWorkspaceVersionsTable.name
    var description by MastraWorkspaceVersionsTable.description
    var filesystem by MastraWorkspaceVersionsTable.filesystem
    var sandbox by MastraWorkspaceVersionsTable.sandbox
    var mounts by MastraWorkspaceVersionsTable.mounts
    var search by MastraWorkspaceVersionsTable.search
    var skills by MastraWorkspaceVersionsTable.skills
    var tools by MastraWorkspaceVersionsTable.tools
    var autoSync by MastraWorkspaceVersionsTable.autoSync
    var operationTimeout by MastraWorkspaceVersionsTable.operationTimeout
    var changedFields by MastraWorkspaceVersionsTable.changedFields
    var changeMessage by MastraWorkspaceVersionsTable.changeMessage
    var createdAt by MastraWorkspaceVersionsTable.createdAt
    var createdAtZ by MastraWorkspaceVersionsTable.createdAtZ
}
