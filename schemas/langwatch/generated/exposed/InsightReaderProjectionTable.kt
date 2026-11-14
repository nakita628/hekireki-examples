package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable

object InsightReaderProjectionTable : IdTable<String>("\"InsightReaderProjection\"") {
    override val id = text("id").entityId()
    val projectId = text("projectId")
    val insightId = text("insightId")
    val userId = text("userId")
    val seenAt = double("seenAt").nullable()
    val archivedAt = double("archivedAt").nullable()
    val keptAt = double("keptAt").nullable()
    val createdAt = double("createdAt")
    val updatedAt = double("updatedAt")
    val occurredAt = double("occurredAt")
    val acceptedAt = double("acceptedAt")
    val lastEventId = text("lastEventId")
    val projectionVersion = text("projectionVersion")
    override val primaryKey = PrimaryKey(id, name = "\"InsightReaderProjection_pkey\"")

    init {
        index("\"InsightReaderProjection_projectId_userId_idx\"", false, projectId, userId)
        uniqueIndex("\"InsightReaderProjection_projectId_insightId_userId_key\"", projectId, insightId, userId)
    }
}
