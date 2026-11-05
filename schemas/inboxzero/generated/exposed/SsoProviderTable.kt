package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object SsoProviderTable : IdTable<String>("\"ssoProvider\"") {
    val userId =
        optReference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ssoProvider_userId_fkey\"",
        )
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val issuer = text("issuer")
    val oidcConfig = text("oidcConfig").nullable()
    val samlConfig = text("samlConfig").nullable()
    val emailAccountId =
        optReference(
            "emailAccountId",
            EmailAccountTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ssoProvider_emailAccountId_fkey\"",
        )
    val providerId = text("providerId").uniqueIndex("\"ssoProvider_providerId_key\"")
    val organizationId =
        optReference(
            "organizationId",
            OrganizationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ssoProvider_organizationId_fkey\"",
        ).uniqueIndex("\"ssoProvider_organizationId_key\"")
    val domain = text("domain")
    override val primaryKey = PrimaryKey(id, name = "\"ssoProvider_pkey\"")

    init {
        index("\"ssoProvider_emailAccountId_idx\"", false, emailAccountId)
    }
}
