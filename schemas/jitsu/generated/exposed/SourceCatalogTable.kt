package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object SourceCatalogTable : CompositeIdTable("source_catalog") {
    val `package` = text("package").entityId()
    val version = text("version").entityId()
    val key = text("key").entityId()
    val catalog = jsonb<String>("catalog", { it }, { it }).nullable()
    val timestamp = pgTimestamptz("timestamp", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val status = text("status")
    val description = text("description").nullable()
    override val primaryKey = PrimaryKey(`package`, version, key, name = "source_catalog_pkey")
}
