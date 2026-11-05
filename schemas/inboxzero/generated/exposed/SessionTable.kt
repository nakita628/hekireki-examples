package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object SessionTable : IdTable<String>("\"Session\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val sessionToken = text("sessionToken").uniqueIndex("\"Session_sessionToken_key\"")
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Session_userId_fkey\"",
        )
    val expires = pgTimestamp("expires", 3)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val emailOtp = bool("emailOtp").default(false)
    val emailOtpVersion = integer("emailOtpVersion").default(0)
    val ipAddress = text("ipAddress").nullable()
    val userAgent = text("userAgent").nullable()
    val activeOrganizationId =
        optReference(
            "activeOrganizationId",
            OrganizationTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Session_activeOrganizationId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"Session_pkey\"")

    init {
        index("\"Session_userId_idx\"", false, userId)
        index("\"Session_activeOrganizationId_idx\"", false, activeOrganizationId)
    }
}
