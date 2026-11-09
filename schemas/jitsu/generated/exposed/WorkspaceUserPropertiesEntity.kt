package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class WorkspaceUserPropertiesEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<WorkspaceUserPropertiesEntity>(WorkspaceUserPropertiesTable)

    val workspaceId by WorkspaceUserPropertiesTable.workspaceId
    val workspace by WorkspaceEntity referencedOn WorkspaceUserPropertiesTable.workspaceId
    val userId by WorkspaceUserPropertiesTable.userId
    val user by UserProfileEntity referencedOn WorkspaceUserPropertiesTable.userId
    var lastUsed by WorkspaceUserPropertiesTable.lastUsed
}
