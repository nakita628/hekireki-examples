package formbricks.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object OrganizationTable : IdTable<String>("\"Organization\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val name = text("name")
    val whitelabel = jsonb<String>("whitelabel", { it }, { it }).default("{}")
    val isAISmartToolsEnabled = bool("isAISmartToolsEnabled").default(false)
    val displayTimeZone = text("displayTimeZone").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Organization_pkey\"")
}
