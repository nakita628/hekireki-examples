package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class ReprocessingWorkersEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<ReprocessingWorkersEntity>(ReprocessingWorkersTable)

    val jobId by ReprocessingWorkersTable.jobId
    val workerIndex by ReprocessingWorkersTable.workerIndex
    var status by ReprocessingWorkersTable.status
    var startedAt by ReprocessingWorkersTable.startedAt
    var updatedAt by ReprocessingWorkersTable.updatedAt
    var completedAt by ReprocessingWorkersTable.completedAt
    var currentFile by ReprocessingWorkersTable.currentFile
    var currentLine by ReprocessingWorkersTable.currentLine
    var currentTs by ReprocessingWorkersTable.currentTs
    var assignedFiles by ReprocessingWorkersTable.assignedFiles
    var processedFiles by ReprocessingWorkersTable.processedFiles
    var totalLines by ReprocessingWorkersTable.totalLines
    var successCount by ReprocessingWorkersTable.successCount
    var errorCount by ReprocessingWorkersTable.errorCount
    var skippedCount by ReprocessingWorkersTable.skippedCount
    var totalBytes by ReprocessingWorkersTable.totalBytes
    var processedBytes by ReprocessingWorkersTable.processedBytes
    var error by ReprocessingWorkersTable.error
    val job by ReprocessingJobsEntity referencedOn ReprocessingWorkersTable.jobId
}
