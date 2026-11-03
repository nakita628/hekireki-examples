package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class MastraKnowledgeNodesEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MastraKnowledgeNodesEntity>(MastraKnowledgeNodesTable)

    var type by MastraKnowledgeNodesTable.type
    var name by MastraKnowledgeNodesTable.name
    var canonicalName by MastraKnowledgeNodesTable.canonicalName
    var kind by MastraKnowledgeNodesTable.kind
    var content by MastraKnowledgeNodesTable.content
    var description by MastraKnowledgeNodesTable.description
    var scope by MastraKnowledgeNodesTable.scope
    var scopeKey by MastraKnowledgeNodesTable.scopeKey
    var version by MastraKnowledgeNodesTable.version
    var mergedInto by MastraKnowledgeNodesTable.mergedInto
    var createdAt by MastraKnowledgeNodesTable.createdAt
    var updatedAt by MastraKnowledgeNodesTable.updatedAt
    var createdAtZ by MastraKnowledgeNodesTable.createdAtZ
    var updatedAtZ by MastraKnowledgeNodesTable.updatedAtZ
}
