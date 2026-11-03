package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class RetentionRunItemEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, RetentionRunItemEntity>(RetentionRunItemTable)

    var run by RetentionRunEntity referencedOn RetentionRunItemTable.runId
    var runId by RetentionRunItemTable.runId
    var targetType by RetentionRunItemTable.targetType
    var targetId by RetentionRunItemTable.targetId
    var targetName by RetentionRunItemTable.targetName
    var action by RetentionRunItemTable.action
    var count by RetentionRunItemTable.count
    var recipient by RetentionRunItemTable.recipient
    var skipReason by RetentionRunItemTable.skipReason
}
