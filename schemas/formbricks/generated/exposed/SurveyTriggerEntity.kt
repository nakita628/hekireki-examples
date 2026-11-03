package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SurveyTriggerEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SurveyTriggerEntity>(SurveyTriggerTable)

    var createdAt by SurveyTriggerTable.createdAt
    var updatedAt by SurveyTriggerTable.updatedAt
    var survey by SurveyEntity referencedOn SurveyTriggerTable.surveyId
    var surveyId by SurveyTriggerTable.surveyId
    var actionClass by ActionClassEntity referencedOn SurveyTriggerTable.actionClassId
    var actionClassId by SurveyTriggerTable.actionClassId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SurveyTriggerTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
