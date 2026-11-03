package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SurveyQuotaEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SurveyQuotaEntity>(SurveyQuotaTable)

    var createdAt by SurveyQuotaTable.createdAt
    var updatedAt by SurveyQuotaTable.updatedAt
    var survey by SurveyEntity referencedOn SurveyQuotaTable.surveyId
    var surveyId by SurveyQuotaTable.surveyId
    var name by SurveyQuotaTable.name
    var limit by SurveyQuotaTable.limit
    var logic by SurveyQuotaTable.logic
    var action by SurveyQuotaTable.action
    var endingCardId by SurveyQuotaTable.endingCardId
    val quotaLinks by ResponseQuotaLinkEntity referrersOn ResponseQuotaLinkTable.quotaId
    var countPartialSubmissions by SurveyQuotaTable.countPartialSubmissions

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SurveyQuotaTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
