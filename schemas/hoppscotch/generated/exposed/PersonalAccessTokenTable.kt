package hoppscotch.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object PersonalAccessTokenTable : IdTable<String>("\"PersonalAccessToken\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val userUid =
        reference(
            "userUid",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"PersonalAccessToken_userUid_fkey\"",
        )
    val label = text("label")
    val token = text("token").clientDefault { UUID.randomUUID().toString() }.uniqueIndex("\"PersonalAccessToken_token_key\"")
    val expiresOn = pgTimestamptz("expiresOn", 3).nullable()
    val createdOn = pgTimestamptz("createdOn", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedOn = pgTimestamptz("updatedOn", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"PersonalAccessToken_pkey\"")
}
