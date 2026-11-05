package openpanel.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ResetPasswordEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ResetPasswordEntity>(ResetPasswordTable)

    var accountId by ResetPasswordTable.accountId
    var account by AccountEntity referencedOn ResetPasswordTable.accountId
    var expiresAt by ResetPasswordTable.expiresAt
    var createdAt by ResetPasswordTable.createdAt
    var updatedAt by ResetPasswordTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ResetPasswordTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
