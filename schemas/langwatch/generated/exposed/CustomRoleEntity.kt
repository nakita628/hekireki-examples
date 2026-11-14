package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class CustomRoleEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, CustomRoleEntity>(CustomRoleTable)

    var organizationId by CustomRoleTable.organizationId
    var name by CustomRoleTable.name
    var description by CustomRoleTable.description
    var permissions by CustomRoleTable.permissions
    var kind by CustomRoleTable.kind
    var createdAt by CustomRoleTable.createdAt
    var updatedAt by CustomRoleTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(CustomRoleTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
