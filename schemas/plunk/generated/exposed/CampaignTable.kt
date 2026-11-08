package plunk.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object CampaignTable : IdTable<String>("campaigns") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val name = text("name")
    val description = text("description").nullable()
    val status = pgEnum("status", "\"CampaignStatus\"", CampaignStatus.entries, CampaignStatus::dbName).default(CampaignStatus.DRAFT)
    val subject = text("subject")
    val body = text("body")
    val from = text("from")
    val fromName = text("fromName").nullable()
    val replyTo = text("replyTo").nullable()
    val type = pgEnum("type", "\"TemplateType\"", TemplateType.entries, TemplateType::dbName).default(TemplateType.MARKETING)
    val audienceType =
        pgEnum("audienceType", "\"CampaignAudienceType\"", CampaignAudienceType.entries, CampaignAudienceType::dbName).default(
            CampaignAudienceType.ALL,
        )
    val audienceCondition = jsonb<String>("audienceCondition", { it }, { it }).nullable()
    val segmentId =
        optReference(
            "segmentId",
            SegmentTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"campaigns_segmentId_fkey\"",
        )
    val scheduledFor = pgTimestamp("scheduledFor", 3).nullable()
    val totalRecipients = integer("totalRecipients").default(0)
    val sentCount = integer("sentCount").default(0)
    val deliveredCount = integer("deliveredCount").default(0)
    val openedCount = integer("openedCount").default(0)
    val clickedCount = integer("clickedCount").default(0)
    val bouncedCount = integer("bouncedCount").default(0)
    val complainedCount = integer("complainedCount").default(0)
    val unsubscribedCount = integer("unsubscribedCount").default(0)
    val archived = bool("archived").default(false)
    val projectId =
        reference(
            "projectId",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"campaigns_projectId_fkey\"",
        )
    val sentAt = pgTimestamp("sentAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "campaigns_pkey")

    init {
        index("\"campaigns_projectId_status_idx\"", false, projectId, status)
        index("\"campaigns_projectId_archived_createdAt_idx\"", false, projectId, archived, createdAt)
        index("\"campaigns_scheduledFor_idx\"", false, scheduledFor)
        index("\"campaigns_segmentId_idx\"", false, segmentId)
        index(
            "\"campaigns_audienceCondition_idx\"",
            false,
            functions = listOf(indexExpression("\"audienceCondition\" jsonb_ops")),
            indexType = "gin",
        )
    }
}
