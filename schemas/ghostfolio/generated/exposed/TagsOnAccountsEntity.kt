package ghostfolio.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import java.time.Instant
import java.time.temporal.ChronoUnit

class TagsOnAccountsEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<TagsOnAccountsEntity>(TagsOnAccountsTable)

    val account by AccountEntity referencedOn TagsOnAccountsTable
    val accountId by TagsOnAccountsTable.accountId
    var createdAt by TagsOnAccountsTable.createdAt
    val tag by TagEntity referencedOn TagsOnAccountsTable.tagId
    val tagId by TagsOnAccountsTable.tagId
    var updatedAt by TagsOnAccountsTable.updatedAt
    val userId by TagsOnAccountsTable.userId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TagsOnAccountsTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
