package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class TopicClusteringRunEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TopicClusteringRunEntity>(TopicClusteringRunTable)

    var projectId by TopicClusteringRunTable.projectId
    var facetId by TopicClusteringRunTable.facetId
    var facetVersion by TopicClusteringRunTable.facetVersion
    var project by ProjectEntity referencedOn TopicClusteringRunTable.projectId
    var status by TopicClusteringRunTable.status
    var config by TopicClusteringRunTable.config
    var error by TopicClusteringRunTable.error
    var createdAt by TopicClusteringRunTable.createdAt
    var startedAt by TopicClusteringRunTable.startedAt
    var finishedAt by TopicClusteringRunTable.finishedAt
    var topicVersionIds by TopicClusteringRunTable.topicVersionIds
}
