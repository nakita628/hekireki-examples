package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class WorkspaceOptionsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, WorkspaceOptionsEntity>(WorkspaceOptionsTable)

    var createdAt by WorkspaceOptionsTable.createdAt
    var updatedAt by WorkspaceOptionsTable.updatedAt
    var workspaceId by WorkspaceOptionsTable.workspaceId
    var workspace by WorkspaceEntity referencedOn WorkspaceOptionsTable.workspaceId
    var namespace by WorkspaceOptionsTable.namespace
    var value by WorkspaceOptionsTable.value

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(WorkspaceOptionsTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
