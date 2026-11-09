package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ReprocessingJobsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ReprocessingJobsEntity>(ReprocessingJobsTable)

    var config by ReprocessingJobsTable.config
    var status by ReprocessingJobsTable.status
    var createdAt by ReprocessingJobsTable.createdAt
    var startedAt by ReprocessingJobsTable.startedAt
    var completedAt by ReprocessingJobsTable.completedAt
    var k8sJobName by ReprocessingJobsTable.k8sJobName
    var totalFiles by ReprocessingJobsTable.totalFiles
    var totalWorkers by ReprocessingJobsTable.totalWorkers
    var error by ReprocessingJobsTable.error
    val reprocessingWorkers by ReprocessingWorkersEntity referrersOn ReprocessingWorkersTable.jobId
}
