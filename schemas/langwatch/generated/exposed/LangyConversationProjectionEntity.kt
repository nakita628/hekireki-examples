package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LangyConversationProjectionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LangyConversationProjectionEntity>(LangyConversationProjectionTable)

    var projectId by LangyConversationProjectionTable.projectId
    var conversationId by LangyConversationProjectionTable.conversationId
    var userId by LangyConversationProjectionTable.userId
    var title by LangyConversationProjectionTable.title
    var titleSource by LangyConversationProjectionTable.titleSource
    var origin by LangyConversationProjectionTable.origin
    var status by LangyConversationProjectionTable.status
    var isShared by LangyConversationProjectionTable.isShared
    var sharedAt by LangyConversationProjectionTable.sharedAt
    var sharedById by LangyConversationProjectionTable.sharedById
    var messageCount by LangyConversationProjectionTable.messageCount
    var lastActivityAt by LangyConversationProjectionTable.lastActivityAt
    var currentTurnId by LangyConversationProjectionTable.currentTurnId
    var lastError by LangyConversationProjectionTable.lastError
    var lastModel by LangyConversationProjectionTable.lastModel
    var pendingHandoffToken by LangyConversationProjectionTable.pendingHandoffToken
    var pendingHandoffTurnId by LangyConversationProjectionTable.pendingHandoffTurnId
    var runToken by LangyConversationProjectionTable.runToken
    var archivedAt by LangyConversationProjectionTable.archivedAt
    var createdAt by LangyConversationProjectionTable.createdAt
    var updatedAt by LangyConversationProjectionTable.updatedAt
    var occurredAt by LangyConversationProjectionTable.occurredAt
    var acceptedAt by LangyConversationProjectionTable.acceptedAt
    var lastEventId by LangyConversationProjectionTable.lastEventId
    var projectionVersion by LangyConversationProjectionTable.projectionVersion
}
