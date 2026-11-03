package hoppscotch.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object UserTable : IdTable<String>("\"User\"") {
    override val id = text("uid").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val displayName = text("displayName").nullable()
    val email = text("email").nullable().uniqueIndex("\"User_email_key\"")
    val photoURL = text("photoURL").nullable()
    val isAdmin = bool("isAdmin").default(false)
    val refreshToken = text("refreshToken").nullable()
    val currentRESTSession = jsonb<String>("currentRESTSession", { it }, { it }).nullable()
    val currentGQLSession = jsonb<String>("currentGQLSession", { it }, { it }).nullable()
    val createdOn = pgTimestamptz("createdOn", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastLoggedOn = pgTimestamptz("lastLoggedOn", 3).nullable()
    val lastActiveOn = pgTimestamptz("lastActiveOn", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"User_pkey\"")
}
