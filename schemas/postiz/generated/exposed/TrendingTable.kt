package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object TrendingTable : IdTable<String>("\"Trending\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val trendingList = text("trendingList")
    val language = text("language").nullable().uniqueIndex("\"Trending_language_key\"")
    val hash = text("hash")
    val date = pgTimestamp("date", 3)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"Trending_pkey\"")

    init {
        index("\"Trending_hash_idx\"", false, hash)
    }
}
