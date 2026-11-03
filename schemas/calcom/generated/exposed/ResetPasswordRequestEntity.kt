package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ResetPasswordRequestEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ResetPasswordRequestEntity>(ResetPasswordRequestTable)

    var createdAt by ResetPasswordRequestTable.createdAt
    var updatedAt by ResetPasswordRequestTable.updatedAt
    var email by ResetPasswordRequestTable.email
    var expires by ResetPasswordRequestTable.expires

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ResetPasswordRequestTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
