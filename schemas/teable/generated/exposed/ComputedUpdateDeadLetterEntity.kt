package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ComputedUpdateDeadLetterEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ComputedUpdateDeadLetterEntity>(ComputedUpdateDeadLetterTable)

    var baseId by ComputedUpdateDeadLetterTable.baseId
    var workerLane by ComputedUpdateDeadLetterTable.workerLane
    var seedTableId by ComputedUpdateDeadLetterTable.seedTableId
    var seedRecordIds by ComputedUpdateDeadLetterTable.seedRecordIds
    var changeType by ComputedUpdateDeadLetterTable.changeType
    var steps by ComputedUpdateDeadLetterTable.steps
    var edges by ComputedUpdateDeadLetterTable.edges
    var status by ComputedUpdateDeadLetterTable.status
    var attempts by ComputedUpdateDeadLetterTable.attempts
    var maxAttempts by ComputedUpdateDeadLetterTable.maxAttempts
    var nextRunAt by ComputedUpdateDeadLetterTable.nextRunAt
    var lockedAt by ComputedUpdateDeadLetterTable.lockedAt
    var lockedBy by ComputedUpdateDeadLetterTable.lockedBy
    var lastError by ComputedUpdateDeadLetterTable.lastError
    var estimatedComplexity by ComputedUpdateDeadLetterTable.estimatedComplexity
    var planHash by ComputedUpdateDeadLetterTable.planHash
    var dirtyStats by ComputedUpdateDeadLetterTable.dirtyStats
    var runId by ComputedUpdateDeadLetterTable.runId
    var originRunIds by ComputedUpdateDeadLetterTable.originRunIds
    var runTotalSteps by ComputedUpdateDeadLetterTable.runTotalSteps
    var runCompletedStepsBefore by ComputedUpdateDeadLetterTable.runCompletedStepsBefore
    var affectedTableIds by ComputedUpdateDeadLetterTable.affectedTableIds
    var affectedFieldIds by ComputedUpdateDeadLetterTable.affectedFieldIds
    var syncMaxLevel by ComputedUpdateDeadLetterTable.syncMaxLevel
    var sourceChangedAt by ComputedUpdateDeadLetterTable.sourceChangedAt
    var stageDepth by ComputedUpdateDeadLetterTable.stageDepth
    var predecessorTaskId by ComputedUpdateDeadLetterTable.predecessorTaskId
    var traceData by ComputedUpdateDeadLetterTable.traceData
    var failedAt by ComputedUpdateDeadLetterTable.failedAt
    var createdAt by ComputedUpdateDeadLetterTable.createdAt
    var updatedAt by ComputedUpdateDeadLetterTable.updatedAt
    val seeds by ComputedUpdateDeadLetterSeedEntity referrersOn ComputedUpdateDeadLetterSeedTable.taskId
}
