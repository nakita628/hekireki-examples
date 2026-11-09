package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import java.time.Instant
import java.time.temporal.ChronoUnit

class WorkspaceAccessEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<WorkspaceAccessEntity>(WorkspaceAccessTable)

    val workspaceId by WorkspaceAccessTable.workspaceId
    var createdAt by WorkspaceAccessTable.createdAt
    var updatedAt by WorkspaceAccessTable.updatedAt
    val userId by WorkspaceAccessTable.userId
    val workspace by WorkspaceEntity referencedOn WorkspaceAccessTable.workspaceId
    val user by UserProfileEntity referencedOn WorkspaceAccessTable.userId
    var role by WorkspaceAccessTable.role

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(WorkspaceAccessTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
