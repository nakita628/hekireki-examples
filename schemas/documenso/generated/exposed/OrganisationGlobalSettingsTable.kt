package documenso.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object OrganisationGlobalSettingsTable : IdTable<String>("\"OrganisationGlobalSettings\"") {
    override val id = text("id").entityId()
    val documentVisibility =
        pgEnum("documentVisibility", "\"DocumentVisibility\"", DocumentVisibility.entries, DocumentVisibility::dbName).default(
            DocumentVisibility.EVERYONE,
        )
    val documentLanguage = text("documentLanguage").default("en")
    val includeSenderDetails = bool("includeSenderDetails").default(true)
    val includeSigningCertificate = bool("includeSigningCertificate").default(true)
    val includeAuditLog = bool("includeAuditLog").default(false)
    val documentTimezone = text("documentTimezone").nullable()
    val documentDateFormat = text("documentDateFormat").default("yyyy-MM-dd hh:mm a")
    val delegateDocumentOwnership = bool("delegateDocumentOwnership").default(false)
    val typedSignatureEnabled = bool("typedSignatureEnabled").default(true)
    val uploadSignatureEnabled = bool("uploadSignatureEnabled").default(true)
    val drawSignatureEnabled = bool("drawSignatureEnabled").default(true)
    val defaultRecipients = jsonb<String>("defaultRecipients", { it }, { it }).nullable()
    val emailId =
        optReference(
            "emailId",
            OrganisationEmailTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OrganisationGlobalSettings_emailId_fkey\"",
        )
    val emailReplyTo = text("emailReplyTo").nullable()
    val emailDocumentSettings = jsonb<String>("emailDocumentSettings", { it }, { it })
    val brandingEnabled = bool("brandingEnabled").default(false)
    val brandingLogo = text("brandingLogo").default("")
    val brandingUrl = text("brandingUrl").default("")
    val brandingCompanyDetails = text("brandingCompanyDetails").default("")
    val brandingColors = jsonb<String>("brandingColors", { it }, { it }).nullable()
    val brandingCss = text("brandingCss").default("")
    val envelopeExpirationPeriod = jsonb<String>("envelopeExpirationPeriod", { it }, { it }).nullable()
    val reminderSettings = jsonb<String>("reminderSettings", { it }, { it }).nullable()
    val aiFeaturesEnabled = bool("aiFeaturesEnabled").default(false)
    override val primaryKey = PrimaryKey(id, name = "\"OrganisationGlobalSettings_pkey\"")
}
