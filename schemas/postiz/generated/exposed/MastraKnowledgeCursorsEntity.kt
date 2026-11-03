package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class MastraKnowledgeCursorsEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<MastraKnowledgeCursorsEntity>(MastraKnowledgeCursorsTable)

    val sourceThreadId by MastraKnowledgeCursorsTable.sourceThreadId
    val agent by MastraKnowledgeCursorsTable.agent
    var lastKnowledgeId by MastraKnowledgeCursorsTable.lastKnowledgeId
    var updatedAt by MastraKnowledgeCursorsTable.updatedAt
    var updatedAtZ by MastraKnowledgeCursorsTable.updatedAtZ
}
