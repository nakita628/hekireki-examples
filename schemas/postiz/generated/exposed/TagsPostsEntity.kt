package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import java.time.Instant
import java.time.temporal.ChronoUnit

class TagsPostsEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<TagsPostsEntity>(TagsPostsTable)

    val postId by TagsPostsTable.postId
    val tagId by TagsPostsTable.tagId
    var createdAt by TagsPostsTable.createdAt
    var updatedAt by TagsPostsTable.updatedAt
    val post by PostEntity referencedOn TagsPostsTable.postId
    val tag by TagsEntity referencedOn TagsPostsTable.tagId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TagsPostsTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
