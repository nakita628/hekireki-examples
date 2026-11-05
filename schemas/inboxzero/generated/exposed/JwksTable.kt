package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object JwksTable : IdTable<String>("\"Jwks\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val publicKey = text("publicKey")
    val privateKey = text("privateKey")
    val createdAt = pgTimestamp("createdAt", 3)
    val expiresAt = pgTimestamp("expiresAt", 3).nullable()
    val alg = text("alg").nullable()
    val crv = text("crv").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Jwks_pkey\"")
}
