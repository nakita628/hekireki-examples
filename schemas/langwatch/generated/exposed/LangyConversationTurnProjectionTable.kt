package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object LangyConversationTurnProjectionTable : IdTable<String>("\"LangyConversationTurnProjection\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val projectId = text("projectId")
    val conversationId = text("conversationId")
    val turnId = text("turnId")
    val status = text("status")
    val questionParts = jsonb<String>("questionParts", { it }, { it })
    val answerParts = jsonb<String>("answerParts", { it }, { it })
    val toolCalls = jsonb<String>("toolCalls", { it }, { it })
    val plan = jsonb<String>("plan", { it }, { it }).nullable()
    val error = text("error").nullable()
    val startedAt = double("startedAt").nullable()
    val endedAt = double("endedAt").nullable()
    val createdAt = double("createdAt")
    val updatedAt = double("updatedAt")
    val occurredAt = double("occurredAt")
    val acceptedAt = double("acceptedAt")
    val lastEventId = text("lastEventId")
    val projectionVersion = text("projectionVersion")
    override val primaryKey = PrimaryKey(id, name = "\"LangyConversationTurnProjection_pkey\"")

    init {
        index("LangyConversationTurnProjection_projectId_conversationId_up_idx", false, projectId, conversationId, updatedAt)
        uniqueIndex("LangyConversationTurnProjection_projectId_conversationId_tu_key", projectId, conversationId, turnId)
    }
}
