package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class UserLastVisitEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, UserLastVisitEntity>(UserLastVisitTable)

    var userId by UserLastVisitTable.userId
    var resourceType by UserLastVisitTable.resourceType
    var resourceId by UserLastVisitTable.resourceId
    var parentResourceId by UserLastVisitTable.parentResourceId
    var lastVisitTime by UserLastVisitTable.lastVisitTime
}
