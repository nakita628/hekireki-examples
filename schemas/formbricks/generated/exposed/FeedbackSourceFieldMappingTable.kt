package formbricks.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object FeedbackSourceFieldMappingTable : IdTable<String>("\"FeedbackSourceFieldMapping\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val feedbackSourceId = text("feedback_source_id")
    val workspaceId = text("workspaceId")
    val sourceFieldId = text("source_field_id")
    val targetFieldId = text("target_field_id")
    val staticValue = text("static_value").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"FeedbackSourceFieldMapping_pkey\"")

    init {
        uniqueIndex(
            "FeedbackSourceFieldMapping_workspaceId_feedbackSourceId_sourceF",
            workspaceId,
            feedbackSourceId,
            sourceFieldId,
            targetFieldId,
        )
        foreignKey(
            feedbackSourceId to FeedbackSourceTable.id,
            workspaceId to FeedbackSourceTable.workspaceId,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            name = "\"FeedbackSourceFieldMapping_feedbackSourceId_workspaceId_fkey\"",
        )
    }
}
