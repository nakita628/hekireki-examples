package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class OrganisationGlobalSettingsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OrganisationGlobalSettingsEntity>(OrganisationGlobalSettingsTable)

    val organisation by OrganisationEntity optionalBackReferencedOn OrganisationTable.organisationGlobalSettingsId
    var documentVisibility by OrganisationGlobalSettingsTable.documentVisibility
    var documentLanguage by OrganisationGlobalSettingsTable.documentLanguage
    var includeSenderDetails by OrganisationGlobalSettingsTable.includeSenderDetails
    var includeSigningCertificate by OrganisationGlobalSettingsTable.includeSigningCertificate
    var includeAuditLog by OrganisationGlobalSettingsTable.includeAuditLog
    var documentTimezone by OrganisationGlobalSettingsTable.documentTimezone
    var documentDateFormat by OrganisationGlobalSettingsTable.documentDateFormat
    var delegateDocumentOwnership by OrganisationGlobalSettingsTable.delegateDocumentOwnership
    var typedSignatureEnabled by OrganisationGlobalSettingsTable.typedSignatureEnabled
    var uploadSignatureEnabled by OrganisationGlobalSettingsTable.uploadSignatureEnabled
    var drawSignatureEnabled by OrganisationGlobalSettingsTable.drawSignatureEnabled
    var defaultRecipients by OrganisationGlobalSettingsTable.defaultRecipients
    var emailId by OrganisationGlobalSettingsTable.emailId
    var email by OrganisationEmailEntity optionalReferencedOn OrganisationGlobalSettingsTable.emailId
    var emailReplyTo by OrganisationGlobalSettingsTable.emailReplyTo
    var emailDocumentSettings by OrganisationGlobalSettingsTable.emailDocumentSettings
    var brandingEnabled by OrganisationGlobalSettingsTable.brandingEnabled
    var brandingLogo by OrganisationGlobalSettingsTable.brandingLogo
    var brandingUrl by OrganisationGlobalSettingsTable.brandingUrl
    var brandingCompanyDetails by OrganisationGlobalSettingsTable.brandingCompanyDetails
    var brandingColors by OrganisationGlobalSettingsTable.brandingColors
    var brandingCss by OrganisationGlobalSettingsTable.brandingCss
    var envelopeExpirationPeriod by OrganisationGlobalSettingsTable.envelopeExpirationPeriod
    var reminderSettings by OrganisationGlobalSettingsTable.reminderSettings
    var aiFeaturesEnabled by OrganisationGlobalSettingsTable.aiFeaturesEnabled
}
