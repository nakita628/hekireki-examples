package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class WorkerInstanceGroupEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, WorkerInstanceGroupEntity>(WorkerInstanceGroupTable)

    var type by WorkerInstanceGroupTable.type
    var name by WorkerInstanceGroupTable.name
    var masterQueue by WorkerInstanceGroupTable.masterQueue
    var description by WorkerInstanceGroupTable.description
    var hidden by WorkerInstanceGroupTable.hidden
    var token by WorkerGroupTokenEntity referencedOn WorkerInstanceGroupTable.tokenId
    var tokenId by WorkerInstanceGroupTable.tokenId
    val workers by WorkerInstanceEntity referrersOn WorkerInstanceTable.workerGroupId
    val backgroundWorkers by BackgroundWorkerEntity optionalReferrersOn BackgroundWorkerTable.workerGroupId
    val defaultForProjects by ProjectEntity optionalReferrersOn ProjectTable.defaultWorkerGroupId
    var organization by OrganizationEntity optionalReferencedOn WorkerInstanceGroupTable.organizationId
    var organizationId by WorkerInstanceGroupTable.organizationId
    var project by ProjectEntity optionalReferencedOn WorkerInstanceGroupTable.projectId
    var projectId by WorkerInstanceGroupTable.projectId
    var cloudProvider by WorkerInstanceGroupTable.cloudProvider
    var location by WorkerInstanceGroupTable.location
    var staticIPs by WorkerInstanceGroupTable.staticIPs
    var workloadType by WorkerInstanceGroupTable.workloadType
    var region by WorkerInstanceGroupTable.region
    var enableFastPath by WorkerInstanceGroupTable.enableFastPath
    var createdAt by WorkerInstanceGroupTable.createdAt
    var updatedAt by WorkerInstanceGroupTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(WorkerInstanceGroupTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
