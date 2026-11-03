package formbricks.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object FeedbackSourceFormbricksMappingTable : IdTable<String>("\"FeedbackSourceFormbricksMapping\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val feedbackSourceId = text("feedback_source_id")
    val workspaceId = text("workspaceId")
    val surveyId = text("surveyId")
    val elementId = text("elementId")
    val hubFieldType = pgEnum("hubFieldType", "\"HubFieldType\"", HubFieldType.entries, HubFieldType::dbName)
    val customFieldLabel = text("custom_field_label").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"FeedbackSourceFormbricksMapping_pkey\"")

    init {
        index("\"FeedbackSourceFormbricksMapping_workspaceId_surveyId_idx\"", false, workspaceId, surveyId)
        index("\"FeedbackSourceFormbricksMapping_surveyId_idx\"", false, surveyId)
        uniqueIndex("FeedbackSourceFormbricksMapping_workspaceId_feedbackSourceId_su", workspaceId, feedbackSourceId, surveyId, elementId)
        foreignKey(
            feedbackSourceId to FeedbackSourceTable.id,
            workspaceId to FeedbackSourceTable.workspaceId,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            name = "FeedbackSourceFormbricksMapping_feedbackSourceId_workspaceId_fk",
        )
        foreignKey(
            surveyId to SurveyTable.id,
            workspaceId to SurveyTable.workspaceId,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            name = "\"FeedbackSourceFormbricksMapping_surveyId_workspaceId_fkey\"",
        )
    }
}
