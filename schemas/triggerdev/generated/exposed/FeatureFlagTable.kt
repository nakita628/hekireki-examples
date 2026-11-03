package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object FeatureFlagTable : IdTable<String>("\"FeatureFlag\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val key = text("key").uniqueIndex("\"FeatureFlag_key_key\"")
    val value = jsonb<String>("value", { it }, { it }).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"FeatureFlag_pkey\"")
}
