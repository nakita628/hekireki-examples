package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ComputedUpdateRunHistoryEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ComputedUpdateRunHistoryEntity>(ComputedUpdateRunHistoryTable)

    var baseId by ComputedUpdateRunHistoryTable.baseId
    var seedTableId by ComputedUpdateRunHistoryTable.seedTableId
    var changeType by ComputedUpdateRunHistoryTable.changeType
    var runId by ComputedUpdateRunHistoryTable.runId
    var originRunIds by ComputedUpdateRunHistoryTable.originRunIds
    var steps by ComputedUpdateRunHistoryTable.steps
    var edges by ComputedUpdateRunHistoryTable.edges
    var affectedTableIds by ComputedUpdateRunHistoryTable.affectedTableIds
    var affectedFieldIds by ComputedUpdateRunHistoryTable.affectedFieldIds
    var sourceFieldIds by ComputedUpdateRunHistoryTable.sourceFieldIds
    var seedRecordCount by ComputedUpdateRunHistoryTable.seedRecordCount
    var stageDepth by ComputedUpdateRunHistoryTable.stageDepth
    var predecessorTaskId by ComputedUpdateRunHistoryTable.predecessorTaskId
    var runTotalSteps by ComputedUpdateRunHistoryTable.runTotalSteps
    var runCompletedStepsBefore by ComputedUpdateRunHistoryTable.runCompletedStepsBefore
    var syncMaxLevel by ComputedUpdateRunHistoryTable.syncMaxLevel
    var estimatedComplexity by ComputedUpdateRunHistoryTable.estimatedComplexity
    var attempts by ComputedUpdateRunHistoryTable.attempts
    var outcome by ComputedUpdateRunHistoryTable.outcome
    var sourceChangedAt by ComputedUpdateRunHistoryTable.sourceChangedAt
    var enqueuedAt by ComputedUpdateRunHistoryTable.enqueuedAt
    var startedAt by ComputedUpdateRunHistoryTable.startedAt
    var completedAt by ComputedUpdateRunHistoryTable.completedAt
    var durationMs by ComputedUpdateRunHistoryTable.durationMs
}
