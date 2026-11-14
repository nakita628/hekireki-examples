package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object TopicClusteringRunHistoryProjectionTable : IdTable<String>("\"TopicClusteringRunHistoryProjection\"") {
    override val id = text("id").entityId()
    val projectId = text("projectId").uniqueIndex("\"TopicClusteringRunHistoryProjection_projectId_key\"")
    val runs = jsonb<String>("runs", { it }, { it })
    val createdAt = double("createdAt")
    val updatedAt = double("updatedAt")
    val occurredAt = double("occurredAt")
    val acceptedAt = double("acceptedAt")
    val lastEventId = text("lastEventId")
    val projectionVersion = text("projectionVersion")
    override val primaryKey = PrimaryKey(id, name = "\"TopicClusteringRunHistoryProjection_pkey\"")
}
