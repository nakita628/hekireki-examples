package hoppscotch.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object MockServerLogTable : IdTable<String>("\"MockServerLog\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val mockServerID =
        reference(
            "mockServerID",
            MockServerTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"MockServerLog_mockServerID_fkey\"",
        )
    val requestMethod = text("requestMethod")
    val requestPath = text("requestPath")
    val requestHeaders = jsonb<String>("requestHeaders", { it }, { it })
    val requestBody = jsonb<String>("requestBody", { it }, { it }).nullable()
    val requestQuery = jsonb<String>("requestQuery", { it }, { it }).nullable()
    val responseStatus = integer("responseStatus")
    val responseHeaders = jsonb<String>("responseHeaders", { it }, { it })
    val responseBody = jsonb<String>("responseBody", { it }, { it }).nullable()
    val responseTime = integer("responseTime")
    val ipAddress = text("ipAddress").nullable()
    val userAgent = text("userAgent").nullable()
    val executedAt = pgTimestamptz("executedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"MockServerLog_pkey\"")

    init {
        index("\"MockServerLog_mockServerID_idx\"", false, mockServerID)
        index("\"MockServerLog_mockServerID_executedAt_idx\"", false, mockServerID, executedAt)
    }
}
