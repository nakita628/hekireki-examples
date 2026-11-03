package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class MastraKnowledgeMentionsEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<MastraKnowledgeMentionsEntity>(MastraKnowledgeMentionsTable)

    val sourceType by MastraKnowledgeMentionsTable.sourceType
    val sourceId by MastraKnowledgeMentionsTable.sourceId
    val recordId by MastraKnowledgeMentionsTable.recordId
}
