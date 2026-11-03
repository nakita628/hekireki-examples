package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class DataMigrationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, DataMigrationEntity>(DataMigrationTable)

    var name by DataMigrationTable.name
    var createdAt by DataMigrationTable.createdAt
    var updatedAt by DataMigrationTable.updatedAt
    var completedAt by DataMigrationTable.completedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(DataMigrationTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
