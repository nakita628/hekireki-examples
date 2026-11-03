package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class TagsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TagsEntity>(TagsTable)

    var name by TagsTable.name
    var color by TagsTable.color
    var orgId by TagsTable.orgId
    var deletedAt by TagsTable.deletedAt
    var createdAt by TagsTable.createdAt
    var updatedAt by TagsTable.updatedAt
    var organization by OrganizationEntity referencedOn TagsTable.orgId
    val posts by TagsPostsEntity referrersOn TagsPostsTable.tagId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TagsTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
