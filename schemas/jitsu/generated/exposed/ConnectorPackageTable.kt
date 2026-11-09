package jitsu.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object ConnectorPackageTable : IdTable<String>("\"ConnectorPackage\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val packageId = text("packageId")
    val packageType = text("packageType").default("airbyte")
    val meta = jsonb<String>("meta", { it }, { it }).nullable()
    val logoSvg = binary("logoSvg").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"ConnectorPackage_pkey\"")
}
