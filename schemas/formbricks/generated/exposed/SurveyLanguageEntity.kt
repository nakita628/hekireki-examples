package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class SurveyLanguageEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<SurveyLanguageEntity>(SurveyLanguageTable)

    val language by LanguageEntity referencedOn SurveyLanguageTable.languageId
    val languageId by SurveyLanguageTable.languageId
    val surveyId by SurveyLanguageTable.surveyId
    val survey by SurveyEntity referencedOn SurveyLanguageTable.surveyId
    var default by SurveyLanguageTable.default
    var enabled by SurveyLanguageTable.enabled
}
