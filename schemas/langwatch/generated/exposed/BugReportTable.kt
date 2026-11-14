package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object BugReportTable : IdTable<String>("\"BugReport\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val createdAt = pgTimestamptz("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val source1 = text("source")
    val kind = text("kind")
    val title = text("title")
    val summary = text("summary").nullable()
    val sessionData = text("sessionData").nullable()
    val sessionTruncated = bool("sessionTruncated").default(false)
    val agent = text("agent").nullable()
    val contactEmail = text("contactEmail").nullable()
    val cliVersion = text("cliVersion").nullable()
    val linkedProjectId = text("linkedProjectId").nullable()
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"BugReport_pkey\"")

    init {
        index("\"BugReport_createdAt_idx\"", false, createdAt)
    }
}
