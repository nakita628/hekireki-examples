package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class FeedbackSourceFormbricksMappingEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, FeedbackSourceFormbricksMappingEntity>(FeedbackSourceFormbricksMappingTable)

    var createdAt by FeedbackSourceFormbricksMappingTable.createdAt
    var feedbackSourceId by FeedbackSourceFormbricksMappingTable.feedbackSourceId
    var workspaceId by FeedbackSourceFormbricksMappingTable.workspaceId
    var surveyId by FeedbackSourceFormbricksMappingTable.surveyId
    var elementId by FeedbackSourceFormbricksMappingTable.elementId
    var hubFieldType by FeedbackSourceFormbricksMappingTable.hubFieldType
    var customFieldLabel by FeedbackSourceFormbricksMappingTable.customFieldLabel
}
