package formbricks.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object RetentionSurveyRestoreTable : IdTable<String>("\"RetentionSurveyRestore\"") {
    override val id =
        text("surveyId")
            .references(
                SurveyTable.id,
                onDelete = ReferenceOption.CASCADE,
                onUpdate = ReferenceOption.CASCADE,
                fkName = "\"RetentionSurveyRestore_surveyId_fkey\"",
            ).entityId()
    val restoredAt = pgTimestamp("restoredAt", 3)
    override val primaryKey = PrimaryKey(id, name = "\"RetentionSurveyRestore_pkey\"")
}
