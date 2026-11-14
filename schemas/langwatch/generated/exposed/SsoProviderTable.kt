package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable

object SsoProviderTable : IdTable<String>("\"SsoProvider\"") {
    override val id = text("id").entityId()
    val issuer = text("issuer")
    val oidcConfig = text("oidcConfig").nullable()
    val samlConfig = text("samlConfig").nullable()
    val userId = text("userId").nullable()
    val providerId = text("providerId").uniqueIndex("\"SsoProvider_providerId_key\"")
    val organizationId = text("organizationId").nullable()
    val domain = text("domain")
    override val primaryKey = PrimaryKey(id, name = "\"SsoProvider_pkey\"")

    init {
        index("\"SsoProvider_organizationId_idx\"", false, organizationId)
        index("\"SsoProvider_domain_idx\"", false, domain)
    }
}
