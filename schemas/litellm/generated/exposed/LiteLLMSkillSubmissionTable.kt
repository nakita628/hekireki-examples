package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object LiteLLMSkillSubmissionTable : IdTable<String>("\"LiteLLM_SkillSubmission\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val pluginName = text("plugin_name")
    val operation = text("operation")
    val status = text("status").default("pending_review")
    val proposalJson = text("proposal_json")
    val archiveContent = binary("archive_content").nullable()
    val submittedBy = text("submitted_by")
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val basePluginId = text("base_plugin_id").nullable()
    val baseUpdatedAt = pgTimestamp("base_updated_at", 3).nullable()
    val reviewedBy = text("reviewed_by").nullable()
    val reviewedAt = pgTimestamp("reviewed_at", 3).nullable()
    val rejectionReason = text("rejection_reason").nullable()
    val scanJson = text("scan_json").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_SkillSubmission_pkey\"")

    init {
        index(
            "\"LiteLLM_SkillSubmission_submitted_by_created_at_id_idx\"",
            false,
            functions = listOf(indexExpression("\"submitted_by\""), indexExpression("\"created_at\" DESC"), indexExpression("\"id\" DESC")),
        )
        index(
            "\"LiteLLM_SkillSubmission_submitted_by_status_created_at_id_idx\"",
            false,
            functions =
                listOf(
                    indexExpression("\"submitted_by\""),
                    indexExpression("\"status\""),
                    indexExpression("\"created_at\" DESC"),
                    indexExpression("\"id\" DESC"),
                ),
        )
        index(
            "\"LiteLLM_SkillSubmission_status_created_at_id_idx\"",
            false,
            functions = listOf(indexExpression("\"status\""), indexExpression("\"created_at\" DESC"), indexExpression("\"id\" DESC")),
        )
        index(
            "\"LiteLLM_SkillSubmission_created_at_id_idx\"",
            false,
            functions = listOf(indexExpression("\"created_at\" DESC"), indexExpression("\"id\" DESC")),
        )
    }
}
