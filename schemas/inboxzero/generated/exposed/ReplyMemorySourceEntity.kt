package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class ReplyMemorySourceEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<ReplyMemorySourceEntity>(ReplyMemorySourceTable)

    var createdAt by ReplyMemorySourceTable.createdAt
    var learnedWritingStyleAnalyzedAt by ReplyMemorySourceTable.learnedWritingStyleAnalyzedAt
    val replyMemoryId by ReplyMemorySourceTable.replyMemoryId
    val replyMemory by ReplyMemoryEntity referencedOn ReplyMemorySourceTable.replyMemoryId
    val draftSendLogId by ReplyMemorySourceTable.draftSendLogId
    val draftSendLog by DraftSendLogEntity referencedOn ReplyMemorySourceTable.draftSendLogId
}
