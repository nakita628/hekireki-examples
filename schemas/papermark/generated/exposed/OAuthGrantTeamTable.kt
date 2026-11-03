package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object OAuthGrantTeamTable : IdTable<String>("\"OAuthGrantTeam\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val grantId = text("grantId").uniqueIndex("\"OAuthGrantTeam_grantId_key\"")
    val teamId = text("teamId")
    val userId = text("userId")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"OAuthGrantTeam_pkey\"")

    init {
        index("\"OAuthGrantTeam_grantId_idx\"", false, grantId)
    }
}
