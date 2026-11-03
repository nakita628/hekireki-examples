package ghostfolio.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class AssetProfileSplitEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AssetProfileSplitEntity>(AssetProfileSplitTable)

    var createdAt by AssetProfileSplitTable.createdAt
    var date by AssetProfileSplitTable.date
    var denominator by AssetProfileSplitTable.denominator
    var numerator by AssetProfileSplitTable.numerator
    var symbolProfile by SymbolProfileEntity referencedOn AssetProfileSplitTable.symbolProfileId
    var symbolProfileId by AssetProfileSplitTable.symbolProfileId
    var updatedAt by AssetProfileSplitTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AssetProfileSplitTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
