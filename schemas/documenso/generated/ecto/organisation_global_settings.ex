defmodule Documenso.OrganisationGlobalSettings do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          document_visibility: atom(),
          document_language: String.t(),
          include_sender_details: boolean(),
          include_signing_certificate: boolean(),
          include_audit_log: boolean(),
          document_timezone: String.t() | nil,
          document_date_format: String.t(),
          delegate_document_ownership: boolean(),
          typed_signature_enabled: boolean(),
          upload_signature_enabled: boolean(),
          draw_signature_enabled: boolean(),
          default_recipients: map() | nil,
          email_reply_to: String.t() | nil,
          email_document_settings: map(),
          branding_enabled: boolean(),
          branding_logo: String.t(),
          branding_url: String.t(),
          branding_company_details: String.t(),
          branding_colors: map() | nil,
          branding_css: String.t(),
          envelope_expiration_period: map() | nil,
          reminder_settings: map() | nil,
          ai_features_enabled: boolean(),
          email: Documenso.OrganisationEmail.t() | nil,
          organisation: Documenso.Organisation.t() | nil
        }

  schema "OrganisationGlobalSettings" do
    field(:id, :string, primary_key: true)
    field(:document_visibility, Ecto.Enum, values: [:EVERYONE, :MANAGER_AND_ABOVE, :ADMIN], default: :EVERYONE, source: :documentVisibility)
    field(:document_language, :string, default: "en", source: :documentLanguage)
    field(:include_sender_details, :boolean, default: true, source: :includeSenderDetails)
    field(:include_signing_certificate, :boolean, default: true, source: :includeSigningCertificate)
    field(:include_audit_log, :boolean, default: false, source: :includeAuditLog)
    field(:document_timezone, :string, source: :documentTimezone)
    field(:document_date_format, :string, default: "yyyy-MM-dd hh:mm a", source: :documentDateFormat)
    field(:delegate_document_ownership, :boolean, default: false, source: :delegateDocumentOwnership)
    field(:typed_signature_enabled, :boolean, default: true, source: :typedSignatureEnabled)
    field(:upload_signature_enabled, :boolean, default: true, source: :uploadSignatureEnabled)
    field(:draw_signature_enabled, :boolean, default: true, source: :drawSignatureEnabled)
    field(:default_recipients, :map, source: :defaultRecipients)
    field(:email_reply_to, :string, source: :emailReplyTo)
    field(:email_document_settings, :map, source: :emailDocumentSettings)
    field(:branding_enabled, :boolean, default: false, source: :brandingEnabled)
    field(:branding_logo, :string, default: "", source: :brandingLogo)
    field(:branding_url, :string, default: "", source: :brandingUrl)
    field(:branding_company_details, :string, default: "", source: :brandingCompanyDetails)
    field(:branding_colors, :map, source: :brandingColors)
    field(:branding_css, :string, default: "", source: :brandingCss)
    field(:envelope_expiration_period, :map, source: :envelopeExpirationPeriod)
    field(:reminder_settings, :map, source: :reminderSettings)
    field(:ai_features_enabled, :boolean, default: false, source: :aiFeaturesEnabled)
    field(:email_id, :string, source: :emailId)
    belongs_to(:email, Documenso.OrganisationEmail, foreign_key: :email_id, define_field: false, type: :string)
    has_one(:organisation, Documenso.Organisation, foreign_key: :organisation_global_settings_id, references: :id)
  end
end