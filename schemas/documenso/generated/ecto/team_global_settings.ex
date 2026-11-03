defmodule Documenso.TeamGlobalSettings do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          document_visibility: atom() | nil,
          document_language: String.t() | nil,
          document_timezone: String.t() | nil,
          document_date_format: String.t() | nil,
          delegate_document_ownership: boolean() | nil,
          include_sender_details: boolean() | nil,
          include_signing_certificate: boolean() | nil,
          include_audit_log: boolean() | nil,
          typed_signature_enabled: boolean() | nil,
          upload_signature_enabled: boolean() | nil,
          draw_signature_enabled: boolean() | nil,
          default_recipients: map() | nil,
          email_reply_to: String.t() | nil,
          email_document_settings: map() | nil,
          branding_enabled: boolean() | nil,
          branding_logo: String.t() | nil,
          branding_url: String.t() | nil,
          branding_company_details: String.t() | nil,
          branding_colors: map() | nil,
          branding_css: String.t() | nil,
          envelope_expiration_period: map() | nil,
          reminder_settings: map() | nil,
          ai_features_enabled: boolean() | nil,
          email: Documenso.OrganisationEmail.t() | nil,
          team: Documenso.Team.t() | nil
        }

  schema "TeamGlobalSettings" do
    field(:id, :string, primary_key: true)
    field(:document_visibility, Ecto.Enum, values: [:EVERYONE, :MANAGER_AND_ABOVE, :ADMIN], source: :documentVisibility)
    field(:document_language, :string, source: :documentLanguage)
    field(:document_timezone, :string, source: :documentTimezone)
    field(:document_date_format, :string, source: :documentDateFormat)
    field(:delegate_document_ownership, :boolean, source: :delegateDocumentOwnership)
    field(:include_sender_details, :boolean, source: :includeSenderDetails)
    field(:include_signing_certificate, :boolean, source: :includeSigningCertificate)
    field(:include_audit_log, :boolean, source: :includeAuditLog)
    field(:typed_signature_enabled, :boolean, source: :typedSignatureEnabled)
    field(:upload_signature_enabled, :boolean, source: :uploadSignatureEnabled)
    field(:draw_signature_enabled, :boolean, source: :drawSignatureEnabled)
    field(:default_recipients, :map, source: :defaultRecipients)
    field(:email_reply_to, :string, source: :emailReplyTo)
    field(:email_document_settings, :map, source: :emailDocumentSettings)
    field(:branding_enabled, :boolean, source: :brandingEnabled)
    field(:branding_logo, :string, source: :brandingLogo)
    field(:branding_url, :string, source: :brandingUrl)
    field(:branding_company_details, :string, source: :brandingCompanyDetails)
    field(:branding_colors, :map, source: :brandingColors)
    field(:branding_css, :string, source: :brandingCss)
    field(:envelope_expiration_period, :map, source: :envelopeExpirationPeriod)
    field(:reminder_settings, :map, source: :reminderSettings)
    field(:ai_features_enabled, :boolean, source: :aiFeaturesEnabled)
    field(:email_id, :string, source: :emailId)
    belongs_to(:email, Documenso.OrganisationEmail, foreign_key: :email_id, define_field: false, type: :string)
    has_one(:team, Documenso.Team, foreign_key: :team_global_settings_id)
  end
end