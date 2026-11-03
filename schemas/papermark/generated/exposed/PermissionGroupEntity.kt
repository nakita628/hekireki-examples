package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class PermissionGroupEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, PermissionGroupEntity>(PermissionGroupTable)

    var name by PermissionGroupTable.name
    var description by PermissionGroupTable.description
    val links by LinkEntity optionalReferrersOn LinkTable.permissionGroupId
    val accessControls by PermissionGroupAccessControlsEntity referrersOn PermissionGroupAccessControlsTable.groupId
    var dataroomId by PermissionGroupTable.dataroomId
    var dataroom by DataroomEntity referencedOn PermissionGroupTable.dataroomId
    var teamId by PermissionGroupTable.teamId
    var team by TeamEntity referencedOn PermissionGroupTable.teamId
    var createdAt by PermissionGroupTable.createdAt
    var updatedAt by PermissionGroupTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(PermissionGroupTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
