package openpanel.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

class ReportLayoutEntity(
    id: EntityID<UUID>,
) : Entity<UUID>(id) {
    companion object : EntityClass<UUID, ReportLayoutEntity>(ReportLayoutTable)

    var reportId by ReportLayoutTable.reportId
    var report by ReportEntity referencedOn ReportLayoutTable.reportId
    var gridColumns by ReportLayoutTable.gridColumns
    var x by ReportLayoutTable.x
    var y by ReportLayoutTable.y
    var w by ReportLayoutTable.w
    var h by ReportLayoutTable.h
    var minW by ReportLayoutTable.minW
    var minH by ReportLayoutTable.minH
    var maxW by ReportLayoutTable.maxW
    var maxH by ReportLayoutTable.maxH
    var createdAt by ReportLayoutTable.createdAt
    var updatedAt by ReportLayoutTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ReportLayoutTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
