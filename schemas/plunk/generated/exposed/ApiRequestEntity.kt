package plunk.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ApiRequestEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ApiRequestEntity>(ApiRequestTable)

    var method by ApiRequestTable.method
    var path by ApiRequestTable.path
    var statusCode by ApiRequestTable.statusCode
    var duration by ApiRequestTable.duration
    var projectId by ApiRequestTable.projectId
    var project by ProjectEntity optionalReferencedOn ApiRequestTable.projectId
    var userId by ApiRequestTable.userId
    var authType by ApiRequestTable.authType
    var ip by ApiRequestTable.ip
    var userAgent by ApiRequestTable.userAgent
    var errorCode by ApiRequestTable.errorCode
    var errorMessage by ApiRequestTable.errorMessage
    var requestSize by ApiRequestTable.requestSize
    var responseSize by ApiRequestTable.responseSize
    var createdAt by ApiRequestTable.createdAt
}
