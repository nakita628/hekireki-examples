package typebot.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import java.time.Instant
import java.time.temporal.ChronoUnit

class MemberInWorkspaceEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<MemberInWorkspaceEntity>(MemberInWorkspaceTable)

    var createdAt by MemberInWorkspaceTable.createdAt
    var updatedAt by MemberInWorkspaceTable.updatedAt
    val userId by MemberInWorkspaceTable.userId
    val workspaceId by MemberInWorkspaceTable.workspaceId
    var role by MemberInWorkspaceTable.role
    val user by UserEntity referencedOn MemberInWorkspaceTable.userId
    val workspace by WorkspaceEntity referencedOn MemberInWorkspaceTable.workspaceId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(MemberInWorkspaceTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
