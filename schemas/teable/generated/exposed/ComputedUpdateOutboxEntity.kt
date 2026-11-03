package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ComputedUpdateOutboxEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ComputedUpdateOutboxEntity>(ComputedUpdateOutboxTable)

    var baseId by ComputedUpdateOutboxTable.baseId
    var seedTableId by ComputedUpdateOutboxTable.seedTableId
    var seedRecordIds by ComputedUpdateOutboxTable.seedRecordIds
    var changeType by ComputedUpdateOutboxTable.changeType
    var steps by ComputedUpdateOutboxTable.steps
    var edges by ComputedUpdateOutboxTable.edges
    var status by ComputedUpdateOutboxTable.status
    var attempts by ComputedUpdateOutboxTable.attempts
    var maxAttempts by ComputedUpdateOutboxTable.maxAttempts
    var nextRunAt by ComputedUpdateOutboxTable.nextRunAt
    var lockedAt by ComputedUpdateOutboxTable.lockedAt
    var lockedBy by ComputedUpdateOutboxTable.lockedBy
    var lastError by ComputedUpdateOutboxTable.lastError
    var estimatedComplexity by ComputedUpdateOutboxTable.estimatedComplexity
    var planHash by ComputedUpdateOutboxTable.planHash
    var dirtyStats by ComputedUpdateOutboxTable.dirtyStats
    var runId by ComputedUpdateOutboxTable.runId
    var originRunIds by ComputedUpdateOutboxTable.originRunIds
    var runTotalSteps by ComputedUpdateOutboxTable.runTotalSteps
    var runCompletedStepsBefore by ComputedUpdateOutboxTable.runCompletedStepsBefore
    var affectedTableIds by ComputedUpdateOutboxTable.affectedTableIds
    var affectedFieldIds by ComputedUpdateOutboxTable.affectedFieldIds
    var syncMaxLevel by ComputedUpdateOutboxTable.syncMaxLevel
    var sourceChangedAt by ComputedUpdateOutboxTable.sourceChangedAt
    var stageDepth by ComputedUpdateOutboxTable.stageDepth
    var predecessorTaskId by ComputedUpdateOutboxTable.predecessorTaskId
    var createdAt by ComputedUpdateOutboxTable.createdAt
    var updatedAt by ComputedUpdateOutboxTable.updatedAt
    val seeds by ComputedUpdateOutboxSeedEntity referrersOn ComputedUpdateOutboxSeedTable.taskId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ComputedUpdateOutboxTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
