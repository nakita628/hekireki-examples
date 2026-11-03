package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class RoleAssignmentEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, RoleAssignmentEntity>(RoleAssignmentTable)

    var ownerId by RoleAssignmentTable.ownerId
    var projectId by RoleAssignmentTable.projectId
    var project by ProjectEntity optionalReferencedOn RoleAssignmentTable.projectId
    var orgId by RoleAssignmentTable.orgId
    var organization by OrganizationEntity referencedOn RoleAssignmentTable.orgId
    var principalId by RoleAssignmentTable.principalId
    var userId by RoleAssignmentTable.userId
    var user by UserEntity optionalReferencedOn RoleAssignmentTable.userId
    var apiKeyId by RoleAssignmentTable.apiKeyId
    var apiKey by ApiKeyEntity optionalReferencedOn RoleAssignmentTable.apiKeyId
    var roleId by RoleAssignmentTable.roleId
    var systemRole by RoleAssignmentTable.systemRole
    var createdAt by RoleAssignmentTable.createdAt
    var updatedAt by RoleAssignmentTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(RoleAssignmentTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
