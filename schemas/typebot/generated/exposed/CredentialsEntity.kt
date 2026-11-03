package typebot.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class CredentialsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, CredentialsEntity>(CredentialsTable)

    var createdAt by CredentialsTable.createdAt
    var workspaceId by CredentialsTable.workspaceId
    var data by CredentialsTable.data
    var name by CredentialsTable.name
    var type by CredentialsTable.type
    var iv by CredentialsTable.iv
    var workspace by WorkspaceEntity referencedOn CredentialsTable.workspaceId
}
