package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ComputedTableActivityEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ComputedTableActivityEntity>(ComputedTableActivityTable)

    var baseId by ComputedTableActivityTable.baseId
    var status by ComputedTableActivityTable.status
    var calculatingFieldCount by ComputedTableActivityTable.calculatingFieldCount
    var queuedFieldCount by ComputedTableActivityTable.queuedFieldCount
    var estimatedComplexity by ComputedTableActivityTable.estimatedComplexity
    var recentCompletions by ComputedTableActivityTable.recentCompletions
    var generation by ComputedTableActivityTable.generation
    var updatedAt by ComputedTableActivityTable.updatedAt
}
