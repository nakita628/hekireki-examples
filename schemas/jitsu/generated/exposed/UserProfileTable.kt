package jitsu.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object UserProfileTable : IdTable<String>("\"UserProfile\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val name = text("name")
    val email = text("email")
    val admin = bool("admin").nullable().default(false)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val loginProvider = text("loginProvider")
    val externalUsername = text("externalUsername").nullable()
    val externalId = text("externalId")
    override val primaryKey = PrimaryKey(id, name = "\"UserProfile_pkey\"")

    init {
        index("\"UserProfile_externalId_idx\"", false, externalId)
        uniqueIndex("\"UserProfile_loginProvider_externalId_key\"", loginProvider, externalId)
    }
}
