package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ExecutedActionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ExecutedActionEntity>(ExecutedActionTable)

    var createdAt by ExecutedActionTable.createdAt
    var updatedAt by ExecutedActionTable.updatedAt
    var type by ExecutedActionTable.type
    var executionStatus by ExecutedActionTable.executionStatus
    var executionStartedAt by ExecutedActionTable.executionStartedAt
    var executedAt by ExecutedActionTable.executedAt
    var executionError by ExecutedActionTable.executionError
    var sentMessageIds by ExecutedActionTable.sentMessageIds
    var executedRuleId by ExecutedActionTable.executedRuleId
    var executedRule by ExecutedRuleEntity referencedOn ExecutedActionTable.executedRuleId
    var messagingChannelId by ExecutedActionTable.messagingChannelId
    var messagingChannel by MessagingChannelEntity optionalReferencedOn ExecutedActionTable.messagingChannelId
    var messagingMessageId by ExecutedActionTable.messagingMessageId
    var messagingMessageSentAt by ExecutedActionTable.messagingMessageSentAt
    var messagingMessageStatus by ExecutedActionTable.messagingMessageStatus
    var label by ExecutedActionTable.label
    var labelId by ExecutedActionTable.labelId
    var subject by ExecutedActionTable.subject
    var content by ExecutedActionTable.content
    var to by ExecutedActionTable.to
    var cc by ExecutedActionTable.cc
    var bcc by ExecutedActionTable.bcc
    var url by ExecutedActionTable.url
    var folderName by ExecutedActionTable.folderName
    var folderId by ExecutedActionTable.folderId
    var staticAttachments by ExecutedActionTable.staticAttachments
    var selectedAttachments by ExecutedActionTable.selectedAttachments
    var integrationName by ExecutedActionTable.integrationName
    var integrationToolName by ExecutedActionTable.integrationToolName
    var integrationArgs by ExecutedActionTable.integrationArgs
    var draftId by ExecutedActionTable.draftId
    var draftStatus by ExecutedActionTable.draftStatus
    var wasDraftSent by ExecutedActionTable.wasDraftSent
    var draftModelProvider by ExecutedActionTable.draftModelProvider
    var draftModelName by ExecutedActionTable.draftModelName
    var draftPipelineVersion by ExecutedActionTable.draftPipelineVersion
    var draftContextMetadata by ExecutedActionTable.draftContextMetadata
    val draftSendLog by DraftSendLogEntity optionalBackReferencedOn DraftSendLogTable.executedActionId
    val digestItems by DigestItemEntity optionalReferrersOn DigestItemTable.actionId
    val scheduledAction by ScheduledActionEntity optionalBackReferencedOn ScheduledActionTable.executedActionId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ExecutedActionTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
