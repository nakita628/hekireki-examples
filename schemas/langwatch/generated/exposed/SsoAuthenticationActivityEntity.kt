package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class SsoAuthenticationActivityEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SsoAuthenticationActivityEntity>(SsoAuthenticationActivityTable)

    var organizationId by SsoAuthenticationActivityTable.organizationId
    var connectionId by SsoAuthenticationActivityTable.connectionId
    var userId by SsoAuthenticationActivityTable.userId
    var authenticatedAt by SsoAuthenticationActivityTable.authenticatedAt
    var providerAccountId by SsoAuthenticationActivityTable.providerAccountId
}
