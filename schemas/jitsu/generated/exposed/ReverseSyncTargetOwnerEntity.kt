package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ReverseSyncTargetOwnerEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ReverseSyncTargetOwnerEntity>(ReverseSyncTargetOwnerTable)

    var workspaceId by ReverseSyncTargetOwnerTable.workspaceId
    var syncId by ReverseSyncTargetOwnerTable.syncId
}
