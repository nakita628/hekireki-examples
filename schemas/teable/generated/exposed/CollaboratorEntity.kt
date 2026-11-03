package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class CollaboratorEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, CollaboratorEntity>(CollaboratorTable)

    var roleName by CollaboratorTable.roleName
    var resourceType by CollaboratorTable.resourceType
    var resourceId by CollaboratorTable.resourceId
    var principalId by CollaboratorTable.principalId
    var principalType by CollaboratorTable.principalType
    var createdBy by CollaboratorTable.createdBy
    var createdTime by CollaboratorTable.createdTime
    var lastModifiedTime by CollaboratorTable.lastModifiedTime
    var lastModifiedBy by CollaboratorTable.lastModifiedBy

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(CollaboratorTable.lastModifiedTime)) {
                lastModifiedTime = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
