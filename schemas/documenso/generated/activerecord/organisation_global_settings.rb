class OrganisationGlobalSettings < ApplicationRecord
  self.table_name = "OrganisationGlobalSettings"

  attribute :documentLanguage, default: "en"
  attribute :includeSenderDetails, default: true
  attribute :includeSigningCertificate, default: true
  attribute :includeAuditLog, default: false
  attribute :documentDateFormat, default: "yyyy-MM-dd hh:mm a"
  attribute :delegateDocumentOwnership, default: false
  attribute :typedSignatureEnabled, default: true
  attribute :uploadSignatureEnabled, default: true
  attribute :drawSignatureEnabled, default: true
  attribute :brandingEnabled, default: false
  attribute :brandingLogo, default: ""
  attribute :brandingUrl, default: ""
  attribute :brandingCompanyDetails, default: ""
  attribute :brandingCss, default: ""
  attribute :aiFeaturesEnabled, default: false

  enum :documentVisibility, { everyone: "EVERYONE", manager_and_above: "MANAGER_AND_ABOVE", admin: "ADMIN" },
       default: :everyone, validate: true

  validates :emailDocumentSettings, exclusion: { in: [ nil ] }

  belongs_to :email, class_name: "OrganisationEmail", foreign_key: "emailId", optional: true,
                     inverse_of: :organisation_global_settings
  has_one :organisation, foreign_key: "organisationGlobalSettingsId", inverse_of: :organisation_global_settings,
                         dependent: :restrict_with_error
end
