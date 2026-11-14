package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class GrantEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, GrantEntity>(GrantTable)

    var organizationId by GrantTable.organizationId
    var principalType by GrantTable.principalType
    var principalId by GrantTable.principalId
    var roleKey by GrantTable.roleKey
    var legacyRole by GrantTable.legacyRole
    var source by GrantTable.source1
    var scopeType by GrantTable.scopeType
    var scopeId by GrantTable.scopeId
    var token by GrantTable.token
    var permission by GrantTable.permission
    var resourceKind by GrantTable.resourceKind
    var projectId by GrantTable.projectId
    var createdByUserId by GrantTable.createdByUserId
    var expiresAt by GrantTable.expiresAt
    var maxViews by GrantTable.maxViews
    var condition by GrantTable.condition
    var replacesGrantId by GrantTable.replacesGrantId
    var occurredAt by GrantTable.occurredAt
    var createdAt by GrantTable.createdAt
    var updatedAt by GrantTable.updatedAt
    var revokedAt by GrantTable.revokedAt
    var revokedReason by GrantTable.revokedReason

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(GrantTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
