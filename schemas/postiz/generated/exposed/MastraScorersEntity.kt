package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class MastraScorersEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MastraScorersEntity>(MastraScorersTable)

    var scorerId by MastraScorersTable.scorerId
    var traceId by MastraScorersTable.traceId
    var runId by MastraScorersTable.runId
    var scorer by MastraScorersTable.scorer
    var preprocessStepResult by MastraScorersTable.preprocessStepResult
    var extractStepResult by MastraScorersTable.extractStepResult
    var analyzeStepResult by MastraScorersTable.analyzeStepResult
    var score by MastraScorersTable.score
    var reason by MastraScorersTable.reason
    var metadata by MastraScorersTable.metadata
    var preprocessPrompt by MastraScorersTable.preprocessPrompt
    var extractPrompt by MastraScorersTable.extractPrompt
    var generateScorePrompt by MastraScorersTable.generateScorePrompt
    var generateReasonPrompt by MastraScorersTable.generateReasonPrompt
    var analyzePrompt by MastraScorersTable.analyzePrompt
    var reasonPrompt by MastraScorersTable.reasonPrompt
    var input by MastraScorersTable.input
    var output by MastraScorersTable.output
    var additionalContext by MastraScorersTable.additionalContext
    var runtimeContext by MastraScorersTable.runtimeContext
    var entityType by MastraScorersTable.entityType
    var entity by MastraScorersTable.entity
    var entityId by MastraScorersTable.entityId
    var source by MastraScorersTable.source1
    var resourceId by MastraScorersTable.resourceId
    var threadId by MastraScorersTable.threadId
    var createdAt by MastraScorersTable.createdAt
    var updatedAt by MastraScorersTable.updatedAt
    var createdAtZ by MastraScorersTable.createdAtZ
    var updatedAtZ by MastraScorersTable.updatedAtZ
    var spanId by MastraScorersTable.spanId
    var requestContext by MastraScorersTable.requestContext
    var organizationId by MastraScorersTable.organizationId
    var projectId by MastraScorersTable.projectId
    var batchId by MastraScorersTable.batchId
    var datasetId by MastraScorersTable.datasetId
    var datasetItemId by MastraScorersTable.datasetItemId
}
