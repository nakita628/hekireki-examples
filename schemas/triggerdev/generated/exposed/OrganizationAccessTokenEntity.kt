package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class OrganizationAccessTokenEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OrganizationAccessTokenEntity>(OrganizationAccessTokenTable)

    var name by OrganizationAccessTokenTable.name
    var type by OrganizationAccessTokenTable.type
    var hashedToken by OrganizationAccessTokenTable.hashedToken
    var organization by OrganizationEntity referencedOn OrganizationAccessTokenTable.organizationId
    var organizationId by OrganizationAccessTokenTable.organizationId
    var expiresAt by OrganizationAccessTokenTable.expiresAt
    var revokedAt by OrganizationAccessTokenTable.revokedAt
    var lastAccessedAt by OrganizationAccessTokenTable.lastAccessedAt
    var createdAt by OrganizationAccessTokenTable.createdAt
    var updatedAt by OrganizationAccessTokenTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(OrganizationAccessTokenTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
