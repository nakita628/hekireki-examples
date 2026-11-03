package ghostfolio.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object AnalyticsTable : IdTable<String>("\"Analytics\"") {
    val activityCount = integer("activityCount").default(0)
    val country = text("country").nullable()
    val dataProviderGhostfolioDailyRequests = integer("dataProviderGhostfolioDailyRequests").default(0)
    val lastRequestAt =
        pgTimestamp("lastRequestAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastRequestClient = pgEnum("lastRequestClient", "\"RequestClient\"", RequestClient.entries, RequestClient::dbName).nullable()
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val id =
        text("userId")
            .references(
                UserTable.id,
                onDelete = ReferenceOption.CASCADE,
                onUpdate = ReferenceOption.CASCADE,
                fkName = "\"Analytics_userId_fkey\"",
            ).entityId()
    override val primaryKey = PrimaryKey(id, name = "\"Analytics_pkey\"")

    init {
        index("\"Analytics_lastRequestAt_idx\"", false, lastRequestAt)
        index("\"Analytics_updatedAt_idx\"", false, updatedAt)
    }
}
