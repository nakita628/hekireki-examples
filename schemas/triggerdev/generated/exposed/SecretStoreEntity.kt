package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SecretStoreEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SecretStoreEntity>(SecretStoreTable)

    var value by SecretStoreTable.value
    var version by SecretStoreTable.version
    var createdAt by SecretStoreTable.createdAt
    var updatedAt by SecretStoreTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SecretStoreTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
