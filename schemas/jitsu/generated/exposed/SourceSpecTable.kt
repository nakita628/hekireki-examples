package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object SourceSpecTable : CompositeIdTable("source_spec") {
    val `package` = text("package").entityId()
    val version = text("version").entityId()
    val specs = jsonb<String>("specs", { it }, { it }).nullable()
    val timestamp = pgTimestamptz("timestamp", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val error = text("error").nullable()
    override val primaryKey = PrimaryKey(`package`, version, name = "source_spec_pkey")
}
