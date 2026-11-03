package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object OAuthClientApprovalTable : IdTable<String>("\"OAuthClientApproval\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val userId = text("userId")
    val clientId = text("clientId")
    val approvedAt = pgTimestamp("approvedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val revokedAt = pgTimestamp("revokedAt", 3).nullable()
    val clientMetadataHash = text("clientMetadataHash").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"OAuthClientApproval_pkey\"")

    init {
        index("\"OAuthClientApproval_clientId_idx\"", false, clientId)
        uniqueIndex("\"OAuthClientApproval_userId_clientId_key\"", userId, clientId)
    }
}
