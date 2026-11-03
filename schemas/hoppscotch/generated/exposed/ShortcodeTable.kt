package hoppscotch.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object ShortcodeTable : IdTable<String>("\"Shortcode\"") {
    override val id = text("id").entityId()
    val request = jsonb<String>("request", { it }, { it })
    val creatorUid =
        optReference(
            "creatorUid",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Shortcode_creatorUid_fkey\"",
        )
    val embedProperties = jsonb<String>("embedProperties", { it }, { it }).nullable()
    val createdOn = pgTimestamptz("createdOn", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedOn = pgTimestamptz("updatedOn", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"Shortcode_pkey\"")

    init {
        uniqueIndex("\"Shortcode_id_key\"", id)
        uniqueIndex("\"Shortcode_id_creatorUid_key\"", id, creatorUid)
    }
}
