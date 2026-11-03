package ghostfolio.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object AssetProfileSplitTable : IdTable<String>("\"AssetProfileSplit\"") {
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val date = pgTimestamp("date", 3)
    val denominator = integer("denominator")
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val numerator = integer("numerator")
    val symbolProfileId =
        reference(
            "symbolProfileId",
            SymbolProfileTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"AssetProfileSplit_symbolProfileId_fkey\"",
        )
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"AssetProfileSplit_pkey\"")

    init {
        index("\"AssetProfileSplit_date_idx\"", false, date)
        uniqueIndex("\"AssetProfileSplit_symbolProfileId_date_key\"", symbolProfileId, date)
    }
}
