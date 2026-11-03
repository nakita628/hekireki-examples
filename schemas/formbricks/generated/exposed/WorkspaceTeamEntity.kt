package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import java.time.Instant
import java.time.temporal.ChronoUnit

class WorkspaceTeamEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<WorkspaceTeamEntity>(WorkspaceTeamTable)

    var createdAt by WorkspaceTeamTable.createdAt
    var updatedAt by WorkspaceTeamTable.updatedAt
    val workspaceId by WorkspaceTeamTable.workspaceId
    val workspace by WorkspaceEntity referencedOn WorkspaceTeamTable.workspaceId
    val teamId by WorkspaceTeamTable.teamId
    val team by TeamEntity referencedOn WorkspaceTeamTable.teamId
    var permission by WorkspaceTeamTable.permission

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(WorkspaceTeamTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
