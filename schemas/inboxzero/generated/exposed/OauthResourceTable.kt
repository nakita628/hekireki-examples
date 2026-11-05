package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object OauthResourceTable : IdTable<String>("\"OauthResource\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val identifier = text("identifier").uniqueIndex("\"OauthResource_identifier_key\"")
    val name = text("name")
    val accessTokenTtl = integer("accessTokenTtl").nullable()
    val refreshTokenTtl = integer("refreshTokenTtl").nullable()
    val signingAlgorithm = text("signingAlgorithm").nullable()
    val signingKeyId = text("signingKeyId").nullable()
    val allowedScopes = array("allowedScopes", TextColumnType()).nullable()
    val customClaims = jsonb<String>("customClaims", { it }, { it }).nullable()
    val dpopBoundAccessTokensRequired = bool("dpopBoundAccessTokensRequired").nullable().default(false)
    val disabled = bool("disabled").nullable().default(false)
    val createdAt = pgTimestamp("createdAt", 3).nullable()
    val updatedAt = pgTimestamp("updatedAt", 3).nullable()
    val policyVersion = integer("policyVersion").nullable().default(1)
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"OauthResource_pkey\"")
}
