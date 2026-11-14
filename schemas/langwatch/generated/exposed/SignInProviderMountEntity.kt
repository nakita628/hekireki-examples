package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SignInProviderMountEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SignInProviderMountEntity>(SignInProviderMountTable)

    var provider by SignInProviderMountTable.provider
    var mounted by SignInProviderMountTable.mounted
    var lastAppliedEventId by SignInProviderMountTable.lastAppliedEventId
    var updatedAt by SignInProviderMountTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SignInProviderMountTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
