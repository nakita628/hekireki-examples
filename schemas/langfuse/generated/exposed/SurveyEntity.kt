package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class SurveyEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SurveyEntity>(SurveyTable)

    var createdAt by SurveyTable.createdAt
    var surveyName by SurveyTable.surveyName
    var response by SurveyTable.response
    var userId by SurveyTable.userId
    var userEmail by SurveyTable.userEmail
    var orgId by SurveyTable.orgId
    var org by OrganizationEntity optionalReferencedOn SurveyTable.orgId
    var user by UserEntity optionalReferencedOn SurveyTable.userId
}
