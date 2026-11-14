package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ScenarioVersionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ScenarioVersionEntity>(ScenarioVersionTable)

    var scenarioId by ScenarioVersionTable.scenarioId
    var projectId by ScenarioVersionTable.projectId
    var version by ScenarioVersionTable.version
    var authorId by ScenarioVersionTable.authorId
    var authorLabel by ScenarioVersionTable.authorLabel
    var changeDescription by ScenarioVersionTable.changeDescription
    var snapshot by ScenarioVersionTable.snapshot
    var schemaVersion by ScenarioVersionTable.schemaVersion
    var createdAt by ScenarioVersionTable.createdAt
}
