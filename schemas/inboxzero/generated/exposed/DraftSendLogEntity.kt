package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class DraftSendLogEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, DraftSendLogEntity>(DraftSendLogTable)

    var createdAt by DraftSendLogTable.createdAt
    var executedActionId by DraftSendLogTable.executedActionId
    var executedAction by ExecutedActionEntity referencedOn DraftSendLogTable.executedActionId
    var sentMessageId by DraftSendLogTable.sentMessageId
    var similarityScore by DraftSendLogTable.similarityScore
    var bodySimilarityScore by DraftSendLogTable.bodySimilarityScore
    var bodySimilarityStatus by DraftSendLogTable.bodySimilarityStatus
    var similarityMetadata by DraftSendLogTable.similarityMetadata
    var sentText by DraftSendLogTable.sentText
    var replyMemorySentText by DraftSendLogTable.replyMemorySentText
    var replyMemoryAttemptCount by DraftSendLogTable.replyMemoryAttemptCount
    var replyMemoryProcessedAt by DraftSendLogTable.replyMemoryProcessedAt
    val replyMemorySources by ReplyMemorySourceEntity referrersOn ReplyMemorySourceTable.draftSendLogId
}
