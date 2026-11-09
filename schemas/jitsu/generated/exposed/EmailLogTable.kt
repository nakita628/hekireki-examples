package jitsu.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object EmailLogTable : IdTable<String>("\"EmailLog\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val time = pgTimestamp("time", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val email = jsonb<String>("email", { it }, { it })
    val error = text("error").nullable()
    val previewUrl = text("previewUrl").nullable()
    val messageId = text("messageId").nullable()
    val status = text("status")
    override val primaryKey = PrimaryKey(id, name = "\"EmailLog_pkey\"")
}
