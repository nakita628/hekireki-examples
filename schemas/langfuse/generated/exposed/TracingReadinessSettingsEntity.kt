package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class TracingReadinessSettingsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TracingReadinessSettingsEntity>(TracingReadinessSettingsTable)

    val project by ProjectEntity referencedOn TracingReadinessSettingsTable.id
    var createdAt by TracingReadinessSettingsTable.createdAt
    var updatedAt by TracingReadinessSettingsTable.updatedAt
    var createdBy by TracingReadinessSettingsTable.createdBy
    var createdByUser by UserEntity optionalReferencedOn TracingReadinessSettingsTable.createdBy
    var updatedBy by TracingReadinessSettingsTable.updatedBy
    var updatedByUser by UserEntity optionalReferencedOn TracingReadinessSettingsTable.updatedBy
    var traceIdleMs by TracingReadinessSettingsTable.traceIdleMs

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TracingReadinessSettingsTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
