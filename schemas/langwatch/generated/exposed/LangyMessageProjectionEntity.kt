package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LangyMessageProjectionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LangyMessageProjectionEntity>(LangyMessageProjectionTable)

    var projectId by LangyMessageProjectionTable.projectId
    var conversationId by LangyMessageProjectionTable.conversationId
    var messageId by LangyMessageProjectionTable.messageId
    var role by LangyMessageProjectionTable.role
    var parts by LangyMessageProjectionTable.parts
    var sourceEventId by LangyMessageProjectionTable.sourceEventId
    var occurredAt by LangyMessageProjectionTable.occurredAt
    var acceptedAt by LangyMessageProjectionTable.acceptedAt
    var createdAt by LangyMessageProjectionTable.createdAt
    var updatedAt by LangyMessageProjectionTable.updatedAt
}
