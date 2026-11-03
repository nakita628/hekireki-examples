package documenso.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object CscCredentialTable : IdTable<String>("\"CscCredential\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val providerId = text("providerId")
    val credentialId = text("credentialId")
    val certCache = binary("certCache").nullable()
    val signatureAlgorithm = text("signatureAlgorithm")
    val keyType = text("keyType")
    val digestAlgorithm = text("digestAlgorithm")
    val keyLenBits = integer("keyLenBits").nullable()
    val signAlgoParams = text("signAlgoParams").nullable()
    val serviceTokenCiphertext = binary("serviceTokenCiphertext").nullable()
    val serviceTokenExpiresAt = pgTimestamp("serviceTokenExpiresAt", 3).nullable()
    val recipientId =
        reference(
            "recipientId",
            RecipientTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"CscCredential_recipientId_fkey\"",
        ).uniqueIndex("\"CscCredential_recipientId_key\"")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"CscCredential_pkey\"")
}
