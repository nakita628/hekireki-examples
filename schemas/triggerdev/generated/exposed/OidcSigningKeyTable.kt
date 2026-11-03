package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object OidcSigningKeyTable : IdTable<String>("\"OidcSigningKey\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val kid = text("kid").uniqueIndex("\"OidcSigningKey_kid_key\"")
    val purpose =
        pgEnum("purpose", "\"OidcSigningKeyPurpose\"", OidcSigningKeyPurpose.entries, OidcSigningKeyPurpose::dbName).default(
            OidcSigningKeyPurpose.EXTERNAL_OIDC,
        )
    val status = pgEnum("status", "\"OidcSigningKeyStatus\"", OidcSigningKeyStatus.entries, OidcSigningKeyStatus::dbName)
    val publicJwk = jsonb<String>("publicJwk", { it }, { it })
    val readyAt = pgTimestamp("readyAt", 3)
    val publicUntil = pgTimestamp("publicUntil", 3).nullable()
    val generatedByUserId = text("generatedByUserId")
    val activatedAt = pgTimestamp("activatedAt", 3).nullable()
    val activatedByUserId = text("activatedByUserId").nullable()
    val retiredAt = pgTimestamp("retiredAt", 3).nullable()
    val retiredByUserId = text("retiredByUserId").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"OidcSigningKey_pkey\"")
}
