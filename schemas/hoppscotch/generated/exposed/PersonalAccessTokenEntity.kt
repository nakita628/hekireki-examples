package hoppscotch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class PersonalAccessTokenEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, PersonalAccessTokenEntity>(PersonalAccessTokenTable)

    var userUid by PersonalAccessTokenTable.userUid
    var label by PersonalAccessTokenTable.label
    var token by PersonalAccessTokenTable.token
    var expiresOn by PersonalAccessTokenTable.expiresOn
    var createdOn by PersonalAccessTokenTable.createdOn
    var updatedOn by PersonalAccessTokenTable.updatedOn
    var user by UserEntity referencedOn PersonalAccessTokenTable.userUid

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(PersonalAccessTokenTable.updatedOn)) {
                updatedOn = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
