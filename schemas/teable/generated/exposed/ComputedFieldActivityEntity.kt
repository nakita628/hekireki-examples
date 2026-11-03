package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ComputedFieldActivityEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ComputedFieldActivityEntity>(ComputedFieldActivityTable)

    var tableId by ComputedFieldActivityTable.tableId
    var baseId by ComputedFieldActivityTable.baseId
    var status by ComputedFieldActivityTable.status
    var activeTaskCount by ComputedFieldActivityTable.activeTaskCount
    var processingTaskCount by ComputedFieldActivityTable.processingTaskCount
    var generation by ComputedFieldActivityTable.generation
    var estimatedComplexity by ComputedFieldActivityTable.estimatedComplexity
    var estimatedDirtyRecords by ComputedFieldActivityTable.estimatedDirtyRecords
    var hasAllTargetRecords by ComputedFieldActivityTable.hasAllTargetRecords
    var queuedAt by ComputedFieldActivityTable.queuedAt
    var startedAt by ComputedFieldActivityTable.startedAt
    var lastCompletedAt by ComputedFieldActivityTable.lastCompletedAt
    var lastDurationMs by ComputedFieldActivityTable.lastDurationMs
    var lastError by ComputedFieldActivityTable.lastError
    var extensions by ComputedFieldActivityTable.extensions
    var updatedAt by ComputedFieldActivityTable.updatedAt
}
