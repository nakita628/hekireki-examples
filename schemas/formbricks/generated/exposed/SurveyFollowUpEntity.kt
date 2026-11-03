package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SurveyFollowUpEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SurveyFollowUpEntity>(SurveyFollowUpTable)

    var createdAt by SurveyFollowUpTable.createdAt
    var updatedAt by SurveyFollowUpTable.updatedAt
    var survey by SurveyEntity referencedOn SurveyFollowUpTable.surveyId
    var surveyId by SurveyFollowUpTable.surveyId
    var name by SurveyFollowUpTable.name
    var trigger by SurveyFollowUpTable.trigger
    var action by SurveyFollowUpTable.action

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SurveyFollowUpTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
