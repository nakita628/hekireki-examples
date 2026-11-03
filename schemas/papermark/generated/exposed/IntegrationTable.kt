package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object IntegrationTable : IdTable<String>("\"Integration\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val name = text("name")
    val slug = text("slug").uniqueIndex("\"Integration_slug_key\"")
    val description = text("description").nullable()
    val readme = text("readme").nullable()
    val developer = text("developer")
    val website = text("website")
    val logo = text("logo").nullable()
    val screenshots = jsonb<String>("screenshots", { it }, { it }).nullable()
    val verified = bool("verified").default(false)
    val installUrl = text("installUrl").nullable()
    val category = text("category").nullable()
    val comingSoon = bool("comingSoon").default(false)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"Integration_pkey\"")
}
