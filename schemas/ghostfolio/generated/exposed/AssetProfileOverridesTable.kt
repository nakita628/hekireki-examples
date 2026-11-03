package ghostfolio.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object AssetProfileOverridesTable : IdTable<String>("\"SymbolProfileOverrides\"") {
    val assetClass = pgEnum("assetClass", "\"AssetClass\"", AssetClass.entries, AssetClass::dbName).nullable()
    val assetSubClass = pgEnum("assetSubClass", "\"AssetSubClass\"", AssetSubClass.entries, AssetSubClass::dbName).nullable()
    val countries = jsonb<String>("countries", { it }, { it }).nullable().default("[]")
    val holdings = jsonb<String>("holdings", { it }, { it }).nullable().default("[]")
    val name = text("name").nullable()
    val sectors = jsonb<String>("sectors", { it }, { it }).nullable().default("[]")
    override val id =
        text("symbolProfileId")
            .references(
                SymbolProfileTable.id,
                onDelete = ReferenceOption.CASCADE,
                onUpdate = ReferenceOption.CASCADE,
                fkName = "\"SymbolProfileOverrides_symbolProfileId_fkey\"",
            ).entityId()
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val url = text("url").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"SymbolProfileOverrides_pkey\"")
}
