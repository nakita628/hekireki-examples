package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class FeedbackSourceFieldMappingEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, FeedbackSourceFieldMappingEntity>(FeedbackSourceFieldMappingTable)

    var createdAt by FeedbackSourceFieldMappingTable.createdAt
    var feedbackSourceId by FeedbackSourceFieldMappingTable.feedbackSourceId
    var workspaceId by FeedbackSourceFieldMappingTable.workspaceId
    var sourceFieldId by FeedbackSourceFieldMappingTable.sourceFieldId
    var targetFieldId by FeedbackSourceFieldMappingTable.targetFieldId
    var staticValue by FeedbackSourceFieldMappingTable.staticValue
}
