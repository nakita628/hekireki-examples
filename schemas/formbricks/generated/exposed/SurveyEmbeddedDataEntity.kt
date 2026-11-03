package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class SurveyEmbeddedDataEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SurveyEmbeddedDataEntity>(SurveyEmbeddedDataTable)

    var workspaceId by SurveyEmbeddedDataTable.workspaceId
    var surveyId by SurveyEmbeddedDataTable.surveyId
    var embeddedDataId by SurveyEmbeddedDataTable.embeddedDataId
    var storageKey by SurveyEmbeddedDataTable.storageKey
    var order by SurveyEmbeddedDataTable.order
}
