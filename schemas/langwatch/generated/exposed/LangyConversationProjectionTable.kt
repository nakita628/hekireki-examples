package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable

object LangyConversationProjectionTable : IdTable<String>("\"LangyConversationProjection\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val projectId = text("projectId")
    val conversationId = text("conversationId")
    val userId = text("userId")
    val title = text("title").nullable()
    val titleSource =
        pgEnum("titleSource", "\"LangyProjectionTitleSource\"", LangyProjectionTitleSource.entries, LangyProjectionTitleSource::dbName)
    val origin = text("origin").default("interactive")
    val status = text("status")
    val isShared = bool("isShared").default(false)
    val sharedAt = double("sharedAt").nullable()
    val sharedById = text("sharedById").nullable()
    val messageCount = integer("messageCount").default(0)
    val lastActivityAt = double("lastActivityAt").nullable()
    val currentTurnId = text("currentTurnId").nullable()
    val lastError = text("lastError").nullable()
    val lastModel = text("lastModel").nullable()
    val pendingHandoffToken = text("pendingHandoffToken").nullable()
    val pendingHandoffTurnId = text("pendingHandoffTurnId").nullable()
    val runToken = text("runToken").nullable()
    val archivedAt = double("archivedAt").nullable()
    val createdAt = double("createdAt")
    val updatedAt = double("updatedAt")
    val occurredAt = double("occurredAt")
    val acceptedAt = double("acceptedAt")
    val lastEventId = text("lastEventId")
    val projectionVersion = text("projectionVersion")
    override val primaryKey = PrimaryKey(id, name = "\"LangyConversationProjection_pkey\"")

    init {
        index("LangyConversationProjection_projectId_userId_conversationId_idx", false, projectId, userId, conversationId)
        index("LangyConversationProjection_projectId_userId_lastActivityAt_idx", false, projectId, userId, lastActivityAt)
        uniqueIndex("\"LangyConversationProjection_projectId_conversationId_key\"", projectId, conversationId)
    }
}
