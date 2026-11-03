package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class MastraAgentVersionsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MastraAgentVersionsEntity>(MastraAgentVersionsTable)

    var agentId by MastraAgentVersionsTable.agentId
    var versionNumber by MastraAgentVersionsTable.versionNumber
    var name by MastraAgentVersionsTable.name
    var description by MastraAgentVersionsTable.description
    var instructions by MastraAgentVersionsTable.instructions
    var model by MastraAgentVersionsTable.model
    var tools by MastraAgentVersionsTable.tools
    var defaultOptions by MastraAgentVersionsTable.defaultOptions
    var workflows by MastraAgentVersionsTable.workflows
    var agents by MastraAgentVersionsTable.agents
    var integrationTools by MastraAgentVersionsTable.integrationTools
    var inputProcessors by MastraAgentVersionsTable.inputProcessors
    var outputProcessors by MastraAgentVersionsTable.outputProcessors
    var memory by MastraAgentVersionsTable.memory
    var scorers by MastraAgentVersionsTable.scorers
    var mcpClients by MastraAgentVersionsTable.mcpClients
    var requestContextSchema by MastraAgentVersionsTable.requestContextSchema
    var workspace by MastraAgentVersionsTable.workspace
    var skills by MastraAgentVersionsTable.skills
    var skillsFormat by MastraAgentVersionsTable.skillsFormat
    var changedFields by MastraAgentVersionsTable.changedFields
    var changeMessage by MastraAgentVersionsTable.changeMessage
    var createdAt by MastraAgentVersionsTable.createdAt
    var createdAtZ by MastraAgentVersionsTable.createdAtZ
    var durable by MastraAgentVersionsTable.durable
    var browser by MastraAgentVersionsTable.browser
    var toolProviders by MastraAgentVersionsTable.toolProviders
}
