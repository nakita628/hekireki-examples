package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class FunctionsServerEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<FunctionsServerEntity>(FunctionsServerTable)

    val workspaceId by FunctionsServerTable.workspaceId
    val `class` by FunctionsServerTable.`class`
    var deploymentId by FunctionsServerTable.deploymentId
    var connections by FunctionsServerTable.connections
    var emptyConnections by FunctionsServerTable.emptyConnections
    var profileBuilders by FunctionsServerTable.profileBuilders
    var createdAt by FunctionsServerTable.createdAt
    var updatedAt by FunctionsServerTable.updatedAt
}
