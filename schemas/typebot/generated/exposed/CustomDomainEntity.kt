package typebot.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class CustomDomainEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, CustomDomainEntity>(CustomDomainTable)

    var createdAt by CustomDomainTable.createdAt
    var workspaceId by CustomDomainTable.workspaceId
    var workspace by WorkspaceEntity referencedOn CustomDomainTable.workspaceId
}
