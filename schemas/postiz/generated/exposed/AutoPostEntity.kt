package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class AutoPostEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AutoPostEntity>(AutoPostTable)

    var organizationId by AutoPostTable.organizationId
    var title by AutoPostTable.title
    var content by AutoPostTable.content
    var onSlot by AutoPostTable.onSlot
    var syncLast by AutoPostTable.syncLast
    var url by AutoPostTable.url
    var lastUrl by AutoPostTable.lastUrl
    var active by AutoPostTable.active
    var addPicture by AutoPostTable.addPicture
    var generateContent by AutoPostTable.generateContent
    var integrations by AutoPostTable.integrations
    var deletedAt by AutoPostTable.deletedAt
    var createdAt by AutoPostTable.createdAt
    var updatedAt by AutoPostTable.updatedAt
    var organization by OrganizationEntity referencedOn AutoPostTable.organizationId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AutoPostTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
