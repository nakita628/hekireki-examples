package hoppscotch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class MockServerLogEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MockServerLogEntity>(MockServerLogTable)

    var mockServerID by MockServerLogTable.mockServerID
    var requestMethod by MockServerLogTable.requestMethod
    var requestPath by MockServerLogTable.requestPath
    var requestHeaders by MockServerLogTable.requestHeaders
    var requestBody by MockServerLogTable.requestBody
    var requestQuery by MockServerLogTable.requestQuery
    var responseStatus by MockServerLogTable.responseStatus
    var responseHeaders by MockServerLogTable.responseHeaders
    var responseBody by MockServerLogTable.responseBody
    var responseTime by MockServerLogTable.responseTime
    var ipAddress by MockServerLogTable.ipAddress
    var userAgent by MockServerLogTable.userAgent
    var executedAt by MockServerLogTable.executedAt
    var mockServer by MockServerEntity referencedOn MockServerLogTable.mockServerID
}
