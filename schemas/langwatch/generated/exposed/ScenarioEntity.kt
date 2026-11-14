package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ScenarioEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ScenarioEntity>(ScenarioTable)

    var projectId by ScenarioTable.projectId
    var name by ScenarioTable.name
    var situation by ScenarioTable.situation
    var criteria by ScenarioTable.criteria
    var labels by ScenarioTable.labels
    var parameters by ScenarioTable.parameters
    var simulatorModel by ScenarioTable.simulatorModel
    var judgeModel by ScenarioTable.judgeModel
    var maxTurns by ScenarioTable.maxTurns
    var minTurns by ScenarioTable.minTurns
    var fields by ScenarioTable.fields1
    var callerVoice by ScenarioTable.callerVoice
    var testSuiteId by ScenarioTable.testSuiteId
    var version by ScenarioTable.version
    var lastUpdatedById by ScenarioTable.lastUpdatedById
    var archivedAt by ScenarioTable.archivedAt
    var createdAt by ScenarioTable.createdAt
    var updatedAt by ScenarioTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ScenarioTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
