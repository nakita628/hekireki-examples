package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object InvitationCodeTable : IdTable<String>("\"InvitationCode\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val code = text("code").uniqueIndex("\"InvitationCode_code_key\"")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"InvitationCode_pkey\"")
}
