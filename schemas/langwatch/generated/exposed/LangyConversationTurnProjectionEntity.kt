package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LangyConversationTurnProjectionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LangyConversationTurnProjectionEntity>(LangyConversationTurnProjectionTable)

    var projectId by LangyConversationTurnProjectionTable.projectId
    var conversationId by LangyConversationTurnProjectionTable.conversationId
    var turnId by LangyConversationTurnProjectionTable.turnId
    var status by LangyConversationTurnProjectionTable.status
    var questionParts by LangyConversationTurnProjectionTable.questionParts
    var answerParts by LangyConversationTurnProjectionTable.answerParts
    var toolCalls by LangyConversationTurnProjectionTable.toolCalls
    var plan by LangyConversationTurnProjectionTable.plan
    var error by LangyConversationTurnProjectionTable.error
    var startedAt by LangyConversationTurnProjectionTable.startedAt
    var endedAt by LangyConversationTurnProjectionTable.endedAt
    var createdAt by LangyConversationTurnProjectionTable.createdAt
    var updatedAt by LangyConversationTurnProjectionTable.updatedAt
    var occurredAt by LangyConversationTurnProjectionTable.occurredAt
    var acceptedAt by LangyConversationTurnProjectionTable.acceptedAt
    var lastEventId by LangyConversationTurnProjectionTable.lastEventId
    var projectionVersion by LangyConversationTurnProjectionTable.projectionVersion
}
