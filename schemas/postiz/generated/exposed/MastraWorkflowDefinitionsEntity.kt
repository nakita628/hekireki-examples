package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class MastraWorkflowDefinitionsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MastraWorkflowDefinitionsEntity>(MastraWorkflowDefinitionsTable)

    var description by MastraWorkflowDefinitionsTable.description
    var metadata by MastraWorkflowDefinitionsTable.metadata
    var inputSchema by MastraWorkflowDefinitionsTable.inputSchema
    var outputSchema by MastraWorkflowDefinitionsTable.outputSchema
    var stateSchema by MastraWorkflowDefinitionsTable.stateSchema
    var requestContextSchema by MastraWorkflowDefinitionsTable.requestContextSchema
    var graph by MastraWorkflowDefinitionsTable.graph
    var schedule by MastraWorkflowDefinitionsTable.schedule
    var status by MastraWorkflowDefinitionsTable.status
    var source by MastraWorkflowDefinitionsTable.source1
    var authorId by MastraWorkflowDefinitionsTable.authorId
    var createdAt by MastraWorkflowDefinitionsTable.createdAt
    var updatedAt by MastraWorkflowDefinitionsTable.updatedAt
    var createdAtZ by MastraWorkflowDefinitionsTable.createdAtZ
    var updatedAtZ by MastraWorkflowDefinitionsTable.updatedAtZ
}
