package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class InsightReaderProjectionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, InsightReaderProjectionEntity>(InsightReaderProjectionTable)

    var projectId by InsightReaderProjectionTable.projectId
    var insightId by InsightReaderProjectionTable.insightId
    var userId by InsightReaderProjectionTable.userId
    var seenAt by InsightReaderProjectionTable.seenAt
    var archivedAt by InsightReaderProjectionTable.archivedAt
    var keptAt by InsightReaderProjectionTable.keptAt
    var createdAt by InsightReaderProjectionTable.createdAt
    var updatedAt by InsightReaderProjectionTable.updatedAt
    var occurredAt by InsightReaderProjectionTable.occurredAt
    var acceptedAt by InsightReaderProjectionTable.acceptedAt
    var lastEventId by InsightReaderProjectionTable.lastEventId
    var projectionVersion by InsightReaderProjectionTable.projectionVersion
}
