package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object OrganizationAccessNoticeTable : IdTable<String>("\"OrganizationAccessNotice\"") {
    override val id = text("id").entityId()
    val organizationId = text("organizationId")
    val kind = text("kind")
    val email = text("email")
    val name = text("name").nullable()
    val idp = text("idp").nullable()
    val dayKey = text("dayKey")
    val idpRefused = bool("idpRefused").default(false)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val mailedAt = pgTimestamp("mailedAt", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"OrganizationAccessNotice_pkey\"")

    init {
        uniqueIndex("\"OrganizationAccessNotice_organizationId_kind_email_dayKey_key\"", organizationId, kind, email, dayKey)
    }
}
