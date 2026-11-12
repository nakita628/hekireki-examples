package blinko.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.json
import java.time.Instant
import java.time.temporal.ChronoUnit

object CacheTable : IdTable<Int>("cache") {
    override val id = integer("id").autoIncrement().entityId()
    val key = pgVarchar("key").uniqueIndex("cache_key_key")
    val value = json<String>("value", { it }, { it })
    val createdAt = pgTimestamptz("createdAt", 6).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamptz("updatedAt", 6).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "cache_pkey")
}
