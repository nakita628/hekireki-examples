package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class NurturingProjectUsageViewEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<NurturingProjectUsageViewEntity>(NurturingProjectUsageViewTable)

    val projectId by NurturingProjectUsageViewTable.projectId
    val owner by NurturingProjectUsageViewTable.owner
    var folded by NurturingProjectUsageViewTable.folded
    var appliedEventIds by NurturingProjectUsageViewTable.appliedEventIds
    var projectionVersion by NurturingProjectUsageViewTable.projectionVersion
    var updatedAt by NurturingProjectUsageViewTable.updatedAt
}
