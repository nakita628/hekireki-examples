package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class IntegrationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, IntegrationEntity>(IntegrationTable)

    var type by IntegrationTable.type
    var config by IntegrationTable.config
    var workspace by WorkspaceEntity referencedOn IntegrationTable.workspaceId
    var workspaceId by IntegrationTable.workspaceId
}
