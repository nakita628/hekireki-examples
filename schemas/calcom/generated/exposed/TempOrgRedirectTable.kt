package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object TempOrgRedirectTable : IdTable<Int>("\"TempOrgRedirect\"") {
    override val id = integer("id").autoIncrement().entityId()
    val from = text("from")
    val fromOrgId = integer("fromOrgId")
    val type = pgEnum("type", "\"RedirectType\"", RedirectType.entries, RedirectType::dbName)
    val toUrl = text("toUrl")
    val enabled = bool("enabled").default(true)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"TempOrgRedirect_pkey\"")

    init {
        uniqueIndex("\"TempOrgRedirect_from_type_fromOrgId_key\"", from, type, fromOrgId)
    }
}
