package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class RetentionRunEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, RetentionRunEntity>(RetentionRunTable)

    var organization by OrganizationEntity referencedOn RetentionRunTable.organizationId
    var organizationId by RetentionRunTable.organizationId
    var entity by RetentionRunTable.entity
    var kind by RetentionRunTable.kind
    var startedAt by RetentionRunTable.startedAt
    var finishedAt by RetentionRunTable.finishedAt
    var notifiedCount by RetentionRunTable.notifiedCount
    var archivedCount by RetentionRunTable.archivedCount
    var deletedCount by RetentionRunTable.deletedCount
    var skippedCount by RetentionRunTable.skippedCount
    var hasChanges by RetentionRunTable.hasChanges
    var scanCursor by RetentionRunTable.scanCursor
    val items by RetentionRunItemEntity referrersOn RetentionRunItemTable.runId
}
