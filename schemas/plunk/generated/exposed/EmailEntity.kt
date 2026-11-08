package plunk.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class EmailEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, EmailEntity>(EmailTable)

    var contact by ContactEntity referencedOn EmailTable.contactId
    var contactId by EmailTable.contactId
    var toName by EmailTable.toName
    var subject by EmailTable.subject
    var body by EmailTable.body
    var from by EmailTable.from
    var fromName by EmailTable.fromName
    var replyTo by EmailTable.replyTo
    var headers by EmailTable.headers
    var attachments by EmailTable.attachments
    var messageId by EmailTable.messageId
    var sourceType by EmailTable.sourceType
    var template by TemplateEntity optionalReferencedOn EmailTable.templateId
    var templateId by EmailTable.templateId
    var campaign by CampaignEntity optionalReferencedOn EmailTable.campaignId
    var campaignId by EmailTable.campaignId
    var workflowExecution by WorkflowExecutionEntity optionalReferencedOn EmailTable.workflowExecutionId
    var workflowExecutionId by EmailTable.workflowExecutionId
    var workflowStepExecution by WorkflowStepExecutionEntity optionalReferencedOn EmailTable.workflowStepExecutionId
    var workflowStepExecutionId by EmailTable.workflowStepExecutionId
    var status by EmailTable.status
    var scheduledFor by EmailTable.scheduledFor
    var sentAt by EmailTable.sentAt
    var deliveredAt by EmailTable.deliveredAt
    var openedAt by EmailTable.openedAt
    var clickedAt by EmailTable.clickedAt
    var bouncedAt by EmailTable.bouncedAt
    var complainedAt by EmailTable.complainedAt
    var unsubscribedAt by EmailTable.unsubscribedAt
    var opens by EmailTable.opens
    var clicks by EmailTable.clicks
    var simulated by EmailTable.simulated
    var error by EmailTable.error
    var project by ProjectEntity referencedOn EmailTable.projectId
    var projectId by EmailTable.projectId
    val events by EventEntity optionalReferrersOn EventTable.emailId
    var createdAt by EmailTable.createdAt
    var updatedAt by EmailTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(EmailTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
