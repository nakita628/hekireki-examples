package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable

object TopicModelProjectionTable : IdTable<String>("\"TopicModelProjection\"") {
    override val id = text("id").entityId()
    val projectId = text("projectId").uniqueIndex("\"TopicModelProjection_projectId_key\"")
    val createdAt = double("createdAt")
    val updatedAt = double("updatedAt")
    val occurredAt = double("occurredAt")
    val acceptedAt = double("acceptedAt")
    val lastEventId = text("lastEventId")
    val projectionVersion = text("projectionVersion")
    override val primaryKey = PrimaryKey(id, name = "\"TopicModelProjection_pkey\"")
}
