package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class RoleEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, RoleEntity>(RoleTable)

    var organizationId by RoleTable.organizationId
    var name by RoleTable.name
    var description by RoleTable.description
    var permissions by RoleTable.permissions
    var kind by RoleTable.kind
    var occurredAt by RoleTable.occurredAt
    var createdAt by RoleTable.createdAt
    var updatedAt by RoleTable.updatedAt
    var deletedAt by RoleTable.deletedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(RoleTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
