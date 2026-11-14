package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class VirtualKeyEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, VirtualKeyEntity>(VirtualKeyTable)

    var organizationId by VirtualKeyTable.organizationId
    var name by VirtualKeyTable.name
    var description by VirtualKeyTable.description
    var status by VirtualKeyTable.status
    var purpose by VirtualKeyTable.purpose
    var externalId by VirtualKeyTable.externalId
    var metadata by VirtualKeyTable.metadata
    var disabledAt by VirtualKeyTable.disabledAt
    var disabledReason by VirtualKeyTable.disabledReason
    var expiresAt by VirtualKeyTable.expiresAt
    var hashedSecret by VirtualKeyTable.hashedSecret
    var displayPrefix by VirtualKeyTable.displayPrefix
    var principalUserId by VirtualKeyTable.principalUserId
    var traceProjectId by VirtualKeyTable.traceProjectId
    var config by VirtualKeyTable.config
    var revision by VirtualKeyTable.revision
    var previousHashedSecret by VirtualKeyTable.previousHashedSecret
    var previousSecretValidUntil by VirtualKeyTable.previousSecretValidUntil
    var revokedAt by VirtualKeyTable.revokedAt
    var revokedById by VirtualKeyTable.revokedById
    var createdAt by VirtualKeyTable.createdAt
    var updatedAt by VirtualKeyTable.updatedAt
    var createdById by VirtualKeyTable.createdById
    var lastUsedAt by VirtualKeyTable.lastUsedAt
    var routingPolicyId by VirtualKeyTable.routingPolicyId
    var routingMode by VirtualKeyTable.routingMode
    var connectServices by VirtualKeyTable.connectServices
    var licenseTokenHash by VirtualKeyTable.licenseTokenHash
    var licenseInstanceId by VirtualKeyTable.licenseInstanceId
    var licenseExpiresAt by VirtualKeyTable.licenseExpiresAt
    var licenseId by VirtualKeyTable.licenseId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(VirtualKeyTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
