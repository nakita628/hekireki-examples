package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object AuthorizationCodeTable : IdTable<String>("\"AuthorizationCode\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val code = text("code").uniqueIndex("\"AuthorizationCode_code_key\"")
    val personalAccessTokenId =
        optReference(
            "personalAccessTokenId",
            PersonalAccessTokenTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"AuthorizationCode_personalAccessTokenId_fkey\"",
        )
    val requesterIp = text("requesterIp").nullable()
    val codeChallenge = text("codeChallenge").nullable()
    val callbackPort = integer("callbackPort").nullable()
    val approvalCodeHash = text("approvalCodeHash").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"AuthorizationCode_pkey\"")
}
