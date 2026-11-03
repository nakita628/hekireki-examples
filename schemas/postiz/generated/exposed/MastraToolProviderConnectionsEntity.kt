package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class MastraToolProviderConnectionsEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<MastraToolProviderConnectionsEntity>(MastraToolProviderConnectionsTable)

    val authorId by MastraToolProviderConnectionsTable.authorId
    val providerId by MastraToolProviderConnectionsTable.providerId
    val connectionId by MastraToolProviderConnectionsTable.connectionId
    var toolkit by MastraToolProviderConnectionsTable.toolkit
    var label by MastraToolProviderConnectionsTable.label
    var scope by MastraToolProviderConnectionsTable.scope
    var createdAt by MastraToolProviderConnectionsTable.createdAt
    var updatedAt by MastraToolProviderConnectionsTable.updatedAt
    var createdAtZ by MastraToolProviderConnectionsTable.createdAtZ
    var updatedAtZ by MastraToolProviderConnectionsTable.updatedAtZ
}
