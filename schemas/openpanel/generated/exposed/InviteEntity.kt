package openpanel.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class InviteEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, InviteEntity>(InviteTable)

    var email by InviteTable.email
    var createdBy by UserEntity referencedOn InviteTable.createdById
    var createdById by InviteTable.createdById
    var organizationId by InviteTable.organizationId
    var organization by OrganizationEntity referencedOn InviteTable.organizationId
    var projectAccess by InviteTable.projectAccess
    var projectScope by InviteTable.projectScope
    var expiresAt by InviteTable.expiresAt
    var createdAt by InviteTable.createdAt
    var updatedAt by InviteTable.updatedAt
    var role by InviteTable.role

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(InviteTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
