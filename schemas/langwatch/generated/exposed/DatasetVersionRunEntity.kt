package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class DatasetVersionRunEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, DatasetVersionRunEntity>(DatasetVersionRunTable)

    var projectId by DatasetVersionRunTable.projectId
    var datasetId by DatasetVersionRunTable.datasetId
    var versionId by DatasetVersionRunTable.versionId
    var runId by DatasetVersionRunTable.runId
    var experimentId by DatasetVersionRunTable.experimentId
    var createdAt by DatasetVersionRunTable.createdAt
}
