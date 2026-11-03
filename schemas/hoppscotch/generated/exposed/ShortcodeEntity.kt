package hoppscotch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ShortcodeEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ShortcodeEntity>(ShortcodeTable)

    var request by ShortcodeTable.request
    var creatorUid by ShortcodeTable.creatorUid
    var embedProperties by ShortcodeTable.embedProperties
    var createdOn by ShortcodeTable.createdOn
    var updatedOn by ShortcodeTable.updatedOn
    var user by UserEntity optionalReferencedOn ShortcodeTable.creatorUid

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ShortcodeTable.updatedOn)) {
                updatedOn = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
