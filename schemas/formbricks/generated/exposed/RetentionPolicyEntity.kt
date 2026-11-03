package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class RetentionPolicyEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, RetentionPolicyEntity>(RetentionPolicyTable)

    var createdAt by RetentionPolicyTable.createdAt
    var updatedAt by RetentionPolicyTable.updatedAt
    var organization by OrganizationEntity referencedOn RetentionPolicyTable.organizationId
    var organizationId by RetentionPolicyTable.organizationId
    var entity by RetentionPolicyTable.entity
    var enabled by RetentionPolicyTable.enabled
    var enabledAt by RetentionPolicyTable.enabledAt
    var warnDays by RetentionPolicyTable.warnDays
    var periodDays by RetentionPolicyTable.periodDays
    var conditions by RetentionPolicyTable.conditions
    var deferredAt by RetentionPolicyTable.deferredAt
    var updatedBy by UserEntity optionalReferencedOn RetentionPolicyTable.updatedById
    var updatedById by RetentionPolicyTable.updatedById

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(RetentionPolicyTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
