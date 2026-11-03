package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class UserOrganizationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, UserOrganizationEntity>(UserOrganizationTable)

    var userId by UserOrganizationTable.userId
    var organizationId by UserOrganizationTable.organizationId
    var disabled by UserOrganizationTable.disabled
    var role by UserOrganizationTable.role
    var createdAt by UserOrganizationTable.createdAt
    var updatedAt by UserOrganizationTable.updatedAt
    var organization by OrganizationEntity referencedOn UserOrganizationTable.organizationId
    var user by UserEntity referencedOn UserOrganizationTable.userId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(UserOrganizationTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
