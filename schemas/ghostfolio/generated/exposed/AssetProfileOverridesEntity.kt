package ghostfolio.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class AssetProfileOverridesEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AssetProfileOverridesEntity>(AssetProfileOverridesTable)

    var assetClass by AssetProfileOverridesTable.assetClass
    var assetSubClass by AssetProfileOverridesTable.assetSubClass
    var countries by AssetProfileOverridesTable.countries
    var holdings by AssetProfileOverridesTable.holdings
    var name by AssetProfileOverridesTable.name
    var sectors by AssetProfileOverridesTable.sectors
    val symbolProfile by SymbolProfileEntity referencedOn AssetProfileOverridesTable.id
    var updatedAt by AssetProfileOverridesTable.updatedAt
    var url by AssetProfileOverridesTable.url

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AssetProfileOverridesTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
