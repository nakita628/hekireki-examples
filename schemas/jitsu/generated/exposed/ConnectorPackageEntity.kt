package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ConnectorPackageEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ConnectorPackageEntity>(ConnectorPackageTable)

    var packageId by ConnectorPackageTable.packageId
    var packageType by ConnectorPackageTable.packageType
    var meta by ConnectorPackageTable.meta
    var logoSvg by ConnectorPackageTable.logoSvg
    var createdAt by ConnectorPackageTable.createdAt
    var updatedAt by ConnectorPackageTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ConnectorPackageTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
