package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import java.time.Instant
import java.time.temporal.ChronoUnit

class SystemMigrationTenantStateEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<SystemMigrationTenantStateEntity>(SystemMigrationTenantStateTable)

    val migrationName by SystemMigrationTenantStateTable.migrationName
    val tenantId by SystemMigrationTenantStateTable.tenantId
    var status by SystemMigrationTenantStateTable.status
    var report by SystemMigrationTenantStateTable.report
    var occurredAt by SystemMigrationTenantStateTable.occurredAt
    var createdAt by SystemMigrationTenantStateTable.createdAt
    var updatedAt by SystemMigrationTenantStateTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SystemMigrationTenantStateTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
