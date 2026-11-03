package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class MastraWorkflowSnapshotEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<MastraWorkflowSnapshotEntity>(MastraWorkflowSnapshotTable)

    val workflowName by MastraWorkflowSnapshotTable.workflowName
    val runId by MastraWorkflowSnapshotTable.runId
    var resourceId by MastraWorkflowSnapshotTable.resourceId
    var snapshot by MastraWorkflowSnapshotTable.snapshot
    var createdAt by MastraWorkflowSnapshotTable.createdAt
    var updatedAt by MastraWorkflowSnapshotTable.updatedAt
    var createdAtZ by MastraWorkflowSnapshotTable.createdAtZ
    var updatedAtZ by MastraWorkflowSnapshotTable.updatedAtZ
}
