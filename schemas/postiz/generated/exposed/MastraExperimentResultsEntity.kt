package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class MastraExperimentResultsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MastraExperimentResultsEntity>(MastraExperimentResultsTable)

    var experimentId by MastraExperimentResultsTable.experimentId
    var itemId by MastraExperimentResultsTable.itemId
    var itemDatasetVersion by MastraExperimentResultsTable.itemDatasetVersion
    var input by MastraExperimentResultsTable.input
    var output by MastraExperimentResultsTable.output
    var groundTruth by MastraExperimentResultsTable.groundTruth
    var error by MastraExperimentResultsTable.error
    var startedAt by MastraExperimentResultsTable.startedAt
    var completedAt by MastraExperimentResultsTable.completedAt
    var retryCount by MastraExperimentResultsTable.retryCount
    var traceId by MastraExperimentResultsTable.traceId
    var status by MastraExperimentResultsTable.status
    var tags by MastraExperimentResultsTable.tags
    var createdAt by MastraExperimentResultsTable.createdAt
    var startedAtZ by MastraExperimentResultsTable.startedAtZ
    var completedAtZ by MastraExperimentResultsTable.completedAtZ
    var createdAtZ by MastraExperimentResultsTable.createdAtZ
    var comment by MastraExperimentResultsTable.comment
    var toolMockReport by MastraExperimentResultsTable.toolMockReport
    var metadata by MastraExperimentResultsTable.metadata
    var organizationId by MastraExperimentResultsTable.organizationId
    var projectId by MastraExperimentResultsTable.projectId
    var attempt by MastraExperimentResultsTable.attempt
}
