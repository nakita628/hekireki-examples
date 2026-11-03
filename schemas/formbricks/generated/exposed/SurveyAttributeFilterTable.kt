package formbricks.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object SurveyAttributeFilterTable : IdTable<String>("\"SurveyAttributeFilter\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val attributeKeyId =
        reference(
            "attributeKeyId",
            ContactAttributeKeyTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"SurveyAttributeFilter_attributeKeyId_fkey\"",
        )
    val surveyId =
        reference(
            "surveyId",
            SurveyTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"SurveyAttributeFilter_surveyId_fkey\"",
        )
    val condition =
        pgEnum(
            "condition",
            "\"SurveyAttributeFilterCondition\"",
            SurveyAttributeFilterCondition.entries,
            SurveyAttributeFilterCondition::dbName,
        )
    val value = text("value")
    override val primaryKey = PrimaryKey(id, name = "\"SurveyAttributeFilter_pkey\"")

    init {
        index("\"SurveyAttributeFilter_attributeKeyId_idx\"", false, attributeKeyId)
        uniqueIndex("\"SurveyAttributeFilter_surveyId_attributeKeyId_key\"", surveyId, attributeKeyId)
    }
}
