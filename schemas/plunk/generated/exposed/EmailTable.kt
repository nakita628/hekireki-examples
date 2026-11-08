package plunk.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object EmailTable : IdTable<String>("emails") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val contactId =
        reference(
            "contactId",
            ContactTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"emails_contactId_fkey\"",
        )
    val toName = text("toName").nullable()
    val subject = text("subject")
    val body = text("body")
    val from = text("from")
    val fromName = text("fromName").nullable()
    val replyTo = text("replyTo").nullable()
    val headers = jsonb<String>("headers", { it }, { it }).nullable()
    val attachments = jsonb<String>("attachments", { it }, { it }).nullable()
    val messageId = text("messageId").nullable().uniqueIndex("\"emails_messageId_key\"")
    val sourceType = pgEnum("sourceType", "\"EmailSourceType\"", EmailSourceType.entries, EmailSourceType::dbName)
    val templateId =
        optReference(
            "templateId",
            TemplateTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"emails_templateId_fkey\"",
        )
    val campaignId =
        optReference(
            "campaignId",
            CampaignTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"emails_campaignId_fkey\"",
        )
    val workflowExecutionId =
        optReference(
            "workflowExecutionId",
            WorkflowExecutionTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"emails_workflowExecutionId_fkey\"",
        )
    val workflowStepExecutionId =
        optReference(
            "workflowStepExecutionId",
            WorkflowStepExecutionTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"emails_workflowStepExecutionId_fkey\"",
        )
    val status = pgEnum("status", "\"EmailStatus\"", EmailStatus.entries, EmailStatus::dbName).default(EmailStatus.PENDING)
    val scheduledFor = pgTimestamp("scheduledFor", 3).nullable()
    val sentAt = pgTimestamp("sentAt", 3).nullable()
    val deliveredAt = pgTimestamp("deliveredAt", 3).nullable()
    val openedAt = pgTimestamp("openedAt", 3).nullable()
    val clickedAt = pgTimestamp("clickedAt", 3).nullable()
    val bouncedAt = pgTimestamp("bouncedAt", 3).nullable()
    val complainedAt = pgTimestamp("complainedAt", 3).nullable()
    val unsubscribedAt = pgTimestamp("unsubscribedAt", 3).nullable()
    val opens = integer("opens").default(0)
    val clicks = integer("clicks").default(0)
    val simulated = bool("simulated").default(false)
    val error = text("error").nullable()
    val projectId =
        reference(
            "projectId",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"emails_projectId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "emails_pkey")

    init {
        index("\"emails_projectId_contactId_idx\"", false, projectId, contactId)
        index("\"emails_contactId_idx\"", false, contactId)
        index("\"emails_campaignId_idx\"", false, campaignId)
        index("\"emails_campaignId_status_idx\"", false, campaignId, status)
        index("\"emails_workflowExecutionId_idx\"", false, workflowExecutionId)
        index("\"emails_workflowStepExecutionId_idx\"", false, workflowStepExecutionId)
        index("emails_status_idx", false, status)
        index("\"emails_createdAt_idx\"", false, createdAt)
        index("\"emails_projectId_sourceType_createdAt_idx\"", false, projectId, sourceType, createdAt)
        index("\"emails_contactId_openedAt_idx\"", false, contactId, openedAt)
        index("\"emails_contactId_clickedAt_idx\"", false, contactId, clickedAt)
        index("\"emails_contactId_bouncedAt_idx\"", false, contactId, bouncedAt)
        index("\"emails_contactId_complainedAt_idx\"", false, contactId, complainedAt)
    }
}
