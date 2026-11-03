package ghostfolio.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class AssetProfileResolutionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AssetProfileResolutionEntity>(AssetProfileResolutionTable)

    var createdAt by AssetProfileResolutionTable.createdAt
    var currency by AssetProfileResolutionTable.currency
    var dataSourceOrigin by AssetProfileResolutionTable.dataSourceOrigin
    var dataSourceTarget by AssetProfileResolutionTable.dataSourceTarget
    var requestCount by AssetProfileResolutionTable.requestCount
    var symbolOrigin by AssetProfileResolutionTable.symbolOrigin
    var symbolTarget by AssetProfileResolutionTable.symbolTarget
    var updatedAt by AssetProfileResolutionTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AssetProfileResolutionTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
