package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object OrganizationTable : IdTable<String>("\"Organization\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val name = text("name")
    val slug = text("slug").uniqueIndex("\"Organization_slug_key\"")
    val logo = text("logo").nullable()
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"Organization_pkey\"")
}
