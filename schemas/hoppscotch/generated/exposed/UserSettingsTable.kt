package hoppscotch.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object UserSettingsTable : IdTable<String>("\"UserSettings\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val userUid =
        reference(
            "userUid",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"UserSettings_userUid_fkey\"",
        ).uniqueIndex("\"UserSettings_userUid_key\"")
    val properties = jsonb<String>("properties", { it }, { it })
    val updatedOn = pgTimestamptz("updatedOn", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"UserSettings_pkey\"")
}
