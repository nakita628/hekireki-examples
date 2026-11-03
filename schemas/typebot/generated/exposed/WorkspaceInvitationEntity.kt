package typebot.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class WorkspaceInvitationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, WorkspaceInvitationEntity>(WorkspaceInvitationTable)

    var createdAt by WorkspaceInvitationTable.createdAt
    var updatedAt by WorkspaceInvitationTable.updatedAt
    var email by WorkspaceInvitationTable.email
    var workspaceId by WorkspaceInvitationTable.workspaceId
    var type by WorkspaceInvitationTable.type
    var workspace by WorkspaceEntity referencedOn WorkspaceInvitationTable.workspaceId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(WorkspaceInvitationTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
