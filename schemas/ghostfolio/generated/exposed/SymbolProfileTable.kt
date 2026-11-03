package ghostfolio.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object SymbolProfileTable : IdTable<String>("\"SymbolProfile\"") {
    val assetClass = pgEnum("assetClass", "\"AssetClass\"", AssetClass.entries, AssetClass::dbName).nullable()
    val assetSubClass = pgEnum("assetSubClass", "\"AssetSubClass\"", AssetSubClass.entries, AssetSubClass::dbName).nullable()
    val comment = text("comment").nullable()
    val countries = jsonb<String>("countries", { it }, { it }).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val currency = text("currency")
    val cusip = text("cusip").nullable()
    val dataGatheringFrequency =
        pgEnum("dataGatheringFrequency", "\"DataGatheringFrequency\"", DataGatheringFrequency.entries, DataGatheringFrequency::dbName)
            .default(DataGatheringFrequency.DAILY)
    val dataSource = pgEnum("dataSource", "\"DataSource\"", DataSource.entries, DataSource::dbName)
    val figi = text("figi").nullable()
    val figiComposite = text("figiComposite").nullable()
    val figiShareClass = text("figiShareClass").nullable()
    val holdings = jsonb<String>("holdings", { it }, { it }).nullable().default("[]")
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val isActive = bool("isActive").default(true)
    val isin = text("isin").nullable()
    val name = text("name").nullable()
    val scraperConfiguration = jsonb<String>("scraperConfiguration", { it }, { it }).nullable()
    val sectors = jsonb<String>("sectors", { it }, { it }).nullable()
    val symbol = text("symbol")
    val symbolMapping = jsonb<String>("symbolMapping", { it }, { it }).nullable()
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val url = text("url").nullable()
    val userId =
        optReference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"SymbolProfile_userId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"SymbolProfile_pkey\"")

    init {
        index("\"SymbolProfile_assetClass_idx\"", false, assetClass)
        index("\"SymbolProfile_currency_idx\"", false, currency)
        index("\"SymbolProfile_cusip_idx\"", false, cusip)
        index("\"SymbolProfile_dataGatheringFrequency_idx\"", false, dataGatheringFrequency)
        index("\"SymbolProfile_dataSource_idx\"", false, dataSource)
        index("\"SymbolProfile_isActive_idx\"", false, isActive)
        index("\"SymbolProfile_isin_idx\"", false, isin)
        index("\"SymbolProfile_name_idx\"", false, name)
        index("\"SymbolProfile_symbol_idx\"", false, symbol)
        uniqueIndex("\"SymbolProfile_dataSource_symbol_key\"", dataSource, symbol)
    }
}
