package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SimulationSuiteEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SimulationSuiteEntity>(SimulationSuiteTable)

    var projectId by SimulationSuiteTable.projectId
    var name by SimulationSuiteTable.name
    var slug by SimulationSuiteTable.slug
    var description by SimulationSuiteTable.description
    var scenarioIds by SimulationSuiteTable.scenarioIds
    var targets by SimulationSuiteTable.targets
    var repeatCount by SimulationSuiteTable.repeatCount
    var labels by SimulationSuiteTable.labels
    var simulatorModel by SimulationSuiteTable.simulatorModel
    var judgeModel by SimulationSuiteTable.judgeModel
    var kind by SimulationSuiteTable.kind
    var scope by SimulationSuiteTable.scope
    var fields by SimulationSuiteTable.fields1
    var evaluators by SimulationSuiteTable.evaluators
    var archivedAt by SimulationSuiteTable.archivedAt
    var createdAt by SimulationSuiteTable.createdAt
    var updatedAt by SimulationSuiteTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SimulationSuiteTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
