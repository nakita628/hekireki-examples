package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class CustomGraphEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, CustomGraphEntity>(CustomGraphTable)

    var projectId by CustomGraphTable.projectId
    var name by CustomGraphTable.name
    var graph by CustomGraphTable.graph
    var filters by CustomGraphTable.filters
    var kind by CustomGraphTable.kind
    var createdAt by CustomGraphTable.createdAt
    var updatedAt by CustomGraphTable.updatedAt
    var dashboardId by CustomGraphTable.dashboardId
    var gridColumn by CustomGraphTable.gridColumn
    var gridRow by CustomGraphTable.gridRow
    var colSpan by CustomGraphTable.colSpan
    var rowSpan by CustomGraphTable.rowSpan

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(CustomGraphTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
