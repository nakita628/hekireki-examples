package formbricks.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object SurveyTriggerTable : IdTable<String>("\"SurveyTrigger\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val surveyId =
        reference(
            "surveyId",
            SurveyTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"SurveyTrigger_surveyId_fkey\"",
        )
    val actionClassId =
        reference(
            "actionClassId",
            ActionClassTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"SurveyTrigger_actionClassId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"SurveyTrigger_pkey\"")

    init {
        uniqueIndex("\"SurveyTrigger_surveyId_actionClassId_key\"", surveyId, actionClassId)
    }
}
