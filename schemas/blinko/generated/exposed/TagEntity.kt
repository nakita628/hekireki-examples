package blinko.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class TagEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, TagEntity>(TagTable)

    var name by TagTable.name
    var icon by TagTable.icon
    var parent by TagTable.parent
    var accountId by TagTable.accountId
    var createdAt by TagTable.createdAt
    var updatedAt by TagTable.updatedAt
    val tagsToNote by TagsToNoteEntity referrersOn TagsToNoteTable.tagId
    var sortOrder by TagTable.sortOrder
    var account by AccountsEntity optionalReferencedOn TagTable.accountId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TagTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
