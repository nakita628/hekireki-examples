package rallly.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class NonprofitApplicationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, NonprofitApplicationEntity>(NonprofitApplicationTable)

    var spaceId by NonprofitApplicationTable.spaceId
    var userId by NonprofitApplicationTable.userId
    var organizationName by NonprofitApplicationTable.organizationName
    var website by NonprofitApplicationTable.website
    var emailDomain by NonprofitApplicationTable.emailDomain
    var status by NonprofitApplicationTable.status
    var reason by NonprofitApplicationTable.reason
    var model by NonprofitApplicationTable.model
    var createdAt by NonprofitApplicationTable.createdAt
    var space by SpaceEntity referencedOn NonprofitApplicationTable.spaceId
    var user by UserEntity referencedOn NonprofitApplicationTable.userId
}
