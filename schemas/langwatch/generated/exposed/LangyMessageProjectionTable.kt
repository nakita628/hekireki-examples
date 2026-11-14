package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object LangyMessageProjectionTable : IdTable<String>("\"LangyMessageProjection\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val projectId = text("projectId")
    val conversationId = text("conversationId")
    val messageId = text("messageId")
    val role = text("role")
    val parts = jsonb<String>("parts", { it }, { it })
    val sourceEventId = text("sourceEventId")
    val occurredAt = double("occurredAt")
    val acceptedAt = double("acceptedAt")
    val createdAt = double("createdAt")
    val updatedAt = double("updatedAt")
    override val primaryKey = PrimaryKey(id, name = "\"LangyMessageProjection_pkey\"")

    init {
        index("\"LangyMessageProjection_projectId_conversationId_createdAt_idx\"", false, projectId, conversationId, createdAt)
        uniqueIndex("\"LangyMessageProjection_projectId_conversationId_messageId_key\"", projectId, conversationId, messageId)
        uniqueIndex("\"LangyMessageProjection_projectId_sourceEventId_key\"", projectId, sourceEventId)
    }
}
