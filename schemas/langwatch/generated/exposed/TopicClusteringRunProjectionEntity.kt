package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class TopicClusteringRunProjectionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TopicClusteringRunProjectionEntity>(TopicClusteringRunProjectionTable)

    var projectId by TopicClusteringRunProjectionTable.projectId
    var lastRequestedAt by TopicClusteringRunProjectionTable.lastRequestedAt
    var lastRequestTrigger by TopicClusteringRunProjectionTable.lastRequestTrigger
    var lastRunAt by TopicClusteringRunProjectionTable.lastRunAt
    var lastRunOutcome by TopicClusteringRunProjectionTable.lastRunOutcome
    var lastRunMode by TopicClusteringRunProjectionTable.lastRunMode
    var lastRunSkippedReason by TopicClusteringRunProjectionTable.lastRunSkippedReason
    var lastRunError by TopicClusteringRunProjectionTable.lastRunError
    var lastRunErrorCode by TopicClusteringRunProjectionTable.lastRunErrorCode
    var lastRunErrorUserActionable by TopicClusteringRunProjectionTable.lastRunErrorUserActionable
    var lastRunTracesProcessed by TopicClusteringRunProjectionTable.lastRunTracesProcessed
    var lastRunTopicsCount by TopicClusteringRunProjectionTable.lastRunTopicsCount
    var lastRunSubtopicsCount by TopicClusteringRunProjectionTable.lastRunSubtopicsCount
    var lastRunPages by TopicClusteringRunProjectionTable.lastRunPages
    var inProgressRunId by TopicClusteringRunProjectionTable.inProgressRunId
    var inProgressTraces by TopicClusteringRunProjectionTable.inProgressTraces
    var inProgressPages by TopicClusteringRunProjectionTable.inProgressPages
    var inProgressStartedAt by TopicClusteringRunProjectionTable.inProgressStartedAt
    var createdAt by TopicClusteringRunProjectionTable.createdAt
    var updatedAt by TopicClusteringRunProjectionTable.updatedAt
    var occurredAt by TopicClusteringRunProjectionTable.occurredAt
    var acceptedAt by TopicClusteringRunProjectionTable.acceptedAt
    var lastEventId by TopicClusteringRunProjectionTable.lastEventId
    var projectionVersion by TopicClusteringRunProjectionTable.projectionVersion
}
