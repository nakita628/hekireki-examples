package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ScheduledActionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ScheduledActionEntity>(ScheduledActionTable)

    var createdAt by ScheduledActionTable.createdAt
    var updatedAt by ScheduledActionTable.updatedAt
    var executedRuleId by ScheduledActionTable.executedRuleId
    var actionType by ScheduledActionTable.actionType
    var messageId by ScheduledActionTable.messageId
    var threadId by ScheduledActionTable.threadId
    var scheduledFor by ScheduledActionTable.scheduledFor
    var emailAccountId by ScheduledActionTable.emailAccountId
    var status by ScheduledActionTable.status
    var schedulingStatus by ScheduledActionTable.schedulingStatus
    var label by ScheduledActionTable.label
    var labelId by ScheduledActionTable.labelId
    var subject by ScheduledActionTable.subject
    var content by ScheduledActionTable.content
    var to by ScheduledActionTable.to
    var cc by ScheduledActionTable.cc
    var bcc by ScheduledActionTable.bcc
    var url by ScheduledActionTable.url
    var folderName by ScheduledActionTable.folderName
    var folderId by ScheduledActionTable.folderId
    var scheduledId by ScheduledActionTable.scheduledId
    var staticAttachments by ScheduledActionTable.staticAttachments
    var selectedAttachments by ScheduledActionTable.selectedAttachments
    var executedAt by ScheduledActionTable.executedAt
    var executedActionId by ScheduledActionTable.executedActionId
    var executedRule by ExecutedRuleEntity referencedOn ScheduledActionTable.executedRuleId
    var executedAction by ExecutedActionEntity optionalReferencedOn ScheduledActionTable.executedActionId
    var emailAccount by EmailAccountEntity referencedOn ScheduledActionTable.emailAccountId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ScheduledActionTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
