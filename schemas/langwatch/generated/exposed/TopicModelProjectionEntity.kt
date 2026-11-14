package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class TopicModelProjectionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TopicModelProjectionEntity>(TopicModelProjectionTable)

    var projectId by TopicModelProjectionTable.projectId
    var createdAt by TopicModelProjectionTable.createdAt
    var updatedAt by TopicModelProjectionTable.updatedAt
    var occurredAt by TopicModelProjectionTable.occurredAt
    var acceptedAt by TopicModelProjectionTable.acceptedAt
    var lastEventId by TopicModelProjectionTable.lastEventId
    var projectionVersion by TopicModelProjectionTable.projectionVersion
}
