package langwatch.models

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

    var organizationId by RetentionPolicyTable.organizationId
    var scopeType by RetentionPolicyTable.scopeType
    var scopeId by RetentionPolicyTable.scopeId
    var category by RetentionPolicyTable.category
    var retentionDays by RetentionPolicyTable.retentionDays
    var createdAt by RetentionPolicyTable.createdAt
    var updatedAt by RetentionPolicyTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(RetentionPolicyTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
