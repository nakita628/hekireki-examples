package ghostfolio.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SymbolProfileEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SymbolProfileEntity>(SymbolProfileTable)

    val activities by OrderEntity referrersOn OrderTable.symbolProfileId
    var assetClass by SymbolProfileTable.assetClass
    val assetProfileOverrides by AssetProfileOverridesEntity optionalBackReferencedOn AssetProfileOverridesTable.id
    val assetProfileSplits by AssetProfileSplitEntity referrersOn AssetProfileSplitTable.symbolProfileId
    var assetSubClass by SymbolProfileTable.assetSubClass
    var comment by SymbolProfileTable.comment
    var countries by SymbolProfileTable.countries
    var createdAt by SymbolProfileTable.createdAt
    var currency by SymbolProfileTable.currency
    var cusip by SymbolProfileTable.cusip
    var dataGatheringFrequency by SymbolProfileTable.dataGatheringFrequency
    var dataSource by SymbolProfileTable.dataSource
    var figi by SymbolProfileTable.figi
    var figiComposite by SymbolProfileTable.figiComposite
    var figiShareClass by SymbolProfileTable.figiShareClass
    var holdings by SymbolProfileTable.holdings
    var isActive by SymbolProfileTable.isActive
    var isin by SymbolProfileTable.isin
    var name by SymbolProfileTable.name
    var scraperConfiguration by SymbolProfileTable.scraperConfiguration
    var sectors by SymbolProfileTable.sectors
    var symbol by SymbolProfileTable.symbol
    var symbolMapping by SymbolProfileTable.symbolMapping
    var updatedAt by SymbolProfileTable.updatedAt
    var url by SymbolProfileTable.url
    var user by UserEntity optionalReferencedOn SymbolProfileTable.userId
    var userId by SymbolProfileTable.userId
    var watchedBy by UserEntity via UserWatchlistTable

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SymbolProfileTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
