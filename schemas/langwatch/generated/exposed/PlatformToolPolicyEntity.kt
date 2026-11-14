package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class PlatformToolPolicyEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, PlatformToolPolicyEntity>(PlatformToolPolicyTable)

    var organizationId by PlatformToolPolicyTable.organizationId
    var toolSlug by PlatformToolPolicyTable.toolSlug
    var allowVk by PlatformToolPolicyTable.allowVk
    var allowOtelDirect by PlatformToolPolicyTable.allowOtelDirect
    var createdAt by PlatformToolPolicyTable.createdAt
    var updatedAt by PlatformToolPolicyTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(PlatformToolPolicyTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
