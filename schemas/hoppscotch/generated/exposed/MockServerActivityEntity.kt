package hoppscotch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class MockServerActivityEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MockServerActivityEntity>(MockServerActivityTable)

    var mockServerID by MockServerActivityTable.mockServerID
    var action by MockServerActivityTable.action
    var performedBy by MockServerActivityTable.performedBy
    var performedAt by MockServerActivityTable.performedAt
    var mockServer by MockServerEntity referencedOn MockServerActivityTable.mockServerID
}
