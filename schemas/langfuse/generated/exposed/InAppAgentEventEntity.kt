package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class InAppAgentEventEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<InAppAgentEventEntity>(InAppAgentEventTable)

    val projectId by InAppAgentEventTable.projectId
    val project by ProjectEntity referencedOn InAppAgentEventTable.projectId
    val conversationId by InAppAgentEventTable.conversationId
    val conversation by InAppAgentConversationEntity referencedOn InAppAgentEventTable
    var runId by InAppAgentEventTable.runId
    val run by InAppAgentRunEntity referencedOn InAppAgentEventTable
    val sequenceNumber by InAppAgentEventTable.sequenceNumber
    var type by InAppAgentEventTable.type
    var event by InAppAgentEventTable.event
    var createdAt by InAppAgentEventTable.createdAt
}
