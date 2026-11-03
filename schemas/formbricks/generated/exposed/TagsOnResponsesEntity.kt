package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class TagsOnResponsesEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<TagsOnResponsesEntity>(TagsOnResponsesTable)

    val responseId by TagsOnResponsesTable.responseId
    val response by ResponseEntity referencedOn TagsOnResponsesTable.responseId
    val tagId by TagsOnResponsesTable.tagId
    val tag by TagEntity referencedOn TagsOnResponsesTable.tagId
}
