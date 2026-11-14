package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class TopicClusteringRunHistoryProjectionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TopicClusteringRunHistoryProjectionEntity>(TopicClusteringRunHistoryProjectionTable)

    var projectId by TopicClusteringRunHistoryProjectionTable.projectId
    var runs by TopicClusteringRunHistoryProjectionTable.runs
    var createdAt by TopicClusteringRunHistoryProjectionTable.createdAt
    var updatedAt by TopicClusteringRunHistoryProjectionTable.updatedAt
    var occurredAt by TopicClusteringRunHistoryProjectionTable.occurredAt
    var acceptedAt by TopicClusteringRunHistoryProjectionTable.acceptedAt
    var lastEventId by TopicClusteringRunHistoryProjectionTable.lastEventId
    var projectionVersion by TopicClusteringRunHistoryProjectionTable.projectionVersion
}
