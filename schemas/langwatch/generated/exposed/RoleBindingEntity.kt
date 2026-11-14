package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class RoleBindingEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, RoleBindingEntity>(RoleBindingTable)

    var organizationId by RoleBindingTable.organizationId
    var userId by RoleBindingTable.userId
    var groupId by RoleBindingTable.groupId
    var apiKeyId by RoleBindingTable.apiKeyId
    var role by RoleBindingTable.role
    var customRoleId by RoleBindingTable.customRoleId
    var scopeType by RoleBindingTable.scopeType
    var scopeId by RoleBindingTable.scopeId
    var createdAt by RoleBindingTable.createdAt
    var updatedAt by RoleBindingTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(RoleBindingTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
